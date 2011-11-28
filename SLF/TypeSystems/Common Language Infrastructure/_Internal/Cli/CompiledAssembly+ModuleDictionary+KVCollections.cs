using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Modules;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
            private new sealed class KeysCollection :
                ControlledDictionary<IGeneralDeclarationUniqueIdentifier, IModule>.KeysCollection
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
                        return this.owner.identifierCopy.Length;
                    }
                }
                public override bool Contains(IGeneralDeclarationUniqueIdentifier item)
                {
                    if (item == null)
                        throw new ArgumentNullException(ThrowHelper.GetArgumentName(ArgumentWithException.item));
                    for (int i = 0; i < this.Count; i++)
                        if (this.owner.identifierCopy[i].Equals(item))
                            return true;
                    return false;
                }

                public override int IndexOf(IGeneralDeclarationUniqueIdentifier key)
                {
                    if (key == null)
                        throw new ArgumentNullException(ThrowHelper.GetArgumentName(ArgumentWithException.key));
                    for (int i = 0; i < this.Count; i++)
                        if (this.owner.identifierCopy[i].Equals(key))
                            return i;
                    return -1;
                }

                protected override IGeneralDeclarationUniqueIdentifier OnGetKey(int index)
                {
                    if (index < 0 || index >= this.Count)
                        throw new ArgumentOutOfRangeException("index");
                    return this.owner.identifierCopy[index];
                }

                public override void CopyTo(IGeneralDeclarationUniqueIdentifier[] array, int arrayIndex = 0)
                {
                    if (this.Count == 0)
                        return;
                    if (arrayIndex < 0 || this.Count + arrayIndex > array.Length)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    for (int i = 0; i < this.Count; i++)
                        array[i + arrayIndex] = this.owner.identifierCopy[i];
                }

                public override IGeneralDeclarationUniqueIdentifier[] ToArray()
                {
                    IGeneralDeclarationUniqueIdentifier[] result = new IGeneralDeclarationUniqueIdentifier[this.Count];
                    this.CopyTo(result);
                    return result;
                }

                public override IEnumerator<IGeneralDeclarationUniqueIdentifier> GetEnumerator()
                {
                    foreach (var moduleIdentifier in this.owner.identifierCopy)
                        yield return moduleIdentifier;
                }

                protected override void GeneralCopyTo(Array array, int arrayIndex)
                {
                    if (this.Count == 0)
                        return;
                    if (arrayIndex < 0 || this.Count + arrayIndex > array.Length)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    for (int i = 0; i < this.Count; i++)
                        array.SetValue(this.owner.moduleData[i].Name, i + arrayIndex);
                }
            }

            private new sealed class ValuesCollection :
                ControlledDictionary<IGeneralDeclarationUniqueIdentifier, IModule>.ValuesCollection
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
                    if (item == null)
                        throw new ArgumentNullException(ThrowHelper.GetArgumentName(ArgumentWithException.item));
                    if (!this.owner.Keys.Contains(item.UniqueIdentifier))
                        return false;
                    var iCompiledItem = item as ICompiledModule;
                    CompiledModule compiledItem;
                    Module cliModule;
                    if (iCompiledItem != null)
                    {
                        compiledItem = iCompiledItem as CompiledModule;
                        cliModule = iCompiledItem.UnderlyingModule;
                    }
                    else
                    {
                        compiledItem = null;
                        cliModule = null;
                    }
                    for (int i = 0; i < this.Count; i++)
                    {
                        if (this.owner.moduleCopy[i] != null)
                        {
                            if (this.owner.moduleCopy[i] == item)
                                return true;
                            else if (this.owner.moduleData[i] == cliModule)
                                return true;
                        }
                        else
                        {
                            if (this.owner.moduleData[i] == cliModule)
                            {
                                if (compiledItem != null)
                                    this.owner.moduleCopy[i] = compiledItem;
                                return true;
                            }
                        }
                    }
                    return false;
                }

                public override void CopyTo(IModule[] array, int arrayIndex = 0)
                {
                    if (this.Count == 0)
                        return;
                    if (arrayIndex < 0 || this.Count + arrayIndex > array.Length)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    if (this.Count + arrayIndex > array.Length)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    for (int i = 0; i < this.Count; i++)
                    {
                        this.owner.CheckItemAt(i);
                        array[i + arrayIndex] = this.owner.moduleCopy[i];
                    }
                }

                public override int IndexOf(IModule item)
                {
                    if (!this.owner.Keys.Contains(item.UniqueIdentifier))
                        return -1;
                    int index = this.owner.Keys.IndexOf(item.UniqueIdentifier);
                    for (int i = 0; i < this.Count; i++)
                        if (this.owner.moduleCopy[i] != null &&
                            this.owner.moduleCopy[i] == item)
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

                protected override IModule OnGetValue(int index)
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

                protected override void GeneralCopyTo(Array array, int arrayIndex)
                {
                    if (this.Count == 0)
                        return;
                    if (arrayIndex < 0 || this.Count + arrayIndex > array.Length)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    if (this.Count + arrayIndex > array.Length)
                        throw new ArgumentOutOfRangeException("arrayIndex");
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
