using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateAssembly<TAssembly>
        where TAssembly :
            IntermediateAssembly<TAssembly>
    {
        /// <summary>
        /// Provides a part collection for an intermediate assembly.
        /// </summary>
        [DebuggerDisplay("Assembly Files: {Count}")]
        protected class PartsCollection :
            ControlledStateCollection<IIntermediateAssembly>,
            IIntermediateSegmentableDeclarationPartCollection<IAssemblyUniqueIdentifier, IIntermediateAssembly>,
            IIntermediateSegmentableDeclarationPartCollection
        {
            private TAssembly root;
            /// <summary>
            /// Creates a new <see cref="PartsCollection"/> with the 
            /// <paramref name="root"/> provided.
            /// </summary>
            /// <param name="root">
            /// The <see cref="IntermediateAssembly{TAssembly}"/>
            /// which owns the <see cref="PartsCollection"/>.
            /// </param>
            public PartsCollection(TAssembly root)
            {
                this.root = root;
            }

            #region IIntermediateSegmentableDeclarationPartCollection<IIntermediateAssembly> Members

            public IIntermediateAssembly Root
            {
                get { return this.root; }
            }

            /// <summary>
            /// Inserts and returns a new 
            /// <see cref="IIntermediateAssembly"/> as a part of the
            /// <see cref="Root"/>.
            /// </summary>
            /// <returns>A new <see cref="IIntermediateAssembly"/> as
            /// a part of the <see cref="Root"/>.</returns>
            public IIntermediateAssembly Add()
            {
                TAssembly result = root.GetNewPart();
                base.AddImpl(result);
                return result;
            }

            public void Add(IIntermediateAssembly part)
            {
                if (part.IsRoot)
                    if (part == this.Root)
                        throw ThrowHelper.ObtainArgumentException(ArgumentWithException.part, ExceptionMessageId.Part_CannotBeRoot, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.assembly));
                    else
                        throw ThrowHelper.ObtainArgumentException(ArgumentWithException.part, ExceptionMessageId.Part_RootOfASeparateSeries, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.assembly));
                if (part.GetRoot() != this.Root)
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.part, ExceptionMessageId.Part_MustReferenceRoot, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.assembly));
                base.AddImpl(part);
            }

            #endregion

            #region IIntermediateSegmentableDeclarationPartCollection Members

            IIntermediateSegmentableDeclaration IIntermediateSegmentableDeclarationPartCollection.Root
            {
                get { return this.Root; }
            }

            IIntermediateSegmentableDeclaration IIntermediateSegmentableDeclarationPartCollection.Add()
            {
                return this.Add();
            }

            void IIntermediateSegmentableDeclarationPartCollection.Add(IIntermediateSegmentableDeclaration part)
            {
                if (part is IIntermediateAssembly)
                    this.Add((IIntermediateAssembly)(part));
                else
                    throw new ArgumentException("part");
            }

            #endregion

        }

        /// <summary>
        /// Obtains a new <typeparamref name="TAssembly"/> instance
        /// with the current <see cref="IntermediateAssembly{TAssembly}"/>
        /// as the root.
        /// </summary>
        /// <returns>A new <typeparamref name="TAssembly"/>
        /// instance with the current
        /// <see cref="IntermediateAssembly{TAssembly}"/> as the root.
        /// </returns>
        /// <remarks>Unless <see cref="GetNewPart"/> is called upon
        /// a root <see cref="IntermediateAssembly{TAssembly}"/>, it will fail.
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">
        /// thrown when the current assembly is not the root
        /// assembly.</exception>
        protected abstract TAssembly GetNewPart();
    }
}
