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
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using Microsoft.VisualBasic.CompilerServices;

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
            IIntermediateNamespaceDeclaration defaultNamespace = null;
            Action OILVersion = () =>
                defaultNamespace = OilVersion();
            Console.WriteLine("OIL version takes {0}.", Time(OILVersion));
            Console.WriteLine("CodeDOM version takes {0}.", Time(CodeDomVersion));
            Console.WriteLine("Extraction01 takes {0}.", Time(() => Extraction01(defaultNamespace)));
        }

        private static IIntermediateNamespaceDeclaration OilVersion()
        {
            var project = IntermediateGateway.CreateAssembly("TestAssembly");

            var dNameSpace = project.DefaultNamespace = project.Namespaces.Add("AllenCopeland.Abstraction.Slf.Examples.TestAssembly");
            var programClass = dNameSpace.Classes.Add("Program");
            programClass.AccessLevel = AccessLevelModifiers.Internal;
            programClass.SpecialModifier = SpecialClassModifier.Module;
            var mainMethod = programClass.Methods.Add("Main");
            mainMethod.AccessLevel = AccessLevelModifiers.Internal;
            mainMethod.IsStatic = true;
            mainMethod.Call(
                typeof(Console).GetInvokeMethodExpression("WriteLine", "It is now {0}.".ToPrimitive(), typeof(DateTime).GetPropertyExpression("Now")));
            return dNameSpace;
        }

        private static void CodeDomVersion()
        {
            var project = new CodeCompileUnit();
            var dNameSpace = new CodeNamespace("AllenCopeland.Abstraction.Slf.Examples.TestAssembly");
            project.Namespaces.Add(dNameSpace);

            CodeTypeDeclaration programClass = new CodeTypeDeclaration("Program");
            dNameSpace.Types.Add(programClass);
            programClass.IsClass = true;
            programClass.Attributes = MemberAttributes.Final | MemberAttributes.Assembly;
            programClass.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference(typeof(StandardModuleAttribute))));
            
            CodeMemberMethod mainMethod = new CodeMemberMethod();
            programClass.Members.Add(mainMethod);
            mainMethod.Name = "Main";
            mainMethod.Attributes = MemberAttributes.Static | MemberAttributes.Assembly;
            mainMethod.Statements.Add(
                new CodeMethodInvokeExpression(
                    new CodeTypeReferenceExpression(
                        typeof(Console)), 
                        "WriteLine", 
                            new CodeExpression[] { 
                                new CodePrimitiveExpression("It is now {0}."), 
                                new CodePropertyReferenceExpression(
                                    new CodeTypeReferenceExpression(typeof(DateTime)), 
                                    "Now") }));
        }

        private static void Extraction01(IIntermediateNamespaceDeclaration dNameSpace)
        {
            var testGeneric = dNameSpace.Classes.Add("GenericType");
            var testGenericParam = testGeneric.TypeParameters.Add("TTypeParam");
            testGenericParam.SpecialConstraint = GenericTypeParameterSpecialConstraint.Struct;
            var testNestGeneric = testGeneric.Classes.Add("TestNestGenericType");
            var testNestGenericParam = testNestGeneric.TypeParameters.Add("TestGenericParameter");
            var testNestGenericMethod = testNestGeneric.Methods.Add("TestNestGenericMethod", new TypedNameSeries() { { "p1", testNestGenericParam }, { "p2", testGenericParam }, { "p3", "TTestNestGenericMethodTypeParam" } }, new GenericParameterData("TTestNestGenericMethodTypeParam"));
            testNestGenericParam.SpecialConstraint = GenericTypeParameterSpecialConstraint.Struct;
            var testNestInstance = testNestGeneric.MakeGenericType(typeof(long), typeof(double));
            var testNestGenericMethodInstance = testNestInstance.Methods[0].Value.MakeGenericMethod(new TypeCollection(typeof(int).GetTypeReference()));

            //IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<IntermediateClassType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<IntermediateClassType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<IntermediateClassType>.PropertyMethodMember, IClassType, IIntermediateClassType, IntermediateClassType>
            var fType = typeof(Dictionary<string, int>).GetTypeReference();
            Console.WriteLine();
            Console.WriteLine(fType.Members.Count);
            foreach (var declaration in fType.Declarations)
                Console.WriteLine(declaration);

        }
    }
}
