using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{

    public interface IIntermediateMethodSignatureMemberDictionary<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> :
        IIntermediateMethodSignatureMemberDictionary<IMethodSignatureParameterMember<TSignature, TSignatureParent>, IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
        IIntermediateGroupedMemberDictionary<TSignatureParent, TIntermediateSignatureParent, IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature>,
        IMethodSignatureMemberDictionary<TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignature, TSignatureParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
        where TSignatureParent :
            IMethodSignatureParent<TSignature, TSignatureParent>
        where TIntermediateSignatureParent :
            IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
            TSignatureParent
    {

    }

    public interface IIntermediateMethodSignatureMemberDictionary<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> :
        IIntermediateSignatureMemberDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>,
        IIntermediateMemberDictionary<TSignatureParent, TIntermediateSignatureParent, IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature>,
        IMethodSignatureMemberDictionary<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TIntermediateSignatureParameter :
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
            TSignatureParameter
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TIntermediateSignature :
            IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
            TSignature
        where TSignatureParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignatureParent :
            TSignatureParent,
            IIntermediateSignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
    {
        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateSignature"/> with the
        /// <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the name the method is referred to by.</param>
        /// <returns>A new <typeparamref name="TIntermediateSignature"/> instance
        /// which has a <see cref="System.Void"/> return-type.</returns>
        TIntermediateSignature Add(string name);
        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateSignature"/> with the
        /// <paramref name="name"/> and <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the name the method is referred to by.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/> which designates
        /// the parameter names and types of the new <typeparamref name="TIntermediateSignature"/>.</param>
        /// <returns>A new <typeparamref name="TIntermediateSignature"/> instance
        /// which has a <see cref="System.Void"/> return-type.</returns>
        TIntermediateSignature Add(string name, TypedNameSeries parameters);
        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateSignature"/> with the
        /// <paramref name="name"/>, <paramref name="parameters"/>, and
        /// <paramref name="typeParameters"/>provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the name the method is referred to by.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/> which designates
        /// the parameter names and types of the new <typeparamref name="TIntermediateSignature"/>.</param>
        /// <param name="typeParameters">An array of <see cref="GenericParameterData"/> which
        /// defines the type-parameters and their individual type and functional constraints.</param>
        /// <returns>A new <typeparamref name="TIntermediateSignature"/>
        /// which has a <see cref="System.Void"/> return-type.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null, or <paramref name="typeParameters"/>
        /// is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/> is <see cref="String.Empty"/>
        /// or an element of <paramref name="parameters"/>, or <paramref name="typeParameters"/> contains an invalid type-reference (<see cref="System.Void"/>).</exception>
        TIntermediateSignature Add(string name, TypedNameSeries parameters, params GenericParameterData[] typeParameters);
        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateSignature"/>
        /// instance with the <paramref name="nameAndReturn"/> provided.
        /// </summary>
        /// <param name="nameAndReturn">A <see cref="TypedName"/> which designates the name of the method and its
        /// return-type.</param>
        /// <returns>A new <typeparamref name="TIntermediateSignature"/>.</returns>
        TIntermediateSignature Add(TypedName nameAndReturn);
        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateSignature"/>
        /// instance with the <paramref name="parameters"/>, <paramref name="nameAndReturn"/> provided.
        /// </summary>
        /// <param name="nameAndReturn">A <see cref="TypedName"/> which designates the name of the method and its
        /// return-type.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/> which designates
        /// the parameter names and types of the new <typeparamref name="TIntermediateSignature"/>.</param>
        /// <returns>A new <typeparamref name="TIntermediateSignature"/>.</returns>
        /// <exception cref="System.ArgumentException">thrown when the name portion of <paramref name="nameAndReturn"/> is <see cref="String.Empty"/>
        /// or an element of <paramref name="parameters"/> contains an invalid type-reference (<see cref="System.Void"/>).</exception>
        TIntermediateSignature Add(TypedName nameAndReturn, TypedNameSeries parameters);
        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateSignature"/>
        /// instane with the <paramref name="parameters"/>, <paramref name="typeParameters"/>, <paramref name="nameAndReturn"/>
        /// provided.
        /// </summary>
        /// <param name="nameAndReturn">A <see cref="TypedName"/> which designates the name of the method and its
        /// return-type.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/> which designates
        /// the parameter names and types of the new <typeparamref name="TIntermediateSignature"/>.</param>
        /// <param name="typeParameters">An array of <see cref="GenericParameterData"/> which
        /// defines the type-parameters and their individual type and functional constraints.</param>
        /// <returns>A new <typeparamref name="TIntermediateSignature"/>.</returns>
        /// <exception cref="System.ArgumentException">thrown when the name portion of <paramref name="nameAndReturn"/> is <see cref="String.Empty"/>
        /// or an element of <paramref name="parameters"/>, or <paramref name="typeParameters"/> contains an invalid type-reference (<see cref="System.Void"/>).</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="typeParameters"/>
        /// is null.</exception>
        TIntermediateSignature Add(TypedName nameAndReturn, TypedNameSeries parameters, params GenericParameterData[] typeParameters);

        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateSignature"/> with the <paramref name="name"/>
        /// and <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value which represents the unique identifier associated
        /// to the new <typeparamref name="TIntermediateSignature"/>.</param>
        /// <param name="signature">The <see cref="IDelegateType"/> which denotes the return-type
        /// and parameters of the <typeparamref name="TIntermediateSignature"/>.</param>
        /// <returns>A new <typeparamref name="TIntermediateSignature"/> with the <paramref name="name"/> provided
        /// which has the <paramref name="signature"/> provided.</returns>
        TIntermediateSignature Add(string name, IDelegateType signature);
    }
    /// <summary>
    /// Defines properties and methods for working with a series of intermediate
    /// method signature instances.
    /// </summary>
    public interface IIntermediateMethodSignatureMemberDictionary :
        IIntermediateSignatureMemberDictionary,
        IIntermediateGroupedMemberDictionary,
        IMethodSignatureMemberDictionary
    {
        /// <summary>
        /// Adds a new <see cref="IIntermediateMethodSignatureMember"/> with the
        /// <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the name the method is referred to by.</param>
        /// <returns>A new <see cref="IIntermediateMethodSignatureMember"/> instance
        /// which has a <see cref="System.Void"/> return-type.</returns>
        IIntermediateMethodSignatureMember Add(string name);
        /// <summary>
        /// Adds a new <see cref="IIntermediateMethodSignatureMember"/> with the
        /// <paramref name="name"/> and <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the name the method is referred to by.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/> which designates
        /// the parameter names and types of the new <see cref="IIntermediateMethodSignatureMember"/>.</param>
        /// <returns>A new <see cref="IIntermediateMethodSignatureMember"/> instance
        /// which has a <see cref="System.Void"/> return-type.</returns>
        IIntermediateMethodSignatureMember Add(string name, TypedNameSeries parameters);
        /// <summary>
        /// Adds a new <see cref="IIntermediateMethodSignatureMember"/> with the
        /// <paramref name="name"/>, <paramref name="parameters"/>, and
        /// <paramref name="typeParameters"/>provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the name the method is referred to by.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/> which designates
        /// the parameter names and types of the new <see cref="IIntermediateMethodSignatureMember"/>.</param>
        /// <param name="typeParameters">An array of <see cref="GenericParameterData"/> which
        /// defines the type-parameters and their individual type and functional constraints.</param>
        /// <returns>A new <see cref="IIntermediateMethodSignatureMember"/>
        /// which has a <see cref="System.Void"/> return-type.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null, or <paramref name="typeParameters"/>
        /// is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/> is <see cref="String.Empty"/>
        /// or an element of <paramref name="parameters"/>, or <paramref name="typeParameters"/> contains an invalid type-reference (<see cref="System.Void"/>).</exception>
        IIntermediateMethodSignatureMember Add(string name, TypedNameSeries parameters, params GenericParameterData[] typeParameters);
        /// <summary>
        /// Adds a new <see cref="IIntermediateMethodSignatureMember"/>
        /// instance with the <paramref name="nameAndReturn"/> provided.
        /// </summary>
        /// <param name="nameAndReturn">A <see cref="TypedName"/> which designates the name of the method and its
        /// return-type.</param>
        /// <returns>A new <see cref="IIntermediateMethodSignatureMember"/>.</returns>
        IIntermediateMethodSignatureMember Add(TypedName nameAndReturn);
        /// <summary>
        /// Adds a new <see cref="IIntermediateMethodSignatureMember"/>
        /// instance with the <paramref name="parameters"/>, <paramref name="nameAndReturn"/> provided.
        /// </summary>
        /// <param name="nameAndReturn">A <see cref="TypedName"/> which designates the name of the method and its
        /// return-type.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/> which designates
        /// the parameter names and types of the new <see cref="IIntermediateMethodSignatureMember"/>.</param>
        /// <returns>A new <see cref="IIntermediateMethodSignatureMember"/>.</returns>
        /// <exception cref="System.ArgumentException">thrown when the name portion of <paramref name="nameAndReturn"/> is <see cref="String.Empty"/>
        /// or an element of <paramref name="parameters"/> contains an invalid type-reference (<see cref="System.Void"/>).</exception>
        IIntermediateMethodSignatureMember Add(TypedName nameAndReturn, TypedNameSeries parameters);
        /// <summary>
        /// Adds a new <see cref="IIntermediateMethodSignatureMember"/>
        /// instane with the <paramref name="parameters"/>, <paramref name="typeParameters"/>, <paramref name="nameAndReturn"/>
        /// provided.
        /// </summary>
        /// <param name="nameAndReturn">A <see cref="TypedName"/> which designates the name of the method and its
        /// return-type.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/> which designates
        /// the parameter names and types of the new <see cref="IIntermediateMethodSignatureMember"/>.</param>
        /// <param name="typeParameters">An array of <see cref="GenericParameterData"/> which
        /// defines the type-parameters and their individual type and functional constraints.</param>
        /// <returns>A new <see cref="IIntermediateMethodSignatureMember"/>.</returns>
        /// <exception cref="System.ArgumentException">thrown when the name portion of <paramref name="nameAndReturn"/> is <see cref="String.Empty"/>
        /// or an element of <paramref name="parameters"/>, or <paramref name="typeParameters"/> contains an invalid type-reference (<see cref="System.Void"/>).</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="typeParameters"/>
        /// is null.</exception>
        IIntermediateMethodSignatureMember Add(TypedName nameAndReturn, TypedNameSeries parameters, params GenericParameterData[] typeParameters);
        /// <summary>
        /// Adds a new <see cref="IIntermediateMethodSignatureMember"/> with the <paramref name="name"/>
        /// and <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value which represents the unique identifier associated
        /// to the new <see cref="IIntermediateMethodSignatureMember"/>.</param>
        /// <param name="signature">The <see cref="IDelegateType"/> which denotes the return-type
        /// and parameters of the <see cref="IIntermediateMethodSignatureMember"/>.</param>
        /// <returns>A new <see cref="IIntermediateMethodSignatureMember"/> with the <paramref name="name"/> provided
        /// which has the <paramref name="signature"/> provided.</returns>
        IIntermediateMethodSignatureMember Add(string name, IDelegateType signature);

    }
}
