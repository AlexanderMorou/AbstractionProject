using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    internal class _EnumTypesBase :
        _Types<IGeneralTypeUniqueIdentifier, IEnumType, IEnumTypeDictionary>,
        IEnumTypeDictionary
    {
        internal _EnumTypesBase(_FullTypesBase master, IEnumTypeDictionary originalSet, IGenericType parent)
            : base(master, originalSet, parent)
        {
        }

        #region IEnumTypeDictionary Members

        ITypeParent IEnumTypeDictionary.Parent
        {
            get { return ((ITypeParent)(base.Parent)); }
        }

        #endregion

        protected override IEnumType ObtainWrapper(IEnumType item)
        {
            var gtRegistrar = item as _IGenericClosureRegistrar;
            var genericParameters = Parent.GenericParameters;
            IGenericType genericResult;
                if (gtRegistrar != null && gtRegistrar.TryObtainGenericClosure(genericParameters, out genericResult))
                    return (IEnumType)genericResult;

            var result = new _EnumTypeBase(item, Parent.GenericParameters);
            if (gtRegistrar != null)
                gtRegistrar.RegisterGenericClosure(result, genericParameters);
            return result;
        }

    }
}
