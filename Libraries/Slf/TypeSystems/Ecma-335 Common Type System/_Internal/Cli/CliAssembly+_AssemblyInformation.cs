using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using System.Reflection;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliAssembly
    {
        private class _AssemblyInformation :
            IAssemblyInformation
        {
            private CliAssembly owner;
            private IVersion assemblyVersion;
            private string company;
            private string copyright;
            private string description;
            private string product;
            private string title;
            private string trademark;
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
                get
                {
                    return this.company ?? (this.company = ReadBoilerplateAttribute(TypeSystemIdentifiers.GetTypeIdentifier("System.Reflection", "AssemblyCompanyAttribute", 0)));
                }
            }

            private string ReadBoilerplateAttribute(IGeneralTypeUniqueIdentifier attributeTypeIdentifier)
            {
                var attributeType = owner.IdentityManager.ObtainTypeReference(attributeTypeIdentifier, this.owner);
                if (owner.IsDefined(attributeType))
                {
                    var attributeDef = owner.Metadata[attributeType];
                    var firstCtorVal = attributeDef.Parameters.FirstOrDefault();
                    var stringTypeRef = owner.IdentityManager.ObtainTypeReference(RuntimeCoreType.String, owner);
                    if (firstCtorVal.ParameterType == stringTypeRef)
                        return firstCtorVal.Value as string;
                }
                return null;
            }

            public string Copyright
            {
                get
                {
                    return this.copyright ?? (this.copyright = ReadBoilerplateAttribute(TypeSystemIdentifiers.GetTypeIdentifier("System.Reflection", "AssemblyCopyrightAttribute", 0)));
                }
            }

            public ICultureIdentifier Culture
            {
                get { return this.owner.UniqueIdentifier.Culture; }
            }

            public string Description
            {
                get
                {
                    return this.description ?? (this.description = ReadBoilerplateAttribute(TypeSystemIdentifiers.GetTypeIdentifier("System.Reflection", "AssemblyDescriptionAttribute", 0)));
                }
            }

            public IVersion FileVersion
            {
                get { throw new NotImplementedException(); }
            }

            public string Product
            {
                get
                {
                    return this.product ?? (this.product = ReadBoilerplateAttribute(TypeSystemIdentifiers.GetTypeIdentifier("System.Reflection", "AssemblyProductAttribute", 0)));
                }
            }

            public string Title
            {
                get
                {
                    return this.title ?? (this.title = ReadBoilerplateAttribute(TypeSystemIdentifiers.GetTypeIdentifier("System.Reflection", "AssemblyTitleAttribute", 0)));
                }
            }

            public string Trademark
            {
                get
                {
                    return this.trademark ?? (this.trademark = ReadBoilerplateAttribute(TypeSystemIdentifiers.GetTypeIdentifier("System.Reflection", "AssemblyTrademarkAttribute", 0)));
                }
            }

            //#endregion
        }

    }
}
