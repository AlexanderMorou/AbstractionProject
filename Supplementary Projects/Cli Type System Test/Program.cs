using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.OwnerDrawnControls;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.Cil;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllenCopeland.Abstraction.Slf.SupplementaryProjects.TestCli
{
    static class Program
    {
        static void Main()
        {
            ReflectionTest();
            CliTypeSystemTest();
        }

        private static void TypeParamTest()
        {
            var identityManager = CliGateway.CreateIdentityManager(CliGateway.CurrentPlatform, CliGateway.CurrentVersion);
            var signature = new[] { identityManager.ObtainTypeReference(identityManager.RuntimeEnvironment.Int32), (IType)identityManager.ObtainTypeReference(identityManager.RuntimeEnvironment.String).MakeArray() };
            var program = (IClassType)identityManager.ObtainTypeReference(typeof(Program));
            var methodIdentifier = AstIdentifier.GetGenericSignatureIdentifier("Test", signature);
            var method = program.Methods[methodIdentifier];
            Console.WriteLine(method.IsDefined(identityManager.ObtainTypeReference(identityManager.RuntimeEnvironment.ExtensionMetadatum)));
            var iit = (IInterfaceType)identityManager.ObtainTypeReference(typeof(IIntermediateInstantiableType<,,,,,,,,,,,,,,>));
            var itp = iit.TypeParameters[AstIdentifier.GetGenericParameterIdentifier(14, true)];
            
            var constraints = itp.Constraints;
            Console.WriteLine(constraints[0].Equals(iit));
        }

        private static void TypeParamsQuery()
        {
            var identityManager = IntermediateGateway.CreateIdentityManager(CliGateway.CurrentPlatform, CliGateway.CurrentVersion);
            var assemblies = (from t in new[] { typeof(IType), typeof(ICliType), typeof(IIntermediateAssembly), typeof(IControlledCollection)}
                              select identityManager.ObtainAssemblyReference(t.Assembly)).ToArray();

            var typesQuery = (from a in assemblies
                              from t in a.GetTypes()
                              where t.IsGenericConstruct && t is IGenericType
                              let genericT = (IGenericType)t
                              from IGenericTypeParameter g in genericT.TypeParameters.Values
                              group g by t);
            foreach (var genericType in typesQuery)
            {
                Console.Write(genericType.Key.Name);
                Console.Write('<');
                bool first = true;
                foreach (var genericParameter in genericType)
                {
                    if (first)
                        first = false;
                    else
                        Console.Write(", ");
                    Console.Write(genericParameter.Name);
                }
                Console.WriteLine('>');
                foreach (var genericParameter in genericType)
                {
                    if (genericParameter.Constraints.Count > 0)
                    {
                        first = true;
                        Console.WriteLine("\twhere {0} :", genericParameter);
                        foreach (var constraint in genericParameter.Constraints)
                        {
                            if (first)
                                first = false;
                            else
                                Console.WriteLine(',');
                            Console.Write("\t\t{0}", constraint);
                        }
                        Console.WriteLine();
                    }
                }
            }
        }


        private static void ReflectionTest()
        {
            var assemblies = (from t in new[] { typeof(IType), typeof(ICliType), typeof(IIntermediateAssembly), typeof(IControlledCollection), typeof(ImageComboBox)}
                              select t.Assembly).ToArray();
            var typesQuery = (from a in assemblies
                              from t in a.GetTypes()
                              from m in t.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                              select m);
            Stopwatch sw = Stopwatch.StartNew();
            var types = typesQuery.ToArray();
            sw.Stop();

            //Type current = null;
            //foreach (var member in types)
            //{
            //    if (member.ReflectedType != current &&
            //        current != null)
            //    {
            //        Console.WriteLine();
            //        current = member.ReflectedType;
            //        Console.WriteLine(current);
            //    }
            //    else if (current == null)
            //    {
            //        current = member.ReflectedType;
            //        Console.WriteLine(current);
            //    }
            //    Console.WriteLine("\t{0}", member);
            //}
            Console.WriteLine("{0} at {1}ms", types.Length, sw.Elapsed.TotalMilliseconds);
            Console.WriteLine("Reflection took {0}ms to process query a second time.", MiscHelperMethods.CreateFunctionOfTime(typesQuery.Count)().Item1.TotalMilliseconds);
        }
        private static void Test(this int t, params string[] test)
        {

        }

        private static void CliTypeSystemTest()
        {
            var identityManager = IntermediateGateway.CreateIdentityManager(CliGateway.CurrentPlatform, CliGateway.CurrentVersion);
            var assemblies = (from t in new[] { typeof(IType), typeof(ICliType), typeof(IIntermediateAssembly), typeof(IControlledCollection), typeof(ImageComboBox) }
                              select identityManager.ObtainAssemblyReference(t.Assembly)).ToArray();

            foreach (var table in from ICliAssembly a in assemblies
                                  from t in a.MetadataRoot.TableStream.Values
                                  select t)
                table.Read();

            var typesQuery = (from a in assemblies
                              from t in a.GetTypes()
                              from m in t.Members.Values
                              select m.Entry);
            Stopwatch sw = Stopwatch.StartNew();
            var types = typesQuery.ToArray();
            sw.Stop();

            //IMemberParent current = null;
            //foreach (var member in types)
            //{
            //    if (member.Parent != current &&
            //        current != null)
            //    {
            //        Console.WriteLine();
            //        current = member.Parent;
            //        Console.WriteLine(current);
            //    }
            //    else if (current == null)
            //    {
            //        current = member.Parent;
            //        Console.WriteLine(current);
            //    }
            //    Console.WriteLine("\t{0}", member);
            //}
            Console.WriteLine("{0} at {1}ms", types.Length, sw.Elapsed.TotalMilliseconds);
            Console.WriteLine("Abstraction took {0}ms to process query a second time.", MiscHelperMethods.CreateFunctionOfTime(typesQuery.Count)().Item1.TotalMilliseconds);
        }
    }
}
