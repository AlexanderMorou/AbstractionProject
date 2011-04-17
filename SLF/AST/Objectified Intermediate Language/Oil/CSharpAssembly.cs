using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.CSharp
{
    /// <summary>
    /// Provides a base implementation for an assembly
    /// defined within the C&#9839; language.
    /// </summary>
    public class CSharpAssembly :
        IntermediateAssembly<CSharpAssembly>,
        ICSharpAssembly
    {
        /// <summary>
        /// Creates a new <see cref="CSharpAssembly"/> which is linked to another
        /// <paramref name="root"/> <see cref="CSharpAssembly"/>.
        /// </summary>
        /// <param name="root">The <see cref="CSharpAssembly"/>
        /// from which the current is a part of.</param>
        internal protected CSharpAssembly(CSharpAssembly root)
            : base(root)
        {
        }

        /// <summary>
        /// Creates a new <see cref="CSharpAssembly"/> with the
        /// <paramref name="name"/> of the assembly provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// which aids in differentiating the <see cref="CSharpAssembly"/>
        /// from other <see cref="IAssembly"/> instances.</param>
        internal protected CSharpAssembly(string name)
            : base(name)
        {

        }

        /// <summary>
        /// Returns a new <see cref="CSharpAssembly"/> for the 
        /// 
        /// </summary>
        /// <returns></returns>
        protected override CSharpAssembly GetNewPart()
        {
            return new CSharpAssembly(this);
        }

    }
}
