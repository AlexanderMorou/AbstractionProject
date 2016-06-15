using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Languages.CSharp;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Cli;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Slf.Cli;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Translation;
using System.IO;
using System.Collections.ObjectModel;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Globalization;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System.Collections;
using AllenCopeland.Abstraction;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf.FiniteAutomata;
using Accessibility;
using System.Windows.Forms;
using System.Drawing;
using AllenCopeland.Abstraction.Utilities.Security;
//using System.Exp;
//using System.Collections.Generic.Exp;
namespace Compiler_Expansion_Project
{
    [Description("Test")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    class Program
    {
        private class Test<A> :
            Test3<A>
        {
            public class Test2<B> :
                Test3<B>.Test4<A>
            {

            }
        }

        private class Test3<C>
        {
            public class Test4<D>
            {

            }
        }

        static void Main(string[] args)
        {
            var cliManager = IntermediateCliGateway.GlobalManager;//IntermediateCliGateway.CreateIdentityManager(CliFrameworkPlatform.x86Platform, CliFrameworkVersion.v4_0_30319, true, true, true);
            var csLan = LanguageVendors.Microsoft.GetCSharpLanguage();
            //var prov = csLan.GetProvider();
            //var testA = prov.CreateAssembly("TEST");
            //var testClass1 = testA.Classes.Add("Test1", new GenericParameterData("A"));
            //var testClass2 = testClass1.Classes.Add("Test2", new GenericParameterData("B"));
            //var testClass3 = testA.Classes.Add("Test3", new GenericParameterData("C"));
            //var testClass4 = testClass3.Classes.Add("Test4", new GenericParameterData("D"));
            //testClass2.BaseType = testClass4.MakeGenericClosure(testClass2.TypeParameters.Values[0], testClass1.TypeParameters.Values[0]);
            //testClass1.BaseType = testClass3.MakeGenericClosure(testClass1.TypeParameters.Values[0]);
            //var testClass2GT = testClass2.MakeGenericClosure(typeof(int).GetTypeReference(), typeof(double).GetTypeReference());
            //var testClass2GTBT = testClass2GT.BaseType;
            //var testClass2GTBTGP = testClass2GTBT.GenericParameters.ToArray();
            //var typem = typeof(Test<int>.Test2<double>).GetTypeReference<IClassType>();
            //var mu = typem.BaseType;
            //var typemGP = typem.GenericParameters;
            //var muG = (IGenericType)mu;
            //var mue = muG.GenericParameters.ToArray();
            //cliManager.Dispose();
            //return;
            //var dummy = new StaticWriteLine_19_00();

            
            //var joinCall = StringExp.Join.Invoke("TEST", new IEnumerableExp<string>(new[] { "Test", "Cest", "Mest" }.ToExpression(cliManager).Cast(typeof(IEnumerable<string>).GetTypeReference(cliManager))));
            //var typeofTest = new TypeExp(new TypeOfExpression(typeof(double).GetTypeReference(cliManager))).GetMethod.Invoke("IsNaN", BindingFlagsExp.Public | BindingFlagsExp.Static, new BinderExp(typeof(Type).GetTypeExpression(cliManager).GetProperty("DefaultBinder")), new MalleableCreateArrayDetailExpression(typeof(Type).GetTypeReference(cliManager), new TypeOfExpression(typeof(double).GetTypeReference(cliManager))), IntermediateGateway.NullValue);
            //var joinSubstring = joinCall.Substring.Invoke(3, 4);
            var buildersData = GetTypeBuilders(
                cliManager, 
                csLan.GetProvider(cliManager),
                typeof(int).GetTypeReference().Assembly,
                typeof(IAccessible).GetTypeReference().Assembly,
                typeof(Color).GetTypeReference().Assembly,
                typeof(Form).GetTypeReference().Assembly,
                typeof(Action<,,,,,,,,>).GetTypeReference().Assembly,
                typeof(Stack<>).GetTypeReference().Assembly,
                typeof(System.Configuration.ConfigurationElement).GetTypeReference().Assembly,
                typeof(System.Xml.XmlElement).GetTypeReference().Assembly,
                typeof(IStrongNameKeyInfo).GetTypeReference().Assembly,
                typeof(IType).GetTypeReference().Assembly,
                typeof(StringHandling).GetTypeReference().Assembly, 
                typeof(HexadecimalBase).GetTypeReference().Assembly,
                typeof(FiniteAutomataBitSet<>).GetTypeReference().Assembly,
                typeof(ICliManager).GetTypeReference().Assembly,
                typeof(IIntermediateAssembly).GetTypeReference().Assembly,
                typeof(IIntermediateCliManager).GetTypeReference().Assembly,
                typeof(ICSharpAssembly).GetTypeReference().Assembly);

            var builders = buildersData.Item2;
            var builderManager = buildersData.Item1;
            foreach (var builder in builders.OrderBy(k => k.TargetType.FullName.Length).Where(k => !(k.TargetType.Parent is IType)))
                builder.BuildPlaceholders(cliManager);
            foreach (var builder in builders.OrderBy(k => k.TargetType.FullName.Length).Where(k => (k.TargetType.Parent is IType)))
                builder.BuildPlaceholders(cliManager);
            //var resultTypes = testAssem.GetTypes().OrderBy(k=>k.NamespaceName).ThenBy(k=>k.FullName).ToArray();
            foreach (var builder in builders)
                builder.AssociateBaseTypes();
            foreach (var builder in builders)
                builder.BuildSignatures();
            var bldrs = builders.OrderBy(k=>k.TargetType.NamespaceName).ThenBy(m=>m.TargetType.ToString()).ToArray();
            builderManager.Finalize();
            builderManager.CreateSignatureTypes();
            foreach (var builder in builders)
                builder.BuildStructure();
            foreach (var assem in builderManager.GetResultAssemblies())
            {
                var resultFolder = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), assem.Name);
                WriteProject(resultFolder, assem);
            }

            cliManager.Dispose();
        }

