using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using Microsoft.VisualBasic;
namespace AllenCopeland.Abstraction.SupplimentaryProjects.BugTestApplication
{
    public static class SimpleCompilerTest 
    {
        internal static void Main(string[] args)
        {
            //var t = typeof(int[, ,][][,][, , ,]).GetTypeReference();
            //Stopwatch timer = new Stopwatch();
            //Console.WriteLine("Preconstructing graph to handle JIT overhead.");
            //TimeSpan codeObjectPreconstruction;
            //TimeSpan disposePregraph;
            //Process(timer, out codeObjectPreconstruction, out disposePregraph);
            //TimeSpan codeObjectConstruction;
            //TimeSpan disposeGraph;
            //Process(timer, out codeObjectConstruction, out disposeGraph);
            //Console.WriteLine("Times:");
            //Console.WriteLine("{0,-25}: {1}", "Full JIT overhead: ", (codeObjectPreconstruction + disposePregraph) - (codeObjectConstruction + disposeGraph));
            //Console.WriteLine("{0,-25}: {1}", "To construct code graph", codeObjectConstruction);
            //Console.WriteLine("{0,-25}: {1}", "To dispose code graph", disposeGraph);
            IIntermediateDynamicHandler handler = IntermediateGateway.CreateDynamicHandler();
            var u = handler.Builder;
            var method1 = handler.Methods.Add("Main", new TypedNameSeries(new TypedName("args", typeof(string[]).GetTypeReference())));
            var m1Args = method1.Parameters["args"];
            method1.ScopeCoercions.AddStaticName("System.Console");
            
            method1.Call("WriteLine".Fuse(new IExpression[] { m1Args.GetReference().GetProperty("Length") }));
            var m1 = method1.Compile<string[]>();
            
            m1(args);
            //IIntermediateDynamicMethod u = null;
            //var p = u.Compile<string[]>();
            //p(args);
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
            if (AddTestMethod == null)
                AddTestMethod = BuildAddTestMethod().Compile();
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
            var mainMethod = programClass.Methods.Add("Main", new TypedNameSeries(new TypedName("args", "String".GetSymbolType().MakeArray())));
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
            mainMethod.Call("Console".Fuse("WriteLine").Fuse(string.Format("OILexer compiled this on {0}.", DateTime.Now).ToPrimitive()));
            var testInference = AddTestMethod(programClass, typeParamNames, symbolTypes);
            testInference.IsStatic          = true;
            testAssembly.ScopeCoercions.AddNames(
                "AllenCopeland.Abstraction.Slf.Oil",
                "AllenCopeland.Abstraction.Slf.Abstract.Members",
                "System",
                "AllenCopeland.Abstraction.Slf.Abstract",
                "AllenCopeland.Abstraction.Slf.Oil.Members",
                "Microsoft.VisualBasic");
            testInference.Call("Console".Fuse("WriteLine").Fuse(new IExpression[]{"inst".GetSymbolExpression()}));
            //var testItem = testInference.Locals.Add("test", testInference.Parameters["inst"].GetReference(), LocalTypingKind.Implicit);
            /* *
             * Below is an example of how easy I want the objects to be to weave together.
             * */
            var assemblyLocal = mainMethod.Locals.Add("assembly", "IntermediateGateway".Fuse(
                    "CreateAssembly").Fuse(
                        "TestAssembly".ToPrimitive()));
            assemblyLocal.AutoDeclare = false;
            mainMethod.DefineLocal(assemblyLocal);
            /* *
             * var defaultNamespace = assembly.DefaultNamespace = assembly.Namespaces.Add("AllenCopeland.Abstraction.SupplimentaryProjects.TestCompile2");
             * */
            var dNamespaceLocal = mainMethod.Locals.Add("defaultNamespace",
                "assembly".Fuse("DefaultNamespace")
                    .Assign(
                        //Strings fuse differently based upon whether they're wrapped up into a primitive or not at a compilation level.
                        "assembly".Fuse("Namespaces").Fuse("Add").Fuse("AllenCopeland.Abstraction.SupplimentaryProjects.TestCompile2".ToPrimitive())));
            dNamespaceLocal.AutoDeclare = false;
            mainMethod.DefineLocal(dNamespaceLocal);
            mainMethod.Call(
                "TestMethod".Fuse(
                    "defaultNamespace".Fuse(
                        "Classes").Fuse(
                            "Add").Fuse("TestClass".ToPrimitive())));
            /* *
             * Below is an example of what I do *NOT* want the
             * framework to require as it's a compiler not strictly
             * a code-gen.
             * * 
             * var createAssemblyReference = typeof(IntermediateGateway).GetTypeReference<IClassType>().Methods.Find("CreateAssembly", true, IntermediateGateway.CommonlyUsedTypeReferences.String).First().Value.GetReference(typeof(IntermediateGateway).GetTypeExpression());
             * var createAssemblyInvoke = createAssemblyReference.Invoke("TestAssembly".ToPrimitive());
             * var addMethodReference = typeof(IIntermediateTypeDictionary<IClassType, IIntermediateClassType>).GetTypeReference<IInterfaceType>().Methods.Find("Add", true, IntermediateGateway.CommonlyUsedTypeReferences.String).First();
             * mainMethod.Call(testInference.GetReference(), 
             *  typeof(IIntermediateTypeDictionary<IClassType, IIntermediateClassType>).GetTypeReference<IInterfaceType>().Methods.Find("Add", true, IntermediateGateway.CommonlyUsedTypeReferences.String).First().Value.GetReference(
             *      createAssemblyInvoke.GetProperty("Classes")).Invoke("TestClass".ToPrimitive()));
             * */
            mainMethod.IsStatic             = true;
            
            return testAssembly;
        }

