using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal class _SymbolType :
        _GenericTypeBase<ISymbolType>,
        ISymbolType,
        IExpression
    {
        public _SymbolType(ISymbolType original, ITypeCollectionBase genericParameters)
            : base(original, genericParameters)
        {
        }
        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Other; }
        }

        protected override IFullMemberDictionary OnGetMembers()
        {
            return LockedFullMembersBase.Empty;
        }

        #region IExpression Members

        ExpressionKinds IExpression.Type
        {
            get { return ExpressionKinds.TypeReference; }
        }

        /// <summary>
        /// Visits the <paramref name="visitor"/> based upon the type of the
        /// <see cref="IExpression"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateCodeVisitor"/> 
        /// to visit.</param>
        /// <remarks>In this instance visits the <paramref name="visitor"/>
        /// through <see cref="IExpressionVisitor.Visit(ITypeReferenceExpression)"/>.</remarks>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="visitor"/>
        /// is null.</exception>
        public void Visit(IExpressionVisitor visitor)
        {
            //ToDo: Fix #2.
            visitor.Visit(this);
        }

        #endregion

        #region ITypeReferenceExpression Members

        public IType ReferenceType
        {
            get { return this; }
        }

        #endregion

        #region IMemberParentReferenceExpression Members

        public IMethodReferenceStub GetMethod(string name)
        {
            return new MethodReferenceStub(this, name);
        }

        public IMethodReferenceStub GetMethod(string name, ITypeCollection genericParameters)
        {
            return new MethodReferenceStub(this, name, genericParameters);
        }

        public IMethodPointerReferenceExpression GetMethodPointer(string name, ITypeCollection signature)
        {
            return this.GetMethod(name).GetPointer(signature);
        }

        public IMethodPointerReferenceExpression GetMethodPointer(string name, ITypeCollection signature, ITypeCollection genericParameters)
        {
            return this.GetMethod(name, genericParameters).GetPointer(signature);
        }

        public IIndexerReferenceExpression GetIndexer(string name, params IExpression[] parameters)
        {
            return new IndexerReferenceExpression(name, parameters, this);
        }

        public IPropertyReferenceExpression GetProperty(string name)
        {
            return new PropertyReferenceExpression(name, this);
        }

        public IIndexerReferenceExpression GetIndexer(params IExpression[] parameters)
        {
            return GetIndexer(null, parameters);
        }

        public IFieldReferenceExpression GetField(string name)
        {
            return new FieldReferenceExpression(name, this);
        }

        public IEventReferenceExpression GetEvent(string name)
        {
            return new EventReferenceExpression(name, this);
        }

        #endregion

        #region ISourceElement Members

        public string FileName { get; set; }

        public LineColumnPair? Start { get; set; }

        public LineColumnPair? End { get; set; }

        #endregion
    }
}
