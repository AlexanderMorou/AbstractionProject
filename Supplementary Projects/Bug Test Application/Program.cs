 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

using System;
using System.Linq;
using System.CodeDom;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.CompilerServices;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections.Generic;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq;
using System.Linq.Expressions;
using System.Diagnostics;
using System.Windows.Forms;
using AllenCopeland.Abstraction.Utilities.Common;

namespace AllenCopeland.Abstraction.SupplimentaryProjects.BugTestApplication
{
    internal static partial class Program
    {
        /// <summary>
        /// The entrypoint for the application.
        /// </summary>
        private static void Main()
        {


            Console.WriteLine("Took {0} to process initially.", Time(Test1));
            Console.ReadKey();
            //Console.Clear();
            Console.WriteLine("Took {0} to clear cache and wait for pending finalizers", Time(CLIGateway.ClearCache));
            Console.WriteLine("Took {0} to process secondarily.",Time(Test1));
            Console.ReadKey(true);
            return;
        }

        private static TimeSpan Time(Action a)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            a();
            sw.Stop();
            return sw.Elapsed;
        }

        private static void Test1()
        {
            Console.WriteLine("test".ToPrimitive().GetIndexer(new IExpression[] { 0.ToPrimitive() }.ToCollection()));
            ILinqExpression queryTestA = new LinqExpression();
            queryTestA.From = new LinqFromClause(/* rangeVariableName: */"person", /* rangeSource: */"people".GetSymbolExpression());
            var queryTestABody = new LinqSelectBody();
            queryTestA.Body = queryTestABody;
            queryTestABody.Clauses.Join(/* rangeVariableName: */"pet",    /* rangeSelector: */"pets".GetSymbolExpression(), /* leftSelector: */"person".GetSymbolExpression(), /* rightSelector: */"pet".Fuse("Owner"), /* intoRangeName: */"gj");
            queryTestABody.Clauses.From(/* rangeVariableName: */"subPet", /* rangeSelector: */"gj".Fuse("DefaultIfEmpty").Fuse(new IExpression[0]));
            queryTestABody.Selection = "string".Fuse("Format").Fuse(((Symbol)"subPet").EqualTo(IntermediateGateway.NullValue).IIf("string".Fuse("Empty"), "subPet".Fuse("Name")).AsNamedParameter("arg1"), "{0,-15}{1}".ToPrimitive().AsNamedParameter("format"), "person".Fuse("FirstName").AsNamedParameter("arg0"));
            ILinqExpression queryTestB =
                LinqHelper
                    .From(/*     rangeVariable: */ "person", /* rangeSource: */"people")
                    .Join(/* rangeVariableName: */    "pet", /* rangeSource: */"pets", /* conditionLeft: */(Symbol)"person", /* conditionRight: */"pet".Fuse("Owner"), /* intoRangeName: */"gj")
                    .From(/* rangeVariableName: */ "subPet", /* rangeSource: */"gj".Fuse("DefaultIfEmpty").Fuse(ExpressionCollection.EmptyExpressionArray))
                    .Select(/*       selection: */ "string".Fuse("Format").Fuse(((Symbol)"subPet").EqualTo(IntermediateGateway.NullValue).IIf("string".Fuse("Empty"), "subPet".Fuse("Name")).AsNamedParameter("arg1"), "{0,-15}{1}".ToPrimitive().AsNamedParameter("format"), "person".Fuse("FirstName").AsNamedParameter("arg0"))).Build();
            Console.WriteLine(queryTestA);
            Console.WriteLine(" - ");
            Console.WriteLine(queryTestB);
            //LeftOuterJoinExample();
            //var ww = ExpressionKinds.AddOperation.ToString();
            Console.WriteLine("OIL version takes {0}.", Time(OilVersion));
            Console.WriteLine("CodeDOM version takes {0}.", Time(CodeDomVersion));
        }

        private static void OilVersion()
        {
            var project = IntermediateGateway.CreateAssembly("TestAssembly");

            var dNameSpace = project.DefaultNamespace = project.Namespaces.Add("AllenCopeland.Abstraction.Slf.Examples.TestAssembly");
            var programClass = dNameSpace.Classes.Add("Program");
            programClass.AccessLevel = AccessLevelModifiers.Internal;
            programClass.SpecialModifier = SpecialClassModifier.Module;
            programClass.SpecialModifier = SpecialClassModifier.ExtensionTarget;
            var mainMethod = programClass.Methods.Add("Main");
            
            mainMethod.AccessLevel = AccessLevelModifiers.Internal;
            mainMethod.Call((typeof(Console).GetMethodExpression("WriteLine").Invoke("It is now {0}.".ToPrimitive(), typeof(DateTime).GetTypeExpression().GetProperty("Now"))));
            mainMethod.Call((Symbol)"Console", "WriteLine", "It is now {0}.".ToPrimitive(), "DateTime".Fuse("Now"));
            mainMethod.Call("Activator".Fuse("CreateInstance").Fuse(typeof(Form)).Fuse(ExpressionCollection.EmptyExpressionArray));
            var testGeneric = dNameSpace.Classes.Add("GenericType");
            var testGenericParam = testGeneric.TypeParameters.Add("TTypeParam");
            testGenericParam.SpecialConstraint = GenericTypeParameterSpecialConstraint.Struct;
            var testNestGeneric = testGeneric.Classes.Add("TestNestGenericType");
            var testNestGenericParam = testNestGeneric.TypeParameters.Add("TestGenericParameter");
            var testNestGenericMethod = testNestGeneric.Methods.Add("TestNestGenericMethod", new TypedNameSeries() { { "p1", testNestGenericParam }, { "p2", testGenericParam }, { "p3", "TTestNestGenericMethodTypeParam" } }, new GenericParameterData("TTestNestGenericMethodTypeParam"));
            testNestGenericParam.SpecialConstraint = GenericTypeParameterSpecialConstraint.Struct;
            var testNestInstance = testNestGeneric.MakeGenericType(typeof(long), typeof(double));
            var testNestGenericMethodInstance = testNestInstance.Methods[0].Value.MakeGenericMethod(new TypeCollection(typeof(int).GetTypeReference()));
            //Console.WriteLine(testNestGenericMethodInstance);
            //Console.WriteLine(testNestInstance.CSharpToString());
            //Console.WriteLine(testNestGeneric.CSharpToString());

            var fType = typeof(AccessLevelModifiers).GetTypeReference();
            Console.WriteLine();
            Console.WriteLine(fType.Members.Count);
            foreach (var member in from m in fType.Members.Shuffle()
                                   select m.Value.Entry)
                Console.WriteLine(member);
            
        }

        private static void CodeDomVersion()
        {
            var project = new CodeCompileUnit();
            var dNameSpace = new CodeNamespace("AllenCopeland.Abstraction.Slf.Examples.TestAssembly");
            project.Namespaces.Add(dNameSpace);

            CodeTypeDeclaration programClass;
            dNameSpace.Types.Add(programClass = new CodeTypeDeclaration("Program") { IsClass = true, Attributes = MemberAttributes.Static | MemberAttributes.Assembly });
            CodeMemberMethod mainMethod;
            programClass.Members.Add(mainMethod = new CodeMemberMethod() { Name = "Main", Attributes = MemberAttributes.Static | MemberAttributes.Assembly });
            mainMethod.Statements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(Console)), "WriteLine", new CodeExpression[] { new CodePrimitiveExpression("It is now {0}."), new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(DateTime)), "Now") }));
        }
    }
}
