using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using AllenCopeland.Abstraction.Slf.Translation;
namespace AllenCopeland.Abstraction.Slf.Languages
{
    internal class OilIntermediateLanguage : 
        IOilIntermediateLanguage
    {
        static readonly ExpressionKind supportedExpressionKinds = new ExpressionKind(ExpressionKind.BinaryOperationSector.All, ExpressionKind.ExpansionRequiredSector.All, ExpressionKind.InvocationSector.All, ExpressionKind.PrimitiveInsertSector.All, ExpressionKind.ReferenceSector.All, ExpressionKind.SpecialFunctionSector.All, ExpressionKind.SymbolSector.All, ExpressionKind.UnaryOperationSector.All);
        #region IOilIntermediateLanguage Members

        public IOilProvider GetProvider()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IHighLevelLanguage<IIntermediateAssembly> Members

        IHighLevelLanguageProvider<IIntermediateAssembly> IHighLevelLanguage<IIntermediateAssembly>.GetProvider()
        {
            return this.GetProvider();
        }

        #endregion

        #region ILanguage Members

        public string Name
        {
            get { return "Objectified Intermediate Langauge"; }
        }

        ILanguageProvider ILanguage.GetProvider()
        {
            return this.GetProvider();
        }

        public CompilerSupport CompilerSupport
        {
            get { return CompilerSupport.FullSupport ^ CompilerSupport.Win32Resources; }
        }

        public ExpressionKind SupportedExpressions
        {
            get { return supportedExpressionKinds; }
        }

        public StatementKinds SupportedStatements
        {
            get { return StatementKinds.All; }
        }

        #endregion
    }
}
