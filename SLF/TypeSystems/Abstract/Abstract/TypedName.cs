using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// The source of the type reference on the typed name.
    /// </summary>
    public enum TypedNameSource
    {
        /// <summary>
        /// The type reference of the <see cref="TypedName"/> is 
        /// a direct reference.
        /// </summary>
        TypeReference,
        /// <summary>
        /// The type reference of the <see cref="TypedName"/>
        /// is a symbol (<see cref="String"/>).
        /// </summary>
        SymbolReference,
        /// <summary>
        /// The <see cref="TypedName"/> is invalid.
        /// </summary>
        InvalidReference,
    }
    /// <summary>
    /// Provides a name (<see cref="String"/>) paired with a Reference 
    /// (<see cref="IType"/>) or Symbol (<see cref="String"/>).
    /// </summary>
    public struct TypedName :
        IEquatable<TypedName>
    {
        /// <summary>
        /// Data member for <see cref="Name"/>.
        /// </summary>
        [ComVisible(false)]
        string name;
        /// <summary>
        /// Data member for <see cref="TypeReference"/>.
        /// </summary>
        [ComVisible(false)]
        IType reference;
        /// <summary>
        /// Data member for <see cref="SymbolReference"/>.
        /// </summary>
        [ComVisible(false)]
        string symbolReference;
        /// <summary>
        /// Data member for <see cref="Direction"/>.
        /// </summary>
        [ComVisible(false)]
        ParameterCoercionDirection direction;

        /// <summary>
        /// Creates a new <see cref="TypedName"/> with
        /// the <paramref name="name"/> and 
        /// type <paramref name="reference"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="System.String"/>
        /// which relates to the name of the <see cref="TypedName"/>.</param>
        /// <param name="reference">The <see cref="IType"/>
        /// which relates to the type of the <see cref="TypedName"/>.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>
        /// or <paramref name="reference"/> is null.</exception>
        public TypedName(string name, IType reference)
            : this(name, reference, ParameterCoercionDirection.In)
        {
        }

        public TypedName(string format, IType reference, params object[] args)
            : this(string.Format(format, args), reference, ParameterCoercionDirection.In)
        {

        }

        /// <summary>
        /// Creates a new <see cref="TypedName"/> with the
        /// <paramref name="name"/> and symbol type 
        /// <paramref name="reference"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="System.String"/>
        /// which relates to the name of the <see cref="TypedName"/>.</param>
        /// <param name="reference">The symbol relative to the type
        /// the <see cref="TypedName"/> refers to.</param>
        /// <remarks>The usage of the typed name may require the 
        /// symbol to be resolved immediately, such as creating a 
        /// generic method that uses a type-parameter that's referenced
        /// after the type-parameter is created.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>
        /// or <paramref name="reference"/> is null.</exception>
        public TypedName(string name, string reference)
            : this(name, reference, ParameterCoercionDirection.In)
        {
        }

        /// <summary>
        /// Creates a new <see cref="TypedName"/> with the
        /// <paramref name="name"/>, <paramref name="runtimeType"/>, <paramref name="identityManager"/>
        /// and optional <paramref name="relativeSource"/>.
        /// </summary>
        /// <param name="name">The <see cref="System.String"/>
        /// which relates to the name of the <see cref="TypedName"/>.</param>
        /// <param name="runtimeType">The <see cref="RuntimeCoreType"/> relative to the 
        /// identity of the type to retrieve from the <paramref name="identityManager"/>.</param>
        /// <param name="identityManager">The <see cref="IIdentityManager"/> responsible for
        /// disambiguating the <paramref name="runtimeType"/>.</param>
        /// <param name="relativeSource">The <see cref="IAssembly"/> relative to the
        /// <paramref name="runtimeType"/> used for context, which either contains the type
        /// or references the assembly containing the type.</param>
        public TypedName(string name, RuntimeCoreType runtimeType, IIdentityManager identityManager, IAssembly relativeSource = null)
            : this(name, identityManager.ObtainTypeReference(runtimeType, relativeSource))
        {
        }

        /// <summary>
        /// Creates a new <see cref="TypedName"/> with the
        /// <paramref name="name"/>, <paramref name="runtimeType"/>, <paramref name="identityManager"/>,
        /// <paramref name="direction"/>, and optional <paramref name="relativeSource"/>.
        /// </summary>
        /// <param name="name">The <see cref="System.String"/>
        /// which relates to the name of the <see cref="TypedName"/>.</param>
        /// <param name="runtimeType">The <see cref="RuntimeCoreType"/> relative to the 
        /// identity of the type to retrieve from the <paramref name="identityManager"/>.</param>
        /// <param name="identityManager">The <see cref="IIdentityManager"/> responsible for
        /// disambiguating the <paramref name="runtimeType"/>.</param>
        /// <param name="direction">The <see cref="ParameterCoercionDirection"/>
        /// which indicates how the type should be directed.</param>
        /// <param name="relativeSource">The <see cref="IAssembly"/> relative to the
        /// <paramref name="runtimeType"/> used for context, which either contains the type
        /// or references the assembly containing the type.</param>
        public TypedName(string name, RuntimeCoreType runtimeType, IIdentityManager identityManager, ParameterCoercionDirection direction, IAssembly relativeSource = null)
            : this(name, identityManager.ObtainTypeReference(runtimeType, relativeSource), direction)
        {
        }

        /// <summary>
        /// Creates a new <see cref="TypedName"/> with the
        /// <paramref name="name"/>, symbol type 
        /// <paramref name="reference"/>, and 
        /// <paramref name="direction"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="System.String"/>
        /// which relates to the name of the <see cref="TypedName"/>.</param>
        /// <param name="reference">The symbol relative to the type
        /// the <see cref="TypedName"/> refers to.</param>
        /// <param name="direction">The <see cref="ParameterCoercionDirection"/>
        /// which indicates how the type should be directed.</param>
        /// <remarks><para>The usage of the typed name may require the 
        /// symbol to be resolved immediately. One such example is creating a 
        /// generic method that uses a type-parameter that's referenced
        /// after the type-parameter is created.</para>
        /// <para>The <paramref name="direction"/> may be disposed
        /// of in cases where it does not apply.</para></remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> or 
        /// <paramref name="reference"/> is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when 
        /// <paramref name="direction"/> is invalid.</exception>
        public TypedName(string name, string reference, ParameterCoercionDirection direction)
        {
            switch (direction)
            {
                case ParameterCoercionDirection.In:
                case ParameterCoercionDirection.Out:
                case ParameterCoercionDirection.Reference:
                    this.direction = direction;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction");
            }
            this.name = name;
            this.symbolReference = reference;
            this.reference = null;
        }

        /// <summary>
        /// Creates a new <see cref="TypedName"/> with the
        /// <paramref name="name"/>, symbol type 
        /// <paramref name="reference"/>, and 
        /// <paramref name="direction"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="System.String"/>
        /// which relates to the name of the <see cref="TypedName"/>.</param>
        /// <param name="reference">The <see cref="IType"/>
        /// which relates to the type of the <see cref="TypedName"/>.</param>
        /// <param name="direction">The <see cref="ParameterCoercionDirection"/>
        /// which indicates how the type should be directed.</param>
        /// <remarks><para>The <paramref name="direction"/> may be disposed
        /// of in cases where it does not apply.</para></remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> or <paramref name="reference"/> is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="direction"/> is invalid.</exception>
        public TypedName(string name, IType reference, ParameterCoercionDirection direction)
        {
            switch (direction)
            {
                case ParameterCoercionDirection.In:
                case ParameterCoercionDirection.Out:
                case ParameterCoercionDirection.Reference:
                    this.direction = direction;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction");
                    //break;
            }
            this.name = name;
            this.symbolReference = null;
            this.reference = reference;
        }

        /// <summary>
        /// Returns the name of the <see cref="TypedName"/>.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            internal set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Returns the <see cref="IType"/> the <see cref="TypedName"/> refers to.
        /// </summary>
        public IType TypeReference
        {
            get
            {
                return this.reference;
            }
            internal set
            {
                this.reference = value;
            }
        }

        public string SymbolReference
        {
            get
            {
                return this.symbolReference;
            }
        }

        /// <summary>
        /// Returns the name and type name of the instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> which
        /// relates to the string-form of the 
        /// <see cref="TypedName"/>.</returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", reference.Name, this.Name);
        }

        public TypedNameSource Source
        {
            get
            {
                if (this.reference == null)
                    if (this.symbolReference == null)
                        return TypedNameSource.InvalidReference;
                    else
                        return TypedNameSource.SymbolReference;
                return TypedNameSource.TypeReference;
            }
        }

        /// <summary>
        /// Returns the <see cref="ParameterCoercionDirection"/>
        /// indicating how the type should directed.
        /// </summary>
        public ParameterCoercionDirection Direction
        {
            get
            {
                return this.direction;
            }
        }

        /// <summary>
        /// Converts a <see cref="KeyValuePair{TKey, TValue}"/> to a
        /// <see cref="TypedName"/> where TKey is <see cref="String"/> and
        /// TValue is <see cref="IType"/>.
        /// </summary>
        /// <param name="keyedValue">The <see cref="KeyValuePair{TKey, TValue}"/> to convert.</param>
        /// <returns>A new <see cref="TypedName"/> if the <see cref="IType"/> of the 
        /// <see cref="KeyValuePair{TKey, TValue}.Value"/> is a good reference.</returns>
        public static implicit operator TypedName(KeyValuePair<string, IType> keyedValue)
        {
            return new TypedName(keyedValue.Key, keyedValue.Value);
        }

        /// <summary>
        /// Converts a <see cref="TypedName"/> to a <see cref="KeyValuePair{TKey, TValue}"/>
        /// where TKey is <see cref="String"/> and TValue is <see cref="IType"/>.
        /// </summary>
        /// <param name="typedName">The <see cref="TypedName"/> to convert.</param>
        /// <returns>A <see cref="KeyValuePair{TKey, TValue}"/> relative to the
        /// <paramref name="typedName"/> provided.</returns>
        public static implicit operator KeyValuePair<string, IType>(TypedName typedName)
        {
            if (typedName.Source != TypedNameSource.TypeReference)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.typedName, ExceptionMessageId.TypedName_ReferenceKind, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.Source), TypedNameSource.TypeReference.ToString());
            return new KeyValuePair<string, IType>(typedName.Name, typedName.TypeReference);
        }

        /* *
         * For languages that don't support implicit type conversion overloads.
         * */
        /// <summary>
        /// Converts a <see cref="TypedName"/> to a <see cref="KeyValuePair{TKey, TValue}"/>
        /// where TKey is <see cref="String"/> and TValue is <see cref="IType"/>.
        /// </summary>
        /// <param name="typedName">The <see cref="TypedName"/> to convert.</param>
        /// <returns>A <see cref="KeyValuePair{TKey, TValue}"/> relative to the
        /// <paramref name="typedName"/> provided.</returns>
        public static KeyValuePair<string, IType> ToKeyValuePair(TypedName typedName)
        {
            return typedName;
        }

        /// <summary>
        /// Converts a <see cref="KeyValuePair{TKey, TValue}"/> to a
        /// <see cref="TypedName"/> where TKey is <see cref="String"/> and
        /// TValue is <see cref="IType"/>.
        /// </summary>
        /// <param name="typedName">The <see cref="KeyValuePair{TKey, TValue}"/> to convert.</param>
        /// <returns>A new <see cref="TypedName"/> if the <see cref="IType"/> of the 
        /// <see cref="KeyValuePair{TKey, TValue}.Value"/> is a good reference.</returns>
        public static TypedName FromKeyValuePair(KeyValuePair<string, IType> typedName)
        {
            return typedName;
        }

        #region IEquatable<TypedName> Members

        public bool Equals(TypedName other)
        {
            return
                this.direction == other.direction &&
                this.name == other.name &&
                this.reference == null ?
                    this.symbolReference == null ?
                        other.symbolReference == null && other.reference == null :
                        this.symbolReference == other.symbolReference :
                    other.reference == null ? 
                        false :
                        this.reference.Equals(other.reference);
        }

        #endregion
    }
}
