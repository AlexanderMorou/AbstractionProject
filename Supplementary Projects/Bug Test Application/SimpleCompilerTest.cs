using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using Microsoft.VisualBasic;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Oil.VisualBasic;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.SupplimentaryProjects.BugTestApplication
{
    public static class SimpleCompilerTest 
    {
        private class TypeAggregator :
            IIntermediateCodeVisitor
        {
            #region IExpressionVisitor Members

            public void Visit<TLeft, TRight>(IBinaryOperationExpression<TLeft, TRight> expression)
                where TLeft : INaryOperandExpression
                where TRight : INaryOperandExpression
            {
                var expressionType = expression.Type;
                if (expressionType == ExpressionKinds.BinaryForwardTerm)
                {
                    switch (expression.Associativity)
                    {
                        case BinaryOperationAssociativity.Left:
                            if (expression.RightSide != null)
                                expression.RightSide.Visit(this);
                            break;
                        case BinaryOperationAssociativity.Right:
                            if (expression.LeftSide != null)
                                expression.LeftSide.Visit(this);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    expression.LeftSide.Visit(this);
                    Console.Write(" ");
                    switch (expressionType.BinaryOperations)
                    {
                        case ExpressionKind.BinaryOperationSector.AssignExpression:
                            Console.Write("=");
                            break;
                        case ExpressionKind.BinaryOperationSector.AssignMultiplyOperation:
                            Console.Write("*=");
                            break;
                        case ExpressionKind.BinaryOperationSector.AssignDivideOperation:
                            Console.Write("/=");
                            break;
                        case ExpressionKind.BinaryOperationSector.AssignModulusOperation:
                            Console.Write("%=");
                            break;
                        case ExpressionKind.BinaryOperationSector.AssignAddOperation:
                            Console.Write("+=");
                            break;
                        case ExpressionKind.BinaryOperationSector.AssignSubtractOperation:
                            Console.Write("-=");
                            break;
                        case ExpressionKind.BinaryOperationSector.AssignLeftShiftOperation:
                            Console.Write("<<=");
                            break;
                        case ExpressionKind.BinaryOperationSector.AssignRightShiftOperation:
                            Console.Write(">>=");
                            break;
                        case ExpressionKind.BinaryOperationSector.AssignBitwiseAndOperation:
                            Console.Write("&=");
                            break;
                        case ExpressionKind.BinaryOperationSector.AssignBitwiseOrOperation:
                            Console.Write("|=");
                            break;
                        case ExpressionKind.BinaryOperationSector.AssignBitwiseExclusiveOrOperation:
                            Console.Write("^=");
                            break;
                        case ExpressionKind.BinaryOperationSector.LogicalOrOperation:
                            Console.Write("||");
                            break;
                        case ExpressionKind.BinaryOperationSector.LogicalAndOperation:
                            Console.Write("&&");
                            break;
                        case ExpressionKind.BinaryOperationSector.BitwiseOrOperation:
                            Console.Write("|");
                            break;
                        case ExpressionKind.BinaryOperationSector.BitwiseExclusiveOrOperation:
                            Console.Write("^");
                            break;
                        case ExpressionKind.BinaryOperationSector.BitwiseAndOperation:
                            Console.Write("&");
                            break;
                        case ExpressionKind.BinaryOperationSector.InequalityOperation:
                            Console.Write("!=");
                            break;
                        case ExpressionKind.BinaryOperationSector.EqualityOperation:
                            Console.Write("==");
                            break;
                        case ExpressionKind.BinaryOperationSector.LessThanOperation:
                            Console.Write("<");
                            break;
                        case ExpressionKind.BinaryOperationSector.LessThanOrEqualToOperation:
                            Console.Write("<=");
                            break;
                        case ExpressionKind.BinaryOperationSector.GreaterThanOperation:
                            Console.Write(">");
                            break;
                        case ExpressionKind.BinaryOperationSector.GreaterThanOrEqualToOperation:
                            Console.Write(">=");
                            break;
                        case ExpressionKind.BinaryOperationSector.TypeCheckOperation:
                            Console.Write("is");
                            break;
                        case ExpressionKind.BinaryOperationSector.TypeCastOrNull:
                            Console.Write("as");
                            break;
                        case ExpressionKind.BinaryOperationSector.ShiftLeftOperation:
                            Console.Write("<<");
                            break;
                        case ExpressionKind.BinaryOperationSector.ShiftRightOperation:
                            Console.Write(">>");
                            break;
                        case ExpressionKind.BinaryOperationSector.AddOperation:
                            Console.Write("+");
                            break;
                        case ExpressionKind.BinaryOperationSector.SubtractOperation:
                            Console.Write("-");
                            break;
                        case ExpressionKind.BinaryOperationSector.MultiplyOperation:
                            Console.Write("*");
                            break;
                        case ExpressionKind.BinaryOperationSector.StrictDivisionOperation:
                            Console.Write("/");
                            break;
                        case ExpressionKind.BinaryOperationSector.ModulusOperation:
                            Console.Write("%");
                            break;
                        case ExpressionKind.BinaryOperationSector.FlexibleDivisionOperation:
                            Console.Write("\\");
                            break;
                        case ExpressionKind.BinaryOperationSector.IntegerDivisionOperation:
                            Console.Write("\\");
                            break;
                    }
                    Console.Write(" ");
                    expression.RightSide.Visit(this);
                }
            }

            public void Visit(IIndexerReferenceExpression expression)
            {
                expression.Source.Visit(this);
                Console.Write("[");
                var parameters = expression.Parameters;
                VisitExpressionCollection(parameters);
                Console.Write("]");
            }

            private void VisitExpressionCollection<T>(IExpressionCollection<T> parameters)
                where T :
                    IExpression
            {
                bool first = true;
                foreach (var parameter in parameters)
                {
                    if (first)
                        first = false;
                    else
                        Console.Write(",");
                    parameter.Visit(this);
                }
            }

            public void Visit(IConditionalExpression expression)
            {
                var expressionType = expression.Type;
                if (expressionType.ExpansionsRequired == ExpressionKind.ExpansionRequiredSector.ConditionalForwardTerm)
                {
                    expression.CheckPart.Visit(this);
                }
                else if (expressionType.ExpansionsRequired == ExpressionKind.ExpansionRequiredSector.ConditionalOperation)
                {
                    expression.CheckPart.Visit(this);
                    Console.Write(" ? ");
                    expression.TruePart.Visit(this);
                    Console.Write(" : ");
                    expression.FalsePart.Visit(this);
                }
            }
            
            private static void DiscernOpFlags(UnaryOperation original, out bool bitInvert, out bool boolInvert, out bool negate, out bool postOp, out bool preOp, out bool decrement, out bool increment)
            {
                /* *
                 * Rediscern the flags based upon logic that cannot be expressed
                 * in mere bits.
                 * */
                bitInvert     = ((original & UnaryOperation.BitwiseInversion) == UnaryOperation.BitwiseInversion);
                boolInvert    = ((original & UnaryOperation.BooleanInversion) == UnaryOperation.BooleanInversion) 
                                    && !bitInvert;
                negate        =    ((original & UnaryOperation.SignInversion) == UnaryOperation.SignInversion) 
                                    && !boolInvert;
                postOp        =       ((original & UnaryOperation.PostAction) == UnaryOperation.PostAction);
                preOp         =        ((original & UnaryOperation.PreAction) == UnaryOperation.PreAction) 
                                    && !postOp;
                decrement     =        ((original & UnaryOperation.Decrement) == UnaryOperation.Decrement) 
                                    && (preOp || postOp);
                increment     =        ((original & UnaryOperation.Increment) == UnaryOperation.Increment) 
                                    && !decrement 
                                    && (preOp || postOp);
            }

            public void Visit(IUnaryOperationExpression expression)
            {
                var expressionType = expression.Type;
                bool bitInvert, boolInvert, negate, postOp, preOp, decrement, increment;
                DiscernOpFlags(expression.Operation, out bitInvert, out boolInvert, out negate, out postOp, out preOp, out decrement, out increment);
                if (bitInvert)
                    Console.Write("~");
                else if (boolInvert)
                    Console.Write("!");
                if (negate)
                    Console.Write("-");
                if (preOp)
                {
                    if (decrement)
                        Console.Write("--");
                    else if (increment)
                        Console.Write("++");
                }
                expression.Term.Visit(this);
                if (postOp)
                {
                    if (decrement)
                        Console.Write("--");
                    else if (increment)
                        Console.Write("++");
                }
            }

            public void Visit(ITypeCastExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ITypeOfExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ITypeReferenceExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(IVariadicTypeCastExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ISymbolExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(IStaticGetMemberHandleExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ISpecialReferenceExpression expression)
            {
                switch (expression.Kind)
                {
                    case SpecialReferenceKind.CurrentClass:
                        Console.Write("class");
                        break;
                    case SpecialReferenceKind.Self:
                        Console.Write("self");
                        break;
                    case SpecialReferenceKind.Base:
                        Console.Write("base");
                        break;
                    case SpecialReferenceKind.This:
                        Console.Write("this");
                        break;
                }
            }

            public void Visit(IPropertyReferenceExpression expression)
            {
                expression.Source.Visit(this);
                Console.WriteLine(".{0}", expression.Name);
            }

            public void Visit(IParenthesizedExpression expression)
            {
                Console.Write("(");
                expression.Parenthesized.Visit(this);
                Console.Write(")");
            }

            public void Visit(INamedParameterExpression expression)
            {
                Console.Write("{0}: ", expression.Name);
                expression.Expression.Visit(this);
            }

            public void Visit(IMethodPointerReferenceExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(IMethodInvokeExpression expression)
            {
                expression.Reference.Visit(this);
            }

            public void Visit(ILocalReferenceExpression expression)
            {
                Console.Write(expression.Name);
            }

            public void Visit(IFieldReferenceExpression expression)
            {
                expression.Source.Visit(this);
                Console.Write(expression.Name);
            }

            public void Visit(IExpressionToCommaTypeReferenceFusionExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(IExpressionToCommaFusionExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(IExpressionFusionExpression expression)
            {
                expression.Left.Visit(this);
                Console.Write(".");
                expression.Right.Visit(this);
            }

            public void Visit(IEventInvokeExpression expression)
            {
                Console.Write(expression.Reference.Name);
                Console.Write("(");
                VisitExpressionCollection(expression.Parameters);
                Console.Write(")");
            }

            public void Visit(IDirectionExpression expression)
            {
                switch (expression.Direction)
                {
                    case ParameterDirection.Out:
                        Console.Write("out ");
                        break;
                    case ParameterDirection.Reference:
                        Console.Write("ref ");
                        break;
                }
                if (expression.Directed != null)
                    expression.Directed.Visit(this);
            }

            public void Visit(IDelegateReferenceExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(IDelegateMethodPointerReferenceExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(IDelegateInvokeExpression expression)
            {
                expression.Reference.Visit(this);
            }

            public void Visit(IDelegateHolderReferenceExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ICreateInstanceMemberAssignment expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ICreateInstanceExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ICreateArrayExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ICreateArrayDetailExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ICommaExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(IArrayDimensionDetailExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(IAnonymousMethodWithParametersExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(IAnonymousMethodExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(Slf.Oil.Expressions.Lambda.ILambdaTypedStatementExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(Slf.Oil.Expressions.Lambda.ILambdaTypeInferredStatementExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(Slf.Oil.Expressions.Lambda.ILambdaTypedSimpleExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(Slf.Oil.Expressions.Lambda.ILambdaTypeInferredSimpleExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(IParameterReferenceExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(IConstructorInvokeExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(IConstructorPointerReferenceExpression ctorPointerReference)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqExpression expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(IAssignmentExpression expression)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region ILinqVisitor Members

            public void Visit(ILinqSelectBody expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqGroupBody expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqFusionSelectBody expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqFusionGroupBody expression)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqDirectedOrderByClause linqClause)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqDirectedOrderByGroupClause linqClause)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqFromClause linqClause)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqJoinClause linqClause)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqLetClause linqClause)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqOrderByClause linqClause)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqOrderByGroupClause linqClause)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqTypedFromClause linqClause)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqTypedJoinClause linqClause)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILinqWhereClause linqClause)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IIntermediatePrimitiveVisitor Members

            public void Visit(IPrimitiveExpression<bool> expression)
            {
                switch (expression.Value)
                {
                    case true:
                        Console.Write("true");
                        break;
                    case false:
                        Console.Write("false");
                        break;
                }
            }

            public void Visit(IPrimitiveExpression<char> expression)
            {
                Console.Write(expression.Value.ToString().EscapeStringOrCharCILAndCS(false));
            }

            public void Visit(IPrimitiveExpression<string> expression)
            {
                Console.Write(expression.Value.EscapeStringOrCharCILAndCS());
            }

            public void Visit(IPrimitiveExpression<byte> expression)
            {
                Console.Write("(byte)({0})", expression.Value);
            }

            public void Visit(IPrimitiveExpression<sbyte> expression)
            {
                Console.Write("(sbyte)({0})", expression.Value);
            }

            public void Visit(IPrimitiveExpression<ushort> expression)
            {
                Console.Write("(ushort)({0})", expression.Value);
            }

            public void Visit(IPrimitiveExpression<short> expression)
            {
                Console.Write("(short)({0})", expression.Value);
            }

            public void Visit(IPrimitiveExpression<uint> expression)
            {
                Console.Write("{0}U", expression.Value);
            }

            public void Visit(IPrimitiveExpression<int> expression)
            {
                Console.Write(expression.Value);
            }

            public void Visit(IPrimitiveExpression<ulong> expression)
            {
                Console.Write("{0}UL", expression.Value);
            }

            public void Visit(IPrimitiveExpression<long> expression)
            {
                Console.Write("{0}L", expression.Value);
            }

            public void Visit(IPrimitiveExpression<float> expression)
            {
                Console.Write("{0}F", expression.Value);
            }

            public void Visit(IPrimitiveExpression<double> expression)
            {
                Console.Write("{0}D", expression.Value);
            }

            public void Visit(IPrimitiveExpression<decimal> expression)
            {
                Console.Write("{0}M", expression.Value);
            }

            public void VisitNull()
            {
                Console.Write("null");
            }

            #endregion

            #region IStatementVisitor Members

            public void Visit(IBlockStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IBreakStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(ICallMethodStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IConditionBlockStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(ICallFusionStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IConditionContinuationStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IEnumerationBlockStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IExplicitlyTypedLocalVariableDeclarationStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IExpressionStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IGoToStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IJumpTarget statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IIterationBlockStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IJumpStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILabelStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(IReturnStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(ISimpleIterationBlockStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(ISwitchCaseBlockStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(ISwitchStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(ITryCatchStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(ITryStatement statement)
            {
                throw new NotImplementedException();
            }

            public void Visit(ILocalDeclarationStatement statement)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IIntermediateDeclarationVisitor Members

            public void Visit(IIntermediateAssembly assembly)
            {
                throw new NotImplementedException();
            }

            public void Visit(IIntermediateNamespaceDeclaration @namespace)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IIntermediateTypeVisitor Members

            public void Visit(IIntermediateClassType @class)
            {
                throw new NotImplementedException();
            }

            public void Visit(IIntermediateDelegateType @delegate)
            {
                throw new NotImplementedException();
            }

            public void Visit(IIntermediateEnumType @enum)
            {
                throw new NotImplementedException();
            }

            public void Visit(IIntermediateInterfaceType @interface)
            {
                throw new NotImplementedException();
            }

            public void Visit(IIntermediateStructType @struct)
            {
                throw new NotImplementedException();
            }

            public void Visit<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> parameter)
                where TGenericParameter : IGenericParameter<TGenericParameter, TParent>
                where TIntermediateGenericParameter : TGenericParameter, IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
                where TParent : IGenericParamParent<TGenericParameter, TParent>
                where TIntermediateParent : TParent, IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IIntermediateMemberVisitor Members

            public void Visit(ILocalMember local)
            {
                throw new NotImplementedException();
            }

            public void Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
                where TCtor : IConstructorMember<TCtor, TType>
                where TIntermediateCtor : TCtor, IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
                where TType : ICreatableType<TCtor, TType>
                where TIntermediateType : TType, IIntermediateCreatableSignatureType<TCtor, TIntermediateCtor, TType, TIntermediateType>
            {
                throw new NotImplementedException();
            }

            public void Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
                where TCtor : IConstructorMember<TCtor, TType>
                where TIntermediateCtor : TCtor, IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
                where TType : ICreatableType<TCtor, TType>
                where TIntermediateType : TType, IIntermediateCreatableType<TCtor, TIntermediateCtor, TType, TIntermediateType>
            {
                throw new NotImplementedException();
            }

            public void Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
                where TEvent : IEventMember<TEvent, TEventParent>
                where TIntermediateEvent : TEvent, IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
                where TEventParent : IEventParent<TEvent, TEventParent>
                where TIntermediateEventParent : TEventParent, IIntermediateEventParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
                where TEvent : IEventSignatureMember<TEvent, TEventParent>
                where TIntermediateEvent : TEvent, IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
                where TEventParent : IEventSignatureParent<TEvent, TEventParent>
                where TIntermediateEventParent : TEventParent, IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TCoercionParent>(IBinaryOperatorCoercionMember<TCoercionParent> binaryCoercion) where TCoercionParent : ICoercibleType<IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TCoercionParent>(ITypeCoercionMember<TCoercionParent> typeCoercion) where TCoercionParent : ICoercibleType<ITypeCoercionMember<TCoercionParent>, TCoercionParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TCoercionParent>(IUnaryOperatorCoercionMember<TCoercionParent> unaryCoercion) where TCoercionParent : ICoercibleType<IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>(IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> field)
                where TField : IFieldMember<TField, TFieldParent>
                where TIntermediateField : TField, IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
                where TFieldParent : IFieldParent<TField, TFieldParent>
                where TIntermediateFieldParent : TFieldParent, IIntermediateFieldParent<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
            {
                throw new NotImplementedException();
            }

            public void Visit(IIntermediateEnumFieldMember field)
            {
                throw new NotImplementedException();
            }

            public void Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexer)
                where TIndexer : IIndexerMember<TIndexer, TIndexerParent>
                where TIntermediateIndexer : TIndexer, IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
                where TIndexerParent : IIndexerParent<TIndexer, TIndexerParent>
                where TIntermediateIndexerParent : TIndexerParent, IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexerSignature)
                where TIndexer : IIndexerSignatureMember<TIndexer, TIndexerParent>
                where TIntermediateIndexer : TIndexer, IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
                where TIndexerParent : IIndexerSignatureParent<TIndexer, TIndexerParent>
                where TIntermediateIndexerParent : TIndexerParent, IIntermediateIndexerSignatureParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>(IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> method)
                where TMethod : IMethodMember<TMethod, TMethodParent>
                where TIntermediateMethod : IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethod
                where TMethodParent : IMethodParent<TMethod, TMethodParent>
                where TIntermediateMethodParent : IIntermediateMethodParent<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethodParent
            {
                throw new NotImplementedException();
            }

            public void Visit<TSignature, TIntermediateSignature, TParent, TIntermediateParent>(IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> methodSignature)
                where TSignature : IMethodSignatureMember<TSignature, TParent>
                where TIntermediateSignature : TSignature, IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
                where TParent : IMethodSignatureParent<TSignature, TParent>
                where TIntermediateParent : TParent, IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> propertySignature)
                where TProperty : IPropertySignatureMember<TProperty, TPropertyParent>
                where TIntermediateProperty : TProperty, IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
                where TPropertyParent : IPropertySignatureParentType<TProperty, TPropertyParent>
                where TIntermediatePropertyParent : TPropertyParent, IIntermediatePropertySignatureParentType<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> property)
                where TProperty : IPropertyMember<TProperty, TPropertyParent>
                where TIntermediateProperty : TProperty, IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
                where TPropertyParent : IPropertyParentType<TProperty, TPropertyParent>
                where TIntermediatePropertyParent : TPropertyParent, IIntermediatePropertyParentType<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
            {
                throw new NotImplementedException();
            }

            public void Visit<TParent, TIntermediateParent>(IIntermediateParameterMember<TParent, TIntermediateParent> parameter)
                where TParent : IParameterParent
                where TIntermediateParent : TParent, IIntermediateParameterParent
            {
                throw new NotImplementedException();
            }

            #endregion

            #region IIntermediateInclusionVisitor Members

            public void Visit(INamedInclusionScopeCoercion namedInclusion)
            {
                throw new NotImplementedException();
            }

            public void Visit(INamedInclusionRenameScopeCoercion renamedInclusion)
            {
                throw new NotImplementedException();
            }

            public void Visit(INamespaceInclusionScopeCoercion namespaceInclusion)
            {
                throw new NotImplementedException();
            }

            public void Visit(INamespaceInclusionRenameScopeCoercion renamedNamespaceInclusion)
            {
                throw new NotImplementedException();
            }

            public void Visit(ITypeInclusionScopeCoercion typeInclusion)
            {
                throw new NotImplementedException();
            }

            public void Visit(ITypeInclusionRenameScopeCoercion renamedTypeInclusion)
            {
                throw new NotImplementedException();
            }

            public void Visit(IStaticInclusionScopeCoercion staticInclusion)
            {
                throw new NotImplementedException();
            }

            #endregion
        }
        internal static void Main(string[] args)
        {

        }

    }
}
