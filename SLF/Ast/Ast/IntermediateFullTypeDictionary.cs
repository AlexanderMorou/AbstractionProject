using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Languages;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides an intermediate full dictionary implementation.
    /// </summary>
    [DebuggerDisplay("Types: {Count}")]
    public class IntermediateFullTypeDictionary :
        IntermediateFullDeclarationDictionary<IGeneralTypeUniqueIdentifier, IType, IIntermediateType>,
        IIntermediateFullTypeDictionary,
        IDisposable
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
                case TypeKind.Enumeration:
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
            var assembly = this.Parent.Assembly;
            switch (kind)
            {
                case TypeKindGeneric.Class:
                    {
                        IIntermediateClassType insertionElement = null;
                        if (assembly != null)
                        {
                            IIntermediateTypeCtorLanguageService<IIntermediateClassType> classService;
                            if (assembly.Provider.TryGetService(LanguageGuids.Services.ClassServices.ClassCreatorService, out classService))
                                insertionElement = classService.GetNew(name, this.Parent);
                        }
                        if (insertionElement == null)
                            insertionElement = new IntermediateClassType(name, this.Parent);
                        if (genParamData.Length > 0)
                            insertionElement.TypeParameters.AddRange(genParamData);
                        rResult = insertionElement;
                        this.Parent.Classes.Add(insertionElement);
                    }
                    break;
                case TypeKindGeneric.Delegate:
                    {
                        IIntermediateDelegateType insertionElement = null;
                        if (assembly != null && assembly.Provider != null)
                        {
                            IIntermediateTypeCtorLanguageService<IIntermediateDelegateType> delegateService;
                            if (assembly.Provider.TryGetService(LanguageGuids.Services.IntermediateDelegateCreatorService, out delegateService))
                                insertionElement = delegateService.GetNew(name, this.Parent);
                        }
                        if (insertionElement == null)
                            insertionElement = new IntermediateDelegateType(name, this.Parent);
                        if (genParamData.Length > 0)
                            insertionElement.TypeParameters.AddRange(genParamData);
                        rResult = insertionElement;
                        this.Parent.Delegates.Add(insertionElement);
                    }
                    break;
                case TypeKindGeneric.Interface:
                    {
                        IIntermediateInterfaceType insertionElement = null;
                        if (assembly != null && assembly.Provider != null)
                        {
                            IIntermediateTypeCtorLanguageService<IIntermediateInterfaceType> interfaceService;
                            if (assembly.Provider.TryGetService(LanguageGuids.Services.InterfaceServices.InterfaceCreatorService, out interfaceService))
                                insertionElement = interfaceService.GetNew(name, this.Parent);
                        }
                        if (insertionElement == null)
                            insertionElement = new IntermediateInterfaceType(name, this.Parent);
                        if (genParamData.Length > 0)
                            insertionElement.TypeParameters.AddRange(genParamData);
                        rResult = insertionElement;
                        this.Parent.Interfaces.Add(insertionElement);
                    }
                    break;
                case TypeKindGeneric.Struct:
                    {
                        IIntermediateStructType insertionElement = null;
                        if (assembly != null && assembly.Provider != null)
                        {
                            IIntermediateTypeCtorLanguageService<IIntermediateStructType> structService;
                            if (assembly.Provider.TryGetService(LanguageGuids.Services.StructServices.StructCreatorService, out structService))
                                insertionElement = structService.GetNew(name, this.Parent);
                        }
                        if (insertionElement == null)
                            insertionElement = new IntermediateStructType(name, this.Parent);
                        if (genParamData.Length > 0)
                            insertionElement.TypeParameters.AddRange(genParamData);
                        rResult = insertionElement;
                        this.Parent.Structs.Add(insertionElement);
                    }
                    break;
                default:
                    throw new NotSupportedException();
            }
            return rResult;
        }

        #endregion

        #region IFullTypeDictionary Members

        public IType[] GetTypesByName(string name)
        {
            return (from t in this.Values
                    where t.Entry.Name == name
                    select (IType)t.Entry).ToArray();
        }

        #endregion



        #region IDisposable Members

        public void Dispose()
        {
            base._Clear();
        }

        #endregion


        internal void ConditionalRemove(IIntermediateTypeParent parent)
        {
            this._RemoveSet(from element in this
                            let intermediateItem = element.Value.Entry as IIntermediateType
                            where intermediateItem != null && intermediateItem.Parent == parent
                            select element.Key);
        }
    }
}
