using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Collections.Generic;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic;
using AllenCopeland.Abstraction.Slf.Languages;
using System.Diagnostics;

namespace AbstractionTestSystem
{
    [TestClass]
    public class IntermediateTests
    {
        private IIntermediateCliManager identityManager;
        private ICSharpProvider csProvider;
        private ICoreVisualBasicProvider cvbProvider;
        private IMyVisualBasicProvider mvbProvider;

        public void CheckManager()
        {
            if (identityManager == null)
                identityManager = IntermediateGateway.CreateIdentityManager(CliGateway.CurrentPlatform);
        }

        public void CheckProviders()
        {
            this.CheckCoreVBProvider();
            this.CheckMyVBProvider();
            this.CheckCSProvider();
        }

        private void CheckCoreVBProvider()
        {
            this.CheckManager();
            if (this.cvbProvider == null)
                this.cvbProvider = LanguageVendors.Microsoft.GetVisualBasicLanguage().GetProvider(VisualBasicVersion.CurrentVersion, this.identityManager);
        }
        private void CheckMyVBProvider()
        {
            this.CheckManager();
            if (this.mvbProvider == null)
                this.mvbProvider = LanguageVendors.Microsoft.GetVisualBasicLanguage().GetMyProvider(VisualBasicVersion.CurrentVersion, this.identityManager);
        }
        private void CheckCSProvider()
        {
            this.CheckManager();
            if (this.csProvider == null)
                this.csProvider = LanguageVendors.Microsoft.GetCSharpLanguage().GetProvider(identityManager);
        }

        [TestMethod]
        public void TestIntermediateClassType()
        {
            this.CheckManager();
            this.CheckProviders();
            this.TestIntermediateClassType(this.cvbProvider.CreateAssembly("TestVBAssembly"));
            this.TestIntermediateClassType(this.mvbProvider.CreateAssembly("TestVBMyAssembly"));
            this.TestIntermediateClassType(this.csProvider.CreateAssembly("TestCSAssembly"));
        }

        public void TestIntermediateClassType(IIntermediateAssembly targetAssembly)
        {
            var defaultNamespace = targetAssembly.DefaultNamespace = targetAssembly.Namespaces.Add("Abstraction.SupplementaryProjects.UnitTests.IntermediateTypeSystem");
            var intermediateClass = defaultNamespace.Classes.Add("IntermediateClass");
            var voidMethod = intermediateClass.Methods.Add("VoidMethod");
            Debug.Assert(voidMethod.ReturnType == identityManager.ObtainTypeReference(RuntimeCoreType.VoidType));
            var returnMethod = intermediateClass.Methods.Add(new TypedName("ReturnMethod", identityManager.ObtainTypeReference(RuntimeCoreType.String)));
            returnMethod.Return("TestReturn".ToPrimitive());

            var writeOnlyProperty = intermediateClass.Properties.Add(new TypedName("IntermediateWriteOnlyProperty", identityManager.ObtainTypeReference(RuntimeCoreType.Int32)), false, true);
            Debug.Assert(!writeOnlyProperty.CanRead && writeOnlyProperty.CanWrite);
            var readOnlyProperty = intermediateClass.Properties.Add(new TypedName("IntermediateReadOnlyProperty", identityManager.ObtainTypeReference(RuntimeCoreType.Int64)), true, false);
            Debug.Assert(readOnlyProperty.CanRead && !readOnlyProperty.CanWrite);
            var regularProperty = intermediateClass.Properties.Add(new TypedName("IntermediateProperty", identityManager.ObtainTypeReference(RuntimeCoreType.Int16)), true, true);
            Debug.Assert(regularProperty.CanRead && regularProperty.CanWrite);

            var writeOnlyIndexer = intermediateClass.Indexers.Add(new TypedName("IntermediateWriteOnlyIndexer", identityManager.ObtainTypeReference(RuntimeCoreType.Int32)), new TypedNameSeries(new TypedName("index", identityManager.ObtainTypeReference(RuntimeCoreType.Int16))), false, true);
            Debug.Assert(!writeOnlyIndexer.CanRead);
            var readOnlyIndexer = intermediateClass.Indexers.Add(new TypedName("IntermediateReadOnlyIndexer", identityManager.ObtainTypeReference(RuntimeCoreType.Int64)), new TypedNameSeries(new TypedName("index", identityManager.ObtainTypeReference(RuntimeCoreType.Byte))), true, false);
            Debug.Assert(!readOnlyIndexer.CanWrite);
            var regularIndexer = intermediateClass.Indexers.Add(new TypedName("IntermediateIndexer", identityManager.ObtainTypeReference(RuntimeCoreType.Int16)), new TypedNameSeries(new TypedName("index", identityManager.ObtainTypeReference(RuntimeCoreType.SByte))), true, true);
            Debug.Assert(regularIndexer.CanWrite && regularIndexer.CanRead);

            const string genericParameterName = "TGenericParameter";
            var equatableType = (IInterfaceType)identityManager.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "IEquatable", 1));
            var genericIntermediateClass = defaultNamespace.Classes.Add("IntermediateClass", new GenericParameterData(genericParameterName));
            var firstGenericParameter = genericIntermediateClass.TypeParameters[genericParameterName];
            firstGenericParameter.Constraints.Add(equatableType.MakeGenericClosure(firstGenericParameter));
            var genericClassInstance = genericIntermediateClass.MakeGenericClosure(identityManager.ObtainTypeReference(RuntimeCoreType.Single));
            var equatableSingle = equatableType.MakeGenericClosure(identityManager.ObtainTypeReference(RuntimeCoreType.Single));
            Debug.Assert(equatableSingle.IsAssignableFrom(genericClassInstance.GenericParameters[0]));

