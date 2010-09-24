using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using System.Reflection.Emit;
namespace AllenCopeland.Abstraction.SupplimentaryProjects.BugTestApplication
{
    public static class SimpleCompilerTest 
    {
        static SimpleCompilerTest()
        {
            Console.WriteLine("Constructing add generic inference build method.");
        }
        internal static void Main()
        {
            //var t = typeof(int[, ,][][,][, , ,]).GetTypeReference();
            Stopwatch timer = new Stopwatch();
            Console.WriteLine("Preconstructing graph to handle JIT overhead.");
            TimeSpan codeObjectPreconstruction;
            TimeSpan disposePregraph;
            Process(timer, out codeObjectPreconstruction, out disposePregraph);
            TimeSpan codeObjectConstruction;
            TimeSpan disposeGraph;
            Process(timer, out codeObjectConstruction, out disposeGraph);
            Console.WriteLine("Times:");
            Console.WriteLine("{0,-25}: {1}", "Full JIT overhead: ", (codeObjectPreconstruction + disposePregraph) - (codeObjectConstruction + disposeGraph));
            Console.WriteLine("{0,-25}: {1}", "To construct code graph", codeObjectConstruction);
            Console.WriteLine("{0,-25}: {1}", "To dispose code graph", disposeGraph);

        }

        private static void Process(Stopwatch timer, out TimeSpan buildGraphTime, out TimeSpan disposeGraphTime)
        {
            timer.Start();
            var testGraph1 = BuildTestGraph1();
            timer.Stop();
            buildGraphTime = timer.Elapsed;
            timer.Reset();
            timer.Start();
            testGraph1.Dispose();
            timer.Stop();
            disposeGraphTime = timer.Elapsed;
            timer.Reset();
        }

        private static IIntermediateAssembly BuildTestGraph1()
        {
            var testAssembly                = IntermediateGateway.CreateAssembly("TestCompile1");
            var assemblyInfo                = testAssembly.AssemblyInformation;
            assemblyInfo.Culture            = CultureIdentifiers.English_UnitedStates;
            assemblyInfo.Copyright          = "Copyright \xA9 2010 Allen C. Copeland Jr.";
            assemblyInfo.Product            = "Abstraction Project\u2122";
            assemblyInfo.Trademark          = "The Abstraction Project\u2122 is a registered trademark of Allen C. Copeland Jr.";
            assemblyInfo.Description        = "A project to assist in building the Abstraction Project's\u2122 compiler foundation.";
            assemblyInfo.Title              = "Test Compile 1";
            testAssembly.DefaultNamespace   = testAssembly.Namespaces.Add("AllenCopeland.Abstraction.SupplimentaryProjects.TestCompile1");
            var programClass                = testAssembly.DefaultNamespace.Classes.Add("Program", GenericParameterData.EmptySet);
            var mainMethod = programClass.Methods.Add("Main", new TypedNameSeries(new TypedName("args", typeof(string[]).GetTypeReference())));
            programClass.SpecialModifier    = SpecialClassModifier.Static;
            string[] typeParamNames         = new string[] 
                { 
                    "TCtor", "TIntermediateCtor", "TEvent", "TIntermediateEvent", "TField", "TIntermediateField", "TIndexer", 
                    "TIntermediateIndexer", "TMethod", "TIntermediateMethod", "TProperty", "TIntermediateProperty", "TType", 
                    "TIntermediateType" 
                };

            var symbolTypes                 =
                (from name in typeParamNames
                 select name.GetSymbolType()).ToArray();
            mainMethod.Call(typeof(Console).GetTypeExpression().GetMethod("WriteLine"), string.Format("OILexer compiled this on {0}.", DateTime.Now).ToPrimitive());
            var testInference = AddTestMethod(programClass, typeParamNames, symbolTypes);
            testInference.IsStatic          = true;
            testInference.Call(typeof(Console).GetTypeExpression(), "WriteLine", testInference.Parameters["inst"].GetReference());
            var testItem = testInference.Locals.Add("test", testInference.Parameters["inst"].GetReference(), LocalTypingKind.Implicit);
            
            /* *
             * Below is an example of what I do *NOT* want the
             * framework to require as it's a compiler not strictly
             * a code-gen.
             * */
            var createAssemblyReference = typeof(IntermediateGateway).GetTypeReference<IClassType>().Methods.Find("CreateAssembly", true, IntermediateGateway.CommonlyUsedTypeReferences.String).First().Value.GetReference(typeof(IntermediateGateway).GetTypeExpression());
            var createAssemblyInvoke = createAssemblyReference.Invoke("TestAssembly".ToPrimitive());
            var assemblyPropertyReference = createAssemblyInvoke.GetProperty("Classes");
            var addMethodReference = typeof(IIntermediateTypeDictionary<IClassType, IIntermediateClassType>).GetTypeReference<IInterfaceType>().Methods.Find("Add", true, IntermediateGateway.CommonlyUsedTypeReferences.String).First();
            mainMethod.Call(testInference.GetReference(), addMethodReference.Value.GetReference(assemblyPropertyReference).Invoke("TestClass".ToPrimitive()));
            mainMethod.IsStatic             = true;
            return testAssembly;
        }

