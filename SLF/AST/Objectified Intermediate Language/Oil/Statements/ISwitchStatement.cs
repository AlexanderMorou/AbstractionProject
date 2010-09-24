using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    /// <summary>
    /// Defines properties and methods for working with 
    /// a switch statement.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Switch/Select statements are jump tables underneath the 
    /// syntactic sugar. Each case is a label, wherein the switch
    /// upon the case is shifted as close to zero as possible and
    /// a switch forward to the various label points is created.
    /// </para>
    /// <para>
    /// In string series, a dictionary is used, which uses the 
    /// string as a key with an index to the jump table's cases
    /// as the value half of the pair.
    /// </para>
    /// </remarks>
    public interface ISwitchStatement :
        IControlledStateCollection<ISwitchCaseBlockStatement>,
        IStatement
    {
        /// <summary>
        /// Returns the <see cref="ILocalMemberDictionary"/> associated
        /// to the <see cref="ISwitchStatement"/> which is shared
        /// across all <see cref="ISwitchCaseBlockStatement"/>
        /// instances.
        /// </summary>
        ILocalMemberDictionary Locals { get; }
        /// <summary>
        /// Returns the <see cref="ISwitchCaseBlockStatement"/> which
        /// represents the default target for the switch statement.
        /// </summary>
        ISwitchCaseBlockStatement DefaultBlock { get; }
        /// <summary>
        /// Returns the <see cref="IBreakExit"/> which denotes the 
        /// exit point of the current <see cref="ISwitchStatement"/>.
        /// </summary>
        IBreakExit BreakExit { get; }
        new IBlockStatementParent Parent { get; }
        /// <summary>
        /// The <see cref="IExpression"/> which selects the target
        /// for the constant jump table.
        /// </summary>
        IExpression Selection { get; set; }
        /// <summary>
        /// Returns a new <see cref="ISwitchCaseBlockStatement"/>
        /// which activates when one of the the <paramref name="conditions"/>
        /// are true.
        /// </summary>
        /// <param name="conditions">The <see cref="IExpression"/>
        /// series which represents the constant series of values relative
        /// to the current switch case block statement.</param>
        /// <returns>A new <see cref="ISwitchCaseBlockStatement"/>
        /// from the <paramref name="conditions"/>
        /// provided.</returns>
        ISwitchCaseBlockStatement Case(params IExpression[] conditions);
        /// <summary>
        /// Returns a new <see cref="ISwitchCaseBlockStatement"/>
        /// which activates when one of the the <paramref name="conditions"/>
        /// are true and designates whether the resulted
        /// <see cref="ISwitchCaseBlockStatement"/> is the default case.
        /// </summary>
        /// <param name="isDefault">Whether the resulted
        /// <see cref="ISwitchCaseBlockStatement"/> is the default case
        /// or not.</param>
        /// <param name="conditions">The <see cref="IExpression"/>
        /// series which represents the constant series of values relative
        /// to the current switch case block statement.</param>
        /// <returns>A new <see cref="ISwitchCaseBlockStatement"/>
        /// from the <paramref name="conditions"/>
        /// provided.</returns>
        ISwitchCaseBlockStatement Case(bool isDefault, params IExpression[] conditions);
        
    }
}
