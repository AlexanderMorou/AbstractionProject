using System;
using System.Collections.Generic;
/*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Defines properties and methods for working with a custom attribute defined on a custom attributed declaration.
    /// </summary>
    public interface IMetadatum :
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="IType"/> the <see cref="IMetadatum"/> is.
        /// </summary>
        IType Type { get; }
        /// <summary>
        /// The <see cref="IMetadataEntity"/> on which the <see cref="IMetadatum"/> was declared.
        /// </summary>
        IMetadataEntity DeclarationPoint { get; }
        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> of pair elements which denote the type, and value of the parameters.
        /// </summary>
        IEnumerable<MetadatumTypedParameter> Parameters { get; }
        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> of triple elements which denote the type, name, and value of the named parameters.
        /// </summary>
        IEnumerable<MetadatumNamedParameter> NamedParameters { get; }
    }

    public struct MetadatumTypedParameter :
        IEquatable<MetadatumTypedParameter>
    {
        public static readonly MetadatumTypedParameter Empty = new MetadatumTypedParameter();

        public IType    ParameterType   { get; internal set; }
        public object   Value           { get; internal set; }
        public bool     IsEmpty         { get { return this.ParameterType   == null && 
                                                       this.Value           == null; } }

        public override bool Equals(object obj)
        {
            if (obj is MetadatumTypedParameter)
                return this == (MetadatumTypedParameter)obj;
            return false;
        }

        public bool Equals(MetadatumTypedParameter other)
        {
            return this == other;
        }

        public static bool operator ==(MetadatumTypedParameter left, MetadatumTypedParameter right)
        {
            if (left.IsEmpty)
                return right.IsEmpty;
            if (left.ParameterType == null && right.ParameterType != null ||
                left.ParameterType != null && right.ParameterType == null)
                return false;
            if (left.ParameterType.Equals(right.ParameterType))
                if (left.Value == null && right.Value == null)
                    return true;
                else if (left.Value == null || right.Value == null)
                    return false;
                else
                    left.Value.Equals(right.Value);
            return false;
        }
        public static bool operator !=(MetadatumTypedParameter left, MetadatumTypedParameter right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return (this.ParameterType == null ? 0 : this.ParameterType.GetHashCode()) ^
                   (this.Value == null ? 0 : this.Value.GetHashCode());
        }

        public static MetadatumTypedParameter<T> ToKnownTypedParameter<T>(MetadatumTypedParameter original)
        {
            if (original.Value is T)
                return new MetadatumTypedParameter<T> { ParameterType = original.ParameterType, Value = (T)original.Value };
            return new MetadatumTypedParameter<T>();
        }
    }

    public struct MetadatumTypedParameter<T> :
        IEquatable<MetadatumTypedParameter<T>>
    {
        public static readonly MetadatumTypedParameter<T> Empty = new MetadatumTypedParameter<T>();
        public IType    ParameterType   { get; internal set; }
        public T        Value           { get; internal set; }
        public bool     IsEmpty         { get { return this.ParameterType == null && 
                                                       object.ReferenceEquals(this.Value, null); } }

        public override bool Equals(object obj)
        {
            if (obj is MetadatumTypedParameter<T>)
                return this == (MetadatumTypedParameter<T>)obj;
            return false;
        }

        public bool Equals(MetadatumTypedParameter<T> other)
        {
            return this == other;
        }

        public static bool operator ==(MetadatumTypedParameter<T> left, MetadatumTypedParameter<T> right)
        {
            if (left.IsEmpty)
                return right.IsEmpty;
            if (left.ParameterType == null && right.ParameterType != null ||
                left.ParameterType != null && right.ParameterType == null)
                return false;
            if (left.ParameterType.Equals(right.ParameterType))
                if (object.ReferenceEquals(left, null) && object.ReferenceEquals(right.Value, null))
                    return true;
                else if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right.Value, null))
                    return false;
                else
                    left.Value.Equals(right.Value);
            return false;
        }
        public static bool operator !=(MetadatumTypedParameter<T> left, MetadatumTypedParameter<T> right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return (this.ParameterType == null ? 0 : this.ParameterType.GetHashCode()) ^
                   (object.ReferenceEquals(this.Value, null) ? 0 : this.Value.GetHashCode());
        }
    }

    public struct MetadatumNamedParameter :
        IEquatable<MetadatumNamedParameter>
    {
        public static readonly MetadatumNamedParameter Empty = new MetadatumNamedParameter();

        public IType    ParameterType   { get; internal set; }
        public object   Value           { get; internal set; }
        public string   MemberName      { get; internal set; }
        public bool     IsEmpty         { get { return this.ParameterType   == null && 
                                                       this.Value           == null && 
                                                       this.MemberName      == null; } }

        public static bool operator ==(MetadatumNamedParameter left, MetadatumNamedParameter right)
        {
            if (left.IsEmpty)
                return right.IsEmpty;
            if (left.ParameterType == null && right.ParameterType != null ||
                left.ParameterType != null && right.ParameterType == null)
                return false;
            if (left.MemberName == null && right.MemberName != null ||
                left.MemberName != null && right.MemberName == null)
                return false;
            if (left.ParameterType.Equals(right.ParameterType) &&
                left.MemberName == right.MemberName)
                if (left.Value == null && right.Value == null)
                    return true;
                else if (left.Value == null || right.Value == null)
                    return false;
                else
                    left.Value.Equals(right.Value);
            return false;
        }

        public static bool operator !=(MetadatumNamedParameter left, MetadatumNamedParameter right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj is MetadatumNamedParameter)
                return this == ((MetadatumNamedParameter)obj);
            return false;
        }

        public bool Equals(MetadatumNamedParameter other)
        {
            return this == other;
        }
        public override int GetHashCode()
        {
            return (this.ParameterType == null ? 0 : this.ParameterType.GetHashCode()) ^
                   (this.Value == null ? 0 : this.Value.GetHashCode()) ^
                   (this.MemberName == null ? 0 : this.MemberName.GetHashCode());
        }

        public static MetadatumNamedParameter<T> ToKnownNamedParameter<T>(MetadatumNamedParameter original)
        {
            if (original.Value is T)
                return new MetadatumNamedParameter<T> { ParameterType = original.ParameterType, Value = (T)original.Value, MemberName = original.MemberName };
            return new MetadatumNamedParameter<T>();
        }

    }

    public struct MetadatumNamedParameter<T> :
        IEquatable<MetadatumNamedParameter<T>>
    {
        public static readonly MetadatumNamedParameter<T> Empty = new MetadatumNamedParameter<T>();
        public IType    ParameterType   { get; internal set; }
        public T        Value           { get; internal set; }
        public string   MemberName      { get; internal set; }
        public bool     IsEmpty         { get { return this.ParameterType   == null             && 
                                                       object.ReferenceEquals(this.Value, null) && 
                                                       this.MemberName      == null; } }
        public static bool operator ==(MetadatumNamedParameter<T> left, MetadatumNamedParameter right)
        {
            return left == MetadatumNamedParameter.ToKnownNamedParameter<T>(right);
        }

        public static bool operator !=(MetadatumNamedParameter<T> left, MetadatumNamedParameter right)
        {
            return !(left == right);
        }

        public static bool operator ==(MetadatumNamedParameter<T> left, MetadatumNamedParameter<T> right)
        {
            if (left.IsEmpty)
                return right.IsEmpty;
            if (left.ParameterType == null && right.ParameterType != null ||
                left.ParameterType != null && right.ParameterType == null)
                return false;
            if (left.MemberName == null && right.MemberName != null ||
                left.MemberName != null && right.MemberName == null)
                return false;
            if (left.ParameterType.Equals(right.ParameterType) &&
                left.MemberName == right.MemberName)
                if (object.ReferenceEquals(left, null) && object.ReferenceEquals(right.Value, null))
                    return true;
                else if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right.Value, null))
                    return false;
                else
                    left.Value.Equals(right.Value);
            return false;
        }

        public static bool operator !=(MetadatumNamedParameter<T> left, MetadatumNamedParameter<T> right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj is MetadatumNamedParameter<T>)
                return this == ((MetadatumNamedParameter<T>)obj);
            return false;
        }

        public bool Equals(MetadatumNamedParameter<T> other)
        {
            return this == other;
        }
        public override int GetHashCode()
        {
            return (this.ParameterType == null ? 0 : this.ParameterType.GetHashCode()) ^
                   (object.ReferenceEquals(this.Value, null) ? 0 : this.Value.GetHashCode()) ^
                   (this.MemberName == null ? 0 : this.MemberName.GetHashCode());
        }

    }
}
