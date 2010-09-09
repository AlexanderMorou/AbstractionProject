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
using AllenCopeland.Abstraction.Utilities.Common;
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
using System.Threading.Tasks;

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
            Console.WriteLine();
            Console.WriteLine("Took {0} to clear cache and wait for pending finalizers", Time(CLIGateway.ClearCache));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Took {0} to process secondarily.", Time(Test1));
            Console.WriteLine();
            Console.WriteLine("Took {0} to to clear and wait (again)", Time(CLIGateway.ClearCache));
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
            Extraction05();
        }

        private static void Extraction05()
        {
            BuildTupleSamples();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.WriteLine();
            Console.WriteLine("Re-running test...");
            BuildTupleSamples();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.ReadKey(true);
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
            var dType = typeof(ControlledStateDictionary<,>);
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
            /* *
             * The system tuple implementation maxes out at eight
             * elements with the final element consisting of a secondary
             * tuple set.
             * */
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var eightTupleType           = typeof(Tuple<,,,,,,,>).GetTypeReference<IClassType>();
            IIntermediateAssembly result = IntermediateGateway.CreateAssembly("TupleProject");
            /* *
             * Add a default namespace and tuple helper class for simpler instantiation of
             * tuple instances.
             * */
            result.DefaultNamespace = result.Namespaces.Add("AllenCopeland.Abstraction.Utilities.Tuples");
            var tupleHelperClass    = result.DefaultNamespace.Classes.Add("TupleHelper");
            /* *
             * Obtain a stopwatch for monitoring each individual pass.
             * */
            TimeSpan[] passTimes = new TimeSpan[maxTuple + 1 - minTuple];
            tupleHelperClass.SuspendDualLayout();
            GenericParameterData[] nameCopy = new GenericParameterData[maxTuple];
            TypedName[] parameterInfoCopy = new TypedName[maxTuple];
            for (int j = 0; j < maxTuple; j++)
            {
                string currentTypeParamName = string.Format("TItem{0}", j + 1);
                nameCopy[j] = new GenericParameterData(currentTypeParamName);
                parameterInfoCopy[j] = new TypedName(string.Format("item{0}", j + 1), currentTypeParamName);
            }
            var getTupleName = new TypedName("GetTuple", IntermediateGateway.CommonlyUsedTypeReferences.Void);
            IClassType[] tupleBaseTypes = new IClassType[7];
            /* *
             * Unrealistic example, but need to verify ability to obtain multiple 
             * type references in a thread safe manner.
             * */
            Parallel.For(0, 8, i =>
                {
                    switch (i)
                    {
                        case 0:
                            tupleBaseTypes[i] = typeof(Tuple<>).GetTypeReference<IClassType>();
                            break;
                        case 1:
                            tupleBaseTypes[i] = typeof(Tuple<,>).GetTypeReference<IClassType>();
                            break;
                        case 2:
                            tupleBaseTypes[i] = typeof(Tuple<,,>).GetTypeReference<IClassType>();
                            break;
                        case 3:
                            tupleBaseTypes[i] = typeof(Tuple<,,,>).GetTypeReference<IClassType>();
                            break;
                        case 4:
                            tupleBaseTypes[i] = typeof(Tuple<,,,,>).GetTypeReference<IClassType>();
                            break;
                        case 5:
                            tupleBaseTypes[i] = typeof(Tuple<,,,,,>).GetTypeReference<IClassType>();
                            break;
                        case 6:
                            tupleBaseTypes[i] = typeof(Tuple<,,,,,,>).GetTypeReference<IClassType>();
                            break;

                    }
                });
            
            var unused = tupleHelperClass.Methods;
            var unused2 = result.DefaultNamespace.Types;
            Parallel.For(minTuple, maxTuple + 1, i =>
            //for (int i = minTuple; i <= maxTuple; i++)
            {
                Stopwatch swInner = new Stopwatch();
                swInner.Start();
                /* *
                 * Each tuple has n-many type-parameters where n is equal
                 * to the current value of i.
                 * *
                 * As such helper methods and the type's constructor must 
                 * have n-many parameters in order to instantiate such 
                 * types.
                 * */
                GenericParameterData[] names = new GenericParameterData[i];
                var parameterInfo            = new TypedName[i];
                Array.Copy(nameCopy, names, i);
                Array.Copy(parameterInfoCopy, parameterInfo, i);
                var dNSParts = result.DefaultNamespace.Parts;
                var currentType = dNSParts.Add().Classes.Add("Tuple", names);
                currentType.SuspendDualLayout();

                var currentTupleHelper       = tupleHelperClass.Methods.Add(getTupleName, parameterInfo, names);

                var mainConstructor = currentType.Constructors.Add(parameterInfo);

                currentTupleHelper.ReturnType = currentType.MakeVerifiedGenericType(currentTupleHelper.GenericParameters);
                LinkedList<ITypeCollection> sevenTupleSets = new LinkedList<ITypeCollection>();
                LinkedList<IExpression[]> sevenParameterSets = new LinkedList<IExpression[]>();
                int sevenTupleSetCount       = (int)Math.Ceiling(((double)(i)) / 7);
                /* *
                 * Since the final eight-type tuple represents a tuple whose last type-parameter
                 * is another tuple, it's necessary to segment the tuple into groups of seven,
                 * where the eighth would be a forward reference to the rest of the tuple's
                 * data elements.  This process is recursive, so a 14-type tuple would be:
                 * Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>>
                 * whereas a 15-type tuple would be:
                 * Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>>
                 * */
                var typeParams               = currentType.TypeParameters.Values.ToArray();
                IExpression[] parameters     = new IExpression[i];
                var parametersEnum           = mainConstructor.Parameters.Values.GetEnumerator();
                int parameterIndex           = 0;

                while (parametersEnum.MoveNext())
                    parameters[parameterIndex++] = parametersEnum.Current.GetReference();
                List<IExpression> currentParamReferences = new List<IExpression>();
                List<IType> currentTupleTypes = new List<IType>();
                for (int k = 0, j = 0; k < i; k++, j++)
                {
                    currentTupleTypes.Add(typeParams[k]);
                    currentParamReferences.Add(parameters[k]);
                    if (j >= 6)
                    {
                        sevenParameterSets.AddLast(currentParamReferences.ToArray());
                        sevenTupleSets.AddLast((LockedTypeCollection)currentTupleTypes.ToLockedCollection());
                        j = -1;
                        currentParamReferences.Clear();
                        currentTupleTypes.Clear();
                    }
                }
                if (currentParamReferences.Count > 0)
                {
                    sevenParameterSets.AddLast(currentParamReferences.ToArray());
                    sevenTupleSets.AddLast((LockedTypeCollection)currentTupleTypes.ToLockedCollection());
                    currentParamReferences.Clear();
                    currentTupleTypes.Clear();
                }
                /* *
                 * We start with the last type in the tuple groups,
                 * this way we can build the type backwards and end up with
                 * a final type where the last few types are in as small a tuple
                 * as possible.
                 * */
                var current                                        = sevenTupleSets.Last;
                IClassType currentTypeBase                         = null;
                ICreateInstanceExpression trailingCascadeParameter = null;
                var currentParamSet                                = sevenParameterSets.Last;
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
                        currentTypeBase = (IClassType)tupleBaseTypes[current.Value.Count - 1].MakeVerifiedGenericType(current.Value);
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
                        var lockedTypes = (LockedTypeCollection)(current.Value);
                        lockedTypes._Add(currentTypeBase);
                        var tempTypeBase = (IClassType)eightTupleType.MakeVerifiedGenericType(lockedTypes);

                        if (currentParamSet != sevenParameterSets.First)
                        {
                            /* *
                             * Rebuild the trailing cascade parameter to new up an instance of
                             * the current temp base type, its final parameter is the current
                             * cascade parameter, thereby making the final cascade member
                             * the results of this action.
                             * */
                            var tempCascadeParameter = tempTypeBase.NewExpression(currentParamSet.Value);
                            tempCascadeParameter.Parameters.IndexedParameters.Add(trailingCascadeParameter);
                            trailingCascadeParameter = tempCascadeParameter;
                        }
                        else
                        {
                            /* *
                             * In cases where it's the last set to be processed, or the
                             * first set of tuple types, instead of newing up the base type,
                             * redirect them to the main constructor's cascade parameters
                             * targeted at the base 8-type tuple.
                             * */
                            mainConstructor.CascadeMembers.AddRange(currentParamSet.Value);
                            mainConstructor.CascadeMembers.Add(trailingCascadeParameter);
                        }
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

                //for use with the constructor of the type.
                var createNewTupleInst = currentTupleHelper.ReturnType.NewExpression();
                foreach (var param in currentTupleHelper.Parameters.Values)
                    createNewTupleInst.Parameters.IndexedParameters.Add(param.GetReference());
                currentTupleHelper.Return(createNewTupleInst);

                //Make the method accessible and static.
                currentTupleHelper.AccessLevel = AccessLevelModifiers.Public;
                currentTupleHelper.IsStatic = true;
                //Make the main constructor accessible.
                mainConstructor.AccessLevel = AccessLevelModifiers.Public;
                currentType.ResumeDualLayout();
                //Obtain the current pass' time and reset the timer.
                swInner.Stop();
                passTimes[i - minTuple] = swInner.Elapsed;

            } /**/);
            sw.Stop();
            TimeSpan actualTimeTaken = sw.Elapsed;
            TimeSpan fullTimeTaken = TimeSpan.Zero;
            sw.Reset();
            sw.Start();
            tupleHelperClass.ResumeDualLayout();
            sw.Stop();
            TimeSpan minTime = TimeSpan.MaxValue;
            TimeSpan maxTime = TimeSpan.MinValue;
            int minIndex = 0,
                maxIndex = 0;
            int index    = minTuple;
            foreach (var span in passTimes)
            {
                if (minTime > span)
                {
                    minTime = span;
                    minIndex = index;
                }
                if (maxTime < span)
                {
                    maxTime = span;
                    maxIndex = index;
                }
                fullTimeTaken += span;
                index++;
            }
            TimeSpan averageSpan = new TimeSpan(fullTimeTaken.Ticks / passTimes.Length);
            TimeSpan averageSpan2 = new TimeSpan(actualTimeTaken.Ticks / passTimes.Length);
            //WriteProject(result, @"C:\Projects\Code\C#\OILexer\");
            //WriteProject(result, @"C:\Projects\Code\C#\OILexer\", ".html", "&nbsp;".Repeat(4), true);
            var dualResume = sw.Elapsed;
            sw.Reset();
            sw.Start();
            result.Dispose();
            sw.Stop();

            Console.WriteLine("To build a series of {0} tuple classes it took: {1}", passTimes.Length, actualTimeTaken);
            Console.WriteLine("The average pass took: {0} / {1}", averageSpan, averageSpan2);
            Console.WriteLine("Total core processing time: {0}", fullTimeTaken);
            Console.WriteLine("Multi-core advantage {0:#.##}% gain", (100 - (((double)actualTimeTaken.Ticks * 100) / (double)(fullTimeTaken.Ticks))));
            Console.WriteLine("MaxPass: {0} ({2})\tMinPass: {1} ({3})", maxTime, minTime, maxIndex, minIndex);
            Console.WriteLine("Resuming dual layout on TupleHelper took {0}", dualResume);
            Console.WriteLine("Disposal took {0}", sw.Elapsed);
        }

    }
}