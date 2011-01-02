using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
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

        protected override AnonymousType GetNewPartial(AnonymousType root, IIntermediateTypeParent parent)
        {
            throw new NotSupportedException();
        }

        protected override IntermediateFullTypeDictionary InitializeTypes()
        {
            throw new NotSupportedException();
        }

    }
}
