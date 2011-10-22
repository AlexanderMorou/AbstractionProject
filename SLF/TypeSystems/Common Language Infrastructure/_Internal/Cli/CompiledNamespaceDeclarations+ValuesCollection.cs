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
        private class _ValuesCollection :
            ValuesCollection
        {
            private CompiledNamespaceDeclaration[] dataCopy;
            private CompiledNamespaceDeclarations parent;
            internal _ValuesCollection(CompiledNamespaceDeclarations parent)
                : base(parent)
            {
                this.parent = parent;
                this.dataCopy = new CompiledNamespaceDeclaration[this.parent.baseData.Length];
            }

            public override bool Contains(INamespaceDeclaration item)
            {
                foreach (var v in dataCopy)
                {
                    if (v == null)
                        continue;
                    else if (v == item)
                        return true;
                }
                return false;
            }
            public override int Count
            {
                get
                {
                    return this.dataCopy.Length;
                }
            }

            public override INamespaceDeclaration[] ToArray()
            {
                INamespaceDeclaration[] result = new INamespaceDeclaration[this.dataCopy.Length];
                for (int i = 0; i < this.dataCopy.Length; i++)
                {
                    CheckLoadStatus(i);
                    result[i] = this.dataCopy[i];
                    
                }
                return result;
            }

            protected override INamespaceDeclaration OnGetValue(int index)
            {
                if (index < 0 || index > this.dataCopy.Length)
                    throw new ArgumentOutOfRangeException("index");
                CheckLoadStatus(index);
                return this.dataCopy[index];
            }

            public override IEnumerator<INamespaceDeclaration> GetEnumerator()
            {
                for (int i = 0; i < this.dataCopy.Length; i++)
                {
                    this.CheckLoadStatus(i);
                    yield return this.dataCopy[i];
                }
                yield break;
            }
            private void CheckLoadStatus(int i)
            {
                if (this.dataCopy[i] == null)
                    this.dataCopy[i] = new CompiledNamespaceDeclaration(this.parent.baseData[i], this.parent.Parent);
            }

            internal void Dispose()
            {
                for (int i = 0; i < this.Count; i++)
                    if (this.dataCopy[i] != null)
                    {
                        this.dataCopy[i].Dispose();
                        this.dataCopy[i] = null;
                    }
            }
        }
    }
}
