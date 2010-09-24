using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers.StateMachines
{
    public interface ITokenizerStateMachine<TToken> :
        ITokenizerStateMachine
    {
        /// <summary>
        /// Obtains a token for the given <paramref name="location"/> provided the state machine is in the
        /// proper state.
        /// </summary>
        /// <param name="location">The <see cref="FileLocale"/> which designates where the token
        /// should be placed in the file.</param>
        /// <returns>A <typeparamref name="TToken"/> instance relative to the state of the current
        /// <see cref="ITokenizerStateMachine{TToken}"/>.</returns>
        new TToken GetToken(FileLocale location);
    }
    public interface ITokenizerStateMachine
    {
        /// <summary>
        /// Instructs the state machine to transition to the next
        /// state using the <paramref name="char"/> provided.
        /// </summary>
        /// <param name="char">The <see cref="Char"/> which represents the next character in the
        /// stream.</param>
        /// <returns>true if the state-machine can still process characters after this
        /// character.</returns>
        bool Next(char @char);
        /// <summary>
        /// Returns whether the state machine is in a valid exit state.
        /// </summary>
        bool IsValidEndState { get; }
        /// <summary>
        /// Sets the <see cref="ITokenizerStateMachine"/> back to the original state.
        /// </summary>
        void Reset();
        /// <summary>
        /// Obtains a token for the given <paramref name="location"/> provided the state machine is in the
        /// proper state.
        /// </summary>
        /// <param name="location">The <see cref="FileLocale"/> which designates where the token
        /// should be placed in the file.</param>
        /// <returns>A <see cref="IToken"/> instance relative to the state of the current
        /// <see cref="ITokenizerStateMachine"/>.</returns>
        IToken GetToken(FileLocale location);
        /// <summary>
        /// Returns the number of bytes consumed when the
        /// state machine is in a valid exit point; zero otherwise.
        /// </summary>
        ulong BytesConsumed { get; }
    }
}
