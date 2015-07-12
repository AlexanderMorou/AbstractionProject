using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Provides a series of event arguments for when a generic parameter is moved
    /// from one position to another.
    /// </summary>
    public class GenericParameterMovedEventArgs :
        EventArgs
    {
        /// <summary>
        /// Creates a new <see cref="GenericParameterMovedEventArgs"/>
        /// instance with the <paramref name="from"/>, <paramref name="to"/> and
        /// <paramref name="parameter"/>
        /// </summary>
        /// <param name="from">The <see cref="Int32"/> of where the <paramref name="parameter"/>
        /// was.</param>
        /// <param name="to">The <see cref="Int32"/> of where the <paramref name="parameter"/>
        /// went to.</param>
        /// <param name="parameter">The <see cref="IIntermediateGenericParameter"/>
        /// that was moved.</param>
        public GenericParameterMovedEventArgs(int from, int to, IIntermediateGenericParameter parameter)
        {
            this.Parameter = parameter;
            this.From = from;
            this.To = to;
        }

        /// <summary>
        /// Returns the <see cref="Int32"/> ordinal index, relative to the <see cref="IIntermediateGenericParameter.Parent"/>,
        /// of the <see cref="Parameter"/> prior to the move.
        /// </summary>
        public int From { get; private set; }

        /// <summary>
        /// Returns the <see cref="Int32"/> ordinal index, relative to the <see cref="IIntermediateGenericParameter.Parent"/>,
        /// of the <see cref="Parameter"/> after to the move.
        /// </summary>
        public int To { get; private set; }

        /// <summary>
        /// Returns the <see cref="IIntermediateGenericParameter"/> which was
        /// repositioned.
        /// </summary>
        public IIntermediateGenericParameter Parameter { get; private set; }

        internal bool InRange(int count)
        {
            if (From < 0 || From > count ||
                To < 0 || To > count)
                return false;
            return true;
        }
    }
}
