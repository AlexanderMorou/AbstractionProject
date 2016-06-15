using System;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides information for declaring a generic parameter.
    /// </summary>
    /// <remarks>Constructor signatures cannot contain 
    /// generic parameter references due to a limitation 
    /// in the current foundation's IL emission.</remarks>
    public class GenericParameterData :
        IEquatable<GenericParameterData>
    {
        /// <summary>
        /// Provides an empty generic parameter data set.
        /// </summary>
        public static readonly GenericParameterData[] EmptySet = new GenericParameterData[0];
        /// <summary>
        /// Data member for <see cref="Name"/>.
        /// </summary>
        string name;
        /// <summary>
        /// Data member for <see cref="Constructors"/>.
        /// </summary>
        private SignaturesData ctors;
        private ExtendedSignaturesData methods;
        private ExtendedSignaturesData indexers;
        private TypedNameSeries properties;
        private TypedNameSeries events;
        private ITypeCollection constraints;

        private GenericTypeParameterSpecialConstraint specialConstraint;

        /// <summary>
        /// Creates a new <see cref="GenericParameterData"/> with the <paramref name="name"/>,
        /// and <paramref name="constraints"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the
        /// unique identifier of the generic parameter to be constructed from
        /// the <see cref="GenericParameterData"/>.</param>
        /// <param name="constraints">The <see cref="IType"/> array
        /// specifying the constraints which bind the resultant generic
        /// parameter.</param>
        public GenericParameterData(string name, IType[] constraints = null)
            : this(name, SignaturesData.Empty, constraints) { }

        /// <summary>
        /// Creates a new <see cref="GenericParameterData"/>
        /// with the <paramref name="name"/> and <paramref name="constructors"/>
        /// provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/>
        /// that represents the name of the generic parameter.</param>
        /// <param name="constructors">The <see cref="SignatureData"/>
        /// series which defines the constructor information on the parameter.</param>
        public GenericParameterData(string name, params SignatureData[] constructors) : this(name, new SignaturesData(constructors)) { }

        /// <summary>
        /// Creates a new <see cref="GenericParameterData"/>
        /// with the <paramref name="name"/>, <paramref name="constructors"/>, and <paramref name="constraints"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/>
        /// that represents the name of the generic parameter.</param>
        /// <param name="constructors">The <see cref="SignaturesData"/>
        /// which defines the constructor information on the parameter.</param>
        /// <param name="constraints">The <see cref="IType"/> array
        /// specifying the constraints which bind the resultant generic
        /// parameter.</param>
        public GenericParameterData(string name, SignaturesData constructors, IType[] constraints = null)
            : this(name, constructors, ExtendedSignaturesData.Empty, ExtendedSignaturesData.Empty, TypedNameSeries.Empty, TypedNameSeries.Empty, constraints) { }

        /// <summary>
        /// Creates a new <see cref="GenericParameterData"/> with the
        /// <paramref name="name"/>, <paramref name="requiresBlankConstructor"/>, <paramref name="constructors"/>,
        /// <paramref name="methods"/>, <paramref name="indexers"/>, <paramref name="properties"/> and <paramref name="events"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value of the unique identifier of the <see cref="IGenericParameter"/>
        /// to create.</param>
        /// <param name="constructors">The <see cref="SignaturesData"/> associated to the constructors to add.</param>
        /// <param name="methods">The <see cref="ExtendedSignaturesData"/> associated to the methods to add.</param>
        /// <param name="indexers">The <see cref="ExtendedSignaturesData"/> associated to the indexers to add.</param>
        /// <param name="properties">The <see cref="TypedNameSeries"/> associated to the properties to add.</param>
        /// <param name="events">The <see cref="TypedNameSeries"/> associated to the events to add.</param>
        /// <param name="constraints">The <see cref="IType"/> array
        /// specifying the constraints which bind the resultant generic
        /// parameter.</param>
        public GenericParameterData(string name, SignaturesData constructors, ExtendedSignaturesData methods, ExtendedSignaturesData indexers, TypedNameSeries properties, TypedNameSeries events, IType[] constraints = null)
        {
            this.name = name;
            this.methods = methods;
            this.indexers = indexers;
            this.properties = properties;
            this.events = events;
            this.ctors = constructors;
            this.specialConstraint = GenericTypeParameterSpecialConstraint.None;
            if (constraints == null)
                this.constraints = null;
            else
                this.constraints = constraints.ToCollection();
        }
        /// <summary>
        /// Creates a new <see cref="GenericParameterData"/> with the
        /// <paramref name="name"/>, <paramref name="methods"/>, 
        /// <paramref name="indexers"/>, <paramref name="properties"/>,
        /// <paramref name="events"/> and <paramref name="constraints"/>
        /// provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value of the 
        /// unique identifier of the <see cref="IGenericParameter"/>
        /// to create.</param>
        /// <param name="methods">The <see cref="ExtendedSignaturesData"/>
        /// associated to the methods to add.</param>
        /// <param name="indexers">The <see cref="ExtendedSignaturesData"/>
        /// associated to the indexers to add.</param>
        /// <param name="properties">The <see cref="TypedNameSeries"/>
        /// associated to the properties to add.</param>
        /// <param name="events">The <see cref="TypedNameSeries"/>
        /// associated to the events to add.</param>
        /// <param name="constraints">The <see cref="IType"/> array
        /// specifying the constraints which bind the resultant generic
        /// parameter.</param>
        public GenericParameterData(string name, ExtendedSignaturesData methods, ExtendedSignaturesData indexers, TypedNameSeries properties, TypedNameSeries events, IType[] constraints = null)
            : this(name, SignaturesData.Empty, methods, indexers, properties, events, constraints) { }

        /// <summary>
        /// Creates a new <see cref="GenericParameterData"/> with the
        /// <paramref name="name"/>, <paramref name="indexers"/>, 
        /// <paramref name="properties"/>, <paramref name="events"/>
        /// and <paramref name="constraints"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value of the 
        /// unique identifier of the <see cref="IGenericParameter"/>
        /// to create.</param>
        /// <param name="indexers">The <see cref="ExtendedSignaturesData"/>
        /// associated to the indexers to add.</param>
        /// <param name="properties">The <see cref="TypedNameSeries"/>
        /// associated to the properties to add.</param>
        /// <param name="events">The <see cref="TypedNameSeries"/>
        /// associated to the events to add.</param>
        /// <param name="constraints">The <see cref="IType"/> array
        /// specifying the constraints which bind the resultant generic
        /// parameter.</param>
        public GenericParameterData(string name, ExtendedSignaturesData indexers, TypedNameSeries properties, TypedNameSeries events, IType[] constraints = null)
            : this(name, SignaturesData.Empty, ExtendedSignaturesData.Empty, indexers, properties, events, constraints)
        {
        }

        /// <summary>
        /// Creates a new <see cref="GenericParameterData"/> with the
        /// <paramref name="name"/>, <paramref name="properties"/>,
        /// <paramref name="events"/> and <paramref name="constraints"/>
        /// provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value of the 
        /// unique identifier of the <see cref="IGenericParameter"/>
        /// to create.</param>
        /// <param name="properties">The <see cref="TypedNameSeries"/>
        /// associated to the properties to add.</param>
        /// <param name="events">The <see cref="TypedNameSeries"/>
        /// associated to the events to add.</param>
        /// <param name="constraints">The <see cref="IType"/> array
        /// specifying the constraints which bind the resultant generic
        /// parameter.</param>
        public GenericParameterData(string name, TypedNameSeries properties, TypedNameSeries events, IType[] constraints = null)
            : this(name, SignaturesData.Empty, ExtendedSignaturesData.Empty, ExtendedSignaturesData.Empty, properties, events, constraints)
        {
        }

        /// <summary>
        /// Returns the <see cref="SignaturesData"/> of the series of
        /// <see cref="SignatureData"/> relative to the 
        /// constructors to be defined by the 
        /// <see cref="GenericParameterData"/>.
        /// </summary>
        public SignaturesData Constructors
        {
            get
            {
                return ctors;
            }
        }

        /// <summary>
        /// Returns the <see cref="ExtendedSignaturesData"/> of the series
        /// of <see cref="ExtendedSignatureData"/> relative to the 
        /// methods to be defined by the <see cref="GenericParameterData"/>.
        /// </summary>
        public ExtendedSignaturesData Methods
        {
            get
            {
                return this.methods;
            }
        }

        /// <summary>
        /// Returns the <see cref="ExtendedSignaturesData"/> of the series
        /// of <see cref="ExtendedSignatureData"/> relative to the 
        /// indexers to be defined by the <see cref="GenericParameterData"/>.
        /// </summary>
        public ExtendedSignaturesData Indexers
        {
            get
            {
                return this.indexers;
            }
        }

        /// <summary>
        /// Returns the <see cref="TypedNameSeries"/> of the series of
        /// <see cref="TypedName"/> instances relative to the
        /// properties defined by the <see cref="GenericParameterData"/>.
        /// </summary>
        public TypedNameSeries Properties
        {
            get
            {
                return this.properties;
            }
        }

        /// <summary>
        /// Returns the <see cref="TypedNameSeries"/> of the series of
        /// <see cref="TypedName"/> instances relative to the
        /// events defined by the <see cref="GenericParameterData"/>.
        /// </summary>
        public TypedNameSeries Events
        {
            get
            {
                return this.events;
            }
        }

        /// <summary>
        /// Returns the <see cref="String"/> that represents
        /// the name of the generic parameter represented by the 
        /// <see cref="GenericParameterData"/>.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> that denotes
        /// the sequence of <see cref="IType"/> elements that constrain
        /// the <see cref="IGenericParameter"/> described by the 
        /// <see cref="GenericParameterData"/>.
        /// </summary>
        public ITypeCollection Constraints
        {
            get
            {
                if (this.constraints == null)
                    this.constraints = new TypeCollection();
                return this.constraints;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is GenericParameterData)
                return this.Equals((GenericParameterData)(obj));
            return false;
        }

        public GenericTypeParameterSpecialConstraint SpecialConstraint { get { return this.specialConstraint; } set { this.specialConstraint = value; } }

        public bool Equals(GenericParameterData other)
        {
            if (other.Name != this.Name)
                return false;
            if (other.specialConstraint != this.specialConstraint)
                return false;
            if (other.constraints != null ^ this.constraints != null)
                return false;
            else if (other.constraints != null && this.constraints != null)
            {
                if (other.Constraints.Count != this.Constraints.Count)
                    return false;
                var extraneous = other.constraints.Except(this.constraints).Concat(this.constraints.Except(other.constraints));
                if (extraneous.Any())
                    return false;
            }
            if (other.methods       != null ^ this.methods      != null ||
                other.events        != null ^ this.events       != null ||
                other.indexers      != null ^ this.indexers     != null ||
                other.properties    != null ^ this.properties   != null ||
                other.ctors         != null ^ this.ctors        != null)
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return
                (this.constraints   != null ? 1 : 0) ^
                (this.ctors         != null ? 1 << 1 : 0) ^
                (this.events        != null ? 1 << 2 : 0) ^
                (this.indexers      != null ? 1 << 3 : 0) ^
                (this.methods       != null ? 1 << 4 : 0) ^
                (this.name          != null ? this.name.GetHashCode() : 0) ^
                (this.properties    != null ? 1 << 5 : 0) ^
                (this.specialConstraint.GetHashCode() << 8);
        }
    }
}
