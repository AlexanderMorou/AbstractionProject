using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public partial class CallParameterSet :
        ICallParameterSet
    {
        private IMalleableExpressionCollection indexedParameters;
        private ICallNamedParameterDictionary namedParameters;
        private IList<IExpression> verbatimOrder = new List<IExpression>();

        #region ICallParameterSet Members

        /// <summary>
        /// Returns the <see cref="IMalleableExpressionCollection"/> 
        /// associated to the <see cref="CallParameterSet"/> which
        /// denotes the parameters passed verbatim.
        /// </summary>
        public IMalleableExpressionCollection IndexedParameters
        {
            get {
                if (this.indexedParameters == null)
                    this.indexedParameters = new IndexedParameterCollection(this);
                return this.indexedParameters;
            }
        }

        /// <summary>
        /// Returns the <see cref="ICallNamedParameterDictionary"/> associated to the
        /// <see cref="CallParameterSet"/> which denotes the parameters
        /// passed via their names.
        /// </summary>
        public ICallNamedParameterDictionary NamedParameters
        {
            get {
                if (this.namedParameters == null)
                    this.namedParameters = new CallNamedParameterDictionary();
                return this.namedParameters;
            }
        }


        /// <summary>
        /// Returns/sets whether the <see cref="CallParameterSet"/>
        /// should track insertion order and to what level.
        /// </summary>
        /// <remarks><para>Regardless of implementation order should always
        /// be tracked, but this is more for the compiler to determine
        /// whether a warning/error needs emitted associated to invalid 
        /// name/index ordered insertions.</para>
        /// <para>Primary reason for this is to ensure that code-gen produces
        /// workable code in cases where the order is ignored but functionally
        /// accurate.</para></remarks>
        public OrderTrackingMethod TrackingMethod { get; set; }

        public INamedParameterExpression Add(string parameterName, IExpression expression)
        {
            var result = new NamedParameterExpression(parameterName, expression);
            this.Add(result);
            return result;
        }

        public void Add(INamedParameterExpression expression)
        {
            this.NamedParameters.Add(expression);
        }

        public void Add(IExpression expression)
        {
            if (expression is INamedParameterExpression)
                this.Add((INamedParameterExpression)expression);
            else
                this.IndexedParameters.Add(expression);
        }

        public void AddRange(IExpression[] expressions)
        {
            foreach (var item in expressions)
                this.Add(item);
        }

        public void AddRange(IExpressionCollection expressions)
        {
            foreach (var item in expressions)
                this.Add(item);
        }
        public void AddRange<T>(IExpressionCollection<T> expressions)
            where T :
                IExpression
        {
            foreach (var item in expressions)
                this.Add(item);
        }

        #endregion

        #region IEnumerable<IExpression> Members

        public IEnumerator<IExpression> GetEnumerator()
        {
            return this.verbatimOrder.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return this.GetEnumerator();
        }

        #endregion

    }
}
