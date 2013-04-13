using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages.VisualBasic;
using AllenCopeland.Abstraction.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralTypingBuilder
{
    internal static class Program
    {
        private static void Main()
        {
            var provider = LanguageVendors.Microsoft.GetVisualBasicLanguage().GetMyProvider(VisualBasicVersion.Version11);
            var timeTest = MiscHelperMethods.CreateActionOfTime<ILanguageProvider<IVisualBasicLanguage, IMyVisualBasicProvider>>(Test);
            var time1 = timeTest(provider);
            var time2 = timeTest(provider);
            Console.WriteLine("Took {0}ms to process the first time (Includes JIT Overhead).", time1);
            Console.WriteLine("Took {0}ms to process the second time.", time2);
            //database.Name = "TestDatabase";
        }

        private static void Test<TLanguage, TProvider>(ILanguageProvider<TLanguage, TProvider> provider)
            where TLanguage :
                ILanguage<TLanguage, TProvider>
            where TProvider :
                ILanguageProvider<TLanguage, TProvider>
        {
            IIntermediateAssembly iia = (IIntermediateAssembly)provider.CreateAssembly("TestAssembly");
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
