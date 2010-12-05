using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Oil.Statements;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    internal class CSharpLanguage :
        ICSharpLanguage
    {
        private static readonly ExpressionKind supportedExpressions = new ExpressionKind( ExpressionKind.BinaryOperationSector.All ^ (ExpressionKind.BinaryOperationSector.IntegerDivisionOperation | ExpressionKind.BinaryOperationSector.FlexibleDivisionOperation),  ExpressionKind.ExpansionRequiredSector.ConditionalOperation | ExpressionKind.ExpansionRequiredSector.CreateArray | ExpressionKind.ExpansionRequiredSector.LambdaExpression | ExpressionKind.ExpansionRequiredSector.LinqExpression, ExpressionKind.InvocationSector.ConstructorInvoke | ExpressionKind.InvocationSector.EventFire | ExpressionKind.InvocationSector.MethodCall | ExpressionKind.InvocationSector.MultiCastDelegateCall, ExpressionKind.PrimitiveInsertSector.All, ExpressionKind.ReferenceSector.All ^ ( ExpressionKind.ReferenceSector.CurrentTypeReference | ExpressionKind.ReferenceSector.SelfReference), ExpressionKind.SpecialFunctionSector.All ^ ExpressionKind.SpecialFunctionSector.VariadicTypeCast, ExpressionKind.SymbolSector.All, ExpressionKind.UnaryOperationSector.All);
        internal static readonly ReadOnlyCollection<Type> AutoFormTypes = new ReadOnlyCollection<Type>
            (
                new List<Type>
                (
                    new Type[]
                        {
                            typeof(byte),
                            typeof(sbyte),
                            typeof(ushort),
                            typeof(short),
                            typeof(uint),
                            typeof(int),
                            typeof(ulong),
                            typeof(long),
                            typeof(void),
                            typeof(bool),
                            typeof(char),
                            typeof(decimal),
                            typeof(float),
                            typeof(double),
                            typeof(object),
                            typeof(string)
                        }
                )
            );

        #region ILanguage Members

        public string Name
        {
            get { return "C\u266f"; }
        }

        ILanguageProvider ILanguage.GetProvider()
        {
            return this.GetProvider();
        }

        #endregion

        #region ICSharpLanguage Members

        public ICSharpProvider GetProvider()
        {
            return new CSharpProvider();
        }

        #endregion

        #region IHighLevelLanguage<ICSharpCompilationUnit> Members

        IHighLevelLanguageProvider<ICSharpCompilationUnit> IHighLevelLanguage<ICSharpCompilationUnit>.GetProvider()
        {
            return this.GetProvider();
        }

        #endregion

        #region ILanguage Members


        public CompilerSupport CompilerSupport
        {
            get { return Compilers.CompilerSupport.FullSupport ^ Compilers.CompilerSupport.Win32Resources; }
        }

        #endregion
    }
}
