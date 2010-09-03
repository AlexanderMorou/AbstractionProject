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
    [DebuggerDisplay("Interfaces: {Count}")]
    public class IntermediateInterfaceTypeDictionary :
        IntermediateGenericTypeDictionary<IInterfaceType, IIntermediateInterfaceType>,
        IIntermediateInterfaceTypeDictionary
    {
        public IntermediateInterfaceTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master)
            : base(parent, master)
        {
        }
        public IntermediateInterfaceTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master, IntermediateInterfaceTypeDictionary root)
            : base(parent, master, root)
        {
        }
        #region IInterfaceTypeDictionary Members

        ITypeParent IInterfaceTypeDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IDeclarationDictionary<IInterfaceType> Members

        public new int IndexOf(IInterfaceType decl)
        {
            if (this.valuesCollection == null)
                return -1;
            int index = 0;
            foreach (var item in this.Values)
                if (item == decl)
                    return index;
                else
                    index++;
            return -1;
        }

        #endregion

        public override IIntermediateInterfaceType Add(string name)
        {
            var result = new IntermediateInterfaceType(name, this.Parent);
            base.Add(result.UniqueIdentifier, result);
            return result;
        }
    }
}
