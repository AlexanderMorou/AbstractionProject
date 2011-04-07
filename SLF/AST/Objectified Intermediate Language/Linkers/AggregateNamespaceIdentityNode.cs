using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public class AggregateNamespaceIdentityNode :
        AggregateIdentityNode,
        IAggregateNamespaceIdentityNode,
        IControlledStateCollection<KeyValuePair<string, IAggregateIdentityNode>>
    {
        private string[] identifiers;
        /// <summary>
        /// The data member for <see cref="Namespaces"/>.
        /// </summary>
        private INamespaceDeclaration[] namespaces;
        /// <summary>
        /// Creates a new <see cref="AggregateNamespaceIdentityNode"/>
        /// with the <paramref name="namespaces"/> provided.
        /// </summary>
        /// <param name="namespaces">The <see cref="INamespaceDeclaration"/> array
        /// which represents the identities that make up the aggregate namespace identity.</param>
        internal AggregateNamespaceIdentityNode(INamespaceDeclaration[] namespaces) 
        {
            this.namespaces = namespaces;
        }

        #region IAggregateNamespaceIdentityNode Members

        public IEnumerable<INamespaceDeclaration> Namespaces
        {
            get
            {
                foreach (var @namespace in this.namespaces)
                    yield return @namespace;
            }
        }

        #endregion


        public override string Name
        {
            get { return this.Namespaces.First().Name; }
        }

        /// <summary>
        /// Returns the <see cref="AggregateIdentityKind"/> which denotes
        /// the kind of identity represented by the current
        /// <see cref="AggregateNamespaceIdentityNode"/>.
        /// </summary>
        /// <remarks>Returns <see cref="AggregateIdentityKind.Namespace"/>.</remarks>
        public override AggregateIdentityKind Kind
        {
            get { return AggregateIdentityKind.Namespace; }
        }

        #region IControlledStateCollection<KeyValuePair<string,IAggregateIdentityNode>> Members

        int IControlledStateCollection<KeyValuePair<string, IAggregateIdentityNode>>.Count
        {
            get { throw new NotImplementedException(); }
        }

        bool IControlledStateCollection<KeyValuePair<string, IAggregateIdentityNode>>.Contains(KeyValuePair<string, IAggregateIdentityNode> item)
        {
            throw new NotImplementedException();
        }

        void IControlledStateCollection<KeyValuePair<string, IAggregateIdentityNode>>.CopyTo(KeyValuePair<string, IAggregateIdentityNode>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        KeyValuePair<string, IAggregateIdentityNode> IControlledStateCollection<KeyValuePair<string, IAggregateIdentityNode>>.this[int index]
        {
            get { throw new NotImplementedException(); }
        }

        KeyValuePair<string, IAggregateIdentityNode>[] IControlledStateCollection<KeyValuePair<string, IAggregateIdentityNode>>.ToArray()
        {
            throw new NotImplementedException();
        }

        int IControlledStateCollection<KeyValuePair<string, IAggregateIdentityNode>>.IndexOf(KeyValuePair<string, IAggregateIdentityNode> element)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region IEnumerable<KeyValuePair<string,IAggregateIdentityNode>> Members

        public IEnumerator<KeyValuePair<string, IAggregateIdentityNode>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IControlledStateDictionary<string,IAggregateIdentityNode> Members

        public IControlledStateCollection<string> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public IControlledStateCollection<IAggregateIdentityNode> Values
        {
            get { throw new NotImplementedException(); }
        }

        public IAggregateIdentityNode this[string key]
        {
            get { throw new NotImplementedException(); }
        }

        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out IAggregateIdentityNode value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
