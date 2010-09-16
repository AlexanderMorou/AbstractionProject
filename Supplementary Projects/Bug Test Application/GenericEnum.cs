using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Cli;
using System.Diagnostics;
using System.Dynamic;

namespace AllenCopeland.Abstraction.SupplimentaryProjects.BugTestApplication
{
    static class GenericEnum
    {
        [MTAThread]
        static void Main()
        {
            dynamic u = new ExpandoObject();
            u.Time = (Func<Action, TimeSpan>)Time;
            Action testMethod1 = TestMethod1;
            var people = new Person[] { new Person() { Age = 13, Name = "Test1" }, new Person() { Age = 19, Name = "Test2" } };
            var peopleQuery = from dynamic d in people
                              select (dynamic)new { Person = d, pName=d.Name };

            var initialTime = u.Time(testMethod1);
            var secondTime = u.Time(testMethod1);
            Console.WriteLine("Took {0} initially.", initialTime);
            Console.WriteLine("Took {0} alternatively.", secondTime);
        }

        private static TimeSpan Time(Action a)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            a();
            sw.Stop();
            return sw.Elapsed;
        }
        private static void TestMethod1()
        {
            var testAssembly = IntermediateGateway.CreateAssembly("TestAssembly1");
            
            testAssembly.DefaultNamespace = testAssembly.Namespaces.Add("AllenCopeland.Abstraction.SupplimentaryProjects.TestAssembly1");
            var testClass = (IIntermediateClassType)testAssembly.DefaultNamespace.Types.Add("TestClass", TypeKind.Class);
            var testMethod1 = testClass.Methods.Add("TestMethod");
            var personClass = testAssembly.DefaultNamespace.Classes.Add("Person");
            var ageField = personClass.Fields.Add(new TypedName("age", typeof(int).GetTypeReference()));
            var nameField = personClass.Fields.Add(new TypedName("name", typeof(string).GetTypeReference()));
            var ageProperty = personClass.Properties.Add(new TypedName("Age", typeof(int).GetTypeReference()), true, true);
            var nameProperty = personClass.Properties.Add(new TypedName("Name", typeof(string).GetTypeReference()), true, true);
            ageProperty.GetMethod.Return(ageField.GetReference());
            ageProperty.SetMethod.Assign(ageField.GetReference(), ageProperty.SetMethod.ValueParameter.GetReference());
            nameProperty.GetMethod.Return(nameField.GetReference());
            nameProperty.SetMethod.Assign(nameField.GetReference(), nameProperty.SetMethod.ValueParameter.GetReference());
            var local1 = testMethod1.Locals.Add(new TypedName("local1", personClass));
            var local1Ref = local1.GetReference();
            var localARef = local1Ref.GetProperty("Age");
            localARef.Name = "AgeAlt";
            var local2 = testMethod1.Locals.Add(new TypedName("local2", typeof(Person).GetTypeReference()));
            var local2Ref = local2.GetReference();
            var localBRef = local2Ref.GetProperty("Age");
            localBRef.Name = "Name";
            testAssembly.Dispose();
            CLIGateway.ClearCache();
        }
        private class Person
        {
            private int age;
            private string name;

            public int Age
            {
                get
                {
                    return this.age;
                }
                set
                {
                    this.age = value;
                }
            }

            public string Name
            {
                get
                {
                    return this.name;
                }
                set
                {
                    this.name = value;
                }
            }
        }
    }
}
