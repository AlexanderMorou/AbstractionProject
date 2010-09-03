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
using AllenCopeland.Abstraction.Utilities.Arrays;
using System.Collections.Generic;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq;
using System.Linq.Expressions;
using System.Diagnostics;
using System.Windows.Forms;
using AllenCopeland.Abstraction.Utilities.Common;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using Microsoft.VisualBasic.CompilerServices;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using System.Reflection.Emit;
using System.Reflection;
using System.Runtime.CompilerServices;
using AllenCopeland.Abstraction.Utilities.Tuples;

namespace AllenCopeland.Abstraction.SupplimentaryProjects.BugTestApplication
{
    internal static partial class Program
    {
        private static int test = 0;

        private static void Extraction04()
        {
            var assemblyName = new AssemblyName("prog");
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save, @"C:\Projects\Code\C#\Abstraction\Supplementary Projects\Bug Test Application\");
            var rootModule = assemblyBuilder.DefineDynamicModule("TestAssembly", "prog.dll");
            var classTest = rootModule.DefineType("testClass1", TypeAttributes.NotPublic | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.Sealed | TypeAttributes.Abstract | TypeAttributes.BeforeFieldInit);
            var testField = classTest.DefineField("testField", typeof(int), FieldAttributes.Static | FieldAttributes.Public);
            testField.SetConstant((int)3);

            var u = classTest.CreateType();
            rootModule.CreateGlobalFunctions();

            var secondAssemblyName = new AssemblyName("prog2");
            var secondAssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(secondAssemblyName, AssemblyBuilderAccess.Save, @"C:\Projects\Code\C#\Abstraction\Supplementary Projects\Bug Test Application\");
            var secondRootModule = secondAssemblyBuilder.DefineDynamicModule("TestAssembly2", "prog2.dll");
            var secondClassTest = secondRootModule.DefineType("testClass2", TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.Sealed | TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.BeforeFieldInit);
            var secondTestField = secondClassTest.DefineField("testField2", typeof(int), FieldAttributes.Public | FieldAttributes.Static);
            var secondStaticCtor = secondClassTest.DefineTypeInitializer();
            var secondStaticGen = secondStaticCtor.GetILGenerator();
            assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(typeof(InternalsVisibleToAttribute).GetConstructor(BindingFlags.Public | BindingFlags.Instance, Type.DefaultBinder, new Type[] { typeof(string) }, null), new object[] { secondAssemblyName.FullName }));
            secondStaticGen.Emit(OpCodes.Ldsfld, testField);
            secondStaticGen.Emit(OpCodes.Stsfld, secondTestField);
            secondStaticGen.Emit(OpCodes.Ret);
            secondClassTest.CreateType();
            assemblyBuilder.Save(@"prog.dll");
            secondAssemblyBuilder.Save(@"prog2.dll");
        }