        private static Func<IIntermediateClassType, string[], ISymbolType[], IIntermediateClassMethodMember> AddTestMethod = BuildAddTestMethod().Compile();

        private static Expression<Func<IIntermediateClassType, string[], ISymbolType[], IIntermediateClassMethodMember>> BuildAddTestMethod()
        {
            const int tCtor                 = 0,
                      tIntermediateCtor     = 1,
                      tEvent                = 2,
                      tIntermediateEvent    = 3,
                      tField                = 4,
                      tIntermediateField    = 5,
                      tIndexer              = 6,
                      tIntermediateIndexer  = 7,
                      tMethod               = 8,
                      tIntermediateMethod   = 9,
                      tProperty             = 10,
                      tIntermediateProperty = 11,
                      tType                 = 12,
                      tIntermediateType     = 13;

            return (IIntermediateClassType programClass, string[] typeParamNames, ISymbolType[] symbolTypes) => programClass.Methods.Add("TestMethod", new TypedNameSeries(new TypedName("inst", typeof(IIntermediateInstantiableType<,,,,,,,,,,,,,>).GetTypeReference(false, symbolTypes))),
                new GenericParameterData(typeParamNames[tCtor], new IType[] 
                    { 
                        typeof(IConstructorMember<,>).GetTypeReference(false, 
                            symbolTypes[tCtor], 
                            symbolTypes[tType]) 
                    }),
                new GenericParameterData(typeParamNames[tIntermediateCtor], new IType[] 
                    {
                        symbolTypes[tCtor], 
                        typeof(IIntermediateConstructorMember<,,,>).GetTypeReference(false, 
                            symbolTypes[tCtor],
                            symbolTypes[tIntermediateCtor], 
                            symbolTypes[tType], 
                            symbolTypes[tIntermediateType])
                    }),
                new GenericParameterData(typeParamNames[tEvent], new IType[]
                    {
                        typeof(IEventMember<,>).GetTypeReference(false, 
                            symbolTypes[tEvent], 
                            symbolTypes[tType])
                    }),
                new GenericParameterData(typeParamNames[tIntermediateEvent], new IType[]
                    {   
                        symbolTypes[tEvent],
                        typeof(IIntermediateEventMember<,,,>).GetTypeReference(false, 
                            symbolTypes[tEvent],
                            symbolTypes[tIntermediateEvent], 
                            symbolTypes[tType], 
                            symbolTypes[tIntermediateType])
                    }),
                new GenericParameterData(typeParamNames[tField], new IType[]
                    {
                        typeof(IFieldMember<,>).GetTypeReference(false, 
                            symbolTypes[tField], 
                            symbolTypes[tType]),
                        typeof(IInstanceMember).GetTypeReference()
                    }),
                new GenericParameterData(typeParamNames[tIntermediateField], new IType[]
                    {   
                        symbolTypes[tField],
                        typeof(IIntermediateFieldMember<,,,>).GetTypeReference(false, 
                            symbolTypes[tField],
                            symbolTypes[tIntermediateField], 
                            symbolTypes[tType], 
                            symbolTypes[tIntermediateType])
                    }),
                new GenericParameterData(typeParamNames[tIndexer], new IType[]
                    {
                        typeof(IIndexerMember<,>).GetTypeReference(false, 
                            symbolTypes[tIndexer], 
                            symbolTypes[tType])
                    }),
                new GenericParameterData(typeParamNames[tIntermediateIndexer], new IType[]
                    {   
                        symbolTypes[tIndexer],
                        typeof(IIntermediateIndexerMember<,,,>).GetTypeReference(false, 
                            symbolTypes[tIndexer],
                            symbolTypes[tIntermediateIndexer], 
                            symbolTypes[tType], 
                            symbolTypes[tIntermediateType])
                    }),
                new GenericParameterData(typeParamNames[tMethod], new IType[]
                    {
                        typeof(IMethodMember<,>).GetTypeReference(false, 
                            symbolTypes[tMethod], 
                            symbolTypes[tType]),
                        typeof(IExtendedInstanceMember).GetTypeReference()
                    }),
                new GenericParameterData(typeParamNames[tIntermediateMethod], new IType[]
                    {   
                        symbolTypes[tMethod],
                        typeof(IIntermediateMethodMember<,,,>).GetTypeReference(false, 
                            symbolTypes[tMethod],
                            symbolTypes[tIntermediateMethod], 
                            symbolTypes[tType], 
                            symbolTypes[tIntermediateType])
                    }),
                new GenericParameterData(typeParamNames[tProperty], new IType[]
                    {
                        typeof(IPropertyMember<,>).GetTypeReference(false, 
                            symbolTypes[tProperty], 
                            symbolTypes[tType])
                    }),
                new GenericParameterData(typeParamNames[tIntermediateProperty], new IType[]
                    {   
                        symbolTypes[tProperty],
                        typeof(IIntermediatePropertyMember<,,,>).GetTypeReference(false, 
                            symbolTypes[tProperty],
                            symbolTypes[tIntermediateProperty], 
                            symbolTypes[tType], 
                            symbolTypes[tIntermediateType])
                    }),
                new GenericParameterData(typeParamNames[tType], new IType[]
                    {
                        typeof(IInstantiableType<,,,,,,>).GetTypeReference(false, 
                            symbolTypes[tCtor], 
                            symbolTypes[tEvent],
                            symbolTypes[tField], 
                            symbolTypes[tIndexer],
                            symbolTypes[tMethod],
                            symbolTypes[tProperty],
                            symbolTypes[tType])
                    }),
                new GenericParameterData(typeParamNames[tIntermediateType], new IType[]
                    {
                        symbolTypes[tType],
                        typeof(IIntermediateInstantiableType<,,,,,,,,,,,,,>).GetTypeReference(false, symbolTypes)
                    }));
        }

