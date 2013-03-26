using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralTypingExample
{
    internal class ConstructStrucalExample
    {
        static void Main()
        {
            ICSharpAssembly iia = LanguageVendors.Microsoft.GetCSharpLanguage().GetProvider().CreateAssembly("TestAssembly");
            var idm = (IIntermediateCliManager)iia.IdentityManager;
            var ienumerableType = (IInterfaceType)idm.ObtainTypeReference(idm.RuntimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier("System.Collections.Generic", "IEnumerable", 1));
            var structuralExampleMethod = iia.Methods.Add("StructuralExample");
            var tdataset = structuralExampleMethod.TypeParameters.Add("TDataset");
            var tdatabase = structuralExampleMethod.TypeParameters.Add("TDatabase");
            var byteRef = idm.ObtainTypeReference(idm.RuntimeEnvironment.Byte);
            var datasetIndex = tdataset.Indexers.Add(byteRef, new TypedNameSeries(new TypedName("index", idm.ObtainTypeReference(idm.RuntimeEnvironment.Int32))), true, false);
            var database = tdataset.Properties.Add(new TypedName("Database", tdatabase));
            var dataCopy = tdataset.Methods.Add(new TypedName("GetDataCopy", byteRef.MakeArray()));
            var clone = tdataset.Methods.Add(new TypedName("Clone", tdataset));
            var ctor = tdataset.Constructors.Add(new TypedName("uInst", tdatabase), new TypedName("data", byteRef.MakeArray()));
            var d = database.GetMethod.GetReference(new SpecialReferenceExpression(SpecialReferenceKind.This)).Invoke();
        }
    }
}
