using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _GenericInstantiableTypeBase<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType>
        where TCtor :
            class,
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
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, IGeneralGenericTypeUniqueIdentifier, TType>,
            IGenericType<IGeneralGenericTypeUniqueIdentifier, TType>
    {
        private class _TypeCoercionsBase :
            _GroupedMembersBase<TType, ITypeCoercionUniqueIdentifier, ITypeCoercionMember<IGeneralGenericTypeUniqueIdentifier, TType>, ITypeCoercionMemberDictionary<IGeneralGenericTypeUniqueIdentifier, TType>>,
            ITypeCoercionMemberDictionary<IGeneralGenericTypeUniqueIdentifier, TType>,
            ITypeCoercionMemberDictionary
        {

            internal _TypeCoercionsBase(_FullMembersBase master, ITypeCoercionMemberDictionary<IGeneralGenericTypeUniqueIdentifier, TType> originalSet, TType parent)
                : base(master, originalSet, parent)
            {
            }
            private class _TypeCoercionMember :
                _MemberBase<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<IGeneralGenericTypeUniqueIdentifier, TType>, TType>,
                ITypeCoercionMember<IGeneralGenericTypeUniqueIdentifier, TType>
            {

                public _TypeCoercionMember(ITypeCoercionMember<IGeneralGenericTypeUniqueIdentifier, TType> original, TType parent)
                    : base(original, parent)
                {
                }

                public override ITypeCoercionUniqueIdentifier UniqueIdentifier
                {
                    get {
                        return AstIdentifier.TypeOperator(this.Requirement, this.Direction, this.CoercionType);
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
                        if (Parent.IsGenericConstruct && !Parent.IsGenericDefinition)
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

            public ITypeCoercionMember<IGeneralGenericTypeUniqueIdentifier, TType> this[TypeConversionRequirement requirement, TypeConversionDirection direction, IType target]
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

            protected override ITypeCoercionMember<IGeneralGenericTypeUniqueIdentifier, TType> ObtainWrapper(ITypeCoercionMember<IGeneralGenericTypeUniqueIdentifier, TType> item)
            {
                return new _TypeCoercionMember(original:item, parent:this.Parent);
            }
        }
    }
}
