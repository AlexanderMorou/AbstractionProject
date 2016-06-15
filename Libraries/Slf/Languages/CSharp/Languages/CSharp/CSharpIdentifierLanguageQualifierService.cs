using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    /* The purpose behind the language-specific qualifiers is: if a method in C# represents the concrete definition of an interface method, there needs to be a way to logically specify it from other
     * methods with the same name.  It isn't possible to specify the type as part of the method name, because we can't always know, while AST building, what the target language is. */
    internal class CSharpIdentifierLanguageQualifierService :
        IIntermediateIdentifierLanguageQualifierService
    {
        private ICSharpProvider _provider;
        private ICSharpLanguage _language;
        public CSharpIdentifierLanguageQualifierService(ICSharpProvider provider, ICSharpLanguage language)
        {
            this._provider = provider;
            this._language = language;
        }

        public bool HandlesGenericParameterIdentifier
        {
            get { return false; }
        }

        public bool HandlesEventMemberIdentifier
        {
            get { return true; }
        }

        public bool HandlesEventSignatureMemberIdentifier
        {
            get { return false; }
        }

        public bool HandlesIndexerMemberIdentifier
        {
            get { return true; }
        }

        public bool HandlesIndexerSignatureMemberIdentifier
        {
            get { return false; }
        }

        public bool HandlesConstructorMemberIdentifier
        {
            get { return false; }
        }

        public bool HandlesConstructorSignatureMemberIdentifier
        {
            get { return false; }
        }

        public bool HandlesMemberIdentifier
        {
            get { return true; }
        }

        public bool HandlesMethodMemberIdentifier
        {
            get { return true; }
        }

        public bool HandlesMethodSignatureMemberIdentifier
        {
            get { return false; }
        }

        public bool HandlesTypeIdentifier
        {
            get { return false; }
        }

        public bool HandlesAssemblyIdentifier
        {
            get { return false; }
        }

        public bool HandlesTypeCoercionIdentifier
        {
            get { return false; }
        }

        public bool HandlesBinaryOperatorCoercionIdentifier
        {
            get { return false; }
        }

        public bool HandlesUnaryOperatorCoercionIdentifier
        {
            get { return false; }
        }

        public ILanguageProvider Provider
        {
            get { return this._provider; }
        }

        public ILanguage Language
        {
            get { return this._language; }
        }

        IServiceProvider<ILanguageService> IService<ILanguageService>.Provider
        {
            get { return this.Provider; }
        }

        public Guid ServiceGuid
        {
            get { return LanguageGuids.Services.UniqueIdentifierService; }
        }

        public IGeneralMemberUniqueIdentifier GetIdentifier(IIntermediateFieldMember member)
        {
            return IntermediateGateway.DefaultUniqueIdentifierService.GetIdentifier(member);
        }

        public IGeneralMemberUniqueIdentifier GetIdentifier(IIntermediatePropertySignatureMember member)
        {
            if (member is IIntermediateClassPropertyMember || member is IIntermediateStructPropertyMember)
            {
                var classProp = member as IIntermediateClassPropertyMember;
                if (classProp != null)
                {
                    var firstImpl = classProp.Implementations.FirstOrDefault();
                    if (firstImpl != null)
                        return TypeSystemIdentifiers.GetMemberIdentifier(member.Name, string.Format("{0}{1}", firstImpl.FullName, member.UserSpecificQualifier));
                }
                else
                {
                    var structProp = member as IIntermediateStructPropertyMember;
                    if (structProp != null)
                    {
                        var firstImpl = structProp.Implementations.FirstOrDefault();
                        if (firstImpl != null)
                            return TypeSystemIdentifiers.GetMemberIdentifier(member.Name, string.Format("{0}{1}", firstImpl.FullName, member.UserSpecificQualifier));
                    }
                }
            }
            return IntermediateGateway.DefaultUniqueIdentifierService.GetIdentifier(member);
        }

        public IGeneralSignatureMemberUniqueIdentifier GetIdentifier(IIntermediateIndexerMember member)
        {
            if (member is IIntermediateClassIndexerMember || member is IIntermediateStructIndexerMember)
            {
                var internalHelper = member as _IIntermediateIndexerSignatureMember;
                var classIndexer = member as IIntermediateClassIndexerMember;
                if (classIndexer != null)
                {
                    var firstImpl = classIndexer.Implementations.FirstOrDefault();
                    if (firstImpl != null)
                        return GetIdentifierInternal(member, internalHelper, firstImpl);
                }
                else
                {
                    var structIndexer = member as IIntermediateStructIndexerMember;
                    var firstImpl = structIndexer.Implementations.FirstOrDefault();
                    if (firstImpl != null)
                        return GetIdentifierInternal(member, internalHelper, firstImpl);
                }
            }
            return IntermediateGateway.DefaultUniqueIdentifierService.GetIdentifier(member);
        }

        private static IGeneralSignatureMemberUniqueIdentifier GetIdentifierInternal(IIntermediateIndexerMember member, _IIntermediateIndexerSignatureMember internalHelper, IType firstImpl)
        {
            if (internalHelper == null)
                return TypeSystemIdentifiers.GetSignatureIdentifier(member.Name, string.Format("{0}{1}", firstImpl.FullName, member.UserSpecificQualifier), member.Parameters.ParameterTypes.ToArray());
            else if (internalHelper.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            else if (internalHelper.AreParametersInitialized)
                return TypeSystemIdentifiers.GetSignatureIdentifier(member.Name, string.Format("{0}{1}", firstImpl.FullName, member.UserSpecificQualifier), member.Parameters.ParameterTypes.ToArray());
            else
                return TypeSystemIdentifiers.GetSignatureIdentifier(member.Name, string.Format("{0}{1}", firstImpl.FullName, member.UserSpecificQualifier), new IType[0]);
        }

        public IGeneralGenericSignatureMemberUniqueIdentifier GetIdentifier(IIntermediateMethodMember member)
        {
            if (member is IIntermediateClassMethodMember || member is IIntermediateStructMethodMember)
            {
                var internalHelper = member as _IIntermediateMethodSignatureMember;
                var classMethod = member as IIntermediateClassMethodMember;
                if (classMethod != null)
                {
                    var firstImpl = classMethod.Implementations.FirstOrDefault();
                    if (firstImpl != null)
                        return GetIdentifierInternal(member, internalHelper, firstImpl);
                }
                else
                {
                    var structMethod = member as IIntermediateStructMethodMember;
                    var firstImpl = structMethod.Implementations.FirstOrDefault();
                    if (firstImpl != null)
                        return GetIdentifierInternal(member, internalHelper, firstImpl);
                }
            }
            return IntermediateGateway.DefaultUniqueIdentifierService.GetIdentifier(member);
        }

        private static IGeneralGenericSignatureMemberUniqueIdentifier GetIdentifierInternal(IIntermediateMethodMember member, _IIntermediateMethodSignatureMember internalHelper, IType firstImpl)
        {
            if (internalHelper == null)
                return TypeSystemIdentifiers.GetGenericSignatureIdentifier(member.Name, member.TypeParameters.Count, string.Format("{0}{1}", firstImpl.FullName, member.UserSpecificQualifier), member.Parameters.ParameterTypes.ToArray());
            else if (internalHelper.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            else if (internalHelper.AreTypeParametersInitialized)
                if (internalHelper.AreParametersInitialized)
                    return TypeSystemIdentifiers.GetGenericSignatureIdentifier(member.Name, member.TypeParameters.Count, string.Format("{0}{1}", firstImpl.FullName, member.UserSpecificQualifier), member.Parameters.ParameterTypes.ToArray());
                else
                    return TypeSystemIdentifiers.GetGenericSignatureIdentifier(member.Name, member.TypeParameters.Count, string.Format("{0}{1}", firstImpl.FullName, member.UserSpecificQualifier), new IType[0]);
            else if (internalHelper.AreParametersInitialized)
                return TypeSystemIdentifiers.GetGenericSignatureIdentifier(member.Name, 0, string.Format("{0}{1}", firstImpl.FullName, member.UserSpecificQualifier), member.Parameters.ParameterTypes.ToArray());
            else
                return TypeSystemIdentifiers.GetGenericSignatureIdentifier(member.Name, 0, string.Format("{0}{1}", firstImpl.FullName, member.UserSpecificQualifier), new IType[0]);
        }

        public IGeneralSignatureMemberUniqueIdentifier GetIdentifier(IIntermediateEventMember member)
        {
            if (member is IIntermediateClassEventMember || member is IIntermediateStructEventMember)
            {
                var internalHelper = member as _IIntermediateEventSignatureMember;
                var classEvent = member as IIntermediateClassEventMember;
                if (classEvent != null)
                {
                    var firstImpl = classEvent.Implementations.FirstOrDefault();
                    if (firstImpl != null)
                        return GetIdentifierInternal(member, internalHelper, firstImpl);
                }
                else
                {
                    var structEvent = member as IIntermediateStructEventMember;
                    var firstImpl = structEvent.Implementations.FirstOrDefault();
                    if (firstImpl != null)
                        return GetIdentifierInternal(member, internalHelper, firstImpl);
                }
            }
            return IntermediateGateway.DefaultUniqueIdentifierService.GetIdentifier(member);
        }

        private static IGeneralSignatureMemberUniqueIdentifier GetIdentifierInternal(IIntermediateEventMember member, _IIntermediateEventSignatureMember internalHelper, IType firstImpl)
        {
            if (internalHelper == null)
                return TypeSystemIdentifiers.GetSignatureIdentifier(member.Name, string.Format("{0}{1}", firstImpl.FullName, member.UserSpecificQualifier), member.Parameters.ParameterTypes.ToArray());
            else if (internalHelper.IsDisposed)
                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
            else if (internalHelper.AreParametersInitialized)
                return TypeSystemIdentifiers.GetSignatureIdentifier(member.Name, string.Format("{0}{1}", firstImpl.FullName, member.UserSpecificQualifier), member.Parameters.ParameterTypes.ToArray());
            else
                return TypeSystemIdentifiers.GetSignatureIdentifier(member.Name, string.Format("{0}{1}", firstImpl.FullName, member.UserSpecificQualifier), new IType[0]);
        }

        public IGeneralSignatureMemberUniqueIdentifier GetIdentifier(IIntermediateConstructorMember member)
        {
            throw new NotSupportedException();
        }

        public IAssemblyUniqueIdentifier GetIdentifier(IIntermediateAssembly assembly)
        {
            throw new NotSupportedException();
        }

        public IGenericParameterUniqueIdentifier GetIdentifier(IIntermediateGenericParameter member)
        {
            throw new NotSupportedException();
        }

        public ITypeCoercionUniqueIdentifier GetIdentifier(IIntermediateTypeCoercionMember member)
        {
            throw new NotSupportedException();
        }

        public IBinaryOperatorUniqueIdentifier GetIdentifier(IIntermediateBinaryOperatorCoercionMember member)
        {
            throw new NotSupportedException();
        }

        public IUnaryOperatorUniqueIdentifier GetIdentifier(IIntermediateUnaryOperatorCoercionMember member)
        {
            throw new NotSupportedException();
        }

        public IGeneralSignatureMemberUniqueIdentifier GetIdentifier(IIntermediateIndexerSignatureMember member)
        {
            throw new NotSupportedException();
        }

        public IGeneralGenericSignatureMemberUniqueIdentifier GetIdentifier(IIntermediateMethodSignatureMember member)
        {
            throw new NotSupportedException();
        }

        public IGeneralSignatureMemberUniqueIdentifier GetIdentifier(IIntermediateConstructorSignatureMember member)
        {
            throw new NotSupportedException();
        }

        public IGeneralSignatureMemberUniqueIdentifier GetIdentifier(IIntermediateEventSignatureMember member)
        {
            throw new NotSupportedException();
        }
    }
}
