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
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public enum CSharpLanguageVersion
    {
        /// <summary>
        /// CSharp version 2 support enables generics.
        /// </summary>
        Version2,
        /// <summary>
        /// CSharp version 3 support enables LINQ, Extension methods and
        /// anonymous types and methods.
        /// </summary>
        Version3,
        /// <summary>
        /// CSharp version 4 support enables a Dynamic typing model,
        /// better COM inerop, and primary interop assembly embedding.
        /// </summary>
        Version4,
        /// <summary>
        /// CSharp version 5 support enables the concept of asynchronous 
        /// coding models.
        /// </summary>
        Version5,
    }
    internal class CSharpLanguage :
        ICSharpLanguage
    {
        private CSharpLanguageVersion versionCompatability;

        public CSharpLanguage(CSharpLanguageVersion versionCompatability = CSharpLanguageVersion.Version5)
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
                if (((int)versionCompatability) >= (int)CSharpLanguageVersion.Version4)
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
