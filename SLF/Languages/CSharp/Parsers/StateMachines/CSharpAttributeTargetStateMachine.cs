using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Parsers.Tokens;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers.StateMachines
{
    /// <summary>
    /// Provides a state machine for an attribute target in the 
    /// C&#9839; language.
    /// </summary>
    [CLSCompliant(false)]
    public class CSharpAttributeTargetStateMachine :
        ITokenizerStateMachine<ICSharpAttributeTargetToken>
    {
        #region CSharpAttributeTargetStateMachine data members.
        /// <summary>
        /// Data member representing the current state which is used to transition to the next state.
        /// </summary>
        private int state;
        /// <summary>
        /// Data member representing a valid exit state.
        /// </summary>
        private int exitState;
        #endregion // CSharpAttributeTargetStateMachine data members.

        #region ITokenizerStateMachine<ICSharpAttributeTargetToken> Members

        /// <summary>
        /// Obtains a token for the given <paramref name="location"/> provided the state machine is in the
        /// proper state.
        /// </summary>
        /// <param name="location">The <see cref="FileLocale"/> which designates where the token
        /// should be placed in the file.</param>
        /// <returns>A <see cref="ICSharpAttributeTargetToken"/> instance relative to the state of the current
        /// <see cref="CSharpAttributeTargetStateMachine"/>.</returns>
        public ICSharpAttributeTargetToken GetToken(FileLocale location)
        {
            CSharpAttributeTarget target = CSharpAttributeTarget.None;
            switch (this.exitState)
            {
                case 36:
                    target = CSharpAttributeTarget.Type;
                     /* ------------------------\
                     |  Build TypeTarget here.  |
                     |  Actual value: type      |
                     \------------------------ */
                    break;
                case 38:
                    target = CSharpAttributeTarget.Event;
                    /* -------------------------\
                    |  Build EventTarget here.  |
                    |  Actual value: event      |
                    \------------------------- */
                    break;
                case 41:
                    target = CSharpAttributeTarget.Property;
                    /* ----------------------------\
                    |  Build PropertyTarget here.  |
                    |  Actual value: property      |
                    \---------------------------- */
                    break;
                case 42:
                    target = CSharpAttributeTarget.Assembly;
                    /* ----------------------------\
                    |  Build AssemblyTarget here.  |
                    |  Actual value: assembly      |
                    \---------------------------- */
                    break;
                case 39:
                    target = CSharpAttributeTarget.ReturnType;
                    /* ------------------------------\
                    |  Build ReturnTypeTarget here.  |
                    |  Actual value: return          |
                    \------------------------------ */
                    break;
                case 40:
                    target = CSharpAttributeTarget.MethodTarget;
                    /* --------------------------\
                    |  Build MethodTarget here.  |
                    |  Actual value: method      |
                    \-------------------------- */
                    break;
                case 37:
                    target = CSharpAttributeTarget.FieldTarget;
                    /* -------------------------\
                    |  Build FieldTarget here.  |
                    |  Actual value: field      |
                    \------------------------- */
                    break;
            }
            if (target != CSharpAttributeTarget.None)
                return new CSharpAttributeTargetToken(location, target);
            throw new InvalidOperationException("Invalid State");
        }

        #endregion

        #region ITokenizerStateMachine Members

        /// <summary>
        /// Instructs the state machine to transition to the next
        /// state using the <paramref name="char"/> provided.
        /// </summary>
        /// <param name="char">The <see cref="Char"/> which represents the next character in the
        /// stream.</param>
        /// <returns>true if the state-machine can still process characters after this
        /// character.</returns>
        public bool Next(char @char)
        {
            switch (this.state)
            {
                case 0:
                    switch (@char)
                    {
                        case 'a':
                            this.state = 29;
                            // Current: a
                            return true;
                        case 'e':
                            this.state = 30;
                            // Current: e
                            return true;
                        case 'f':
                            this.state = 32;
                            // Current: f
                            return true;
                        case 'm':
                            this.state = 4;
                            // Current: m
                            return true;
                        case 'p':
                            this.state = 13;
                            // Current: p
                            return true;
                        case 'r':
                            this.state = 2;
                            // Current: r
                            return true;
                        case 't':
                            this.state = 15;
                            // Current: t
                            return true;
                    }
                    break;
                case 1:
                    if ((@char == 'e'))
                    {
                        this.state = (this.exitState = 36);
                        // Yields: TypeTarget
                        return false;
                    }
                    break;
                case 2:
                    if ((@char == 'e'))
                    {
                        this.state = 8;
                        // Current: re
                        return true;
                    }
                    break;
                case 3:
                    if ((@char == 'e'))
                    {
                        this.state = 14;
                        // Current: prope
                        return true;
                    }
                    break;
                case 4:
                    if ((@char == 'e'))
                    {
                        this.state = 10;
                        // Current: me
                        return true;
                    }
                    break;
                case 5:
                    if ((@char == 'e'))
                    {
                        this.state = 25;
                        // Current: fie
                        return true;
                    }
                    break;
                case 6:
                    if ((@char == 'e'))
                    {
                        this.state = 23;
                        // Current: eve
                        return true;
                    }
                    break;
                case 7:
                    if ((@char == 'e'))
                    {
                        this.state = 24;
                        // Current: asse
                        return true;
                    }
                    break;
                case 8:
                    if ((@char == 't'))
                    {
                        this.state = 31;
                        // Current: ret
                        return true;
                    }
                    break;
                case 9:
                    if ((@char == 't'))
                    {
                        this.state = 16;
                        // Current: propert
                        return true;
                    }
                    break;
                case 10:
                    if ((@char == 't'))
                    {
                        this.state = 33;
                        // Current: met
                        return true;
                    }
                    break;
                case 11:
                    if ((@char == 't'))
                    {
                        this.state = (this.exitState = 38);
                        // Yields: EventTarget
                        return false;
                    }
                    break;
                case 12:
                    if ((@char == 'r'))
                    {
                        this.state = 22;
                        // Current: retur
                        return true;
                    }
                    break;
                case 13:
                    if ((@char == 'r'))
                    {
                        this.state = 20;
                        // Current: pr
                        return true;
                    }
                    break;
                case 14:
                    if ((@char == 'r'))
                    {
                        this.state = 9;
                        // Current: proper
                        return true;
                    }
                    break;
                case 15:
                    if ((@char == 'y'))
                    {
                        this.state = 18;
                        // Current: ty
                        return true;
                    }
                    break;
                case 16:
                    if ((@char == 'y'))
                    {
                        this.state = (this.exitState = 41);
                        // Yields: PropertyTarget
                        return false;
                    }
                    break;
                case 17:
                    if ((@char == 'y'))
                    {
                        this.state = (this.exitState = 42);
                        // Yields: AssemblyTarget
                        return false;
                    }
                    break;
                case 18:
                    if ((@char == 'p'))
                    {
                        this.state = 1;
                        // Current: typ
                        return true;
                    }
                    break;
                case 19:
                    if ((@char == 'p'))
                    {
                        this.state = 3;
                        // Current: prop
                        return true;
                    }
                    break;
                case 20:
                    if ((@char == 'o'))
                    {
                        this.state = 19;
                        // Current: pro
                        return true;
                    }
                    break;
                case 21:
                    if ((@char == 'o'))
                    {
                        this.state = 27;
                        // Current: metho
                        return true;
                    }
                    break;
                case 22:
                    if ((@char == 'n'))
                    {
                        this.state = (this.exitState = 39);
                        // Yields: ReturnTypeTarget
                        return false;
                    }
                    break;
                case 23:
                    if ((@char == 'n'))
                    {
                        this.state = 11;
                        // Current: even
                        return true;
                    }
                    break;
                case 24:
                    if ((@char == 'm'))
                    {
                        this.state = 34;
                        // Current: assem
                        return true;
                    }
                    break;
                case 25:
                    if ((@char == 'l'))
                    {
                        this.state = 28;
                        // Current: fiel
                        return true;
                    }
                    break;
                case 26:
                    if ((@char == 'l'))
                    {
                        this.state = 17;
                        // Current: assembl
                        return true;
                    }
                    break;
                case 27:
                    if ((@char == 'd'))
                    {
                        this.state = (this.exitState = 40);
                        // Yields: MethodTarget
                        return false;
                    }
                    break;
                case 28:
                    if ((@char == 'd'))
                    {
                        this.state = (this.exitState = 37);
                        // Yields: FieldTarget
                        return false;
                    }
                    break;
                case 29:
                    if ((@char == 's'))
                    {
                        this.state = 35;
                        // Current: as
                        return true;
                    }
                    break;
                case 30:
                    if ((@char == 'v'))
                    {
                        this.state = 6;
                        // Current: ev
                        return true;
                    }
                    break;
                case 31:
                    if ((@char == 'u'))
                    {
                        this.state = 12;
                        // Current: retu
                        return true;
                    }
                    break;
                case 32:
                    if ((@char == 'i'))
                    {
                        this.state = 5;
                        // Current: fi
                        return true;
                    }
                    break;
                case 33:
                    if ((@char == 'h'))
                    {
                        this.state = 21;
                        // Current: meth
                        return true;
                    }
                    break;
                case 34:
                    if ((@char == 'b'))
                    {
                        this.state = 26;
                        // Current: assemb
                        return true;
                    }
                    break;
                case 35:
                    if ((@char == 's'))
                    {
                        this.state = 7;
                        // Current: ass
                        return true;
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// Returns whether the state machine is in a valid exit state.
        /// </summary>
        public bool IsValidEndState
        {
            get { return this.exitState != 0; }
        }

        /// <summary>
        /// Sets the <see cref="CSharpAttributeTargetStateMachine"/> back to the original state.
        /// </summary>
        public void Reset()
        {
            this.state = this.exitState = 0;
        }

        IToken ITokenizerStateMachine.GetToken(FileLocale location)
        {
            return this.GetToken(location);
        }

        /// <summary>
        /// Returns the number of bytes consumed when the
        /// state machine is in a valid exit point; zero otherwise.
        /// </summary>
        public ulong BytesConsumed
        {
            get
            {
                switch (this.exitState)
                {
                    case 36://TypeTarget
                        return 4;
                    case 37://FieldTarget
                    case 38://EventTarget
                        return 5;
                    case 39://ReturnTypeTarget
                    case 40://MethodTarget
                        return 6;
                    case 41://PropertyTarget
                    case 42://AssemblyTarget
                        return 8;
                    default://Something else.
                        return 0;
                }
            }
        }

        #endregion
    }
}
