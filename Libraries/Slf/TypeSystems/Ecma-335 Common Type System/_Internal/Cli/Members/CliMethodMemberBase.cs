using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract partial class CliMethodMemberBase<TMethod, TMethodParent> :
        CliMethodSignatureBase<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent>,
        IMethodMember<TMethod, TMethodParent>,
        _IGenericMethodRegistrar
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {
        private GenericMethodCache<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent> genericCache;
        protected CliMethodMemberBase(ICliMetadataMethodDefinitionTableRow metadataEntry, _ICliAssembly assembly, TMethodParent parent, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier)
            : base(metadataEntry, assembly, parent, uniqueIdentifier)
        {
        }

        public AccessLevelModifiers AccessLevel
        {
            get { return CliCommon.GetMethodAccessLevel((MethodAttributes)this.MetadataEntry.UsageDetails.Accessibility); }
        }
        protected override sealed CliParameterMemberDictionary<TMethod, IMethodParameterMember<TMethod, TMethodParent>> InitializeParameters()
        {
            return new ParameterMemberDictionary(this);
        }


        private void CheckGenericCache()
        {
            if (this.genericCache == null)
                this.genericCache = new GenericMethodCache<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent>();
        }

        #region _IGenericMethodRegistrar Members

        public void RegisterGenericChild(IMethodParent parent, IMethodMember genericChild)
        {
            this.CheckGenericCache();
            this.genericCache.RegisterGenericChild(parent, genericChild);
        }

        public void UnregisterGenericChild(IMethodParent parent)
        {
            this.CheckGenericCache();
            this.genericCache.UnregisterGenericChild(parent);
        }

        public void RegisterGenericMethod(IMethodMember targetSignature, IControlledTypeCollection typeParameters)
        {
            this.CheckGenericCache();
            this.genericCache.RegisterGenericMethod(targetSignature, typeParameters);
        }

        public void UnregisterGenericMethod(IControlledTypeCollection typeParameters)
        {
            this.CheckGenericCache();
            this.genericCache.UnregisterGenericMethod(typeParameters);
        }

        #endregion

        protected override bool ContainsGenericMethod(IControlledTypeCollection typeParameters, ref TMethod r)
        {
            this.CheckGenericCache();
            return this.genericCache.ContainsGenericMethod(typeParameters, ref r);
        }

        internal static IEnumerable<IInterfaceType> GetImplementations(ICliMetadataTypeDefinitionTableRow parentDef, ICliMetadataMethodDefinitionTableRow methodMetadata, CliManager identityManager, IType activeType, IMethodSignatureMember activeMethod, IAssembly activeAssembly)
        {
            var targets = (from t in parentDef.ImplementationMap
                           where t.MethodBody == methodMetadata
                           select t.MethodDeclaration);
            foreach (var target in targets)
            {
                IType currentType = null;
                switch (target.MethodDefOrRefEncoding)
                {
                    case CliMetadataMethodDefOrRefTag.MethodDefinition:
                        ICliMetadataMethodDefinitionTableRow methodDef = (ICliMetadataMethodDefinitionTableRow)target;
                        currentType = identityManager.ObtainTypeReference(methodMetadata.MetadataRoot.TableStream.TypeDefinitionTable[(int)methodMetadata.MetadataRoot.TableStream.TypeDefinitionTable.GetTypeFromMethodIndex(methodDef.Index)], activeType, activeMethod, activeAssembly);
                        break;
                    case CliMetadataMethodDefOrRefTag.MemberRef:
                        ICliMetadataMemberReferenceTableRow memberRef = (ICliMetadataMemberReferenceTableRow)target;
                        switch (memberRef.ClassSource)
                        {
                            case CliMetadataMemberRefParentTag.TypeDefinition:
                                currentType = identityManager.ObtainTypeReference((ICliMetadataTypeDefinitionTableRow)memberRef.Class, activeType, activeMethod, activeAssembly);
                                break;
                            case CliMetadataMemberRefParentTag.TypeReference:
                                currentType = identityManager.ObtainTypeReference((ICliMetadataTypeRefTableRow)memberRef.Class, activeType, activeMethod, activeAssembly);
                                break;
                            case CliMetadataMemberRefParentTag.TypeSpecification:
                                currentType = identityManager.ObtainTypeReference((ICliMetadataTypeSpecificationTableRow)memberRef.Class, activeType, activeMethod, activeAssembly);
                                break;
                        }
                        break;
                }
                if (currentType != null && currentType is IInterfaceType)
                    yield return (IInterfaceType)currentType;
            }
        }

    }
}
