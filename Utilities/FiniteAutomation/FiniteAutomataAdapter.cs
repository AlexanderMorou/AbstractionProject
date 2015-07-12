using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.FiniteAutomata
{
    public abstract class FiniteAutomataAdapter<TCheck, TState, TForwardNodeTarget, TSourceElement, TAdapter, TForwardAdapterTarget, TAdapterContext> :
        IFiniteAutomataAdapter<TCheck, TState, TForwardNodeTarget, TSourceElement, TAdapter, TForwardAdapterTarget, TAdapterContext>
        where TCheck :
            IFiniteAutomataSet<TCheck>,
            new()
        where TState :
            IFiniteAutomataState<TCheck, TState, TForwardNodeTarget, TSourceElement>
        where TSourceElement :
            IFiniteAutomataSource
        where TAdapter :
            IFiniteAutomataAdapter<TCheck, TState, TForwardNodeTarget, TSourceElement, TAdapter, TForwardAdapterTarget, TAdapterContext>,
            new()
        where TAdapterContext :
            new()
    {
        private IFiniteAutomataTransitionTable<TCheck, TAdapter, TForwardAdapterTarget> outgoingTransitions;
        protected FiniteAutomataAdapter()
        {
        }

        public TState AssociatedState { get; internal set; }

        public TAdapterContext AssociatedContext { get; internal set; }

        public IFiniteAutomataTransitionTable<TCheck, TAdapter, TForwardAdapterTarget> OutgoingTransitions
        {
            get {
                return this.outgoingTransitions ?? (this.outgoingTransitions = this.InitializeOutgoingTransitions());
            }
        }

        protected abstract IFiniteAutomataTransitionTable<TCheck, TAdapter, TForwardAdapterTarget> InitializeOutgoingTransitions();
        public abstract void ConnectContext(object connectContext);

    }
}
