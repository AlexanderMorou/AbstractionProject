using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// property signature.
    /// </summary>
    /// <typeparam name="TProperty">The type of property signature used in the
    /// abstract type system.</typeparam>
    /// <typeparam name="TIntermediateProperty">The type of property signature used in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TPropertyParent">The type which acts as the parent of the properties
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediatePropertyParent">The type which acts as the parent of the 
    /// properties in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> :
        IIntermediateMember<TPropertyParent, TIntermediatePropertyParent>,
        IIntermediatePropertySignatureMember,
        IPropertySignatureMember<TProperty, TPropertyParent>
        where TProperty :
            IPropertySignatureMember<TProperty, TPropertyParent>
        where TIntermediateProperty :
            TProperty,
            IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
        where TPropertyParent :
            IPropertySignatureParentType<TProperty, TPropertyParent>
        where TIntermediatePropertyParent :
            TPropertyParent,
            IIntermediatePropertySignatureParentType<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with an intermediate
    /// property signature.
    /// </summary>
    public interface IIntermediatePropertySignatureMember :
        IIntermediateMember,
        IPropertySignatureMember
    {
        /// <summary>
        /// Returns/sets the type that the <see cref="IIntermediatePropertySignatureMember"/>
        /// is defined as.
        /// </summary>
        new IType PropertyType { get; set; }
        /// <summary>
        /// Returns/sets whether the <see cref="IIntermediatePropertySignatureMember"/>
        /// can be read from.
        /// </summary>
        /// <remarks>If set to false, from true, the <see cref="IIntermediateMethodSignatureMember"/> for the <see cref="GetMethod"/>
        /// will be disposed.</remarks>
        new bool CanRead { get; set; }
        /// <summary>
        /// Returns/sets whether the <see cref="IIntermediatePropertySignatureMember"/> 
        /// can be written to.
        /// </summary>
        /// <remarks>If set to false, from true, the <see cref="IIntermediateMethodSignatureMember"/> for the <see cref="SetMethod"/>
        /// will be disposed.</remarks>
        new bool CanWrite { get; set; }
        /// <summary>
        /// Returns the <see cref="IIntermediatePropertySignatureMethodMember"/> 
        /// which represents the get method of the 
        /// <see cref="IIntermediatePropertySignatureMember"/>.
        /// </summary>
        /// <remarks>Is null if <paramref name="CanRead"/> is false.</remarks>
        new IIntermediatePropertySignatureMethodMember GetMethod { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediatePropertySignatureMethodMember"/> 
        /// which represents the set method of the 
        /// <see cref="IIntermediatePropertySignatureMember"/>.
        /// </summary>
        /// <remarks>Is null if <paramref name="CanWrite"/> is false.</remarks>
        new IIntermediatePropertySignatureMethodMember SetMethod { get; }
    }
}
