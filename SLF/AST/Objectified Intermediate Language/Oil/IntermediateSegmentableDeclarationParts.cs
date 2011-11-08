using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Oil;
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
    /// <summary>
    /// Provides a base implementation of 
    /// <see cref="IIntermediateSegmentableDeclarationPartCollection"/>.
    /// </summary>
    /// <typeparam name="TDeclaration">The type of <see cref="IIntermediateSegmentableDeclaration"/>
    /// which needs partialalbe functionality</typeparam>
    /// <typeparam name="TInstDeclaration">The specific type of <typeparamref name="TDeclaration"/>
    /// used within the parts that is instantiated and used as the root and partial
    /// elements.</typeparam>
    public class IntermediateSegmentableDeclarationParts<TIdentifier, TDeclaration, TInstDeclaration> :
        ControlledStateCollection<TDeclaration>,
        IIntermediateSegmentableDeclarationPartCollection<TIdentifier, TDeclaration>,
        IIntermediateSegmentableDeclarationPartCollection
        where TIdentifier :
            IDeclarationUniqueIdentifier<TIdentifier>
        where TDeclaration :
            class,
            IIntermediateSegmentableDeclaration<TIdentifier, TDeclaration>
        where TInstDeclaration :
            class,
            TDeclaration//,
            //new (TDeclaration rootDeclaration)
    {
        private TDeclaration rootDeclaration;
        private Func<TInstDeclaration> creator;
        private string identityName;

        #region IIntermediateSegmentableDeclarationParts<TDeclaration> Members

        public TDeclaration Root
        {
            get { return rootDeclaration; }
        }

        public TDeclaration Add()
        {
            TDeclaration t = creator();
            if (t == null)
                return default(TDeclaration);
            lock (this.baseList)
                this.baseList.Add(t);
            return t;
        }

        public void Remove(TDeclaration part)
        {
            if (!this.Contains(part))
                throw new ArgumentException("part");
            part.Dispose();
            lock (this.baseList)
                this.baseList.Remove(part);
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= this.Count)
                throw new ArgumentOutOfRangeException("index");
            this.Remove(this[index]);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this.baseList == null)
                return;
            foreach (var t in this)
                t.Dispose();
            this.baseList.Clear();
            this.baseList = null;
        }

        #endregion

        #region IntermediateSegmentableDeclarationParts{TDeclaration} Constructors

        /// <summary>
        /// Creates a new <see cref="IntermediateSegmentableDeclarationParts{TIdentifier, TDeclaration, TInstDeclaration}"/>
        /// with the <paramref name="rootDeclaration"/>, <paramref name="creator"/>, and <paramref name="identityName"/> provided.
        /// </summary>
        /// <param name="rootDeclaration">The <typeparamref name="TDeclaration"/> which is the root instance
        /// of the series.</param>
        /// <param name="creator">The <see cref="Func{T}"/> which handles object creation, providing the root instance
        /// to obtain the partial instance.</param>
        /// <param name="identityName">The <see cref="String"/> value that identifies the 
        /// sequence as a concept.</param>
        public IntermediateSegmentableDeclarationParts(TDeclaration rootDeclaration, Func<TInstDeclaration> creator, string identityName)
        {
            this.creator = creator;
            this.rootDeclaration = rootDeclaration;
            this.identityName = identityName;
        }

        #endregion

        #region IIntermediateSegmentableDeclarationParts Members

        IIntermediateSegmentableDeclaration IIntermediateSegmentableDeclarationPartCollection.Root
        {
            get {  return this.Root; }
        }

        IIntermediateSegmentableDeclaration IIntermediateSegmentableDeclarationPartCollection.Add()
        {
            return this.Add();
        }

        #endregion

        #region IIntermediateSegmentableDeclarationPartCollection Members


        void IIntermediateSegmentableDeclarationPartCollection.Add(IIntermediateSegmentableDeclaration part)
        {
            if (!(part is TDeclaration))
                throw new ArgumentException("part");
            this.Add((TDeclaration)(part));
        }

        #endregion

        /// <summary>
        /// Obtains the word used to describe the elements of the sequence.
        /// </summary>
        /// <returns>The <see cref="String"/> value that describes the instance
        /// in readable form.</returns>
        protected abstract string GetIdentityName();

        #region IIntermediateSegmentableDeclarationPartCollection<TDeclaration> Members

        public void Add(TDeclaration part)
        {
            if (part.IsRoot)
                if (part == this.Root)
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.part, ExceptionMessageId.Part_CannotBeRoot, this.identityName);
                else
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.part, ExceptionMessageId.Part_RootOfASeparateSeries, this.identityName);
            if (part.GetRoot() != this.Root)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.part, ExceptionMessageId.Part_MustReferenceRoot, this.identityName);
            this.baseList.Add(part);
        }

        #endregion
    }
}
