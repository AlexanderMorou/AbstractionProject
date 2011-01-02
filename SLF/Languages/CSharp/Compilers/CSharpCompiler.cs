using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public class CSharpCompiler :
        IntermediateCompiler<ICSharpCompilationUnit>,
        ICSharpCompiler
    {
        private ICSharpProvider provider;
        internal CSharpCompiler (ICSharpProvider provider)
        {
            this.provider = provider;
        }

        public CSharpCompiler()
            : base()
        {
            
        }

        public override IHighLevelLanguageProvider<ICSharpCompilationUnit> Provider
        {
            get { return this.provider; }
        }

        private ICSharpProvider _Provider
        {
            get
            {
                return (ICSharpProvider)this.Provider;
            }
        }

        protected override bool NeedsToRewrite(RewriteSectors expansionElement)
        {
            switch (expansionElement)
            {
                case RewriteSectors.LinqExpression:
                case RewriteSectors.LambdaExpression:
                    switch (this._Provider.Language.Version)
                    {
                        case CSharpLanguageVersion.CSharp_v2:
                        case CSharpLanguageVersion.CSharp_v3:
                        case CSharpLanguageVersion.CSharp_v3_5:
                            break;
                        case CSharpLanguageVersion.CSharp_v4:
                        case CSharpLanguageVersion.CSharp_v5:
                        default:
                            return false;
                    }
                    goto default;
                case RewriteSectors.ConditionalOperation:
                case RewriteSectors.YieldFunctionality:
                case RewriteSectors.CreateArray:
                    return false;
                case RewriteSectors.CommaExpression:
                case RewriteSectors.WorkspaceExpression:
                case RewriteSectors.DuckTyping:
                default:
                    return base.NeedsToRewrite(expansionElement);
            }
        }

        public override ICompilerResults Compile(IIntermediateAssembly source)
        {
            return base.Compile(source);
        }
    }
}
