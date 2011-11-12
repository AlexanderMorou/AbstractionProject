using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.Cil
{
    /// <summary>
    /// Provides a default base type for an intermediate assembly.
    /// </summary>
    public sealed class CommonIntermediateAssembly :
        IntermediateAssembly<ICommonIntermediateLanguage, ICommonIntermediateProvider, CommonIntermediateAssembly>,
        ICommonIntermediateAssembly
    {
        /// <summary>
        /// Data member for <see cref="Provider"/>.
        /// </summary>
        private ICommonIntermediateProvider provider;
        /// <summary>
        /// Creates a new <see cref="CommonIntermediateAssembly"/>
        /// with the <paramref name="root"/> assembly provided.
        /// </summary>
        /// <param name="root">The <see cref="CommonIntermediateAssembly"/>
        /// which acts as the root instance of the intermediate assembly
        /// whose source spans multiple files.</param>
        internal CommonIntermediateAssembly(CommonIntermediateAssembly root)
            : base(root)
        {

        }

        /// <summary>
        /// Creates a new <see cref="CommonIntermediateAssembly"/> with
        /// the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The name of the 
        /// <see cref="CommonIntermediateAssembly"/>.</param>
        internal CommonIntermediateAssembly(string name)
            : this(name, CommonIntermediateLanguage.Singleton.GetProvider())
        {
        }

        internal CommonIntermediateAssembly(string name, ICommonIntermediateProvider provider)
            : base(name)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Obtains a new <see cref="CommonIntermediateAssembly"/>
        /// instance with the current <see cref="CommonIntermediateAssembly"/>
        /// as the root.
        /// </summary>
        /// <returns>A new <see cref="CommonIntermediateAssembly"/>
        /// instance with the current <see cref="CommonIntermediateAssembly"/>
        /// as the root.</returns>
        /// <remarks>Unless <see cref="GetNewPart"/> is called upon
        /// a root <see cref="CommonIntermediateAssembly"/>, it will fail.
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">
        /// thrown when the current assembly is not the root
        /// assembly.</exception>
        protected override CommonIntermediateAssembly GetNewPart()
        {
            if (this.IsRoot)
                return new CommonIntermediateAssembly(this);
            else
                throw new InvalidOperationException();
        }


        #region IIntermediateAssembly<ICommonIntermediateLanguage,ICommonIntermediateProvider> Members

        /// <summary>
        /// Returns the <see cref="ICommonIntermediateLanguage">language</see> in which the 
        /// <see cref="CommonIntermediateAssembly"/> is written in.
        /// </summary>
        public override ICommonIntermediateLanguage Language
        {
            get
            {
                return CommonIntermediateLanguage.Singleton;
            }
        }

        /// <summary>
        /// Returns the <see cref="ICommonIntermediateProvider">provider</see>
        /// which created the <see cref="CommonIntermediateAssembly"/>.
        /// </summary>
        public override ICommonIntermediateProvider Provider
        {
            get
            {
                if (this.IsRoot)
                    return this.provider;
                else
                    return this.GetRoot().provider;
            }
        }

        #endregion
    }
}
