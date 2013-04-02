using System;
using System.Collections.Generic;
using System.Text;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    public interface IOperatorOverloadMember
    {
        /// <summary>
        /// The operator overloaded.
        /// </summary>
        int Operator { get; set; }
    }
}
