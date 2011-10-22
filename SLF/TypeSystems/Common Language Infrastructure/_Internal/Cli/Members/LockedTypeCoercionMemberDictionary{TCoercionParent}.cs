using System;
using System.Reflection;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class LockedTypeCoercionMemberDictionary<TCoercionParentIdentifier, TCoercionParent> :
        LockedGroupedMembersBase<TCoercionParent, ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParentIdentifier, TCoercionParent>, MethodInfo>,
        ITypeCoercionMemberDictionary<TCoercionParentIdentifier, TCoercionParent>,
        ITypeCoercionMemberDictionary
        where TCoercionParentIdentifier :
            ITypeUniqueIdentifier<TCoercionParentIdentifier>
        where TCoercionParent :
            ICoercibleType<ITypeCoercionUniqueIdentifier, TCoercionParentIdentifier, ITypeCoercionMember<TCoercionParentIdentifier, TCoercionParent>, TCoercionParent>
    {
        public LockedTypeCoercionMemberDictionary(LockedFullMembersBase master, TCoercionParent parent, MethodInfo[] sourceData, Func<MethodInfo, ITypeCoercionMember<TCoercionParentIdentifier, TCoercionParent>> fetchImpl)
            : base(master, parent, sourceData, fetchImpl, null)
        {
        }


        protected override ITypeCoercionUniqueIdentifier FetchKey(MethodInfo item)
        {
            return item.GetTypeCoercionUniqueIdentifier();
        }

        #region ITypeCoercionMemberDictionary<TCoercionParent> Members

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

        public ITypeCoercionMember<TCoercionParentIdentifier, TCoercionParent> this[TypeConversionRequirement requirement, TypeConversionDirection direction, IType target]
        {
            get { 
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

    }
}
