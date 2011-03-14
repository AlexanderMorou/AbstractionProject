using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    internal abstract class EventSignatureMemberBase<TEvent, TEventParameter, TEventParent> :
        SignatureMemberBase<TEvent, TEventParameter, TEventParent>,
        IEventSignatureMember<TEvent, TEventParameter, TEventParent>,
        IEventSignatureMember
        where TEvent :
            IEventSignatureMember<TEvent, TEventParameter, TEventParent>
        where TEventParameter :
            IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
        where TEventParent :
            IEventSignatureParent<TEvent, TEventParameter, TEventParent>
    {
        /// <summary>
        /// Creates a new <see cref="EventSignatureMemberBase{TEvent, TEventParameter, TEventParent}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The parent of the 
        /// <see cref="EventSignatureMemberBase{TEvent, TEventParameter, TEventParent}"/>.</param>
        public EventSignatureMemberBase(TEventParent parent)
            : base(parent)
        {
        }

        #region IEventSignatureMember Members


        /// <summary>
        /// Returns the <see cref="EventSignatureSource"/> which
        /// designates where the <see cref="IEventMember"/>'s signature 
        /// is sourced.
        /// </summary>
        public EventSignatureSource SignatureSource
        {
            get { return this.SignatureSourceImpl; }
        }

        /// <summary>
        /// Returns the <see cref="IDelegateType"/> from 
        /// which the <see cref="IEventMember"/>'s 
        /// signature is sourced.
        /// </summary>
        /// <remarks><para>An <see cref="IEventMember"/>'s
        /// <see cref="SignatureType"/> can be an auto-generated
        /// type.</para>
        /// <para>If the <see cref="SignatureType"/> is generated,
        /// then the type yielded may or may not be emitted
        /// based upon the specifics of the target 
        /// translation language.</para><para>The primary reason for this is:
        /// Visual Basic can auto-generate the associated type, where as
        /// C&#9839; requires the signature to be a type already defined.  
        /// C&#9839; will emit an auto-generated type upon translation if
        /// <see cref="SignatureSource"/> is <see cref="EventSignatureSource.Declared"/> and
        /// Visual Basic will not.</para></remarks>
        public IDelegateType SignatureType
        {
            get { return this.SignatureTypeImpl; }
        }

        public abstract IType ReturnType { get; }

        #endregion

        #region Abstract Implementation Members
        /// <summary>
        /// Implementation form of <see cref="SignatureSource"/>.
        /// </summary>
        /// <remarks>Used to enable first-generation inheritors
        /// to hide the <see cref="SignatureSource"/> property.</remarks>
        protected abstract EventSignatureSource SignatureSourceImpl { get; }
        /// <summary>
        /// Implementation form of <see cref="SignatureType"/>
        /// </summary>
        /// <remarks>Used to enable first-generation inheritors
        /// to hide the <see cref="SignatureType"/> property.</remarks>
        protected abstract IDelegateType SignatureTypeImpl { get; }
        #endregion


    }
}
