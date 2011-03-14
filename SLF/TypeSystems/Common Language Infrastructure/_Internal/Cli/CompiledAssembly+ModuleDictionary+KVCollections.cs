using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledAssembly
    {
        partial class ModuleDictionary
        {
            private sealed class KeysCollection :
                ControlledStateDictionary<string, IModule>.KeysCollection
            {
                private ModuleDictionary owner;

                public KeysCollection(ModuleDictionary owner)
                    : base(owner)
                {
                    this.owner = owner;
                }

                public override int Count
                {
                    get
                    {
                        return this.owner.moduleData.Length;
                    }
                }
                public override bool Contains(string item)
                {
                    for (int i = 0; i < this.Count; i++)
                        if (this.owner.moduleData[i].Name == item)
                            return true;
                    return false;
                }

                public override int IndexOf(string key)
                {
                    for (int i = 0; i < this.Count; i++)
                        if (this.owner.moduleData[i].Name == key)
                            return i;
                    return -1;
                }

                protected override string OnGetKey(int index)
                {
                    if (index < 0 || index >= this.Count)
                        throw new ArgumentOutOfRangeException("index");
                    return this.owner.moduleData[index].Name;
                }

                public override void CopyTo(string[] array, int arrayIndex = 0)
                {
                    if (this.Count == 0)
                        return;
                    if (arrayIndex < 0 || arrayIndex >= array.Length)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    if (this.Count + arrayIndex > array.Length)
                        throw new ArgumentException("array");
                    for (int i = 0; i < this.Count; i++)
                        array[i + arrayIndex] = this.owner.moduleData[i].Name;
                }

                public override string[] ToArray()
                {
                    string[] result = new String[this.Count];
                    this.CopyTo(result);
                    return result;
                }

                public override IEnumerator<string> GetEnumerator()
                {
                    foreach (var module in this.owner.moduleData)
                        yield return module.Name;
                }

                protected override void ICollection_CopyTo(Array array, int arrayIndex)
                {
                    if (this.Count == 0)
                        return;
                    if (arrayIndex < 0 || arrayIndex >= array.Length)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    if (this.Count + arrayIndex > array.Length)
                        throw new ArgumentException("array");
                    for (int i = 0; i < this.Count; i++)
                        array.SetValue(this.owner.moduleData[i].Name, i + arrayIndex);
                }
            }

            private sealed class ValuesCollection :
                ControlledStateDictionary<string, IModule>.ValuesCollection
            {
                private ModuleDictionary owner;

                public ValuesCollection(ModuleDictionary owner)
                    : base(owner)
                {
                    this.owner = owner;
                }

                public override int Count
                {
                    get
                    {
                        return this.owner.moduleData.Length;
                    }
                }

                public override bool Contains(IModule item)
                {
                    if (!this.owner.Keys.Contains(item.Name))
                        return false;
                    for (int i = 0; i < this.Count; i++)
                        if (this.owner.moduleCopy[i] != null && 
                            this.owner.moduleCopy[i] == item)
                            return true;
                    return false;
                }

                public override void CopyTo(IModule[] array, int arrayIndex = 0)
                {
                    if (this.Count == 0)
                        return;
                    if (arrayIndex < 0 || arrayIndex >= array.Length)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    if (this.Count + arrayIndex > array.Length)
                        throw new ArgumentException("array");
                    for (int i = 0; i < this.Count; i++)
                    {
                        this.owner.CheckItemAt(i);
                        array[i + arrayIndex] = this.owner.moduleCopy[i];
                    }
                }

                public override int IndexOf(IModule element)
                {
                    if (!this.owner.Keys.Contains(element.Name))
                        return -1;
                    for (int i = 0; i < this.Count; i++)
                        if (this.owner.moduleCopy[i] != null &&
                            this.owner.moduleCopy[i] == element)
                            return i;
                    return -1;
                }

                public override IEnumerator<IModule> GetEnumerator()
                {
                    if (this.Count == 0)
                        yield break;
                    for (int i = 0; i < this.Count; i++)
                    {
                        this.owner.CheckItemAt(i);
                        yield return this.owner.moduleCopy[i];
                    }
                }

                protected override IModule OnGetThis(int index)
                {
                    if (index < 0 || index >= this.Count)
                        throw new ArgumentOutOfRangeException("index");
                    this.owner.CheckItemAt(index);
                    return this.owner.moduleCopy[index];
                }

                public override IModule[] ToArray()
                {
                    IModule[] result = new IModule[this.Count];
                    this.CopyTo(result);
                    return result;
                }

                protected override void ICollection_CopyTo(Array array, int arrayIndex)
                {
                    if (this.Count == 0)
                        return;
                    if (arrayIndex < 0 || arrayIndex >= array.Length)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    if (this.Count + arrayIndex > array.Length)
                        throw new ArgumentException("array");
                    for (int i = 0; i < this.Count; i++)
                    {
                        this.owner.CheckItemAt(i);
                        array.SetValue(this.owner.moduleCopy[i], i + arrayIndex);
                    }
                }
            }
        }
    }
}
