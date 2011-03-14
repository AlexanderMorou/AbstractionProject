using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _ClassTypeBase
    {
        private class _IndexersBase :
            _IndexerMembersBase<IClassIndexerMember, IClassType>
        {
            private _ClassTypeBase _Parent
            {
                get
                {
                    return ((_ClassTypeBase)(base.Parent));
                }
            }

            public _IndexersBase(_FullMembersBase master, IIndexerMemberDictionary<IClassIndexerMember, IClassType> originalSet, _ClassTypeBase parent)
                : base(master, originalSet, parent)
            {
            }

            private class _IndexerMember :
                _IndexerMemberBase<IClassIndexerMember, IClassType>,
                IClassIndexerMember
            {
                internal _IndexerMember(IClassIndexerMember original, _ClassTypeBase parent)
                    : base(original, parent)
                {
                }
                private class _MethodMember :
                    _MethodsBase._Method,
                    IPropertyMethodMember
                {
                    internal _MethodMember(IClassType parent, IClassMethodMember method)
                        : base(parent, method)
                    {
                    }

                    private IPropertyMethodMember _Original { get { return (IPropertyMethodMember)base.Original; } }
                    #region IPropertySignatureMethodMember Members

                    public PropertyMethodType MethodType
                    {
                        get { return _Original.MethodType; }
                    }

                    #endregion

                }

                protected override IPropertyMethodMember OnGetMethod(IMethodMember originalMethod)
                {
                    return new _MethodMember(this.Parent, (IClassMethodMember)originalMethod);
                }

                public override string UniqueIdentifier
                {
                    get {
                        return string.Format("{0}[{1}]", this.Name, string.Join(",", this.Parameters.Values)); }
                }
            }

            protected override IClassIndexerMember ObtainWrapper(IClassIndexerMember item)
            {
                return new _IndexerMember(item, this._Parent);
            }
        }
    }
}
