using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Linkers
{
    public class AssemblyReferenceIdentityAggregate :
        AggregateNamespaceParentIdentityNode,
        IAssemblyReferenceIdentityAggregate
    {
        /// <summary>
        /// Creates a new <see cref="AssemblyReferenceIdentityAggregate"/> initialized
        /// to its default state with the <paramref name="source"/>,
        /// seed <paramref name="references"/>, and <paramref name="aliases"/>
        /// provided.
        /// </summary>
        /// <param name="source">
        /// The <see cref="IAggregateAliasReferenceGroups"/> which acts as
        /// a data source for the <see cref="AssemblyReferenceIdentityAggregate"/>.
        /// </param>
        /// <param name="references">
        /// The <see cref="IAssemblyReference"/> series which denotes the
        /// set of assemblies which define the <see cref="AssemblyReferenceIdentityAggregate"/>.
        /// </param>
        /// <param name="aliases">
        /// The <see cref="String"/> series which denotes the external aliases
        /// the <see cref="AssemblyReferenceIdentityAggregate"/> is referred 
        /// to by.
        /// </param>
        internal AssemblyReferenceIdentityAggregate(IAggregateAliasReferenceGroups source, IEnumerable<IAssemblyReference> references, string alias)
            : base((from r in references
                    where r.Reference != null
                    select r.Reference).ToArray())
        {
            /* *
             * Utilize LINQ to duplicate and obscure the fact that
             * it's from an array on the aliases and references.
             * *
             * The duplicate is to ensure that if they access the members
             * illegally (think this is no longer possible)
             * it's in vein.
             * */
            this.Alias = alias;
            this.IdentitySource = source;
            this.References = from r in references.ToArray()
                              where r.Reference != null
                              orderby r.Reference.Name
                              select r;
        }

        #region IAssemblyReferenceIdentityAggregate Members

        /// <summary>
        /// Returns the <see cref="String"/> values which 
        /// represents the alias associated to the series of assembly
        /// references aggregated into the current identity set.
        /// </summary>
        public string Alias { get; private set; }

        /// <summary>
        /// Returns the <see cref="IAssemblyReference"/> set which is 
        /// collected into an aggregate identity set.
        /// </summary>
        public IEnumerable<IAssemblyReference> References { get; private set; }

        /// <summary>
        /// Returns the <see cref="IAggregateAliasReferenceGroups"/> from which the
        /// assembly namespace identities are aggregated from.
        /// </summary>
        public IAggregateAliasReferenceGroups IdentitySource { get; private set; }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Disposes the members associated to the <see cref="AssemblyReferenceIdentityAggregate"/>.
        /// </summary>
        public override void Dispose()
        {
            try
            {
                this.Alias = null;
                this.References = null;
                this.IdentitySource = null;
            }
            finally
            {
                base.Dispose();
            }
        }

        #endregion
        /// <summary>
        /// Returns the name of the root level namespace.
        /// </summary>
        /// <remarks>Returns a null value (C&#9839;), or Nothing (VB.NET).</remarks>
        public override string Name
        {
            get { return null; }
        }

        /// <summary>
        /// Identifies the kind of aggregate identity relative to the type of node it is.
        /// </summary>
        /// <remarks>
        /// Returns <see cref="AggregateIdentityKind.RootNamespace"/>.
        /// </remarks>
        public override AggregateIdentityKind Kind
        {
            get { return AggregateIdentityKind.RootNamespace; }
        }

        /// <summary>
        /// Obtains the parent of the current aggregate identity node.
        /// </summary>
        /// <returns>Returns a null value (C&#9839;), or Nothing (VB.NET).</returns>
        protected override IAggregateIdentityNode OnGetParent()
        {
            return null;
        }

        internal void AddReference(IAssemblyReference reference)
        {
            throw new NotImplementedException();
        }
    }
}
