using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class CliTypeCoercionMemberDictionary<TCoercionParent> :
        CliCoercionMemberDictionary<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, TCoercionParent>,
        ITypeCoercionMemberDictionary<TCoercionParent>,
        ITypeCoercionMemberDictionary
        where TCoercionParent :
            ICoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, TCoercionParent>
    {
        public CliTypeCoercionMemberDictionary(TCoercionParent parent, CliFullMemberDictionary fullMembers)
            : base(parent, fullMembers, CliMemberType.TypeCoercionOperator)
        {
        }

        public bool HasExplicitCoercionTo(IType target)
        {
            foreach (var member in this.Values)
                if (member.Direction == TypeConversionDirection.ToContainingType &&
                    member.Requirement == TypeConversionRequirement.Explicit &&
                    member.CoercionType == target)
                    return true;
            return false;
        }

        public bool HasImplicitCoercionTo(IType target)
        {
            foreach (var member in this.Values)
                if (member.Direction == TypeConversionDirection.ToContainingType &&
                    member.Requirement == TypeConversionRequirement.Implicit &&
                    member.CoercionType == target)
                    return true;
            return false;
        }

        public bool HasExplicitCoercionFrom(IType target)
        {
            foreach (var member in this.Values)
                if (member.Direction == TypeConversionDirection.FromContainingType &&
                    member.Requirement == TypeConversionRequirement.Explicit &&
                    member.CoercionType == target)
                    return true;
            return false;
        }

        public bool HasImplicitCoercionFrom(IType target)
        {
            foreach (var member in this.Values)
                if (member.Direction == TypeConversionDirection.FromContainingType &&
                    member.Requirement == TypeConversionRequirement.Implicit &&
                    member.CoercionType == target)
                    return true;
            return false;
        }

        public ITypeCoercionMember<TCoercionParent> this[TypeConversionRequirement requirement, TypeConversionDirection direction, IType target]
        {
            get
            {
                switch (requirement)
                {
                    case TypeConversionRequirement.Explicit:
                    case TypeConversionRequirement.Implicit:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("requirement");
                }
                switch (direction)
                {
                    case TypeConversionDirection.ToContainingType:
                    case TypeConversionDirection.FromContainingType:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("direction");
                }
                foreach (var member in this.Values)
                    if (member.Direction == direction &&
                        member.Requirement == requirement &&
                        member.CoercionType == target)
                        return member;
                throw new ArgumentException(string.Format("There is no {0} transition {1} '{2}'", requirement == TypeConversionRequirement.Explicit ? "explicit" : "implicit", direction == TypeConversionDirection.FromContainingType ? "from" : "to", target), "target");
            }
        }

        ITypeCoercionMember ITypeCoercionMemberDictionary.this[TypeConversionRequirement requirement, TypeConversionDirection direction, IType target]
        {
            get { return this[requirement, direction, target]; }
        }
    }
}
