using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.OwnerDrawnControls;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;
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
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public class TestAttribute :
        Attribute
    {
        private AccessLevelModifiers[] modifiers;
        public TestAttribute(params AccessLevelModifiers[] modifiers)
        {
            this.modifiers = modifiers;
        }

        public object[] TestAttributeValue { get; set; }
        public object M { get; set; }
    }

    [Test(AccessLevelModifiers.Public, AccessLevelModifiers.ProtectedOrInternal, TestAttributeValue = new object[] { "Test", typeof(___3.___3), AttributeTargets.Method | AttributeTargets.Property, new AccessLevelModifiers[] { AccessLevelModifiers.Public, AccessLevelModifiers.Private, AccessLevelModifiers.Internal }, new object[] { new string[] { "Test", "Decimal", "Test", "test" }, new int[] { 81, 82, 83, 84, 85, 86 } } })]//TestAttributeValue = new object[] { "Test", typeof(___3.___3), new AttributeTargets[] { AttributeTargets.Class, AttributeTargets.Method, AttributeTargets.Property }, new object[] { new string[] { "Test", "Decimal" }, new int[] { 81, 82, 83, 84, 85, 86 } } }, 
    class Program
    {
        private static ICliManager identityManager = IntermediateGateway.CreateIdentityManager(CliGateway.CurrentPlatform, CliGateway.CurrentVersion);
        static void Main()
        {

            var identityManager = CliGateway.CreateIdentityManager(CliGateway.CurrentPlatform, CliGateway.CurrentVersion);
            var targetType = (IClassType)typeof(Program).GetTypeReference(identityManager);
            var metadataTarget = targetType;
            var attribute = metadataTarget.Metadata[0];
            var metadataUsage = identityManager.MetadatumHandler.GetMetadatumInfo(attribute.Type);
            var namedParamM = (from m in attribute.NamedParameters
                               where m.Item2 == "TestAttributeValue"
                               select m).FirstOrDefault();
            //Console.WriteLine(namedParamM.Item3);
            //typeof(Program).GetTypeReference();
            //var typeId = TIFlatDFARules.QualifiedTypeNamePointer();
            //Console.Write(sb);
            //MemberTest();
            //Console.WriteLine("Eliminating JIT overhead");
            //CliTypeSystemTest();
            //Console.Clear();
            //test:
            //Console.WriteLine("Running Abstraction Test...");
            //CliTypeSystemTest();
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //Console.ReadKey(true);
            //Console.WriteLine("Running Reflection Test...");
            //ReflectionTest();
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //Console.ReadKey(true);
            //goto test;
        }

        private static void MemberTest()
        {
            var identityManager = CliGateway.CreateIdentityManager(CliGateway.CurrentPlatform, CliGateway.CurrentVersion);
            var m = (IClassType)identityManager.ObtainTypeReference(typeof(IntermediateClassType));
            Console.WriteLine(m.Members.Count);
            var gmTF = MiscHelperMethods.CreateFunctionOfTime<IClassType, IScopedDeclaration[]>(GetMembers);
            var first = gmTF(m);
            var second = gmTF(m);
            foreach (IScopedDeclaration decl in first.Item2)
                Console.WriteLine("{0} {1}\n", decl.AccessLevel, decl);

            Console.WriteLine("Pre JIT-retrieval: {0}ms", first.Item1.TotalMilliseconds);
            Console.WriteLine("Post JIT-retrieval: {0}ms", second.Item1.TotalMilliseconds);
        }

        private static IScopedDeclaration[] GetMembers(IClassType m)
        {
            var query = (from IScopedDeclaration member in m.GetAvailableMembersFor(AccessLevelModifiers.ProtectedAndInternal)
                         orderby member.AccessLevel ascending
                         //        member.Name ascending
                         select member).ToArray();
            return query;
        }

        private static void TypeParamsQuery()
        {
            var identityManager = IntermediateGateway.CreateIdentityManager(CliGateway.CurrentPlatform, CliGateway.CurrentVersion);
            var assemblies = (from t in new[] { typeof(IType), typeof(ICliType), typeof(IIntermediateAssembly), typeof(IControlledCollection), typeof(Program) }
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
            /* *
             * Don't try to beat reflection on the assemblies it has loaded already:
             * the default libraries are likely loaded upon runtime load.
             * */
            var assemblies = (from t in new[] { typeof(Form), typeof(IType), typeof(ICliType), typeof(IIntermediateAssembly), typeof(IControlledCollection), typeof(ImageComboBox), typeof(Program) }
                              select t.Assembly).ToArray();
            Stopwatch sw = Stopwatch.StartNew();
            var typesQuery = (from a in assemblies
                              from t in a.GetTypes()
                              from m in t.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                              select m).Concat(from a in assemblies
                                               from t in a.GetTypes()
                                               select t);
            var types = typesQuery.ToArray();
            sw.Stop();
            var mainTime = sw.Elapsed;
            sw.Restart();
            var attributes = (from t in typesQuery
                              let tca = t.CustomAttributes.ToArray()
                              from c in tca
                              select new { c, P = c.ConstructorArguments, NP = c.NamedArguments }).ToArray();
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
            Console.WriteLine("{0} members at {1}ms", types.Length, mainTime);
            Console.WriteLine("{0} custom attributes at {1}ms", attributes.Length, sw.Elapsed.TotalMilliseconds);
            Console.WriteLine("{0} at {1}ms", types.Length, sw.Elapsed.TotalMilliseconds);
            Console.WriteLine("Reflection took {0}ms to process query a second time.", MiscHelperMethods.CreateFunctionOfTime(typesQuery.Count)().Item1.TotalMilliseconds);
        }
        internal static void CliTypeSystemTest()
        {
            Stopwatch sw = Stopwatch.StartNew();
            var identityManager = Program.identityManager;
            var assemblies = (from t in new[] { typeof(Form), typeof(IType), typeof(ICliType), typeof(IIntermediateAssembly), typeof(IControlledCollection), typeof(ImageComboBox), typeof(Program) }
                              select identityManager.ObtainAssemblyReference(t.Assembly)).ToArray();

            //foreach (var table in from ICliAssembly a in assemblies
            //                      from t in a.MetadataRoot.TableStream.Values
            //                      select t)
            //    table.Read();

            var typesQuery = (from a in assemblies
                              from t in a.GetTypes()
                              from m in t.Members.Values
                              select (IDeclaration)m.Entry);
            typesQuery = typesQuery.Concat(from m in typesQuery
                                           let p = m as IPropertySignatureMember
                                           where p != null
                                           from pm in (
                                            p.CanRead ?
                                                p.CanWrite ? new[] { p.GetMethod, p.SetMethod } :
                                                             new[] { p.GetMethod } :
                                            p.CanWrite ? new[] { p.SetMethod } : new IPropertySignatureMethodMember[0])
                                           select pm);
            typesQuery = typesQuery.Concat(from m in typesQuery
                                           let e = m as IEventSignatureMember
                                           where e != null
                                           from em in new [] { e.OnAddMethod, e.OnRemoveMethod }
                                           select em);
            typesQuery = typesQuery.Concat(from a in assemblies
                                           from t in a.GetTypes()
                                           select (IDeclaration)t);
            var types = typesQuery.ToArray();
            sw.Stop();
            var mainTime = sw.Elapsed;
            sw.Restart();
            var metadataQuery = (from IMetadataEntity d in from q in typesQuery
                                                           where q is IMetadataEntity
                                                           select q
                                 let md = d.Metadata
                                 from m in md
                                 select new { m, P = m.Parameters.ToArray(), NP = m.NamedParameters.ToArray() }).ToArray();
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
            Console.WriteLine("{0} members at {1}ms", types.Length, mainTime);
            Console.WriteLine("{0} custom attributes at {1}ms", metadataQuery.Length, sw.Elapsed.TotalMilliseconds);
            Console.WriteLine("Abstraction took {0}ms to process query a second time.", MiscHelperMethods.CreateFunctionOfTime(typesQuery.Count)().Item1.TotalMilliseconds);
        }
    }
}
