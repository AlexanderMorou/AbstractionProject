using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a base implementation of a type that is ambiguous between 
    /// other types of the same name.
    /// </summary>
    public class AmbiguousType :
        IAmbiguousType
    {
        private IList<IType> _source;
        private IReadOnlyCollection<IType> source;

        /// <summary>
        /// Creates a new <see cref="AmbiguousType"/> with the <paramref name="source"/> 
        /// types provided.
        /// </summary>
        /// <param name="source">The <see cref="IType"/> array from which the current
        /// <see cref="AmbiguousType"/> is derived.</param>
        internal AmbiguousType(IType[] source)
        {
            this._source = new List<IType>(source);
        }

        #region IType Members

        /// <summary>
        /// Returns the special classification given to <see cref="ElementType"/>.
        /// </summary>
        public TypeElementClassification ElementClassification
        {
            get { return TypeElementClassification.None; }
        }

        /// <summary>
        /// Returns the element type of special classification types.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">thrown when
        /// <see cref="ElementClassification"/> is <see cref="TypeElementClassification.None"/>.</exception>
        public IType ElementType
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Ambiguous Types are not generic type parameters.
        /// </summary>
        public bool IsGenericTypeParameter
        {
            get { return false; }
        }

        /// <summary>
        /// Ambiguous types are not generic types.
        /// </summary>
        public bool IsGenericType
        {
            get { return false; }
        }

        /// <summary>
        /// Ambiguous types have no declaring type.
        /// </summary>
        public IType DeclaringType
        {
            get { return null; }
        }

        /// <summary>
        /// Ambiguous types are an ambiguity kind of type.
        /// </summary>
        public TypeKind Type
        {
            get { return TypeKind.Ambiguity; }
        }

        /// <summary>
        /// Ambiguous types are not structures.
        /// </summary>
        public bool IsNullable
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Ambiguous types cannot be made into an array.
        /// </summary>
        /// <param name="rank"></param>
        public IArrayType MakeArray(int rank)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Ambiguous types cannot be made into an array.
        /// </summary>
        public IArrayType MakeArray()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Ambiguous types cannot be made into an array.
        /// </summary>
        /// <param name="lowerBounds"></param>
        public IArrayType MakeArray(params int[] lowerBounds)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Ambiguous types cannot be made into a pointer.
        /// </summary>
        /// <returns></returns>
        public IType MakePointer()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Ambiguous types cannot be made into a by-ref type.
        /// </summary>
        /// <returns></returns>
        public IType MakeByReference()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Ambiguous types cannot be made into a nullable type.
        /// </summary>
        /// <returns></returns>
        public IType MakeNullable()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Ambiguous types are ambiguous and therefore are not a subclass of anything specifically.
        /// </summary>
        /// <param name="other"></param>
        public bool IsSubclassOf(IType other)
        {
            return false;
        }

        /// <summary>
        /// Ambiguous types are not assignable by anything.
        /// </summary>
        /// <param name="target"></param>
        public bool IsAssignableFrom(IType target)
        {
            return false;
        }

        /// <summary>
        /// Returns the full name of the <see cref="IType"/>.
        /// </summary>
        public string FullName
        {
            get {
                return this.Source.FirstOrDefault().FullName;
            }
        }

        /// <summary>
        /// Returns the namespace in which the <see cref="IType"/> is declared.
        /// </summary>
        public INamespaceDeclaration Namespace
        {
            get { return this.source.FirstOrDefault().Namespace; }
        }

        public string NamespaceName
        {
            get
            {
                return this.Namespace.FullName;
            }
        }


        /// <summary>
        /// Returns the base type of the current <see cref="IType"/>.
        /// </summary>
        public IType BaseType
        {
            get { return null; }
        }

        /// <summary>
        /// Returns a collection of <see cref="IType"/> instances that are implemented by the current
        /// <see cref="IType"/>.
        /// </summary>
        public ILockedTypeCollection ImplementedInterfaces
        {
            get { return LockedTypeCollection.Empty; }
        }

        /// <summary>
        /// Returns the <see cref="IAssembly"/> in which the <see cref="IType"/> is declared
        /// </summary>
        public IAssembly Assembly
        {
            get { return null; }
        }

        /// <summary>
        /// Returns the <see cref="IFullMemberDictionary"/> of 
        /// a series of <see cref="IGroupedMemberDictionary"/>.
        /// </summary>
        /// <exception cref="System.NotSupportedException">The <see cref="IType"/>
        /// does not support members.</exception>
        public IFullMemberDictionary Members
        {
            get { throw new NotSupportedException(); }
        }

        #endregion

        #region IEquatable<IType> Members

        public bool Equals(IType other)
        {
            if (other is IAmbiguousType)
            {
                IAmbiguousType aOther = (IAmbiguousType)other;
                if (aOther.Source.Count == this.Source.Count)
                    foreach (var item in this.Source)
                        if (!aOther.Source.Contains(item))
                            return false;
                return true;
            }
            return false;
        }

        #endregion

        #region ICustomAttributedDeclaration Members

        public ICustomAttributeCollection CustomAttributes
        {
            get { return null; }
        }

        public bool IsDefined(IType attributeType)
        {
            return false;
        }

        #endregion

        #region IDeclaration Members

        public string Name
        {
            get { return this.Source.FirstOrDefault().Name; }
        }

        public string UniqueIdentifier
        {
            get { return this.FullName; }
        }

        public event EventHandler Disposed;

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this._source.Clear();
            this.source = null;
            this._source = null;
            if (this.Disposed != null)
            {
                this.Disposed(this, EventArgs.Empty);
                this.Disposed = null;
            }
        }

        #endregion

        #region IScopedDeclaration Members

        public AccessLevelModifiers AccessLevel
        {
            get { return AccessLevelModifiers.Public; }
        }

        #endregion

        #region IAmbiguousType Members

        public IReadOnlyCollection<IType> Source
        {
            get {
                if (this.source == null)
                    this.source = new ReadOnlyCollection<IType>(this._source);
                return this.source; }
        }

        #endregion

        #region IType Members


        public IEnumerable<IDeclaration> Declarations
        {
            get { yield break; }
        }

        #endregion
    }
}
