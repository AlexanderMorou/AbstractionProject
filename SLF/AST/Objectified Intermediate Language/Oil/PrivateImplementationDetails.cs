using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using System.Globalization;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
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
        internal PrivateImplementationDetails(IIntermediateAssembly parent)
            : base(parent)
        {
            DetailGuid = Guid.NewGuid();
        }
        protected PrivateImplementationDetails(PrivateImplementationDetails root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }

        public Guid DetailGuid { get; set; }

        protected override string OnGetName()
        {
            return string.Format(CultureInfo.CurrentCulture, "<PrivateImplementationDetails>{{{0}}}", DetailGuid);
        }

        #region IPrivateImplementationDetails Members

        public IAnonymousTypeCollection AnonymousTypes
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
