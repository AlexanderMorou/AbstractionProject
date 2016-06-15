using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
//using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    [DebuggerDisplay("Metadata sets: {Count}, Metadatum count: {FullCount}")]
    public partial class MetadataDefinitionCollection :
        ControlledCollection<IMetadataDefinition>,
        IMetadataDefinitionCollection
    {
        private Wrapper wrapper;
        /// <summary>
        /// Creates a new <see cref="MetadataDefinitionCollection"/> with
        /// the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateMetadataEntity"/>
        /// which needs the attribute collection series.</param>
        /// <param name="owningAssembly">The <see cref="IIntermediateAssembly"/>
        /// which contains the entity which requires metadata.</param>
        public MetadataDefinitionCollection(IIntermediateMetadataEntity parent, IIntermediateAssembly owningAssembly)
        {
            this.Parent = parent;
            this.OwningAssembly = owningAssembly;
        }

        public MetadataDefinitionCollection(MetadataDefinitionCollection wrapped, IIntermediateMetadataEntity parent, IIntermediateAssembly owningAssembly)
            : base(wrapped.baseList)
        {
            this.OwningAssembly = owningAssembly;
            this.Parent = parent;
        }

        #region IMetadataDefinitionCollection Members

        public IMetadataDefinition Add()
        {
            MetadataDefinition target = new MetadataDefinition(this);
            base.baseList.Add(target);
            return target;
        }

        public IMetadataDefinition Add(params MetadatumDefinitionParameterValueCollection[] attributeData)
        {
            MetadataDefinition target = new MetadataDefinition(this);
            foreach (var item in attributeData)
                target.Add(item);
            base.baseList.Add(target);
            return target;
        }

        public IMetadataDefinition Add(params IMetadatumDefinition[] attributes)
        {
            MetadataDefinition target = new MetadataDefinition(this);
            foreach (var item in attributes)
                target.Add(item);
            base.baseList.Add(target);
            return target;
        }

        /// <summary>
        /// Returns the full count of <see cref="IMetadatumDefinition"/> elements
        /// contained within all <see cref="IMetadataDefinition"/>
        /// in the current <see cref="MetadataDefinitionCollection"/>.
        /// </summary>
        public int FullCount
        {
            get
            {
                int count = 0;
                foreach (var item in this)
                    count += item.Count;
                return count;
            }
        }


        /// <summary>
        /// Returns the <see cref="IIntermediateMetadataEntity"/>
        /// which contains the <see cref="MetadataDefinitionCollection"/>.
        /// </summary>
        public IIntermediateMetadataEntity Parent { get; private set; }

        public IMetadatumDefinition[] Flatten()
        {
            int count = 0;
            foreach (var set in this.baseList)
                count += set.Count;
            IMetadatumDefinition[] result = new IMetadatumDefinition[count];
            int k = 0;
            foreach (var item in this)
                foreach (var decl in item)
                    result[k++] = decl;
            return result;
        }

        public bool Contains(IType attributeType)
        {
            foreach (var item in this)
                if (item.Contains(attributeType))
                    return true;
            return false;
        }

        public IMetadatumDefinition this[IType attributeType]
        {
            get {
                /* *
                 * No mru series for this setup, mainly because there's no benefit
                 * to caching instances that may no longer exist as elements of the
                 * series.
                 * *
                 * I *could* setup a versioning system for the definition sets and adjust
                 * the MRU as needed, but that's a bit of a pain, and not worth the effort.
                 * */
                IMetadataDefinition containing = null;
                foreach (var item in this)
                    if (item.Contains(attributeType))
                    {
                        containing = item;
                        break;
                    }
                IMetadatumDefinition closeMatch = null;
                if (containing == null)
                    return null;
                foreach (var item in containing)
                    if (item.Type == attributeType)
                        return item;
                    else if (closeMatch == null && attributeType.IsAssignableFrom(item.Type))
                        closeMatch = item;
                return closeMatch;
            }
        }

        public void Remove(IMetadatumDefinition customAttribute)
        {
            bool found = false;
            List<IMetadataDefinition> containers = new List<IMetadataDefinition>();
            foreach (var set in base.baseList)
                if (set.Contains(customAttribute))
                {
                    containers.Add(set);
                    set.Remove(customAttribute);
                    found = true;
                }
            foreach (var set in containers)
                if (set.Count == 0)
                    base.baseList.Remove(set);
            if (!found)
                throw new ArgumentException("customAttribute provided was not found in any collection of the series.", "customAttribute");
        }

        public void RemoveSet(IEnumerable<IMetadatumDefinition> series)
        {
            List<IMetadataDefinition> containers = new List<IMetadataDefinition>();
            foreach (var item in series)
            {
                foreach (var set in base.baseList)
                {
                    if (set.Contains(item))
                    {
                        if (!containers.Contains(set))
                            containers.Add(set);
                        set.Remove(item);
                    }
                }
            }
            foreach (var set in containers)
                if (set.Count == 0)
                    base.baseList.Remove(set);
        }

        #endregion

        #region IDisposable Members
        /// <summary>
        /// Disposes the <see cref="MetadataDefinitionCollection"/>.
        /// </summary>
        public void Dispose()
        {
            foreach (var item in this)
                item.Dispose();
            if (this.wrapper != null)
                this.wrapper.Dispose();
        }

        #endregion

        internal Wrapper GetWrapper()
        {
            if (this.wrapper == null)
                this.wrapper = new Wrapper(this.Parent);
            return wrapper;
        }

        public IIntermediateIdentityManager IdentityManager
        {
            get { return this.OwningAssembly.IdentityManager; }
        }

        public IIntermediateAssembly OwningAssembly { get; private set; }
    }
}
