using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    internal class DefaultIntermediateIdentifierLanguageQualifierService :
        IIntermediateIdentifierLanguageQualifierService
    {

        public bool HandlesIndexerMemberIdentifier { get { return true; } }

        public bool HandlesIndexerSignatureMemberIdentifier { get { return true; } }

        public bool HandlesGenericParameterIdentifier { get { return true; } }

        public bool HandlesConstructorMemberIdentifier { get { return true; } }

        public bool HandlesConstructorSignatureMemberIdentifier { get { return true; } }

        public bool HandlesMemberIdentifier { get { return true; } }

        public bool HandlesMethodMemberIdentifier { get { return true; } }

        public bool HandlesTypeIdentifier { get { return true; } }

        public bool HandlesAssemblyIdentifier { get { return true; } }

        public bool HandlesTypeCoercionIdentifier { get { return true; } }

        public bool HandlesBinaryOperatorCoercionIdentifier { get { return true; } }

        public bool HandlesUnaryOperatorCoercionIdentifier { get { return true; } }

        public bool HandlesEventMemberIdentifier
        {
            get { return true; }
        }

        public bool HandlesEventSignatureMemberIdentifier
        {
            get { return true; }
        }

        public bool HandlesMethodSignatureMemberIdentifier
        {
            get { return true; }
        }

        public IAssemblyUniqueIdentifier GetIdentifier(IIntermediateAssembly assembly)
        {
            //if (assembly.PublicKeyInfo == null || assembly.PublicKeyInfo.PublicToken.Token == null || assembly.PublicKeyInfo.PublicToken.Token.Length == 0)
                return TypeSystemIdentifiers.GetAssemblyIdentifier(assembly.Name, assembly.AssemblyInformation.AssemblyVersion, assembly.AssemblyInformation.Culture);
            //else
            //    return TypeSystemIdentifiers.GetAssemblyIdentifier(assembly.Name, assembly.AssemblyInformation.AssemblyVersion, assembly.AssemblyInformation.Culture, assembly.PublicKeyInfo.PublicToken.Token);
        }

        public IGenericParameterUniqueIdentifier GetIdentifier(IIntermediateGenericParameter member)
        {
            if (member.Position > -1)
                return TypeSystemIdentifiers.GetGenericParameterIdentifier(member.Position, member.Name, member is IGenericTypeParameter);
            else
                return TypeSystemIdentifiers.GetGenericParameterIdentifier(member.Name, member is IGenericTypeParameter);
        }

        public ITypeCoercionUniqueIdentifier GetIdentifier(IIntermediateTypeCoercionMember member)
        {
            return TypeSystemIdentifiers.GetTypeOperatorIdentifier(member.Requirement, member.Direction, member.CoercionType, member.UserSpecificQualifier);
        }

        public IBinaryOperatorUniqueIdentifier GetIdentifier(IIntermediateBinaryOperatorCoercionMember member)
        {
            return TypeSystemIdentifiers.GetBinaryOperatorIdentifier(member.Operator, member.UserSpecificQualifier, member.ContainingSide, member.OtherSide);
        }

        public IUnaryOperatorUniqueIdentifier GetIdentifier(IIntermediateUnaryOperatorCoercionMember member)
        {
            return TypeSystemIdentifiers.GetUnaryOperatorIdentifier(member.Operator, member.UserSpecificQualifier);
        }

        public IGeneralMemberUniqueIdentifier GetIdentifier(IIntermediatePropertySignatureMember member)
        {
            return TypeSystemIdentifiers.GetMemberIdentifier(member.Name, member.UserSpecificQualifier);
        }

        public IGeneralMemberUniqueIdentifier GetIdentifier(IIntermediateFieldMember member)
        {
            return TypeSystemIdentifiers.GetMemberIdentifier(member.Name, member.UserSpecificQualifier);
        }

        public IGeneralSignatureMemberUniqueIdentifier GetIdentifier(IIntermediateIndexerMember member)
        {
            var internalHelper = member as _IIntermediateIndexerSignatureMember;
            if (internalHelper == null)
                return TypeSystemIdentifiers.GetSignatureIdentifier(member.Name, member.Parameters.ParameterTypes.ToArray());
            else if (internalHelper.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            else if (internalHelper.AreParametersInitialized)
                return TypeSystemIdentifiers.GetSignatureIdentifier(member.Name, member.Parameters.ParameterTypes.ToArray());
            else
                return TypeSystemIdentifiers.GetSignatureIdentifier(member.Name);
        }

        public IGeneralSignatureMemberUniqueIdentifier GetIdentifier(IIntermediateIndexerSignatureMember member)
        {
            var internalHelper = member as _IIntermediateIndexerSignatureMember;
            if (internalHelper == null)
                return TypeSystemIdentifiers.GetSignatureIdentifier(member.Name, member.Parameters.ParameterTypes.ToArray());
            else if (internalHelper.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            else if (internalHelper.AreParametersInitialized)
                return TypeSystemIdentifiers.GetSignatureIdentifier(member.Name, member.Parameters.ParameterTypes.ToArray());
            else
                return TypeSystemIdentifiers.GetSignatureIdentifier(member.Name);
        }

        public IGeneralGenericSignatureMemberUniqueIdentifier GetIdentifier(IIntermediateMethodMember member)
        {
            return GetIdentifier((IIntermediateMethodSignatureMember)(member));
        }

        public IGeneralSignatureMemberUniqueIdentifier GetIdentifier(IIntermediateConstructorMember member)
        {
            return GetIdentifier((IIntermediateConstructorSignatureMember)member);
        }

        public IGeneralSignatureMemberUniqueIdentifier GetIdentifier(IIntermediateConstructorSignatureMember member)
        {
            var internalHelper = member as _IntermediateConstructorSignatureMember;
            if (internalHelper == null)
                return TypeSystemIdentifiers.GetCtorSignatureIdentifier(member.Parameters.ParameterTypes.ToArray());
            else if (internalHelper.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            else if (internalHelper.IsTypeInitializer)
                return TypeSystemIdentifiers.GetCtorSignatureIdentifier();
            else if (internalHelper.AreParametersInitialized)
                return TypeSystemIdentifiers.GetCtorSignatureIdentifier(member.Parameters.ParameterTypes.ToArray());
            else
                return TypeSystemIdentifiers.GetCtorSignatureIdentifier(new IType[0]);
        }

        public ILanguageProvider Provider
        {
            get { return null; }
        }

        public ILanguage Language
        {
            get { return null; }
        }

        IServiceProvider<ILanguageService> IService<ILanguageService>.Provider
        {
            get { return null; }
        }

        public Guid ServiceGuid
        {
            get { return LanguageGuids.Services.UniqueIdentifierService; }
        }

        public IGeneralGenericSignatureMemberUniqueIdentifier GetIdentifier(IIntermediateMethodSignatureMember member)
        {
            var internalHelper = member as _IIntermediateMethodSignatureMember;
            if (internalHelper == null)
                return TypeSystemIdentifiers.GetGenericSignatureIdentifier(member.Name, member.TypeParameters.Count, member.UserSpecificQualifier, member.Parameters.ParameterTypes.ToArray());
            else if (internalHelper.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            else if (internalHelper.AreTypeParametersInitialized)
                if (internalHelper.AreParametersInitialized)
                    return TypeSystemIdentifiers.GetGenericSignatureIdentifier(member.Name, member.TypeParameters.Count, member.UserSpecificQualifier, member.Parameters.ParameterTypes.ToArray());
                else
                    return TypeSystemIdentifiers.GetGenericSignatureIdentifier(member.Name, member.TypeParameters.Count, member.UserSpecificQualifier);
            else if (internalHelper.AreParametersInitialized)
                return TypeSystemIdentifiers.GetGenericSignatureIdentifier(member.Name, member.UserSpecificQualifier, member.Parameters.ParameterTypes.ToArray());
            else
                return TypeSystemIdentifiers.GetGenericSignatureIdentifier(member.Name, member.UserSpecificQualifier);
        }

        public IGeneralSignatureMemberUniqueIdentifier GetIdentifier(IIntermediateEventSignatureMember member)
        {
            var internalHelper = member as _IIntermediateEventSignatureMember;
            if (internalHelper == null)
                return TypeSystemIdentifiers.GetSignatureIdentifier(member.Name, member.UserSpecificQualifier, member.Parameters.ParameterTypes.ToArray());
            else if (internalHelper.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            else if (internalHelper.AreParametersInitialized)
                return TypeSystemIdentifiers.GetSignatureIdentifier(member.Name, member.UserSpecificQualifier, member.Parameters.ParameterTypes.ToArray());
            else
                return TypeSystemIdentifiers.GetSignatureIdentifier(member.Name, member.UserSpecificQualifier, new IType[0]);
        }

        public IGeneralSignatureMemberUniqueIdentifier GetIdentifier(IIntermediateEventMember member)
        {
            return GetIdentifier((IIntermediateEventSignatureMember)member);
        }
    }
}
