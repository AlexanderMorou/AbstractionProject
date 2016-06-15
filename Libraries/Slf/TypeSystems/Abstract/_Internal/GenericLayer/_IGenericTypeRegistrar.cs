using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal interface _IGenericClosureRegistrar
    {
        /// <summary>
        /// Registers a generic closure
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="typeParameters"></param>
        void RegisterGenericClosure(IGenericType targetType, ILockedTypeCollection typeParameters);
        bool ContainsGenericClosure(ILockedTypeCollection typeParameters);
        IGenericType ObtainGenericClosure(ILockedTypeCollection typeParameters);
        void UnregisterGenericClosure(ILockedTypeCollection typeParameters);
        bool TryObtainGenericClosure(ILockedTypeCollection typeParameters, out IGenericType targetType);
    }
}
