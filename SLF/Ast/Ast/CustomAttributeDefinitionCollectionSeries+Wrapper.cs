using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    partial class MetadataDefinitionCollection
    {
        protected internal class Wrapper :
            IMetadataCollection
        {

            internal Wrapper(IIntermediateMetadataEntity owner)
            {
                this.Parent = owner;
            }

            public IIntermediateMetadataEntity Parent { get; private set; }

            #region IMetadataCollection Members

            public bool Contains(IType attributeType)
            {
                return Parent.CustomAttributes.Contains(attributeType);
            }

            IMetadataEntity IMetadataCollection.Parent
            {
                get { return this.Parent; }
            }

            public IMetadatum this[IType attributeType]
            {
                get { return Parent.CustomAttributes[attributeType]; }
            }

            #endregion

            #region IControlledCollection<IMetadatum> Members

            public int Count
            {
                get { return Parent.CustomAttributes.FullCount; }
            }

            public bool Contains(IMetadatum item)
            {
                foreach (var _item in Parent.CustomAttributes.Flatten())
                    if (_item == item)
                        return true;
                return false;
            }

            public int IndexOf(IMetadatum element)
            {
                int index = 0;
                foreach (var _item in Parent.CustomAttributes.Flatten())
                    if (_item == element)
                        return index;
                    else
                        index++;
                return -1;
            }

            public void CopyTo(IMetadatum[] array, int arrayIndex = 0)
            {
                this.ToArray().CopyTo(array, arrayIndex);
            }

            public IMetadatum this[int index]
            {
                get
                {
                    int i = 0;

                    foreach (var definitions in Parent.CustomAttributes)
                        foreach (var definition in definitions)
                            if (i == index)
                                return definition;
                            else
                                i++;
                    throw new ArgumentOutOfRangeException("index");
                }
            }

            public IMetadatum[] ToArray()
            {
                IMetadatum[] result = new IMetadatum[this.Count];
                int i = 0;
                foreach (var definitions in this.Parent.CustomAttributes)
                    foreach (var definition in definitions)
                        result[i++] = definition;
                return result;
            }

            #endregion

            #region IEnumerable<IMetadatum> Members

            public IEnumerator<IMetadatum> GetEnumerator()
            {
                foreach (var definitions in this.Parent.CustomAttributes)
                    foreach (var definition in definitions)
                        yield return definition;
                yield break;
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region IDisposable Members
            /// <summary>
            /// Disposes the <see cref="Wrapper"/> instance.
            /// </summary>
            public void Dispose()
            {
                try
                {
                    this.Parent = null;
                }
                finally
                {
                    GC.SuppressFinalize(this);
                }
            }

            #endregion

        }
    }
}