        private static void Extraction03()
        {
            Console.WriteLine("Took {0} to process initially.", Time(Test1));
            Console.ReadKey();
            Console.WriteLine("Took {0} to clear cache and wait for pending finalizers", Time(CLIGateway.ClearCache));
            Console.WriteLine("Took {0} to process secondarily.", Time(Test1));
            Console.ReadKey(true);
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
            var p = mainMethod.If("Test".ToPrimitive().EqualTo((Symbol)"test"));
            var innerClass1 = p.Classes.Add("TestInnerClass");
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
        /// <summary>
        /// The entrypoint for the application.
        /// </summary>
        private static void Main()
        {
            BuildTupleSamples();
            Extraction03();
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
            var dType = typeof(IntermediateDeclarationDictionary<,>);
            var fType = dType.GetTypeReference();

            Console.WriteLine(fType.Members.Count);
            foreach (var declaration in fType.Declarations)
                Console.WriteLine(declaration);
        }

        private static void Extraction02(Type dType, IType fType)
        {
            var iMap = dType.GetInterfaceMap(dType.GetInterfaces().First(p =>
            {
                if (p.IsInterface && p.IsGenericType && !p.IsGenericTypeDefinition)
                    if (p.GetGenericTypeDefinition() == typeof(IIntermediateDeclarationDictionary<,>))
                        return true;
                return false;
            }));
            var valuesProp = fType.Members.First(p => p.Value.Entry.Name == "Values").Value.Entry as ICompiledPropertyMember;
            var valuesPropGetMethod = valuesProp.GetMethod as ICompiledMethodMember;
            if (valuesPropGetMethod.MemberInfo == iMap.TargetMethods[0])
                Console.WriteLine("??");
        }


        private static void BuildTupleSamples()
        {
            int minTuple = 9;
            int maxTuple = 20;
            var eightTupleType = typeof(Tuple<,,,,,,,>).GetTypeReference<IClassType>();
            IIntermediateAssembly result = IntermediateGateway.CreateAssembly("TupleProject");
            result.DefaultNamespace = result.Namespaces.Add("AllenCopeland.Abstraction.Utilities.Tuples");
            var tupleHelperClass = result.DefaultNamespace.Parts.Add().Classes.Add("TupleHelper");
            for (int i = minTuple; i <= maxTuple; i++)
            {
                GenericParameterData[] names = new GenericParameterData[i];
                for (int j = 0; j < i; j++)
                    names[j] = new GenericParameterData(string.Format("T{0}", j + 1));
                var currentType = result.DefaultNamespace/*.Partials.AddNew()*/.Classes.Add("Tuple");
                for (int j = 0; j < i; j++)
                    currentType.TypeParameters.Add(names[j]);
                TypedName[] parameterInfo = new TypedName[i];
                for (int j = 0; j < i; j++)
                    parameterInfo[j] = new TypedName(string.Format("item{0}", j + 1), currentType.TypeParameters.Values[j]);
                var currentTupleHelper = tupleHelperClass.Methods.Add(new TypedName("GetTuple", IntermediateGateway.CommonlyUsedTypeReferences.Void), parameterInfo.ToSeries());
                
                var mainConstructor = currentType.Constructors.Add(parameterInfo);
                for (int j = 0; j < i; j++)
                {
                    currentTupleHelper.TypeParameters.Add(names[j]);
                    currentTupleHelper.Parameters.Values[j].ParameterType = currentTupleHelper.TypeParameters.Values[j];
                }
                currentTupleHelper.ReturnType = currentType.MakeGenericType(currentTupleHelper.GenericParameters);
                Console.WriteLine(result.DefaultNamespace.Classes[result.DefaultNamespace.Classes.IndexOf(currentType)].Key);
                Console.WriteLine(tupleHelperClass.Methods[tupleHelperClass.Methods.IndexOf(currentTupleHelper)].Key);
                LinkedList<ITypeCollection> sevenTupleSets = new LinkedList<ITypeCollection>();
                LinkedList<IParameterReferenceExpression[]> sevenParameterSets = new LinkedList<IParameterReferenceExpression[]>();
                int sevenTupleSetCount = (int)Math.Ceiling(((double)(i)) / 7);
                for (int j = 0; j < sevenTupleSetCount; j++)
                {
                    var currentSetRange = new Tuple<int, int>((j * 7), Math.Min(((j + 1) * 7), i));
                    var currentTupleSet = new TypeCollection();
                    List<IParameterReferenceExpression> currentParamRefs = new List<IParameterReferenceExpression>();
                    for (int k = currentSetRange.Item1; k < currentSetRange.Item2; k++)
                    {
                        currentTupleSet.Add(currentType.TypeParameters.Values[k]);
                        currentParamRefs.Add(mainConstructor.Parameters.Values[k].GetReference());
                    }
                    sevenParameterSets.AddLast(currentParamRefs.ToArray());
                    sevenTupleSets.AddLast(currentTupleSet);
                }
                var current = sevenTupleSets.Last;
                IClassType currentTypeBase = null;
                ICreateInstanceExpression trailingCascadeParameter = null;
                var currentParamSet = sevenParameterSets.Last;
                while (current != null)
                {
                    if (currentTypeBase == null)
                    {
                        switch (current.Value.Count)
                        {
                            case 1:
                                currentTypeBase = typeof(Tuple<>).GetTypeReference<IClassType>(current.Value);
                                break;
                            case 2:
                                currentTypeBase = typeof(Tuple<,>).GetTypeReference<IClassType>(current.Value);
                                break;
                            case 3:
                                currentTypeBase = typeof(Tuple<,,>).GetTypeReference<IClassType>(current.Value);
                                break;
                            case 4:
                                currentTypeBase = typeof(Tuple<,,,>).GetTypeReference<IClassType>(current.Value);
                                break;
                            case 5:
                                currentTypeBase = typeof(Tuple<,,,,>).GetTypeReference<IClassType>(current.Value);
                                break;
                            case 6:
                                currentTypeBase = typeof(Tuple<,,,,,>).GetTypeReference<IClassType>(current.Value);
                                break;
                            case 7:
                                currentTypeBase = typeof(Tuple<,,,,,,>).GetTypeReference<IClassType>(current.Value);
                                break;
                        }
                        trailingCascadeParameter = currentTypeBase.NewExpression();
                        foreach (var parameterRef in currentParamSet.Value)
                            trailingCascadeParameter.Parameters.Add(parameterRef);
                    }
                    else
                    {
                        var tempTypeBase = eightTupleType.MakeGenericType(current.Value.Concat(new IType[]{currentTypeBase}).ToArray());
                        if (currentParamSet != sevenParameterSets.First)
                        {
                            var tempCascadeParameter = tempTypeBase.NewExpression();
                            foreach (var parameterRef in currentParamSet.Value)
                                tempCascadeParameter.Parameters.Add(parameterRef);
                            tempCascadeParameter.Parameters.Add(trailingCascadeParameter);
                            trailingCascadeParameter = tempCascadeParameter;
                        }
                        else
                            foreach (var parameterRef in currentParamSet.Value)
                                mainConstructor.CascadeMembers.Add(parameterRef);
                        currentTypeBase = tempTypeBase;
                    }
                    current = current.Previous;
                    currentParamSet = currentParamSet.Previous;
                }
                currentType.BaseType = currentTypeBase;
                mainConstructor.CascadeTarget = ConstructorCascadeTarget.Base;
                var creationExpression = currentTupleHelper.ReturnType.NewExpression(new ExpressionCollection((from parameter in currentTupleHelper.Parameters.Values
                                                                                                               select parameter.GetReference()).ToArray()));
                creationExpression.PropertyAssignments.Add("test", 3.ToPrimitive());
                currentTupleHelper.Return(creationExpression);
                currentTupleHelper.AccessLevel = AccessLevelModifiers.Public;
                currentTupleHelper.IsStatic = true;
                mainConstructor.CascadeMembers.Add(trailingCascadeParameter);
                mainConstructor.AccessLevel = AccessLevelModifiers.Public;
            }

            //WriteProject(result, @"C:\Projects\Code\C#\OILexer\");
            //WriteProject(result, @"C:\Projects\Code\C#\OILexer\", ".html", "&nbsp;".Repeat(4), true);

        }

    }
}
