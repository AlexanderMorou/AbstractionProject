using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    public interface IGoToLabelStatement :
        IStatement<CodeGotoStatement>
    {
        /// <summary>
        /// Returns/sets the label the statement directs the execution to.
        /// </summary>
        ILabelStatement LabelStatement { get; set; }
    }
}
