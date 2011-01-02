using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
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
        private class _PropertiesBase :
            _PropertyMembersBase<IClassPropertyMember, IClassType>
        {
            public _PropertiesBase(_FullMembersBase master, IPropertyMemberDictionary<IClassPropertyMember, IClassType> originalSet, _ClassTypeBase parent)
                : base(master, originalSet, parent)
            {
            }
            private class _Property :
                _PropertyBase<IClassPropertyMember, IClassType>,
                IClassPropertyMember
            {
                internal _Property(IClassPropertyMember original, IClassType parent)
                    : base(original, parent)
                {
                }
                protected override IPropertyMethodMember OnGetMethod(IPropertyMethodMember originalMethod)
                {
                    return new _Method(Parent, (IClassPropertyMethodMember)originalMethod);
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
                    IClassPropertyMethodMember
                {
                    internal _Method(IClassType parent, IClassPropertyMethodMember original)
                        : base(parent, original)
                    {
                    }

                    protected new IClassPropertyMethodMember Original { get { return ((IClassPropertyMethodMember)(base.Original)); } }
                    #region IPropertySignatureMethodMember Members

                    public PropertyMethodType MethodType
                    {
                        get { return this.Original.MethodType; }
                    }

                    #endregion
                }

            }

            protected override IClassPropertyMember ObtainWrapper(IClassPropertyMember item)
            {
                return new _Property(item, this.Parent);
            }
        }
    }
}
