using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Documentation;
using System.Reflection;
using System.IO;
using AllenCopeland.Abstraction.Slf.Platforms.WindowsNT;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using System.Runtime.InteropServices;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System.Security.Cryptography;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using System.Diagnostics;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Modules;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliManager :
        _ICliManager
    {
        private Dictionary<string, IAssemblyUniqueIdentifier> fileIdentifiers = new Dictionary<string, IAssemblyUniqueIdentifier>();

        private Dictionary<IAssemblyUniqueIdentifier, CliAssembly> loadedAssemblies = new Dictionary<IAssemblyUniqueIdentifier, CliAssembly>();
        private Dictionary<string, Tuple<PEImage, CliMetadataRoot, string>> loadedModules = new Dictionary<string, Tuple<PEImage, CliMetadataRoot, string>>();
        private MultikeyedDictionary<CliAssembly, string, CliModule> moduleAssemblyIdentities = new MultikeyedDictionary<CliAssembly, string, CliModule>();
        //"System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"

        private ICliRuntimeEnvironmentInfo runtimeEnvironment;
        /// <summary>
        /// Creates a new <see cref="CliManager"/> with the 
        /// <paramref name="runtimeEnvironment"/> specified.
        /// </summary>
        /// <param name="runtimeEnvironment">The <see cref="ICliRuntimeEnvironmentInfo"/>
        /// which the <see cref="CliManager"/> targets.</param>
        public CliManager(ICliRuntimeEnvironmentInfo runtimeEnvironment)
        {
            this.runtimeEnvironment = runtimeEnvironment;
        }

        //#region ITypeIdentityManager<Type> Members

        public IType ObtainTypeReference(Type typeIdentity)
        {
            throw new NotImplementedException();
        }

        //#endregion

        //#region ITypeIdentityManager Members

        public bool IsMetadatumInheritable(IType metadatumType)
        {
            throw new NotImplementedException();
        }

        public IType ObtainTypeReference(object typeIdentity)
        {
            if (typeIdentity is string)
                return ObtainTypeReference((string) typeIdentity);
            else if (typeIdentity is Type)
                return ObtainTypeReference((Type) typeIdentity);
            else if (typeIdentity is PrimitiveType)
                return ObtainTypeReference((PrimitiveType) typeIdentity);
            throw new ArgumentOutOfRangeException("typeIdentity");
        }

        public IType ObtainTypeReference(PrimitiveType typeIdentity)
        {
            throw new NotImplementedException();
        }

        //#endregion

        //#region IDisposable Members

        public void Dispose()
        {
            lock (this.loadedAssemblies)
            {
                foreach (var assembly in this.loadedAssemblies.Values)
                    assembly.Dispose();
                this.loadedAssemblies.Clear();
                this.fileIdentifiers.Clear();
            }
        }

        //#endregion

        //#region IAssemblyIdentityManager<Assembly,ICompiledAssembly> Members

        public ICliAssembly ObtainAssemblyReference(Assembly assemblyIdentity)
        {
            return this.ObtainAssemblyReference(assemblyIdentity.Location);
        }

        //#endregion

        //#region ICliManager Members
        public ICliRuntimeEnvironmentInfo RuntimeEnvironment
        {
            get { return this.runtimeEnvironment; }
        }


        public ICliMetadataModuleTableRow LoadModule(ICliMetadataModuleReferenceTableRow metadata)
        {
            if (metadata.MetadataRoot == null)
                throw new ArgumentException("metadata must contain proper root.");
            ICliMetadataFileTable fileTable;
            var relativeAssembly = GetRelativeAssembly(metadata.MetadataRoot);
            if (relativeAssembly == null)
                throw new ArgumentException("No assembly loaded for metadata.", "metadata");
            if ((fileTable = metadata.MetadataRoot.TableStream.FileTable) != null)
            {
                fileTable.Read();
                ICliMetadataFileTableRow toCheck = null;
                foreach (var file in fileTable)
                {
                    /* *
                     * Valid metadata.
                     * */
                    if (file.NameIndex == metadata.NameIndex &&
                        file.Flags == CliMetadataFileAttributes.ContainsMetadata)
                    {
                        toCheck = file;
                        break;
                    }
                }
                if (toCheck == null)
                    throw new BadImageFormatException("There is no file entry in the metadata for the module.");
                Tuple<PEImage, CliMetadataRoot, string> valid = null;

                foreach (var path in (from dirInfo in this.RuntimeEnvironment.ResolutionPaths
                                      select dirInfo.FullName).Concat<string>(new string[] { Path.GetDirectoryName(relativeAssembly.Location) }).Distinct().ToArray())
                {
                    var currentFilename = string.Format("{0}{1}{2}", path, Path.DirectorySeparatorChar, toCheck.Name);
                    if (!File.Exists(currentFilename))
                        continue;
                    if (this.loadedModules.ContainsKey(path))
                    {
                        CliModule result;
                        if (this.moduleAssemblyIdentities.TryGetValue(relativeAssembly, currentFilename, out result))
                            return result.Metadata;
                    }
                    var current = CliCommon.CheckFilename(currentFilename, false);
                    if (current == null)
                        continue;
                    else
                    {
                        valid = Tuple.Create(current.Item3, current.Item4, current.Item6);
                        break;
                    }
                }
                if (valid == null)
                    throw new FileNotFoundException("The file associated to the module to load cannot be found.", toCheck.Name);
                if (valid.Item2.TableStream.ModuleTable == null ||
                    valid.Item2.TableStream.ModuleTable.Count == 0)
                    throw new BadImageFormatException("No module module table present in the metadata.");
                this.loadedModules.Add(valid.Item3, valid);
                {
                    CliModule result;
                    return valid.Item2.TableStream.ModuleTable[1];
                    //this.moduleAssemblyIdentities.Add(relativeAssembly, valid.Item3, result = new CliModule(relativeAssembly, valid.Item2.TableStream.ModuleTable[1]));
                    //return result.Metadata;
                }

            }
            throw new NotImplementedException();
        }

        private CliAssembly GetRelativeAssembly(CliMetadataRoot root)
        {
            foreach (var assembly in this.loadedAssemblies.Values)
                if (assembly.MetadataRoot == root)
                    return assembly;
            return null;
        }

        //#endregion

        //#region ITypeIdentityManager<string> Members

        public IType ObtainTypeReference(string typeIdentity)
        {
            throw new NotImplementedException();
        }

        //#endregion

        //#region IAssemblyIdentityManager<IAssemblyUniqueIdentifier,ICompiledAssembly> Members

        public ICliAssembly ObtainAssemblyReference(IAssemblyUniqueIdentifier assemblyIdentity)
        {
            CliAssembly result;
            string[] extensions = new string[] { "exe", "dll" };
            bool gacAssembly = false;
            if (!this.loadedAssemblies.TryGetValue(assemblyIdentity, out result))
            {
                var baseName = assemblyIdentity.Name;
                Tuple<IAssemblyUniqueIdentifier, IStrongNamePublicKeyInfo, PEImage, CliMetadataRoot, ICliMetadataAssemblyTableRow, string> validResult = null;
                /* *
                 * Resolution method:
                 * Runtime folder, GAC Cache
                 * */
                foreach (var path in runtimeEnvironment.ResolutionPaths)
                {
                    var check = CliCommon.CheckFilename(path.FullName, baseName, "exe");
                    if (check != null && check.Item1.Equals(assemblyIdentity))
                    {
                        validResult = check;
                        break;
                    }
                    if (validResult != null)
                        break;
                    check = CliCommon.CheckFilename(path.FullName, baseName, "dll");
                    if (check != null && check.Item1.Equals(assemblyIdentity))
                    {
                        validResult = check;
                        break;
                    }
                    if (validResult != null)
                        break;
                }
                if (validResult == null)
                {
                    foreach (var path in runtimeEnvironment.GacLocationFor(assemblyIdentity))
                    {
                        var check = CliCommon.CheckFilename(path.FullName, baseName, "exe");
                        if (check != null && check.Item1.Equals(assemblyIdentity))
                            {
                                validResult = check;
                                gacAssembly = true;
                                break;
                            }
                        if (validResult != null)
                            break;
                        check = CliCommon.CheckFilename(path.FullName, baseName, "dll");
                        if (check != null && check.Item1.Equals(assemblyIdentity))
                        {
                            validResult = check;
                            gacAssembly = true;
                            break;
                        }
                        if (validResult != null)
                            break;
                    }
                }
                if (validResult != null)
                {
                    this.loadedAssemblies.Add(validResult.Item1, result = new CliAssembly(validResult.Item6, this, validResult.Item5, validResult.Item1, validResult.Item2));
                    if (gacAssembly)
                        result.IsFromGlobalAssemblyCache = true;
                    result.InitializeCommon();
                    this.fileIdentifiers.Add(validResult.Item6, validResult.Item1);
                }
                else
                    throw new FileNotFoundException(string.Format("Assembly {0} not found.", assemblyIdentity));
            }
            return this.loadedAssemblies[assemblyIdentity];
        }


        //#endregion


        //#region IAssemblyIdentityManager<string,ICompiledAssembly> Members


        /// <summary>
        /// Obtains a <see cref="ICliAssembly"/> reference by
        /// the filename.
        /// </summary>
        /// <param name="filename">The <see cref="String"/> value
        /// which denotes the location of the assembly image.</param>
        /// <returns>A <see cref="ICliAssembly"/>
        /// which denotes the assembly in question.</returns>
        /// <exception cref="System.IO.FileNotFoundException">thrown when 
        /// <paramref name="filename"/> was not found.</exception>
        public ICliAssembly ObtainAssemblyReference(string filename)
        {
            filename = CliCommon.MinimizeFilename(filename);
            IAssemblyUniqueIdentifier uniqueIdentifier;
            if (!this.fileIdentifiers.TryGetValue(filename, out uniqueIdentifier))
            {
                if (File.Exists(filename))
                {
                    var peAndMetadata = CliCommon.LoadAssemblyMetadata(filename);
                    var peImage = peAndMetadata.Item1;
                    var metadataRoot = peAndMetadata.Item2;
                    using (peImage)
                    {
                        var imageKind = peImage.OptionalHeader.ImageKind;
                        bool supportedOnPlatform = false;
                        if (this.runtimeEnvironment.Platform == FrameworkPlatform.x64Platform)
                        {
                            /* *
                             * x64 is backwards compatible with x86, therefore
                             * if it doesn't require a 32 bit mode, it should be valid, 
                             * since it targets the Any runtimeEnvironment, but the PE Header
                             * is PE32.
                             * */
                            CliHeader header;
                            if (imageKind == PEImageKind.x86Image &&
                                ((((header = metadataRoot.Header).Flags & CliRuntimeFlags.IntermediateLanguageOnly) == CliRuntimeFlags.IntermediateLanguageOnly) &&
                                  ((header.Flags & CliRuntimeFlags.Requires32BitProcess) != CliRuntimeFlags.Requires32BitProcess)))
                                supportedOnPlatform = true;
                            /* *
                             * In this case regardless of whether they have IL only 
                             * */
                            else if (imageKind == PEImageKind.x64Image)
                                supportedOnPlatform = true;
                        }
                        else if (this.runtimeEnvironment.Platform == FrameworkPlatform.x86Platform)
                        {
                            /* *
                             * ... however, the reverse is not true.
                             * */
                            if (imageKind == PEImageKind.x64Image)
                                supportedOnPlatform = false;
                            else if (imageKind == PEImageKind.x86Image)
                                supportedOnPlatform = true;
                        }
                        if (supportedOnPlatform)
                        {
                            if (metadataRoot.TableStream.ContainsKey(CliMetadataTableKinds.Assembly))
                            {
                                var pubKeyId = CliCommon.GetAssemblyUniqueIdentifier(metadataRoot.TableStream.AssemblyTable[1]);
                                var firstAssemblyRow = pubKeyId.Item3;
                                IAssemblyUniqueIdentifier assemblyUniqueIdentifier = pubKeyId.Item2;
                                if (assemblyUniqueIdentifier == null)
                                    this.ThrowNoMetadataFound();
                                IStrongNamePublicKeyInfo publicKeyInfo = pubKeyId.Item1;
                                CliAssembly result;
                                if (this.loadedAssemblies.ContainsKey(assemblyUniqueIdentifier))
                                {
                                    peImage.Dispose();
                                    metadataRoot.Dispose();
                                    return this.loadedAssemblies[assemblyUniqueIdentifier];
                                }
                                loadedAssemblies.Add(assemblyUniqueIdentifier, result = new CliAssembly(filename, this, firstAssemblyRow, assemblyUniqueIdentifier, publicKeyInfo));
                                result.InitializeCommon();
                                this.fileIdentifiers.Add(filename, assemblyUniqueIdentifier);
                                return result;
                            }
                        }
                        else
                            throw new BadImageFormatException(string.Format("Expecting {0} image, but got {1}.", this.runtimeEnvironment.Platform, imageKind));
                    }
                }
                else
                    throw new FileNotFoundException("AssemblyIdentity not found.", filename);
            }
            return this.loadedAssemblies[this.fileIdentifiers[filename]];
        }

        private void ThrowNoMetadataFound()
        {
            throw new BadImageFormatException("No Assembly metadata entry found.");
        }

        //#endregion

        //#region ITypeIdentityManager<CliMetadataTypeDefinitionTableRow> Members

        public IType ObtainTypeReference(ICliMetadataTypeDefinitionTableRow typeIdentity)
        {

            throw new NotImplementedException();
        }

        //#endregion

        //#region IAssemblyIdentityManager<CliMetadataAssemblyTableRow,ICompiledAssembly> Members

        public ICliAssembly ObtainAssemblyReference(ICliMetadataAssemblyTableRow assemblyIdentity)
        {
            var identity = CliCommon.GetAssemblyUniqueIdentifier(assemblyIdentity);
            CliAssembly result;
            if (!this.loadedAssemblies.TryGetValue(identity.Item2, out result))
            {
                result = new CliAssembly(assemblyIdentity.MetadataRoot.SourceImage.Filename, this, assemblyIdentity, identity.Item2, identity.Item1);
                result.InitializeCommon();
                this.loadedAssemblies.Add(identity.Item2, result);
            }
            return this.loadedAssemblies[identity.Item2];
        }

        //#endregion

        //#region ITypeIdentityManager<CliMetadataTypeRefTableRow> Members

        public IType ObtainTypeReference(ICliMetadataTypeRefTableRow typeIdentity)
        {
            throw new NotImplementedException();
        }

        //#endregion

        //#region IAssemblyIdentityManager<CliMetadataAssemblyRefTableRow,ICompiledAssembly> Members

        public ICliAssembly ObtainAssemblyReference(ICliMetadataAssemblyRefTableRow assemblyIdentity)
        {
            CliAssembly result;
            var identity = CliCommon.GetAssemblyUniqueIdentifier(assemblyIdentity);
            if (!this.loadedAssemblies.TryGetValue(identity.Item2, out result))
            {
                var relativeAssembly = GetRelativeAssembly(assemblyIdentity.MetadataRoot);
                string relativePath = Path.GetDirectoryName(relativeAssembly.Location);
                var baseName = identity.Item2.Name;
                Tuple<IAssemblyUniqueIdentifier, IStrongNamePublicKeyInfo, PEImage, CliMetadataRoot, ICliMetadataAssemblyTableRow, string> validResult = null;
                var check = CliCommon.CheckFilename(relativePath, baseName, "exe");
                if (check != null && check.Item1.Equals(assemblyIdentity))
                    validResult = check;
                else
                {
                    check = CliCommon.CheckFilename(relativePath, baseName, "dll");
                    if (check != null && check.Item1.Equals(assemblyIdentity))
                        validResult = check;
                }
                if (validResult != null)
                {
                    this.loadedAssemblies.Add(validResult.Item1, result = new CliAssembly(validResult.Item6, this, validResult.Item5, validResult.Item1, validResult.Item2));
                    result.InitializeCommon();
                    this.fileIdentifiers.Add(validResult.Item6, validResult.Item1);
                }
                else
                    return this.ObtainAssemblyReference(identity.Item2);
            }
            return this.loadedAssemblies[identity.Item2];
        }

        //#endregion

        #region ITypeIdentityManager Members

        IStandardRuntimeEnvironmentInfo ITypeIdentityManager.RuntimeEnvironment
        {
            get { return this.runtimeEnvironment; }
        }

        #endregion

    }
}
