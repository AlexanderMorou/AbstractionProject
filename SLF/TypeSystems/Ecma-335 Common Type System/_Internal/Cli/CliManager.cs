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

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliManager :
        _ICliManager
    {
        private Dictionary<string, IAssemblyUniqueIdentifier> fileIdentifiers = new Dictionary<string, IAssemblyUniqueIdentifier>();
        private Dictionary<IAssemblyUniqueIdentifier, CompiledAssembly> loadedAssemblies = new Dictionary<IAssemblyUniqueIdentifier, CompiledAssembly>();
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

        #region ITypeIdentityManager<Type> Members

        public IType ObtainTypeReference(Type typeIdentity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ITypeIdentityManager Members

        public bool IsMetadatumInheritable(IType metadatumType)
        {
            throw new NotImplementedException();
        }

        public IType ObtainTypeReference(TypeSystemSpecialIdentity typeIdentity)
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

        #endregion

        #region IDisposable Members

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

        #endregion

        #region IAssemblyIdentityManager<Assembly,ICompiledAssembly> Members

        public ICompiledAssembly ObtainAssemblyReference(Assembly assemblyIdentity)
        {
            return this.ObtainAssemblyReference(assemblyIdentity.Location);
        }

        #endregion

        #region ICliManager Members
        public ICliRuntimeEnvironmentInfo RuntimeEnvironment
        {
            get { return this.runtimeEnvironment; }
        }

        #endregion

        #region ITypeIdentityManager<string> Members

        public IType ObtainTypeReference(string typeIdentity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IAssemblyIdentityManager<IAssemblyUniqueIdentifier,ICompiledAssembly> Members

        public ICompiledAssembly ObtainAssemblyReference(IAssemblyUniqueIdentifier assemblyIdentity)
        {
            CompiledAssembly result;
            string[] extensions = new string[] { "exe", "dll" };
            if (!this.loadedAssemblies.TryGetValue(assemblyIdentity, out result))
            {
                var baseName = assemblyIdentity.Name;
                Tuple<IAssemblyUniqueIdentifier, IStrongNamePublicKeyInfo, PEImage, CliMetadataRoot, string> validResult = null;
                foreach (var path in runtimeEnvironment.ResolutionPaths)
                {
                    foreach (var ext in extensions)
                    {
                        var check = CliCommon.CheckFileName(path.FullName, baseName, ext);
                        if (check != null)
                        {
                            if (check.Item1.Equals(assemblyIdentity))
                                validResult = check;
                            break;
                        }
                    }
                    if (validResult != null)
                        break;
                }
                if (validResult != null)
                {
                    this.loadedAssemblies.Add(validResult.Item1, new CompiledAssembly(validResult.Item5, this, validResult.Item4, validResult.Item1, validResult.Item2));
                    this.fileIdentifiers.Add(validResult.Item5, validResult.Item1);
                }
                else
                    throw new FileNotFoundException(string.Format("Assembly {0} not found.", assemblyIdentity));
            }
            return this.loadedAssemblies[assemblyIdentity];
        }


        #endregion


        #region IAssemblyIdentityManager<string,ICompiledAssembly> Members


        /// <summary>
        /// Obtains a <see cref="ICompiledAssembly"/> reference by
        /// the filename.
        /// </summary>
        /// <param name="filename">The <see cref="String"/> value
        /// which denotes the location of the assembly image.</param>
        /// <returns>A <see cref="ICompiledAssembly"/>
        /// which denotes the assembly in question.</returns>
        /// <exception cref="System.IO.FileNotFoundException">thrown when 
        /// <paramref name="filename"/> was not found.</exception>
        public ICompiledAssembly ObtainAssemblyReference(string filename)
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
                                var firstAssemblyEntry = metadataRoot.TableStream.AssemblyTable[1];
                                var pubKeyId = CliCommon.GetAssemblyUniqueIdentifier(firstAssemblyEntry);
                                IAssemblyUniqueIdentifier assemblyUniqueIdentifier = pubKeyId.Item2;
                                if (assemblyUniqueIdentifier == null)
                                    this.ThrowNoMetadataFound();
                                IStrongNamePublicKeyInfo publicKeyInfo = pubKeyId.Item1;
                                CompiledAssembly result;
                                if (this.loadedAssemblies.ContainsKey(assemblyUniqueIdentifier))
                                {
                                    peImage.Dispose();
                                    metadataRoot.Dispose();
                                    return this.loadedAssemblies[assemblyUniqueIdentifier];
                                }
                                loadedAssemblies.Add(assemblyUniqueIdentifier, result = new CompiledAssembly(filename, this, metadataRoot, assemblyUniqueIdentifier, publicKeyInfo));
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

        #endregion

        #region ITypeIdentityManager<CliMetadataTypeDefinitionTableRow> Members

        public IType ObtainTypeReference(ICliMetadataTypeDefinitionTableRow typeIdentity)
        {

            throw new NotImplementedException();
        }

        #endregion

        #region IAssemblyIdentityManager<CliMetadataAssemblyTableRow,ICompiledAssembly> Members

        public ICompiledAssembly ObtainAssemblyReference(ICliMetadataAssemblyTableRow assemblyIdentity)
        {
            var identity = CliCommon.GetAssemblyUniqueIdentifier(assemblyIdentity);
            CompiledAssembly result;
            if (!this.loadedAssemblies.TryGetValue(identity.Item2, out result))
            {
                result = new CompiledAssembly(assemblyIdentity.MetadataRoot.SourceImage.Filename, this, assemblyIdentity.MetadataRoot, identity.Item2, identity.Item1);
                this.loadedAssemblies.Add(identity.Item2, result);
            }
            return this.loadedAssemblies[identity.Item2];
        }

        #endregion

        #region ITypeIdentityManager<CliMetadataTypeRefTableRow> Members

        public IType ObtainTypeReference(ICliMetadataTypeRefTableRow typeIdentity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IAssemblyIdentityManager<CliMetadataAssemblyRefTableRow,ICompiledAssembly> Members

        public ICompiledAssembly ObtainAssemblyReference(ICliMetadataAssemblyRefTableRow assemblyIdentity)
        {
            var identity = CliCommon.GetAssemblyUniqueIdentifier(assemblyIdentity);
            CompiledAssembly result;
            if (!this.loadedAssemblies.TryGetValue(identity.Item2, out result))
                return this.ObtainAssemblyReference(identity.Item2);
            return this.loadedAssemblies[identity.Item2];
        }

        #endregion
    }
}
