using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
//using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Utilities.Properties;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Provides an implementation of a constructor member.
    /// </summary>
    /// <typeparam name="TCtor">The type of the constructor in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCtor">The type of the constructor in the intermediate type system.</typeparam>
    /// <typeparam name="TType">The type of the owning <see cref="ICreatableParent{TCtor, TIntermediateType}"/> in 
    /// the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The type of the owning <see cref="IIntermediateCreatableParent{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    public partial class IntermediateConstructorSignatureMemberBase<TCtor, TIntermediateCtor, TType, TIntermediateType> :
        IntermediateSignatureMemberBase<IGeneralSignatureMemberUniqueIdentifier, TCtor, TIntermediateCtor, IConstructorParameterMember<TCtor, TType>, IIntermediateConstructorSignatureParameterMember<TCtor, TIntermediateCtor, TType, TIntermediateType>, TType, TIntermediateType>,
        IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>,
        _IntermediateConstructorSignatureMember
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            TCtor,
            IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TType :
            ICreatableParent<TCtor, TType>
        where TIntermediateType :
            TType,
            IIntermediateCreatableSignatureParent<TCtor, TIntermediateCtor, TType, TIntermediateType>
    {
        private bool typeInitializer;
        private IGeneralSignatureMemberUniqueIdentifier uniqueIdentifier;
        private IMetadataDefinitionCollection metadata;
        private IMetadataCollection metadataBack;

        /// <summary>
        /// Creates a new <see cref="IntermediateConstructorSignatureMemberBase{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// with the <paramref name="parent"/> provdied.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateType"/>
        /// which the <see cref="IntermediateConstructorSignatureMemberBase{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// belongs to.</param>
        /// <param name="assembly">The <see cref="IIntermediateAssembly"/>
        /// which contains the intermediate signature member and contains context relative
        /// to disambiguating type identities.</param>
        /// <param name="typeInitializer">Whether the <see cref="IntermediateConstructorSignatureMemberBase{TCtor, TIntermediateCtor, TType, TIntermediateType}"/> 
        /// is a type initializer</param>
        internal IntermediateConstructorSignatureMemberBase(TIntermediateType parent, IIntermediateAssembly assembly, bool typeInitializer = false)
            : base(parent, assembly)
        {
            this.typeInitializer = typeInitializer;
            this.AssignName(typeInitializer ? ".cctor" : ".ctor");
        }
        public bool IsStaticConstructor { get { return this.typeInitializer; } }

        #region IIntermediateScopedDeclaration Members

        /// <summary>
        /// Returns/sets the access level of the
        /// <see cref="IntermediateConstructorSignatureMemberBase{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>.
        /// </summary>
        public virtual AccessLevelModifiers AccessLevel { get; set; }

        #endregion

        protected override IntermediateParameterMemberDictionary<TCtor, TIntermediateCtor, IConstructorParameterMember<TCtor, TType>, IIntermediateConstructorSignatureParameterMember<TCtor, TIntermediateCtor, TType, TIntermediateType>> InitializeParameters()
        {
            return new ParameterDictionary(this);
        }

        public override IGeneralSignatureMemberUniqueIdentifier UniqueIdentifier
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (uniqueIdentifier == null)
                    {
                        var service = this.Assembly.GetUniqueIdentifierService();
                        if (this is IIntermediateConstructorMember)
                        {
                            var cMem = (IIntermediateConstructorMember)this;
                            this.uniqueIdentifier = service.HandlesConstructorMemberIdentifier
                                                    ? service.GetIdentifier(cMem)
                                                    : IntermediateGateway.DefaultUniqueIdentifierService.GetIdentifier(cMem);
                        }
                        else
                        {
                            this.uniqueIdentifier = service.HandlesConstructorSignatureMemberIdentifier
                                                    ? service.GetIdentifier(this)
                                                    : IntermediateGateway.DefaultUniqueIdentifierService.GetIdentifier(this);
                        }
                    }
                    return this.uniqueIdentifier;
                }
            }
        }

        protected override void ClearIdentifier()
        {
            lock (this.SyncObject)
                if (this.uniqueIdentifier != null)
                    this.uniqueIdentifier = null;
        }

        public override void Accept(IIntermediateMemberVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        IMetadataCollection IMetadataEntity.Metadata
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.metadataBack != null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                        else
                            this.metadataBack = ((MetadataDefinitionCollection)(this.Metadata)).GetWrapper();
                    return this.metadataBack;
                }
            }
        }

        public IMetadataDefinitionCollection Metadata
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.metadata == null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                        else
                            this.metadata = new MetadataDefinitionCollection(this, this.Parent.Assembly);
                    return this.metadata;
                }
            }
        }

        public bool IsDefined(IType metadatumType)
        {
            return this.Metadata.Contains(metadatumType);
        }

        bool _IntermediateConstructorSignatureMember.AreParametersInitialized
        {
            get { return this.AreParametersInitialized; }
        }

        bool _IntermediateConstructorSignatureMember.IsDisposed
        {
            get { return this.IsDisposed; }
        }

        bool _IntermediateConstructorSignatureMember.IsTypeInitializer
        {
            get { return this.typeInitializer; }
        }
    }
    internal interface _IntermediateConstructorSignatureMember
    {
        bool IsDisposed { get; }
        bool IsTypeInitializer { get; }
        bool AreParametersInitialized { get; }
    }
}
