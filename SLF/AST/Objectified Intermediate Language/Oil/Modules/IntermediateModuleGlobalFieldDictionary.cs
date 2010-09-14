using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Diagnostics;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Modules
{
    /// <summary>
    /// Provides an intermediate module global field dictionary.
    /// </summary>
    [DebuggerDisplay("Global Fields: {Count}")]
    public class IntermediateModuleGlobalFieldDictionary :
        IntermediateGroupedMemberDictionary<IModule, IIntermediateModule, IModuleGlobalField, IIntermediateModuleGlobalField>,
        IIntermediateModuleGlobalFieldDictionary
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateModuleGlobalFieldDictionary"/>
        /// </summary>
        /// <param name="master">
        /// The <see cref="IntermediateFullMemberDictionary"/> master
        /// dictionary which groups the intermediate module's members
        /// together.</param>
        /// <param name="parent">The <see cref="IntermediateModule"/> which owns the <see cref="IntermediateModuleGlobalFieldDictionary"/>.</param>
        protected internal IntermediateModuleGlobalFieldDictionary(IntermediateFullMemberDictionary master, IntermediateModule parent) :
            base(master, parent)
        {
        }

        #region IIntermediateFieldMemberDictionary<IModuleGlobalField,IIntermediateModuleGlobalField,IModule,IIntermediateModule> Members

        /// <summary>
        /// Adds a new <see cref="IIntermediateModuleGlobalField"/> with
        /// the <paramref name="nameAndType"/> provided.
        /// </summary>
        /// <param name="nameAndType">The <see cref="TypedName"/>
        /// which specifies the type of the field and its name.</param>
        /// <returns>A new <see cref="IIntermediateModuleGlobalField"/>
        /// which represents the field added.</returns>
        public IIntermediateModuleGlobalField Add(TypedName nameAndType)
        {
            return this.Add(nameAndType, null);
        }

        /// <summary>
        /// Adds a new <see cref="IIntermediateModuleGlobalField"/> with the
        /// <paramref name="nameAndType"/> and
        /// <paramref name="initializationexpression"/> provided.
        /// </summary>
        /// <param name="nameAndType">The <see cref="TypedName"/>
        /// which specifies the type of the field and its name.</param>
        /// <param name="initializationExpression">The <see cref="IExpression"/>
        /// to which the <see cref="IIntermediateModuleGlobalField"/>
        /// is initialized to.</param>
        /// <returns>A new <see cref="IIntermediateModuleGlobalField"/>
        /// which represents the field added.</returns>
        public IIntermediateModuleGlobalField Add(TypedName nameAndType, IExpression initializationExpression)
        {
            IntermediateModuleGlobalField result = new IntermediateModuleGlobalField(nameAndType.Name, base.Parent);
            result.InitializationExpression = initializationExpression;
            this.AddDeclaration(result);
            return result;
        }

        #endregion
    }
}
