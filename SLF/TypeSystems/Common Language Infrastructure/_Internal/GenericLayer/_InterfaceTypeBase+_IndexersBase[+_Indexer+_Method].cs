﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _InterfaceTypeBase
    {
        protected internal sealed class _IndexersBase :
            _IndexerSignatureMembersBase<IInterfaceIndexerMember, IInterfaceType>
        {
            private _InterfaceTypeBase _Parent { get { return (_InterfaceTypeBase)base.Parent; } }

            internal _IndexersBase(_FullMembersBase master, IIndexerSignatureMemberDictionary<IInterfaceIndexerMember,IInterfaceType> originalSet, _InterfaceTypeBase parent)
                : base(master, originalSet, parent)
            {
            }

            protected override IInterfaceIndexerMember ObtainWrapper(IInterfaceIndexerMember item)
            {
                return new _IndexerMember(item, this._Parent);
            }

            internal protected sealed class _IndexerMember :
                _IndexerSignatureMemberBase<IInterfaceIndexerMember, IInterfaceType>,
                IInterfaceIndexerMember
            {
                internal _IndexerMember(IInterfaceIndexerMember original, _InterfaceTypeBase parent)
                    : base(original, parent)
                {
                }

                protected override IPropertySignatureMethodMember OnGetMethod(IPropertySignatureMethodMember originalMethod)
                {
                    return new _MethodMember(this._Parent, ((IInterfaceMethodMember)originalMethod));
                }

                public override string UniqueIdentifier
                {
                    get { return string.Format("{0}[{1}]", this.Name, string.Join(",", this.Parameters.Values)); }
                }
                internal protected class _MethodMember :
                    _MethodsBase._Method,
                    IPropertySignatureMethodMember
                {

                    internal _MethodMember(_InterfaceTypeBase parent, IInterfaceMethodMember original)
                        : base(parent, original)
                    {

                    }

                    #region IIndexerSignatureMethodMember Members

                    public PropertyMethodType MethodType
                    {
                        get
                        {
                            return this._Original.MethodType;
                        }
                    }

                    #endregion

                    private IPropertySignatureMethodMember _Original
                    {
                        get
                        {
                            return (IPropertySignatureMethodMember)base.Original;
                        }
                    }

                    protected override sealed IInterfaceMethodMember OnMakeGenericMethod(ITypeCollectionBase genericReplacements)
                    {
                        throw new InvalidOperationException();
                    }
                }

                internal _InterfaceTypeBase _Parent { get { return (_InterfaceTypeBase)base.Parent; } }

            }

        }
    }
}