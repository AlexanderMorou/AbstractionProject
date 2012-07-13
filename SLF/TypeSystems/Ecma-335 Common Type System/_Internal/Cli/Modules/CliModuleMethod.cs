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
        /// <param name="manager">The <see cref="_ICliManager"/> which maintains the type identity 
        /// resolution of the <paramref name="owner"/>.</param>
        /// <param name="owner">The <see cref="ICliModule"/> from which the
        /// <see cref="CliModuleMethod"/> is derived.</param>
        internal CliModuleMethod(ICliMetadataMethodDefinitionTableRow metadata, _ICliManager manager, ICliModule owner)
            : base(metadata, manager, owner)
        {
        }

        /// <summary>
        /// Creates a <see cref="IModuleGlobalMethod"/> as a generic closure of the current 
        /// <see cref="CliModuleMethod"/> with the <paramref name="genericReplacements"/>
        /// provided.
        /// </summary>
        /// <param name="genericReplacements">The <see cref="ITypeCollectionBase"/> which
        /// contains the </param>
        /// <returns>A <see cref="IModuleGlobalMethod"/> as a generic closure of the current 
        /// <see cref="CliModuleMethod"/> with the <paramref name="genericReplacements"/></returns>
        public override IModuleGlobalMethod MakeGenericClosure(ITypeCollectionBase genericReplacements)
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
                var flags = this.Metadata.Flags;
                if ((flags & MethodAttributes.Public) == MethodAttributes.Public)
                    return AccessLevelModifiers.Public;
                else if ((flags & MethodAttributes.Assembly) == MethodAttributes.Assembly)
                    return AccessLevelModifiers.Internal;
                else if ((flags & MethodAttributes.Private) == MethodAttributes.Private)
                    return AccessLevelModifiers.Private;
                else if ((flags & MethodAttributes.Family) == MethodAttributes.Family)
                    return AccessLevelModifiers.Protected;
                else if ((flags & MethodAttributes.FamORAssem) == MethodAttributes.FamORAssem)
                    return AccessLevelModifiers.ProtectedOrInternal;
                else if ((flags & MethodAttributes.FamANDAssem) == MethodAttributes.FamANDAssem)
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
    }
}
