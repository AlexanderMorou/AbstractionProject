using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides a base implementation of an intermediate assembly that
    /// targets a specific language and provider.
    /// </summary>
    /// <typeparam name="TLanguage">The kind of <see cref="ILanguage"/>
    /// which is targeted by the <see cref="IntermediateAssembly{TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>.</typeparam>
    /// <typeparam name="TProvider">The kind of <see cref="ILanguageProvider"/>
    /// which provides the functional gateway to the instances of the 
    /// <see cref="IntermediateAssembly{TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>.</typeparam>
    /// <typeparam name="TIdentityManager">The <see cref="IIdentityManager{TTypeIdentity, TAssemblyIdentity}"/>
    /// which maintains consistent type and assembly identity.</typeparam>
    /// <typeparam name="TAssemblyIdentity">The identity used to obtain assembly references.</typeparam>
    /// <typeparam name="TTypeIdentity">The identity used to denote types within the 
    /// identity manager.</typeparam>
    public class IntermediateAssembly<TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity> :
        IntermediateAssembly<TLanguage, TProvider, IntermediateAssembly<TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity>, TIdentityManager, TTypeIdentity, TAssemblyIdentity>
        where TLanguage :
            ILanguage
        where TProvider :
            ILanguageProvider
        where TIdentityManager :
            IIdentityManager<TTypeIdentity, TAssemblyIdentity>
    {
        private TLanguage language;
        private TProvider provider;
        /// <summary>
        /// Creates a new <see cref="IntermediateAssembly{TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/> with the
        /// <paramref name="name"/>, <paramref name="provider"/>, and <paramref name="language"/>
        /// provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name which is used to
        /// help differentiate the <see cref="IntermediateAssembly{TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>
        /// from others.</param>
        /// <param name="provider">The <typeparamref name="TProvider"/>
        /// which created the <see cref="IntermediateAssembly{TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>.</param>
        /// <param name="language">The <typeparamref name="TLanguage"/> in which
        /// the <see cref="IntermediateAssembly{TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>
        /// targets.</param>
        internal IntermediateAssembly(string name, TProvider provider, TLanguage language)
            : base(name)
        {
            this.provider = provider;
            this.language = language;
        }

        protected IntermediateAssembly(IntermediateAssembly<TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity> root)
            : base(root)
        {
        }

        /// <summary>
        /// Creates a new part for the <see cref="IntermediateAssembly{TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>
        /// which is used to represent a new file of the assembly.
        /// </summary>
        /// <returns>A new <see cref="IntermediateAssembly{TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>
        /// which is a new partial of the current root.</returns>
        protected override IntermediateAssembly<TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity> GetNewPart()
        {
            return new IntermediateAssembly<TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity>(this);
        }

        public override TLanguage Language
        {
            get
            {
                if (this.IsRoot)
                    return this.language;
                else
                    return this.GetRoot().language;
            }
        }

        public override TProvider Provider
        {
            get
            {
                if (this.IsRoot)
                    return this.provider;
                else
                    return this.GetRoot().provider;
            }
        }

    }
}