        private static void TestMethod<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer,
            TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType>(IIntermediateInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType> inst)
            where TCtor :
                IConstructorMember<TCtor, TType>
            where TIntermediateCtor :
                TCtor,
                IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
            where TEvent :
                IEventMember<TEvent, TType>
            where TIntermediateEvent :
                TEvent,
                IIntermediateEventMember<TEvent, TIntermediateEvent, TType, TIntermediateType>
            where TField :
                IFieldMember<TField, TType>,
                IInstanceMember
            where TIntermediateField :
                TField,
                IIntermediateFieldMember<TField, TIntermediateField, TType, TIntermediateType>
            where TIndexer :
                IIndexerMember<TIndexer, TType>
            where TIntermediateIndexer :
                TIndexer,
                IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TType, TIntermediateType>
            where TMethod :
                IMethodMember<TMethod, TType>,
                IExtendedInstanceMember
            where TIntermediateMethod :
                TMethod,
                IIntermediateMethodMember<TMethod, TIntermediateMethod, TType, TIntermediateType>
            where TProperty :
                IPropertyMember<TProperty, TType>
            where TIntermediateProperty :
                TProperty,
                IIntermediatePropertyMember<TProperty, TIntermediateProperty, TType, TIntermediateType>
            where TType :
                IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType>
            where TIntermediateType :
                TType,
                IIntermediateInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType>
        {

        }

    }
}
