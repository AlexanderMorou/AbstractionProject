using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public class NamespaceInclusionScopeCoercion :
        INamespaceInclusionScopeCoercion
    {
        #region INamespaceInclusionScopeCoercion Members

        /// <summary>
        /// Returns/sets the <see cref="String"/> value associated to the 
        /// namespace to include to coerce identity resolution.
        /// </summary>
        public string Namespace { get; set; }

        #endregion

        #region ISourceElement Members

        /// <summary>
        /// Returns/sets the filename associated to the <see cref="NamespaceInclusionScopeCoercion"/>.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The <see cref="LineColumnPair"/> which denotes
        /// the start point of the <see cref="NamespaceInclusionScopeCoercion"/>.
        /// </summary>
        public LineColumnPair? Start { get; set; }

        /// <summary>
        /// The <see cref="LineColumnPair"/> which denotes the
        /// end point of the <see cref="NamespaceInclusionScopeCoercion"/>.
        /// </summary>
        public LineColumnPair? End { get; set; }

        #endregion

    }
}
