using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Globalization;
using AllenCopeland.Abstraction.Utilities.Events;
using AllenCopeland.Abstraction.Slf.Languages;
/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public sealed class IntermediateAssemblyInformation<TLanguage, TProvider, TAssembly> :
        IIntermediateAssemblyInformation
        where TLanguage :
            ILanguage
        where TProvider :
            ILanguageProvider
        where TAssembly :
            IntermediateAssembly<TLanguage, TProvider, TAssembly>
    {
        private static readonly Version defaultVersion = new Version(1, 0, 0, 0);
        private Version fileVersion;
        private Version assemblyVersion;
        private TAssembly owner;
        private ICultureIdentifier identifier;
        internal IntermediateAssemblyInformation(TAssembly owner)
        {
            this.owner = owner;
        }
        
        #region IIntermediateAssemblyInformation Members

        public string AssemblyName
        {
            get
            {
                return owner.Name;
            }
            set
            {
                owner.Name = value;
            }
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Company { get; set; }

        public string Product { get; set; }

        public string Copyright { get; set; }

        public string Trademark { get; set; }

        public ICultureIdentifier Culture
        {
            get
            {
                if (this.identifier == null)
                    return CultureIdentifiers.None;
                else
                    return this.identifier;
            }
            set
            {
                if (this.identifier == value)
                    return;
                this.identifier = value;

            }
        }

        public Version FileVersion
        {
            get
            {
                if (this.fileVersion == null)
                    return IntermediateAssemblyInformation<TLanguage, TProvider, TAssembly>.defaultVersion;
                return this.fileVersion;
            }
            set
            {
                if (fileVersion == value)
                    return;
                if (value == IntermediateAssemblyInformation<TLanguage, TProvider, TAssembly>.defaultVersion)
                {
                    this.fileVersion = null;
                    return;
                }
                this.fileVersion = value;
            }
        }

        public Version AssemblyVersion
        {
            get
            {
                if (this.assemblyVersion == null)
                    return IntermediateAssemblyInformation<TLanguage, TProvider, TAssembly>.defaultVersion;
                return this.assemblyVersion;
            }
            set
            {
                if (this.assemblyVersion == value) 
                    return;
                if (Object.ReferenceEquals(value, IntermediateAssemblyInformation<TLanguage, TProvider, TAssembly>.defaultVersion))
                {
                    this.assemblyVersion = null;
                    return;
                }
                this.assemblyVersion = value;
            }
        }

        public void ReadyAssemblyMetaData(bool full = true)
        {
            var fileVersion = full ? this.FileVersion : this.fileVersion;
            var assemblyVersion = full ? this.AssemblyVersion : this.assemblyVersion;
            List<CustomAttributeDefinition.ParameterValueCollection> toAdd = null;
            StandardAttributeCheck(this.owner, typeof(AssemblyFileVersionAttribute), fileVersion, ref toAdd);
            StandardAttributeCheck(this.owner, typeof(AssemblyVersionAttribute), assemblyVersion, ref toAdd);
            StandardAttributeCheck(this.owner, typeof(AssemblyTitleAttribute), this.Title, ref toAdd);
            StandardAttributeCheck(this.owner, typeof(AssemblyDescriptionAttribute), this.Description, ref toAdd);
            StandardAttributeCheck(this.owner, typeof(AssemblyCompanyAttribute), this.Company, ref toAdd);
            StandardAttributeCheck(this.owner, typeof(AssemblyProductAttribute), this.Product, ref toAdd);
            StandardAttributeCheck(this.owner, typeof(AssemblyCopyrightAttribute), this.Copyright, ref toAdd);
            StandardAttributeCheck(this.owner, typeof(AssemblyTrademarkAttribute), this.Trademark, ref toAdd);
            if (toAdd != null)
            {
                //Add the ones which need added, as a series, to reduce the overall number of
                //definition collections.
                this.owner._CustomAttributes.Add(toAdd.ToArray());
            }
        }

        private static void StandardAttributeCheck<T>(TAssembly owner, Type attributeType, T value, ref List<CustomAttributeDefinition.ParameterValueCollection> toAdd)
            where T :
                class
        {
            if (value != null)
            {
                IType attributeTypeInternal = attributeType.GetTypeReference();
                if (owner._CustomAttributes.Contains(attributeTypeInternal))
                {
                    var attributeDecl = owner._CustomAttributes[attributeTypeInternal];
                    if (attributeDecl.Parameters.Count != 1 || !(attributeDecl.Parameters[0] is ICustomAttributeDefinitionParameter<string>) || attributeDecl.Parameters[0] is ICustomAttributeDefinitionNamedParameter)
                    {
                        attributeDecl.Parameters.Clear();
                        attributeDecl.Parameters.Add(value.ToString());
                    }
                    else
                        ((ICustomAttributeDefinitionParameter<string>)(attributeDecl.Parameters[0])).Value = value.ToString();
                }
                else
                {
                    if (toAdd == null)
                        toAdd = new List<CustomAttributeDefinition.ParameterValueCollection>();
                    toAdd.Add(new CustomAttributeDefinition.ParameterValueCollection(attributeTypeInternal) { { value.ToString() } });
                }
            }
        }
        #endregion

        public void Dispose()
        {
            try
            {
                this.owner = null;
                if (this.fileVersion != null)
                    this.fileVersion = null;
                if (this.assemblyVersion != null)
                    this.assemblyVersion = null;
                if (this.identifier != null)
                    this.identifier = null;
            }
            finally
            {
                GC.SuppressFinalize(this);
            }
        }

        #region IIntermediateAssemblyInformation Members

        /// <summary>
        /// Occurs when the title of the assembly changes.
        /// </summary>
        public event EventHandler<EventArgsR1R2<string, string>> TitleChanged;

        /// <summary>
        /// Occurs when the description of the assembly changes.
        /// </summary>
        public event EventHandler<EventArgsR1R2<string, string>> DescriptionChanged;

        /// <summary>
        /// Returns/sets the company name of the assembly.
        /// </summary>
        public event EventHandler<EventArgsR1R2<string, string>> CompanyChanged;

        /// <summary>
        /// Returns/sets the product name of the assembly.
        /// </summary>
        public event EventHandler<EventArgsR1R2<string, string>> ProductChanged;

        /// <summary>
        /// Occurs when the copyright of the assembly changes.
        /// </summary>
        public event EventHandler<EventArgsR1R2<string, string>> CopyrightChanged;

        /// <summary>
        /// Occurs when the trademark of the assembly changes.
        /// </summary>
        public event EventHandler<EventArgsR1R2<string, string>> TrademarkChanged;

        /// <summary>
        /// Occurs when the culture of the assembly changes.
        /// </summary>
        public event EventHandler<EventArgsR1R2<ICultureIdentifier, ICultureIdentifier>> CultureChanged;

        /// <summary>
        /// Occurs when the file version of the assembly changes.
        /// </summary>
        public event EventHandler<EventArgsR1R2<Version, Version>> FileVersionChanged;

        /// <summary>
        /// Occurs when the assembly version changes.
        /// </summary>
        public event EventHandler<EventArgsR1R2<Version, Version>> AssemblyVersionChanged;

        #endregion
    }
}
