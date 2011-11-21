using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
    /// <typeparam name="TProperty">The type of the property member
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TEvent">The type of the event member in the
    /// abstract type system.</typeparam>
    /// <typeparam name="TIndexer">The type of the indexer member
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TParent">The type of parent which contains
    /// the concrete members in the abstract type system.</typeparam>
    public interface IInterfaceMemberMapping<TMethod, TProperty, TEvent, TIndexer, TParent>
        where TMethod :
            IMethodMember<TMethod, TParent>,
            IExtendedInstanceMember
        where TProperty :
            IPropertyMember<TProperty, TParent>
        where TIndexer :
            IIndexerMember<TIndexer, TParent>
        where TEvent :
            IEventMember<TEvent, TParent>,
            IExtendedInstanceMember
        where TParent :
            IEventParent<TEvent, TParent>,
            IMethodParent<TMethod, TParent>,
            IPropertyParent<TProperty, TParent>,
            IIndexerParent<TIndexer, TParent>
    {
        /// <summary>
        /// Returns the <see cref="IInterfaceType"/> which is represented by
        /// the current mapping.
        /// </summary>
        IInterfaceType Target { get; }

        /// <summary>
        /// Returns the list of implemented properties with
        /// a link back to the original interface member.
        /// </summary>
        IEnumerable<MemberMap<IGeneralMemberUniqueIdentifier, TProperty, IInterfacePropertyMember>> Properties { get; }
        /// <summary>
        /// Returns the list of implemented methods
        /// with a link back to the original interface 
        /// member.
        /// </summary>
        IEnumerable<MemberMap<IGeneralGenericSignatureMemberUniqueIdentifier, TMethod, IInterfaceMethodMember>> Methods { get; }
        /// <summary>
        /// Returns the list of implemented indexers with
        /// a link back to the original interface member.
        /// </summary>
        IEnumerable<MemberMap<IGeneralSignatureMemberUniqueIdentifier, TIndexer, IInterfaceIndexerMember>> Indexers { get; }
        /// <summary>
        /// Returns the list of implemented indexers with
        /// a link back to the original interface member.
        /// </summary>
        IEnumerable<MemberMap<IGeneralSignatureMemberUniqueIdentifier, TEvent, IInterfaceEventMember>> Events { get; }
    }
    
    /// <summary>
    /// Provides a member mapping from the signature variety to the
    /// implementation variety.
    /// </summary>
    /// <typeparam name="TA">The type for the implementation of the 
    /// <typeparamref name="TB"/> member.</typeparam>
    /// <typeparam name="TB">The type for the signature variant of the
    /// member.</typeparam>
    public struct MemberMap<TIdentifier, TA, TB>
        where TA :
            IExtendedInstanceMember
        where TB :
            IMember
        where TIdentifier :
            IMemberUniqueIdentifier
    {
        private TA _implementedMember;
        private TB _interfaceMember;
        private TIdentifier _identifier;

        internal MemberMap(TIdentifier identifier, TA _implementedMember, TB _interfaceMember)
        {
            this._interfaceMember = _interfaceMember;
            this._implementedMember = _implementedMember;
            this._identifier = identifier;
        }
        /// <summary>
        /// Returns the <typeparamref name="TIdentifier"/> which represents the
        /// unique id that represents both the <see cref="InterfaceMember"/>
        /// and the <see cref="ImplementedMember"/>.
        /// </summary>
        TIdentifier Identifier { get { return this._identifier; } }
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
