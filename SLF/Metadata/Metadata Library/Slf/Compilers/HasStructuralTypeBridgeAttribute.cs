using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Represents metadata which links a type to a structural typing bridge
    /// which indicates how to marshal the type for a particular set of 
    /// functionality.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Delegate | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Struct, AllowMultiple = true, Inherited = true)]
    public class HasStructuralTypeBridgeAttribute :
        Attribute
    {
        /// <summary>
        /// Data member for <see cref="BridgeTarget"/>.
        /// </summary>
        private Type bridgeTarget;
        /// <summary>
        /// Data member for <see cref="BridgeGuid"/>.
        /// </summary>
        private Guid bridgeGuid;
        /// <summary>
        /// Creates a new <see cref="HasStructuralTypeBridgeAttribute"/> with the
        /// <paramref name="bridgeGuid"/> and <paramref name="bridgeTarget"/> provided.
        /// </summary>
        /// <param name="bridgeGuid">The <see cref="String"/> value of the <see cref="Guid"/> associated
        /// to the structural bridge.</param>
        /// <param name="bridgeTarget">The <see cref="Type"/> which denotes the class that
        /// implements the appropriate bridge.</param>
        public HasStructuralTypeBridgeAttribute(string bridgeGuid, Type bridgeTarget)
        {
            this.bridgeTarget = bridgeTarget;
            Guid.TryParse(bridgeGuid, out this.bridgeGuid);
        }

        /// <summary>
        /// Returns whether the <paramref name="Bridge"/> can be created.
        /// </summary>
        public bool BridgeCreatable { get { return bridgeTarget.GetConstructor(new Type[0]) != null; } }

        /// <summary>
        /// Returns the <see cref="Type"/> of the the implementation for the 
        /// structural typing bridge.
        /// </summary>
        public Type Bridge { get { return this.bridgeTarget; } }

        /// <summary>
        /// Returns the <see cref="Guid"/> associated to the bridge
        /// </summary>
        public Guid BridgeGuid { get { return this.bridgeGuid; } }

        /// <summary>
        /// Returns whether the <see cref="BridgeGuid"/> is valid.
        /// </summary>
        public bool BridgeGuidValid { get { return !this.bridgeGuid.Equals(Guid.Empty); } }
    }
}
