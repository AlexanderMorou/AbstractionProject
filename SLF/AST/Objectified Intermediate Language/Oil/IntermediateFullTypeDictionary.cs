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
            IIntermediateType result = null;
            switch (kind)
            {
                case TypeKind.Class:
                    result = new IntermediateClassType(name, this.Parent);
                    break;
                case TypeKind.Delegate:
                    result = new IntermediateDelegateType(name, this.Parent);
                    break;
                case TypeKind.Enumerator:
                    result = new IntermediateEnumType(name, this.Parent);
                    break;
                case TypeKind.Interface:
                    result = new IntermediateInterfaceType(name, this.Parent);
                    break;
                case TypeKind.Struct:
                    result = new IntermediateStructType(name, this.Parent);
                    break;
                case TypeKind.Other:
                case TypeKind.Ambiguity:
                    break;
                default:
                    break;
            }
            if (result == null)
                throw new NotSupportedException();
            return result;
        }

        public IIntermediateGenericType Add(string name, TypeKindGeneric kind, params GenericParameterData[] genParamData)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
