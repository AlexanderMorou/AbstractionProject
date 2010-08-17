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

    public interface IIntermediateMethodSignatureMemberDictionary<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> :
        IIntermediateMethodSignatureMemberDictionary<IMethodSignatureParameterMember<TSignature, TSignatureParent>, IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
        IIntermediateGroupedMemberDictionary<TSignatureParent, TIntermediateSignatureParent, TSignature, TIntermediateSignature>,
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
        IIntermediateSignatureMemberDictionary<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>,
        IIntermediateMemberDictionary<TSignatureParent, TIntermediateSignatureParent, TSignature, TIntermediateSignature>,
        IMethodSignatureMemberDictionary<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TIntermediateSignatureParameter :
            TSignatureParameter,
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignatureParent :
            TSignatureParent,
            IIntermediateSignatureParent<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
    {
        /// <summary>
        /// Adds a new <see cref="TIntermediateSignature"/> with the
        /// <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the name the method is referred to by.</param>
        /// <returns>A new <see cref="TIntermediateSignature"/> instance
        /// which has a <see cref="System.Void"/> return-type.</returns>
        TIntermediateSignature Add(string name);
        /// <summary>
        /// Adds a new <see cref="TIntermediateSignature"/> with the
        /// <paramref name="name"/> and <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the name the method is referred to by.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/> which designates
        /// the parameter names and types of the new <see cref="TIntermediateSignature"/>.</param>
        /// <returns>A new <see cref="TIntermediateSignature"/> instance
        /// which has a <see cref="System.Void"/> return-type.</returns>
        TIntermediateSignature Add(string name, TypedNameSeries parameters);
        /// <summary>
        /// Adds a new <see cref="TIntermediateSignature"/> with the
        /// <paramref name="name"/>, <paramref name="parameters"/>, and
        /// <paramref name="typeParameters"/>provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the name the method is referred to by.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/> which designates
        /// the parameter names and types of the new <see cref="TIntermediateSignature"/>.</param>
        /// <param name="typeParameters">An array of <see cref="GenericParameterData"/> which
        /// defines the type-parameters and their individual type and functional constraints.</param>
        /// <returns>A new <typeparamref name="TIntermediateSignature"/>
        /// which has a <see cref="System.Void"/> return-type.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null, or <see cref="typeParameters"/>
        /// is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/> is <see cref="String.Empty"/>
        /// or an element of <paramref name="parameters"/>, or <paramref name="typeParameters"/> contains an invalid type-reference (<see cref="System.Void"/>).</exception>
        TIntermediateSignature Add(string name, TypedNameSeries parameters, params GenericParameterData[] typeParameters);
        /// <summary>
        /// Adds a new <see cref="TIntermediateSignature"/>
        /// instance with the <paramref name="nameAndReturn"/> provided.
        /// </summary>
        /// <param name="nameAndReturn">A <see cref="TypedName"/> which designates the name of the method and its
        /// return-type.</param>
        /// <returns>A new <typeparamref name="TIntermediateSignature"/>.</returns>
        TIntermediateSignature Add(TypedName nameAndReturn);
        /// <summary>
        /// Adds a new <see cref="TIntermediateSignature"/>
        /// instance with the <paramref name="parameters"/>, <paramref name="nameAndReturn"/> provided.
        /// </summary>
        /// <param name="nameAndReturn">A <see cref="TypedName"/> which designates the name of the method and its
        /// return-type.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/> which designates
        /// the parameter names and types of the new <see cref="TIntermediateSignature"/>.</param>
        /// <returns>A new <typeparamref name="TIntermediateSignature"/>.</returns>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/> is <see cref="String.Empty"/>
        /// or an element of <paramref name="parameters"/> contains an invalid type-reference (<see cref="System.Void"/>).</exception>
        TIntermediateSignature Add(TypedName nameAndReturn, TypedNameSeries parameters);
        /// <summary>
        /// Adds a new <see cref="TIntermediateSignature"/>
        /// instane with the <paramref name="parameters"/>, <paramref name="typeParameters"/>, <paramref name="nameAndReturn"/>
        /// provided.
        /// </summary>
        /// <param name="nameAndReturn">A <see cref="TypedName"/> which designates the name of the method and its
        /// return-type.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/> which designates
        /// the parameter names and types of the new <see cref="TIntermediateSignature"/>.</param>
        /// <param name="typeParameters">An array of <see cref="GenericParameterData"/> which
        /// defines the type-parameters and their individual type and functional constraints.</param>
        /// <returns>A new <typeparamref name="TIntermediateSignature"/>.</returns>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/> is <see cref="String.Empty"/>
        /// or an element of <paramref name="parameters"/>, or <paramref name="typeParameters"/> contains an invalid type-reference (<see cref="System.Void"/>).</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <see cref="typeParameters"/>
        /// is null.</exception>
        TIntermediateSignature Add(TypedName nameAndReturn, TypedNameSeries parameters, params GenericParameterData[] typeParameters);
    }
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
        /// <returns>A new <see cref="IIntermediateMethodSignatureMmber"/>
        /// which has a <see cref="System.Void"/> return-type.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null, or <see cref="typeParameters"/>
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
        /// <returns>A new <see cref="IIntermediateMethodSignatureMmber"/>.</returns>
        IIntermediateMethodSignatureMember Add(TypedName nameAndReturn);
        /// <summary>
        /// Adds a new <see cref="IIntermediateMethodSignatureMember"/>
        /// instance with the <paramref name="parameters"/>, <paramref name="nameAndReturn"/> provided.
        /// </summary>
        /// <param name="nameAndReturn">A <see cref="TypedName"/> which designates the name of the method and its
        /// return-type.</param>
        /// <param name="parameters">The <see cref="TypedNameSeries"/> which designates
        /// the parameter names and types of the new <see cref="IIntermediateMethodSignatureMember"/>.</param>
        /// <returns>A new <see cref="IIntermediateMethodSignatureMmber"/>.</returns>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/> is <see cref="String.Empty"/>
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
        /// <returns>A new <see cref="IIntermediateMethodSignatureMmber"/>.</returns>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/> is <see cref="String.Empty"/>
        /// or an element of <paramref name="parameters"/>, or <paramref name="typeParameters"/> contains an invalid type-reference (<see cref="System.Void"/>).</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <see cref="typeParameters"/>
        /// is null.</exception>
        IIntermediateMethodSignatureMember Add(TypedName nameAndReturn, TypedNameSeries parameters, params GenericParameterData[] typeParameters);
    }
}
