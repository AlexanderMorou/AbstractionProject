using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    /// <summary>
    /// Defines properties and methods for working with an adapter
    /// for a deterministic finite automation to store additional
    /// information or adapt additional functionality beyond the
    /// scope of the initial automation.
    /// </summary>
    /// <typeparam name="TCheck">The type of set used
    /// to represent the transition from state set to state set.</typeparam>
    /// <typeparam name="TNFAState">The kind of 
    /// <see cref="INFAState{TCheck, TNFAState, TDFAState, TSourceElement}"/>
    /// used to represent the non-deterministic elements of the
    /// automation.</typeparam>
    /// <typeparam name="TDFAState">The type used to construct
    /// a deterministic model of the current nondeterministic 
    /// automation.</typeparam>
    /// <typeparam name="TSourceElement">The kind of elements from 
    /// which the 
    /// <see cref="IDFAState{TCheck, TNFAState, TDFAState, TSourceElement}"/>
    /// was derived</typeparam>
    /// <typeparam name="TAdapter"></typeparam>
    /// <typeparam name="TAdapterContext"></typeparam>
    public interface IDFAAdapter<TCheck, TNFAState, TDFAState, TSourceElement, TAdapter, TAdapterContext> :
        IFiniteAutomataAdapter<TCheck, TDFAState, TDFAState, TSourceElement, TAdapter, TAdapter, TAdapterContext>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TNFAState :
            INFAState<TCheck, TNFAState, TDFAState, TSourceElement>
        where TDFAState :
            IDFAState<TCheck, TNFAState, TDFAState, TSourceElement>,
            new()
        where TSourceElement :
            IFiniteAutomataSource
        where TAdapter :
            IFiniteAutomataAdapter<TCheck, TDFAState, TDFAState, TSourceElement, TAdapter, TAdapter, TAdapterContext>,
            new()
        where TAdapterContext :
            new()
    {
    }
}
