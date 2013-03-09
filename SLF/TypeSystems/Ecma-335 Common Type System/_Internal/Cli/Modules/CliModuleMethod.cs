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

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Modules
{
    internal class CliModuleMethod :
        CliMethodSignatureBase<IMethodParameterMember<IModuleGlobalMethod, IModule>, IModuleGlobalMethod, IModule>,
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
        internal CliModuleMethod(ICliMetadataMethodDefinitionTableRow metadata, _ICliAssembly assembly, ICliModule owner)
            : base(metadata, assembly, owner)
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
        public override IModuleGlobalMethod MakeGenericClosure(IControlledTypeCollection genericReplacements)
        {
            throw new NotImplementedException();
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


        protected override CliParameterMemberDictionary<IModuleGlobalMethod, IMethodParameterMember<IModuleGlobalMethod, IModule>> InitializeParameters()
        {
            throw new NotImplementedException();
        }

        protected override IType ActiveType
        {
            get { return null; }
        }
    }
}
