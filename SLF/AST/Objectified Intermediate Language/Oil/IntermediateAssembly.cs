using System;
using System.Collections.Generic;
using System.Text;
/*---------------------------------------------------------------------\
| Copyright © 2009 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a default base type for an intermediate assembly.
    /// </summary>
    public sealed class IntermediateAssembly :
        IntermediateAssembly<IntermediateAssembly>
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateAssembly"/>
        /// with the <paramref name="root"/> assembly provided.
        /// </summary>
        /// <param name="root">The <see cref="IntermediateAssembly"/>
        /// which acts as the root instance of the intermediate assembly
        /// whose source spans multiple files.</param>
        internal IntermediateAssembly(IntermediateAssembly root)
            : base(root)
        {

        }

        /// <summary>
        /// Creates a new <see cref="IntermediateAssembly"/> with
        /// the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The name of the 
        /// <see cref="IntermediateAssembly"/>.</param>
        internal IntermediateAssembly(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Obtains a new <see cref="IntermediateAssembly"/>
        /// instance with the current <see cref="IntermediateAssembly"/>
        /// as the root.
        /// </summary>
        /// <returns>A new <see cref="IntermediateAssembly"/>
        /// instance with the current <see cref="IntermediateAssembly"/>
        /// as the root.</returns>
        /// <remarks>Unless <see cref="GetNewPart"/> is called upon
        /// a root <see cref="IntermediateAssembly"/>, it will fail.
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">
        /// thrown when the current assembly is not the root
        /// assembly.</exception>
        protected override IntermediateAssembly GetNewPart()
        {
            if (this.IsRoot)
                return new IntermediateAssembly(this);
            else
                throw new InvalidOperationException();
        }
        
    }
}
