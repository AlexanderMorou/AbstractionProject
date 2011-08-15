using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    [DebuggerDisplay("Attribute Groups: {Count}")]
    public partial class CustomAttributeDefinitionCollectionSeries :
        ControlledStateCollection<ICustomAttributeDefinitionCollection>,
        ICustomAttributeDefinitionCollectionSeries
    {
        private Wrapper wrapper;
        /// <summary>
        /// Creates a new <see cref="CustomAttributeDefinitionCollectionSeries"/> with
        /// the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateCustomAttributedDeclaration"/>
        /// which needs the attribute collection series.</param>
        public CustomAttributeDefinitionCollectionSeries(IIntermediateCustomAttributedDeclaration parent)
        {
            this.Parent = parent;
        }

        public CustomAttributeDefinitionCollectionSeries(CustomAttributeDefinitionCollectionSeries wrapped, IIntermediateCustomAttributedDeclaration parent)
            : base(wrapped.baseList)
        {
        }

        #region ICustomAttributeDefinitionCollectionSeries Members

        public ICustomAttributeDefinitionCollection Add()
        {
            CustomAttributeDefinitionCollection target = new CustomAttributeDefinitionCollection(this);
            base.baseList.Add(target);
            return target;
        }

        public ICustomAttributeDefinitionCollection Add(params CustomAttributeDefinition.ParameterValueCollection[] attributeData)
        {
            CustomAttributeDefinitionCollection target = new CustomAttributeDefinitionCollection(this);
            foreach (var item in attributeData)
                target.Add(item);
            base.baseList.Add(target);
            return target;
        }

        public ICustomAttributeDefinitionCollection Add(params ICustomAttributeDefinition[] attributes)
        {
            CustomAttributeDefinitionCollection target = new CustomAttributeDefinitionCollection(this);
            foreach (var item in attributes)
                target.Add(item);
            base.baseList.Add(target);
            return target;
        }

        /// <summary>
        /// Returns the full count of <see cref="ICustomAttributeDefinition"/> elements
        /// contained within all <see cref="ICustomAttributeDefinitionCollection"/>
        /// in the current <see cref="CustomAttributeDefinitionCollectionSeries"/>.
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
        /// Returns the <see cref="IIntermediateCustomAttributedDeclaration"/>
        /// which contains the <see cref="CustomAttributeDefinitionCollectionSeries"/>.
        /// </summary>
        public IIntermediateCustomAttributedDeclaration Parent { get; private set; }

        public ICustomAttributeDefinition[] Flatten()
        {
            int count = 0;
            foreach (var set in this.baseList)
                count += set.Count;
            ICustomAttributeDefinition[] result = new ICustomAttributeDefinition[count];
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

        public ICustomAttributeDefinition this[IType attributeType]
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
                ICustomAttributeDefinitionCollection containing = null;
                foreach (var item in this)
                    if (item.Contains(attributeType))
                    {
                        containing = item;
                        break;
                    }
                ICustomAttributeDefinition closeMatch = null;
                if (containing == null)
                    return null;
                foreach (var item in containing)
                {
                    if (item.Type == attributeType)
                        return item;
                    else if (attributeType.IsAssignableFrom(item.Type))
                        closeMatch = item;
                }
                return closeMatch;
            }
        }

        public TAttribute GetAttributeInstance<TAttribute>() 
            where TAttribute :
                Attribute
        {
            IType k = typeof(TAttribute).GetTypeReference();
            if (!this.Contains(k))
                return default(TAttribute);
            return ((TAttribute)(this[k].WrappedAttribute));
        }

        public void Remove(ICustomAttributeDefinition customAttribute)
        {
            bool found = false;
            List<ICustomAttributeDefinitionCollection> containers = new List<ICustomAttributeDefinitionCollection>();
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

        public void RemoveSet(IEnumerable<ICustomAttributeDefinition> series)
        {
            List<ICustomAttributeDefinitionCollection> containers = new List<ICustomAttributeDefinitionCollection>();
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
        /// Disposes the <see cref="CustomAttributeDefinitionCollectionSeries"/>.
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
    }
}
