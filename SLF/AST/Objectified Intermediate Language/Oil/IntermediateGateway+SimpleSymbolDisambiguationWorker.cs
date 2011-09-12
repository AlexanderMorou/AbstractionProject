using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateGateway
    {
        private class SimpleTypeDisambiguationWorker
        {
            private IIntermediateDeclaration originPoint;
            private DisambiguateFromSelector selector;

            private SimpleTypeDisambiguationWorker()
            {
            }

            internal static IType Disambiguate(IType sourceType, IIntermediateDeclaration originPoint)
            {
                var worker = new SimpleTypeDisambiguationWorker();
                worker.originPoint = originPoint;
                worker.selector = GetSelector(originPoint);
                return worker.Disambiguate(sourceType);
            }

            public IType Disambiguate(IType source)
            {
                switch (source.ElementClassification)
                {
                    case TypeElementClassification.None:
                        if (source is ISymbolType && !source.IsGenericConstruct)
                            return selector(originPoint, source.Name, source);
                        break;
                    case TypeElementClassification.Array:
                        var arrayType = source as IArrayType;
                        if (arrayType != null)
                            return Disambiguate(source.ElementType).MakeArray(arrayType.LowerBounds);
                        else
                            break;
                    case TypeElementClassification.Nullable:
                        return Disambiguate(source.ElementType).MakeNullable();
                    case TypeElementClassification.Pointer:
                        return Disambiguate(source.ElementType).MakePointer();
                    case TypeElementClassification.Reference:
                        return Disambiguate(source.ElementType).MakeByReference();
                    case TypeElementClassification.GenericTypeDefinition:
                        var genericSource = source as IGenericType;

                        IType[] gParamCopy = genericSource.GenericParameters.ToArray();
                        bool varies = false;
                        for (int i = 0; i < gParamCopy.Length; i++)
                        {
                            var recent = gParamCopy[i];
                            var current = Disambiguate(recent);
                            if (current != recent)
                                varies = true;
                            gParamCopy[i] = current;
                        }
                        if (varies)
                            return ((IGenericType)(genericSource.ElementType)).MakeGenericClosure(gParamCopy.ToCollection());
                        else
                            break;
                }
                return source;
            }
        }
    }
}