        private static void WriteProject(string resultPath, IIntermediateAssembly newAssembly)
        {
            var opts = new IntermediateCodeTranslatorOptions(new DefaultCodeTranslatorFormatterProvider());
            //opts.AllowPartials = true;
            /* Below controls the emission order of the elements.  The default would be verbatim order (the order they were inserted.) */
            CSharpProjectTranslator cspt = new CSharpProjectTranslator(opts);
            opts.AllowPartials = false;
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Fields);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Constructors);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.UnaryOperatorCoercions);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.BinaryOperatorCoercions);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.TypeCoercions);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Properties);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Methods);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Classes);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Delegates);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Enums);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Interfaces);
            opts.TranslationOrder.Add(DeclarationTranslationOrder.Structs);
            opts.ElementOrderingMethod = TranslationOrderKind.Specific | TranslationOrderKind.Alphabetic;
            opts.ShortenFilenames = false;
            cspt.WriteProject(newAssembly, resultPath);
        }

        private static Tuple<TypeGeneratorManager,TypeGenerator[]> GetTypeBuilders(IIntermediateCliManager manager, ICSharpProvider provider, params IAssembly[] assemblies)
        {
            return GetTypeBuilders((IEnumerable<IAssembly>)assemblies, provider, manager);
        }
        private static Tuple<TypeGeneratorManager, TypeGenerator[]> GetTypeBuilders(IEnumerable<IAssembly> assemblies, ICSharpProvider provider, IIntermediateCliManager manager)
        {
            var tgm = new TypeGeneratorManager(manager, provider);
            var set =
                      (from assembly in assemblies
                       from type in assembly.GetTypes()
                       where TypeIsValid(assembly, type, provider)
                       let builder = new TypeGenerator(type, tgm)
                       select builder).ToArray();
            tgm.Associate(set);
            return Tuple.Create(tgm, set);
        }

        private static bool TypeIsValid(IAssembly assembly, IType type, ICSharpProvider provider)
        {
            bool isMsCorLib = assembly.UniqueIdentifier == provider.IdentityManager.RuntimeEnvironment.CoreLibraryIdentifier;
            IType currentType = type;
            while (currentType != null)
            {
                if (currentType.Metadata.Contains(GetObsoleteAttribute(assembly, provider)))
                    return false;
                if (currentType.Parent is IType)
                    currentType = (IType)currentType.Parent;
                else
                    break;
            }
            return AccessLevelIsValid(type) &&
                    !(type.UniqueIdentifier.Equals(assembly.UniqueIdentifier.GetTypeIdentifier("System.Threading", "AsyncLocalValueChangedArgs", 1)) ||
                      type.UniqueIdentifier.Equals(assembly.UniqueIdentifier.GetTypeIdentifier("System.Runtime.Versioning", "CompatibilitySwitch", 0)));
        }

        private static IType GetObsoleteAttribute(IAssembly assembly, ICSharpProvider provider)
        {
            return provider.IdentityManager.ObtainTypeReference(provider.IdentityManager.RuntimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier("System", "ObsoleteAttribute", 0), assembly);
        }

        private static bool AccessLevelIsValid(IType type)
        {
            if (type.Parent is IType)
                if (!AccessLevelIsValid((IType)type.Parent))
                    return false;
            return type.AccessLevel == AccessLevelModifiers.Public ||
                                          type.AccessLevel == AccessLevelModifiers.Protected ||
                                          type.AccessLevel == AccessLevelModifiers.ProtectedOrInternal;
        }

    }

    internal class TypeInterpreter
    {
        private IIntermediateCliManager cliManager;
        private IType[] primitives;
        private TypeGeneratorManager manager;
        public TypeInterpreter(IIntermediateCliManager cliManager, TypeGeneratorManager manager)
        {
            this.manager = manager;
            this.cliManager = cliManager;
            this.primitives = new[] { cliManager.RuntimeEnvironment.Boolean, cliManager.RuntimeEnvironment.Byte, cliManager.RuntimeEnvironment.Char, cliManager.RuntimeEnvironment.Decimal, cliManager.RuntimeEnvironment.Double, cliManager.RuntimeEnvironment.Int16, cliManager.RuntimeEnvironment.Int32, cliManager.RuntimeEnvironment.Int64, cliManager.RuntimeEnvironment.SByte, cliManager.RuntimeEnvironment.Single, cliManager.RuntimeEnvironment.String, cliManager.RuntimeEnvironment.UInt16, cliManager.RuntimeEnvironment.UInt32, cliManager.RuntimeEnvironment.UInt64 }.Select(k => cliManager.ObtainTypeReference(k)).ToArray();
        }

        public IType GetParameterType(IParameterMember pm)
        {
            var expectedType = pm.ParameterType;
            return GetExpectedType(expectedType);
        }

        public IType GetExpectedType(IType expectedType, bool allowPre = true)
        {
            TypeGenerator generator;
            switch (expectedType.ElementClassification)
            {
                case TypeElementClassification.None:
                    if (allowPre && primitives.Contains(expectedType))
                        return ((IClassType)typeof(PrimitiveRepresentationExpression<>).GetTypeReference()).MakeGenericClosure(expectedType);
                    generator = this.manager.GetGenerator(expectedType);
                    if (generator != null)
                        return generator.ResultType;
                    goto default;
                case TypeElementClassification.Reference:
                    if (allowPre)
                        return typeof(IDirectionExpression).GetTypeReference();
                    else
                        return typeof(IExpression).GetTypeReference();
                case TypeElementClassification.GenericTypeDefinition:
                    var elementType = expectedType.ElementType;
                    generator = this.manager.GetGenerator(elementType);
                    if (generator != null)
                        return ((IGenericType)generator.ResultType).MakeGenericClosure(((IGenericType)expectedType).GenericParameters);
                    goto default;
                case TypeElementClassification.Array:
                case TypeElementClassification.Nullable:
                case TypeElementClassification.Pointer:
                case TypeElementClassification.ModifiedType:
                default:
                    return typeof(IExpression).GetTypeReference();
            }
        }

        public IType TranslateReturnType(IType retType)
        {
            TypeGenerator gTarget = null;
            switch (retType.ElementClassification)
            {
                case TypeElementClassification.None:
                    if (this.cliManager.ObtainTypeReference(RuntimeCoreType.VoidType) == retType || retType.IsGenericTypeParameter)
                        return typeof(IMethodInvokeExpression).GetTypeReference();
                    gTarget = this.manager.GetGenerator(retType);
                    return gTarget.ResultType;
                case TypeElementClassification.Nullable:
                case TypeElementClassification.Reference:
                case TypeElementClassification.Pointer:
                case TypeElementClassification.Array:
                case TypeElementClassification.ModifiedType:
                default:
                    return typeof(IMethodInvokeExpression).GetTypeReference();
                case TypeElementClassification.GenericTypeDefinition:
                    var retTypeDef = retType.ElementType;
                    gTarget = this.manager.GetGenerator(retTypeDef);
                    return ((IGenericType)gTarget.ResultType).MakeGenericClosure(((IGenericType)(retType)).GenericParameters);
            }
        }
    }

    internal class TypeGeneratorManager
    {
        private Dictionary<IType, TypeGenerator> generators;
        private Dictionary<IAssembly, IIntermediateAssembly> assemblies;
        private TypeInterpreter tinter;
        private HashList<TypeSignaturePattern> signaturePatterns;
        private MultikeyedDictionary<string, IAssembly, HashList<NamedTypeSignaturesSet>> namedSignatureSets;
        private ICSharpProvider provider;
        public TypeGeneratorManager(IIntermediateCliManager cliManager, ICSharpProvider provider)
        {
            this.assemblies = new Dictionary<IAssembly, IIntermediateAssembly>();
            this.provider = provider;
            this.tinter = new TypeInterpreter(cliManager, this);
            this.signaturePatterns = new HashList<TypeSignaturePattern>();
            this.namedSignatureSets = new MultikeyedDictionary<string, IAssembly, HashList<NamedTypeSignaturesSet>>();
        }

        public IIntermediateAssembly GetAssemblyFor(IAssembly target)
        {
            if (!this.assemblies.ContainsKey(target))
                this.assemblies.Add(target, provider.CreateAssembly(string.Format("{0}Exp", target.Name)));
            return this.assemblies[target];
        }

        internal void Associate(IEnumerable<TypeGenerator> generators)
        {
            this.generators = generators.ToDictionary(k => k.TargetType, v => v);
        }

        public TypeGenerator GetGenerator(IIntermediateType type)
        {
            return this.generators.Values.Where(k => k.ResultType == type).FirstOrDefault();
        }

        public TypeGenerator GetGenerator(IType type)
        {
            if (this.generators.ContainsKey(type))
                return this.generators[type];
            return null;
        }

        public TypeSignaturePattern GeneratePattern(ISignatureMember sig)
        {
            TypeSignaturePattern result = new TypeSignaturePattern();
            IType retType = null;
            if (sig is IMethodSignatureMember)
                retType = ((IMethodSignatureMember)sig).ReturnType;
            else if (sig is IIndexerMember)
                retType = ((IIndexerMember)sig).PropertyType;
            else if (sig is IIndexerSignatureMember)
                retType = ((IIndexerSignatureMember)sig).PropertyType;
            else if (sig is IConstructorMember)
            {
                var generator = this.GetGenerator(((ICreatableParent)(sig.Parent)));
                retType = generator.TargetType;
            }
            result.ReturnType = tinter.TranslateReturnType(retType);
            result.ParameterNames = new HashList<string>(sig.Parameters.Values.Cast<IParameterMember>().Select(k => k.Name));
            if (sig is IMethodSignatureMember)
            {
                var methodMember = ((IMethodSignatureMember)sig);
                if (methodMember.TypeParameters != null && methodMember.TypeParameters.Count > 0)
                    result.GenericParameters = new HashList<GenericParameterData>(methodMember.TypeParameters.Values.Select(k => NamedTypeSignaturesSet.GetGenericParameterFor((IIntermediateCliManager)result.ReturnType.IdentityManager, k)));
            }
            IInstanceMember instMember = (sig as IInstanceMember);
            if (instMember != null)
            {
                result.IsStatic = instMember.IsStatic;
                result.StaticParent = instMember.Parent as IType;
            }
            else
                result.IsStatic = false;

            if (sig.Parent is IGenericType)
            {
                IGenericType gtParent = sig.Parent as IGenericType;
                if (gtParent.TypeParameters != null && gtParent.IsGenericConstruct && gtParent.IsGenericDefinition)
                    result.ParentTypeParameters = new HashList<IType>(((IEnumerable)(gtParent.GenericParameters)).Cast<IType>());
            }

            foreach (var t in sig.Parameters.Values.Cast<IParameterMember>())
                result.Add(this.tinter.GetParameterType(t));
            var lParam = sig.Parameters.Values.Cast<IParameterMember>().LastOrDefault();
            if (lParam != null && sig.LastIsParams && lParam.ParameterType.ElementClassification == TypeElementClassification.Array)
            {
                result.RemoveAt(result.Count - 1);
                result.Add(tinter.GetExpectedType(lParam.ParameterType.ElementType));
                result.LastIsParams = true;
            }
            int index = this.signaturePatterns.IndexOf(result);
            if (index == -1)
            {
                this.signaturePatterns.Add(result);
                return result;
            }
            return this.signaturePatterns[index];
        }

        public TypeInterpreter TypeInterpreter { get { return this.tinter; } }

        public NamedTypeSignaturesSet GetNamedSignatureSet(string name, IIntermediateAssembly assembly, IEnumerable<TypeSignaturePattern> patterns)
        {
            HashList<NamedTypeSignaturesSet> workingSet;
            if (!namedSignatureSets.TryGetValue(name, assembly, out workingSet))
                namedSignatureSets.Add(name, assembly, workingSet = new HashList<NamedTypeSignaturesSet>());
            var tester = new NamedTypeSignaturesSet(patterns) { Name = name };
            int index = workingSet.IndexOf(tester);
            if (index != -1)
            {
                var result=workingSet[index];
                result.CompareParameterNames(tester);
                return result;
            }
            workingSet.Add(tester);
            return tester;
        }

        internal void Finalize()
        {
            var dataCopy = this.namedSignatureSets.OrderBy(k => k.Keys.Key1).ThenBy(k => k.Keys.Key2.Name).ToArray();
            this.namedSignatureSets = new MultikeyedDictionary<string, IAssembly, HashList<NamedTypeSignaturesSet>>();
            foreach (var element in dataCopy)
                this.namedSignatureSets.Add(element.Keys.Key1, element.Keys.Key2, element.Value);
        }

        public IEnumerable<IIntermediateAssembly> GetResultAssemblies()
        {
            foreach (var assem in this.assemblies.Values)
                yield return assem;
        }

        internal void CreateSignatureTypes()
        {
            foreach (var assem in this.assemblies.Values)
            {
                var ns = TypeGenerator.GetNamespace(string.Format("AllenCopeland.Abstraction.Slf.Ast.Expressions.Parameters.{0}", assem.Name.Replace(".", "_")), assem);
                List<IType> generatedTypes = assem.GetTypes().ToList();
                var currentSet = namedSignatureSets.Where(k => k.Keys.Key2 == assem).ToDictionary(k => k.Keys.Key1, v => v.Value);
                foreach (var name in currentSet.Keys)
                {
                    var workingSet = currentSet[name];
                    var instanceTargets = workingSet.Where(k => k.HasInstance);
                    var staticTargets = workingSet.Where(k => k.HasStatic);
                    if (instanceTargets.Count() > 0)
                        BuildBasedUpon(ns, generatedTypes, name, instanceTargets, false);
                    if (staticTargets.Count() > 0)
                        BuildBasedUpon(ns, generatedTypes, name, staticTargets, true);
                }
            }
        }

        private void BuildBasedUpon(IIntermediateNamespaceDeclaration ns, List<IType> generatedTypes, string name, IEnumerable<NamedTypeSignaturesSet> targetSet, bool isStatic)
        {
            int index = 0;
            var staticModifierText = isStatic ? "Static" : string.Empty;
            foreach (var subset in targetSet)
            {
                var typeName = string.Format("{3}{0}_{1:00}_{2:00}", name.Replace(".ctor", "Constructor"), subset.Count, index, staticModifierText);
                while (ns.Types.Keys.Any(k => k.Name == typeName))
                {
                    index++;
                    typeName = string.Format("{3}{0}_{1:00}_{2:00}", name.Replace(".ctor", "Constructor"), subset.Count, index, staticModifierText);
                }
                var currentType = ns/*.Parts.Add()*/.Classes.Add(typeName);
                subset.BuildOn(currentType, generatedTypes, isStatic, this);
                currentType.AccessLevel = AccessLevelModifiers.Public;
            }
                
        }
    }

    internal class NamedTypeSignaturesSet :
        HashSet<TypeSignaturePattern>
    {
        private bool needsRehashed;
        private int currentHash;
        public string Name { get; set; }

        public bool HasStatic { get { return this.Any(k => k.IsStatic); } }

        public bool HasInstance { get { return this.Any(k => !k.IsStatic); } }

        public NamedTypeSignaturesSet(IEnumerable<TypeSignaturePattern> set)
            : base(set.OrderBy(k=>k.GetHashCode()))
        {
            needsRehashed = true;
        }

        public override int GetHashCode()
        {
            if (this.needsRehashed)
            {
                this.currentHash = this.CreateHash();
                this.needsRehashed = false;
            }
            return this.currentHash;
        }

        private int CreateHash()
        {
            int count = this.Count;
            int startHash = count.GetHashCode() ^ 34502201,
                hash = 785532;
            int i = 0;
            foreach (var element in this)
            {
                switch (i)
                {
                    case 0:
                        hash += element.GetHashCode() ^ 4000111;
                        break;
                    case 1:
                    case 9:
                        hash += element.GetHashCode() ^ 9332;
                        break;
                    case 3:
                    case 5:
                        hash += element.GetHashCode() ^ 288001;
                        break;
                    case 4:
                    case 17:
                    case 33:
                        hash += element.GetHashCode() ^ 9522;
                        break;
                    case 11:
                        hash += element.GetHashCode() ^ 31227;
                        break;
                    case 6:
                        hash += element.GetHashCode() ^ 922;
                        break;
                    case 7:
                        hash += element.GetHashCode() ^ 229332;
                        break;
                    case 10:
                        hash += element.GetHashCode() ^ 883322;
                        break;
                    default:
                        if (i % 5 == 0)
                            hash += element.GetHashCode() * i;
                        else if (i % 4 == 0)
                            hash *= element.GetHashCode() ^ i;
                        else if (i % 3 == 0)
                            hash ^= element.GetHashCode() * i;
                        else if (i % 2 == 0)
                            hash |= element.GetHashCode() ^ i;
                        else
                            hash += element.GetHashCode() ^ i;
                        break;
                }
                i++;
            }
            return hash ^ startHash;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is NamedTypeSignaturesSet))
                return false;
            var other = (NamedTypeSignaturesSet)obj;
            if (other.GetHashCode() != this.GetHashCode())
                return false;
            return other.SequenceEqual(this);
        }

        internal void BuildOn(IIntermediateClassType currentType, IList<IType> internalTypes, bool isStatic, TypeGeneratorManager manager)
        {
            var targetVersions = this.Where(tsp => tsp.IsStatic == isStatic);

            BuildOutSet(currentType, internalTypes, targetVersions, manager, isStatic);
            if (isStatic)
                this.StaticType = currentType;
            else
                this.InstanceType = currentType;
        }

        private void BuildOutSet(IIntermediateClassType currentType, IList<IType> internalTypes, IEnumerable<TypeSignaturePattern> targetSet, TypeGeneratorManager manager, bool isStatic)
        {
            var identityManager = ((IIntermediateCliManager)currentType.IdentityManager);
            IIntermediateClassFieldMember memberParentReference = null;
            var staticParent = isStatic
                               ? targetSet.FirstOrDefault(k => k.StaticParent != null)
                               : null;
            var typeParameters =
                targetSet.FirstOrDefault(k => k.ParentTypeParameters != null);
            if (this.Name != ".ctor" && staticParent == null)
            {
                memberParentReference = currentType.Fields.Add(typeof(IMemberParentReferenceExpression).WithName("_memberParentReference")); 
                var disposeMethod = currentType.Methods.Add(typeof(void).WithName("Dispose"));
                disposeMethod.AccessLevel = AccessLevelModifiers.Public;
                disposeMethod.Assign(memberParentReference, IntermediateGateway.NullValue);
                currentType.ImplementedInterfaces.ImplementInterfaceQuick(typeof(IDisposable).GetTypeReference<IInterfaceType>());
                var thisCtor = currentType.Constructors.Add(new TypedName("memberParentReference", memberParentReference.FieldType));
                var mprParam = thisCtor.Parameters["memberParentReference"];
                thisCtor.Assign(memberParentReference, mprParam);
                thisCtor.AccessLevel = AccessLevelModifiers.Internal;
            }
            if (typeParameters != null)
            {
                foreach (IGenericTypeParameter gParam in typeParameters.ParentTypeParameters.Select(k => k as IGenericParameter))
                {
                    var gpDetail = GetGenericParameterFor(identityManager, gParam);
                    currentType.TypeParameters.Add(gpDetail);
                }
            }
            foreach (var variation in targetSet.OrderBy(k => k.Count).ThenBy(k => k.GetHashCode()))
            {
                var typedNames = new TypedNameSeries();
                int index = 0;
                //if (this.Name == ".ctor" || staticParent != null)
                //    typedNames.Add(new TypedName("identityManager", typeof(ICliManager).GetTypeReference(identityManager)));
                bool lastIsParams = variation.LastIsParams;
                foreach (var t in variation)
                {
                    if (index == variation.Count - 1 && variation.LastIsParams)
                        typedNames.Add(new TypedName(variation.ParameterNames[index], t.MakeArray(), index++));
                    else
                        typedNames.Add(new TypedName(variation.ParameterNames[index], t, index++));
                }
                var tParams = variation.GenericParameters == null ? 0 : variation.GenericParameters.Count;
                var term = this.Name == ".ctor" ? "New" : "Invoke";
                bool isCtor = this.Name == ".ctor";
                var identifier = TypeSystemIdentifiers.GetGenericSignatureIdentifier(term, tParams, typedNames.Select(k => k.TypeReference).ToArray());
                int offset = 0;
                GenericParameterData[] genericDetail = new GenericParameterData[tParams];
                if (tParams > 0)
                    for (int tParamIndex = 0; tParamIndex < tParams; tParamIndex++)
                        genericDetail[tParamIndex] = variation.GenericParameters[tParamIndex];
                //while (currentType.Methods.ContainsKey(identifier))
                if (currentType.Methods.ContainsKey(identifier))
                {
                    string nameAdder = variation.ReturnType.Name;
                    if (internalTypes.Contains(variation.ReturnType))
                        if (nameAdder.EndsWith("Exp"))
                            nameAdder = nameAdder.Substring(0, nameAdder.Length - 3);
                    identifier = TypeSystemIdentifiers.GetGenericSignatureIdentifier(string.Format("{1}_{0}", nameAdder.Replace("`", ""), term), tParams, typedNames.Select(k => k.TypeReference).ToArray());

                    while (currentType.Methods.ContainsKey(identifier))
                        identifier = TypeSystemIdentifiers.GetGenericSignatureIdentifier(string.Format("{2}_{0}{1}", nameAdder.Replace("`", ""), ++offset, term), tParams, typedNames.Select(k => k.TypeReference).ToArray());
                }
                IIntermediateClassMethodMember currentMethod;
                if (variation.GenericParameters == null)
                    currentMethod = currentType.Methods.Add(variation.ReturnType.WithName(identifier.Name), typedNames);
                else
                    currentMethod = currentType.Methods.Add(variation.ReturnType.WithName(identifier.Name), typedNames, genericDetail);
                if (lastIsParams)
                {
                    var lastParam = currentMethod.Parameters.Values.Last();
                    lastParam.Metadata.Add(new MetadatumDefinitionParameterValueCollection(identityManager.ObtainTypeReference(identityManager.RuntimeEnvironment.ParamArrayMetadatum)));
                }
                if (memberParentReference != null)
                {
                    var invocation = memberParentReference.GetReference().GetMethod("GetMethod").Invoke(this.Name.ToPrimitive()).GetMethod("Invoke").Invoke(currentMethod.Parameters.Values.Select(parameter => (IExpression)parameter.GetReference()).ToArray());
                    IType returnType = variation.ReturnType;
                    if (returnType.IsGenericConstruct)
                    {
                        IGenericType t = (IGenericType)(returnType);
                        if (!t.IsGenericDefinition)
                            returnType = (IGenericType)t.ElementType;
                    }
                    IExpression returnValue = invocation;
                    if (lastIsParams)
                    {
                        var lastParam = invocation.Parameters[invocation.Parameters.Count-1];
                        invocation.Parameters.Remove(lastParam);
                        var invocationRef = currentMethod.Locals.Add("invocationExpression", invocation);
                        var enumeration = currentMethod.Enumerate("lastParamArg", lastParam);
                        enumeration.Call(invocationRef.GetReference().GetProperty("Parameters").GetMethod("Add").Invoke(enumeration.Local.GetReference()));
                        returnValue = invocationRef.GetReference();
                    }
                    returnValue = CheckReturnType(manager, variation, currentMethod, returnType, returnValue, isCtor);
                    currentMethod.Return(returnValue);
                }
                else if (staticParent == null)
                {
                    //var idManParam = currentMethod.Parameters["identityManager"];
                    var targetResultType = currentMethod.ReturnType;
                    var generator = manager.GetGenerator((IIntermediateType)targetResultType);
                    var typeofExpression = new TypeOfExpression(generator.TargetType);

                    var idManTypeRef = typeof(CliGateway).GetTypeExpression(identityManager).GetMethod("GetTypeReference").Invoke(typeofExpression, typeof(IntermediateCliGateway).GetTypeExpression(identityManager).GetProperty("GlobalManager"));
                    var invocation = typeof(IntermediateGateway).GetTypeExpression(identityManager).GetMethod("GetNewExpression").Invoke(currentMethod.Parameters.Values.Select(parameter => (IExpression)parameter.GetReference()).ToArray().AddBefore(idManTypeRef));
                    IExpression returnValue = invocation;
                    IType returnType = variation.ReturnType;
                    if (returnType.IsGenericConstruct)
                    {
                        IGenericType t = (IGenericType)(returnType);
                        if (!t.IsGenericDefinition)
                            returnType = (IGenericType)t.ElementType;
                    }
                    if (lastIsParams)
                    {
                        var lastParam = invocation.Parameters[invocation.Parameters.Count-1];
                        invocation.Parameters.Remove(lastParam);
                        var invocationRef = currentMethod.Locals.Add("invocationExpression", invocation);
                        var enumeration = currentMethod.Enumerate("lastParamArg", lastParam);
                        enumeration.Call(invocationRef.GetReference().GetProperty("Parameters").GetMethod("Add").Invoke(enumeration.Local.GetReference()));
                        returnValue = invocationRef.GetReference();
                    }
                    returnValue = CheckReturnType(manager, variation, currentMethod, returnType, returnValue, isCtor);
                    currentMethod.Return(returnValue);
                }
                else
                {
                    //var idManParam = currentMethod.Parameters["identityManager"];
                    var targetResultType = staticParent.StaticParent;
                    var generator = manager.GetGenerator(targetResultType);
                    var typeofExpression = new TypeOfExpression(generator.TargetType);
                    var idManTypeRef = typeof(ExpressionExtensions).GetTypeExpression(identityManager).GetMethod("GetTypeExpression").Invoke(typeofExpression, typeof(IntermediateCliGateway).GetTypeExpression(identityManager).GetProperty("GlobalManager"));
                    var invocation = idManTypeRef.GetMethod("GetMethod").Invoke(this.Name.ToPrimitive()).GetMethod("Invoke").Invoke(currentMethod.Parameters.Values.Select(parameter => (IExpression)parameter.GetReference()).ToArray());
                    IExpression returnValue = invocation;
                    IType returnType = variation.ReturnType;
                    if (returnType.IsGenericConstruct)
                    {
                        IGenericType t = (IGenericType)(returnType);
                        if (!t.IsGenericDefinition)
                            returnType = (IGenericType)t.ElementType;
                    }
                    if (lastIsParams)
                    {
                        var lastParam = invocation.Parameters[invocation.Parameters.Count-1];
                        invocation.Parameters.Remove(lastParam);
                        var invocationRef = currentMethod.Locals.Add("invocationExpression", invocation);
                        var enumeration = currentMethod.Enumerate("lastParamArg", lastParam);
                        enumeration.Call(invocationRef.GetReference().GetProperty("Parameters").GetMethod("Add").Invoke(enumeration.Local.GetReference()));
                        returnValue = invocationRef.GetReference();
                    }
                    returnValue = CheckReturnType(manager, variation, currentMethod, returnType, returnValue, isCtor);
                    currentMethod.Return(returnValue);
                }
                currentMethod.AccessLevel = AccessLevelModifiers.Public;
            }
        }

        private static IExpression CheckReturnType(TypeGeneratorManager manager, TypeSignaturePattern variation, IIntermediateClassMethodMember currentMethod, IType returnType, IExpression returnValue, bool isConstructor)
        {
            if (returnType is IIntermediateType)
            {
                var actualTargetType = returnType;

                var generator = manager.GetGenerator((IIntermediateType)returnType);
                if (variation.ReturnType != returnType)
                    actualTargetType = ((IGenericType)(isConstructor ? generator.ResultType : generator.MethodInvocationType)).MakeGenericClosure(((IGenericType)variation.ReturnType).GenericParameters);
                else if (generator != null)
                    actualTargetType = isConstructor ? generator.ResultType : generator.MethodInvocationType;

                returnValue = actualTargetType.GetNewExpression(returnValue);
                currentMethod.ReturnType = actualTargetType;
            }
            return returnValue;
        }

        public static GenericParameterData GetGenericParameterFor(IIntermediateCliManager identityManager, IGenericParameter gParam)
        {
            GenericParameterData gpDetail;
            var rStr = identityManager.ObtainTypeReference(identityManager.RuntimeEnvironment.RootStruct);
            if (gParam.HasEmptyConstructor && !gParam.Constraints.Contains(rStr))
                gpDetail = new GenericParameterData(gParam.Name, new SignaturesData(new SignatureData()), gParam.Constraints.ToArray());
            else
                gpDetail = new GenericParameterData(gParam.Name, gParam.Constraints.ToArray());

            if (gpDetail.Constraints.Contains(rStr))
                gpDetail.Constraints.Remove(rStr);
            gpDetail.SpecialConstraint = ((IGenericParameter)gParam).SpecialConstraint;
            return gpDetail;
        }

        public IClassType StaticType { get; private set; }
        public IClassType InstanceType { get; private set; }


        internal void CompareParameterNames(NamedTypeSignaturesSet tester)
        {
            foreach (var element in this)
            {
                var relevantCopy =
                    tester.First(k => k.Equals(element));
                element.CompareParameterNames(relevantCopy);
            }
        }
    }

    internal class TypeSignaturePattern :
        HashList<IType>
    {
        public bool LastIsParams { get; set; }
        public IType ReturnType { get; set; }
        public bool IsStatic { get; set; }
        public HashList<GenericParameterData> GenericParameters { get; set; }
        public HashList<IType> ParentTypeParameters { get; set; }
        public HashList<string> ParameterNames { get; set; }

        public override bool Equals(HashList<IType> other)
        {
            if (!(other is TypeSignaturePattern))
                return false;
            var tspOther = (TypeSignaturePattern)other;
            if (tspOther.Count != this.Count)
                return false;
            if (tspOther.IsStatic != this.IsStatic)
                return false;
            if (tspOther.ReturnType != this.ReturnType)
                return false;
            if (tspOther.LastIsParams != this.LastIsParams)
                return false;
            if (tspOther.GenericParameters != null && this.GenericParameters == null)
                return false;
            if (tspOther.GenericParameters == null && this.GenericParameters != null)
                return false;
            if (tspOther.GenericParameters != null && this.GenericParameters != null &&
                !tspOther.GenericParameters.SequenceEqual(this.GenericParameters))
                return false;
            if (tspOther.ParentTypeParameters != null && this.ParentTypeParameters == null)
                return false;
            if (tspOther.ParentTypeParameters == null && this.ParentTypeParameters != null)
                return false;
            if (tspOther.ParentTypeParameters != null && this.ParentTypeParameters != null &&
                !tspOther.ParentTypeParameters.SequenceEqual(this.ParentTypeParameters))
                return false;
            if (tspOther.IsStatic && this.IsStatic &&
                tspOther.StaticParent != this.StaticParent)
                return false;
            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ (LastIsParams ? 8675309 : 0) ^ this.ReturnType.GetHashCode() ^ (this.IsStatic ? 1 : 0) << 4 ^ (this.GenericParameters != null ? this.GenericParameters.GetHashCode() : 0) ^ (this.IsStatic ? this.StaticParent.GetHashCode() : 0) ^ (this.ParentTypeParameters == null ? 0 : ParentTypeParameters.GetHashCode());
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            bool first = true;

            sb.Append(typeof(IType).Name);
            sb.Append("[");
            sb.Append(this.Count.ToString(CultureInfo.InvariantCulture));
            sb.Append("] {");
            int index = 0;
            foreach (var t in this)
            {
                if (first)
                    first = false;
                else
                    sb.Append(", ");
                if (index == this.Count - 1 && this.LastIsParams)
                    sb.Append("params ");
                sb.Append(t.ToString());
                if (index == this.Count - 1 && this.LastIsParams)
                    sb.Append("[]");
                index++;
            }
            sb.Append("}");
            return sb.ToString();

        }

        public IType StaticParent { get; set; }

        internal void CompareParameterNames(TypeSignaturePattern relevantCopy)
        {
            for (int i = 0; i < this.ParameterNames.Count; i++)
            {
                var myName = this.ParameterNames[i];
                var theirName = relevantCopy.ParameterNames[i];
                if (!myName.Equals(theirName))
                    this.ParameterNames[i] = string.Format("arg{0}", i);
            }
        }
    }
    internal class TypeGenerator
    {
        private IType targetType;
        private IIntermediateClassType resultType;

        private TypeGeneratorManager manager;
        private IIntermediateCliManager _identityManager;
        private IIntermediateClassType _methodInvocationType;
        private IIntermediateClassType _fieldReferenceType;
        private IIntermediateClassType _propertyReferenceType;
        public TypeGenerator(IType targetType, TypeGeneratorManager manager)
        {
            this.targetType = targetType;
            this.manager = manager;
        }

        public override string ToString()
        {
            return this.targetType.ToString();
        }

        public void BuildPlaceholders(IIntermediateCliManager idManager)
        {
            IIntermediateAssembly assembly = this.manager.GetAssemblyFor(this.targetType.Assembly);
            this._identityManager = idManager;
            bool needsResult;
            if ((!((needsResult = NeedsResultType)/* | (needsStat = NeedsStaticType)*/)))
                return;
            IIntermediateTypeParent typeParent;
            if (this.targetType.Parent is INamespaceDeclaration || this.targetType.Parent is IAssembly)
                typeParent = GetNamespace(string.Format("{0}.Exp", string.IsNullOrEmpty(this.TargetType.NamespaceName) ? "_" : this.TargetType.NamespaceName), assembly)/*.Parts.Add()*/;
            else
            {
                var parentGenerator = this.manager.GetGenerator((IType)this.targetType.Parent);
                typeParent = (IIntermediateTypeParent)((IIntermediateSegmentableType)parentGenerator.ResultType)/*.Parts.Add()*/;
            }

            GenericParameterData[] gpData = new GenericParameterData[0];
            if (this.targetType.IsGenericConstruct)
                gpData = ((IGenericType)this.targetType).TypeParameters.Values.Cast<IGenericTypeParameter>().Select(k =>
                {
                    GenericParameterData result;
                    var rStr = this._identityManager.ObtainTypeReference(this._identityManager.RuntimeEnvironment.RootStruct);
                    if (k.HasEmptyConstructor && !k.Constraints.Contains(rStr))
                        result = new GenericParameterData(k.Name, new SignaturesData(new SignatureData()), ((IGenericParameter)k).Constraints.ToArray());
                    else
                        result = new GenericParameterData(k.Name, ((IGenericParameter)k).Constraints.ToArray());

                    if (result.Constraints.Contains(rStr))
                        result.Constraints.Remove(rStr);
                    result.SpecialConstraint = ((IGenericParameter)k).SpecialConstraint;
                    
                    return result;
                }).ToArray();
            if (needsResult)
            {
                if (typeParent is IType)
                    this.resultType = typeParent.Classes.Add(this.targetType.Name, gpData);
                else
                    this.resultType = typeParent.Classes.Add(string.Format("{0}Exp", this.targetType.Name), gpData);
                this.resultType.AccessLevel = AccessLevelModifiers.Public;
            }
            //if (this.targetType.Parent is INamespaceDeclaration || this.targetType.Parent is IAssembly)
            //    typeParent = GetNamespace(string.Format("{0}.Exp", string.IsNullOrEmpty(this.TargetType.NamespaceName) ? "_" : this.TargetType.NamespaceName), assembly);
            //else
            //{
            //    var parentGenerator = this.manager.GetGenerator((IType)this.targetType.Parent);
            //    typeParent = (IIntermediateTypeParent)parentGenerator.StaticType;
            //}
            //if (needsStat)
            //{
            //    this.staticType = typeParent.Classes.Add(string.Format("{0}ExpS", this.targetType.Name), gpData);
            //    this.staticType.AccessLevel = AccessLevelModifiers.Public;
            //}
        }

        public static IIntermediateNamespaceDeclaration GetNamespace(string ns, IIntermediateAssembly assem)
        {
            var dottedName = ns.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            IIntermediateNamespaceParent insp = assem;
            string currentName = string.Empty;
            bool first = true;
            foreach (var namePoint in dottedName)
            {
                if (first)
                    first=false;
                else
                    currentName+=".";
                currentName += namePoint;
                if (insp.Namespaces.ContainsName(currentName))
                    insp = insp.Namespaces[currentName];
                else
                    break;
            }
            if (insp is IIntermediateNamespaceDeclaration)
            {
                var nsDecl = (IIntermediateNamespaceDeclaration)insp;
                if (nsDecl.FullName == ns)
                    return nsDecl;
            }
            return assem.Namespaces.Add(ns);
        }
        public void AssociateBaseTypes()
        {
            var kCol = typeof(KeyedCollection<,>).GetTypeReference();
            if (this.resultType != null)
            {
                var baseType = this.targetType;
                var genericTypeOvr = false;
                baseType = baseType.BaseType;
                ITypeCollection baseTypeGenericParams = null;
                if (baseType != null)
                {
                    if (baseType.IsGenericConstruct && !((IGenericType)baseType).IsGenericDefinition)
                    {
                        baseTypeGenericParams = ((IGenericType)baseType).GenericParameters.ToCollection();
                        baseType = baseType.ElementType;
                        genericTypeOvr = true;
                    }
                    var generatorBase = this.manager.GetGenerator(baseType);
                    IType instType = generatorBase.ResultType;
                    if (genericTypeOvr)
                        instType = ((IGenericType)instType).MakeGenericClosure(baseTypeGenericParams);
                    this.resultType.BaseType = (IClassType)instType;
                    BuildDefInheritedCtor();
                }
                else
                {
                    var rootType = this._identityManager.ObtainTypeReference(this._identityManager.RuntimeEnvironment.RootType);
                    if (this.targetType == rootType)
                    {
                        this.resultType.BaseType = (IClassType)typeof(MemberParentReferenceExpressionBase).GetTypeReference();
                        var typeOverride = this.resultType.Properties.Add(new TypedName("Type", typeof(ExpressionKind).GetTypeReference()), true, false);
                        typeOverride.AccessLevel = AccessLevelModifiers.Public;
                        var visit1 = this.resultType.Methods.Add(new TypedName("Visit", typeof(void).GetTypeReference()), new TypedNameSeries(new TypedName("visitor", typeof(IExpressionVisitor).GetTypeReference())));
                        visit1.AccessLevel = AccessLevelModifiers.Public;
                        visit1.IsOverride = true;
                        typeOverride.IsOverride = true;
                        var visit2 = this.resultType.Methods.Add(new TypedName("Visit", "TResult"), new TypedNameSeries(new TypedName("visitor", ((IInterfaceType)typeof(IExpressionVisitor<,>).GetTypeReference()).MakeGenericClosure("TResult".GetSymbolType(), "TContext".GetSymbolType())), new TypedName("context", "TContext")), new GenericParameterData("TContext"), new GenericParameterData("TResult"));
                        visit2.AccessLevel = AccessLevelModifiers.Public;
                        visit2.IsOverride = true;
                        var source = this.resultType.Fields.Add(new TypedName("_source", typeof(IExpression).GetTypeReference()));
                        source.AccessLevel = AccessLevelModifiers.Protected;
                        typeOverride.GetMethod.If(source.EqualTo(IntermediateGateway.NullValue)).Throw(typeof(InvalidOperationException).GetTypeReference().GetNewExpression("Source is null".ToPrimitive()));
                        typeOverride.GetMethod.Return(source.GetReference().GetProperty("Type"));
                        visit1.If(source.EqualTo(IntermediateGateway.NullValue)).Throw(typeof(InvalidOperationException).GetTypeReference().GetNewExpression("Source is null".ToPrimitive()));
                        visit2.If(source.EqualTo(IntermediateGateway.NullValue)).Throw(typeof(InvalidOperationException).GetTypeReference().GetNewExpression("Source is null".ToPrimitive()));
                        visit1.Call(source.GetReference().GetMethod("Visit").Invoke(visit1.Parameters["visitor"].GetReference()));
                        visit2.Return(source.GetReference().GetMethod("Visit").Invoke(visit2.Parameters["visitor"].GetReference(), visit2.Parameters["context"].GetReference()));
                        var defConst = this.resultType.Constructors.Add(new TypedName("source", typeof(IExpression).GetTypeReference()));
                        defConst.Assign(source, defConst.Parameters["source"]);
                        defConst.AccessLevel = AccessLevelModifiers.Public;
                        defConst = ((IIntermediateClassType)this.ResultType).Constructors.Add();
                        defConst.AccessLevel = AccessLevelModifiers.Protected;
                        var toStringOverride = ((IIntermediateClassType)this.ResultType).Methods.Add(new TypedName("ToString", typeof(string).GetTypeReference()));
                        toStringOverride.IsOverride = true;
                        toStringOverride.AccessLevel = AccessLevelModifiers.Public;
                        toStringOverride.Return(source.GetReference().GetMethod("ToString").Invoke());
                    }
                    else
                    {
                        var baseTypeGen = this.manager.GetGenerator(rootType);
                        this.resultType.BaseType = (IClassType)baseTypeGen.ResultType;
                        this.BuildDefInheritedCtor();
                    }
                }
            }
            //if (this.staticType != null)
            //{
            //    var baseType = this.targetType;
            //    baseType = baseType.BaseType;
            //    ITypeCollection baseTypeGenericParams = null;
            //    if (baseType != null)
            //    {
            //        if (baseType.IsGenericConstruct && !((IGenericType)baseType).IsGenericDefinition)
            //        {
            //            baseTypeGenericParams = ((IGenericType)baseType).GenericParameters.ToCollection();
            //            baseType = baseType.ElementType;
            //        }
            //        var generatorBase = this.manager.GetGenerator(baseType);
            //        var staticType = generatorBase.StaticType;
            //        if (baseType.IsGenericConstruct && !((IGenericType)baseType).IsGenericDefinition)
            //            staticType = ((IGenericType)staticType).MakeGenericClosure(baseTypeGenericParams);
            //        this.staticType.BaseType = (IClassType)staticType;
            //    }
            //}
        }

        private void BuildDefInheritedCtor()
        {
            IClassType thisType = this.TargetType as IClassType;
            if (thisType == null ||
               (thisType.SpecialModifier & SpecialClassModifier.Static) != SpecialClassModifier.Static)
            {
                var defConst = this.resultType.Constructors.Add(new TypedName("source", typeof(IExpression).GetTypeReference()));
                defConst.CascadeMembers.Add(defConst.Parameters["source"].GetReference());
                defConst.CascadeTarget = AllenCopeland.Abstraction.Slf.Ast.Members.ConstructorCascadeTarget.Base;
                defConst.AccessLevel = AccessLevelModifiers.Public;
            }
            else
                ((IIntermediateClassType)this.ResultType).Constructors.Add().AccessLevel = AccessLevelModifiers.Private;
        }

        public IClassType ResultType
        {
            get
            {
                if (this.resultType != null)
                    return this.resultType;
                else if (this.targetType.BaseType != null)
                {
                    var cleanBaseType = this.targetType.BaseType;
                    if (cleanBaseType.IsGenericConstruct)
                    {
                        cleanBaseType = cleanBaseType.ElementType;
                        return this.manager.GetGenerator(cleanBaseType).ResultType.MakeGenericClosure(((IGenericType)(this.targetType.BaseType)).GenericParameters);
                    }
                    return this.manager.GetGenerator(cleanBaseType).ResultType;
                }
                return null;
            }
        }

        public IIntermediateClassType MethodInvocationType
        {
            get
            {
                if (this._methodInvocationType == null)
                {
                    this._methodInvocationType = ((IIntermediateClassType)this.ResultType).Classes.Add("_MethodInvocation");
                    this._methodInvocationType.BaseType = this.ResultType;
                    this._methodInvocationType.AccessLevel = AccessLevelModifiers.Public;
                    var ctor = this._methodInvocationType.Constructors.Add(new TypedName("source", typeof(IMethodInvokeExpression).GetTypeReference()));
                    ctor.AccessLevel = AccessLevelModifiers.Public;
                    ctor.CascadeTarget = ConstructorCascadeTarget.Base;
                    ctor.CascadeMembers.Add("source".ParamRefOf(ctor));
                    this._methodInvocationType.ImplementedInterfaces.ImplementInterfaceQuick(typeof(IMethodInvokeExpression).GetTypeReference<IInterfaceType>());
                    var referenceProp = this._methodInvocationType.Properties.Add(new TypedName("Reference", typeof(IMethodPointerReferenceExpression).GetTypeReference()), true, false);
                    referenceProp.GetMethod.Return(this._methodInvocationType.GetThis().GetField("_source").Cast(typeof(IMethodInvokeExpression).GetTypeReference()).GetProperty("Reference"));
                    var parametersProp = this._methodInvocationType.Properties.Add(new TypedName("Parameters", typeof(IMalleableExpressionCollection).GetTypeReference()), true, false);
                    parametersProp.GetMethod.Return(this._methodInvocationType.GetThis().GetField("_source").Cast(typeof(IMethodInvokeExpression).GetTypeReference()).GetProperty("Parameters"));
                    referenceProp.AccessLevel = AccessLevelModifiers.Public;
                    parametersProp.AccessLevel = AccessLevelModifiers.Public;
                    var validAsStatement = this._methodInvocationType.Properties.Add(new TypedName("ValidAsStatement", typeof(bool).GetTypeReference()), true, false);
                    validAsStatement.GetMethod.Return(this._methodInvocationType.GetThis().GetField("_source").Cast(typeof(IMethodInvokeExpression).GetTypeReference()).GetProperty("ValidAsStatement"));
                    validAsStatement.AccessLevel = AccessLevelModifiers.Public;
                }
                
                return this._methodInvocationType;
            }
        }

        public IIntermediateClassType FieldReferenceType
        {
            get
            {
                if (this._fieldReferenceType == null)
                {
                    this._fieldReferenceType = ((IIntermediateClassType)this.ResultType).Classes.Add("_FieldReference");
                    this._fieldReferenceType.BaseType = this.ResultType;
                    this._fieldReferenceType.AccessLevel = AccessLevelModifiers.Public;
                    var ctor = this._fieldReferenceType.Constructors.Add(new TypedName("source", typeof(IFieldReferenceExpression).GetTypeReference()));
                    ctor.AccessLevel = AccessLevelModifiers.Public;
                    ctor.CascadeTarget = ConstructorCascadeTarget.Base;
                    ctor.CascadeMembers.Add("source".ParamRefOf(ctor));
                    this._fieldReferenceType.ImplementedInterfaces.ImplementInterfaceQuick(typeof(IFieldReferenceExpression).GetTypeReference<IInterfaceType>());
                    var referenceProp = this._fieldReferenceType.Properties.Add(new TypedName("Source", typeof(IMemberParentReferenceExpression).GetTypeReference()), true, false);
                    referenceProp.GetMethod.Return(this._fieldReferenceType.GetThis().GetField("_source").Cast(typeof(IFieldReferenceExpression).GetTypeReference()).GetProperty("Source"));
                    var nameProp = this._fieldReferenceType.Properties.Add(new TypedName("Name", typeof(string).GetTypeReference()), true, false);
                    nameProp.GetMethod.Return(this._fieldReferenceType.GetThis().GetField("_source").Cast(typeof(IFieldReferenceExpression).GetTypeReference()).GetProperty("Name"));
                    nameProp.AccessLevel = AccessLevelModifiers.Public;
                    referenceProp.AccessLevel = AccessLevelModifiers.Public;
                }

                return this._fieldReferenceType;
            }
        }

        public IIntermediateClassType PropertyReferenceType
        {
            get
            {
                if (this._propertyReferenceType == null)
                {
                    this._propertyReferenceType = ((IIntermediateClassType)this.ResultType).Classes.Add("_PropertyReference");
                    this._propertyReferenceType.BaseType = this.ResultType;
                    this._propertyReferenceType.AccessLevel = AccessLevelModifiers.Public;
                    var ctor = this._propertyReferenceType.Constructors.Add(new TypedName("source", typeof(IPropertyReferenceExpression).GetTypeReference()));
                    ctor.AccessLevel = AccessLevelModifiers.Public;
                    ctor.CascadeTarget = ConstructorCascadeTarget.Base;
                    ctor.CascadeMembers.Add("source".ParamRefOf(ctor));
                    this._propertyReferenceType.ImplementedInterfaces.ImplementInterfaceQuick(typeof(IPropertyReferenceExpression).GetTypeReference<IInterfaceType>());
                    var referenceProp = this._propertyReferenceType.Properties.Add(new TypedName("Source", typeof(IMemberParentReferenceExpression).GetTypeReference()), true, false);
                    referenceProp.GetMethod.Return(this._propertyReferenceType.GetThis().GetProperty("_source").Cast(typeof(IPropertyReferenceExpression).GetTypeReference()).GetProperty("Source"));
                    referenceProp.AccessLevel = AccessLevelModifiers.Public;
                    var nameProp = this._propertyReferenceType.Properties.Add(new TypedName("Name", typeof(string).GetTypeReference()), true, false);
                    nameProp.GetMethod.Return(this._propertyReferenceType.GetThis().GetProperty("_source").Cast(typeof(IPropertyReferenceExpression).GetTypeReference()).GetProperty("Name"));
                    nameProp.AccessLevel = AccessLevelModifiers.Public;
                    var referenceTypeProp = this._propertyReferenceType.Properties.Add(new TypedName("ReferenceType", typeof(MethodReferenceType).GetTypeReference()), true, false);
                    referenceTypeProp.GetMethod.Return(this._propertyReferenceType.GetThis().GetProperty("_source").Cast(typeof(IPropertyReferenceExpression).GetTypeReference()).GetProperty("ReferenceType"));
                    referenceTypeProp.AccessLevel = AccessLevelModifiers.Public;
                }

                return this._propertyReferenceType;
            }
        }

        //public IType StaticType
        //{
        //    get
        //    {
        //        if (this.staticType != null)
        //            return this.staticType;
        //        else if (this.targetType.BaseType != null)
        //        {
        //            var cleanBaseType = this.targetType.BaseType;
        //            if (cleanBaseType.IsGenericConstruct)
        //                cleanBaseType = cleanBaseType.ElementType;
        //            return this.manager.GetGenerator(cleanBaseType).StaticType;
        //        }
        //        return null;
        //    }
        //}

        public bool IsAccessible(AccessLevelModifiers accessLevel, IType owner = null)
        {
            if (owner != null)
            {
                if (owner is IClassType)
                {
                    var ict = (IClassType)owner;
                    if ((ict.SpecialModifier & SpecialClassModifier.Sealed) == SpecialClassModifier.Sealed)
                        return accessLevel == AccessLevelModifiers.Public;
                }
                else if (owner is IStructType)
                    return accessLevel == AccessLevelModifiers.Public;
            }
            return (accessLevel == AccessLevelModifiers.Public ||
                    accessLevel == AccessLevelModifiers.ProtectedOrInternal ||
                    accessLevel == AccessLevelModifiers.Protected);
        }

        public bool NeedsResultType
        {
            get
            {
                if (this.targetType.ToString() == "System.Drawing.Printing.PrintController, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")
                {
                }
                if (targetType is ITypeParent)
                {
                    var parentTarget = (ITypeParent)targetType;
                    if (parentTarget.Types.Values.Any(k => IsAccessible(k.Entry.AccessLevel, this.targetType)))
                        return true;
                }
                if (targetType is IClassType)
                {
                    IClassType ict = targetType as IClassType;
                    return ict.Properties.Values.Any(k => IsAccessible(k.AccessLevel, this.targetType)) ||
                           ict.Constructors.Values.Any(k => IsAccessible(k.AccessLevel, this.targetType)) ||
                           ict.Fields.Values.Any(k => IsAccessible(k.AccessLevel, this.targetType)) ||
                           ict.Indexers.Values.Any(k => IsAccessible(k.AccessLevel, this.targetType)) ||
                           ict.Methods.Values.Any(k => IsAccessible(k.AccessLevel, this.targetType)) ||
                           ict.IsGenericDefinition;
                }
                else if (targetType is IStructType)
                {
                    IStructType ist = targetType as IStructType;
                    return ist.Properties.Values.Any(k => IsAccessible(k.AccessLevel, this.targetType)) ||
                           ist.Constructors.Values.Any(k => IsAccessible(k.AccessLevel, this.targetType)) ||
                           ist.Fields.Values.Any(k => IsAccessible(k.AccessLevel, this.targetType)) ||
                           ist.Indexers.Values.Any(k => IsAccessible(k.AccessLevel, this.targetType)) ||
                           ist.Methods.Values.Any(k => IsAccessible(k.AccessLevel, this.targetType)) ||
                           ist.IsGenericDefinition;
                }
                else if (targetType is IEnumType)
                    return true;
                else if (targetType is IDelegateType)
                    return true;
                else if (targetType is IInterfaceType)
                    return true;
                return false;
            }
        }

        public bool NeedsInstanceType
        {
            get
            {
                if (targetType is IClassType)
                {
                    IClassType ict = targetType as IClassType;
                    return ict.Properties.Values.Any(k => !k.IsStatic && IsAccessible(k.AccessLevel, this.targetType)) ||
                           ict.Constructors.Values.Any(k => IsAccessible(k.AccessLevel, this.targetType)) ||
                           ict.Fields.Values.Any(k => !(k.IsStatic || k.Constant) && IsAccessible(k.AccessLevel, this.targetType)) ||
                           ict.Indexers.Values.Any(k => !k.IsStatic && IsAccessible(k.AccessLevel, this.targetType)) ||
                           ict.Methods.Values.Any(k => !k.IsStatic && IsAccessible(k.AccessLevel, this.targetType));
                }
                else if (targetType is IStructType)
                {
                    IStructType ist = targetType as IStructType;
                    return ist.Properties.Values.Any(k => !k.IsStatic && IsAccessible(k.AccessLevel, this.targetType)) ||
                           ist.Constructors.Values.Any(k => IsAccessible(k.AccessLevel, this.targetType)) ||
                           ist.Fields.Values.Any(k => !(k.IsStatic || k.Constant) && IsAccessible(k.AccessLevel, this.targetType)) ||
                           ist.Indexers.Values.Any(k => !k.IsStatic && IsAccessible(k.AccessLevel, this.targetType)) ||
                           ist.Methods.Values.Any(k => !k.IsStatic && IsAccessible(k.AccessLevel, this.targetType));
                }
                else if (targetType is IEnumType)
                    return false;
                else if (targetType is IDelegateType)
                    return true;
                else if (targetType is IInterfaceType)
                    return true;
                return false;
            }
        }
        public bool NeedsStaticType
        {
            get
            {
                if (targetType is IClassType)
                {
                    IClassType ict = targetType as IClassType;
                    return ict.Properties.Values.Any(k => k.IsStatic && IsAccessible(k.AccessLevel, this.targetType)) ||
                           ict.Fields.Values.Any(k => (k.IsStatic || k.Constant) && IsAccessible(k.AccessLevel, this.targetType)) ||
                           ict.Indexers.Values.Any(k => k.IsStatic && IsAccessible(k.AccessLevel, this.targetType)) ||
                           ict.Methods.Values.Any(k => k.IsStatic && IsAccessible(k.AccessLevel, this.targetType));
                }
                else if (targetType is IStructType)
                {
                    IStructType ist = targetType as IStructType;
                    return ist.Properties.Values.Any(k => k.IsStatic && IsAccessible(k.AccessLevel, this.targetType)) ||
                           ist.Fields.Values.Any(k => (k.IsStatic || k.Constant) && IsAccessible(k.AccessLevel, this.targetType)) ||
                           ist.Indexers.Values.Any(k => k.IsStatic && IsAccessible(k.AccessLevel, this.targetType)) ||
                           ist.Methods.Values.Any(k => k.IsStatic && IsAccessible(k.AccessLevel, this.targetType));
                }
                else if (targetType is IEnumType)
                    return true;
                else if (targetType is IDelegateType)
                    return false;
                else if (targetType is IInterfaceType)
                    return false;
                return false;
            }
        }

        public IType TargetType { get { return this.targetType; } }

        internal void BuildSignatures()
        {
            if (this.NeedsResultType)
            {
                var assem = (IIntermediateAssembly)this.ResultType.Assembly;
                if (this.targetType is IClassType ||
                    this.targetType is IStructType)
                {
                    var methods = ((IMethodParent)(this.targetType)).Methods.Values.Cast<IMethodMember>();
                    var indexers = ((IIndexerParent)(this.targetType)).Indexers.Values.Cast<IIndexerMember>();
                    var ctors = ((ICreatableParent)(this.targetType)).Constructors.Values.Cast<IConstructorMember>();
                    var indexerSigDetail = (from m in indexers
                                            //let instMember = (IInstanceMember)m
                                            where IsAccessible(m.AccessLevel, this.targetType)
                                            group m by m.Name into r
                                            from m in r
                                            group Tuple.Create(this.manager.GeneratePattern(m), m) by m.Name into r
                                            group new { SignatureSet = this.manager.GetNamedSignatureSet(r.Key, assem, r.Select(p => p.Item1).Distinct()), Key = r.Key, M = r } by r.Key).ToDictionary(k => k.Key, v => (from m in v
                                                                                                                                                                                                                  from k in m.M
                                                                                                                                                                                                                  group k by m.SignatureSet)
                                                                                                                                                                                                                .Select(k => Tuple.Create(k.Select(l => Tuple.Create(l.Item2, l.Item1)).ToArray(), k.Key)).FirstOrDefault());
                    var methodSigDetail = (from m in methods
                                           let instMember = (IInstanceMember)m
                                           where IsAccessible(m.AccessLevel, this.targetType)
                                           group m by m.Name into r
                                           from m in r
                                           group Tuple.Create(this.manager.GeneratePattern(m), m) by m.Name into r
                                           group new { SignatureSet = this.manager.GetNamedSignatureSet(r.Key, assem, r.Select(p => p.Item1).Distinct()), Key = r.Key, M = r } by r.Key).ToDictionary(k => k.Key, v => (from m in v
                                                                                                                                                                                                                 from k in m.M
                                                                                                                                                                                                                 group k by m.SignatureSet)
                                                                                                                                                                                                                .Select(k => Tuple.Create(k.Select(l => Tuple.Create(l.Item2, l.Item1)).ToArray(), k.Key)).FirstOrDefault());
                    var ctorSigDetail = (from m in ctors
                                         where ((ICreatableParent)(this.targetType)).TypeInitializer != m && IsAccessible(m.AccessLevel, this.targetType)
                                         group m by m.Name into r
                                         from m in r
                                         group Tuple.Create(this.manager.GeneratePattern(m), m) by m.Name into r
                                         group new { SignatureSet = this.manager.GetNamedSignatureSet(r.Key, assem, r.Select(p => p.Item1).Distinct()), Key = r.Key, M = r } by r.Key).ToDictionary(k => k.Key, v => (from m in v
                                                                                                                                                                                                               from k in m.M
                                                                                                                                                                                                               group k by m.SignatureSet)
                                                                                                                                                                                                                .Select(k => Tuple.Create(k.Select(l=>Tuple.Create(l.Item2, l.Item1)).ToArray(), k.Key)).FirstOrDefault());
                    
                    this.ResultDetail = new InstanceResultDetails { MethodSignatureSets = methodSigDetail, CtorSignatureSets = ctorSigDetail, IndexerSignatureSets = indexerSigDetail };
                }
                else if (this.targetType is IInterfaceType)
                {
                    var methods = ((IMethodSignatureParent)(this.targetType)).Methods.Values.Cast<IMethodSignatureMember>();
                    var indexers = ((IIndexerSignatureParent)(this.targetType)).Indexers.Values.Cast<IIndexerSignatureMember>();

                    var indexerSigDetail = (from m in indexers
                                            //let instMember = (IInstanceMember)m
                                            group m by m.Name into r
                                            from m in r
                                            group Tuple.Create(this.manager.GeneratePattern(m), m) by m.Name into r
                                            group new { SignatureSet = this.manager.GetNamedSignatureSet(r.Key, assem, r.Select(p => p.Item1).Distinct()), Key = r.Key, M = r } by r.Key).ToDictionary(k => k.Key, v => (from m in v
                                                                                                                                                                                                                  from k in m.M
                                                                                                                                                                                                                  group k by m.SignatureSet)
                                                                                                                                                                                                                .Select(k => Tuple.Create(k.Select(l => Tuple.Create(l.Item2, l.Item1)).ToArray(), k.Key)).FirstOrDefault());
                    var methodSigDetail = (from m in methods
                                           group m by m.Name into r
                                           from m in r
                                           group Tuple.Create(this.manager.GeneratePattern(m), m) by m.Name into r
                                           group new { SignatureSet = this.manager.GetNamedSignatureSet(r.Key, assem, r.Select(p => p.Item1).Distinct()), Key = r.Key, M = r } by r.Key).ToDictionary(k => k.Key, v => (from m in v
                                                                                                                                                                                                                 from k in m.M
                                                                                                                                                                                                                 group k by m.SignatureSet)
                                                                                                                                                                                                                .Select(k => Tuple.Create(k.Select(l => Tuple.Create(l.Item2, l.Item1)).ToArray(), k.Key)).FirstOrDefault());
                    this.ResultInterfaceDetail = new InterfaceResultDetails() { MethodSignatureSets = methodSigDetail, IndexerSignatureSets = indexerSigDetail };
                }
            }
        }

        public InstanceResultDetails ResultDetail { get; set; }
        public InterfaceResultDetails ResultInterfaceDetail { get; set; }

        internal void BuildStructure()
        {
            if (ResultDetail != null)
            {
                if (ResultDetail.MethodSignatureSets != null && ResultDetail.MethodSignatureSets.Count > 0)
                {
                    foreach (var methodDetailKey in ResultDetail.MethodSignatureSets.Keys)
                    {
                        var currentMethodDetail = ResultDetail.MethodSignatureSets[methodDetailKey];
                        if (currentMethodDetail.Item2.HasInstance && currentMethodDetail.Item2.HasStatic)
                        {
                            BuildMemberFor(currentMethodDetail.Item2, currentMethodDetail.Item2.StaticType, true, "S_");
                            BuildMemberFor(currentMethodDetail.Item2, currentMethodDetail.Item2.InstanceType, false);
                        }
                        else if (currentMethodDetail.Item2.HasInstance)
                            BuildMemberFor(currentMethodDetail.Item2, currentMethodDetail.Item2.InstanceType, false);
                        else
                            BuildMemberFor(currentMethodDetail.Item2, currentMethodDetail.Item2.StaticType, true);
                    }

                    foreach (var ctorDetailKey in ResultDetail.CtorSignatureSets.Keys)
                    {
                        var ctorDetail = ResultDetail.CtorSignatureSets[ctorDetailKey];
                        string overriddenName = "Create";
                        int index = 0;
                        while (this.ResultType.Members.Keys.Any(k => k.Name == overriddenName))
                            overriddenName = string.Format("Create{0}", ++index);
                        BuildMemberFor(ctorDetail.Item2, ctorDetail.Item2.InstanceType, true, nameOverride: overriddenName);
                    }
                }
            }
            else if (ResultInterfaceDetail != null)
            {
                if (ResultInterfaceDetail.MethodSignatureSets != null && ResultInterfaceDetail.MethodSignatureSets.Count > 0)
                {
                    foreach (var methodDetailKey in ResultInterfaceDetail.MethodSignatureSets.Keys)
                    {
                        var currentMethodDetail = ResultInterfaceDetail.MethodSignatureSets[methodDetailKey];
                        if (currentMethodDetail.Item2.HasInstance)
                            BuildMemberFor(currentMethodDetail.Item2, currentMethodDetail.Item2.InstanceType, false);
                    }
                }
            }
            if (this.TargetType is IFieldParent)
            {
                var fields =
                    ((IFieldParent)(this.TargetType)).Fields.Values.Cast<IFieldMember>().Where(k=>IsAccessible(k is IScopedDeclaration ? ((IScopedDeclaration)k).AccessLevel : AccessLevelModifiers.Public, this.TargetType)).ToArray();
                if (this.TargetType is IEnumType)
                    fields = fields.Where(k => k.Name != "value__").ToArray();
                foreach (var field in fields)
                {
                    var fieldType = this.manager.TypeInterpreter.GetExpectedType(field.FieldType, false);
                    if (fieldType is IIntermediateType || fieldType.ElementClassification == TypeElementClassification.GenericTypeDefinition && fieldType.ElementType is IIntermediateType)
                    {
                        TypeGenerator generator;
                        var originalFieldType = fieldType;
                        if (fieldType.ElementClassification == TypeElementClassification.GenericTypeDefinition && fieldType.ElementType is IIntermediateType)
                        {
                            generator = this.manager.GetGenerator((IIntermediateType)fieldType.ElementType);
                            fieldType = generator.FieldReferenceType.MakeGenericClosure(((IGenericType)fieldType).GenericParameters);
                        }
                        else
                        {
                            generator = this.manager.GetGenerator((IIntermediateType)fieldType);
                            fieldType = generator.FieldReferenceType;
                        }
                    }
                    string fieldName = string.Format("_{0}", field.Name.LowerFirstCharacter());
                    int index = 0;
                    while (resultType.Members.Keys.Any(k => k.Name == fieldName))
                        fieldName = string.Format("_{0}{1}", field.Name.LowerFirstCharacter(), ++index);
                    var backingField = ((IIntermediateClassType)this.ResultType).Fields.Add(new TypedName(fieldName, fieldType));
                    backingField.AccessLevel = AccessLevelModifiers.Private;
                    var resultProp = ((IIntermediateClassType)this.ResultType).Properties.Add(new TypedName(field.Name, fieldType), true, false);
                    resultProp.AccessLevel = AccessLevelModifiers.Public;
                    if (field is IInstanceMember)
                    {
                        var instField = (IInstanceMember)field;
                        if (instField.IsStatic)
                        {
                            backingField.IsStatic = true;
                            resultProp.IsStatic = true;
                        }
                    }
                    else if (field is IEnumFieldMember)
                    {
                        backingField.IsStatic = true;
                        resultProp.IsStatic = true;
                    }
                    var backingNullCheck = resultProp.GetMethod.If(backingField.EqualTo(IntermediateGateway.NullValue));
                    INaryOperandExpression innerExpression = null;
                    if (backingField.IsStatic)
                        innerExpression = typeof(ExpressionExtensions).GetTypeExpression(this._identityManager).GetMethod("GetTypeExpression").Invoke(new TypeOfExpression(this.TargetType), typeof(IntermediateCliGateway).GetTypeExpression(this._identityManager).GetProperty("GlobalManager")).GetMethod("GetField").Invoke(field.Name.ToPrimitive());
                    else
                        innerExpression = new SpecialReferenceExpression(SpecialReferenceKind.This).GetMethod("GetField").Invoke(field.Name.ToPrimitive());
                    //if (backingField.FieldType.ElementClassification == TypeElementClassification.GenericTypeDefinition && ((IGenericType)(backingField.FieldType)).ElementType == typeof(PrimitiveRepresentationExpression<>).GetTypeReference(this._identityManager))
                    //    innerExpression = backingField.FieldType.GetNewExpression(innerExpression);
                    if (backingField.FieldType is IIntermediateType || backingField.FieldType.ElementClassification == TypeElementClassification.GenericTypeDefinition && ((IGenericType)(backingField.FieldType)).ElementType is IIntermediateType)
                        innerExpression = fieldType.GetNewExpression(innerExpression);

                    backingNullCheck.Assign(backingField.GetReference(), innerExpression);

                    resultProp.GetMethod.Return(backingField.GetReference());
                }
                if (this.TargetType is IEnumType)
                {
                    var enumType = (IEnumType)this.TargetType;
                    var flagsAttr = enumType.Metadata.Any(k => k.Type == this._identityManager.ObtainTypeReference(this._identityManager.RuntimeEnvironment.CoreLibraryIdentifier.GetTypeIdentifier("System", "FlagsAttribute", 0)));
                    if (flagsAttr)
                    {
                        var bitwiseAndMethod = ((IIntermediateClassType)this.ResultType).Methods.Add(new TypedName("BitwiseAnd", this.ResultType), new TypedNameSeries(new TypedName("rightSide", this.ResultType)));
                        bitwiseAndMethod.Return(this.ResultType.GetNewExpression(typeof(CSharpExpressionExtensions).GetTypeExpression(this._identityManager).GetMethod("BitwiseAnd").Invoke(new SpecialReferenceExpression(SpecialReferenceKind.This), bitwiseAndMethod.Parameters["rightSide"].GetReference())));
                        bitwiseAndMethod.AccessLevel = AccessLevelModifiers.Public;
                        var bitwiseOrMethod = ((IIntermediateClassType)this.ResultType).Methods.Add(new TypedName("BitwiseOr", this.ResultType), new TypedNameSeries(new TypedName("rightSide", this.ResultType)));
                        bitwiseOrMethod.Return(this.ResultType.GetNewExpression(typeof(CSharpExpressionExtensions).GetTypeExpression(this._identityManager).GetMethod("BitwiseAnd").Invoke(new SpecialReferenceExpression(SpecialReferenceKind.This), bitwiseOrMethod.Parameters["rightSide"].GetReference())));
                        bitwiseOrMethod.AccessLevel = AccessLevelModifiers.Public;
                        var bitwiseAndCoercion = ((IIntermediateClassType)this.ResultType).BinaryOperatorCoercions.Add(CoercibleBinaryOperators.BitwiseAnd, this.ResultType);
                        bitwiseAndCoercion.Return(this.ResultType.GetNewExpression(typeof(CSharpExpressionExtensions).GetTypeExpression(this._identityManager).GetMethod("BitwiseAnd").Invoke(bitwiseAndCoercion.LeftSide.GetReference(), bitwiseAndCoercion.RightSide.GetReference())));
                        var bitwiseOrCoercion = ((IIntermediateClassType)this.ResultType).BinaryOperatorCoercions.Add(CoercibleBinaryOperators.BitwiseOr, this.ResultType);
                        bitwiseOrCoercion.Return(this.ResultType.GetNewExpression(typeof(CSharpExpressionExtensions).GetTypeExpression(this._identityManager).GetMethod("BitwiseOr").Invoke(bitwiseOrCoercion.LeftSide.GetReference(), bitwiseOrCoercion.RightSide.GetReference())));
                    }

                }
            }
            if (this.TargetType is IPropertyParent)
            {
                var properties =
                    ((IPropertyParent)(this.TargetType)).Properties.Values.Cast<IPropertyMember>().Where(k => IsAccessible(k is IScopedDeclaration ? ((IScopedDeclaration)k).AccessLevel : AccessLevelModifiers.Public, this.TargetType)).ToArray();
                foreach (var property in properties)
                {
                    var propertyType = this.manager.TypeInterpreter.GetExpectedType(property.PropertyType, false);
                    if (propertyType is IIntermediateType || propertyType.ElementClassification == TypeElementClassification.GenericTypeDefinition && propertyType.ElementType is IIntermediateType)
                    {
                        TypeGenerator generator;
                        var originalPropertyType = propertyType;
                        if (propertyType.ElementClassification == TypeElementClassification.GenericTypeDefinition && propertyType.ElementType is IIntermediateType)
                        {
                            generator = this.manager.GetGenerator((IIntermediateType)propertyType.ElementType);
                            propertyType = generator.PropertyReferenceType.MakeGenericClosure(((IGenericType)propertyType).GenericParameters);
                        }
                        else
                        {
                            generator = this.manager.GetGenerator((IIntermediateType)propertyType);
                            propertyType = generator.PropertyReferenceType;
                        }
                    }
                    string fieldName = string.Format("_{0}", property.Name.LowerFirstCharacter());
                    int index = 0;
                    while (resultType.Members.Keys.Any(k => k.Name == fieldName))
                        fieldName = string.Format("_{0}{1}", property.Name.LowerFirstCharacter(), ++index);
                    var backingField = ((IIntermediateClassType)this.ResultType).Fields.Add(new TypedName(fieldName, propertyType));
                    string propertyName = property.Name;
                    index = 0;
                    while (resultType.Members.Keys.Any(k => k.Name == propertyName))
                        propertyName = string.Format("{0}{1}", property.Name, ++index);

                    backingField.AccessLevel = AccessLevelModifiers.Private;
                    var resultProp = ((IIntermediateClassType)this.ResultType).Properties.Add(new TypedName(propertyName, propertyType), true, false);
                    resultProp.AccessLevel = AccessLevelModifiers.Public;
                    if (property is IInstanceMember)
                    {
                        var instProperty = (IInstanceMember)property;
                        if (instProperty.IsStatic)
                        {
                            backingField.IsStatic = true;
                            resultProp.IsStatic = true;
                        }
                    }
                    var backingNullCheck = resultProp.GetMethod.If(backingField.EqualTo(IntermediateGateway.NullValue));
                    INaryOperandExpression innerExpression = null;
                    if (backingField.IsStatic)
                        innerExpression = typeof(ExpressionExtensions).GetTypeExpression(this._identityManager).GetMethod("GetTypeExpression").Invoke(new TypeOfExpression(this.TargetType), typeof(IntermediateCliGateway).GetTypeExpression(this._identityManager).GetProperty("GlobalManager")).GetMethod("GetProperty").Invoke(property.Name.ToPrimitive());
                    else
                        innerExpression = new SpecialReferenceExpression(SpecialReferenceKind.This).GetMethod("GetProperty").Invoke(property.Name.ToPrimitive());
                    if (backingField.FieldType is IIntermediateType || backingField.FieldType.ElementClassification == TypeElementClassification.GenericTypeDefinition && ((IGenericType)(backingField.FieldType)).ElementType is IIntermediateType)
                        innerExpression = propertyType.GetNewExpression(innerExpression);

                    backingNullCheck.Assign(backingField.GetReference(), innerExpression);

                    resultProp.GetMethod.Return(backingField.GetReference());
                }
            }
        }

        internal void BuildMemberFor(NamedTypeSignaturesSet namedSet, IClassType expressionSet, bool isStatic, string prefixAdder = "", string nameOverride = "")
        {

            var targetField = ((IIntermediateClassType)this.ResultType).Fields.Add(new TypedName(string.Format("_{1}{0}", prefixAdder == string.Empty ? (nameOverride == string.Empty ? namedSet.Name : nameOverride).LowerFirstCharacter() : nameOverride == string.Empty ? namedSet.Name : nameOverride, prefixAdder == string.Empty ? string.Empty : prefixAdder.LowerFirstCharacter()), expressionSet));
            var propName = string.Format("{1}{0}", nameOverride == string.Empty ? namedSet.Name : nameOverride, prefixAdder);
            var propNameBase = propName;
            int index = 0;
            while (this.ResultType.Members.Keys.Any(k=>k.Name == propName))
                propName = string.Format("{0}{1}", propNameBase, ++index);
            var targetProperty = ((IIntermediateClassType)this.ResultType).Properties.Add(expressionSet.WithName(propName), true, false);
            if (isStatic)
            {
                targetField.IsStatic = true;
                targetProperty.IsStatic = true;
            }

            targetProperty.AccessLevel = AccessLevelModifiers.Public;
            targetField.AccessLevel = AccessLevelModifiers.Private;
            var propNullCheck = targetProperty.GetMethod.If(targetField.EqualTo(IntermediateGateway.NullValue));
            if (isStatic)
                propNullCheck.Assign(targetField, expressionSet.GetNewExpression());
            else
                propNullCheck.Assign(targetField, expressionSet.GetNewExpression(new SpecialReferenceExpression(SpecialReferenceKind.This)));
            targetProperty.GetMethod.Return(targetField);

            
        }
    }

    internal class StaticResultDetails
    {
        public Dictionary<string, Tuple<Tuple<IIndexerMember, TypeSignaturePattern>[],NamedTypeSignaturesSet>> IndexerSignatureSets { get; set; }
        public Dictionary<string, Tuple<Tuple<IMethodMember, TypeSignaturePattern>[],NamedTypeSignaturesSet>> MethodSignatureSets { get; set; }
    }
    internal class InstanceResultDetails :
        StaticResultDetails
    {
        public Dictionary<string, Tuple<Tuple<IConstructorMember, TypeSignaturePattern>[], NamedTypeSignaturesSet>> CtorSignatureSets { get; set; }
    }

    internal class InterfaceResultDetails
    {
        public Dictionary<string, Tuple<Tuple<IIndexerSignatureMember, TypeSignaturePattern>[], NamedTypeSignaturesSet>> IndexerSignatureSets { get; set; }
        public Dictionary<string, Tuple<Tuple<IMethodSignatureMember, TypeSignaturePattern>[], NamedTypeSignaturesSet>> MethodSignatureSets { get; set; }
    }

}
