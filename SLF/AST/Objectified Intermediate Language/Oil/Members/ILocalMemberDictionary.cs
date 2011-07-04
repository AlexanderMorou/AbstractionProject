using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Defines properties and methods for working with a series of local members
    /// which are either, explicitly, implicitly or dynamically typed.
    /// </summary>
    public interface ILocalMemberDictionary :
        IIntermediateMemberDictionary<IBlockStatementParent, IBlockStatementParent, ILocalMember, ILocalMember>
    {
        /// <summary>
        /// Inserts a new <see cref="ITypedLocalMember"/> from the <paramref name="name"/>
        /// and <paramref name="localType"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing
        /// the unique name, within the active scope, for the local to add.</param>
        /// <param name="localType">The <see cref="IType"/>
        /// associated to the <see cref="ITypedLocalMember"/> within the 
        /// active scope.</param>
        /// <returns>A new <see cref="ITypedLocalMember"/> with
        /// the <paramref name="name"/> and <paramref name="localType"/> provided.
        /// </returns>
        ITypedLocalMember Add(string name, IType localType);

        /// <summary>
        /// Inserts a new <see cref="ITypedLocalMember"/> from the <paramref name="nameAndType"/>
        /// provided.
        /// </summary>
        /// <param name="nameAndType">The <see cref="TypedName"/>
        /// which represents both the the unique name and type, within the active scope, 
        /// for the local to add.</param>
        /// <param name="initializationExpression">The <see cref="IExpression"/>
        /// which denotes the default value of the <see cref="ITypedLocalMember"/></param>
        /// <returns>A new <see cref="ITypedLocalMember"/> with
        /// the <paramref name="nameAndType"/> provided.
        /// </returns>
        ITypedLocalMember Add(TypedName nameAndType, IExpression initializationExpression);

        /// <summary>
        /// Inserts a new <see cref="ITypedLocalMember"/> from the <paramref name="nameAndType"/>
        /// provided.
        /// </summary>
        /// <param name="nameAndType">The <see cref="TypedName"/>
        /// which represents both the the unique name and type, within the active scope, 
        /// for the local to add.</param>
        /// <returns>A new <see cref="ITypedLocalMember"/> with
        /// the <paramref name="nameAndType"/> provided.
        /// </returns>
        ITypedLocalMember Add(TypedName nameAndType);

        /// <summary>
        /// Inserts a series of new <see cref="ITypedLocalMember"/> instances
        /// with the <see cref="TypedNameSeries"/>, relative to the active
        /// scope, for the <see cref="ITypedLocalMember"/> elements to add.
        /// </summary>
        /// <param name="namesAndTypes">The <see cref="TypedNameSeries"/>
        /// which denotes each element's name and type within the active scope.</param>
        /// <returns>a series of new <see cref="ITypedLocalMember"/> instances
        /// with the <see cref="TypedNameSeries"/> provided.</returns>
        ITypedLocalMember[] AddRange(TypedNameSeries namesAndTypes);

        /// <summary>
        /// Inserts a series of new <see cref="ITypedLocalMember"/> instances
        /// with the series of <see cref="TypedName"/> elements, relative
        /// to the active scope, for the <see cref="ITypedLocalMember"/> elements to add.
        /// </summary>
        /// <param name="namesAndTypes">The <see cref="TypedName"/> series
        /// which denotes each element's name and type within the active scope.</param>
        /// <returns>a series of new <see cref="ITypedLocalMember"/> instances
        /// with the <see cref="TypedName"/> series provided.</returns>
        ITypedLocalMember[] AddRange(params TypedName[] namesAndTypes);

        /// <summary>
        /// Inserts a new <see cref="ILocalMember"/> with the
        /// <paramref name="name"/>, <paramref name="initializationExpression"/>, 
        /// and <paramref name="typingMethod"/> provided.
        /// </summary>
        /// <param name="typingMethod">The <see cref="LocalTypingKind"/>
        /// which designates whether the local is dynamically typed or the
        /// type is inferred from its initialization expression.</param>
        /// <param name="name">The <see cref="String"/> value representing the unique
        /// identifier of the <see cref="ILocalMember"/> that results.</param>
        /// <param name="initializationExpression">The <see cref="IExpression"/> which
        /// initializes the <see cref="ILocalMember"/>.</param>
        /// <returns>A new <see cref="ILocalMember"/> with the
        /// <paramref name="typingMethod"/> and the <paramref name="name"/>
        /// and <paramref name="initializationExpression"/> provided.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">thrown when <paramref name="typingMethod"/>
        /// is not <see cref="LocalTypingKind.Implicit"/> or <see cref="LocalTypingKind.Dynamic"/>.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null.</exception>
        ILocalMember Add(string name, IExpression initializationExpression, LocalTypingKind typingMethod = LocalTypingKind.Implicit);
    }
}
