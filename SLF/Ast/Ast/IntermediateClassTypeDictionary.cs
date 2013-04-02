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
    /// <summary>
    /// Provides a base implementation of an intermediate class dictionary
    /// which contains a series of classes in intermediate form.
    /// </summary>
    [DebuggerDisplay("Classes: {Count}")]
    public class IntermediateClassTypeDictionary :
        IntermediateGenericTypeDictionary<IGeneralGenericTypeUniqueIdentifier, IClassType, IIntermediateClassType>,
        IIntermediateClassTypeDictionary
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateClassTypeDictionary"/> with the
        /// <paramref name="parent"/> and <paramref name="master"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/> which contains the
        /// <see cref="IntermediateClassTypeDictionary"/>.</param>
        /// <param name="master">The <see cref="IntermediateFullTypeDictionary"/>
        /// which contains the set of classes and other types.</param>
        public IntermediateClassTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master)
            : base(parent, master)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateClassTypeDictionary"/> with the
        /// <paramref name="parent"/> and <paramref name="master"/> and 
        /// <paramref name="root"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/> which contains the
        /// <see cref="IntermediateClassTypeDictionary"/>.</param>
        /// <param name="master">The <see cref="IntermediateFullTypeDictionary"/>
        /// which contains the set of classes.</param>
        /// <param name="root">The <see cref="IntermediateClassTypeDictionary"/> which links
        /// multiple sets together.</param>
        public IntermediateClassTypeDictionary(IIntermediateTypeParent parent, IntermediateFullTypeDictionary master, IntermediateClassTypeDictionary root)
            : base(parent, master, root)
        {
        }


        #region IClassTypeDictionary Members

        ITypeParent IClassTypeDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        /// <summary>
        /// Creates a new <see cref="IIntermediateClassType"/> 
        /// instance with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the new
        /// <see cref="IIntermediateStructType"/>.</param>
        /// <returns>A new <see cref="IIntermediateClassType"/>, if successful.</returns>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/>
        /// equals <see cref="String.Empty"/>.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/> is null.</exception>
        protected override IIntermediateClassType GetNewType(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (name == string.Empty)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.name, ExceptionMessageId.ArgumentCannotBeEmpty, ThrowHelper.GetArgumentName(ArgumentWithException.name));
            var assembly = this.Parent.Assembly;
            if (assembly != null && assembly.Provider != null)
            {
                IIntermediateTypeCtorLanguageService<IIntermediateClassType> classService;
                if (assembly.Provider.TryGetService(LanguageGuids.Services.ClassServices.ClassCreatorService, out classService))
                    return classService.GetNew(name, this.Parent);
            }
            return new IntermediateClassType(name, this.Parent);
        }


    }
}
