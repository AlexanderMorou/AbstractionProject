using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Utilities.Collections;
using AllenCopeland.Abstraction.OldCodeGen.Expression;

namespace AllenCopeland.Abstraction.OldCodeGen.Statements
{
    /// <summary>
    /// Defines properties and methods for working with the conditional parts of a 
    /// <see cref="IConditionSeriesStatement"/>.
    /// </summary>
    public interface IConditionSeriesParts :
        IControlledStateCollection<IConditionBlock>
    {
        IConditionBlock AddCondition(IExpression condition);
        IConditionBlock AddCondition(IExpression condition, IStatement[] statements);
    }
}
