using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public abstract class AggregateNamespaceParentIdentityNode :
        AggregateIdentitySetNode<string, IAggregateIdentityNode>,
        IAggregateNamespaceParentIdentityNode
    {

        private string[] identifiers;
        private MutableDictionary<int, IAggregateIdentityNode> lazyIdentities = new MutableDictionary<int, IAggregateIdentityNode>();
        /// <summary>
        /// The data member for <see cref="Namespaces"/>.
        /// </summary>
        private INamespaceParent[] namespaces;
        /// <summary>
        /// Creates a new <see cref="AggregateNamespaceIdentityNode"/>
        /// with the <paramref name="namespaces"/> provided.
        /// </summary>
        /// <param name="namespaces">The <see cref="INamespaceDeclaration"/> array
        /// which represents the identities that make up the aggregate namespace identity.</param>
        internal AggregateNamespaceParentIdentityNode(INamespaceParent[] namespaces) 
        {
            this.namespaces = namespaces;
        }

        public override bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public override int Count
        {
            get {
                this.InitializeCheck();
                return this.identifiers.Length;
            }
        }

        protected override string GetLimiter(int index)
        {
            if (index < 0 || index >= this.Count)
                throw new ArgumentOutOfRangeException("index");
            return this.identifiers[index];
        }

        protected override IAggregateIdentityNode GetIdentity(int index)
        {
            if (!this.lazyIdentities.ContainsKey(index))
            {
                var key = this.GetLimiter(index);
                var childSpaces = (from ns in this.namespaces
                                   from childSpace in ns.Namespaces.Values
                                   where childSpace.Name == key
                                   orderby childSpace.Name
                                   select childSpace).ToArray();
                var childTypes = (from ns in this.namespaces
                                  from t in ns.Types.Values
                                  let type = t.Entry
                                  where type.Name == key
                                  orderby type.Name
                                  select type).ToArray();
                bool isAmbiguityNode = childSpaces.Length > 0 && childTypes.Length> 0;
                if (isAmbiguityNode)
                {
                }
                else
                {
                    if (childSpaces.Length > 0)
                        this.lazyIdentities.Add(index, new AggregateNamespaceIdentityNode(this, childSpaces));
                    else if (childTypes.Length > 0)
                    {

                    }
                    else//ToDo: Add global methods which are scoped by namespace.
                    {
                        //Unknown?
                    }
                }
            }
            return this.lazyIdentities[key: index];
        }

        private void InitializeCheck()
        {
            if (this.identifiers == null)
                this.Initialize();
        }

        private void Initialize()
        {
            this.identifiers =
                (from n in this.namespaces
                 from a in n.AggregateIdentifiers
                 orderby a ascending
                 select a).Distinct().ToArray();

        }



        #region IDisposable Members

        public override void Dispose()
        {
            try
            {
                this.namespaces = null;
                this.identifiers = null;
                this.lazyIdentities.Values.OnAll(p => p.Dispose());
                this.lazyIdentities.Clear();
            }
            finally
            {
                GC.SuppressFinalize(this);
            }
        }

        #endregion
    }
}
