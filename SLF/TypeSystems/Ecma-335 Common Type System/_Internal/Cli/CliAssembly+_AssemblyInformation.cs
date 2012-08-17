using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliAssembly
    {
        private class _AssemblyInformation :
            IAssemblyInformation
        {
            private CliAssembly owner;
            private IVersion assemblyVersion;
            public _AssemblyInformation(CliAssembly owner)
            {
                this.owner = owner;
            }

            //#region IAssemblyInformation Members

            public string AssemblyName
            {
                get { return this.owner.metadataRoot.TableStream.AssemblyTable[1].Name; }
            }

            public IVersion AssemblyVersion
            {
                get {
                    if (this.assemblyVersion == null)
                        this.assemblyVersion = this.owner.metadataRoot.TableStream.AssemblyTable[1].Version.ToVersion();
                    return this.assemblyVersion;
                }
            }

            public string Company
            {
                get { throw new NotImplementedException(); }
            }

            public string Copyright
            {
                get { throw new NotImplementedException(); }
            }

            public ICultureIdentifier Culture
            {
                get { return this.owner.UniqueIdentifier.Culture; }
            }

            public string Description
            {
                get { throw new NotImplementedException(); }
            }

            public IVersion FileVersion
            {
                get { throw new NotImplementedException(); }
            }

            public string Product
            {
                get { throw new NotImplementedException(); }
            }

            public string Title
            {
                get { throw new NotImplementedException(); }
            }

            public string Trademark
            {
                get { throw new NotImplementedException(); }
            }

            //#endregion
        }

    }
}
