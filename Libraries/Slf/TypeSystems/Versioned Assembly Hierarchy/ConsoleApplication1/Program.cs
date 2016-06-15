using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Cli;
//using AllenCopeland.Abstraction.Slf.Mva;
using AllenCopeland.Abstraction.Slf.Vre;
using AllenCopeland.Abstraction.Utilities;
using MVATests.Properties;
using MVATests.VreModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using AllenCopeland.Abstraction;
namespace MVATests
{
    static class Program
    {
        //private static readonly CliFrameworkVersion[] frameworkVersions = new[] { CliFrameworkVersion.v1_0_3705, CliFrameworkVersion.v1_1_4322, CliFrameworkVersion.v2_0_50727, CliFrameworkVersion.v3_0, CliFrameworkVersion.v3_5, CliFrameworkVersion.v4_0_30319 };

        public static byte[] StringToBytes(this string byteData)
        {
            byte[] result = new byte[byteData.Length * sizeof(char)];
            Buffer.BlockCopy(byteData.ToCharArray(), 0, result, 0, result.Length);
            return result;
        }

        public static byte[] DecompressByteStream(this byte[] compressedData)
        {
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(compressedData, 0, compressedData.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);
            using (var gzStream = new GZipStream(memoryStream, CompressionMode.Decompress, true))
            using (var ms = new MemoryStream())
            {
                gzStream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static byte[] CompressByteStream(this byte[] decompressedData)
        {
            using (var memoryStream = new MemoryStream())
            using (var gzStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gzStream.Write(decompressedData, 0, decompressedData.Length);
                gzStream.Flush();
                gzStream.Dispose();
                memoryStream.Seek(0, SeekOrigin.Begin);
                BinaryReader memoryReader = new BinaryReader(memoryStream);
                byte[] result = memoryReader.ReadBytes((int)memoryStream.Length);
                return result;
            }
        }

        public static string BytesToString(this byte[] stringData)
        {
            char[] resultBody = new char[stringData.Length / sizeof(char) + (((stringData.Length % 2) == 0) ? 0 : 1)];
            Buffer.BlockCopy(stringData, 0, resultBody, 0, stringData.Length);
            return new string(resultBody);
        }

        static void Main(string[] args)
        {

            var xmlDoc = new XmlDocument();
            //const string fName = @"C:\Projects\Code\C#\Abstraction\SLF\TypeSystems\Versioned Assembly Hierarchy\Vre\clr.gz";
            //using (var fStream = new FileStream(fName, FileMode.OpenOrCreate, FileAccess.Write))
            //{
            //    var data = Resources.CommonLanguageRuntime.StringToBytes().CompressByteStream();
            //    fStream.Write(data, 0, data.Length);
            //}
            using (StringReader sr = new StringReader(Resources.clr.DecompressByteStream().BytesToString()))
                xmlDoc.Load(sr);

            //Process(xmlDoc);

            //return;
            IterInfo(xmlDoc);
            //GInfo(xmlDoc);

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private static void IterInfo(XmlDocument xmlDoc)
        {
            using (var vrEnv = VreXmlGateway.GetVersionedEnvironment<CliRuntimeEnvironment, CliRuntimeEnvironmentVersion, ICliManager>(xmlDoc, (xmlNamespaceManager) => new CliRuntimeEnvironment(xmlDoc, xmlNamespaceManager)))
            {
                IVreType<CliRuntimeEnvironment, CliRuntimeEnvironmentVersion, ICliManager>[] mm;
                TimeSpan mue;
                var mued = vrEnv.Libraries["mscorlib"];
                var qued = mued.Versions;
                qued[0].Value.UniqueIdentifier.ToString();
                GetSetAndTime(vrEnv.CurrentVersion.PreviousMajorVersion, out mm, out mue);
                foreach (var obsType in mm)
                    Console.WriteLine(obsType);
                Console.WriteLine("Took {0}ms", mue.TotalMilliseconds);
                GetSetAndTime(vrEnv.CurrentVersion, out mm, out mue);
                Console.WriteLine("Took {0}ms", mue.TotalMilliseconds);
            }
        }

        private static void GetSetAndTime(CliRuntimeEnvironmentVersion version, out IVreType<CliRuntimeEnvironment, CliRuntimeEnvironmentVersion, ICliManager>[] mm, out TimeSpan mue)
        {
            Stopwatch f = Stopwatch.StartNew();
            mm = version.DeprecatedTypes.ToArray();
            f.Stop();
            mue = f.Elapsed;
        }

        private static void GInfo(XmlDocument xmlDoc)
        {
            XNamespace vreNamespace = XNamespace.Get(@"http://www.AlexanderMorou.com/schemas/VersionedRuntimeEnvironment/2015/vre.xsd");
            /* *
             * Each version of each environment needs its own identity manager to avoid overlapping identities pulling
             * the previous version's instance of a given library.
             * */
            Dictionary<string, ISymbolType> symbolReplacements = new Dictionary<string, ISymbolType>();
            IIdentityManager commonIdentityHandlerForGenerics = CliGateway.CreateIdentityManager(CliFrameworkPlatform.AnyPlatform);
            HashSet<string> publicKeyTokenStrings = new HashSet<string>();
            Dictionary<string, int> publicKeyTokenIds = new Dictionary<string, int>();
            using (commonIdentityHandlerForGenerics)
            using (var vrEnv = VreXmlGateway.GetVersionedEnvironment<CliRuntimeEnvironment, CliRuntimeEnvironmentVersion, ICliManager>(xmlDoc, (xmlNamespaceManager) => new CliRuntimeEnvironment(xmlDoc, xmlNamespaceManager)))
            {
                var identityManagers = from v in vrEnv.Versions
                                       select v.IdentityManager;
                foreach (var identityManager in identityManagers)
                    identityManager.AssemblyLoaded += identityManager_AssemblyLoaded;
                var versionPaths = (from v in vrEnv.Versions
                                    from p in v.HintPaths
                                    let dirInfo = new DirectoryInfo(p)
                                    where dirInfo.Exists
                                    let files = dirInfo.GetFiles("*.dll")
                                    from f in files
                                    //where f.Name.ToLower() == "system.dll"
                                    where CliGateway.IsFullAssembly(f.FullName)
                                    group new { Path = p, Assembly = ((ICliManager)v.IdentityManager).ObtainAssemblyReference(f.FullName) } by v).ToDictionary(k => k.Key, v => (from v2 in v
                                                                                                                                                                                 group v2.Assembly by v2.Path).ToDictionary(k2 => k2.Key, v2 => v2.ToArray()));

                var uniqueNames = (from version in versionPaths.Keys
                                   from hintPath in versionPaths[version].Keys
                                   from library in versionPaths[version][hintPath]
                                   let firstObservedVersion = (from versionInner in versionPaths.Keys
                                                               from hintPathInner in versionPaths[versionInner].Keys
                                                               from libraryInner in versionPaths[versionInner][hintPathInner]
                                                               where libraryInner.Name == library.Name
                                                               orderby versionInner.VersionQualifier,
                                                                       hintPathInner,
                                                                       libraryInner.Name
                                                               select versionInner).FirstOrDefault()
                                   where firstObservedVersion != null
                                   orderby firstObservedVersion.VersionQualifier,
                                           library.Name
                                   select new { firstObservedVersion, library.UniqueIdentifier.Name }).Distinct().ToArray();
                var libraryVersions =
                    (from u in uniqueNames
                     let thisLibraryVersions =
                         (from v in versionPaths.Keys
                          from p in versionPaths[v].Keys
                          from l in versionPaths[v][p]
                          orderby v.VersionQualifier
                          where l.Name == u.Name
                          select new { Library = l, Version = v }).Distinct()
                     select new LibraryInfo { Name = u.Name, Versions = thisLibraryVersions.ToDictionary(k => k.Version, v => v.Library), FirstVersion = u.firstObservedVersion }).ToArray();
                publicKeyTokenStrings = new HashSet<string>(from l in libraryVersions
                                                            from v in l.Versions.Values
                                                            select v.UniqueIdentifier.PublicKeyToken.FormatHexadecimal());
                foreach (var str in publicKeyTokenStrings)
                    publicKeyTokenIds.Add(str, publicKeyTokenIds.Count);

                var libraryTypes =
                    (from library in libraryVersions
                     from v in library.Versions.Keys
                     let versionedLibrary = library.Versions[v]
                     let types = (from t in versionedLibrary.GetTypes()
                                  where IsAccessible(t)
                                  let typeName = t.FullName
                                  where t.NamespaceName != "XamlGeneratedNamespace"
                                  select new TypeAndName { Type = t, Name = typeName, Namespace = t.NamespaceName }).ToArray()
                     select new { Library = versionedLibrary, Types = types }).ToDictionary(k => k.Library, v => v.Types);
                var distinctTypeNames =
                    (from library in libraryVersions.AsParallel()
                     from versionedLibrary in library.Versions.Values
                     from t in libraryTypes[versionedLibrary]
                     let fullName = t.Name
                     orderby fullName
                     select new { Name = fullName, Namespace = t.Namespace }).Distinct().ToArray();
                var comparer = new MemberComparator(commonIdentityHandlerForGenerics);
                var maxNameLen = (from l in libraryVersions
                                  select l.Name.Length).Max();
                var typeVersions =
                    (from name in distinctTypeNames.AsParallel()
                     let versions = (from library in libraryVersions
                                     from v in library.Versions.Keys
                                     let versionedLibrary = library.Versions[v]
                                     from t in libraryTypes[versionedLibrary]
                                     let fullName = t.Name
                                     where fullName == name.Name
                                     orderby v.VersionQualifier ascending
                                     group t.Type by v).ToDictionary(k => k.Key, v => v.FirstOrDefault())
                     let members = (from library in libraryVersions
                                    from v in library.Versions.Keys
                                    let versionedLibrary = library.Versions[v]
                                    from t in libraryTypes[versionedLibrary]
                                    //let genericVariation = t.Type.IsGenericConstruct ? CreateGenericSimulant(((IGenericType)t.Type), symbolReplacements, commonIdentityHandlerForGenerics) : t.Type
                                    where name.Name == t.Name
                                    from memberEntry in t.Type.Members.Values
                                    let member = memberEntry.Entry
                                    where IsAccessible(member, t.Type)
                                    let uid = member.UniqueIdentifier
                                    select new { v, uid }).GroupBy(k => k.uid, v => Tuple.Create(v.uid, v.v), comparer)
                     orderby name.Namespace ascending,
                             name.Name ascending
                     group new { Name = string.IsNullOrEmpty(name.Namespace) ? name.Name : name.Name.Substring(name.Namespace.Length + 1), Versions = versions, Members = members, Namespace = name.Namespace } by
                     (name.Namespace ?? string.Empty)).AsEnumerable().OrderBy(k => k.Key).ToDictionary(k => k.Key, v =>
                         v.ToDictionary(k2 => (k2.Name ?? string.Empty), v2 =>
                             new TypeVersionDetails
                             {
                                 Members =
                                     v2.Members.Where(k => k.Key != null).ToDictionary(k3 => k3.Key, v3 => v3.ToArray()),
                                 Versions = v2.Versions
                             }));
                XDocument environmentDocument = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement(
                        vreNamespace + "Environment",
                        new XAttribute(XNamespace.Xmlns + "vre", vreNamespace.NamespaceName),
                        new XAttribute("Name", vrEnv.Name),
                        new XAttribute("CurrentVersion", vrEnv.CurrentVersion.VersionText),
                        RemoveXElementVre(((XmlDocument)vrEnv.XmlNode).DocumentElement.SelectSingleNode("./vre:Versions", vrEnv.XmlNamespaceManager), vreNamespace),
                        new XElement(vreNamespace + "Libraries",
                            from library in
                                (from library in libraryVersions
                                 orderby library.Name ascending
                                 select library)
                            select BuildXLibrary(library, publicKeyTokenIds, vreNamespace)),
                        BuildNamespaces((from @namespace in typeVersions.Keys
                                         from typeName in typeVersions[@namespace].Keys
                                         let type = typeVersions[@namespace][typeName]
                                         where !type.Versions.Values.Any(k => k.Parent is IType)
                                         group type by @namespace).ToDictionary(k => k.Key, v => v.ToArray()), typeVersions, vreNamespace),
                        new XElement(vreNamespace + "PublicKeyTokens",
                            from idDetails in publicKeyTokenIds
                            select new XElement(vreNamespace + "PublicKey",
                                new XAttribute("Token",idDetails.Key),
                                new XAttribute("Id", idDetails.Value)))));
                Console.WriteLine(environmentDocument);

                //foreach (var library in from l in libraryVersions
                //                        orderby l.Name ascending
                //                        select l)
                //{
                //    var versionData = library.Versions;

                //    var firstVersion = library.Versions.Values.First();
                //    Console.WriteLine(@"<vre:Library Name=""{0}"" {2}Introduced=""{1}""{3}>", library.Name, library.FirstVersion.VersionText, new string(' ', maxNameLen - firstVersion.Name.Length), library.FirstVersion.IsServicePack ? string.Format(@" IntroducedServicePack=""SP{0}""", library.FirstVersion.ServicePackLevel) : string.Empty);
                //    if (string.Format("{0}.dll", firstVersion.Name).ToLower() != firstVersion.ManifestModule.Name.ToLower() || firstVersion.Modules.Count > 1)
                //    {
                //        Console.WriteLine(@"    <vre:Modules>");
                //        Console.WriteLine(@"        <vre:ManifestModule Name=""{0}""/>", firstVersion.ManifestModule.Name);
                //        if (firstVersion.Modules.Count > 1)
                //            foreach (var module in firstVersion.Modules.Values)
                //                if (module != firstVersion.ManifestModule)
                //                    Console.WriteLine(@"        <vre:Module Name=""{0}""/>", module.Name);
                //        Console.WriteLine(@"    </vre:Modules>");
                //    }
                //    EmitLibraryVersionHistory(versionData);
                //    Console.WriteLine(@"</vre:Library>");
                //}

                //EmitNamespaceInfo();
            }

            commonIdentityHandlerForGenerics.Dispose();
            foreach (var element in symbolReplacements.Values)
                element.Dispose();
            symbolReplacements.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private static XElement RemoveXElementVre(XmlNode xmlElement, XNamespace vreNamespace)
        {
            var parse = XElement.Parse(xmlElement.OuterXml);
            var result = new XElement(vreNamespace + parse.Name.LocalName, from attr in parse.Attributes()
                                                                           where string.IsNullOrEmpty(attr.Name.NamespaceName)
                                                                           select attr, parse.Elements());
            return result;
        }

        private static XElement BuildXLibrary(LibraryInfo library, Dictionary<string, int> publicKeyIDs, XNamespace vreNamespace)
        {
            var firstVersionAssembly = library.Versions.Values.First();
            return new XElement(vreNamespace + "Library",
                BuildXLibraryAttributes(library, vreNamespace, firstVersionAssembly),
                BuildXLibraryElements(library, vreNamespace, publicKeyIDs, firstVersionAssembly));
        }

        private static IEnumerable<XAttribute> BuildXLibraryAttributes(LibraryInfo library, XNamespace vreNamespace, IAssembly firstVersionAssembly)
        {
            yield return new XAttribute("Name", library.Name);
            yield return new XAttribute("Introduced", library.FirstVersion.VersionText);
            if (library.FirstVersion.IsServicePack)
                yield return new XAttribute("IntroducedServicePack", string.Format("SP{0}", library.FirstVersion.ServicePackLevel));
        }

        private static IEnumerable<XElement> BuildXLibraryElements(LibraryInfo library, XNamespace vreNamespace, Dictionary<string, int> publicKeyIDs, IAssembly firstVersionAssembly)
        {
            if (firstVersionAssembly.Modules.Count > 1 || string.Format("{0}.dll", firstVersionAssembly.Name.ToLower()) != firstVersionAssembly.ManifestModule.Name.ToLower())
                yield return new XElement(vreNamespace + "Modules",
                    from module in firstVersionAssembly.Modules.Values
                    select new XElement(vreNamespace + string.Format("{0}Module", (module == firstVersionAssembly.ManifestModule) ? "Manifest" : string.Empty), new XAttribute("Name", module.Name)));
            yield return new XElement(vreNamespace + "VersionHistory",
                BuildXLibraryVersionHistory(library, vreNamespace, publicKeyIDs, firstVersionAssembly));
        }

        private static IEnumerable<XElement> BuildXLibraryVersionHistory(LibraryInfo library, XNamespace vreNamespace, Dictionary<string, int> publicKeyIDs, IAssembly firstVersionAssembly)
        {
            ICliAssembly previousVersion = null;
            foreach (var version in library.Versions.Keys)
            {
                var versionedAssembly = library.Versions[version];
                ICliAssembly cliAssembly = (ICliAssembly)versionedAssembly;
                foreach (var tableKind in cliAssembly.MetadataRoot.TableStream.Keys)
                    cliAssembly.MetadataRoot.TableStream[tableKind].Read();
                if (previousVersion == null || versionedAssembly.UniqueIdentifier.Version.ToString() != previousVersion.UniqueIdentifier.Version.ToString())
                    yield return new XElement(vreNamespace + "Version",
                        BuildXLibraryVersionDetailAttributes(library, vreNamespace, publicKeyIDs, firstVersionAssembly, version, cliAssembly, previousVersion));
                previousVersion = cliAssembly;
            }
        }

        private static IEnumerable<XAttribute> BuildXLibraryVersionDetailAttributes(LibraryInfo library, XNamespace vreNamespace, Dictionary<string, int> publicKeyIDs, IAssembly firstVersionAssembly, CliRuntimeEnvironmentVersion version, ICliAssembly currentAssembly, ICliAssembly previousAssembly)
        {
            yield return new XAttribute("Runtime", version.VersionText);
            if (version.IsServicePack)
                yield return new XAttribute("ServicePack", string.Format("SP{0}", version.ServicePackLevel));
            yield return new XAttribute("Value", currentAssembly.UniqueIdentifier.Version.ToString());
            int publicKeyId;
            if (publicKeyIDs.TryGetValue(currentAssembly.UniqueIdentifier.PublicKeyToken.FormatHexadecimal(), out publicKeyId))
                yield return new XAttribute("PublicKeyId", publicKeyId);
        }


        //private static void EmitLibraryVersionHistory(Dictionary<CliRuntimeEnvironmentVersion, IAssembly> versionData)
        //{
        //    Console.WriteLine(@"    <vre:VersionHistory>");
        //    IAssembly previousVersion = null;
        //    foreach (var version in versionData.Keys)
        //    {
        //        var versionedAssembly = versionData[version];
        //        ICliAssembly cliAssembl = (ICliAssembly)versionedAssembly;
        //        foreach (var tableKind in cliAssembl.MetadataRoot.TableStream.Keys)
        //            cliAssembl.MetadataRoot.TableStream[tableKind].Read();
        //        if (previousVersion == null || versionedAssembly.UniqueIdentifier.Version.ToString() != previousVersion.UniqueIdentifier.Version.ToString())
        //            if (version.IsServicePack)
        //                Console.WriteLine(@"        <vre:Version Runtime=""{0}"" ServicePack=""{1}"" Value=""{2}""/>", version.VersionText, string.Format(@"SP{0}",version.ServicePackLevel), versionedAssembly.UniqueIdentifier.Version);
        //            else
        //                Console.WriteLine(@"        <vre:Version Runtime=""{0}"" Value=""{1}""/>", version.VersionText, versionedAssembly.UniqueIdentifier.Version);
        //        previousVersion = versionedAssembly;
        //    }
        //    Console.WriteLine(@"    </vre:VersionHistory>");
        //}

        static void identityManager_AssemblyLoaded(object sender, CliAssemblyLoadedEventArgs e)
        {
            if (!e.AssemblyLocation.ToLower().Contains(@"C:\Internet Downloads\Executables\Applications\Microsoft\.Net\Old Versions".ToLower()))
            {

            }
        }

        private static XElement BuildTypeInfo(TypeVersionDetails typeInfo, Dictionary<string, Dictionary<string, TypeVersionDetails>> typeVersions, XNamespace vreNamespace)
        {
            IAssembly currentAssembly = null;
            return new XElement(vreNamespace + "Type",
                BuildTypeAttributes(typeInfo, typeVersions, vreNamespace),
                BuildTypeElements(typeInfo, typeVersions, vreNamespace));
        }

        private static IEnumerable<XElement> BuildTypeElements(TypeVersionDetails typeInfo, Dictionary<string, Dictionary<string, TypeVersionDetails>> typeVersions, XNamespace vreNamespace)
        {
            var childTypeLookup = (from v in typeInfo.Versions.Keys
                                   let t = typeInfo.Versions[v]
                                   where t is ITypeParent
                                   let tParent = (ITypeParent)t
                                   from childEntry in tParent.Types.Values
                                   let child = childEntry.Entry
                                   let namespaceName = child.NamespaceName
                                   let fullName = child.FullName
                                   group fullName by namespaceName).ToDictionary(k => k.Key, v => v.Distinct().ToArray());
            var childTypeInfo =
                (from namespaceName in childTypeLookup.Keys
                 from childName in childTypeLookup[namespaceName]
                 where typeVersions.ContainsKey(namespaceName) &&
                       typeVersions[namespaceName].ContainsKey(childName)
                 group typeVersions[namespaceName][childName] by namespaceName).ToDictionary(k => k.Key, v => v.ToArray());
            var versionInfo = new XElement(vreNamespace + "VersionHistory", BuildTypeVersionHistoryElements(typeInfo, typeVersions, vreNamespace));
            foreach (var element in BuildNamespaces(childTypeInfo, typeVersions, vreNamespace))
                yield return element;
            if (versionInfo.Elements().Count() > 0)
                yield return versionInfo;

#if IncludeMemberInfo
            foreach (var element in BuildTypeMemberInfo(typeInfo, vreNamespace))
                yield return element;
#endif
        }

        private static IEnumerable<XElement> BuildTypeVersionHistoryElements(TypeVersionDetails typeInfo, Dictionary<string, Dictionary<string, TypeVersionDetails>> typeVersions, XNamespace vreNamespace)
        {
            IAssembly currentAssembly = null;
            bool obsoleted=false;
            foreach (var version in typeInfo.Versions.Keys)
            {
                var versionedType = typeInfo.Versions[version];
                if (currentAssembly != null && versionedType.Assembly.Name != currentAssembly.Name)
                {
                    if (version.IsServicePack)
                        yield return new XElement(vreNamespace + "ForwardedTo", new XAttribute("Version", version.VersionText), new XAttribute("Library", versionedType.Assembly.Name), new XAttribute("ServicePack", string.Format("SP{0}", version.ServicePackLevel)));
                    else
                        yield return new XElement(vreNamespace + "ForwardedTo", new XAttribute("Version", version.VersionText), new XAttribute("Library", versionedType.Assembly.Name));
                }
                if (versionedType.Metadata.Any(k => k.Type.Name == "ObsoleteAttribute" && k.Type.NamespaceName == "System") && !obsoleted)
                {
                    if (version.IsServicePack)
                        yield return new XElement(vreNamespace + "Deprecated", new XAttribute("Version", version.VersionText), new XAttribute("ServicePack", string.Format("SP{0}", version.ServicePackLevel)));
                    else
                        yield return new XElement(vreNamespace + "Deprecated", new XAttribute("Version", version.VersionText));
                    obsoleted = true;
                }
                else if (!(versionedType.Metadata.Any(k => k.Type.Name == "ObsoleteAttribute" && k.Type.NamespaceName == "System")) && obsoleted)
                {
                    if (version.IsServicePack)
                        yield return new XElement(vreNamespace + "Supported", new XAttribute("Version", version.VersionText), new XAttribute("ServicePack", string.Format("SP{0}", version.ServicePackLevel)));
                    else
                        yield return new XElement(vreNamespace + "Supported", new XAttribute("Version", version.VersionText));
                    obsoleted = false;
                }
                currentAssembly = versionedType.Assembly;
            }
        }


        private static int TypeIdentity = 0;
        private static object TypeIdentityLocker = new object();
        private static IEnumerable<XAttribute> BuildTypeAttributes(TypeVersionDetails typeInfo, Dictionary<string, Dictionary<string, TypeVersionDetails>> typeVersions, XNamespace vreNamespace)
        {
            int typeId = 0;
            lock (TypeIdentityLocker)
                typeId = TypeIdentity++;
            var fType = typeInfo.Versions.Values.First();
            int tParamCount = 0;
            if (fType.IsGenericConstruct)
            {
                IGenericType gType = (IGenericType)fType;
                tParamCount = gType.TypeParameters.Count;
            }
            yield return new XAttribute("Name", string.Format("{0}{1}", fType.Name, tParamCount > 0 ? string.Format("`{0}", tParamCount) : string.Empty));
            yield return new XAttribute("TypeKind", fType.Type);
            yield return new XAttribute("TypeId", typeId);
            var firstVersion = typeInfo.Versions.Keys.FirstOrDefault();
            if (firstVersion != null)
            {
                yield return new XAttribute("Introduced", firstVersion.VersionText);
                var firstAssembly = typeInfo.Versions[firstVersion].Assembly;
                if (firstAssembly != null)
                    yield return new XAttribute("InitialLibrary", firstAssembly.Name);
            }
        }

        private static IEnumerable<XElement> BuildTypeMemberInfo(TypeVersionDetails typeInfo, XNamespace vreNamespace)
        {
            foreach (var initialMemberId in typeInfo.Members.Keys)
            {
                var currentMemberVersionSet = (from mvInfo in typeInfo.Members[initialMemberId]
                                               orderby mvInfo.Item2
                                               select new { Version = mvInfo.Item2, Id = mvInfo.Item1, Member = typeInfo.Versions[mvInfo.Item2].Members[(IGeneralMemberUniqueIdentifier)mvInfo.Item1].Entry }).ToArray();
                var first = currentMemberVersionSet.FirstOrDefault();
                var firstDepreciated = (from cmvs in currentMemberVersionSet
                                        let member = cmvs.Member
                                        where member is IMetadataEntity
                                        let metadataEntity = (IMetadataEntity)member
                                        from IMetadatum metadatum in metadataEntity.Metadata
                                        where metadatum.Type.NamespaceName == "System" && 
                                              metadatum.Type.Name == "ObsoleteAttribute"
                                        select cmvs.Version).FirstOrDefault();
                if (first.Member is IBinaryOperatorCoercionMember)
                    yield return BuildMemberInfo(from member in currentMemberVersionSet
                                                 select Tuple.Create((IBinaryOperatorCoercionMember)(member.Member), ((IBinaryOperatorCoercionMember)member.Member).UniqueIdentifier, member.Version), (IBinaryOperatorCoercionMember)first.Member, firstDepreciated, vreNamespace);
                else if (first.Member is IConstructorMember)
                    yield return BuildMemberInfo(from member in currentMemberVersionSet
                                                 select Tuple.Create((IConstructorMember)(member.Member), ((IConstructorMember)member.Member).UniqueIdentifier, member.Version), (IConstructorMember)first.Member, firstDepreciated, vreNamespace);
                else if (first.Member is IFieldMember)
                    yield return BuildMemberInfo(from member in currentMemberVersionSet
                                                 select Tuple.Create((IFieldMember)(member.Member), ((IFieldMember)member.Member).UniqueIdentifier, member.Version), (IFieldMember)first.Member, firstDepreciated, vreNamespace);
                else if (first.Member is IEventSignatureMember)
                    yield return BuildMemberInfo(from member in currentMemberVersionSet
                                                 select Tuple.Create((IEventSignatureMember)(member.Member), ((IEventSignatureMember)member.Member).UniqueIdentifier, member.Version), (IEventSignatureMember)first.Member, firstDepreciated, vreNamespace);
                else if (first.Member is IIndexerSignatureMember)
                    yield return BuildMemberInfo(from member in currentMemberVersionSet
                                                 select Tuple.Create((IIndexerSignatureMember)(member.Member), ((IIndexerSignatureMember)member.Member).UniqueIdentifier, member.Version), (IIndexerSignatureMember)first.Member, firstDepreciated, vreNamespace);
                else if (first.Member is IMethodSignatureMember)
                    yield return BuildMemberInfo(from member in currentMemberVersionSet
                                                 select Tuple.Create((IMethodSignatureMember)(member.Member), ((IMethodSignatureMember)member.Member).UniqueIdentifier, member.Version), (IMethodSignatureMember)first.Member, firstDepreciated, vreNamespace);
                else if (first.Member is IPropertySignatureMember)
                    yield return BuildMemberInfo(from member in currentMemberVersionSet
                                                 select Tuple.Create((IPropertySignatureMember)(member.Member), ((IPropertySignatureMember)member.Member).UniqueIdentifier, member.Version), (IPropertySignatureMember)first.Member, firstDepreciated, vreNamespace);
                else if (first.Member is ITypeCoercionMember)
                    yield return BuildMemberInfo(from member in currentMemberVersionSet
                                                 select Tuple.Create((ITypeCoercionMember)(member.Member), ((ITypeCoercionMember)member.Member).UniqueIdentifier, member.Version), (ITypeCoercionMember)first.Member, firstDepreciated, vreNamespace);
                else if (first.Member is IUnaryOperatorCoercionMember)
                    yield return BuildMemberInfo(from member in currentMemberVersionSet
                                                 select Tuple.Create((IUnaryOperatorCoercionMember)(member.Member), ((IUnaryOperatorCoercionMember)member.Member).UniqueIdentifier, member.Version), (IUnaryOperatorCoercionMember)first.Member, firstDepreciated, vreNamespace);
            }
        }

        private static XElement BuildMemberInfo(
            IEnumerable<Tuple<IBinaryOperatorCoercionMember, IBinaryOperatorUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable,
            IBinaryOperatorCoercionMember first,
            CliRuntimeEnvironmentVersion firstDepreciated,
            XNamespace vreNamespace)
        {
            return new XElement(vreNamespace + "BinaryOp", BuildMemberAttributes(enumerable, first, firstDepreciated, vreNamespace), BuildMemberElements(enumerable, first, firstDepreciated, vreNamespace));
        }

        private static IEnumerable<XAttribute> BuildMemberAttributes(IEnumerable<Tuple<IBinaryOperatorCoercionMember, IBinaryOperatorUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IBinaryOperatorCoercionMember first, CliRuntimeEnvironmentVersion firstDepreciated, XNamespace vreNamespace)
        {
            yield break;
        }

        private static IEnumerable<XElement> BuildMemberElements(IEnumerable<Tuple<IBinaryOperatorCoercionMember, IBinaryOperatorUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IBinaryOperatorCoercionMember first, CliRuntimeEnvironmentVersion firstDepreciated, XNamespace vreNamespace)
        {
            yield break;
        }

        private static XElement BuildMemberInfo(
            IEnumerable<Tuple<IConstructorMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable,
            IConstructorMember first, 
            CliRuntimeEnvironmentVersion firstDepreciated,
            XNamespace vreNamespace)
        {
            var resultName = "Ctor";
            if (first.Parent is IClassType)
            {
                var cParent = (IClassType)first.Parent;
                if (cParent.TypeInitializer == first)
                    resultName = "Cctor";
            }
            else if (first.Parent is IStructType)
            {
                var sParent = (IStructType)first.Parent;
                if (sParent.TypeInitializer == first)
                    resultName = "Cctor";
            }
            return new XElement(vreNamespace + resultName, BuildMemberAttributes(enumerable, resultName, first, firstDepreciated, vreNamespace), BuildMemberElements(enumerable, resultName, first, firstDepreciated, vreNamespace));
        }

        private static IEnumerable<XAttribute> BuildMemberAttributes(IEnumerable<Tuple<IConstructorMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, string resultName, IConstructorMember first, CliRuntimeEnvironmentVersion firstDepreciated, XNamespace vreNamespace)
        {
            yield break;
        }

        private static IEnumerable<XElement> BuildMemberElements(IEnumerable<Tuple<IConstructorMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, string resultName, IConstructorMember first, CliRuntimeEnvironmentVersion firstDepreciated, XNamespace vreNamespace)
        {
            foreach (IParameterMember parameter in first.Parameters.Values)
                yield return BuildTypedName("Parameter", parameter.ParameterType, vreNamespace);
        }

        private static XElement BuildMemberInfo(
            IEnumerable<Tuple<IFieldMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable,
            IFieldMember first, 
            CliRuntimeEnvironmentVersion firstDepreciated,
            XNamespace vreNamespace)
        {
            return new XElement(vreNamespace + "Field", BuildMemberAttributes(enumerable, first, firstDepreciated, vreNamespace), BuildMemberElements(enumerable, first, firstDepreciated, vreNamespace));
        }

        private static IEnumerable<XAttribute> BuildMemberAttributes(IEnumerable<Tuple<IFieldMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IFieldMember first, CliRuntimeEnvironmentVersion firstDepreciated, XNamespace vreNamespace)
        {
            yield break;
        }

        private static IEnumerable<XElement> BuildMemberElements(IEnumerable<Tuple<IFieldMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IFieldMember first, CliRuntimeEnvironmentVersion firstDepreciated, XNamespace vreNamespace)
        {
            yield break;
        }

        private static XElement BuildMemberInfo(
            IEnumerable<Tuple<IEventSignatureMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable,
            IEventSignatureMember first, 
            CliRuntimeEnvironmentVersion firstDepreciated,
            XNamespace vreNamespace)
        {
            return new XElement(vreNamespace + "Event", BuildMemberAttributes(enumerable, first, firstDepreciated, vreNamespace), BuildMemberElements(enumerable, first, firstDepreciated, vreNamespace));
        }

        private static IEnumerable<XAttribute> BuildMemberAttributes(IEnumerable<Tuple<IEventSignatureMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IEventSignatureMember first, CliRuntimeEnvironmentVersion firstDepreciated, XNamespace vreNamespace)
        {
            yield break;
        }

        private static IEnumerable<XElement> BuildMemberElements(IEnumerable<Tuple<IEventSignatureMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IEventSignatureMember first, CliRuntimeEnvironmentVersion firstDepreciated, XNamespace vreNamespace)
        {
            yield break;
        }

        private static XElement BuildMemberInfo(
            IEnumerable<Tuple<IIndexerSignatureMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable,
            IIndexerSignatureMember first, 
            CliRuntimeEnvironmentVersion firstDepreciated,
            XNamespace vreNamespace)
        {
            return new XElement(vreNamespace + "Indexer", BuildMemberAttributes(enumerable, first, firstDepreciated, vreNamespace), BuildMemberElements(enumerable, first, firstDepreciated, vreNamespace));
        }

        private static IEnumerable<XAttribute> BuildMemberAttributes(IEnumerable<Tuple<IIndexerSignatureMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IIndexerSignatureMember first, CliRuntimeEnvironmentVersion firstDepreciated, XNamespace vreNamespace)
        {
            yield break;
        }

        private static IEnumerable<XElement> BuildMemberElements(IEnumerable<Tuple<IIndexerSignatureMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IIndexerSignatureMember first, CliRuntimeEnvironmentVersion firstDepreciated, XNamespace vreNamespace)
        {
            foreach (IParameterMember parameter in first.Parameters.Values)
                yield return BuildTypedName("Parameter", parameter.ParameterType, vreNamespace);
        }

        private static XElement BuildMemberInfo(
            IEnumerable<Tuple<IMethodSignatureMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable,
            IMethodSignatureMember first, 
            CliRuntimeEnvironmentVersion firstDepreciated,
            XNamespace vreNamespace)
        {
            return new XElement(vreNamespace + "Method", BuildMemberAttributes(enumerable, first, firstDepreciated, vreNamespace), BuildMemberElements(enumerable, first, firstDepreciated, vreNamespace));
        }

        private static IEnumerable<XAttribute> BuildMemberAttributes(IEnumerable<Tuple<IMethodSignatureMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IMethodSignatureMember first, CliRuntimeEnvironmentVersion firstDepreciated, XNamespace vreNamespace)
        {
            yield return new XAttribute("Name", first.Name);
        }

        private static IEnumerable<XElement> BuildMemberElements(IEnumerable<Tuple<IMethodSignatureMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IMethodSignatureMember first, CliRuntimeEnvironmentVersion firstDepreciated, XNamespace vreNamespace)
        {
            foreach (IParameterMember parameter in first.Parameters.Values)
                yield return BuildTypedName("Parameter", parameter.ParameterType, vreNamespace);
        }

        private static XElement BuildMemberInfo(
            IEnumerable<Tuple<IPropertySignatureMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable,
            IPropertySignatureMember first,
            CliRuntimeEnvironmentVersion firstDepreciated,
            XNamespace vreNamespace)
        {
            return new XElement(vreNamespace + "Property", BuildMemberAttributes(enumerable, first, firstDepreciated, vreNamespace), BuildMemberElements(enumerable, first, firstDepreciated, vreNamespace));
        }

        private static IEnumerable<XAttribute> BuildMemberAttributes(IEnumerable<Tuple<IPropertySignatureMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IPropertySignatureMember first, CliRuntimeEnvironmentVersion firstDepreciated, XNamespace vreNamespace)
        {
            yield break;
        }

        private static IEnumerable<XElement> BuildMemberElements(IEnumerable<Tuple<IPropertySignatureMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IPropertySignatureMember first, CliRuntimeEnvironmentVersion firstDepreciated, XNamespace vreNamespace)
        {
            yield break;
        }

        private static XElement BuildMemberInfo(
            IEnumerable<Tuple<ITypeCoercionMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable,
            ITypeCoercionMember first,
            CliRuntimeEnvironmentVersion firstDepreciated,
            XNamespace vreNamespace)
        {

            return new XElement(vreNamespace + "TypeCoercion", new XAttribute("Direction", first.Direction.ToString()), new XAttribute("Requirement", first.Requirement.ToString()), BuildTypedName(first.Direction == TypeConversionDirection.FromContainingType ? "In" : "Out", first.CoercionType, vreNamespace));
        }

        private static XElement BuildMemberInfo(
            IEnumerable<Tuple<IUnaryOperatorCoercionMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable,
            IUnaryOperatorCoercionMember first,
            CliRuntimeEnvironmentVersion firstDepreciated,
            XNamespace vreNamespace)
        {
            return new XElement(vreNamespace + "UnaryOp", BuildMemberAttributes(enumerable, first, firstDepreciated, vreNamespace), BuildMemberElements(enumerable, first, firstDepreciated, vreNamespace));
        }

        private static IEnumerable<XAttribute> BuildMemberAttributes(IEnumerable<Tuple<IUnaryOperatorCoercionMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IUnaryOperatorCoercionMember first, CliRuntimeEnvironmentVersion firstDepreciated, XNamespace vreNamespace)
        {
            yield break;
        }

        private static IEnumerable<XElement> BuildMemberElements(IEnumerable<Tuple<IUnaryOperatorCoercionMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IUnaryOperatorCoercionMember first, CliRuntimeEnvironmentVersion firstDepreciated, XNamespace vreNamespace)
        {
            yield break;
        }

        private static void EmitTypeMemberInfo(TypeVersionDetails typeInfo, int depth)
        {
            foreach (var initialMemberId in typeInfo.Members.Keys)
            {
                var currentMemberVersionSet = (from mvInfo in typeInfo.Members[initialMemberId]
                                               orderby mvInfo.Item2
                                               select new { Version = mvInfo.Item2, Id = mvInfo.Item1, Member = typeInfo.Versions[mvInfo.Item2].Members[(IGeneralMemberUniqueIdentifier)mvInfo.Item1].Entry }).ToArray();
                var first = currentMemberVersionSet.FirstOrDefault();
                var firstDepreciated = (from cmvs in currentMemberVersionSet
                                        let member = cmvs.Member
                                        where member is IMetadataEntity
                                        let metadataEntity = (IMetadataEntity)member
                                        from IMetadatum metadatum in metadataEntity.Metadata
                                        where metadatum.Type.NamespaceName == "System" && metadatum.Type.Name == "ObsoleteAttribute"
                                        select cmvs.Version).FirstOrDefault();
                if (first.Member is IMethodSignatureMember)
                    EmitMemberInfo(from member in currentMemberVersionSet
                                   select Tuple.Create((IMethodSignatureMember)(member.Member), ((IMethodSignatureMember)member.Member).UniqueIdentifier, member.Version), (IMethodSignatureMember)first.Member, firstDepreciated, depth);
                else if (first.Member is IFieldMember)
                    EmitMemberInfo(from member in currentMemberVersionSet
                                   select Tuple.Create((IFieldMember)(member.Member), ((IFieldMember)member.Member).UniqueIdentifier, member.Version), (IFieldMember)first.Member, firstDepreciated, depth);
                else if (first.Member is IBinaryOperatorCoercionMember)
                    EmitMemberInfo(from member in currentMemberVersionSet
                                   select Tuple.Create((IBinaryOperatorCoercionMember)(member.Member), ((IBinaryOperatorCoercionMember)member.Member).UniqueIdentifier, member.Version), (IBinaryOperatorCoercionMember)first.Member, firstDepreciated, depth);
                else if (first.Member is IUnaryOperatorCoercionMember)
                    EmitMemberInfo(from member in currentMemberVersionSet
                                   select Tuple.Create((IUnaryOperatorCoercionMember)(member.Member), ((IUnaryOperatorCoercionMember)member.Member).UniqueIdentifier, member.Version), (IUnaryOperatorCoercionMember)first.Member, firstDepreciated, depth);
                else if (first.Member is ITypeCoercionMember)
                    EmitMemberInfo(from member in currentMemberVersionSet
                                   select Tuple.Create((ITypeCoercionMember)(member.Member), ((ITypeCoercionMember)member.Member).UniqueIdentifier, member.Version), (ITypeCoercionMember)first.Member, firstDepreciated, depth);
                else if (first.Member is IPropertySignatureMember)
                    EmitMemberInfo(from member in currentMemberVersionSet
                                   select Tuple.Create((IPropertySignatureMember)(member.Member), ((IPropertySignatureMember)member.Member).UniqueIdentifier, member.Version), (IPropertySignatureMember)first.Member, firstDepreciated, depth);
                else if (first.Member is IConstructorMember)
                    EmitMemberInfo(from member in currentMemberVersionSet
                                   select Tuple.Create((IConstructorMember)(member.Member), ((IConstructorMember)member.Member).UniqueIdentifier, member.Version), (IConstructorMember)first.Member, firstDepreciated, depth);
                else if (first.Member is IEventSignatureMember)
                    EmitMemberInfo(from member in currentMemberVersionSet
                                   select Tuple.Create((IEventSignatureMember)(member.Member), ((IEventSignatureMember)member.Member).UniqueIdentifier, member.Version), (IEventSignatureMember)first.Member, firstDepreciated, depth);
                else if (first.Member is IIndexerSignatureMember)
                    EmitMemberInfo(from member in currentMemberVersionSet
                                   select Tuple.Create((IIndexerSignatureMember)(member.Member), ((IIndexerSignatureMember)member.Member).UniqueIdentifier, member.Version), (IIndexerSignatureMember)first.Member, firstDepreciated, depth);
            }
        }

        private static void EmitMemberInfo(IEnumerable<Tuple<ITypeCoercionMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, ITypeCoercionMember first, CliRuntimeEnvironmentVersion depreciated, int depth)
        {
            string leftBuffer = new string(' ', depth * 4);
            Console.WriteLine(@"{0}<vre:TypeCoercion Direction=""{1}"" Requirement=""{2}"">", leftBuffer, first.Direction, first.Requirement);
            var inType = first.Direction == TypeConversionDirection.FromContainingType ? (IType)first.Parent : first.CoercionType;
            var outType = first.Direction == TypeConversionDirection.ToContainingType ? (IType)first.Parent : first.CoercionType;
            if (first.Direction == TypeConversionDirection.ToContainingType)
                EmitTypedName("In", inType, depth + 1);
            if (first.Direction == TypeConversionDirection.FromContainingType)
                EmitTypedName("Out", outType, depth + 1);
            Console.WriteLine("{0}</vre:TypeCoercion>", leftBuffer);
        }

        private static XElement BuildTypedName(string tagName, IType type, XNamespace vreNamespace)
        {
            return new XElement(vreNamespace + tagName, BuildTypedNameAttributes(type), BuildTypedNameElements(type, vreNamespace));
        }
        private static IEnumerable<XElement> BuildTypedNameElements(IType type, XNamespace vreNamespace)
        {
            bool buildElementType = true;

            switch (type.ElementClassification)
            {
                case TypeElementClassification.Array:
                    if (type is IArrayType)
                    {
                        var aType = (IArrayType)type;
                        if ((aType.Lengths != null && aType.Lengths.Count > 0) || (aType.LowerBounds != null && aType.LowerBounds.Count > 0))
                        {
                            for (int dimensionIndex = 0, rankLength = Math.Min(aType.ArrayRank, Math.Max(aType.Lengths != null ? aType.Lengths.Count : 0, aType.LowerBounds != null ? aType.LowerBounds.Count : 0)); dimensionIndex < rankLength; dimensionIndex++)
                            {
                                int lowerBound = 0;
                                uint? length = null;
                                if (aType.Lengths != null && aType.Lengths.Count > dimensionIndex)
                                    length = aType.Lengths[dimensionIndex];
                                if (aType.LowerBounds != null && aType.LowerBounds.Count > dimensionIndex)
                                    lowerBound = aType.LowerBounds[dimensionIndex];
                                if (lowerBound == 0 && length == null)
                                    continue;
                                if (lowerBound != 0)
                                    if (length != null)
                                        yield return new XElement(vreNamespace + "Dimension", new XAttribute("LowerBound", lowerBound), new XAttribute("Length", length.Value));
                                    else
                                        yield return new XElement(vreNamespace + "Dimension", new XAttribute("LowerBound", lowerBound));
                                else
                                    yield return new XElement(vreNamespace + "Dimension", new XAttribute("Length", length.Value));
                            }
                        }
                    }
                    break;
                case TypeElementClassification.GenericTypeDefinition:
                    if (type.Parent != null && type.Parent is IType)
                        yield return BuildTypedName("Parent", (IType)type.Parent, vreNamespace);
                    if (type is IGenericType)
                    {
                        var gType = (IGenericType)type;
                        IEnumerable<IType> genericParams;
                        var gElementType = gType.ElementType as IGenericType;
                        if (gElementType != null)
                            genericParams = gType.GenericParameters.Skip(gType.GenericParameters.Count - gElementType.TypeParameters.Count);
                        else
                            genericParams = new IType[0];
                        foreach (var gParam in genericParams)
                            yield return BuildTypedName("GenericParameter", gParam, vreNamespace);
                    }
                    break;
                case TypeElementClassification.ModifiedType:
                    
                    break;
                default:
                case TypeElementClassification.None:
                    buildElementType = false;
                    if (type.Parent != null && type.Parent is IType && !type.IsGenericTypeParameter)
                        yield return BuildTypedName("Parent", (IType)type.Parent, vreNamespace);
                    break;
            }
            if (buildElementType)
                yield return BuildTypedName("ElementType", type.ElementType, vreNamespace);
        }
        private static IEnumerable<XAttribute> BuildTypedNameAttributes(IType type)
        {
            bool emitClassification = true;
            switch (type.ElementClassification)
            {
                case TypeElementClassification.Array:
                    if (type is IArrayType)
                    {
                        var aType = (IArrayType)type;
                        if (!aType.IsZeroBased || aType.ArrayRank != 1)
                            //Non-vector Array
                            yield return new XAttribute("Rank", aType.ArrayRank);
                    }
                    break;
                case TypeElementClassification.GenericTypeDefinition:
                    break;
                case TypeElementClassification.ModifiedType:

                    break;
                default:
                case TypeElementClassification.None:
                    string name = type.Name;
                    if (type.IsGenericTypeParameter && type is IGenericParameter)
                    {
                        var gParameter = (IGenericParameter)type;
                        if (type.Parent is IMember)
                            yield return new XAttribute("Name", string.Format("{0}!!", gParameter.Position));
                        else if (type.Parent is IType)
                            yield return new XAttribute("Name", string.Format("{0}!", gParameter.Position));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(type.NamespaceName))
                            yield return new XAttribute("Namespace", type.NamespaceName);
                        if (type.IsGenericConstruct && type is IGenericType)
                        {
                            var gType = (IGenericType)type;
                            if (gType.TypeParameters.Count > 0)
                                name = string.Format("{0}`{1}", name, gType.TypeParameters.Count);
                        }
                        yield return new XAttribute("Name", name);
                    }
                    emitClassification = false;
                    break;
                case TypeElementClassification.Nullable:

                    break;
                case TypeElementClassification.Pointer:

                    break;
                case TypeElementClassification.Reference:

                    break;
            }
            if (emitClassification)
                yield return new XAttribute("ElementClassification", type.ElementClassification.ToString());
        }

        public static void EmitTypedName(string tagName, IType target, int depth, bool newLine = true)
        {
            tagName = string.Format("vre:{0}", tagName);
            string leftBuffer = new string(' ', depth * 4);
            switch (target.ElementClassification)
            {
                case TypeElementClassification.None:
                    {
                        string body = string.Format(@"<{0}{1} Name=""{2}""/>", tagName, string.IsNullOrEmpty(target.NamespaceName) ? string.Empty : string.Format(@" Namespace=""{0}""", target.NamespaceName), target.Name);
                        if (newLine)
                            Console.WriteLine(string.Format("{0}{1}", leftBuffer, body));
                        else
                            Console.Write(body);
                    }
                    break;
                case TypeElementClassification.Array:
                    Console.Write(@"{0}<{1} ElementClassification=""Array""", leftBuffer, tagName, string.IsNullOrEmpty(target.NamespaceName) ? string.Empty : string.Format(@" Namespace=""{0}""", target.NamespaceName));

                    break;
                case TypeElementClassification.Nullable:
                case TypeElementClassification.Pointer:
                case TypeElementClassification.Reference:
                    Console.Write(@"{0}<{1} ElementClassification=""{2}"">", leftBuffer, tagName, target.ElementClassification);
                    EmitTypedName("ElementType", target.ElementType, depth, false);
                    Console.Write(@"</{0}>", tagName);
                    break;
                case TypeElementClassification.GenericTypeDefinition:
                    break;
                case TypeElementClassification.ModifiedType:
                    break;
                default:
                    break;
            }
        }

        private static void EmitMemberInfo(IEnumerable<Tuple<IIndexerSignatureMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IIndexerSignatureMember first, CliRuntimeEnvironmentVersion depreciated, int depth)
        {
        }

        private static void EmitMemberInfo(IEnumerable<Tuple<IEventSignatureMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IEventSignatureMember first, CliRuntimeEnvironmentVersion depreciated, int depth)
        {
        }

        private static void EmitMemberInfo(IEnumerable<Tuple<IConstructorMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IConstructorMember first, CliRuntimeEnvironmentVersion depreciated, int depth)
        {
        }

        private static void EmitMemberInfo(IEnumerable<Tuple<IPropertySignatureMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IPropertySignatureMember first, CliRuntimeEnvironmentVersion depreciated, int depth)
        {
        }

        private static void EmitMemberInfo(IEnumerable<Tuple<IUnaryOperatorCoercionMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IUnaryOperatorCoercionMember first, CliRuntimeEnvironmentVersion depreciated, int depth)
        {
        }

        private static void EmitMemberInfo(IEnumerable<Tuple<IBinaryOperatorCoercionMember, IBinaryOperatorUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IBinaryOperatorCoercionMember first, CliRuntimeEnvironmentVersion depreciated, int depth)
        {
        }

        private static void EmitMemberInfo(IEnumerable<Tuple<IFieldMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IFieldMember first, CliRuntimeEnvironmentVersion depreciated, int depth)
        {
        }

        private static void EmitMemberInfo(IEnumerable<Tuple<IMethodSignatureMember, IMemberUniqueIdentifier, CliRuntimeEnvironmentVersion>> enumerable, IMethodSignatureMember first, CliRuntimeEnvironmentVersion depreciated, int depth)
        {
        }

        private static IEnumerable<XElement> BuildNamespaces(Dictionary<string, TypeVersionDetails[]> namespaceAndTypeInfo, Dictionary<string, Dictionary<string, TypeVersionDetails>> typeVersions, XNamespace vreNamespace)
        {
            var nameless = namespaceAndTypeInfo.ContainsKey(string.Empty) ? namespaceAndTypeInfo[string.Empty] : null;
            if (nameless != null)
                namespaceAndTypeInfo.Remove(string.Empty);
            if (namespaceAndTypeInfo.Count > 0)
                yield return new XElement(vreNamespace + "Namespaces",
                    from @namespace in namespaceAndTypeInfo.Keys
                    select BuildNamespace(@namespace, namespaceAndTypeInfo[@namespace], typeVersions, vreNamespace));
            if (nameless != null)
                yield return BuildTypes(nameless, typeVersions, vreNamespace);
        }

        private static XElement BuildTypes(TypeVersionDetails[] types, Dictionary<string, Dictionary<string, TypeVersionDetails>> typeVersions, XNamespace vreNamespace)
        {
            return new XElement(vreNamespace + "Types",
                from type in types
                select BuildTypeInfo(type, typeVersions, vreNamespace));
        }


        private static XElement BuildNamespace(string @namespace, TypeVersionDetails[] namespaceTypes, Dictionary<string, Dictionary<string, TypeVersionDetails>> typeVersions, XNamespace vreNamespace)
        {
            return new XElement(
                vreNamespace + "Namespace",
                new XAttribute("Name", @namespace),
                BuildTypes(namespaceTypes, typeVersions, vreNamespace));
        }

        //private static void EmitNamespaceInfo(Dictionary<string, TypeVersionDetails[]> namespaceAndTypeInfo, Dictionary<string, Dictionary<string, TypeVersionDetails>> typeVersions, int depth = 0)
        //{
        //    var nameless = namespaceAndTypeInfo.ContainsKey(string.Empty) ? namespaceAndTypeInfo[string.Empty] : null;
        //    if (nameless != null)
        //        namespaceAndTypeInfo.Remove(string.Empty);
        //    if (namespaceAndTypeInfo.Count > 0)
        //    {
        //        string leftBuffer = new string(' ', depth * 4);
        //        Console.WriteLine(@"{0}<vre:Namespaces>", leftBuffer);
        //        foreach (var @namespace in namespaceAndTypeInfo.Keys)
        //        {
        //            if (@namespace == string.Empty)
        //                continue;
        //            var types = namespaceAndTypeInfo[@namespace];
        //            EmitNamespace(@namespace, types, typeVersions, depth + 1);
        //        }
        //        Console.WriteLine(@"{0}</vre:Namespaces>", leftBuffer);
        //    }
        //    if (nameless != null)
        //        EmitTypes(nameless, typeVersions, depth);
        //}

        //private static void EmitNamespace(string @namespace, TypeVersionDetails[] namespaceTypes, Dictionary<string, Dictionary<string, TypeVersionDetails>> typeVersions, int depth = 0)
        //{
        //    string leftBuffer = new string(' ', depth * 4);
        //    Console.WriteLine(@"{0}<vre:Namespace Name=""{1}"">", leftBuffer, @namespace);
        //    EmitTypes(namespaceTypes, typeVersions, depth + 1);
        //    Console.WriteLine(@"{0}</vre:Namespace>", leftBuffer);
        //}
        //private static void EmitTypes(TypeVersionDetails[] types, Dictionary<string, Dictionary<string, TypeVersionDetails>> typeVersions, int depth = 0)
        //{
        //    string leftBuffer = new string(' ', depth * 4);
        //    Console.WriteLine(@"{0}<vre:Types>", leftBuffer);
        //    foreach (var type in types)
        //        YieldTypeInfo(type, typeVersions, depth + 1);
        //    Console.WriteLine(@"{0}</vre:Types>", leftBuffer);
        //}

        private class MemberComparator :
            IEqualityComparer<IMemberUniqueIdentifier>
        {
            private IIdentityManager identityManager;

            public MemberComparator(IIdentityManager identityManager)
            {
                this.identityManager = identityManager;
            }

            private bool Compare(IGeneralGenericSignatureMemberUniqueIdentifier x, IGeneralGenericSignatureMemberUniqueIdentifier y)
            {
                if (x.TypeParameters != y.TypeParameters)
                    return false;
                return Compare((ISignatureMemberUniqueIdentifier)x, (ISignatureMemberUniqueIdentifier)y);
            }

            private bool Compare(ITypeCoercionUniqueIdentifier x, ITypeCoercionUniqueIdentifier y)
            {
                if (Object.ReferenceEquals(x, y))
                    return true;
                if (x == null ^ y == null ||
                    x.CoercionType == null ^ y.CoercionType == null)
                    return false;
                if (x.CoercionType == null)
                    return x.Direction == y.Direction && x.Requirement == y.Requirement;
                var xCoercionType = ReformGeneric(x.CoercionType, this.identityManager);
                var yCoercionType = ReformGeneric(y.CoercionType, this.identityManager);

                return x.Direction == y.Direction && x.Requirement == y.Requirement && (xCoercionType.Equals(yCoercionType));
            }

            private bool Compare(IUnaryOperatorUniqueIdentifier x, IUnaryOperatorUniqueIdentifier y)
            {
                if (x == null ^ y == null)
                    return false;
                return x.Operator == y.Operator;
            }

            private bool Compare(IBinaryOperatorUniqueIdentifier x, IBinaryOperatorUniqueIdentifier y)
            {
                if (x == null ^ y == null)
                    return false;
                if (x.ContainingSide != y.ContainingSide ||
                    x.ContainingSide != BinaryOpCoercionContainingSide.Invalid &&
                    x.OtherSide == null ^ y.OtherSide == null)
                    return false;
                if (x.OtherSide == null)
                    return true;
                if (x.ContainingSide == BinaryOpCoercionContainingSide.Both)
                    return true;
                IType xOther = ReformGeneric(x.OtherSide, this.identityManager),
                      yOther = ReformGeneric(y.OtherSide, this.identityManager);
                return xOther.Equals(yOther);
            }

            private bool Compare(ISignatureMemberUniqueIdentifier x, ISignatureMemberUniqueIdentifier y)
            {
                if (x.ParameterCount != y.ParameterCount)
                    return false;
                using (var enumX = x.Parameters.GetEnumerator())
                using (var enumY = y.Parameters.GetEnumerator())
                {
                    for (; enumX.MoveNext() | enumY.MoveNext(); )
                    {
                        var currentX = ReformGeneric(enumX.Current, this.identityManager);
                        var currentY = ReformGeneric(enumY.Current, this.identityManager);
                        if (currentX.FullName != currentY.FullName)
                            return false;
                    }
                }
                return CompareInternal(x, y);
            }

            private static IType ReformGeneric(IType t, IIdentityManager identityManager)
            {
                return StripAssembly(t, identityManager);
            }

            private static IType StripAssembly(IType t, IIdentityManager identityManager)
            {
                if (t == null)
                    return null;
                if (t is ISymbolType)
                    return t;
                if (t.Parent is IType && (!t.IsGenericTypeParameter))
                    if (t.IsGenericConstruct)
                        return IntermediateGateway.GetSymbolType(t.Name, string.Format("{0}{1}", StripAssembly(t.Parent as IType, identityManager).FullName, string.IsNullOrEmpty(t.NamespaceName) ? "+" : string.Format("+{0}", t.NamespaceName)), (from typeParam in ((IGenericType)t).GenericParameters
                                                                                                                                                                                                                                                     select StripAssembly(typeParam, identityManager)).ToCollection(), identityManager);
                    else
                        return IntermediateGateway.GetSymbolType(t.Name, string.Format("{0}{1}", StripAssembly(t.Parent as IType, identityManager).FullName, string.IsNullOrEmpty(t.NamespaceName) ? "+" : string.Format("+{0}", t.NamespaceName)), identityManager);
                else
                    switch (t.ElementClassification)
                    {
                        case TypeElementClassification.Array:
                            IArrayType arrayType = (IArrayType)t;
                            if ((arrayType.Flags & ArrayFlags.Vector) == ArrayFlags.Vector)
                                return IntermediateGateway.GetSymbolType(t.ElementType.Name, string.IsNullOrEmpty(t.ElementType.NamespaceName) ? null : t.ElementType.NamespaceName, identityManager).MakeArray();
                            else if ((arrayType.Flags & (ArrayFlags.DimensionLengths | ArrayFlags.DimensionLowerBounds)) != ArrayFlags.Vector)
                                return IntermediateGateway.GetSymbolType(t.ElementType.Name, string.IsNullOrEmpty(t.ElementType.NamespaceName) ? null : t.ElementType.NamespaceName, identityManager).MakeArray(arrayType.LowerBounds.ToArray(), arrayType.Lengths.ToArray());
                            else
                                return IntermediateGateway.GetSymbolType(t.ElementType.Name, string.IsNullOrEmpty(t.ElementType.NamespaceName) ? null : t.ElementType.NamespaceName, identityManager).MakeArray(arrayType.ArrayRank);
                        case TypeElementClassification.Nullable:
                            return StripAssembly(t.ElementType, identityManager).MakeNullable();
                        case TypeElementClassification.Pointer:
                            return StripAssembly(t.ElementType, identityManager).MakePointer();
                        case TypeElementClassification.Reference:
                            return StripAssembly(t.ElementType, identityManager).MakeByReference();
                        case TypeElementClassification.ModifiedType:
                            IModifiedType modifiedType = (IModifiedType)t;
                            return StripAssembly(t.ElementType, identityManager).MakeModified(modifiedType.Modifiers.ToArray());
                        case TypeElementClassification.GenericTypeDefinition:
                            return IntermediateGateway.GetSymbolType(t.Name, string.IsNullOrEmpty(t.NamespaceName) ? null : t.NamespaceName, (from typeParam in ((IGenericType)t).GenericParameters
                                                                                                                                              select StripAssembly(typeParam, identityManager)).ToCollection(), identityManager);
                        default:
                            return IntermediateGateway.GetSymbolType(t.Name, string.IsNullOrEmpty(t.NamespaceName) ? null : t.NamespaceName, identityManager);
                    }
            }

            private bool CompareInternal(IMemberUniqueIdentifier x, IMemberUniqueIdentifier y)
            {
                if (x == null && y != null)
                    return false;
                if (x != null && y == null)
                    return false;
                return x == y || x.Name == y.Name;
            }

            public bool Equals(IMemberUniqueIdentifier x, IMemberUniqueIdentifier y)
            {
                if (x is IGeneralGenericSignatureMemberUniqueIdentifier)
                    if (y is IGeneralGenericSignatureMemberUniqueIdentifier)
                        return Compare((IGeneralGenericSignatureMemberUniqueIdentifier)x, (IGeneralGenericSignatureMemberUniqueIdentifier)y);
                    else
                        return false;
                else if (x is ISignatureMemberUniqueIdentifier)
                    if (y is ISignatureMemberUniqueIdentifier)
                        return Compare((ISignatureMemberUniqueIdentifier)x, (ISignatureMemberUniqueIdentifier)y);
                    else
                        return false;
                else if (x is ITypeCoercionUniqueIdentifier)
                    if (y is ITypeCoercionUniqueIdentifier)
                        return this.Compare((ITypeCoercionUniqueIdentifier)x, (ITypeCoercionUniqueIdentifier)y);
                    else
                        return false;
                else if (x is IBinaryOperatorUniqueIdentifier)
                    if (y is IBinaryOperatorUniqueIdentifier)
                        return this.Compare((IBinaryOperatorUniqueIdentifier)x, (IBinaryOperatorUniqueIdentifier)y);
                    else
                        return false;
                else if (x is IUnaryOperatorUniqueIdentifier)
                    if (y is IUnaryOperatorUniqueIdentifier)
                        return this.Compare((IUnaryOperatorUniqueIdentifier)x, (IUnaryOperatorUniqueIdentifier)y);
                    else
                        return false;
                return CompareInternal(x, y);
            }


            public int GetHashCode(IMemberUniqueIdentifier obj)
            {
                /* *
                 * Special calculation needed.
                 * */
                if (obj is IGeneralGenericSignatureMemberUniqueIdentifier)
                {
                }
                if (obj is ISignatureMemberUniqueIdentifier)
                {

                }
                return obj.GetHashCode();
            }
        }

        private class TypeAndName
        {
            public IType Type { get; set; } public string Name { get; set; }
            public string Namespace { get; set; }
        }

        private static IType CreateGenericSimulant(IGenericType type, Dictionary<string, ISymbolType> symbolReplacements, IIdentityManager identityManager)
        {
            if (type.IsGenericConstruct && type.IsGenericDefinition)
                return type.MakeGenericClosure((from IGenericParameter tP in type.TypeParameters.Values
                                                select GetSymbolFor(tP.Name, symbolReplacements, identityManager)).ToArray());
            else
                return type;
        }

        private static IType GetSymbolFor(string s, Dictionary<string, ISymbolType> symbolReplacements, IIdentityManager identityManager)
        {
            ISymbolType result;
            if (!symbolReplacements.TryGetValue(s, out result))
                symbolReplacements.Add(s, result = IntermediateGateway.GetSymbolType(s, identityManager));
            return result;
        }

        private static bool IsAccessible(IMember member, IType t)
        {
            if (t is IInterfaceType ||
                t is IEnumType ||
                t is IDelegateType)
                return true;
            if (member is IScopedDeclaration)
                return IsAccessibleInternal((IScopedDeclaration)member);
            else
                return true;
        }

        private static bool IsAccessibleInternal(IScopedDeclaration decl)
        {
            return decl.AccessLevel == AccessLevelModifiers.Public ||
                   decl.AccessLevel == AccessLevelModifiers.ProtectedOrInternal ||
                   decl.AccessLevel == AccessLevelModifiers.Protected;
        }

        private static bool IsAccessible(IType t)
        {
            bool parentAccessible = true;
            if (t.Parent is IType)
            {
                parentAccessible = IsAccessible(t.Parent as IType);
            }
            else if (t.Parent == null)
                return false;
            return parentAccessible && IsAccessibleInternal(t);
        }

        private static void Process(XmlDocument xmlDoc)
        {
            using (var vrEnv = VreXmlGateway.GetVersionedEnvironment<CliRuntimeEnvironment, CliRuntimeEnvironmentVersion, ICliManager>(xmlDoc, (xmlNamespaceManager) => new CliRuntimeEnvironment(xmlDoc, xmlNamespaceManager)))
            {
                foreach (var version in vrEnv.Versions)
                {
                    foreach (var hintpath in version.HintPaths)
                    {
                        DirectoryInfo di = new DirectoryInfo(hintpath);
                        foreach (var file in di.GetFiles())
                        {
                            string fName = Path.GetFileNameWithoutExtension(file.Name);
                            if (file.Extension != null && file.Extension != ".dll" && fName.ToLower().Contains("_dll_") && (fName.ToLower().EndsWith("x86") || fName.ToLower().EndsWith("_amd64") || fName.ToLower().EndsWith("x86_ln") || fName.ToLower().EndsWith("dll_mui") || fName.ToLower().EndsWith("x86_enu") || fName.ToLower().EndsWith("a64")))
                            {
                                if (fName.StartsWith("FL_"))
                                    fName = fName.Substring(3);
                                int dllPoint = fName.ToLower().IndexOf("_dll_");
                                fName = string.Format("{0}.dll", fName.Substring(0, dllPoint));
                                try
                                {
                                    file.MoveTo(Path.Combine(di.FullName, fName));
                                }
                                catch (IOException) { }
                            }
                            else if (file.Extension != null && file.Extension.ToLower() == ".x86")
                            {
                                try
                                {
                                    file.MoveTo(Path.Combine(di.FullName, string.Format("{0}", fName)));
                                }
                                catch (IOException) { }
                            }
                            else if (file.Extension != null && (file.Extension.ToLower() == ".dll_x86" || file.Extension.ToLower() == ".dll_x86_enu"))
                            {
                                try
                                {
                                    file.MoveTo(Path.Combine(di.FullName, string.Format("{0}.dll", fName)));
                                }
                                catch (IOException) { }
                            }
                            else if (file.Extension != null && file.Extension.ToLower() == ".dll")
                            {
                                if (fName.ToLower().EndsWith("_x86"))
                                {
                                    fName = fName.Substring(0, fName.Length - 4);
                                    try
                                    {
                                        file.MoveTo(Path.Combine(di.FullName, string.Format("{0}.dll", fName)));
                                    }
                                    catch (IOException) { }
                                }
                                else if (!CliGateway.IsCliLibrary(file.FullName))
                                    try
                                    {
                                        file.MoveTo(Path.Combine(di.FullName, string.Format("______notCLILib_{0}.dll", fName)));
                                    }
                                    catch (IOException) { }

                                else if (fName.Contains("_"))
                                    try
                                    {
                                        file.MoveTo(Path.Combine(di.FullName, file.Name.Replace("_", ".")));
                                    }
                                    catch (IOException) { }
                            }
                        }
                    }
                }
            }
        }

#if false
        private static void HandleRenames(XmlDocument xmlDoc)
        {
            using (var identityManager = CliGateway.CreateIdentityManager(CliFrameworkPlatform.AnyPlatform))
            {
                var vrEnv = VreGateway.GetVersionedEnvironment(xmlDoc, identityManager);
                foreach (var version in vrEnv.Versions)
                {
                    foreach (var hintpath in version.HintPaths)
                    {
                        DirectoryInfo di = new DirectoryInfo(hintpath);
                        foreach (var file in di.GetFiles())
                        {
                            string fName = Path.GetFileNameWithoutExtension(file.Name);
                            if (file.Extension != null && file.Extension != ".dll" && fName.ToLower().Contains("_dll_") && (fName.ToLower().EndsWith("x86") || fName.ToLower().EndsWith("_amd64") || fName.ToLower().EndsWith("x86_ln") || fName.ToLower().EndsWith("dll_mui") || fName.ToLower().EndsWith("x86_enu") || fName.ToLower().EndsWith("a64")))
                            {
                                if (fName.StartsWith("FL_"))
                                    fName = fName.Substring(3);
                                int dllPoint = fName.ToLower().IndexOf("_dll_");
                                fName = string.Format("{0}.dll", fName.Substring(0, dllPoint));
                                try
                                {
                                    file.MoveTo(Path.Combine(di.FullName, fName));
                                }
                                catch (IOException) { }
                            }
                            else if (file.Extension != null && file.Extension.ToLower() == ".x86")
                            {
                                try
                                {
                                    file.MoveTo(Path.Combine(di.FullName, string.Format("{0}", fName)));
                                }
                                catch (IOException) { }
                            }
                            else if (file.Extension != null && (file.Extension.ToLower() == ".dll_x86" || file.Extension.ToLower() == ".dll_x86_enu"))
                            {
                                try
                                {
                                    file.MoveTo(Path.Combine(di.FullName, string.Format("{0}.dll", fName)));
                                }
                                catch (IOException) { }
                            }
                            else if (file.Extension != null && file.Extension.ToLower() == ".dll")
                            {
                                if (fName.ToLower().EndsWith("_x86"))
                                {
                                    fName = fName.Substring(0, fName.Length - 4);
                                    try
                                    {
                                        file.MoveTo(Path.Combine(di.FullName, string.Format("{0}.dll", fName)));
                                    }
                                    catch (IOException) { }
                                }
                                else if (!CliGateway.IsFullAssembly(file.FullName))
                                    try
                                    {
                                        file.MoveTo(Path.Combine(di.FullName, string.Format("______nfa{0}.dll", fName)));
                                    }
                                    catch (IOException) { }

                                else if (fName.Contains("_"))
                                    try
                                    {
                                        file.MoveTo(Path.Combine(di.FullName, file.Name.Replace("_", ".")));
                                    }
                                    catch (IOException) { }
                            }
                        }
                    }
                }
            }
        }
#endif
    }
}
