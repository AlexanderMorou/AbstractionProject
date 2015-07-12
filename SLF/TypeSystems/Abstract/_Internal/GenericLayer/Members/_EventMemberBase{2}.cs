using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract partial class _EventMemberBase<TEvent, TEventParent> :
        _EventSignatureMemberBase<TEvent, IEventParameterMember<TEvent, TEventParent>, TEventParent>,
        IEventMember<TEvent, TEventParent>
        where TEvent :
            IEventMember<TEvent, TEventParent>
        where TEventParent :
            IEventParent<TEvent, TEventParent>
    {
        private IMethodMember onAddMethod;
        private IMethodMember _onAddMethod;
        private IMethodMember onRemoveMethod;
        private IMethodMember _onRemoveMethod;
        private IMethodMember onRaiseMethod;
        private IMethodMember _onRaiseMethod;
        protected _EventMemberBase(TEvent original, TEventParent adjustedParent)
            : base(original, adjustedParent)
        { 
        }

        #region IEventMember Members

        public IMethodMember OnAddMethod
        {
            get
            {
                return (IMethodMember)base.OnAddMethod;
            }
        }

        public IMethodMember OnRemoveMethod
        {
            get
            {
                return (IMethodMember)base.OnRemoveMethod;
            }
        }

        public IMethodMember OnRaiseMethod
        {
            get
            {
                if (this.onRaiseMethod == null)
                {
                    IMethodMember origOnRaiseMethod = this.Original.OnRaiseMethod;
                    _onRaiseMethod = origOnRaiseMethod;
                    _onRaiseMethod.Disposed += new EventHandler(_onRaiseMethod_Disposed);
                    onRaiseMethod = this.OnGetMethod(Original.OnRaiseMethod);
                }
                return this.onRaiseMethod;
            }
        }

        void _onRaiseMethod_Disposed(object sender, EventArgs e)
        {
            if (this._onRaiseMethod != null)
            {
                _onRaiseMethod.Disposed -= new EventHandler(_onRaiseMethod_Disposed);
                this._onRaiseMethod = null;
            }
            if (this.onRaiseMethod != null)
            {
                this.onRaiseMethod.Dispose();
                this.onRaiseMethod = null;
            }
        }
        public bool CanRaise
        {
            get { return Original.CanRaise; }
        }

        #endregion

        #region IExtendedInstanceMember Members

        public ExtendedMemberAttributes Attributes
        {
            get { return this.Original.Attributes; }
        }

        public bool IsStatic
        {
            get { return this.Original.IsStatic; }
        }

        public bool IsAbstract
        {
            get { return this.Original.IsAbstract; }
        }

        public bool IsVirtual
        {
            get { return this.Original.IsVirtual; }
        }

        public bool IsHideBySignature
        {
            get { return this.Original.IsHideBySignature; }
        }

        public bool IsFinal
        {
            get { return this.Original.IsFinal; }
        }

        public bool IsOverride
        {
            get { return this.Original.IsOverride; }
        }

        #endregion

        #region IScopedDeclaration Members

        public AccessLevelModifiers AccessLevel
        {
            get { return this.Original.AccessLevel; }
        }

        #endregion

        protected override IParameterMemberDictionary<TEvent, IEventParameterMember<TEvent, TEventParent>> InitializeParameters()
        {
            return new _Parameters(this.Original.Parameters, this);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override IMethodSignatureMember OnGetMethod(IMethodSignatureMember original)
        {
            return this.OnGetMethod(((IMethodMember)(original)));
        }

        protected abstract IMethodMember OnGetMethod(IMethodMember original);

        #region IInstanceMember Members

        InstanceMemberAttributes IInstanceMember.Attributes
        {
            get { return (InstanceMemberAttributes)this.Attributes; }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("event {0}", this.UniqueIdentifier.ToString(this.Parent.ToString()));
        }

    }
}
