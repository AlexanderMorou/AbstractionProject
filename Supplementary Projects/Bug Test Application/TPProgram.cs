using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Tuples;
using AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq;
using System.Diagnostics;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.COM.InterOp;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using System.Reflection;
/*---------------------------------------------------------------------\
| Copyright © 2009 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.SupplimentaryProjects.BugTestApplication
{
    internal class A<B> :
        Dictionary<string, B>
    {
        internal class C<D> :
            A<D>
            where D :
                struct
        {
            public C(B a, params D?[] b)
            {
            }
        }
    }

    static class TPProgram
    {

        static void Main()
        {
            
            Console.WriteLine(LinqHelper.
                    From("child", (Symbol)"children").
                    Where(("child").Fuse("Age").GreaterThan(12.ToPrimitive())).
                    Select((Symbol)"child").Build());
            Test1();
            Test1();
            Test3();
        }

        private static void Test3()
        {            
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var toE = typeof(ComErrors).GetTypeReference<IClassType>();
            var resultProject = IntermediateGateway.CreateAssembly("Test");
            var ns = resultProject.Namespaces.Add("AllenCopeland.Abstraction.COM.InterOp");
            var testEnum = ns.Enums.Add("HResults");
            testEnum.BaseType = EnumerationBaseType.UInt32;
            var toHR = typeof(HResult).GetTypeReference();
            Stopwatch swReflection = new Stopwatch();
            var iiet = typeof(IList<>).GetTypeReference();
            foreach (var member in iiet.Members)
                Console.WriteLine(member.Value.Entry.Name);
            foreach (var property in toE.Properties)
            {
                swReflection.Start();
                var value = ((uint)(HResult)((ICompiledMethodMember)property.Value.GetMethod).MemberInfo.Invoke(null, new Object[0]));
                swReflection.Stop();
                var current = testEnum.Fields.Add(property.Key, value);
                //current.GetMethod.Return(value.ToPrimitive());
            }
            sw.Stop();
            Console.WriteLine("Took {0} to mirror {1} elements.", sw.Elapsed - swReflection.Elapsed, toE.Properties.Count);
                //testEnum.Fields.Add(property.Key, (uint)(HResult)((ICompiledMethodMember)property.Value.GetMethod).MemberInfo.Invoke(null, new Object[0]));
        }

        private static void Test1()
        {
            CLIGateway.ClearCache();
            Test1_2();
            
        }

        private static void Test1_2()
        {
            Stopwatch v = new Stopwatch();
            v.Start();

            var p = typeof(long).GetTypeReference();
            //Console.WriteLine(p.Assembly.Namespaces["System"].Types["Int64"].Entry == p);
            var dct = (IClassType)typeof(A<Form>).GetTypeReference();
            var enm = dct.Classes.Values.First(r => r.Name == "C").MakeGenericType(typeof(Form), typeof(int));
            var ctor = enm.Constructors[0].Value;
            v.Stop();
            Console.WriteLine(v.Elapsed);
            v.Reset();
            v.Start();
            //var trA = typeof(A<A<A<A<A<A<A<double>>>>>>>).GetTypeReference();
            var baseRef =typeof(A<>).GetTypeReference<IClassType>();
            var baseRef2 = typeof(A<>.C<>).GetTypeReference<IClassType>();
            var trB =
                baseRef2.MakeGenericType(
                    baseRef.MakeGenericType(
                        baseRef2.MakeGenericType(
                            baseRef.MakeGenericType(
                                baseRef2.MakeGenericType(
                                    baseRef.MakeGenericType(
                                        baseRef2.MakeGenericType(typeof(Double), typeof(Double))), typeof(Double).GetTypeReference())), typeof(Double).GetTypeReference())),
                                        typeof(int).GetTypeReference());
            var ctor2 = trB.Constructors[0].Value;
            var i1 = typeof(Dictionary<int, long>[]).GetTypeReference().ImplementedInterfaces;
            var i2 = typeof(IList<string>).GetTypeReference();
            var i4 = typeof(IList<IList<IList<A<IList<string>>>>>).GetTypeReference();
            var i3 = typeof(Dictionary<string[], double[]>).GetTypeReference().ImplementedInterfaces;
            v.Stop();
            Console.WriteLine(v.Elapsed);
            v.Reset();
            v.Start();
            ToyAssembly.Bridge.Register();
            var tAssem = IntermediateGateway.CreateAssembly<ToyAssembly>("TestAssembly");
            var tName = tAssem.Namespaces.Add("TestNamespace");
            var tClass = tName.Classes.Add("TestClass");
            v.Stop();
            Console.WriteLine(v.Elapsed);
            var ns = tClass.Namespace;
            Console.WriteLine(ns.FullName);

            Console.WriteLine();
        }
    }

    internal class ToyAssembly :
        IntermediateAssembly<ToyAssembly>
    {
        internal ToyAssembly(string name)
            : base(name)
        {
        }

        internal ToyAssembly(ToyAssembly rootAssembly)
            : base(rootAssembly)
        {
        }

        protected override ToyAssembly GetNewPart()
        {
            return new ToyAssembly(this);
        }

        internal class Bridge :
            IntermediateGateway.ICreateAssemblyBridge<ToyAssembly>
        {
            private static bool registered = false;
            private static Bridge singleTon = new Bridge();
            #region ICreateAssemblyBridge<ToyAssembly> Members

            ToyAssembly IntermediateGateway.ICreateAssemblyBridge<ToyAssembly>.ctor(string name)
            {
                return new ToyAssembly(name);
            }

            #endregion

            internal static void Register()
            {
                if (Bridge.registered)
                    return;
                Bridge.registered = true;
                singleTon.RegisterCreateAssemblyBridge();
            }
        }
    }


}
