using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Defines properties and methods for working with the private
    /// implementation details of an assembly.
    /// </summary>
    /// <remarks>Details such as the anonymous types, types associated
    /// to fixed-size static data fields defined within an assembly.
    /// </remarks>
    public interface IPrivateImplementationDetails :
        IIntermediateClassType
    {
        /// <summary>
        /// Returns the <see cref="IAnonymousTypeDictionary"/> which enables
        /// declaration and management of anonymous types within the 
        /// <see cref="IPrivateImplementationDetails"/>.
        /// </summary>
        IAnonymousTypeDictionary AnonymousTypes { get; }
        /// <summary>
        /// Returns/sets the <see cref="Guid"/> which associates a
        /// uniqueness value to the assembly to distinguish it between
        /// other assemblies of the same name.
        /// </summary>
        Guid DetailGuid { get; set; }
        /// <summary>
        /// Obtains a <see cref="IDataSizeType"/>
        /// of the given <paramref name="dataSize"/>.
        /// </summary>
        /// <param name="dataSize">The <see cref="Int32"/> value 
        /// representing the number of bytes within the data
        /// type.</param>
        /// <returns>A <see cref="IDataSizeType"/> relative
        /// to the <paramref name="dataSize"/> provided.</returns>
        /// <remarks><para>Types defined within this are purely for
        /// convenience, the yielded type at the point of compile
        /// may change based upon the library used to handle
        /// compilation.</para><para>
        /// An example is System.Reflection.Emit which emits
        /// a type at the global-level, whereas C&#9839; emits
        /// it within the 
        /// PrivateImplementationDetails&lt;<see cref="DetailGuid"/>&gt;</para>
        /// of the assembly.</remarks>
        IDataSizeType GetSizeDataType(int dataSize);
    }
}