        private static AddTestMethodSignature AddTestMethod;

        private delegate IIntermediateClassMethodMember AddTestMethodSignature(IIntermediateClassType programClass, string[] typeParamNames, ISymbolType[] symbolTypes);

        private static Expression<AddTestMethodSignature> BuildAddTestMethod()
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

            return (IIntermediateClassType programClass, string[] typeParamNames, ISymbolType[] symbolTypes) => programClass.Methods.Add("TestMethod", new TypedNameSeries(new TypedName("inst", "IIntermediateInstantiableType".GetSymbolType(symbolTypes))),
                new GenericParameterData(typeParamNames[tCtor], new IType[] 
                    { 
                        "IConstructorMember".GetSymbolType(
                            symbolTypes[tCtor], 
                            symbolTypes[tType]) 
                    }),
                new GenericParameterData(typeParamNames[tIntermediateCtor], new IType[] 
                    {
                        symbolTypes[tCtor], 
                        "IIntermediateConstructorMember".GetSymbolType(
                            symbolTypes[tCtor],
                            symbolTypes[tIntermediateCtor], 
                            symbolTypes[tType], 
                            symbolTypes[tIntermediateType])
                    }),
                new GenericParameterData(typeParamNames[tEvent], new IType[]
                    {
                        "IEventMember".GetSymbolType(
                            symbolTypes[tEvent], 
                            symbolTypes[tType])
                    }),
                new GenericParameterData(typeParamNames[tIntermediateEvent], new IType[]
                    {   
                        symbolTypes[tEvent],
                        "IIntermediateEventMember".GetSymbolType(
                            symbolTypes[tEvent],
                            symbolTypes[tIntermediateEvent], 
                            symbolTypes[tType], 
                            symbolTypes[tIntermediateType])
                    }),
                new GenericParameterData(typeParamNames[tField], new IType[]
                    {
                        "IFieldMember".GetSymbolType(
                            symbolTypes[tField], 
                            symbolTypes[tType]),
                        "IInstanceMember".GetSymbolType()
                    }),
                new GenericParameterData(typeParamNames[tIntermediateField], new IType[]
                    {   
                        symbolTypes[tField],
                        "IIntermediateFieldMember".GetSymbolType(
                            symbolTypes[tField],
                            symbolTypes[tIntermediateField], 
                            symbolTypes[tType], 
                            symbolTypes[tIntermediateType])
                    }),
                new GenericParameterData(typeParamNames[tIndexer], new IType[]
                    {
                        "IIndexerMember".GetSymbolType(
                            symbolTypes[tIndexer], 
                            symbolTypes[tType])
                    }),
                new GenericParameterData(typeParamNames[tIntermediateIndexer], new IType[]
                    {   
                        symbolTypes[tIndexer],
                        "IIntermediateIndexerMember".GetSymbolType(
                            symbolTypes[tIndexer],
                            symbolTypes[tIntermediateIndexer], 
                            symbolTypes[tType], 
                            symbolTypes[tIntermediateType])
                    }),
                new GenericParameterData(typeParamNames[tMethod], new IType[]
                    {
                        "IMethodMember".GetSymbolType(
                            symbolTypes[tMethod], 
                            symbolTypes[tType]),
                        "IExtendedInstanceMember".GetSymbolType()
                    }),
                new GenericParameterData(typeParamNames[tIntermediateMethod], new IType[]
                    {   
                        symbolTypes[tMethod],
                        "IIntermediateMethodMember".GetSymbolType(
                            symbolTypes[tMethod],
                            symbolTypes[tIntermediateMethod], 
                            symbolTypes[tType], 
                            symbolTypes[tIntermediateType])
                    }),
                new GenericParameterData(typeParamNames[tProperty], new IType[]
                    {
                        "IPropertyMember".GetSymbolType(
                            symbolTypes[tProperty], 
                            symbolTypes[tType])
                    }),
                new GenericParameterData(typeParamNames[tIntermediateProperty], new IType[]
                    {   
                        symbolTypes[tProperty],
                        "IIntermediatePropertyMember".GetSymbolType(
                            symbolTypes[tProperty],
                            symbolTypes[tIntermediateProperty], 
                            symbolTypes[tType], 
                            symbolTypes[tIntermediateType])
                    }),
                new GenericParameterData(typeParamNames[tType], new IType[]
                    {
                        "IInstantiableType".GetSymbolType(
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
                        "IIntermediateInstantiableType".GetSymbolType(symbolTypes)
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
