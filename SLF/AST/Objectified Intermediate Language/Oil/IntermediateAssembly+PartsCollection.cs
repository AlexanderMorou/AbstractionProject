using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;
using System.Diagnostics;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
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
            IIntermediateSegmentableDeclarationPartCollection<IIntermediateAssembly>,
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

            #endregion

            #region IList Members

            int IList.Add(object value)
            {
                if (!(value is TAssembly))
                    throw new ArgumentException("value");
                int c = this.Count;
                this.baseList.Add((TAssembly)value);
                return c;
            }

            void IList.Clear()
            {
                foreach (var item in this)
                    item.Dispose();
                this.baseList.Clear();
            }

            bool IList.Contains(object value)
            {
                if (!(value is TAssembly))
                    return false;
                return this.Contains((TAssembly)value);
            }

            int IList.IndexOf(object value)
            {
                if (!(value is TAssembly))
                    return -1;
                int index = 0;
                foreach (var item in this)
                    if (item == value)
                        return index;
                    else
                        index++;
                return -1;
            }

            void IList.Insert(int index, object value)
            {
                if (!(value is TAssembly))
                    throw new ArgumentException("value");
                TAssembly v = (TAssembly)value;
                List<IIntermediateAssembly> k = (List<IIntermediateAssembly>)(this.baseList);
                k.Insert(index, v);
            }

            /// <summary>
            /// Returns whether the <see cref="PartsCollection"/> is
            /// fixed-length.
            /// </summary>
            /// <remarks>Returns false.</remarks>
            public bool IsFixedSize
            {
                get { return false; }
            }

            /// <summary>
            /// Returns whether the <see cref="PartsCollection"/>
            /// is read-only.
            /// </summary>
            /// <remarks>Returns false.</remarks>
            public bool IsReadOnly
            {
                get { return false; }
            }

            void IList.Remove(object value)
            {
                if (!(value is TAssembly))
                    throw new ArgumentException("value");
                TAssembly v = (TAssembly)value;
                this.baseList.Remove(v);
            }

            void IList.RemoveAt(int index)
            {
                if (index < 0 || index >= this.Count)
                    throw new ArgumentOutOfRangeException("index");
                List<IIntermediateAssembly> k = (List<IIntermediateAssembly>)(this.baseList);
                k.RemoveAt(index);
            }

            object IList.this[int index]
            {
                get
                {
                    return ((List<IIntermediateAssembly>)this.baseList)[index];
                }
                set
                {
                    if (!(value is TAssembly))
                        throw new ArgumentException("value");
                    TAssembly v = (TAssembly)value;
                    if (this.Contains(v))
                        throw new InvalidOperationException();
                    if (v.GetRoot().Equals(this.Root))
                    {
                        ((List<IIntermediateAssembly>)(this.baseList))[index] = v;
                        return;
                    }
                    throw new ArgumentException("value");
                }
            }

            #endregion

            #region IIntermediateSegmentableDeclarationPartCollection Members

            void IIntermediateSegmentableDeclarationPartCollection.Add(IIntermediateSegmentableDeclaration part)
            {
                if (part is IIntermediateAssembly)
                    this.Add((IIntermediateAssembly)(part));
                else
                    throw new ArgumentException("part");
            }

            #endregion

            #region IIntermediateSegmentableDeclarationPartCollection<IIntermediateAssembly> Members

            public void Add(IIntermediateAssembly part)
            {
                if (part.IsRoot || part.GetRoot() != this.Root)
                    throw new ArgumentException("part");
                base.AddImpl(part);
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
