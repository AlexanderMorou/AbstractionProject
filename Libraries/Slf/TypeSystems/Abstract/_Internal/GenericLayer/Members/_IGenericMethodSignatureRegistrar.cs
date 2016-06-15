using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    /// <summary>
    /// Defines properties and methods for a generic method signature's 
    /// original to receive constructor verification of a generic 
    /// instantiation.
    /// </summary>
    /// <remarks>Used to assist the intermediate gateway in maintaining type-parameter
    /// fidelity when a generic parameter is added, removed, and/or repositioned.</remarks>
    internal interface _IGenericMethodSignatureRegistrar
    {
        /// <summary>
        /// Registers a generic child signature with the <paramref name="parent"/> which
        /// contains it.
        /// </summary>
        /// <param name="parent">The <see cref="IMethodSignatureParent"/> which contains
        /// the <paramref name="genericChild"/> being registered.</param>
        /// <param name="genericChild">The <see cref="IMethodSignatureMember"/> to be
        /// registered under <paramref name="parent"/>.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="parent
        /// or <paramref name="genericChild"/> is null.</exception>
        void RegisterGenericChild(IMethodSignatureParent parent, IMethodSignatureMember genericChild);
        /// <summary>
        /// Unregisters a generic child signature by the <paramref name="parent"/> which
        /// contained it.
        /// </summary>
        /// <param name="parent">The <see cref="IMethodSignatureParent"/> which contains
        /// the generic child being unregistered.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="parent"/> is null.</exception>
        void UnregisterGenericChild(IMethodSignatureParent parent);
        /// <summary>
        /// Registers a generic method signature under the <paramref name="typeParameters"/> provided with the 
        /// <paramref name="targetSignature"/> which represents the current generic variation.
        /// </summary>
        /// <param name="targetSignature">The <see cref="IMethodSignatureMember"/> which represents the generic closure
        /// of the original by the <paramref name="typeParameters"/>.</param>
        /// <param name="typeParameters">The <see cref="IControlledTypeCollection"/> which denotes the
        /// types of the generic closure.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="targetSignature"/>
        /// or <paramref name="typeParameters"/> is null.</exception>
        void RegisterGenericMethod(IMethodSignatureMember targetSignature, IControlledTypeCollection typeParameters);
        /// <summary>
        /// Unregisters a generic method signature under the <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="IControlledTypeCollection"/> which denotes the types within
        /// the generic closure to be unregistered.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="typeParameters"/>
        /// is null.</exception>
        void UnregisterGenericMethod(IControlledTypeCollection typeParameters);
    }
}
