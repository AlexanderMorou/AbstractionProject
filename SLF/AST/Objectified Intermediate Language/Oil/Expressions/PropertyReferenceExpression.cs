using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Provides a base implementation of an <see cref="IPropertyReferenceExpression"/>
    /// which references a <see cref="IPropertySignatureMember"/> 
    /// in code by name.
    /// </summary>
    public class PropertyReferenceExpression :
        MemberParentReferenceExpressionBase,
        IPropertyReferenceExpression
    {
        /// <summary>
        /// Data member for <see cref="ReferenceType"/>.
        /// </summary>
        private MethodReferenceType referenceType;
        /// <summary>
        /// Data member for <see cref="AssociatedMember"/>.
        /// </summary>
        private IPropertySignatureMember associatedMember;
        /// <summary>
        /// Data member for <see cref="Name"/>.
        /// </summary>
        private string name;
        /// <summary>
        /// Data member for <see cref="Source"/>.
        /// </summary>
        private IMemberParentReferenceExpression source;

        /// <summary>
        /// Creates a new <see cref="PropertyReferenceExpression"/> with the <paramref name="name"/>
        /// and <paramref name="source"/> provided.
        /// </summary>
        /// <param name="name">A <see cref="System.String"/> relative to the
        /// property to retrieve a reference to.</param>
        /// <param name="source">The <see cref="IMemberParentReferenceExpression"/>
        /// from which the <see cref="PropertyReferenceExpression"/> is to be
        /// sourced.</param>
        public PropertyReferenceExpression(string name, IMemberParentReferenceExpression source)
        {
            this.name = name;
            this.source = source;
        }

        #region IPropertyReferenceExpression Members
        /*
        /// <summary>
        /// Returns the <see cref="IPropertySignatureMember"/> 
        /// associated to the <see cref="PropertyReferenceExpression"/>.
        /// </summary>
        public IPropertySignatureMember AssociatedMember
        {
            get
            {
                if (!this.IsLinked)
                    this.Link();
                return this.associatedMember;
            }
        }
        */
        /// <summary>
        /// Returns/sets the type of reference to the 
        /// <see cref="IPropertyReferenceExpression"/>,
        /// get/set methods, is.
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
        /// Returns the <see cref="IMemberParentReferenceExpression"/>
        /// that sourced the <see cref="IPropertyReferenceExpression"/>.
        /// </summary>
        public IMemberParentReferenceExpression Source
        {
            get { return this.source; }
        }

        #endregion

        #region IMemberReferenceExpression Members

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
        /*
        IMember IMemberReferenceExpression.AssociatedMember
        {
            get { return this.AssociatedMember; }
        }
        */

        #endregion
        /*
        /// <summary>
        /// Returns the type which is used as a spring
        /// point for obtaining and linking the members.
        /// </summary>
        /// <remarks>Necessary for every 
        /// <see cref="IMemberParentReferenceExpression"/>
        /// to have in order to properly link.</remarks>
        public override IType ForwardType
        {
            get {
                return this.AssociatedMember.PropertyType;
            }
        }

        /// <summary>
        /// Invoked when <see cref="Link"/> is called.
        /// </summary>
        /// <remarks>Used in case <see cref="Link"/> needs 
        /// hidden and overridden.</remarks>
        protected override void OnLink()
        {
            try
            {
                IType t = this.Source.ForwardType;
                if (t.ElementClassification == TypeElementClassification.Array)
                    this.associatedMember = ((ICompiledClassType)typeof(Array).GetTypeReference()).Properties[this.Name];
                else if (t is IPropertyParentType)
                {
                    while (t.ElementClassification != TypeElementClassification.None)
                        t = t.ElementType;
                    this.associatedMember = (IPropertySignatureMember)((IPropertyParentType)(t)).Properties[this.Name];
                }
            }
            catch (Exception e)
            {
                //Rethrow the exception wrapped in an invalid operation exception.
                throw new InvalidOperationException(string.Format("Could not link property '{0}'.", this.name), e);
            }
        }
        */
        /// <summary>
        /// Returns the type of expression the <see cref="PropertyReferenceExpression"/> is.
        /// </summary>
        /// <remarks>Returns <see cref="ExpressionType.PropertyReference"/>.</remarks>
        public override ExpressionKind Type
        {
            get { return ExpressionKinds.PropertyReference; }
        }



        public override string ToString()
        {
            return string.Format("{0}.{1}", this.source.ToString(), this.Name);
        }

        public override void Visit(IIntermediateCodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
