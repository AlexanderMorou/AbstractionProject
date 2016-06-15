using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Compilers;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public partial class AnonymousType :
        IntermediateClassType<AnonymousType>,
        IAnonymousType
    {
        /// <summary>
        /// Data member containing the members of the <see cref="AnonymousType"/>.
        /// </summary>
        private AnonymousTypeMember[] members;
        /// <summary>
        /// Data member which correlates to the order in which
        /// the <see cref="AnonymousType"/> was created in relation
        /// to the others for the parent intermediate assembly.
        /// </summary>
        private int index;

        internal AnonymousType(IIntermediateAssembly parent, params AnonymousTypeMember[] members)
            : base(parent.PrivateImplementationDetails)
        {
            this.index = parent.PrivateImplementationDetails.AnonymousTypes.Count;
            this.members = members;
            this.Lock();
        }

        #region IAnonymousType Members

        /// <summary>
        /// Returns the number of members in the <see cref="AnonymousType"/>.
        /// </summary>
        public int MemberCount
        {
            get { return members.Length; }
        }

        /// <summary>
        /// Returns the number which correlates to the
        /// order in which the <see cref="AnonymousType"/>
        /// was made in relation to the others.
        /// </summary>
        public int Index
        {
            get { return this.index; }
        }

        #endregion

        protected override IntermediateGenericSegmentableInstantiableType<Abstract.Members.IClassCtorMember, Members.IIntermediateClassCtorMember, Abstract.Members.IClassEventMember, Members.IIntermediateClassEventMember, IntermediateClassEventMember<AnonymousType>.EventMethodMember, Abstract.Members.IClassFieldMember, Members.IIntermediateClassFieldMember, Abstract.Members.IClassIndexerMember, Members.IIntermediateClassIndexerMember, IntermediateClassIndexerMember<AnonymousType>.IndexerMethodMember, Abstract.Members.IClassMethodMember, Members.IIntermediateClassMethodMember, Abstract.Members.IClassPropertyMember, Members.IIntermediateClassPropertyMember, IntermediateClassPropertyMember<AnonymousType>.PropertyMethodMember, IClassType, IIntermediateClassType, AnonymousType>.PropertyDictionary InitializeProperties()
        {
            var result = base.InitializeProperties();
            
            return result;
        }

        protected override IntermediateGenericTypeBase<IGeneralGenericTypeUniqueIdentifier, IClassType, IIntermediateClassType>.TypeParameterDictionary InitializeTypeParameters()
        {
            var result = base.InitializeTypeParameters();
            /* *
             * No need to worry about the lock placed upon
             * the the anonymous type's members: initialization
             * preceeds the lock placed, since the reference obtained
             * through the above means ignores the locked
             * state.  It's only after the initialization is complete that
             * the lock is placed.
             * */
            result.AddRange((from i in this.MemberCount.Range()
                             select new GenericParameterData(string.Format("T{0}", members[i].Name))).ToArray());
            return result;
        }

        protected override AnonymousType GetNewPartial(AnonymousType root, IIntermediateTypeParent parent)
        {
            return null;
        }
    }
}
