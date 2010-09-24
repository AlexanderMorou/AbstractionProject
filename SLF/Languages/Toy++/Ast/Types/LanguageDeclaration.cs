using System;
using System.Collections.Generic;
using System.Text;
/*---------------------------------------------------------------------\
| Copyright © 2010 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a base implementation of a language declaration.
    /// </summary>
    public class LanguageDeclaration :
        IntermediateClassType<LanguageDeclaration>
    {
        /// <summary>
        /// Creates a new <see cref="LanguageDeclaration"/> with the
        /// <paramref name="name"/> and <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="name">The name of the current 
        /// <see cref="LanguageDeclaration"/>.</param>
        /// <param name="parent">The <see cref="ILanguageParent"/>
        /// which contains the <see cref="LanguageDeclaration"/>.</param>
        internal LanguageDeclaration(string name, ILanguageParent parent)
            : base(name, parent)
        {
        }

        public LanguageDeclaration(LanguageDeclaration root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }

        internal LanguageDeclaration(IIntermediateTypeParent parent)
            : base(parent)
        {

        }

        protected override LanguageDeclaration GetNewPartial(LanguageDeclaration root, IIntermediateTypeParent parent)
        {
            return new LanguageDeclaration(root, parent);
        }
    }
}
