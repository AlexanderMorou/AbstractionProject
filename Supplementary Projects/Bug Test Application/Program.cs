﻿ /*---------------------------------------------------------------------\
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
            //First time's skewed due to JIT compilation.
            BuildTupleSamples();
            Console.WriteLine("Press any key to run test again...");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.ReadKey(true);
            Console.Clear();
            BuildTupleSamples();
            //Extraction03();
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
            int maxTuple = 92;
            /* *
             * The system tuple implementation maxes out at eight
             * elements with the final element consisting of a secondary
             * tuple set.
             * */
            var eightTupleType = typeof(Tuple<,,,,,,,>).GetTypeReference<IClassType>();
            IIntermediateAssembly result = IntermediateGateway.CreateAssembly("TupleProject");
            /* *
             * Add a default namespace and tuple helper class for simpler instantiation of
             * tuple instances.
             * */
            result.DefaultNamespace = result.Namespaces.Add("AllenCopeland.Abstraction.Utilities.Tuples");
            var tupleHelperClass = result.DefaultNamespace.Parts.Add().Classes.Add("TupleHelper");
            /* *
             * Obtain a stopwatch for monitoring each individual pass.
             * */
            Stopwatch sw = new Stopwatch();
            TimeSpan[] passTimes = new TimeSpan[maxTuple + 1 - minTuple];

            for (int i = minTuple; i <= maxTuple; i++)
            {
                sw.Start();
                /* *
                 * Each tuple has n-many type-parameters where n is equal
                 * to the current value of i.
                 * *
                 * As such helper methods and the type's constructor must 
                 * have n-many parameters in order to instantiate such 
                 * types.
                 * */
                GenericParameterData[] names = new GenericParameterData[i];
                for (int j = 0; j < i; j++)
                    names[j] = new GenericParameterData(string.Format("T{0}", j + 1));
                var currentType = result.DefaultNamespace.Parts.Add().Classes.Add("Tuple", names);
                TypedName[] parameterInfo = new TypedName[i];
                for (int j = 0; j < i; j++)
                    parameterInfo[j] = new TypedName(string.Format("item{0}", j + 1), names[j].Name);
                var currentTupleHelper = tupleHelperClass.Methods.Add(new TypedName("GetTuple", IntermediateGateway.CommonlyUsedTypeReferences.Void), parameterInfo.ToSeries(), names);
                
                var mainConstructor = currentType.Constructors.Add(parameterInfo);

                currentTupleHelper.ReturnType = currentType.MakeGenericType(currentTupleHelper.GenericParameters);
                LinkedList<ITypeCollection> sevenTupleSets = new LinkedList<ITypeCollection>();
                LinkedList<IParameterReferenceExpression[]> sevenParameterSets = new LinkedList<IParameterReferenceExpression[]>();
                int sevenTupleSetCount = (int)Math.Ceiling(((double)(i)) / 7);
                /* *
                 * Since the final eight-type tuple represents a tuple whose last type-parameter
                 * is another tuple, it's necessary to segment the tuple into groups of seven,
                 * where the eighth would be a forward reference to the rest of the tuple's
                 * data elements.  This process is recursive, so a 14-type tuple would be:
                 * Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>>
                 * whereas a 15-type tuple would be:
                 * Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>>
                 * */
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
                /* *
                 * We start with the last type in the tuple groups,
                 * this way we can build the type backwards and end up with
                 * a final type where the last few types are in as small a tuple
                 * as possible.
                 * */
                var current = sevenTupleSets.Last;
                IClassType currentTypeBase = null;
                ICreateInstanceExpression trailingCascadeParameter = null;
                var currentParamSet = sevenParameterSets.Last;
                while (current != null)
                {
                    /* *
                     * Since we start with the last element, it's only
                     * null on the last element, or the first pass.
                     * */
                    if (currentTypeBase == null)
                    {
                        /* *
                         * The last set will always have no more than seven types.
                         * *
                         * So create a reference to a tuple grouping with the appropriate
                         * number of type-parameters.
                         * */
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
                        /* *
                         * The trailing cascade parameter instantiates the final tuple on all
                         * versions of the type-parameter, except for the last, which has no more
                         * than seven elements.
                         * */
                        trailingCascadeParameter = currentTypeBase.NewExpression(currentParamSet.Value);
                    }
                    else
                    {
                        /* *
                         * Every element after the first processed (or last in line)
                         * has exactly eight types.
                         * */
                        var tempTypeBase = eightTupleType.MakeGenericType(current.Value.Concat(new IType[] { currentTypeBase }).ToArray());
                        if (currentParamSet != sevenParameterSets.First)
                        {
                            /* *
                             * Rebuild the trailing cascade parameter to new up an instance of
                             * the current temp base type, its final parameter is the current
                             * cascade parameter, thereby making the final cascade member
                             * the results of this action.
                             * */
                            var tempCascadeParameter = tempTypeBase.NewExpression();
                            foreach (var parameterRef in currentParamSet.Value)
                                tempCascadeParameter.Parameters.Add(parameterRef);
                            tempCascadeParameter.Parameters.Add(trailingCascadeParameter);
                            trailingCascadeParameter = tempCascadeParameter;
                        }
                        else
                            /* *
                             * In cases where it's the last set to be processed, or the
                             * first set of tuple types, instead of newing up the base type,
                             * redirect them to the main constructor's cascade parameters
                             * targeted at the base 8-type tuple.
                             * */
                            foreach (var parameterRef in currentParamSet.Value)
                                mainConstructor.CascadeMembers.Add(parameterRef);
                        currentTypeBase = tempTypeBase;
                    }
                    current = current.Previous;
                    currentParamSet = currentParamSet.Previous;
                }
                /* *
                 * Assign the current type's base type.
                 * */
                currentType.BaseType = currentTypeBase;
                //Denote the cascade target.
                mainConstructor.CascadeTarget = ConstructorCascadeTarget.Base;

                //Obtain a series of references to the parameters.
                var createTupleHelperParams = from parameter in currentTupleHelper.Parameters.Values.AsQueryable()
                                              select parameter.GetReference();
                //for use with the constructor of the type.
                currentTupleHelper.Return(currentTupleHelper.ReturnType.NewExpression(createTupleHelperParams));
                //Make the method accessible and static.
                currentTupleHelper.AccessLevel = AccessLevelModifiers.Public;
                currentTupleHelper.IsStatic = true;
                //Apply the trailing cascade parameter.
                mainConstructor.CascadeMembers.Add(trailingCascadeParameter);
                //Make the main constructor accessible.
                mainConstructor.AccessLevel = AccessLevelModifiers.Public;
                //Obtain the current pass' time and reset the timer.
                sw.Stop();
                passTimes[i - minTuple] = sw.Elapsed;
                sw.Reset();
            }
            TimeSpan fullTimeTaken = TimeSpan.Zero;

            foreach (var span in passTimes)
                fullTimeTaken += span;
            TimeSpan averageSpan = new TimeSpan(fullTimeTaken.Ticks / (passTimes.Length - 1));
            //WriteProject(result, @"C:\Projects\Code\C#\OILexer\");
            //WriteProject(result, @"C:\Projects\Code\C#\OILexer\", ".html", "&nbsp;".Repeat(4), true);
            result.Dispose();
            Console.WriteLine("To build a series of {0} tuple classes it took: {1}", passTimes.Length, fullTimeTaken);
            Console.WriteLine("The average pass took: {0}", averageSpan);
        }

    }
}