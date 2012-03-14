using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Provides a base implementation of an <see cref="IType"/>
    /// which is by-reference.
    /// </summary>
    public class ByRefType :
        IType
    {
        private IType elementType;
        private IMetadataCollection customAttributes;
        /// <summary>
        /// Creates a new <see cref="ByRefType"/> with the <paramref name="elementType"/>
        /// provided.
        /// </summary>
        /// <param name="elementType">The <see cref="IType"/> which is to be made
        /// into a by-reference type.</param>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="elementType"/>
        /// is already a reference type.</exception>
        internal ByRefType(IType elementType)
        {
            if (elementType.ElementClassification == TypeElementClassification.Reference)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.elementType, ExceptionMessageId.TypeInvalidElementType, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.by_reference_type), ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.by_reference_type));
            //throw new ArgumentException("elementType");
            this.elementType = elementType;
        }

        #region IType Members

        public IEnumerable<IDeclaration> Declarations
        {
            get { return TypeBase<IGeneralTypeUniqueIdentifier>.EmptyDeclarations; }
        }

        public TypeElementClassification ElementClassification
        {
            get { return TypeElementClassification.Reference; }
        }

        public int ArrayRank
        {
            get { throw new InvalidOperationException(); }
        }

        public IType ElementType
        {
            get { return this.elementType; }
        }

        public bool ContainsGenericParameters
        {
            get { return this.ContainsGenericParameters(); }
        }

        public bool IsGenericConstruct
        {
            get { return false; }
        }

        public bool IsGenericDefinition
        {
            get { return false; }
        }

        public bool IsGenericTypeParameter
        {
            get { return false; }
        }

        public ITypeCollection GenericParameters
        {
            get { throw new NotSupportedException(); }
        }

        public IType DeclaringType
        {
            get { return null; }
        }

        public TypeKind Type
        {
            get { return TypeKind.Class; }
        }

        public bool IsNullable
        {
            get { return false; }
        }

        public IArrayType MakeArray(int rank)
        {
            throw ThrowHelper.ObtainNotSupportedException(ExceptionMessageId.TypeInvalidElementType, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.by_reference_type), ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.array));
        }

        public IArrayType MakeArray()
        {
            throw ThrowHelper.ObtainNotSupportedException(ExceptionMessageId.TypeInvalidElementType, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.by_reference_type), ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.array));
        }

        public IArrayType MakeArray(params int[] lowerBounds)
        {
            throw ThrowHelper.ObtainNotSupportedException(ExceptionMessageId.TypeInvalidElementType, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.by_reference_type), ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.array));
        }

        public IType MakePointer()
        {
            throw ThrowHelper.ObtainNotSupportedException(ExceptionMessageId.TypeInvalidElementType, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.by_reference_type), ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.pointer));
        }

        public IType MakeByReference()
        {
            throw ThrowHelper.ObtainNotSupportedException(ExceptionMessageId.TypeInvalidElementType, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.by_reference_type), ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.by_reference_type));
        }

        public IType MakeNullable()
        {
            throw ThrowHelper.ObtainNotSupportedException(ExceptionMessageId.TypeInvalidElementType, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.by_reference_type), ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.nullable));
        }

        public bool IsSubclassOf(IType other)
        {
            return false;
        }

        public bool IsAssignableFrom(IType target)
        {
            if (target == this)
                return true;
            else
                return false;
        }

        public string FullName
        {
            get { return string.Format("{0}&", this.elementType.FullName); }
        }

        public INamespaceDeclaration Namespace
        {
            get { return null; }
        }

        public string NamespaceName
        {
            get
            {
                return null;
            }
        }
        public IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get
            {
                yield break;
            }
        }
        public IType BaseType
        {
            get { return null; }
        }

        public ILockedTypeCollection ImplementedInterfaces
        {
            get { return LockedTypeCollection.Empty; }
        }

        public IAssembly Assembly
        {
            get { return elementType.Assembly; }
        }

        public IFullMemberDictionary Members
        {
            get { return LockedFullMembersBase.Empty; }
        }

        #endregion

        #region IEquatable<IType> Members

        public bool Equals(IType other)
        {
            if (other.ElementClassification == TypeElementClassification.Reference && other.ElementType.Equals(this.ElementType))
                return true;
            return false;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                this.elementType = null;
                if (this.customAttributes != null)
                {
                    this.customAttributes.Dispose();
                    this.customAttributes = null;
                }
            }
            finally { 
                this.OnDisposed();
                this.Disposed = null;
                GC.SuppressFinalize(this);
            }
        }

        #endregion

        #region IScopedDeclaration Members

        public AccessLevelModifiers AccessLevel
        {
            get { return AccessLevelModifiers.Private; }
        }

        #endregion

        private void OnDisposed()
        {
            var disposedCopy = this.Disposed;
            if (disposedCopy != null)
                disposedCopy(this, EventArgs.Empty);
        }

        #region IDeclaration Members
        public event EventHandler Disposed;

        public string Name
        {
            get { return string.Format("{0}&", this.ElementType.Name); }
        }

        public IGeneralTypeUniqueIdentifier UniqueIdentifier
        {
            get { return this.elementType.UniqueIdentifier; }
        }

        IGeneralDeclarationUniqueIdentifier IDeclaration.UniqueIdentifier
        {
            get { return this.UniqueIdentifier; }
        }
        #endregion

        #region IMetadataEntity Members

        public IMetadataCollection CustomAttributes
        {
            get
            {
                if (this.customAttributes == null)
                    this.customAttributes = new LockedEmptyCustomAttributeCollection(this);
                return this.customAttributes;
            }
        }

        public bool IsDefined(IType metadatumType)
        {
            return false;
        }

        public bool IsDefined(IType metadatumType, bool inherited)
        {
            return false;
        }

        #endregion

        public ITypeIdentityManager Manager { get { return this.ElementType.Manager; } }

        public override string ToString()
        {
            return this.BuildTypeName(true);
        }

    }
}
