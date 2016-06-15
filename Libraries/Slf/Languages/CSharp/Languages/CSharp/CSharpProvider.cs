using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Parsers;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Languages;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Parsers;
using AllenCopeland.Abstraction.Slf.Languages.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    internal partial class CSharpProvider :
        CliVersionedLanguageProvider<ICSharpLanguage, ICSharpProvider, CSharpLanguageVersion, IIntermediateCliManager, Type, Assembly>,
        ICSharpProvider
    {
        internal CSharpProvider(CSharpLanguageVersion version, IIntermediateCliManager identityManager)
            : base(version, identityManager)
        {
            this.RegisterService<IIntermediateAssemblyCtorLanguageService<ICSharpProvider, ICSharpLanguage, ICSharpAssembly>>(LanguageGuids.Services.IntermediateAssemblyCreatorService, new AssemblyService(this));
            this.RegisterService<IIntermediateIdentifierLanguageQualifierService>(LanguageGuids.Services.UniqueIdentifierService, new CSharpIdentifierLanguageQualifierService(this, this.Language));
            this.RegisterService<IExpressionService<ICSharpProvider, ICSharpLanguage>>(LanguageGuids.Services.ExpressionService, new ExpressionService(this));
        }

        protected override ICSharpLanguage OnGetLanguage()
        {
            return CSharpLanguage.Singleton;
        }

        //#region ICSharpProvider Members

        public new ICSharpParser Parser
        {
            get { throw new NotImplementedException(); }
        }

        public new ICSharpCSTTranslator CSTTranslator
        {
            get { throw new NotImplementedException(); }
        }

        public new ICSharpAssembly CreateAssembly(string name)
        {
            return (ICSharpAssembly)base.CreateAssembly(name);
        }

        //#endregion

        /// <summary>
        /// Returns the <see cref="IIntermediateCliManager"/> which marshalls
        /// the identities of intermediate and non-intermediate (compiled)
        /// assemblies and types.
        /// </summary>
        public new IIntermediateCliManager IdentityManager
        {
            get { return (IIntermediateCliManager)base.IdentityManager; }
        }

        public IEnumerable<IType> AutoFormTypes
        {
            get {
                int[] autoformTypes = 
                    new int[]
                    {
                        (int)RuntimeCoreType.Byte, 
                        (int)RuntimeCoreType.SByte,
                        (int)RuntimeCoreType.UInt16,
                        (int)RuntimeCoreType.Int16,
                        (int)RuntimeCoreType.UInt32,
                        (int)RuntimeCoreType.Int32,
                        (int)RuntimeCoreType.UInt64,
                        (int)RuntimeCoreType.Int64,
                        (int)RuntimeCoreType.VoidType, 
                        (int)RuntimeCoreType.Boolean, 
                        (int)RuntimeCoreType.Char, 
                        (int)RuntimeCoreType.Decimal,
                        (int)RuntimeCoreType.Single, 
                        (int)RuntimeCoreType.Double,
                        (int)RuntimeCoreType.RootType,
                        (int)RuntimeCoreType.String
                    };
                foreach (var autoformType in autoformTypes)
                    yield return this.IdentityManager.ObtainTypeReference((RuntimeCoreType)autoformType);
            }
        }
    }
}
