using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using System.Reflection;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public enum CSharpLanguageVersion
    {
        CSharp_v2,
        CSharp_v3,
        CSharp_v3_5,
        CSharp_v4,
        CSharp_v5,
    }
    internal class CSharpLanguage :
        ICSharpLanguage
    {
        private static readonly ExpressionKind supportedExpressions = new ExpressionKind( ExpressionKind.BinaryOperationSector.All ^ (ExpressionKind.BinaryOperationSector.IntegerDivisionOperation | ExpressionKind.BinaryOperationSector.FlexibleDivisionOperation),  ExpressionKind.ExpansionRequiredSector.ConditionalOperation | ExpressionKind.ExpansionRequiredSector.CreateArray | ExpressionKind.ExpansionRequiredSector.LambdaExpression | ExpressionKind.ExpansionRequiredSector.LinqExpression, ExpressionKind.InvocationSector.ConstructorInvoke | ExpressionKind.InvocationSector.EventFire | ExpressionKind.InvocationSector.MethodCall | ExpressionKind.InvocationSector.MultiCastDelegateCall, ExpressionKind.PrimitiveInsertSector.All, ExpressionKind.ReferenceSector.All ^ ( ExpressionKind.ReferenceSector.CurrentTypeReference | ExpressionKind.ReferenceSector.SelfReference), ExpressionKind.SpecialFunctionSector.All ^ ExpressionKind.SpecialFunctionSector.VariadicTypeCast, ExpressionKind.SymbolSector.All, ExpressionKind.UnaryOperationSector.All);
        private CSharpLanguageVersion versionCompatability;

        public CSharpLanguage(CSharpLanguageVersion versionCompatability = CSharpLanguageVersion.CSharp_v5)
        {
            this.versionCompatability = versionCompatability;
        }

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

        /// <summary>
        /// Returns the <see cref="String"/> value representing
        /// the unique name of the language.
        /// </summary>
        /// <remarks>Returns "C&#9839;".</remarks>
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

        /// <summary>
        /// Returns a new <see cref="ICSharpProvider"/> associated to the current
        /// <see cref="CSharpLanguage"/>.
        /// </summary>
        /// <returns>A new <see cref="ICSharpProvider"/> for the current
        /// <see cref="CSharpLanguage"/>.</returns>
        public ICSharpProvider GetProvider()
        {
            return new CSharpProvider(this.versionCompatability);
        }

        #endregion

        #region IHighLevelLanguage<ICSharpCompilationUnit> Members

        IHighLevelLanguageProvider<ICSharpCompilationUnit> IHighLevelLanguage<ICSharpCompilationUnit>.GetProvider()
        {
            return this.GetProvider();
        }

        #endregion

        #region ILanguage Members
        /// <summary>
        /// Returns the level of functionality support the 
        /// compiler contains.
        /// </summary>
        public CompilerSupport CompilerSupport
        {
            get {
                
                CompilerSupport result = CompilerSupport.FullSupport ^ (CompilerSupport.Win32Resources | CompilerSupport.PrimaryInteropEmbedding | CompilerSupport.DuckTyping);
                if (((int)versionCompatability) >= (int)CSharpLanguageVersion.CSharp_v4)
                    result |= CompilerSupport.PrimaryInteropEmbedding;
                return result;
            }
        }

        #endregion

        #region ICSharpLanguage Members


        public CSharpLanguageVersion Version
        {
            get { return this.versionCompatability; }
        }

        #endregion
    }
}
