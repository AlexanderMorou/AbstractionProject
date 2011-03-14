using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public class PrivateImplementationDetails :
        IntermediateClassType<PrivateImplementationDetails>,
        IPrivateImplementationDetails
    {
        private Guid detailGuid;
        internal PrivateImplementationDetails(IIntermediateAssembly parent)
            : base(parent)
        {
            DetailGuid = Guid.NewGuid();
        }
        protected PrivateImplementationDetails(PrivateImplementationDetails root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }

        public Guid DetailGuid
        {
            get
            {
                if (this.IsRoot)
                    return this.detailGuid;
                else
                    return this.GetRoot().DetailGuid;
            }
            set
            {
                if (this.IsRoot)
                    this.detailGuid = value;
                else
                    this.GetRoot().DetailGuid = value;
            }
        }

        protected override string OnGetName()
        {
            return string.Format(CultureInfo.CurrentCulture, "<PrivateImplementationDetails>{{{0}}}", DetailGuid);
        }

        #region IPrivateImplementationDetails Members

        public IAnonymousTypeDictionary AnonymousTypes
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        protected override PrivateImplementationDetails GetNewPartial(PrivateImplementationDetails root, IIntermediateTypeParent parent)
        {
            return new PrivateImplementationDetails(root, parent);
        }
    }
}
