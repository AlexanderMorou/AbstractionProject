using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Cli.Modules;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Modules;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Modules
{
    internal class CliModuleMethod :
        CliMethodMemberBase<IModuleGlobalMethod, IModule>,
        IModuleGlobalMethod
    {
        /// <summary>
        /// Creates a new <see cref="CliModuleMethod"/> with the
        /// <paramref name="metadata"/>, <paramref name="manager"/>, and <paramref name="owner"/> provided.
        /// </summary>
        /// <param name="metadata">The <see cref="ICliMetadataMethodDefinitionTableRow"/> from which the
        /// <see cref="CliModuleMethod"/> is derived.</param>
        /// <param name="assembly">The <see cref="_ICliAssembly"/> which contains the <see cref="CliModuleMethod"/>.</param>
        /// <param name="owner">The <see cref="ICliModule"/> from which the
        /// <see cref="CliModuleMethod"/> is derived.</param>
        internal CliModuleMethod(ICliMetadataMethodDefinitionTableRow metadata, _ICliAssembly assembly, ICliModule owner, IGeneralGenericSignatureMemberUniqueIdentifier uniqueIdentifier)
            : base(metadata, assembly, owner, uniqueIdentifier)
        {
        }

        /// <summary>
        /// Creates a <see cref="IModuleGlobalMethod"/> as a generic closure of the current 
        /// <see cref="CliModuleMethod"/> with the <paramref name="genericReplacements"/>
        /// provided.
        /// </summary>
        /// <param name="genericReplacements">The <see cref="IControlledTypeCollection"/> which
        /// contains the </param>
        /// <returns>A <see cref="IModuleGlobalMethod"/> as a generic closure of the current 
        /// <see cref="CliModuleMethod"/> with the <paramref name="genericReplacements"/></returns>
        protected override IModuleGlobalMethod OnMakeGenericClosure(IControlledTypeCollection genericReplacements)
        {
            return new _GlobalMethodMember(this, genericReplacements);
        }

        #region IScopedDeclaration Members

        /// <summary>
        /// Returns the <see cref="AccessLevelModifiers"/> which help discern how the
        /// method can be accessed.
        /// </summary>
        public AccessLevelModifiers AccessLevel
        {
            get
            {
                var access = this.MetadataEntry.UsageDetails.Accessibility;
                if (access == MethodMemberAccessibility.Public)
                    return AccessLevelModifiers.Public;
                else if (access == MethodMemberAccessibility.Assembly)
                    return AccessLevelModifiers.Internal;
                else if (access == MethodMemberAccessibility.Private)
                    return AccessLevelModifiers.Private;
                else if (access == MethodMemberAccessibility.Family)
                    return AccessLevelModifiers.Protected;
                else if (access == MethodMemberAccessibility.FamilyOrAssembly)
                    return AccessLevelModifiers.ProtectedOrInternal;
                else if (access == MethodMemberAccessibility.FamilyAndAssembly)
                    //Special case, not available in C# or VB.
                    return AccessLevelModifiers.ProtectedAndInternal;
                return AccessLevelModifiers.PrivateScope;
            }
        }

        #endregion
        protected override IType ActiveType
        {
            get { return null; }
        }
        internal override CliMethodSignatureBase<IMethodParameterMember<IModuleGlobalMethod, IModule>, IModuleGlobalMethod, IModule>.TypeParameter GetTypeParameter(int index, ICliMetadataGenericParameterTableRow metadataEntry)
        {
            return new TypeParameter(this, metadataEntry, index);
        }
        private class TypeParameter :
            CliMethodSignatureBase<IMethodParameterMember<IModuleGlobalMethod, IModule>, IModuleGlobalMethod, IModule>.TypeParameter
        {
            public TypeParameter(CliModuleMethod parent, ICliMetadataGenericParameterTableRow metadataEntry, int position)
                : base(parent, metadataEntry, position)
            {
            }

            public new CliModuleMethod Parent { get { return (CliModuleMethod)base.Parent; } }

            protected override IAssembly OnGetAssembly()
            {
                return this.Parent.Assembly;
            }

            protected override IIdentityManager OnGetManager()
            {
                return this.Parent.IdentityManager;
            }
        }

        internal override IMethodParameterMember<IModuleGlobalMethod, IModule> CreateParameter(int index, ICliMetadataParameterTableRow metadata)
        {
            return new ParameterMember(metadata, this, index);
        }

        private new class ParameterMember :
            CliMethodMemberBase<IModuleGlobalMethod, IModule>.ParameterMember
        {
            internal ParameterMember(ICliMetadataParameterTableRow metadataEntry, CliModuleMethod parent, int index)
                : base(metadataEntry, parent, index)
            {
            }

            protected override IType ActiveType
            {
                get { return null; }
            }
        }

    }
}
