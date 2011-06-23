using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
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
    /// Provides an implementation of a constructor member.
    /// </summary>
    /// <typeparam name="TCtor">The type of the constructor in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCtor">The type of the constructor in the intermediate type system.</typeparam>
    /// <typeparam name="TType">The type of the owning <see cref="ICreatableType{TCtor, TIntermediateType}"/> in 
    /// the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The type of the owning <see cref="IIntermediateCreatableType{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    public partial class IntermediateConstructorSignatureMemberBase<TCtor, TIntermediateCtor, TType, TIntermediateType> :
        IntermediateSignatureMemberBase<TCtor, TIntermediateCtor, IConstructorParameterMember<TCtor, TType>, IIntermediateConstructorSignatureParameterMember<TCtor, TIntermediateCtor, TType, TIntermediateType>, TType, TIntermediateType>,
        IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            TCtor,
            IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TType :
            ICreatableType<TCtor, TType>
        where TIntermediateType :
            TType,
            IIntermediateCreatableSignatureType<TCtor, TIntermediateCtor, TType, TIntermediateType>
    {
        private bool naming = false;
        private bool typeInitializer;
        /// <summary>
        /// Creates a new <see cref="IntermediateConstructorSignatureMemberBase{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// with the <paramref name="parent"/> provdied.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateType"/>
        /// which the <see cref="IntermediateConstructorSignatureMemberBase{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// belongs to.</param>
        /// <param name="typeInitializer">Whether the <see cref="IntermediateConstructorSignatureMemberBase{TCtor, TIntermediateCtor, TType, TIntermediateType}"/> 
        /// is a type initializer</param>
        internal IntermediateConstructorSignatureMemberBase(TIntermediateType parent, bool typeInitializer = false)
            : base(parent)
        {
            this.typeInitializer = typeInitializer;
            if (typeInitializer)
            {
                naming = true;
                this.Name = ".cctor";
                typeInitializer = true;
                naming = false;
            }
            else
            {
                naming = true;
                this.Name = ".ctor";
                typeInitializer = false;
                naming = false;
            }
        }

        #region IIntermediateScopedDeclaration Members

        /// <summary>
        /// Returns/sets the access level of the
        /// <see cref="IntermediateConstructorSignatureMemberBase{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>.
        /// </summary>
        public virtual AccessLevelModifiers AccessLevel { get; set; }

        #endregion

        protected override IntermediateParameterMemberDictionary<TCtor, TIntermediateCtor, IConstructorParameterMember<TCtor, TType>, IIntermediateConstructorSignatureParameterMember<TCtor, TIntermediateCtor, TType, TIntermediateType>> InitializeParameters()
        {
            return new ParameterDictionary(((TIntermediateCtor)((object)(this))));
        }

        public override string UniqueIdentifier
        {
            get {
                StringBuilder uid = new StringBuilder();
                bool first = true;
                foreach (var p in this.Parameters.Values)
                {
                    if (first)
                        first = false;
                    else
                        uid.Append(", ");
                    if (p.ParameterType.FullName == null)
                        uid.Append(p.ParameterType.Name);
                    else
                        uid.Append(p.ParameterType.FullName);
                }
                return string.Format("{0}({1})", this.Name, uid.ToString());

            }
        }
        protected override void OnRenaming(DeclarationRenamingEventArgs e)
        {
            if (!naming && e != null)
            {
                //Disallow the name change.
                e.Change = false;
                return;
            }
            base.OnRenaming(e);
        }


        public override void Visit(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
