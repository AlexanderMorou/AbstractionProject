using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*---------------------------------------------------------------------\
| Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */


namespace StructuralTypingExample
{
    internal class ConstructStrucalExample
    {
        static void Main()
        {
            var provider = LanguageVendors.Microsoft.GetCSharpLanguage().GetProvider(CSharpLanguageVersion.Version3);
            var timeTest = MiscHelperMethods.CreateActionOfTime<ICSharpProvider>(Test);
            var time1 = timeTest(provider);
            var time2 = timeTest(provider);
            Console.WriteLine("Took {0}ms to process the first time (Includes JIT Overhead).", time1);
            Console.WriteLine("Took {0}ms to process the second time.", time2);
            //database.Name = "TestDatabase";
        }

        private static void Test(ICSharpProvider provider)
        {
            ICSharpAssembly iia = provider.CreateAssembly("TestAssembly");
            var idm = iia.IdentityManager;
            var ienumerableType = (IInterfaceType)idm.ObtainTypeReference(TypeSystemIdentifiers.GetTypeIdentifier("System.Collections.Generic", "IEnumerable", 1));
            var structuralExampleMethod = iia.Methods.Add("StructuralExample");
            var tdataset = structuralExampleMethod.TypeParameters.Add("TDataset");
            var tdatabase = structuralExampleMethod.TypeParameters.Add("TDatabase");
            var byteRef = idm.ObtainTypeReference(idm.RuntimeEnvironment.Byte);
            var datasetIndex = tdataset.Indexers.Add(byteRef, new TypedNameSeries(new TypedName("index", idm.ObtainTypeReference(idm.RuntimeEnvironment.Int32))), true, true);
            var database = tdataset.Properties.Add(new TypedName("Database", tdatabase), true, false);
            var dataCopy = tdataset.Methods.Add(new TypedName("GetDataCopy", byteRef.MakeArray()));
            var clone = tdataset.Methods.Add(new TypedName("Clone", tdataset));
            var ctor = tdataset.Constructors.Add(new TypedName("uInst", tdatabase), new TypedName("data", byteRef.MakeArray()));
            var d = datasetIndex.GetMethod.GetReference(new SpecialReferenceExpression(SpecialReferenceKind.This)).Invoke();
            var gvm = datasetIndex.GetMethod.UniqueIdentifier;
            var parameters = datasetIndex.Parameters;
            var nextParameter = parameters.Add(new TypedName("testIndex", idm.ObtainTypeReference(RuntimeCoreType.Int16)));
            var valueParameter = datasetIndex.SetMethod.ValueParameter;
            nextParameter.ParameterType = idm.ObtainTypeReference(RuntimeCoreType.SByte);
            
            iia.Dispose();
        }
    }
}
