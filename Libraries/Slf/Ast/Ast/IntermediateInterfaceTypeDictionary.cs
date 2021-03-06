﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Languages;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    [DebuggerDisplay("Interfaces: {Count}")]
    public class IntermediateInterfaceTypeDictionary :
        IntermediateGenericTypeDictionary<IGeneralGenericTypeUniqueIdentifier, IInterfaceType, IIntermediateInterfaceType>,
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
            if (this.valuesInstance == null)
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

        /// <summary>
        /// Creates a new <see cref="IIntermediateInterfaceType"/> 
        /// instance with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the new
        /// <see cref="IIntermediateStructType"/>.</param>
        /// <returns>A new <see cref="IntermediateInterfaceType"/>, if successful.</returns>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/>
        /// equals <see cref="String.Empty"/>.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null.</exception>
        protected override IIntermediateInterfaceType GetNewType(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (name == string.Empty)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.name, ExceptionMessageId.ArgumentCannotBeEmpty, ThrowHelper.GetArgumentName(ArgumentWithException.name));
            var assembly = this.Parent.Assembly;
            if (assembly != null)
            {
                IIntermediateTypeCtorLanguageService<IIntermediateInterfaceType> interfaceService;
                if (assembly.Provider.TryGetService(LanguageGuids.Services.InterfaceServices.InterfaceCreatorService, out interfaceService))
                    return interfaceService.New(name, this.Parent);
            }
            return new IntermediateInterfaceType(name, this.Parent);
        }
    }
}
