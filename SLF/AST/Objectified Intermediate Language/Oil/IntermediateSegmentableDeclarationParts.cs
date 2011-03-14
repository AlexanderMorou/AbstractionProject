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
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
    public class IntermediateSegmentableDeclarationParts<TDeclaration, TInstDeclaration> :
        ControlledStateCollection<TDeclaration>,
        IIntermediateSegmentableDeclarationPartCollection<TDeclaration>,
        IIntermediateSegmentableDeclarationPartCollection
        where TDeclaration :
            class,
            IIntermediateSegmentableDeclaration<TDeclaration>
        where TInstDeclaration :
            class,
            TDeclaration//,
            //new (TDeclaration rootDeclaration)
    {
        private TDeclaration rootDeclaration;
        private Func<TInstDeclaration> creator;

        #region IIntermediateSegmentableDeclarationParts<TDeclaration> Members

        public TDeclaration Root
        {
            get { return rootDeclaration; }
        }

        public TDeclaration Add()
        {
            TDeclaration t = creator();
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

        public IntermediateSegmentableDeclarationParts(TDeclaration rootDeclaration, Func<TInstDeclaration> creator)
        {
            this.creator = creator;
            this.rootDeclaration = rootDeclaration;
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

        #region IIntermediateSegmentableDeclarationPartCollection<TDeclaration> Members


        public void Add(TDeclaration part)
        {
            if (part.IsRoot || part.GetRoot() != this.Root)
                throw new ArgumentException("root");
            this.baseList.Add(part);
        }

        #endregion
    }
}
