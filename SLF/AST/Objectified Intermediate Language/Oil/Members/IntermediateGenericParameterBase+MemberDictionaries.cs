using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    partial class IntermediateGenericParameterBase<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
        where TGenericParameter :
            class,
            IGenericParameter<TGenericParameter, TParent>
        where TIntermediateGenericParameter :
            IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>,
            TGenericParameter
        where TParent :
            IGenericParamParent<TGenericParameter, TParent>
        where TIntermediateParent :
            IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>,
            TParent
    {
        /// <summary>
        /// Provides a constructors dictionary for the generic parameter.
        /// </summary>
        public class ConstructorsDictionary :
            IntermediateConstructorSignatureMemberDictionary<IGenericParameterConstructorMember<TGenericParameter>, IIntermediateGenericParameterConstructorMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterConstructorMemberDictionary<TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterConstructorMemberDictionary
        {
            /// <summary>
            /// Creates a new <see cref="ConstructorsDictionary"/> with the 
            /// <paramref name="master"/> and <paramref name="parent"/>
            /// provided.
            /// </summary>
            /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
            /// which contains the other members of the <typeparamref name="TIntermediateGenericParameter"/>.</param>
            /// <param name="parent">The parent which contains the <see cref="ConstructorsDictionary"/>.</param>
            public ConstructorsDictionary(IntermediateFullMemberDictionary master, TIntermediateGenericParameter parent) 
                : base(master, parent)
            {

            }

            #region IIntermediateGenericParameterConstructorMemberDictionary Members

            IIntermediateGenericParameter IIntermediateGenericParameterConstructorMemberDictionary.Parent
            {
                get { return base.Parent; }
            }

            #endregion

            #region IGenericParameterConstructorMemberDictionary Members

            IGenericParameter IGenericParameterConstructorMemberDictionary.Parent
            {
                get { return base.Parent; }
            }

            #endregion

            protected override IIntermediateGenericParameterConstructorMember<TGenericParameter, TIntermediateGenericParameter> GetConstructor(TypedNameSeries parameters)
            {
                ConstructorMember k = new IntermediateGenericParameterBase<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>.ConstructorMember(this.Parent);
                foreach (TypedName u in parameters)
                    k.Parameters.Add(u.Name, u.Reference, u.Direction);
                return k;
            }
        }

        /// <summary>
        /// Provides an events dictionary for the generic parameter.
        /// </summary>
        public class EventsDictionary :
            IntermediateEventSignatureMemberDictionary<IGenericParameterEventMember<TGenericParameter>, IIntermediateGenericParameterEventMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterEventMemberDictionary<TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterEventMemberDictionary
        {
            /// <summary>
            /// Creates a new <see cref="EventsDictionary"/> with the 
            /// <paramref name="master"/> and <paramref name="parent"/>
            /// provided.
            /// </summary>
            /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
            /// which contains the other members of the <typeparamref name="TIntermediateGenericParameter"/>.</param>
            /// <param name="parent">The parent which contains the <see cref="EventsDictionary"/>.</param>
            public EventsDictionary(IntermediateFullMemberDictionary master, TIntermediateGenericParameter parent) 
                : base(master, parent)
            {

            }

            #region IIntermediateGenericParameterConstructorMemberDictionary Members

            IIntermediateGenericParameter IIntermediateGenericParameterEventMemberDictionary.Parent
            {
                get { return base.Parent; }
            }

            #endregion

            #region IGenericParameterConstructorMemberDictionary Members

            IGenericParameter IGenericParameterEventMemberDictionary.Parent
            {
                get { return base.Parent; }
            }

            #endregion

            protected override IIntermediateGenericParameterEventMember<TGenericParameter, TIntermediateGenericParameter> GetEvent(TypedName typedName)
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// Provides an indexers dictionary for the generic parameter.
        /// </summary>
        public class IndexersDictionary :
            IntermediateIndexerSignatureMemberDictionary<IGenericParameterIndexerMember<TGenericParameter>, IIntermediateGenericParameterIndexerMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterIndexerMemberDictionary<TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterIndexerMemberDictionary
        {
            /// <summary>
            /// Creates a new <see cref="IndexersDictionary"/> with the 
            /// <paramref name="master"/> and <paramref name="parent"/>
            /// provided.
            /// </summary>
            /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
            /// which contains the other members of the <typeparamref name="TIntermediateGenericParameter"/>.</param>
            /// <param name="parent">The parent which contains the <see cref="IndexersDictionary"/>.</param>
            public IndexersDictionary(IntermediateFullMemberDictionary master, TIntermediateGenericParameter parent) 
                : base(master, parent)
            {
            }

            #region IIntermediateGenericParameterConstructorMemberDictionary Members

            IIntermediateGenericParameter IIntermediateGenericParameterIndexerMemberDictionary.Parent
            {
                get { return base.Parent; }
            }

            #endregion

            #region IGenericParameterConstructorMemberDictionary Members

            IGenericParameter IGenericParameterIndexerMemberDictionary.Parent
            {
                get { return base.Parent; }
            }

            #endregion
        }
    }
}
