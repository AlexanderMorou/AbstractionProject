using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with a 
    /// type provider for a language.
    /// </summary>
    public interface IIntermediateLanguageTypeProvider
    {
        /// <summary>
        /// Creates a <see cref="IIntermediateClassType"/> with the
        /// <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// which denotes the unique name of the class.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which contains the new <see cref="IIntermediateClassType"/></param>
        /// <returns>A <see cref="IIntermediateClassType"/>
        /// which results from the operation.</returns>
        IIntermediateClassType CreateClass(string name, IIntermediateTypeParent parent);
        /// <summary>
        /// Creates a <see cref="IIntermediateDelegateType"/> with the
        /// <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// which denotes the unique name of the delegate.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which contains the new <see cref="IIntermediateDelegateType"/></param>
        /// <returns>A <see cref="IIntermediateDelegateType"/>
        /// which results from the operation.</returns>
        IIntermediateDelegateType CreateDelegate(string name, IIntermediateTypeParent parent);
        /// <summary>
        /// Creates a <see cref="IIntermediateEnumType"/> with the
        /// <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// which denotes the unique name of the enum.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which contains the new <see cref="IIntermediateEnumType"/></param>
        /// <returns>A <see cref="IIntermediateEnumType"/>
        /// which results from the operation.</returns>
        IIntermediateEnumType CreateEnum(string name, IIntermediateTypeParent parent);
        /// <summary>
        /// Creates a <see cref="IIntermediateInterfaceType"/> with the
        /// <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// which denotes the unique name of the interface.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which contains the new <see cref="IIntermediateInterfaceType"/></param>
        /// <returns>A <see cref="IIntermediateInterfaceType"/>
        /// which results from the operation.</returns>
        IIntermediateInterfaceType CreateInterface(string name, IIntermediateTypeParent parent);
        /// <summary>
        /// Creates a <see cref="IIntermediateStructType"/> with the
        /// <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// which denotes the unique name of the struct.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/>
        /// which contains the new <see cref="IIntermediateStructType"/></param>
        /// <returns>A <see cref="IIntermediateStructType"/>
        /// which results from the operation.</returns>
        IIntermediateStructType CreateStruct(string name, IIntermediateTypeParent parent);
    }
}
