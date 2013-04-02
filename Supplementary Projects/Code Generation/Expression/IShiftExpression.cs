using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.OldCodeGen.Expression
{
    public enum ShiftDirection
    { 
        Left,
        Right
    }

    public interface IShiftExpression :
        IExpression
    {
        IExpression LeftSide { get; set; }
        ShiftDirection Direction { get; set; }
        IExpression RightSide { get; set; }
    }
}
