using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public enum OrderTrackingMethod
    {
        /// <summary>
        /// The insertion order is vital and if an indexed parameter
        /// is inserted after a named parameter, a compiler error
        /// should be thrown.
        /// </summary>
        InsertionOrderProper,
        /// <summary>
        /// Denotes that full reordering should occur and if an indexed 
        /// parameter is inserted after a named parameter, no compiler error is
        /// generated, but the expressions of the parameter set should be
        /// executed in the order in which they are stated.
        /// The order in which they are passed is expected, all
        /// indexed parameters then all named parameters.
        /// </summary>
        InsertionOrderFullReorder,
        /// <summary>
        /// Denotes that relevant reordering should occur and 
        /// named parameters should be executed in the order in which
        /// they were received; however, no named parameters should be
        /// evaluated prior to the indexed parameters.
        /// </summary>
        InsertionOrderRelevantReorder,
        /// <summary>
        /// The insertion order is irrelevant and improper mixes of
        /// named and indexed parameters don't require ordering rewrites.
        /// Neither named parameters nor indexed parameters require local
        /// variable caches to ensure the reordering, required by linking,
        /// to execute properly.
        /// </summary>
        InsertionOrderIrrelevant,
    }

    /// <summary>
    /// Defines properties and methods for working with a call
    /// parameter set.
    /// </summary>
    public interface ICallParameterSet :
        IEnumerable<IExpression>
    {
        /// <summary>
        /// Returns the <see cref="IMalleableExpressionCollection"/> 
        /// associated to the <see cref="ICallParameterSet"/> which
        /// denotes the parameters passed verbatim.
        /// </summary>
        IMalleableExpressionCollection IndexedParameters { get; }
        /// <summary>
        /// Returns the <see cref="ICallNamedParameterDictionary"/> associated to the
        /// <see cref="ICallParameterSet"/> which denotes the parameters
        /// passed via their names.
        /// </summary>
        ICallNamedParameterDictionary NamedParameters { get; }
        /// <summary>
        /// Returns/sets whether the <see cref="ICallParameterSet"/>
        /// should track insertion order and to what level.
        /// </summary>
        /// <remarks><para>Regardless of implementation order should always
        /// be tracked, but this is more for the compiler to determine
        /// whether a warning/error needs emitted associated to invalid 
        /// name/index ordered insertions.</para>
        /// <para>Primary reason for this is to ensure that code-gen produces
        /// workable code in cases where the order is ignored but functionally
        /// accurate.</para></remarks>
        OrderTrackingMethod TrackingMethod { get; set; }
        /// <summary>
        /// Adds a <see cref="INamedParameterExpression"/> to the
        /// <see cref="NamedParameters"/> with the
        /// <paramref name="parameterName"/> and <paramref name="expression"/>
        /// provided.
        /// </summary>
        /// <param name="parameterName">The <see cref="String"/> value
        /// representing the name of the parameter the
        /// <see cref="INamedParameterExpression"/> refers to.</param>
        /// <param name="expression">The <see cref="IExpression"/>
        /// wrapped by the <see cref="INamedParameterExpression"/> which
        /// is to be executed.</param>
        /// <returns></returns>
        INamedParameterExpression Add(string parameterName, IExpression expression);
        /// <summary>
        /// Adds a <see cref="INamedParameterExpression"/> directly to the
        /// <see cref="NamedParameters"/>.
        /// </summary>
        /// <param name="expression">The <see cref="INamedParameterExpression"/>
        /// to add.</param>
        void Add(INamedParameterExpression expression);
        /// <summary>
        /// Adds a <see cref="IExpression"/> directly to the <see cref="IndexedParameters"/>.
        /// </summary>
        /// <param name="expression">The <see cref="IExpression"/>
        /// to add.</param>
        void Add(IExpression expression);
        /// <summary>
        /// Adds a series of <see cref="IExpression"/> instances to the 
        /// <see cref="ICallParameterSet"/>.
        /// </summary>
        /// <param name="expressions">The <see cref="IExpression"/>
        /// series to add to the <see cref="ICallParameterSet"/>.</param>
        void AddRange(IExpression[] expressions);
        /// <summary>
        /// Adds a <see cref="IExpressionCollection"/> to the 
        /// <see cref="ICallParameterSet"/>.
        /// </summary>
        /// <param name="expressions">The <see cref="IExpressionCollection"/>
        /// to add to the <see cref="ICallParameterSet"/>.</param>
        void AddRange(IExpressionCollection expressions);
    }
}
