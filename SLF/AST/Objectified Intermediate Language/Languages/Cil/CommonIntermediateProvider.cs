using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.Cil
{
    public class CommonIntermediateProvider :
        ICommonIntermediateProvider
    {
        #region ICommonIntermediateProvider Members

        /// <summary>
        /// Returns the 
        /// <see cref="ICommonIntermediateLanguage">Common Intermediate Language</see>.
        /// </summary>
        public ICommonIntermediateLanguage Language
        {
            get { return CommonIntermediateLanguage.Singleton; }
        }

        /// <summary>
        /// Creates a new <see cref="ICommonIntermediateAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="ICommonIntermediateAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        public ICommonIntermediateAssembly CreateAssembly(string name)
        {
            return new CommonIntermediateAssembly(name, this);
        }

        #endregion

        #region ILanguageProvider Members

        ILanguage ILanguageProvider.Language
        {
            get { return this.Language; }
        }

        IIntermediateAssembly ILanguageProvider.CreateAssembly(string name)
        {
            return this.CreateAssembly(name);
        }

        #endregion
    }
}
