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
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions;
using System.Globalization;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

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
                //using System;
                assembly.ScopeCoercions.Add(typeof(Console).Namespace);
                //using System.Linq;
                assembly.ScopeCoercions.Add(typeof(Queryable).Namespace);
                //using System.Globalization;
                assembly.ScopeCoercions.Add(typeof(CultureInfo).Namespace);
                var @namespace = assembly.Namespaces.Add("LinqExample");
                var topLevelMethod = @namespace.Methods.Add("LinqTest");
                //var digits = new String[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" }; 
                var digits = topLevelMethod.Locals.Add(
                        "digits",
                        new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" }.ToExpression(), 
                        LocalTypingKind.Implicit);


                /* *
                 * var sortedDigits = from digit in digits
                 *                    orderby digit.Length descending,
                 *                            digit[0]
                 *                    select digit;
                 * */
                var sortedDigits = topLevelMethod.Locals.Add("sortedDigits", 
                        LinqHelper
                        .From("digit", /* in */ digits.GetReference())
                            .OrderBy("digit".Fuse("Length"), LinqOrderByDirection.Descending)
                            .ThenBy("digit".GetIndexer(0.ToPrimitive()))
                        .Select("digit").Build(), LocalTypingKind.Implicit);
                topLevelMethod.DefineLocal(digits);
                topLevelMethod.DefineLocal(sortedDigits);
                /* *
                 * Construction of expressions is pretty simple; just fuse strings together, 
                 * differentiation takes place on whether the string is an expression or not.
                 * */

                /* *
                 * Console.WriteLine(CultureInfo.CurrentCulture.TextInfo.ToTitleCase("sorted digits"));
                 * */
                topLevelMethod.Call("Console".Fuse("WriteLine").Fuse("CultureInfo".Fuse("CurrentCulture").Fuse("TextInfo").Fuse("ToTitleCase").Fuse("sorted digits".ToPrimitive())));

                /* *
                 * foreach (digit in sortedDigits)
                 *     Console.WriteLine(digit);
                 * */
                var enumerationBlock = topLevelMethod.Enumerate("digit", sortedDigits.GetReference());
                enumerationBlock.Call("Console".Fuse("WriteLine").Fuse((IExpression)(Symbol)"digit"));
                return Tuple.Create(assembly, topLevelMethod);
            }
        }
    }
}