            var structuralExampleMethod = targetAssembly.Methods.Add("StructuralExample");
            var tdataset = structuralExampleMethod.TypeParameters.Add("TDataset");
            var tdatabase = structuralExampleMethod.TypeParameters.Add("TDatabase");
            var byteRef = identityManager.ObtainTypeReference(RuntimeCoreType.Byte);
            var datasetIndex = tdataset.Indexers.Add(byteRef, new TypedNameSeries(new TypedName("index", identityManager.ObtainTypeReference(RuntimeCoreType.Int32))), true, true);
            var database = tdataset.Properties.Add(new TypedName("Database", tdatabase), true, false);
            var dataCopy = tdataset.Methods.Add(new TypedName("GetDataCopy", byteRef.MakeArray()));
            var clone = tdataset.Methods.Add(new TypedName("Clone", tdataset));
            var ctor = tdataset.Constructors.Add(new TypedName("db", tdatabase), new TypedName("data", byteRef.MakeArray()));
            targetAssembly.Dispose();

        }

        [TestMethod]
        public void TestIntermediateStructType()
        {
            this.CheckManager();
            this.CheckProviders();
            this.TestIntermediateStructType(this.cvbProvider.CreateAssembly("TestVBAssembly"));
            this.TestIntermediateStructType(this.mvbProvider.CreateAssembly("TestVBMyAssembly"));
            this.TestIntermediateStructType(this.csProvider.CreateAssembly("TestCSAssembly"));
        }

        [TestMethod]
        public void TestIntermediateDelegateType()
        {
            this.CheckManager();
            this.CheckProviders();
            this.TestIntermediateDelegateType(this.cvbProvider.CreateAssembly("TestVBAssembly"));
            this.TestIntermediateDelegateType(this.mvbProvider.CreateAssembly("TestVBMyAssembly"));
            this.TestIntermediateDelegateType(this.csProvider.CreateAssembly("TestCSAssembly"));
        }

        private void TestIntermediateDelegateType(IIntermediateAssembly targetAssembly)
        {
            var defaultNamespace = targetAssembly.DefaultNamespace = targetAssembly.Namespaces.Add("Abstraction.SupplementaryProjects.UnitTests.IntermediateTypeSystem");
            var intermediateDelegate = defaultNamespace.Delegates.Add("IntermediateDelegate");
            intermediateDelegate.ReturnType = identityManager.ObtainTypeReference(RuntimeCoreType.UInt64);
            intermediateDelegate.Parameters.AddRange(new TypedName("testParameter1", identityManager.ObtainTypeReference(RuntimeCoreType.UInt32)), new TypedName("testParameter2", identityManager.ObtainTypeReference(RuntimeCoreType.UInt32)));
            Debug.Assert(intermediateDelegate.Parameters.Count == 2);
            var intermediateGenericDelegate = defaultNamespace.Delegates.Add("IntermediateDelegate", new GenericParameterData("TGenericParameter1"));
            var id = ((IDelegateType)intermediateGenericDelegate);
            var intermediateTypeParameter1 = intermediateGenericDelegate.TypeParameters["TGenericParameter1"];
            var intermediateTypeParameter2 = intermediateGenericDelegate.TypeParameters.Add("TGenericParameter2");
            var intermediateTypeParameter3 = intermediateGenericDelegate.TypeParameters.Add("TGenericParameter3");
            intermediateTypeParameter3.Constraints.Add(((IInterfaceType)(identityManager.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Collections.Generic", "IDictionary", 2)))).MakeGenericClosure(intermediateTypeParameter1, intermediateTypeParameter2));
            var param1 = intermediateGenericDelegate.Parameters.Add(new TypedName("param1", ((IDelegateType)(identityManager.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "Func", 2)))).MakeGenericClosure(((IInterfaceType)(identityManager.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Collections.Generic", "IEnumerable", 1)))).MakeGenericClosure(((IStructType)(identityManager.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Collections.Generic", "KeyValuePair", 2)))).MakeGenericClosure(intermediateTypeParameter1, intermediateTypeParameter2)), intermediateTypeParameter3)));
            var intermediateDelegateClosure = intermediateGenericDelegate.MakeGenericClosure(identityManager.ObtainTypeReference(RuntimeCoreType.Int32), identityManager.ObtainTypeReference(RuntimeCoreType.String), ((IClassType)(identityManager.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Collections.Generic", "Dictionary", 2)))).MakeGenericClosure(identityManager.ObtainTypeReference(RuntimeCoreType.Int32), identityManager.ObtainTypeReference(RuntimeCoreType.String)));
            var delegateClosureParam1 = intermediateDelegateClosure.Parameters.Values[0];
            /* *
             * Ensure that the delegate closure's first parameter is
             * indeed accurate according to the type replacements.
             * */
            param1.ParameterType.ContainsSymbols();
            var expectedType = ((IDelegateType)(identityManager.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System", "Func", 2)))).MakeGenericClosure(((IInterfaceType)(identityManager.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Collections.Generic", "IEnumerable", 1)))).MakeGenericClosure(((IStructType)(identityManager.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Collections.Generic", "KeyValuePair", 2)))).MakeGenericClosure(identityManager.ObtainTypeReference(RuntimeCoreType.Int32), identityManager.ObtainTypeReference(RuntimeCoreType.String))), ((IClassType)(identityManager.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Collections.Generic", "Dictionary", 2)))).MakeGenericClosure(identityManager.ObtainTypeReference(RuntimeCoreType.Int32), identityManager.ObtainTypeReference(RuntimeCoreType.String)));
            Debug.Assert(delegateClosureParam1.ParameterType == expectedType);
        }

        public void TestIntermediateStructType(IIntermediateAssembly targetAssembly)
        {
            var defaultNamespace = targetAssembly.DefaultNamespace = targetAssembly.Namespaces.Add("Abstraction.SupplementaryProjects.UnitTests.IntermediateTypeSystem");
            var intermediateStruct = defaultNamespace.Structs.Add("IntermediateStruct");
            var voidMethod = intermediateStruct.Methods.Add("VoidMethod");
            Debug.Assert(voidMethod.ReturnType == identityManager.ObtainTypeReference(RuntimeCoreType.VoidType));
            var returnMethod = intermediateStruct.Methods.Add(new TypedName("ReturnMethod", identityManager.ObtainTypeReference(RuntimeCoreType.String)));
            returnMethod.Return("TestReturn".ToPrimitive());

            var writeOnlyProperty = intermediateStruct.Properties.Add(new TypedName("IntermediateWriteOnlyProperty", identityManager.ObtainTypeReference(RuntimeCoreType.Int32)), false, true);
            Debug.Assert(!writeOnlyProperty.CanRead && writeOnlyProperty.CanWrite);
            var readOnlyProperty = intermediateStruct.Properties.Add(new TypedName("IntermediateReadOnlyProperty", identityManager.ObtainTypeReference(RuntimeCoreType.Int64)), true, false);
            Debug.Assert(readOnlyProperty.CanRead && !readOnlyProperty.CanWrite);
            var regularProperty = intermediateStruct.Properties.Add(new TypedName("IntermediateProperty", identityManager.ObtainTypeReference(RuntimeCoreType.Int16)), true, true);
            Debug.Assert(regularProperty.CanRead && regularProperty.CanWrite);

            var writeOnlyIndexer = intermediateStruct.Indexers.Add(new TypedName("IntermediateWriteOnlyIndexer", identityManager.ObtainTypeReference(RuntimeCoreType.Int32)), new TypedNameSeries(new TypedName("index", identityManager.ObtainTypeReference(RuntimeCoreType.Int16))), false, true);
            Debug.Assert(!writeOnlyIndexer.CanRead);
            var readOnlyIndexer = intermediateStruct.Indexers.Add(new TypedName("IntermediateReadOnlyIndexer", identityManager.ObtainTypeReference(RuntimeCoreType.Int64)), new TypedNameSeries(new TypedName("index", identityManager.ObtainTypeReference(RuntimeCoreType.Byte))), true, false);
            Debug.Assert(!readOnlyIndexer.CanWrite);
            var regularIndexer = intermediateStruct.Indexers.Add(new TypedName("IntermediateIndexer", identityManager.ObtainTypeReference(RuntimeCoreType.Int16)), new TypedNameSeries(new TypedName("index", identityManager.ObtainTypeReference(RuntimeCoreType.SByte))), true, true);
            Debug.Assert(regularIndexer.CanWrite && regularIndexer.CanRead);

            const string genericParameterName = "TGenericParameter";
            var genericIntermediateStruct = defaultNamespace.Classes.Add("IntermediateStruct", new GenericParameterData(genericParameterName));
            var firstGenericParameter = genericIntermediateStruct.TypeParameters[genericParameterName];

            var structuralExampleMethod = targetAssembly.Methods.Add("StructuralExample");
            var tdataset = structuralExampleMethod.TypeParameters.Add("TDataset");
            var tdatabase = structuralExampleMethod.TypeParameters.Add("TDatabase");
            var byteRef = identityManager.ObtainTypeReference(RuntimeCoreType.Byte);
            var datasetIndex = tdataset.Indexers.Add(byteRef, new TypedNameSeries(new TypedName("index", identityManager.ObtainTypeReference(RuntimeCoreType.Int32))), true, true);
            var database = tdataset.Properties.Add(new TypedName("Database", tdatabase), true, false);
            var dataCopy = tdataset.Methods.Add(new TypedName("GetDataCopy", byteRef.MakeArray()));
            var clone = tdataset.Methods.Add(new TypedName("Clone", tdataset));
            var ctor = tdataset.Constructors.Add(new TypedName("db", tdatabase), new TypedName("data", byteRef.MakeArray()));
            targetAssembly.Dispose();
        }
    }

    [TestClass]
    public class CliTests
    {
        private ICliManager cliManager;
        [TestMethod]
        public void TestClassType()
        {
            CheckManager();
            if (cliManager != null)
            {
                var classTest = (IClassType)typeof(Dictionary<int, int>).GetTypeReference(cliManager);
                foreach (var member in classTest.Members.Values)
                    member.ToString();
            }
        }
        [TestMethod]
        public void TestInterfaceType()
        {
            CheckManager();
            if (cliManager != null)
            {
                var interfaceTest = (IInterfaceType)typeof(IDictionary<int, int>).GetTypeReference(cliManager);
                foreach (var member in interfaceTest.Members.Values)
                    member.ToString();
            }
        }
        [TestMethod]
        public void TestEnumType()
        {
            CheckManager();
            if (cliManager != null)
            {
                var enumTest = (IEnumType)typeof(AttributeTargets).GetTypeReference(cliManager);
                foreach (var member in enumTest.Members.Values)
                    member.ToString();
            }
        }

        [TestMethod]
        public void TestDelegateType()
        {
            CheckManager();
            if (cliManager != null)
            {
                var delegateTest = (IDelegateType)typeof(Func<int, int, int, Tuple<int>>).GetTypeReference(cliManager);
                foreach (var member in delegateTest.Members.Values)
                    member.ToString();
            }
        }

        [TestMethod]
        public void TestStructType()
        {
            CheckManager();
            if (cliManager != null)
            {
                var structTest = (IStructType)typeof(KeyValuePair<int, string>).GetTypeReference(cliManager);
                foreach (var member in structTest.Members.Values)
                    member.ToString();
            }
        }

        private void CheckManager()
        {
            if (cliManager == null)
                cliManager = CliGateway.CreateIdentityManager(CliGateway.CurrentPlatform, CliGateway.CurrentVersion);
        }

    }
}
