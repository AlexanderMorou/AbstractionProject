﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public partial class BlockStatementScopeLabelDictionary :
        IBlockStatementLabelDictionary
    {
        private IBlockStatementParent initialPoint;
        private KeysCollection keys;
        private ValuesCollection values;

        public BlockStatementScopeLabelDictionary(IBlockStatementParent initialPoint)
        {
            this.initialPoint = initialPoint;
        }

        private IEnumerable<ILabelStatement> GetLabels()
        {
            var current = initialPoint;
            Stack<IBlockStatementParent> parents = new Stack<IBlockStatementParent>();
            while (current != null)
            {
                parents.Push(current);
                if (current is IBlockStatement)
                    current = ((IBlockStatement)current).Parent;
                else
                    break;
            }
            while (parents.Count > 0)
                foreach (var label in parents.Pop().Labels.Values)
                    yield return label;
            yield break;
        }


        #region IControlledDictionary<string,ILabelStatement> Members

        public IControlledCollection<string> Keys
        {
            get {
                if (this.keys == null)
                    this.keys = new KeysCollection(this);
                return this.keys;
            }
        }

        public IControlledCollection<ILabelStatement> Values
        {
            get {
                if (this.values == null)
                    this.values = new ValuesCollection(this);
                return this.values;
            }
        }

        public ILabelStatement this[string key]
        {
            get
            {
                ILabelStatement result;
                if (TryGetValue(key, out result))
                    return result;
                throw new KeyNotFoundException();
            }
        }

        public bool ContainsKey(string key)
        {
            return this.Keys.Contains(key);
        }

        public bool TryGetValue(string key, out ILabelStatement value)
        {
            foreach (var label in this.GetLabels())
                if (label.Name == key)
                {
                    value = label;
                    return true;
                }
            value = null;
            return false;
        }

        #endregion

        #region IControlledCollection<KeyValuePair<string,ILabelStatement>> Members

        public int Count
        {
            get { return this.GetLabels().Count(); }
        }

        public bool Contains(KeyValuePair<string, ILabelStatement> item)
        {
            foreach (var label in this.GetLabels())
                if (label.Name == item.Key &&
                    item.Value == label)
                    return true;
            return false;
        }

        public void CopyTo(KeyValuePair<string, ILabelStatement>[] array, int arrayIndex = 0)
        {
            this.ToArray().CopyTo(array, arrayIndex);
        }

        public KeyValuePair<string, ILabelStatement> this[int index]
        {
            get {
                int i = 0;
                foreach (var label in this.GetLabels())
                {
                    if (i++ == index)
                        return new KeyValuePair<string, ILabelStatement>(label.Name, label);
                }
                throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
            }
        }

        public KeyValuePair<string, ILabelStatement>[] ToArray()
        {
            return Enumerable.ToArray(this);
        }

        public int IndexOf(KeyValuePair<string, ILabelStatement> element)
        {
            int index = 0;
            foreach (var label in this.GetLabels())
            {
                if (label.Name == element.Key &&
                    element.Value == label)
                    return index;
                else
                    index++;
            }
            return -1;
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,ILabelStatement>> Members

        public IEnumerator<KeyValuePair<string, ILabelStatement>> GetEnumerator()
        {
            return (from label in this.GetLabels()
                    select new KeyValuePair<string, ILabelStatement>(label.Name, label)).GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
