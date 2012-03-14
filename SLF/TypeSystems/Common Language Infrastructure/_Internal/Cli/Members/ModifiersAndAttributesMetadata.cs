using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract class ModifiersAndAttributesMetadata :
        IModifiersAndAttributesMetadata
    {
        private IType[] requiredModifiers;
        private IType[] optionalModifiers;
        private Type[] _requiredModifiers;
        private Type[] _optionalModifiers;
        private object _attributesLock = new object();
        private object _requiredLock = new object();
        private object _optionalLock = new object();
        private Func<bool, object[]> getCustomAttributes;
        private CompiledCustomAttributeCollection attributes;
        private ICliManager manager;

        public ModifiersAndAttributesMetadata(ICliManager manager)
        {
            this.manager = manager;
        }

        protected abstract Type[] GetRequiredModifiers();

        protected abstract Type[] GetOptionalModifiers();

        protected abstract object[] GetCustomAttributes(bool inherit);

        #region IModifiersAndAttributesMetadata Members

        public IEnumerable<IType> RequiredModifiers
        {
            get
            {
                Type[] requiredModifiers;
                lock (_requiredLock)
                {
                    if (this._requiredModifiers == null)
                    {
                        this._requiredModifiers = this.GetRequiredModifiers();
                        this.requiredModifiers = new IType[this._requiredModifiers.Length];
                    }
                    requiredModifiers = this._requiredModifiers;
                }

                for (int i = 0; i < requiredModifiers.Length; i++)
                {
                    IType current;
                    lock (this._requiredLock)
                    {
                        if (this.requiredModifiers[i] == null)
                            current = this.requiredModifiers[i] = this.manager.ObtainTypeReference(requiredModifiers[i]);
                        else
                            current = this.requiredModifiers[i];
                    }
                    yield return current;
                }
            }
        }

        public IEnumerable<IType> OptionalModifiers
        {
            get
            {
                Type[] optionalModifiers;
                lock (_optionalLock)
                {
                    if (this._optionalModifiers == null)
                    {
                        this._optionalModifiers = this.GetOptionalModifiers();
                        this.optionalModifiers = new IType[this._optionalModifiers.Length];
                    }
                    optionalModifiers = this._optionalModifiers;
                }

                for (int i = 0; i < optionalModifiers.Length; i++)
                {
                    IType current;
                    lock (this._optionalLock)
                    {
                        if (this.optionalModifiers[i] == null)
                            current = this.optionalModifiers[i] = this.manager.ObtainTypeReference(optionalModifiers[i]);
                        else
                            current = this.optionalModifiers[i];
                    }
                    yield return current;
                }
            }
        }

        #endregion

        #region IMetadataEntity Members

        public IMetadataCollection CustomAttributes
        {
            get
            {
                lock (this._attributesLock)
                {
                    if (this.attributes == null)
                        this.attributes = new CompiledCustomAttributeCollection(this.GetCustomAttributes, this.manager);
                    return this.attributes;
                }
            }
        }

        public bool IsDefined(IType metadatumType)
        {
            return this.StandardIsDefined(metadatumType);
        }

        #endregion
    }
}
