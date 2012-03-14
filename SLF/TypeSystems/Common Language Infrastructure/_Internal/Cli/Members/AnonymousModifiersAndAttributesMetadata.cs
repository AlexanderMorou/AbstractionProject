using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class AnonymousModifiersAndAttributesMetadata :
        ModifiersAndAttributesMetadata
    {
        private Func<Type[]> requiredModifiersPtr;
        private Func<Type[]> optionalModifiersPtr;
        private Func<bool, object[]> customAttributesPtr;

        public AnonymousModifiersAndAttributesMetadata(Func<Type[]> requiredModifiersPtr, Func<Type[]> optionalModifiersPtr, Func<bool, object[]> customAttributesPtr, ICliManager manager)
            : base(manager)
        {
            this.requiredModifiersPtr = requiredModifiersPtr;
            this.optionalModifiersPtr = requiredModifiersPtr;
            this.customAttributesPtr = customAttributesPtr;
        }

        protected override Type[] GetRequiredModifiers()
        {
            return this.requiredModifiersPtr();
        }

        protected override Type[] GetOptionalModifiers()
        {
            return this.optionalModifiersPtr();
        }

        protected override object[] GetCustomAttributes(bool inherit)
        {
            return this.GetCustomAttributes(inherit);
        }
    }
}
