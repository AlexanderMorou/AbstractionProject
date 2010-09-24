using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines properties and methods for working with an interface
    /// member mapping defined on a non-signatured type and a 
    /// signatured type.  The signature variants represent the
    /// interface members, the non signatures represent the 
    /// concrete implementation of the signature parent's members.
    /// </summary>
    /// <typeparam name="TMethod">The type of extended
    /// instance method member in the abstract type system.</typeparam>
    /// <typeparam name="TMethodSig">The type of the 
    /// method signature member in the abstract type system.</typeparam>
    /// <typeparam name="TProperty">The type of the property member
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TPropertySig">The type of the
    /// property signature member in the abstract type system.</typeparam>
    /// <typeparam name="TEvent">The type of the event member in the
    /// abstract type system.</typeparam>
    /// <typeparam name="TEventSig">The type of the event signature
    /// member in the abstract type system.</typeparam>
    /// <typeparam name="TIndexer">The type of the indexer member
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIndexerSig">The type of the indexer
    /// signature member in the abstract type system.</typeparam>
    /// <typeparam name="TParent">The type of parent which contains
    /// the concrete members in the abstract type system.</typeparam>
    /// <typeparam name="TParentSig">The type of parent which contains
    /// the signature members in the abstract type system.</typeparam>
    public interface IInterfaceMemberMapping<TMethod, TMethodSig, TProperty, TPropertySig, TEvent, TEventSig, TIndexer, TIndexerSig, TParent, TParentSig>
        where TMethod :
            IMethodMember<TMethod, TParent>,
            IExtendedInstanceMember
        where TMethodSig :
            IMethodSignatureMember<TMethodSig, TParentSig>
        where TProperty :
            IPropertyMember<TProperty, TParent>
        where TPropertySig :
            IPropertySignatureMember<TPropertySig, TParentSig>
        where TIndexer :
            IIndexerMember<TIndexer, TParent>
        where TIndexerSig :
            IIndexerSignatureMember<TIndexerSig,TParentSig>
        where TEvent :
            IEventMember<TEvent, TParent>,
            IExtendedInstanceMember
        where TEventSig :
            IEventSignatureMember<TEventSig, TParentSig>
        where TParent :
            IEventParent<TEvent, TParent>,
            IMethodParent<TMethod, TParent>,
            IPropertyParentType<TProperty, TParent>,
            IIndexerParent<TIndexer, TParent>
        where TParentSig :
            IEventSignatureParent<TEventSig, TParentSig>,
            IMethodSignatureParent<TMethodSig, TParentSig>,
            IPropertySignatureParentType<TPropertySig, TParentSig>,
            IIndexerSignatureParent<TIndexerSig, TParentSig>
    {
        /// <summary>
        /// Returns the list of implemented properties with
        /// a link back to the original interface member.
        /// </summary>
        IEnumerable<MemberMap<TProperty, TPropertySig>> Properties { get; }
        /// <summary>
        /// Returns the list of implemented methods
        /// with a link back to the original interface 
        /// member.
        /// </summary>
        IEnumerable<MemberMap<TMethod, TMethodSig>> Methods { get; }
        /// <summary>
        /// Returns the list of implemented indexers with
        /// a link back to the original interface member.
        /// </summary>
        IEnumerable<MemberMap<TIndexer, TIndexerSig>> Indexers { get; }
        /// <summary>
        /// Returns the list of implemented indexers with
        /// a link back to the original interface member.
        /// </summary>
        IEnumerable<MemberMap<TEvent, TEventSig>> Events { get; }
    }
    /// <summary>
    /// Provides a member mapping from the signature variety to the
    /// implementation variety.
    /// </summary>
    /// <typeparam name="TA">The type for the implementation of the 
    /// <typeparamref name="TB"/> member.</typeparam>
    /// <typeparam name="TB">The type for the signature variant of the
    /// member.</typeparam>
    public struct MemberMap<TA, TB>
        where TA :
            IExtendedInstanceMember
        where TB :
            IMember
    {
        private TA _implementedMember;
        private TB _interfaceMember;

        internal MemberMap(TA _implementedMember, TB _interfaceMember)
        {
            this._interfaceMember = _interfaceMember;
            this._implementedMember = _implementedMember;
        }
        /// <summary>
        /// Returns the <see cref="IMember"/> which
        /// is implemented by
        /// <see cref="ImplementedMember"/>.
        /// </summary>
        public TB InterfaceMember { get { return this._interfaceMember; } }
        /// <summary>
        /// Returns the <see cref="IExtendedInstanceMember"/>
        /// which is the implemented form of 
        /// <see cref="InterfaceMember"/>.
        /// </summary>
        public TA ImplementedMember { get { return this._implementedMember; } }
    }
}
