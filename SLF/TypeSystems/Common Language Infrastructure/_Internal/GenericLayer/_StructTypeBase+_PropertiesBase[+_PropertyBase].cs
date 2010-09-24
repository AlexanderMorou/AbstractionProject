using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _StructTypeBase
    {
        private class _PropertiesBase :
            _PropertyMembersBase<IStructPropertyMember, IStructType>
        {
            public _PropertiesBase(_FullMembersBase master, IPropertyMemberDictionary<IStructPropertyMember, IStructType> originalSet, _StructTypeBase parent)
                : base(master, originalSet, parent)
            {
            }
            private class _Property :
                _PropertyBase<IStructPropertyMember, IStructType>,
                IStructPropertyMember
            {
                internal _Property(IStructPropertyMember original, IStructType parent)
                    : base(original, parent)
                {
                }
                protected override IPropertyMethodMember OnGetMethod(IPropertyMethodMember originalMethod)
                {
                    return new _Method(Parent, (IStructPropertyMethodMember)originalMethod);
                }

                public override string UniqueIdentifier
                {
                    get
                    {
                        return this.Name;
                    }
                }

                private class _Method :
                    _MethodsBase._Method,
                    IStructPropertyMethodMember
                {
                    internal _Method(IStructType parent, IStructPropertyMethodMember original)
                        : base(parent, original)
                    {
                    }

                    protected new IStructPropertyMethodMember Original { get { return ((IStructPropertyMethodMember)(base.Original)); } }
                    #region IPropertySignatureMethodMember Members

                    public PropertyMethodType MethodType
                    {
                        get { return this.Original.MethodType; }
                    }

                    #endregion
                }

            }

            protected override IStructPropertyMember ObtainWrapper(IStructPropertyMember item)
            {
                return new _Property(item, this.Parent);
            }
        }
    }
}
