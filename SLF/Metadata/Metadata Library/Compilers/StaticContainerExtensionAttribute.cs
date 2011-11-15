using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Marks a method as an extension method on a static container.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class StaticContainerExtensionAttribute :
        Attribute
    {
        private Type extensionTarget;
        /// <summary>
        /// Creates a new <see cref="StaticContainerExtensionAttribute"/>
        /// </summary>
        /// <param name="extensionTarget">The static <see cref="Type"/>
        /// which is targeted by the extension method.</param>
        public StaticContainerExtensionAttribute(Type extensionTarget)
        {
            this.extensionTarget = extensionTarget;
        }

        /// <summary>
        /// Returns the static <see cref="Type"/> which is
        /// the target of the extension method.
        /// </summary>
        public Type ExtensionTarget
        {
            get
            {
                return this.extensionTarget;
            }
        }
    }
}
