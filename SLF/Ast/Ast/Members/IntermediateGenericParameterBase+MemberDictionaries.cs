using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    partial class IntermediateGenericParameterBase<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
        where TGenericParameter :
            class,
            IGenericParameter<TGenericParameter, TParent>
        where TIntermediateGenericParameter :
            class,
            IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>,
            TGenericParameter
        where TParent :
            IGenericParamParent<TGenericParameter, TParent>
        where TIntermediateParent :
            class,
            IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>,
            TParent
    {
        /// <summary>
        /// Provides a constructors dictionary for the generic parameter.
        /// </summary>
        public class ConstructorMemberDictionary :
            IntermediateConstructorSignatureMemberDictionary<IGenericParameterConstructorMember<TGenericParameter>, IIntermediateGenericParameterConstructorMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterConstructorMemberDictionary<TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterConstructorMemberDictionary
        {
            /// <summary>
            /// Creates a new <see cref="ConstructorMemberDictionary"/> with the 
            /// <paramref name="master"/> and <paramref name="parent"/>
            /// provided.
            /// </summary>
            /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
            /// which contains the other members of the <typeparamref name="TIntermediateGenericParameter"/>.</param>
            /// <param name="parent">The parent which contains the <see cref="ConstructorMemberDictionary"/>.</param>
            public ConstructorMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateGenericParameter parent) 
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

            protected override IIntermediateGenericParameterConstructorMember<TGenericParameter, TIntermediateGenericParameter> GetConstructor()
            {
                return new ConstructorMember(this.Parent);
            }
        }

        /// <summary>
        /// Provides an events dictionary for the generic parameter.
        /// </summary>
        public class EventMemberDictionary :
            IntermediateEventSignatureMemberDictionary<IGenericParameterEventMember<TGenericParameter>, IIntermediateGenericParameterEventMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterEventMemberDictionary<TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterEventMemberDictionary
        {
            /// <summary>
            /// Creates a new <see cref="EventMemberDictionary"/> with the 
            /// <paramref name="master"/> and <paramref name="parent"/>
            /// provided.
            /// </summary>
            /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
            /// which contains the other members of the <typeparamref name="TIntermediateGenericParameter"/>.</param>
            /// <param name="parent">The parent which contains the <see cref="EventMemberDictionary"/>.</param>
            public EventMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateGenericParameter parent) 
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
        public class IndexerMemberDictionary :
            IntermediateIndexerSignatureMemberDictionary<IGenericParameterIndexerMember<TGenericParameter>, IIntermediateGenericParameterIndexerMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterIndexerMemberDictionary<TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterIndexerMemberDictionary
        {
            /// <summary>
            /// Creates a new <see cref="IndexerMemberDictionary"/> with the 
            /// <paramref name="master"/> and <paramref name="parent"/>
            /// provided.
            /// </summary>
            /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
            /// which contains the other members of the <typeparamref name="TIntermediateGenericParameter"/>.</param>
            /// <param name="parent">The parent which contains the <see cref="IndexerMemberDictionary"/>.</param>
            public IndexerMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateGenericParameter parent) 
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

            protected override IIntermediateGenericParameterIndexerMember<TGenericParameter, TIntermediateGenericParameter> GetNew(TypedName nameAndReturn, TypedNameSeries parameters, bool canGet = true, bool canSet = true)
            {
                var result = new IndexerMember(nameAndReturn.Name, this.Parent, this.Parent.IdentityManager);
                if (parameters.Count > 0)
                    result.Parameters.AddRange(parameters.ToArray());
                result.PropertyType = nameAndReturn.TypeReference;
                result.CanRead = canGet;
                result.CanWrite = canSet;
                return result;
            }
        }

        public class MethodMemberDictionary :
            IntermediateMethodSignatureMemberDictionary<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterMethodMemberDictionary<TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterMethodMemberDictionary
        {
            /// <summary>
            /// Creates a new <see cref="MethodMemberDictionary"/> with the 
            /// <paramref name="master"/> and <paramref name="parent"/>
            /// provided.
            /// </summary>
            /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
            /// which contains the other members of the <typeparamref name="TIntermediateGenericParameter"/>.</param>
            /// <param name="parent">The parent which contains the <see cref="MethodMemberDictionary"/>.</param>
            public MethodMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateGenericParameter parent) 
                : base(master, parent, parent.IdentityManager)
            {
            }

            #region IIntermediateGenericParameterMethodMemberDictionary Members

            IIntermediateGenericParameter IIntermediateGenericParameterMethodMemberDictionary.Parent
            {
                get { return base.Parent; }
            }

            #endregion

            #region IGenericParameterMethodMemberDictionary Members

            IGenericParameter IGenericParameterMethodMemberDictionary.Parent
            {
                get { return base.Parent; }
            }

            #endregion

            protected override IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter> OnGetNewMethod(string name)
            {
                return new MethodMember(name, this.Parent);
            }
        }

        public class PropertyMemberDictionary :
            IntermediatePropertySignatureMemberDictionary<IGenericParameterPropertyMember<TGenericParameter>, IIntermediateGenericParameterPropertyMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterPropertyMemberDictionary<TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterPropertyMemberDictionary
        {
            /// <summary>
            /// Creates a new <see cref="PropertyMemberDictionary"/> with the 
            /// <paramref name="master"/> and <paramref name="parent"/>
            /// provided.
            /// </summary>
            /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
            /// which contains the other members of the <typeparamref name="TIntermediateGenericParameter"/>.</param>
            /// <param name="parent">The parent which contains the <see cref="PropertyMemberDictionary"/>.</param>
            public PropertyMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateGenericParameter parent)
                : base(master, parent)
            {
            }

            #region IIntermediateGenericParameterPropertyMemberDictionary Members

            IIntermediateGenericParameter IIntermediateGenericParameterPropertyMemberDictionary.Parent
            {
                get { return base.Parent; }
            }

            #endregion

            #region IGenericParameterPropertyMemberDictionary Members

            IGenericParameter IGenericParameterPropertyMemberDictionary.Parent
            {
                get { return base.Parent; }
            }

            #endregion

            protected override IIntermediateGenericParameterPropertyMember<TGenericParameter, TIntermediateGenericParameter> OnGetProperty(TypedName nameAndType)
            {
                var result = new PropertyMember(nameAndType.Name, this.Parent);
                result.PropertyType = nameAndType.TypeReference;
                return result;
            }
        }
    }
}
