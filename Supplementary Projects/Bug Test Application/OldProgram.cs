using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using AllenCopeland.Abstraction.OwnerDrawnControls;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Gateways.Cli;
using AllenCopeland.Abstraction.Slf.Gateways.Oil;
using AllenCopeland.Abstraction.Slf.Languages.Metadata;
using AllenCopeland.Abstraction.Slf.TypeSystems.Abstract;
using AllenCopeland.Abstraction.Slf.TypeSystems.Abstract.Members;
using AllenCopeland.Abstraction.Slf.TypeSystems.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Tuples;
/*----------------------------------------\
| Copyright © 2009 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.SupplimentaryProjects.BugTestApplication
{
    static class OldProgram
    {
        static void IntermediateTest()
        {
            ISymbolType ist = "Dictionary".GetSymbolType("System.Collections.Generic", new TypeCollection("String".GetSymbolType(), "Form".GetSymbolType()));
            Console.WriteLine(ist.FullName);
            var myAssembly = IntermediateGateway.CreateAssembly("myAssembly");
            myAssembly.AssemblyInformation.AssemblyVersion = new Version(1, 0);
            var defaultNamespace = myAssembly.Namespaces.Add("MyProgram.DefaultNamespace");
            var testClass = defaultNamespace.Classes.Add("TestClass");
            var bitwiseAndOp = testClass.BinaryOperatorCoercions.Add(CoercibleBinaryOperators.BitwiseAnd, BinaryOpCoercionContainingSide.RightSide, typeof(long).GetTypeReference(), testClass);
            var testMethod = testClass.Methods.Add("test", new TypedNameSeries() 
            { 
                { 
                    "testParam1", 
                    "Int64",
                    ParameterDirection.Reference
                },
                {
                    "testParam2",
                    "Form"
                }
            });
            testClass.CustomAttributes.Add(new CustomAttributeDefinition.ParameterValueCollection(typeof(ExtensionAttribute).GetTypeReference()));
            var testParam1 = testMethod.Parameters["testParam1"];
            testParam1.CustomAttributes.Add(new CustomAttributeDefinition.ParameterValueCollection(typeof(ParamArrayAttribute).GetTypeReference()));
            bitwiseAndOp.AccessLevel = AccessLevelModifiers.InternalProtected;
            Console.WriteLine(bitwiseAndOp);
            Console.WriteLine(testMethod);
        }

        private static void DoTest()
        {
            Console.WriteLine("Adding mscorlib.dll");
            IAssemblyWorkspace iaw = new AssemblyWorkspace(typeof(long).Assembly.GetAssemblyReference());
            /*
            iaw.References.Add(typeof(IAssemblyWorkspace).Assembly.GetAssemblyReference());
            iaw.References.Add(typeof(ICompiledType).Assembly.GetAssemblyReference());
            iaw.References.Add(typeof(IIntermediateAssembly).Assembly.GetAssemblyReference());
            iaw.References.Add(typeof(ImageComboBox).Assembly.GetAssemblyReference());
            iaw.References.Add(typeof(Tuple<>).Assembly.GetAssemblyReference());
            iaw.References.Add(typeof(LanguageMetaHelper).Assembly.GetAssemblyReference());
            //*/
            //*
            Console.WriteLine("There are {0} namespaces in total", CountNamespaces(iaw));
            iaw.AddAssembly(typeof(Uri).Assembly.GetAssemblyReference());
            iaw.AddAssembly(typeof(IGrouping<,>).Assembly.GetAssemblyReference());
            iaw.AddAssembly(typeof(Point).Assembly.GetAssemblyReference());
            iaw.AddAssembly(typeof(Form).Assembly.GetAssemblyReference());
            //*/
            iaw.AddAssembly(typeof(IAssemblyWorkspace).Assembly.GetAssemblyReference());
            iaw.AddAssembly(typeof(ICompiledType).Assembly.GetAssemblyReference());
            iaw.AddAssembly(typeof(IIntermediateAssembly).Assembly.GetAssemblyReference());
            iaw.AddAssembly(typeof(ImageComboBox).Assembly.GetAssemblyReference());
            iaw.AddAssembly(typeof(Tuple<>).Assembly.GetAssemblyReference());
            iaw.AddAssembly(typeof(LanguageMetaHelper).Assembly.GetAssemblyReference());

            //Iterate(0, iaw);
        }

        public static void AddAssembly(this IAssemblyWorkspace target, IAssembly assembly)
        {
            Console.WriteLine("Adding {0}", assembly.ManifestModule.Name);
            target.References.Add(assembly);
            Console.WriteLine("There are {0} namespaces in total", CountNamespaces(target));
        }

        private static int CountNamespaces(INamespaceParent target)
        {
            int result = 0;
            result += target.Namespaces.Count;
            foreach (var item in target.Namespaces.Values)
                result += CountNamespaces(item);
            return result;
        }

        private static void Iterate(int depth, INamespaceParent parent)
        {
            string k = ' '.Repeat(depth * 4);
            foreach (var ns in parent.Namespaces.Values)
            {
                Console.WriteLine("{0}{1}", k, ns.Name);
                Iterate(depth + 1, (INamespaceParent)ns);
                Iterate(depth + 1, (ITypeParent)ns);
            }
        }

        private static void Iterate(int depth, ITypeParent parent)
        {
            string k = ' '.Repeat(depth * 4);
            foreach (var @class in parent.Classes.Values)
            {
                WriteTypeName(k, @class);
                if (@class.Type != TypeKind.Ambiguity)
                    Iterate(depth + 1, @class);
            }
            foreach (var @delegate in parent.Delegates.Values)
                WriteTypeName(k, @delegate);
            foreach (var @enum in parent.Enums.Values)
                WriteTypeName(k, @enum);
            foreach (var @interface in parent.Interfaces.Values)
                WriteTypeName(k, @interface);
            foreach (var @struct in parent.Structs.Values)
            {
                WriteTypeName(k, @struct);
                if (@struct.Type != TypeKind.Ambiguity)
                    Iterate(depth + 1, @struct);
            }
        }

        private static void WriteTypeName(string lead, IType type)
        {
            ConsoleColor cc = Console.ForegroundColor;
            if (type.AccessLevel == AccessLevelModifiers.Public || type.AccessLevel == AccessLevelModifiers.Protected)
                switch (type.Type)
                {
                    case TypeKind.Class:
                    case TypeKind.Delegate:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case TypeKind.Enumerator:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case TypeKind.Interface:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                    case TypeKind.Struct:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case TypeKind.Ambiguity:
                        if (type is IClassType ||
                            type is IDelegateType)
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        else if (type is IEnumType)
                            Console.ForegroundColor = ConsoleColor.Blue;
                        else if (type is IInterfaceType)
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        else if (type is IStructType)
                            Console.ForegroundColor = ConsoleColor.Green;
                        else
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case TypeKind.Other:
                    default:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                }
            else
                switch (type.Type)
                {
                    case TypeKind.Class:
                    case TypeKind.Delegate:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        break;
                    case TypeKind.Enumerator:
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                    case TypeKind.Interface:
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        break;
                    case TypeKind.Struct:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    case TypeKind.Ambiguity:
                        if (type is IClassType ||
                            type is IDelegateType)
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        else if (type is IEnumType)
                            Console.ForegroundColor = ConsoleColor.Blue;
                        else if (type is IInterfaceType)
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        else if (type is IStructType)
                            Console.ForegroundColor = ConsoleColor.Green;
                        else
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case TypeKind.Other:
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        break;
                }
            Console.Write("{0}{1}", lead, type.Name);
            if (type.Type == TypeKind.Ambiguity)
                if (type.AccessLevel == AccessLevelModifiers.Public || type.AccessLevel == AccessLevelModifiers.Protected)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
            switch (type.Type)
            {
                case TypeKind.Class:
                    if (type.BaseType != null)
                        Console.Write(" ({0}, {1})", type.BaseType.Name, type.Assembly.Name);
                    else
                        goto default;
                    break;
                case TypeKind.Ambiguity:
                    Console.Write(" ({");
                    IAmbiguousType aType = ((IAmbiguousType)(type));
                    for (int i = 0; i < aType.Source.Count; i++)
                    {
                        if (i > 0)
                            Console.Write(", ");
                        Console.Write(aType.Source[i].Assembly.Name);
                    }
                    Console.Write("})");
                    break;
                default:
                    Console.Write(" ({0})", type.Assembly.Name);
                    break;
            }
            Console.WriteLine();
            Console.ForegroundColor = cc;
        }

        private static void OldTest()
        {
            //Initialize the CLI type-system.
            MyTest();
            //Make the test valid.
            CLIGateway.ClearCache();
            //Obtain a type instance.
            var genericTester = typeof(Dictionary<string, IDictionary<Single, IList<IClassType>>>[]);
            //Obtain a stopwatch for time testing.
            Stopwatch myStopWatch = new Stopwatch();
            myStopWatch.Start();
            //Obtain a type-reference through the CLI type-system.
            IType genericReference = genericTester.GetTypeReference();
            myStopWatch.Stop();
            TimeSpan getTypeReference = myStopWatch.Elapsed;
            Console.WriteLine("CLI Type System:");
            Console.WriteLine(genericTester);
            myStopWatch.Reset();
            myStopWatch.Start();
            Console.WriteLine(genericTester.GetElementType().GetGenericTypeDefinition().MakeGenericType(typeof(long), typeof(double)));
            myStopWatch.Stop();
            TimeSpan makeGenericTypeCLI = myStopWatch.Elapsed;
            Console.WriteLine();
            Console.WriteLine("Abstraction CLI Type System:");
            Console.WriteLine(genericReference);
            myStopWatch.Reset();
            myStopWatch.Start();
            Console.WriteLine(((IClassType)genericReference.ElementType).ElementType.MakeGenericType(typeof(long), typeof(double)));
            myStopWatch.Stop();
            TimeSpan makeGenericTypeAbsCLI = myStopWatch.Elapsed;

            Type typeFromString = Type.GetType(genericReference.FullName, false, true);

            Console.WriteLine();
            Console.WriteLine("Getting type from string of Abstraction Type System:");
            if (Object.ReferenceEquals(typeFromString, genericTester))
                Console.WriteLine("Same reference as before.");
            Console.WriteLine(typeFromString == null ? "<null>" : typeFromString.ToString());
            Console.WriteLine();
            Console.WriteLine("CLIGateway.GetTypeReference took       : {0}", getTypeReference);
            Console.WriteLine("MakeGenericType (CLI) took             : {0}", makeGenericTypeCLI);
            Console.WriteLine("MakeGenericType (Abstraction CLI) took : {0}", makeGenericTypeAbsCLI);
            Console.ReadKey(true);
        }
        private static void MyTest()
        {
            IType testType = typeof(decimal).GetTypeReference();
            IType testType2 = ((IGenericType)typeof(IList<>).GetTypeReference()).MakeGenericType(testType);
        }
    }
}
