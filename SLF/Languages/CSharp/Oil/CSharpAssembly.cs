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

        internal protected CSharpAssembly(string name)
            : base(name)
        {

        }
        protected override CSharpAssembly GetNewPart()
        {
            return new CSharpAssembly(this);
        }
    }
}
