using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
	public interface IBlockedStatement<TDom> :
        IStatement<TDom>,
        IBlockedStatement
        where TDom :
            CodeStatement
    {
    }
}
