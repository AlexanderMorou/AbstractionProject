using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledNamespaceDeclarations
    {
        private class _KeysCollection :
            KeysCollection
        {
            private IGeneralDeclarationUniqueIdentifier[] identifiers;
            private CompiledNamespaceDeclarations parent;
            public _KeysCollection(CompiledNamespaceDeclarations parent)
                : base(parent)
            {
                this.parent = parent;
                this.identifiers = new IGeneralDeclarationUniqueIdentifier[this.parent.baseData.Length];
            }

            public override int Count
            {
                get
                {
                    return this.parent.baseData.Length;
                }
            }

            protected override IGeneralDeclarationUniqueIdentifier OnGetKey(int index)
            {
                if (index < 0 || index >= this.parent.baseData.Length)
                    throw new ArgumentOutOfRangeException("index");
                this.CheckItemAt(index);
                return this.identifiers[index];
            }

            public override bool Contains(IGeneralDeclarationUniqueIdentifier item)
            {
                if (item == null)
                    throw new ArgumentNullException("item");
                return this.parent.baseData.Contains(item.Name);
            }

            public override IGeneralDeclarationUniqueIdentifier[] ToArray()
            {
                /* *
                 * Array references pass the original reference, 
                 * right?
                 * *
                 * That would be bad in this case.
                 * */
                var r = new IGeneralDeclarationUniqueIdentifier[this.parent.baseData.Length];
                this.CopyTo(r, 0);
                return r;
            }

            public override IEnumerator<IGeneralDeclarationUniqueIdentifier> GetEnumerator()
            {
                for (int i = 0, c=this.Count; i < c; i++)
                {
                    this.CheckItemAt(i);
                    yield return this.identifiers[i];
                }
            }

            private void CheckItemAt(int index)
            {
                if (this.identifiers[index] == null)
                    this.identifiers[index] = AstIdentifier.Declaration(this.parent.baseData[index]);
            }

            public override void CopyTo(IGeneralDeclarationUniqueIdentifier[] array, int arrayIndex)
            {
                this.parent.baseData.CopyTo(array, arrayIndex);
            }
            
        }
    }
}
