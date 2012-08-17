using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _StructTypeBase
    {
        private class _IndexersBase :
            _IndexerMembersBase<IStructIndexerMember, IStructType>
        {
            private _StructTypeBase _Parent
            {
                get
                {
                    return ((_StructTypeBase)(base.Parent));
                }
            }

            public _IndexersBase(_FullMembersBase master, IIndexerMemberDictionary<IStructIndexerMember, IStructType> originalSet, _StructTypeBase parent)
                : base(master, originalSet, parent)
            {
            }

            private class _IndexerMember :
                _IndexerMemberBase<IStructIndexerMember, IStructType>,
                IStructIndexerMember
            {
                internal _IndexerMember(IStructIndexerMember original, _StructTypeBase parent)
                    : base(original, parent)
                {
                }
                private class _MethodMember :
                    _MethodsBase._Method,
                    IPropertyMethodMember
                {
                    internal _MethodMember(IStructType parent, IStructMethodMember method)
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
                    return new _MethodMember(this.Parent, (IStructMethodMember)originalMethod);
                }

                public override IGeneralSignatureMemberUniqueIdentifier UniqueIdentifier
                {
                    get
                    {
                        return AstIdentifier.GetSignatureIdentifier(this.Name, this.Parameters.ParameterTypes);
                    }
                }
            }

            protected override IStructIndexerMember ObtainWrapper(IStructIndexerMember item)
            {
                return new _IndexerMember(item, this._Parent);
            }
        }
    }
}
