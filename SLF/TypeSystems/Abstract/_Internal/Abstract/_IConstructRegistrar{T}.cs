using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    interface _IConstructCacheRegistrar<TType, TCacheKey>
    {
        void RegisterConstruct(TType cachedType, TCacheKey cacheKey);
        bool ContainsConstruct(TCacheKey cacheKey);
        TType ObtainConstruct(TCacheKey cacheKey);
        void UnregisterConstruct(TCacheKey cacheKey);
        bool TryObtainConstruct(TCacheKey cacheKey, out TType cachedType);
    }
}
