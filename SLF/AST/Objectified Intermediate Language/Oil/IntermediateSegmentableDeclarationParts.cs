using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.CompilerServices;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;
using System.Collections.Generic;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
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
            lock (this.baseCollection)
                this.baseCollection.Add(t);
            return t;
        }

        public void Remove(TDeclaration part)
        {
            if (!this.Contains(part))
                throw new ArgumentException("part");
            part.Dispose();
            lock (this.baseCollection)
                this.baseCollection.Remove(part);
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
            if (this.baseCollection == null)
                return;
            foreach (var t in this)
                t.Dispose();
            this.baseCollection.Clear();
            this.baseCollection = null;
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

        #region IList Members

        int IList.Add(object value)
        {
            if (value is TDeclaration)
            {
                TDeclaration v = (TDeclaration)value;
                if (v.GetRoot().Equals(this.rootDeclaration))
                {
                    if (v.GetRoot().Equals(v))
                        return -1;
                    if (!this.Contains(v))
                    {
                        int c = this.Count;
                        this.baseCollection.Add(v);
                        return c;
                    }
                    else
                    {
                        int index = 0;
                        foreach (TDeclaration k in this)
                        {
                            if (v.Equals(k))
                                return index;
                            index++;
                        }
                        return -1;
                    }
                }
            }
            throw new ArgumentException("value");

        }

        void IList.Clear()
        {
            for (int i = 0; i < this.Count; i++)
                this[i].Dispose();
            this.baseCollection.Clear();
        }

        bool IList.Contains(object value)
        {
            if (!(value is TDeclaration))
                return false;
            return this.baseCollection.Contains(((TDeclaration)(value)));
        }

        int IList.IndexOf(object value)
        {
            int index = 0;
            if (!(value is TDeclaration))
                return -1;
            TDeclaration k = (TDeclaration)value;

            foreach (var v in this)
            {
                if (v.Equals(k))
                    return index;
                index++;
            }
            throw new ArgumentException("value");
        }

        void IList.Insert(int index, object value)
        {
            if (!(value is TDeclaration))
                throw new ArgumentException("value");
            TDeclaration v = (TDeclaration)value;
            List<TDeclaration> k = (List<TDeclaration>)(this.baseCollection);
            k.Insert(index, v);
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        bool IList.IsReadOnly
        {
            get { return false; }
        }

        void IList.Remove(object value)
        {
            if (!(value is TDeclaration))
                throw new ArgumentException("value");
            TDeclaration v = (TDeclaration)value;
            this.baseCollection.Remove(v);
        }

        void IList.RemoveAt(int index)
        {
            if (index < 0 || index >= this.Count)
                throw new ArgumentOutOfRangeException("index");
            List<TDeclaration> k = (List<TDeclaration>)(this.baseCollection);
            k.RemoveAt(index);
        }

        object IList.this[int index]
        {
            get
            {
                return ((List<TDeclaration>)this.baseCollection)[index];
            }
            set
            {
                if (!(value is TDeclaration))
                    throw new ArgumentException("value");
                TDeclaration v = (TDeclaration)value;
                if (this.Contains(v))
                    throw new InvalidOperationException();
                if (v.GetRoot().Equals(this.rootDeclaration))
                {
                    ((List<TDeclaration>)(this.baseCollection))[index] = v;
                    return;
                }
                throw new ArgumentException("value");
            }
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
            this.baseCollection.Add(part);
        }

        #endregion
    }
}
