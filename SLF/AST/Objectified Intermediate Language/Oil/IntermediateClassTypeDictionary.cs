using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
using System.Diagnostics;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    [DebuggerDisplay("Classes: {Count}")]
    public class IntermediateClassTypeDictionary :
        IntermediateTypeDictionary<IClassType, IIntermediateClassType>,
        IIntermediateClassTypeDictionary
    {
        public IntermediateClassTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master)
            : base(parent, master)
        {
        }
        public IntermediateClassTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master, IntermediateClassTypeDictionary root)
            : base(parent, master, root)
        {
        }


        #region IClassTypeDictionary Members

        ITypeParent IClassTypeDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        public override IIntermediateClassType Add(string name)
        {
            IntermediateClassType ict = new IntermediateClassType(name, this.Parent);
            base.Add(ict.UniqueIdentifier, ict);
            return ict;
        }
    }
}
