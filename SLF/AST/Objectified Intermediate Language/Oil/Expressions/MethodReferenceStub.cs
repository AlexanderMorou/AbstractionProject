﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides a base implementation for <see cref="IMethodReferenceStub"/>
    /// </summary>
    /// <remarks>Simpler form of 
    /// <see cref="IMethodPointerReferenceExpression"/>,
    /// used to obtain initial context data
    /// used to make a lookup.</remarks>
    public class MethodReferenceStub :
        IMethodReferenceStub
    {
        /// <summary>
        /// Data member for <see cref="ReferenceType"/>.
        /// </summary>
        private MethodReferenceType referenceType;

        /// <summary>
        /// Data member for <see cref="GenericParameters"/>.
        /// </summary>
        private ILockedTypeCollection genericParameters;

        /// <summary>
        /// Data member for <see cref="Name"/>.
        /// </summary>
        private string name;

        /// <summary>
        /// Data member for <see cref="Source"/>.
        /// </summary>
        private IMemberParentReferenceExpression source;

        /// <summary>
        /// Creates a new <see cref="MethodReferenceStub"/> with the 
        /// <paramref name="source"/>, <paramref name="name"/>, 
        /// <paramref name="genericParameters"/> and 
        /// <paramref name="referenceType"/> provdied.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// from which the <see cref="MethodReferenceStub"/> was sourced.</param>
        /// <param name="name">A <see cref="System.String"/>
        /// relative to the name of the method.</param>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/>
        /// of generic parameter replacements for the signature.</param>
        /// <param name="referenceType">The means to refer to
        /// the method.</param>
        public MethodReferenceStub(IMemberParentReferenceExpression source, string name, ITypeCollectionBase genericParameters, MethodReferenceType referenceType)
        {
            this.name = name;
            this.genericParameters = genericParameters is ILockedTypeCollection ? ((ILockedTypeCollection)(genericParameters)) : genericParameters.ToLockedCollection();
            this.referenceType = referenceType;
            this.source = source;
        }

        /// <summary>
        /// Creates a new <see cref="MethodReferenceStub"/> with the 
        /// <paramref name="source"/>, <paramref name="name"/>, and
        /// <paramref name="genericParameters"/> provdied.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// from which the <see cref="MethodReferenceStub"/> was sourced.</param>
        /// <param name="name">A <see cref="System.String"/>
        /// relative to the name of the method.</param>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/>
        /// of generic parameter replacements for the signature.</param>
        public MethodReferenceStub(IMemberParentReferenceExpression source, string name, ITypeCollectionBase genericParameters)
        {
            this.name = name;
            this.genericParameters = genericParameters is ILockedTypeCollection ? ((ILockedTypeCollection)(genericParameters)) : genericParameters.ToLockedCollection();
            this.source = source;
        }

        /// <summary>
        /// Creates a new <see cref="MethodReferenceStub"/> with the 
        /// <paramref name="source"/>, <paramref name="name"/>, and 
        /// <paramref name="referenceType"/> provdied.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// from which the <see cref="MethodReferenceStub"/> was sourced.</param>
        /// <param name="name">A <see cref="System.String"/>
        /// relative to the name of the method.</param>
        /// <param name="referenceType">The means to refer to
        /// the method.</param>
        public MethodReferenceStub(IMemberParentReferenceExpression source, string name, MethodReferenceType referenceType)
        {
            this.name = name;
            this.referenceType = referenceType;
            this.source = source;
        }

        /// <summary>
        /// Creates a new <see cref="MethodReferenceStub"/> with
        /// the <paramref name="source"/>, and <paramref name="name"/> 
        /// provided.
        /// </summary>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// from which the <see cref="MethodReferenceStub"/> was sourced.</param>
        /// <param name="name">A <see cref="System.String"/>
        /// relative to the name of the method.</param>
        public MethodReferenceStub(IMemberParentReferenceExpression source, string name)
        {
            this.name = name;
            this.source = source;
        }

        /// <summary>
        /// Creates a new <see cref="MethodReferenceStub"/> with the 
        /// <paramref name="name"/>, <paramref name="genericParameters"/> 
        /// and <paramref name="referenceType"/> provdied.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/>
        /// relative to the name of the method.</param>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/>
        /// of generic parameter replacements for the signature.</param>
        /// <param name="referenceType">The means to refer to
        /// the method.</param>
        public MethodReferenceStub(string name, ITypeCollectionBase genericParameters, MethodReferenceType referenceType)
            : this(null, name, genericParameters, referenceType)
        {
        }

        /// <summary>
        /// Creates a new <see cref="MethodReferenceStub"/> with the 
        /// <paramref name="name"/>, and <paramref name="genericParameters"/> 
        /// provdied.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/>
        /// relative to the name of the method.</param>
        /// <param name="genericParameters">The <see cref="ITypeCollection"/>
        /// of generic parameter replacements for the signature.</param>
        public MethodReferenceStub(string name, ITypeCollectionBase genericParameters)
            : this(null, name, genericParameters)
        {
        }

        /// <summary>
        /// Creates a new <see cref="MethodReferenceStub"/> with the 
        /// <paramref name="source"/>, <paramref name="name"/>, and 
        /// <paramref name="referenceType"/> provdied.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/>
        /// relative to the name of the method.</param>
        /// <param name="referenceType">The means to refer to
        /// the method.</param>
        public MethodReferenceStub(string name, MethodReferenceType referenceType)
            : this(null, name, referenceType)
        {
        }

        /// <summary>
        /// Creates a new <see cref="MethodReferenceStub"/> with
        /// the  <paramref name="name"/> 
        /// provided.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/>
        /// relative to the name of the method.</param>
        public MethodReferenceStub(string name)
        {
            this.name = name;
        }

        #region IMethodReferenceStub Members

        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/>
        /// that sourced the <see cref="IMethodReferenceStub"/>.
        /// </summary>
        public IMemberParentReferenceExpression Source
        {
            get { return this.source; }
        }

        /// <summary>
        /// Returns/sets the type of reference the 
        /// <see cref="MethodReferenceStub"/> is.
        /// </summary>
        public MethodReferenceType ReferenceType
        {
            get
            {
                return this.referenceType;
            }
            set
            {
                this.referenceType = value;
            }
        }

        /// <summary>
        /// Returns the <see cref="ITypeCollection"/> of 
        /// <see cref="IType"/> instances used to replace
        /// the generic parameters of the method.
        /// </summary>
        public ILockedTypeCollection GenericParameters
        {
            get { return this.genericParameters; }
        }

        /// <summary>
        /// Returns/sets the name of the method associated
        /// to the <see cref="IMethodReferenceStub"/>.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Obtains a <see cref="IMethodInvokeExpression"/>
        /// by evaluating the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="parameters">A series of 
        /// <see cref="IExpression"/> elements 
        /// which relate to the data of the 
        /// parameters of the invoke, and the 
        /// types of the parameters.</param>
        /// <returns>A new <see cref="IMethodInvokeExpression"/> 
        /// relative to the signature and data of 
        /// the <paramref name="parameters"/> 
        /// provided.</returns>
        public IMethodInvokeExpression Invoke(IExpressionCollection parameters)
        {
            return new MethodPointerReferenceExpression(this).Invoke(parameters);
        }

        /// <summary>
        /// Obtains a <see cref="IMethodInvokeExpression"/>
        /// by evaluating the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="parameters">A series of
        /// <see cref="IExpression"/> elements 
        /// which relate to the data of the 
        /// parameters of the invoke, and the types 
        /// of the parameters.</param>
        /// <returns>A new <see cref="IMethodInvokeExpression"/> 
        /// relative to the signature and data of 
        /// the <paramref name="parameters"/> 
        /// provided.</returns>
        public IMethodInvokeExpression Invoke(params IExpression[] parameters)
        {
            return this.Invoke(parameters.ToCollection());
        }
        /// <summary>
        /// Obtains a <see cref="IMethodPointerRefernceExpression"/>
        /// with the <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="signature">The <see cref="ITypeCollection"/>
        /// relative to the type-signature of the <see cref="IMethodPointerReferenceExpression"/>
        /// to obtain.</param>
        /// <returns>A new <see cref="IMethodPointerReferenceExpression"/>
        /// relative to the <paramref name="signature"/>
        /// provided.</returns>
        public IMethodPointerReferenceExpression GetPointer(ITypeCollection signature)
        {
            return new MethodPointerReferenceExpression(this, signature);
        }

        /// <summary>
        /// Obtains a <see cref="IMethodPointerRefernceExpression"/>
        /// with the <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="signature">The series if <see cref="IType"/>
        /// elements relative to the type-signature of the 
        /// <see cref="IMethodPointerReferenceExpression"/>
        /// to obtain.</param>
        /// <returns>A new <see cref="IMethodPointerReferenceExpression"/>
        /// relative to the <paramref name="signature"/>
        /// provided.</returns>
        public IMethodPointerReferenceExpression GetPointer(params IType[] signature)
        {
            return this.GetPointer(signature.ToCollection());
        }

        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.Source != null)
            {
                sb.Append(this.Source.ToString());
                sb.Append(".");
            }
            sb.Append(this.Name);
            if (this.genericParameters != null && this.genericParameters.Count > 0)
            {
                bool first = true;
                sb.Append("<");
                foreach (IType t in this.GenericParameters)
                {
                    if (first)
                        first = false;
                    else
                        sb.Append(", ");
                    sb.Append(t.Name);
                }
                sb.Append(">");
            }
            return sb.ToString();
        }
    }
}