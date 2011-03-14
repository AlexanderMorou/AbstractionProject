using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast.Expressions.Linq
{
    internal abstract class LinqTailBodyBuilder :
        ILinqTailBodyBuilder,
        ILinqBodyBuilderParent
    {
        public LinqTailBodyBuilder(ILinqBodyBuilderParent parent)
        {
            this.Parent = parent;
        }

        public ILinqBodyBuilderParent Parent { get; private set; }

        #region ILinqTailBodyBuilder Members

        public ILinqFusionBodyBuilder Into(string intoRangeName)
        {
            return new LinqFusionBodyBuilder(this, intoRangeName);
        }

        public ILinqExpression Build()
        {
            var path = new Stack<ILinqBodyBuilderParent>();
            //Build a reverse-order listing of the current expression graph.
            for (ILinqBodyBuilderParent current = this; current != null; path.Push(current), current = current.Parent) 
                ;

            ILinqExpression result = new LinqExpression();
            int bodyCount = 0;
            var pathNodes = path.ToArray();

            const int selectBody = 1,
                      selectFuseBody = 3;
            const int groupBody = 2,
                      groupFuseBody = 4;
            //Count the number of body continuations.
            for (int i = 0; i < pathNodes.Length; i++)
                if (pathNodes[i] is ILinqTailBodyBuilder)
                    bodyCount++;

            int[] bodyTypes = new int[bodyCount];
            int bodyIndex = 0;

            /* *
             * Iterate through and, from the tails, discern
             * the kind of body to use.
             * */
            for (int i = 0; i < pathNodes.Length; i++)
                if (pathNodes[i] is ILinqTailBodyBuilder)
                {
                    bool last = i==pathNodes.Length - 1;

                    if (pathNodes[i] is LinqTailGroupBodyBuilder)
                        bodyTypes[bodyIndex++] = last ? groupBody : groupFuseBody;
                    else
                        bodyTypes[bodyIndex++] = last ? selectBody : selectFuseBody;
                }
            ILinqBody currentBody = null;
            bodyIndex = 0;
            var selector = pathNodes[0] as LinqTypedFromBodyBuilder;
            /* *
             * The first element is always a from clause, either typed or 
             * untyped.
             * *
             * This is why initialization pipeline is closed.
             * */
            if (selector == null)
                result.From = BuildFromClause(pathNodes[0] as LinqFromBodyBuilder);
            else
                result.From = BuildFromClause(selector);
            /* *
             * Iterate through the elements and build the bodies, 
             * linking the clauses in between.
             * */
            for (int i = 1; i < pathNodes.Length; i++)
            {
                bool last = i == pathNodes.Length - 1;
                /* *
                 * Create the initial body from the current element.
                 * */
                if (currentBody == null)
                {
                    currentBody = result.Body = GetBody(bodyTypes[bodyIndex++]);
                    if (currentBody == null)
                        throw new InvalidOperationException("Unknown error.");
                }
                /* *
                 * This breaks programming practice rules, bite me.
                 * */
                var fromClauseT = pathNodes[i] as LinqTypedFromBodyBuilder;
                if (fromClauseT != null)
                {
                    currentBody.Clauses.From(fromClauseT.RangeVariableInfo, fromClauseT.RangeSource);
                    continue;
                }
                var fromClause = pathNodes[i] as LinqFromBodyBuilder;
                if (fromClause != null)
                {
                    currentBody.Clauses.From(fromClause.RangeVariableName, fromClause.RangeSource);
                    continue;
                }
                var joinClauseT = pathNodes[i] as LinqTypedJoinBodyBuilder;
                if (joinClauseT != null)
                {
                    currentBody.Clauses.Join(joinClauseT.RangeVariableInfo, joinClauseT.RangeSource, joinClauseT.LeftConditionSelector, joinClauseT.RightConditionSelector, joinClauseT.IntoTarget);
                    continue;
                }
                var joinClause = pathNodes[i] as LinqJoinBodyBuilder;
                if (joinClause != null)
                {
                    currentBody.Clauses.Join(joinClause.RangeVariableName, joinClause.RangeSource, joinClause.LeftConditionSelector, joinClause.RightConditionSelector, joinClause.IntoTarget);                    
                    continue;
                }
                var letClause = pathNodes[i] as LinqLetBodyBuilder;
                if (letClause != null)
                {
                    currentBody.Clauses.Let(letClause.RangeVariableName, letClause.RangeSource);
                    continue;
                }
                var dOrderClause = pathNodes[i] as LinqDirectedOrderByBodyBuilder;
                if (dOrderClause != null)
                {
                    currentBody.Clauses.OrderBy(dOrderClause.OrderKey, dOrderClause.Direction);
                    continue;
                }
                var orderClause = pathNodes[i] as LinqOrderByBodyBuilder;
                if (orderClause != null)
                {
                    currentBody.Clauses.OrderBy(orderClause.OrderKey);
                    continue;
                }
                var whereClause = pathNodes[i] as LinqWhereBodyBuilder;
                if (whereClause != null)
                {
                    currentBody.Clauses.Where(whereClause.BooleanCondition);
                    continue;
                }
                var groupBuilder = pathNodes[i] as LinqTailGroupBodyBuilder;
                LinqTailSelectBodyBuilder selectBuilder;
                /* *
                 * If we're working with a group builder...
                 * */
                if (groupBuilder != null)
                {
                    var gbCurrent = currentBody as ILinqGroupBody;
                    if (gbCurrent == null)
                        break;
                    /* *
                     * No continuation, we're finished.
                     * */
                    if (last)
                    {
                        gbCurrent.Key = groupBuilder.Key;
                        gbCurrent.Selection = groupBuilder.Selection;
                        break;
                    }
                    else
                    {
                        /* *
                         * Link the current to the next, and set its
                         * target 'into' range variable name.
                         * */
                        var gfbCurrent = gbCurrent as ILinqFusionGroupBody;
                        if (gfbCurrent == null)
                            break;
                        gfbCurrent.Key = groupBuilder.Key;
                        gfbCurrent.Selection = groupBuilder.Selection;
                        var next = pathNodes[++i] as LinqFusionBodyBuilder;
                        if (next == null)
                            break;
                        gfbCurrent.Target.Name = next.IntoRangeName;
                        gfbCurrent.Next = currentBody = GetBody(bodyTypes[bodyIndex++]);
                    }
                }
                else if ((selectBuilder = pathNodes[i] as LinqTailSelectBodyBuilder) != null)
                {
                    var sbCurrent = currentBody as ILinqSelectBody;
                    if (sbCurrent == null)
                        break;
                    /* *
                     * No continuation, we're finished.
                     * */
                    if (last)
                    {
                        sbCurrent.Selection = selectBuilder.Selection;
                        break;
                    }
                    else
                    {
                        /* *
                         * Link the current to the next, and set its
                         * target 'into' range variable name.
                         * */
                        var sfbCurrent = sbCurrent as ILinqFusionSelectBody;
                        if (sfbCurrent == null)
                            break;
                        sfbCurrent.Selection = selectBuilder.Selection;
                        var next = pathNodes[++i] as LinqFusionBodyBuilder;
                        if (next == null)
                            break;
                        sfbCurrent.Target.Name = next.IntoRangeName;
                        sfbCurrent.Next = currentBody = GetBody(bodyTypes[bodyIndex++]);
                    }
                }
            }
            return result;
        }

        private ILinqBody GetBody(int p)
        {
            const int selectBody = 1,
                      selectFuseBody = 3;
            const int groupBody = 2,
                      groupFuseBody = 4;
            switch (p)
            {
                case selectBody:
                    return new LinqSelectBody();
                case selectFuseBody:
                    return new LinqFusionSelectBody();
                case groupBody:
                    return new LinqGroupBody();
                case groupFuseBody:
                    return new LinqFusionGroupBody();
            }
            return null;
        }


        #endregion

        private static ILinqFromClause BuildFromClause(LinqFromBodyBuilder builder)
        {
            return new LinqFromClause(builder.RangeVariableName, builder.RangeSource);
        }

        private static ILinqTypedFromClause BuildFromClause(LinqTypedFromBodyBuilder builder)
        {
            return new LinqTypedFromClause(builder.RangeVariableInfo, builder.RangeSource);
        }
    }
}
