using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledNamespaceDeclarations
    {
        private class _KeysCollection :
            ControlledStateDictionary<string, INamespaceDeclaration>.KeysCollection
        {
            private CompiledNamespaceDeclarations parent;
            public _KeysCollection(CompiledNamespaceDeclarations parent)
                : base(parent)
            {
                this.parent = parent;
            }

            public override int Count
            {
                get
                {
                    return this.parent.baseData.Length;
                }
            }

            protected override string OnGetKey(int index)
            {
                if (index < 0 || index >= this.parent.baseData.Length)
                    throw new ArgumentOutOfRangeException("index");
                return this.parent.baseData[index];
            }

            public override bool Contains(string item)
            {
                return this.parent.baseData.Contains(item);
            }

            public override string[] ToArray()
            {
                /* *
                 * Array references pass the original reference, 
                 * right?
                 * *
                 * That would be bad in this case.
                 * */
                var r = new string[this.parent.baseData.Length];
                this.CopyTo(r, 0);
                return r;
            }

            public override IEnumerator<string> GetEnumerator()
            {
                foreach (string s in this.parent.baseData)
                    yield return s;
                yield break;
            }
            public override void CopyTo(string[] array, int arrayIndex)
            {
                this.parent.baseData.CopyTo(array, arrayIndex);
            }
            
        }
    }
}
