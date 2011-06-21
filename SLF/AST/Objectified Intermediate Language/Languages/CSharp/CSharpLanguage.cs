using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Diagnostics.SymbolStore;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    public enum CSharpLanguageVersion
    {
        /// <summary>
        /// CSharp version 2 support provided the initial implementation of
        /// generics.
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
        private const CSharpLanguageVersion DefaultVersion = CSharpLanguageVersion.Version5;
        internal static readonly CSharpLanguage Singleton = new CSharpLanguage();
        public CSharpLanguage()
        {
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
        /// Returns a new <see cref="ICSharpProvider"/> associated to the
        /// <see cref="CSharpLanguage">C&#9839; language</see>.
        /// </summary>
        /// <returns>A new <see cref="ICSharpProvider"/> for the 
        /// <see cref="CSharpLanguage">C&#9839; language</see>.</returns>
        public ICSharpProvider GetProvider()
        {
            return this.GetProvider(CSharpLanguage.DefaultVersion);
        }

        #endregion

        #region IHighLevelLanguage<ICSharpCompilationUnit> Members

        IHighLevelLanguageProvider<ICSharpCompilationUnit> IHighLevelLanguage<ICSharpCompilationUnit>.GetProvider()
        {
            return this.GetProvider();
        }

        public Guid Guid
        {
            get { return SymLanguageType.CSharp; }
        }

        #endregion

        public IMicrosoftLanguageVendor Vendor
        {
            get
            {
                return LanguageVendors.Microsoft;
            }
        }

        #region ILanguage Members
        /// <summary>
        /// Returns the level of functionality support the 
        /// compiler contains.
        /// </summary>
        public CompilerSupport CompilerSupport
        {
            get {
                return GetCompilerSupport(CSharpLanguage.DefaultVersion);
            }
        }

        ILanguageVendor ILanguage.Vendor
        {
            get { return this.Vendor; }
        }
        #endregion

        #region IVersionedLanguage Members

        public CompilerSupport GetCompilerSupport(CSharpLanguageVersion version)
        {

            CompilerSupport result = CompilerSupport.FullSupport ^ (CompilerSupport.Win32Resources | CompilerSupport.PrimaryInteropEmbedding | CompilerSupport.StructuralTyping);
            if (((int)version) >= (int)CSharpLanguageVersion.Version4)
                result |= CompilerSupport.PrimaryInteropEmbedding;
            return result;
        }

        IVersionedLanguageProvider<CSharpLanguageVersion> IVersionedLanguage<CSharpLanguageVersion>.GetProvider(CSharpLanguageVersion version)
        {
            return GetProvider(version);
        }

        /// <summary>
        /// Returns a <see cref="IEnumerable{T}"/> of
        /// elements which denotes the various 
        /// <see cref="CSharpLanguageVersion">versions</see> of 
        /// the <see cref="CSharpLanguage">C&#9839; language</see>.
        /// </summary>
        public IEnumerable<CSharpLanguageVersion> Versions
        {
            get
            {
                yield return CSharpLanguageVersion.Version2;
                yield return CSharpLanguageVersion.Version3;
                yield return CSharpLanguageVersion.Version4;
                yield return CSharpLanguageVersion.Version5;
            }
        }

        #endregion

        #region IVersionedHighLevelLanguage<CSharpLanguageVersion,ICSharpCompilationUnit> Members

        IVersionedHighLevelLanguageProvider<CSharpLanguageVersion, ICSharpCompilationUnit> IVersionedHighLevelLanguage<CSharpLanguageVersion, ICSharpCompilationUnit>.GetProvider(CSharpLanguageVersion version)
        {
            return GetProvider(version);
        }

        #endregion


        #region ICSharpLanguage Members

        /// <summary>
        /// Returns a new <see cref="ICSharpProvider"/> associated to the
        /// <see cref="CSharpLanguage">C&#9839; language</see> relative
        /// to the <paramref name="version"/>.
        /// </summary>
        /// <param name="version">The <see cref="CSharpLanguageVersion"/>
        /// value which denotes what version of the language to return 
        /// the provider for.</param>
        /// <returns>A new <see cref="ICSharpProvider"/> for the 
        /// <see cref="CSharpLanguage">C&#9839; language</see>.</returns>
        public ICSharpProvider GetProvider(CSharpLanguageVersion version)
        {
            return new CSharpProvider(version);
        }

        #endregion

        #region ICSharpLanguage Members

        /// <summary>
        /// Creates a new <see cref="ICSharpAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="ICSharpAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        public ICSharpAssembly CreateAssembly(string name)
        {
            return new CSharpAssembly(name, this.GetProvider());
        }

        /// <summary>
        /// Creates a new <see cref="IIntermediateAssembly"/>
        /// with the <paramref name="name"/> and 
        /// <paramref name="version"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <param name="version">The <see cref="CSharpLanguageVersion"/>
        /// to which the <see cref="ICSharpAssembly"/>
        /// is built against.</param>
        /// <returns>A new <see cref="ICSharpAssembly"/>
        /// with the <paramref name="name"/> and 
        /// <paramref name="version"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>
        /// or <paramref name="version"/> is not one of 
        /// <see cref="CSharpLanguageVersion"/>.</exception>
        public ICSharpAssembly CreateAssembly(string name, CSharpLanguageVersion version)
        {
            return new CSharpAssembly(name, this.GetProvider(version));
        }

        #endregion

        #region IVersionedHighLevelLanguage<CSharpLanguageVersion,ICSharpCompilationUnit> Members


        IIntermediateAssembly IVersionedHighLevelLanguage<CSharpLanguageVersion, ICSharpCompilationUnit>.CreateAssembly(string name, CSharpLanguageVersion version)
        {
            return this.CreateAssembly(name, version);
        }

        #endregion

        #region IHighLevelLanguage<ICSharpCompilationUnit> Members

        IIntermediateAssembly ILanguage.CreateAssembly(string name)
        {
            return this.CreateAssembly(name);
        }

        #endregion
    }
}
