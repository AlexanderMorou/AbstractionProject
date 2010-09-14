using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class CustomAttributeDefinitionCollectionSeries
    {
        protected internal class Wrapper :
            ICustomAttributeCollection
        {

            internal Wrapper(IIntermediateCustomAttributedDeclaration owner)
            {
                this.Parent = owner;
            }

            public IIntermediateCustomAttributedDeclaration Parent { get; private set; }

            #region ICustomAttributeCollection Members

            public bool Contains(IType attributeType)
            {
                return Parent.CustomAttributes.Contains(attributeType);
            }

            ICustomAttributedDeclaration ICustomAttributeCollection.Parent
            {
                get { return this.Parent; }
            }

            public ICustomAttributeInstance this[IType attributeType]
            {
                get { return Parent.CustomAttributes[attributeType]; }
            }

            #endregion

            #region IControlledStateCollection<ICustomAttributeInstance> Members

            public int Count
            {
                get { return Parent.CustomAttributes.FullCount; }
            }

            public bool Contains(ICustomAttributeInstance item)
            {
                foreach (var _item in Parent.CustomAttributes.Flatten())
                    if (_item == item)
                        return true;
                return false;
            }

            public int IndexOf(ICustomAttributeInstance element)
            {
                int index = 0;
                foreach (var _item in Parent.CustomAttributes.Flatten())
                    if (_item == element)
                        return index;
                    else
                        index++;
                return -1;
            }

            public void CopyTo(ICustomAttributeInstance[] array, int arrayIndex)
            {
                this.ToArray().CopyTo(array, arrayIndex);
            }

            public ICustomAttributeInstance this[int index]
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

            public ICustomAttributeInstance[] ToArray()
            {
                ICustomAttributeInstance[] result = new ICustomAttributeInstance[this.Count];
                int i = 0;
                foreach (var definitions in this.Parent.CustomAttributes)
                    foreach (var definition in definitions)
                        result[i++] = definition;
                return result;
            }

            #endregion

            #region IEnumerable<ICustomAttributeInstance> Members

            public IEnumerator<ICustomAttributeInstance> GetEnumerator()
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
