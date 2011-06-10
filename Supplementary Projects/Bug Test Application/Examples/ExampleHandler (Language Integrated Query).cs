using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Oil.Members;

namespace AllenCopeland.Abstraction.SupplementaryProjects.BugTestApplication.Examples
{
    partial class ExampleHandler
    {
        partial class LanguageIntegratedQuery
        {
            public static Tuple<TAssembly, IIntermediateTopLevelMethodMember> CreateStructure<TAssembly>(TAssembly assembly)
                where TAssembly :
                    IIntermediateAssembly
            {
                var @namespace = assembly.Namespaces.Add("LinqExample");
                var topLevelMethod = @namespace.Methods.Add("LinqTest");
                var digits = topLevelMethod.Locals.Add(new TypedName("digits", CommonTypeRefs.StringArray), (new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" }).ToExpression());
                var digitSymbol = (Symbol)"digit";
                var sortedDigits = topLevelMethod.Locals.Add("sortedDigits", 
                        LinqHelper
                        .From("digit", /* in */ digits.GetReference())
                            .OrderBy(digitSymbol.GetProperty("Length"), LinqOrderByDirection.Ascending)
                            .OrderBy(digitSymbol, LinqOrderByDirection.Ascending)
                        .Select(digitSymbol).Build(), LocalTypingKind.Implicit);
                topLevelMethod.Call(typeof(Console).GetTypeExpression(), "WriteLine", "Sorted Digits".ToPrimitive());
                //topLevelMethod.ForEach()
                return new Tuple<TAssembly, IIntermediateTopLevelMethodMember>(assembly, topLevelMethod);

            }
        }
    }
}
