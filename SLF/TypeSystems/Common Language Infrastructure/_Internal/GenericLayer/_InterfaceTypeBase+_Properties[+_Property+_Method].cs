using System;
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
        internal sealed class _PropertiesBase :
            _PropertySignatureMembersBase<IInterfacePropertyMember, IInterfaceType>
        {
            private _InterfaceTypeBase _Parent { get { return (_InterfaceTypeBase)base.Parent; } }

            internal _PropertiesBase(_FullMembersBase master, IPropertySignatureMemberDictionary<IInterfacePropertyMember,IInterfaceType> originalSet, _InterfaceTypeBase parent)
                : base(master, originalSet, parent)
            {
            }

            protected override IInterfacePropertyMember ObtainWrapper(IInterfacePropertyMember item)
            {
                return new _PropertyMember(item, this._Parent);
            }

            internal sealed class _PropertyMember :
                _PropertySignatureBase<IInterfacePropertyMember, IInterfaceType>,
                IInterfacePropertyMember
            {
                internal _PropertyMember(IInterfacePropertyMember original, _InterfaceTypeBase parent)
                    : base(original, parent)
                {
                }

                protected override IPropertySignatureMethodMember OnGetMethod(IPropertySignatureMethodMember originalMethod)
                {
                    return new _MethodMember(this._Parent, ((IInterfaceMethodMember)originalMethod));
                }
                
                public override string UniqueIdentifier
                {
                    get { return this.Name; }
                }
                internal class _MethodMember :
                    _MethodsBase._Method,
                    IPropertySignatureMethodMember
                {

                    internal _MethodMember(_InterfaceTypeBase parent, IInterfaceMethodMember original)
                        : base(parent, original)
                    {

                    }

                    #region IPropertySignatureMethodMember Members

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
