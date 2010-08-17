using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Diagnostics;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    [DebuggerDisplay("Enumerations: {Count}")]
    public class IntermediateEnumTypeDictionary :
        IntermediateTypeDictionary<IEnumType, IIntermediateEnumType>,
        IIntermediateEnumTypeDictionary
    {
        public IntermediateEnumTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master)
            : base(parent, master)
        {
        }
        public IntermediateEnumTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master, IntermediateEnumTypeDictionary root)
            : base(parent, master, root)
        {
        }
        #region IEnumTypeDictionary Members

        ITypeParent IEnumTypeDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion


        public override IIntermediateEnumType Add(string name)
        {
            var result = new IntermediateEnumType(name, this.Parent);
            base.Add(result.UniqueIdentifier, result);
            return result;
        }

    }
}
