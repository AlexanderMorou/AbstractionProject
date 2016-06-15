using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Ast;
//using AllenCopeland.Abstraction.Slf.Ast.Cli;
//using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Modules;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
//using AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages;
using System.IO;
//using AllenCopeland.Abstraction.Slf.Languages.CSharp.Translation;

namespace AllenCopeland.Abstraction.Slf.Transformation
{
    public static class VisitorModelExtensions
    {

        public static IType TurnTypeParametersIntoSymbols(this IType original)
        {
            if (original.IsGenericTypeParameter)
                return original.Name.GetSymbolType();
            switch (original.ElementClassification)
            {
                case TypeElementClassification.Array:
                    var arrayOriginal = (IArrayType)original;
                    return TurnTypeParametersIntoSymbols(original.ElementType).MakeArray(arrayOriginal.LowerBounds.ToArray(), arrayOriginal.Lengths.ToArray());
                case TypeElementClassification.Nullable:
                    return TurnTypeParametersIntoSymbols(original.ElementType).MakeNullable();
                case TypeElementClassification.Pointer:
                    return TurnTypeParametersIntoSymbols(original.ElementType).MakePointer();
                case TypeElementClassification.Reference:
                    return TurnTypeParametersIntoSymbols(original.ElementType).MakeByReference();
                case TypeElementClassification.GenericTypeDefinition:
                    return ((IGenericType)(TurnTypeParametersIntoSymbols(original.ElementType))).MakeGenericClosure(((IGenericType)original).GenericParameters.Select(p => p.TurnTypeParametersIntoSymbols()).ToArray());
                case TypeElementClassification.ModifiedType:
                    return ((IModifiedType)(TurnTypeParametersIntoSymbols(original.ElementType))).MakeModified(((IModifiedType)(original)).Modifiers.Select(s => new TypeModification(s.ModifierType.TurnTypeParametersIntoSymbols(), s.IsRequiredType)).ToArray());
                default:
                    return original;
            }
        }

    }
}
