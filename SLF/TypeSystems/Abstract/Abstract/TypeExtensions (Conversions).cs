using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract
{
    partial class TypeExtensions
    {
        internal static bool ContainsName<TGenericParameter, TParent>(this IGenericParameterDictionary<TGenericParameter, TParent> target, string name)
            where TGenericParameter :
                IGenericParameter<TGenericParameter, TParent>
            where TParent :
                IGenericParamParent<TGenericParameter, TParent>
        {
            for (int i = 0, c = target.Count; i < c; i++)
                if (target.Keys[i].Name == name)
                    return true;
            return false;
        }
        internal static bool ContainsName(this IGenericParameterDictionary target, string name)
        {
            for (int i = 0, c = target.Count; i < c; i++)
            {
                var currentKey = target.Keys[i] as IGenericParameterUniqueIdentifier;
                if (currentKey == null)
                    continue;
                else if (currentKey.Name == name)
                    return true;
            }
            return false;
        }

    }
}
