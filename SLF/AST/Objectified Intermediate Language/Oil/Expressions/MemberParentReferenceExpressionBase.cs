using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides an abstract base for <see cref="IMemberParentReferenceExpression"/>
    /// implementations.
    /// </summary>
    public abstract class MemberParentReferenceExpressionBase :
        IMemberParentReferenceExpression
    {

        /// <summary>
        /// Returns the type of expression the <see cref="ExpressionBase"/> is.
        /// </summary>
        public abstract ExpressionKind Type { get; }

        #region IMemberParentReferenceExpression Members


        public IEventReferenceExpression GetEvent(string name)
        {
            return new EventReferenceExpression(this.ObtainRelativeGetMemberTarget(), name);
        }

        /// <summary>
        /// Obtains a <see cref="IMethodReferenceStub"/> for
        /// a method with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="System.String"/>
        /// of the method to reference.</param>
        /// <returns>A <see cref="IMethodReferenceStub"/> instance
        /// which points to a series of methods named '<paramref name="name"/>'.</returns>
        public virtual IMethodReferenceStub GetMethod(string name)
        {
            return new MethodReferenceStub(this.ObtainRelativeGetMemberTarget(), name);
        }

        /// <summary>
        /// Obtains a <see cref="IMethodReferenceStub"/> for
        /// a method with the <paramref name="name"/> and
        /// <paramref name="genericParameters"/> provided.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/> which
        /// denotes the name of the method.</param>
        /// <param name="genericParameters">A <see cref="ITypeCollection"/>
        /// which is used as generic parameter replacements.</param>
        /// <returns>A <see cref="IMethodReferenceStub"/> which
        /// refers to a series of methods with the given 
        /// generic parameters.</returns>
        public virtual IMethodReferenceStub GetMethod(string name, ITypeCollection genericParameters)
        {
            return new MethodReferenceStub(this.ObtainRelativeGetMemberTarget(), name, genericParameters);
        }

        /// <summary>
        /// Obtains a <see cref="IMethodPointerReferenceExpression"/>
        /// for a method with the <paramref name="name"/> and
        /// <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/> which
        /// denotes the name of the method.</param>
        /// <param name="signature">An <see cref="ITypeCollection"/>
        /// used to denote the signature of the method.</param>
        /// <returns>A <see cref="IMethodPointerReferenceExpression"/>
        /// instance which should be a verifiable pointer to the 
        /// method.</returns>
        /// <remarks>For verifiability, <see cref="ILinkableExpression.Link()"/>
        /// the <see cref="IMethodPointerReferenceExpression"/>.</remarks>
        public virtual IMethodPointerReferenceExpression GetMethodPointer(string name, ITypeCollection signature)
        {
            return this.GetMethod(name).GetPointer(signature);
        }

        /// <summary>
        /// Obtains a <see cref="IMethodPointerReferenceExpression"/>
        /// for a method with the <paramref name="name"/>, 
        /// <paramref name="signature"/>, and <paramref name="genericParameters"/> provided.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/> which
        /// denotes the name of the method.</param>
        /// <param name="signature">An <see cref="ITypeCollection"/>
        /// used to denote the signature of the method.</param>
        /// <param name="genericParameters">An <see cref="ITypeCollection"/>
        /// which is used as generic parameter replacements.</param>
        /// <returns>A <see cref="IMethodPointerReferenceExpression"/>
        /// instance which should be a verifiable pointer to the 
        /// method.</returns>
        /// <remarks>For verifiability, <see cref="ILinkableExpression.Link()"/>
        /// the <see cref="IMethodPointerReferenceExpression"/>.</remarks>
        public virtual IMethodPointerReferenceExpression GetMethodPointer(string name, ITypeCollection signature, ITypeCollection genericParameters)
        {
            return this.GetMethod(name, genericParameters).GetPointer(signature);
        }

        /// <summary>
        /// Gets a <paramref name="name"/>d indexer with the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/> indicating
        /// the name of the indexer to retrieve.</param>
        /// <param name="parameters">The <see cref="IExpressionCollection"/>
        /// used to reference the indexer.</param>
        /// <returns>An <see cref="IIndexerReferenceExpression"/> as
        /// described by <paramref name="name"/> and
        /// <paramref name="parameters"/>.</returns>
        /// <remarks>C&#9839; does not allow indexers of any other
        /// name than 'Item', because its language semantics
        /// do not have named indexers.</remarks>
        public virtual IIndexerReferenceExpression GetIndexer(string name, params IExpression[] parameters)
        {
            return new IndexerReferenceExpression(name, parameters, this.ObtainRelativeGetMemberTarget());
        }

        /// <summary>
        /// Obtains a <see cref="IPropertyReferenceExpression"/>
        /// </summary>
        /// <param name="name">A <see cref="System.String"/>
        /// denoting the name of the property to reference.</param>
        /// <returns>A <see cref="IPropertyReferenceExpression"/> related
        /// to the property described by <paramref name="name"/>.</returns>
        public virtual IPropertyReferenceExpression GetProperty(string name)
        {
            var binding = LooselyBindProperty(name);
            if (binding == null)
                return new PropertyReferenceExpression(name, this.ObtainRelativeGetMemberTarget());
            else
                return binding.GetPropertyReference(this);
        }

        internal IPropertySignatureMember LooselyBindProperty(string name)
        {
            var typeLookupAid = this.TypeLookupAid;
            if (typeLookupAid != null && !(typeLookupAid is ISymbolType))
            {
                if (typeLookupAid is IPropertyParentType)
                {
                    var currentParent = typeLookupAid;
                repeat:
                    var propertyParent = currentParent as IPropertyParentType;
                    if (propertyParent != null)
                        foreach (IPropertySignatureMember property in propertyParent.Properties.Values)
                            if (property.Name == name)
                                if (property is IPropertyMember)
                                    return property;
                                else
                                    return property;
                    if (currentParent != null)
                    {
                        currentParent = currentParent.BaseType;
                        goto repeat;
                    }
                }
                else if (typeLookupAid is IInterfaceType)
                {
                    var currentParent = typeLookupAid as IInterfaceType;
                    Queue<IInterfaceType> implementedInterfaces = new Queue<IInterfaceType>(currentParent.ImplementedInterfaces.Cast<IInterfaceType>());
                    var propertyParent = currentParent;
                repeat:
                    if (propertyParent != null)
                        foreach (IPropertySignatureMember property in propertyParent.Properties.Values)
                            if (property.Name == name)
                                if (property is IPropertyMember)
                                    return property;
                                else
                                    return property;
                    if (implementedInterfaces.Count > 0){
                        propertyParent = implementedInterfaces.Dequeue();
                        goto repeat;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a general case indexer with the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="parameters">The <see cref="IExpressionCollection"/>
        /// used to reference the indexer.</param>
        /// <returns></returns>
        /// <remarks>If the <see cref="ForwardType"/> is an array type
        /// returns an array indexer where all parameter types
        /// must be a number; otherwise it returns
        /// an indexer with the default name 'Item'.</remarks>
        public virtual IIndexerReferenceExpression GetIndexer(params IExpression[] parameters)
        {
            return GetIndexer(null, parameters);
        }

        /// <summary>
        /// Returns the <see cref="IFieldReferenceExpression"/>
        /// relative to the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/> of the 
        /// name of the field to retrieve.</param>
        /// <returns>A <see cref="IFieldReferenceExpression"/> 
        /// relative to the <paramref name="name"/>d field 
        /// that needs retrieved.</returns>
        public virtual IFieldReferenceExpression GetField(string name)
        {
            var looseBind = LooselyBindField(name);
            if (looseBind != null)
                return looseBind.GetFieldReference(this);
            else
                return new FieldReferenceExpression(name, this.ObtainRelativeGetMemberTarget());
        }

        internal IFieldMember LooselyBindField(string name)
        {
            var typeLookupAid = this.TypeLookupAid;
            if (typeLookupAid != null && !(typeLookupAid is ISymbolType))
            {
                var currentParent = typeLookupAid;
            repeat:
                var fieldParent = currentParent as IFieldParent;
                if (fieldParent != null)
                    foreach (IFieldMember field in fieldParent.Fields.Values)
                        if (field.Name == name)
                            return field;
                if (currentParent != null)
                {
                    currentParent = currentParent.BaseType;
                    goto repeat;
                }
            }
            return null;
        }

        /*
        /// <summary>
        /// Returns the type which is used as a spring
        /// point for obtaining and linking the members.
        /// </summary>
        /// <remarks>Necessary for every 
        /// <see cref="IMemberParentReferenceExpression"/>
        /// to have in order to properly link.</remarks>
        public abstract IType ForwardType { get; }
        */
        #endregion

        protected virtual MemberParentReferenceExpressionBase ObtainRelativeGetMemberTarget()
        {
            return this;
        }

        internal virtual MethodReferenceType MethodReferenceType
        {
            get
            {
                return MethodReferenceType.VirtualMethodReference;
            }
        }

        public abstract void Visit(IExpressionVisitor visitor);

        protected virtual IType TypeLookupAid
        {
            get
            {
                return null;
            }
        }

    }
}
