using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _GenericInstantiableTypeBase<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TEvent :
            IEventMember<TEvent, TType>
        where TField :
            IFieldMember<TField, TType>,
            IInstanceMember
        where TIndexer :
            IIndexerMember<TIndexer, TType>
        where TMethod :
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TProperty :
            IPropertyMember<TProperty, TType>
        where TType :
            class,
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType>,
            IGenericType<TType>
    {
        private class _TypeCoercionsBase :
            _GroupedMembersBase<TType, ITypeCoercionMember<TType>, ITypeCoercionMemberDictionary<TType>>,
            ITypeCoercionMemberDictionary<TType>,
            ITypeCoercionMemberDictionary
        {

            internal _TypeCoercionsBase(_FullMembersBase master, ITypeCoercionMemberDictionary<TType> originalSet, TType parent)
                : base(master, originalSet, parent)
            {
            }
            private class _TypeCoercionMember :
                _MemberBase<ITypeCoercionMember<TType>, TType>,
                ITypeCoercionMember<TType>
            {

                public _TypeCoercionMember(ITypeCoercionMember<TType> original, TType parent)
                    : base(original, parent)
                {
                }

                public override string UniqueIdentifier
                {
                    get {
                        var opName = string.Empty;
                        switch (this.Requirement)
                        {
                            case TypeConversionRequirement.Implicit:
                                opName = "op_Implicit";
                                break;
                            case TypeConversionRequirement.Explicit:
                            default:
                                opName = "op_Explicit";
                                break;
                        }
                        string result;
                        switch (this.Direction)
                        {
                            case TypeConversionDirection.FromContainingType:
                                result = string.Format("{0} {1}({2})", this.CoercionType, opName, this.Parent);
                                break;
                            case TypeConversionDirection.ToContainingType:
                            default:
                                result = string.Format("{0} {1}({2})", this.Parent, opName, this.CoercionType);
                                break;
                        }
                        return result;
                    }
                }

                #region ICoercionMember Members

                ICoercibleType ICoercionMember.Parent
                {
                    get { return base.Parent; }
                }

                #endregion

                #region IScopedDeclaration Members

                public AccessLevelModifiers AccessLevel
                {
                    get { return this.Original.AccessLevel; }
                }

                #endregion

                #region ITypeCoercionMember Members

                public TypeConversionRequirement Requirement
                {
                    get { return this.Original.Requirement; }
                }

                public TypeConversionDirection Direction
                {
                    get { return this.Original.Direction; }
                }

                public IType CoercionType
                {
                    get {
                        if (Parent.IsGenericType && !Parent.IsGenericTypeDefinition)
                            return Original.CoercionType.Disambiguify(Parent.GenericParameters, null, TypeParameterSources.Type);
                        return Original.CoercionType;
                    }
                }

                #endregion
            }

            #region ITypeCoercionMemberDictionary<TType> Members

            public bool HasExplicitCoercionTo(IType target)
            {
                foreach (var coercion in this.Values)
                    if (coercion.Requirement == TypeConversionRequirement.Explicit &&
                        coercion.Direction == TypeConversionDirection.ToContainingType &&
                        target.IsAssignableFrom(coercion.CoercionType))
                        return true;
                return false;
            }

            public bool HasImplicitCoercionTo(IType target)
            {
                foreach (var coercion in this.Values)
                    if (coercion.Requirement == TypeConversionRequirement.Implicit &&
                        coercion.Direction == TypeConversionDirection.ToContainingType &&
                        target.IsAssignableFrom(coercion.CoercionType))
                        return true;
                return false;
            }

            public bool HasExplicitCoercionFrom(IType target)
            {
                foreach (var coercion in this.Values)
                    if (coercion.Requirement == TypeConversionRequirement.Explicit &&
                        coercion.Direction == TypeConversionDirection.FromContainingType &&
                        coercion.CoercionType.IsAssignableFrom(target))
                        return true;
                return false;
            }

            public bool HasImplicitCoercionFrom(IType target)
            {
                foreach (var coercion in this.Values)
                    if (coercion.Requirement == TypeConversionRequirement.Implicit &&
                        coercion.Direction == TypeConversionDirection.FromContainingType &&
                        coercion.CoercionType.IsAssignableFrom(target))
                        return true;
                return false;
            }

            public ITypeCoercionMember<TType> this[TypeConversionRequirement requirement, TypeConversionDirection direction, IType target]
            {
                get
                {
                    foreach (var coercion in this.Values)
                        if (coercion.Requirement == requirement &&
                            coercion.Direction == direction)
                        {
                            switch (direction)
                            {
                                case TypeConversionDirection.ToContainingType:
                                    if (target.IsAssignableFrom(coercion.CoercionType))
                                        return coercion;
                                    break;
                                case TypeConversionDirection.FromContainingType:
                                    if (coercion.CoercionType.IsAssignableFrom(target))
                                        return coercion;
                                    break;
                                default:
                                    break;
                            }
                        }
                    return null;
                }
            }

            #endregion

            #region ITypeCoercionMemberDictionary Members

            ITypeCoercionMember ITypeCoercionMemberDictionary.this[TypeConversionRequirement requirement, TypeConversionDirection direction, IType target]
            {
                get { return this[requirement, direction, target]; }
            }

            #endregion

            protected override ITypeCoercionMember<TType> ObtainWrapper(ITypeCoercionMember<TType> item)
            {
                return new _TypeCoercionMember(original:item, parent:this.Parent);
            }
        }
    }
}
