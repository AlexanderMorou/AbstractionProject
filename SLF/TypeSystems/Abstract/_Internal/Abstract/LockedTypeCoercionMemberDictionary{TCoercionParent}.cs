﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    internal class LockedTypeCoercionMemberDictionary<TCoercionParent> :
        LockedGroupedMembersBase<TCoercionParent, ITypeCoercionMember<TCoercionParent>, MethodInfo>,
        ITypeCoercionMemberDictionary<TCoercionParent>,
        ITypeCoercionMemberDictionary
        where TCoercionParent :
            ICoercibleType<ITypeCoercionMember<TCoercionParent>, TCoercionParent>
    {
        public LockedTypeCoercionMemberDictionary(LockedFullMembersBase master, TCoercionParent parent, MethodInfo[] sourceData, Func<MethodInfo, ITypeCoercionMember<TCoercionParent>> fetchImpl)
            : base(master, parent, sourceData, fetchImpl)
        {
        }

        protected override string FetchKey(MethodInfo item)
        {
            return item.GetUniqueIdentifier();
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

        public ITypeCoercionMember<TCoercionParent> this[TypeConversionRequirement requirement, TypeConversionDirection direction, IType target]
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