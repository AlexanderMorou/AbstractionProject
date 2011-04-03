using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides an intermediate full dictionary implementation.
    /// </summary>
    [DebuggerDisplay("Types: {Count}")]
    public class IntermediateFullTypeDictionary :
        IntermediateFullDeclarationDictionary<IType, IIntermediateType>,
        IIntermediateFullTypeDictionary
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateFullTypeDictionary"/> initialized
        /// to the default state.
        /// </summary>
        public IntermediateFullTypeDictionary(IIntermediateTypeParent parent)
            : base()
        {
            this.Parent = parent;
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateFullTypeDictionary"/> with the
        /// <paramref name="root"/> set provided.
        /// </summary>
        public IntermediateFullTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary root)
            : base(root)
        {
            this.Parent = parent;
        }

        #region IIntermediateFullTypeDictionary Members

        public IIntermediateTypeParent Parent { get; private set; }

        public void RemoveSoft(IIntermediateType type)
        {
            
        }

        public IIntermediateType Add(string name, TypeKind kind)
        {
            IIntermediateType rResult = null;
            switch (kind)
            {
                case TypeKind.Class:
                    {
                        var result = new IntermediateClassType(name, this.Parent);
                        rResult = result;
                        this.Parent.Classes.Add(result);
                    }
                    break;
                case TypeKind.Delegate:
                    {
                        var result = new IntermediateDelegateType(name, this.Parent);
                        rResult = result;
                        this.Parent.Delegates.Add(result);
                    }
                    break;
                case TypeKind.Enumerator:
                    {
                        var result = new IntermediateEnumType(name, this.Parent);
                        rResult = result;
                        this.Parent.Enums.Add(result);
                    }
                    break;
                case TypeKind.Interface:
                    {
                        var result = new IntermediateInterfaceType(name, this.Parent);
                        rResult = result;
                        this.Parent.Interfaces.Add(result);
                    }
                    break;
                case TypeKind.Struct:
                    {
                        var result = new IntermediateStructType(name, this.Parent);
                        rResult = result;
                        this.Parent.Structs.Add(result);
                    }
                    break;
                case TypeKind.Other:
                case TypeKind.Ambiguity:
                    break;
                default:
                    break;
            }
            if (rResult == null)
                throw new NotSupportedException();
            return rResult;
        }

        public IIntermediateGenericType Add(string name, TypeKindGeneric kind, params GenericParameterData[] genParamData)
        {
            if (genParamData == null)
                throw new ArgumentNullException("genParamData");
            if (name == null)
                throw new ArgumentNullException("name");
            IIntermediateGenericType rResult = null;
            switch (kind)
            {
                case TypeKindGeneric.Class:
                    {
                        var result = new IntermediateClassType(name, this.Parent);
                        if (genParamData.Length > 0)
                            result.TypeParameters.AddRange(genParamData);
                        rResult = result;
                        this.Parent.Classes.Add(result);
                    }
                    break;
                case TypeKindGeneric.Delegate:
                    {
                        var result = new IntermediateDelegateType(name, this.Parent);
                        if (genParamData.Length > 0)
                            result.TypeParameters.AddRange(genParamData);
                        rResult = result;
                        this.Parent.Delegates.Add(result);
                    }
                    break;
                case TypeKindGeneric.Interface:
                    {
                        var result = new IntermediateInterfaceType(name, this.Parent);
                        if (genParamData.Length > 0)
                            result.TypeParameters.AddRange(genParamData);
                        rResult = result;
                        this.Parent.Interfaces.Add(result);
                    }
                    break;
                case TypeKindGeneric.Struct:
                    {
                        var result = new IntermediateStructType(name, this.Parent);
                        if (genParamData.Length > 0)
                            result.TypeParameters.AddRange(genParamData);
                        rResult = result;
                        this.Parent.Structs.Add(result);
                    }
                    break;
                default:
                    throw new NotSupportedException();
            }
            return rResult;
        }

        #endregion

        #region IFullTypeDictionary Members

        public IType FindTypeByName(string typeName, int typeParameterCount = 0)
        {
            foreach (var type in this.Values)
            {
                var entry = type.Entry;
                if (entry.Name == typeName)
                {
                    if (typeParameterCount > 0 && entry.IsGenericConstruct)
                    {
                        var generic = entry as IGenericType;
                        if (generic == null)
                            continue;
                        if (generic.TypeParameters.Count == typeParameterCount)
                            return generic;
                    }
                    else if (typeParameterCount == 0 && !entry.IsGenericConstruct)
                        return entry;
                }
            }
            return null;
        }

        #endregion
    }
}
