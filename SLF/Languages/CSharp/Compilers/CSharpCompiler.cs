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

        protected override bool NeedsToRewrite(RewriteSectors expansionElement)
        {
            switch (expansionElement)
            {
                case RewriteSectors.LambdaExpression:
                case RewriteSectors.ConditionalOperation:
                case RewriteSectors.ConditionalForwardTerm:
                case RewriteSectors.CreateArray:
                case RewriteSectors.LinqExpression:
                case RewriteSectors.YieldFunctionality:
                    return false;
                case RewriteSectors.CommaExpression:
                case RewriteSectors.WorkspaceExpression:
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
