using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.OldCodeGen.Types
{
    public interface IAutoCommentType :
        IType
    {
        /// <summary>
        /// Returns/sets the <see cref="String"/> value associated to the 
        /// documentation comments generated with the code for the type
        /// under the summary tag.
        /// </summary>
        string Summary { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="String"/> value associated to the 
        /// documentation comments generated with the code for the type
        /// under the remarks tag.
        /// </summary>
        string Remarks { get; set; }
    }
}
