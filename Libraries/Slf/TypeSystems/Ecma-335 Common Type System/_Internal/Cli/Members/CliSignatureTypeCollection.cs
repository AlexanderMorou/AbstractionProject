using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
using System.Collections;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Abstract.Members;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class CliSignatureTypeCollection :
        IControlledTypeCollection
    {
        private ICliMetadataMethodSignature signature;
        private IType[] signatureTypes;
        private _ICliManager manager;
        public CliSignatureTypeCollection(_ICliManager manager, ICliMetadataMethodSignature signature, IType activeType, IMethodSignatureMember activeMethod)
        {
            if (manager == null)
                throw new ArgumentNullException("manager");
            if (signature == null)
                throw new ArgumentNullException("signature");
            this.manager = manager;
            this.signatureTypes = new IType[signature.Parameters.Count];
            this.ActiveType = activeType;
            this.ActiveMethod = activeMethod;
            this.signature = signature;
        }

        #region IControlledTypeCollection Members

        public int IndexOf(IType type)
        {
            int firstNull = -1;
            for (int typeIndex = 0; typeIndex < this.signatureTypes.Length; typeIndex++)
            {
                if (this.signatureTypes[typeIndex] != null)
                {
                    if (this.signatureTypes[typeIndex].Equals(type))
                        return typeIndex;
                }
                else if (firstNull == -1)
                    firstNull = typeIndex;
            }
            /* *
             * All elements were available, none matched, so exit.
             * */
            if (firstNull == -1)
                return -1;
            for (int typeIndex = firstNull; typeIndex < this.Count; typeIndex++)
            {
                if (!CheckItemAt(typeIndex) && this.signatureTypes[typeIndex].Equals(type))
                    return typeIndex;
            }
            return -1;
        }

        #endregion

        #region IControlledCollection<IType> Members

        public int Count
        {
            get { return this.signatureTypes.Length; }
        }

        public bool Contains(IType item)
        {
            int firstNull = -1;
            for (int typeIndex = 0; typeIndex < this.signatureTypes.Length; typeIndex++)
            {
                if (this.signatureTypes[typeIndex] != null)
                {
                    if (this.signatureTypes[typeIndex].Equals(item))
                        return true;
                }
                else if (firstNull == -1)
                    firstNull = typeIndex;
            }
            /* *
             * All elements were available, none matched, so exit.
             * */
            if (firstNull == -1)
                return false;
            for (int typeIndex = firstNull; typeIndex < this.Count; typeIndex++)
            {
                if (!CheckItemAt(typeIndex) && this.signatureTypes[typeIndex].Equals(item))
                    return true;
            }
            return false;
        }

        private bool CheckItemAt(int i)
        {
            if (this.signatureTypes[i] == null)
            {
                this.InitializeItemAt(i);
                return false;
            }
            return true;
        }


        private void InitializeItemAt(int typeIndex)
        {
            var param = this.signature.Parameters[typeIndex];
            IAssembly owningAssembly;
            if (this.ActiveType == null)
                owningAssembly = this.ActiveType.Assembly;
            else
                owningAssembly = this.ActiveMethod.Parent as IAssembly;
            this.signatureTypes[typeIndex] = new CliModifiedType(manager.ObtainTypeReference(param.ParameterType, this.ActiveType, this.ActiveMethod, owningAssembly), param.CustomModifiers);
        }

        public void CopyTo(IType[] array, int arrayIndex = 0)
        {
            ThrowHelper.CopyToCheck(array, arrayIndex, this.Count);
            for (int typeIndex = 0; typeIndex < this.Count; typeIndex++)
            {
                this.CheckItemAt(typeIndex);
                array[typeIndex + arrayIndex] = this.signatureTypes[typeIndex];
            }
        }

        public IType this[int index]
        {
            get
            {
                this.CheckItemAt(index);
                return this.signatureTypes[index];
            }
        }

        public IType[] ToArray()
        {
            IType[] result = new IType[this.Count];
            this.CopyTo(result);
            return result;
        }

        #endregion

        #region IEnumerable<IType> Members

        public IEnumerator<IType> GetEnumerator()
        {
            for (int typeIndex = 0; typeIndex < this.Count; typeIndex++)
            {
                this.CheckItemAt(typeIndex);
                yield return this.signatureTypes[typeIndex];
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IEquatable<IControlledTypeCollection> Members

        public bool Equals(IControlledTypeCollection other)
        {
            if (this.Count != other.Count)
                return false;
            for (int typeIndex = 0; typeIndex < this.Count; typeIndex++)
            {
                this.CheckItemAt(typeIndex);
                if (!this.signatureTypes[typeIndex].Equals(other[typeIndex]))
                    return false;
            }
            return true;
        }

        #endregion

        public IType ActiveType { get; set; }

        public IMethodSignatureMember ActiveMethod { get; set; }
    }
}
