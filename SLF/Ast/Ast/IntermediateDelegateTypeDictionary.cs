using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Modules;
using AllenCopeland.Abstraction.Slf.Languages;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    [DebuggerDisplay("Delegates: {Count}")]
    public class IntermediateDelegateTypeDictionary :
        IntermediateGenericTypeDictionary<IGeneralGenericTypeUniqueIdentifier, IDelegateType, IIntermediateDelegateType>,
        IIntermediateDelegateTypeDictionary
    {
        public IntermediateDelegateTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master)
            : base(parent, master)
        {
        }
        public IntermediateDelegateTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master, IntermediateDelegateTypeDictionary root)
            : base(parent, master, root)
        {
        }
        #region IDelegateTypeDictionary Members

        ITypeParent IDelegateTypeDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        /// <summary>
        /// Creates a new <see cref="IIntermediateDelegateType"/>
        /// instance with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the new
        /// <see cref="IIntermediateDelegateType"/>.</param>
        /// <returns>A new <see cref="IIntermediateDelegateType"/>, if successful.</returns>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/>
        /// equals <see cref="String.Empty"/>.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null.</exception>
        protected override IIntermediateDelegateType GetNewType(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (name == string.Empty)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.name, ExceptionMessageId.ArgumentCannotBeEmpty, ThrowHelper.GetArgumentName(ArgumentWithException.name));
            var assembly = this.Parent.Assembly;
            if (assembly != null && assembly.Provider != null)
            {
                IIntermediateTypeCtorLanguageService<IIntermediateDelegateType> delegateService;
                if (assembly.Provider.TryGetService(LanguageGuids.Services.IntermediateDelegateCreatorService, out delegateService))
                    return delegateService.GetNew(name, this.Parent);
            }
            return new IntermediateDelegateType(name, this.Parent);
        }
    }
}
