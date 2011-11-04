using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction
{
    internal static class ThrowHelper
    {
        public static ArgumentException ObtainArgumentException(ArgumentWithException argument, ArgumentExceptionMessage message)
        {

        }


        private static string GetArgumentName(ArgumentWithException argument)
        {
            switch (argument)
            {
                case ArgumentWithException.argument:
                    return "argument";
                case ArgumentWithException.array:
                    return "array";
                case ArgumentWithException.e:
                    return "e";
                case ArgumentWithException.genericParameters:
                    return "genericParameters";
                case ArgumentWithException.genericType:
                    return "genericType";
                case ArgumentWithException.identifier:
                    return "identifier";
                case ArgumentWithException.index:
                    return "index";
                case ArgumentWithException.items:
                    return "items";
                case ArgumentWithException.key:
                    return "key";
                case ArgumentWithException.length:
                    return "length";
                case ArgumentWithException.methodReplacements:
                    return "methodReplacements";
                case ArgumentWithException.name:
                    return "name";
                case ArgumentWithException.nameAndDelegateType:
                    return "nameAndDelegateType";
                case ArgumentWithException.nameAndType:
                    return "nameAndType";
                case ArgumentWithException.@namespace:
                    return "namespace";
                case ArgumentWithException.owner:
                    return "owner";
                case ArgumentWithException.searchCriteria:
                    return "searchCriteria";
                case ArgumentWithException.series:
                    return "series";
                case ArgumentWithException.signature:
                    return "signature";
                case ArgumentWithException.sizes:
                    return "sizes";
                case ArgumentWithException.target:
                    return "target";
                case ArgumentWithException.TAssembly:
                    return "TAssembly";
                case ArgumentWithException.typeParameters:
                    return "typeParameters";
                case ArgumentWithException.typeReplacements:
                    return "typeReplacements";
                case ArgumentWithException.typeSymbol:
                    return "typeSymbol";
                case ArgumentWithException.value:
                    return "value";
                default:
                    throw ObtainArgumentException(ArgumentWithException.argument, ArgumentExceptionMessage.UnknownArgument);
            }
        }
    }
}
