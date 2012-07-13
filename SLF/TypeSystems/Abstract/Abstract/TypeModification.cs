using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines a structure which denotes a type modifier as either required or optional.
    /// </summary>
    public struct TypeModification :
        IEquatable<TypeModification>
    {
        private IType modifierType;
        private bool isRequiredType;
        private Func<IType> modifierTypeProcurer;

        /// <summary>
        /// Creates a new <see cref="TypeModification"/> with the 
        /// the <paramref name="modifiedType"/> and <paramref name="isRequiredType"/>
        /// provided.
        /// </summary>
        /// <param name="modifiedType">The <see cref="IType"/> which is
        /// modified by the <see cref="TypeModification"/>.</param>
        /// <param name="isRequiredType"><paramref name="modifiedType"/>
        /// is required to be processed by tools and compilers, or whether it can be
        /// ignored.</param>
        public TypeModification(IType modifiedType, bool isRequiredType)
        {
            if (modifiedType == null)
                throw new ArgumentNullException("modifiedType");
            this.modifierType = modifiedType;
            this.isRequiredType = isRequiredType;
            this.modifierTypeProcurer = null;
        }

        internal TypeModification(Func<IType> modifierTypeProcurer, bool isRequiredType)
        {
            this.modifierType = null;
            this.isRequiredType = isRequiredType;
            this.modifierTypeProcurer = modifierTypeProcurer;
        }

        /// <summary>
        /// Returns the <see cref="IType"/> which represents the modifier.
        /// </summary>
        public IType ModifierType
        {
            get
            {
                if (this.modifierType         == null &&
                    this.modifierTypeProcurer != null)
                {
                    this.modifierType = modifierTypeProcurer();
                    this.modifierTypeProcurer = null;
                }
                return this.modifierType;
            }
        }

        /// <summary>
        /// Returns whether <see cref="ModifierType"/> represents a 
        /// required type modifier, or an optional modifier.
        /// </summary>
        /// <remarks>true, if <see cref="ModifierType"/> is required;
        /// false, if <see cref="ModifierType"/> is optional.</remarks>
        public bool IsRequiredType { get { return this.isRequiredType; } }


        #region IEquatable<TypeModification> Members

        public bool Equals(TypeModification other)
        {
            return  other.isRequiredType == this.isRequiredType &&
                   (other.modifierType   ==               null  && 
                     this.modifierType   ==               null) ||
                    other.modifierType   !=               null  && 
                    other.modifierType.Equals(this.modifierType);
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is TypeModification)
                return this.Equals((TypeModification) obj);
            return false;
        }

        public override int GetHashCode()
        {
            if (this.modifierType == null)
                return -1;
            return this.modifierType.GetHashCode() ^ this.isRequiredType.GetHashCode();
        }
    }
}
