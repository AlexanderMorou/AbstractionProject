using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Modules;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public interface IIntermediateType<TTypeIdentifier, TType, TIntermediateType> :
        IIntermediateType,
        IIntermediateDeclaration<TTypeIdentifier>,
        IType<TTypeIdentifier, TType>
        where TTypeIdentifier :
            ITypeUniqueIdentifier,
            IGeneralDeclarationUniqueIdentifier
        where TType :
            IType<TTypeIdentifier,TType>
        where TIntermediateType :
            IIntermediateType<TTypeIdentifier, TType, TIntermediateType>
    {

    }

    /// <summary>
    /// Defines properties and methods for working with an intermediate type.
    /// </summary>
    public interface IIntermediateType :
        IEquatable<IIntermediateType>,
        IIntermediateMetadataEntity,
        IIntermediateScopedDeclaration,
        IType
    {
        /// <summary>
        /// Occurs when the base type of the <see cref="IIntermediateType"/>
        /// has changed.
        /// </summary>
        event EventHandler<EventArgs<IType, IType>> BaseTypeChanged;
        /// <summary>
        /// Returns/sets the <see cref="IIntermediateModule"/>
        /// in which the <see cref="IIntermediateType"/>
        /// is defined.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// value is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when 
        /// the target module is from another assembly.</exception>
        IIntermediateModule DeclaringModule { get;  set; }

        /// <summary>
        /// Returns the <see cref="IIntermediateTypeParent"/> which contains
        /// the current <see cref="IIntermediateType"/>.
        /// </summary>
        new IIntermediateTypeParent Parent { get; }

        /// <summary>
        /// Returns the <see cref="IIntermediateFullMemberDictionary"/>
        /// which designates a complete listing of the members contained
        /// within the <see cref="IIntermediateType"/>
        /// </summary>
        new IIntermediateFullMemberDictionary Members { get; }

        /// <summary>
        /// Returns the <see cref="IIntermediateNamespaceDeclaration"/>
        /// in which the current type (or its parent type) is declared.
        /// </summary>
        new IIntermediateNamespaceDeclaration Namespace { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> in which the
        /// <see cref="IIntermediateTypeParent"/> is defined.
        /// </summary>
        new IIntermediateAssembly Assembly { get; }
        /// <summary>
        /// Visits the <paramref name="visitor"/> provided.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateTypeVisitor"/> to
        /// receive the <see cref="IIntermediateType"/> as a visitor.</param>
        void Visit(IIntermediateTypeVisitor visitor);
        /// <summary>
        /// Visits the <paramref name="visitor"/> provided.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateTypeVisitor"/> to
        /// receive the <see cref="IIntermediateType"/> as a visitor.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        TResult Visit<TResult, TContext>(IIntermediateTypeVisitor<TResult, TContext> visitor, TContext context);
        /// <summary>
        /// Returns whether the <see cref="IIntermediateType"/> is disposed.
        /// </summary>
        bool IsDisposed { get; }
        /// <summary>
        /// Returns the <see cref="IIntermediateIdentityManager"/> which
        /// helps resolve type identities.
        /// </summary>
        new IIntermediateIdentityManager IdentityManager { get; }
        /// <summary>
        /// Returns/sets the <see cref="String"/> value denoting
        /// the Summary text of the <see cref="IIntermediateType"/>.
        /// </summary>
        string SummaryText { get; set; }
        /// <summary>
        /// Returns/sets the <see cref="String"/> value denoting
        /// the remarks text of the <see cref="IIntermediateType"/>.
        /// </summary>
        string RemarksText { get; set; }
    }
}
