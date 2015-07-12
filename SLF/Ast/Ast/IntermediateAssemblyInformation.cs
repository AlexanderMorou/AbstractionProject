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
using System.Globalization;
/*---------------------------------------------------------------------\
| Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides an intermediate assembly's meta-data.
    /// </summary>
    /// <typeparam name="TLanguage">The kind of <see cref="ILanguage"/>
    /// which is targeted by the <see cref="IntermediateAssembly{TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>.</typeparam>
    /// <typeparam name="TProvider">The kind of <see cref="ILanguageProvider"/>
    /// which provides the functional gateway to the instances of the 
    /// <see cref="IntermediateAssembly{TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>.</typeparam>
    /// <typeparam name="TAssembly">The kind of <see cref="IntermediateAssembly{TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>
    /// represented within the implementation.</typeparam>
    /// <typeparam name="TIdentityManager">The <see cref="IIdentityManager{TTypeIdentity, TAssemblyIdentity}"/>
    /// which maintains consistent type and assembly identity.</typeparam>
    /// <typeparam name="TAssemblyIdentity">The identity used to obtain assembly references.</typeparam>
    /// <typeparam name="TTypeIdentity">The identity used to denote types within the 
    /// identity manager.</typeparam>
    public sealed class IntermediateAssemblyInformation<TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity> :
        IIntermediateAssemblyInformation
        where TLanguage :
            ILanguage
        where TProvider :
            ILanguageProvider
        where TAssembly :
            IntermediateAssembly<TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity>
        where TIdentityManager :
            IIdentityManager<TTypeIdentity, TAssemblyIdentity>,
            IIntermediateIdentityManager
    {
        private static readonly IIntermediateVersion defaultVersion = new IntermediateVersion(1, 0);
        private IIntermediateVersion fileVersion;
        private IIntermediateVersion assemblyVersion;
        private TAssembly owner;
        private ICultureIdentifier cultureIdentifier;
        private string title;
        private string description;
        private string company;
        private string product;
        private string copyright;
        private string trademark;

        internal IntermediateAssemblyInformation(TAssembly owner)
        {
            this.owner = owner;
        }
        
        #region IIntermediateAssemblyInformation Members

        /// <summary>
        /// Returns/sets the name of the assembly.
        /// </summary>
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

        /// <summary>
        /// Returns/sets the title of the assembly.
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if (this.title == value)
                    return;
                var oldTitle = this.title;
                this.title = value;
                this.OnTitleChanged(oldTitle, value);
            }
        }

        /// <summary>
        /// Returns/sets the description of the assembly.
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                if (this.description == value)
                    return;
                var oldDescription = this.description;
                this.description = value;
                this.OnDescriptionChanged(oldDescription, value);
            }
        }


        /// <summary>
        /// Returns/sets the company name of the assembly.
        /// </summary>
        public string Company
        {
            get
            {
                return this.company;
            }
            set
            {
                if (this.company == value)
                    return;
                var oldCompany = this.company;
                this.company = value;
                this.OnCompanyChanged(oldCompany, value);
            }
        }

        /// <summary>
        /// Returns/sets the product name of the assembly.
        /// </summary>
        public string Product
        {
            get
            {
                return this.product;
            }
            set
            {
                if (this.product == value)
                    return;
                var oldProduct = this.product;
                this.product = value;
                this.OnProductChanged(oldProduct, value);
            }
        }

        /// <summary>
        /// Returns/sets the copyright information of the assembly.
        /// </summary>
        public string Copyright
        {
            get
            {
                return this.copyright;
            }
            set
            {
                if (this.copyright == value)
                    return;
                var oldCopyright = this.copyright;
                this.copyright = value;
                this.OnCopyrightChanged(oldCopyright, value);
            }
        }

        /// <summary>
        /// Returns/sets the trademark of the assembly.
        /// </summary>
        public string Trademark
        {
            get
            {
                return this.trademark;
            }
            set
            {
                if (this.trademark == value)
                    return;
                var oldTrademark = this.trademark;
                this.trademark = value;
                this.OnTrademarkChanged(oldTrademark, value);
            }
        }

        /// <summary>
        /// Returns/sets the culture, relative to the <see cref="CultureInfo"/>, of the assembly.
        /// </summary>
        public ICultureIdentifier Culture
        {
            get
            {
                if (this.cultureIdentifier == null)
                    return CultureIdentifiers.None;
                else
                    return this.cultureIdentifier;
            }
            set
            {
                if (this.cultureIdentifier == value)
                    return;
                this.cultureIdentifier = value;

            }
        }

        /// <summary>
        /// Returns/sets the version of the assembly file.
        /// </summary>
        public IIntermediateVersion FileVersion
        {
            get
            {
                if (this.fileVersion == null)
                    return IntermediateAssemblyInformation<TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity>.defaultVersion;
                return this.fileVersion;
            }
            set
            {
                if (fileVersion == value)
                    return;
                var original = this.fileVersion;
                try
                {
                    if (value == IntermediateAssemblyInformation<TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity>.defaultVersion)
                    {
                        this.fileVersion = null;
                        return;
                    }
                    this.fileVersion = value;
                }
                finally
                {
                    this.OnFileVersionChanged(original, this.fileVersion);
                }
            }
        }


        /// <summary>
        /// Returns/sets the version of the assembly.
        /// </summary>
        public IIntermediateVersion AssemblyVersion
        {
            get
            {
                if (this.assemblyVersion == null)
                    return IntermediateAssemblyInformation<TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity>.defaultVersion;
                return this.assemblyVersion;
            }
            set
            {
                if (this.assemblyVersion == value)
                    return;
                var original = this.assemblyVersion;
                try
                {
                    if (value == IntermediateAssemblyInformation<TLanguage, TProvider, TAssembly, TIdentityManager, TTypeIdentity, TAssemblyIdentity>.defaultVersion)
                    {
                        this.assemblyVersion = null;
                        return;
                    }
                    this.assemblyVersion = value;
                }
                finally
                {
                    this.OnAssemblyVersionChanged(original, this.assemblyVersion);
                }
            }
        }

        private void OnFileVersionChanged(IIntermediateVersion oldFileVersion, IIntermediateVersion newFileVersion)
        {
            var fileVersionChanged = this.FileVersionChanged;
            if (fileVersionChanged != null)
                fileVersionChanged(this, new EventArgsR1R2<IVersion, IVersion>(oldFileVersion, newFileVersion));
        }

        private void OnTitleChanged(string oldTitle, string newTitle)
        {
            var titleChanged = this.TitleChanged;
            if (titleChanged != null)
                titleChanged(this, new EventArgsR1R2<string, string>(oldTitle, newTitle));
        }

        private void OnDescriptionChanged(string oldDescription, string newDescription)
        {
            var descriptionChanged = this.DescriptionChanged;
            if (descriptionChanged != null)
                descriptionChanged(this, new EventArgsR1R2<string, string>(oldDescription, newDescription));
        }

        private void OnProductChanged(string oldProduct, string newProduct)
        {
            var productChanged = this.ProductChanged;
            if (productChanged != null)
                productChanged(this, new EventArgsR1R2<string, string>(oldProduct, newProduct));
        }

        private void OnCompanyChanged(string oldCompany, string newCompany)
        {
            var companyChanged = this.CompanyChanged;
            if (companyChanged != null)
                companyChanged(this, new EventArgsR1R2<string, string>(oldCompany, newCompany));
        }

        private void OnCopyrightChanged(string oldCopyright, string newCopyright)
        {
            var copyrightChanged = this.CopyrightChanged;
            if (copyrightChanged != null)
                copyrightChanged(this, new EventArgsR1R2<string, string>(oldCopyright, newCopyright));
        }

        private void OnTrademarkChanged(string oldTrademark, string newTrademark)
        {
            var trademarkChanged = this.TrademarkChanged;
            if (trademarkChanged != null)
                trademarkChanged(this, new EventArgsR1R2<string, string>(oldTrademark, newTrademark));
        }

        private void OnAssemblyVersionChanged(IVersion oldVersion, IVersion newVersion)
        {
            var assemblyVersionChanged = this.AssemblyVersionChanged;
            if (assemblyVersionChanged != null)
                assemblyVersionChanged(this, new EventArgsR1R2<IVersion,IVersion>(oldVersion, newVersion));
        }

        public void ReadyAssemblyMetaData(bool full = true)
        {
            var fileVersion = full ? this.FileVersion : this.fileVersion;
            var assemblyVersion = full ? this.AssemblyVersion : this.assemblyVersion;
            List<MetadatumDefinitionParameterValueCollection> toAdd = null;
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

        private static void StandardAttributeCheck<T>(TAssembly owner, Type attributeType, T value, ref List<MetadatumDefinitionParameterValueCollection> toAdd)
            where T :
                class
        {
            if (value != null)
            {
                IType attributeTypeInternal =  owner.IdentityManager.ObtainTypeReference(attributeType);
                if (owner._CustomAttributes.Contains(attributeTypeInternal))
                {
                    var attributeDecl = owner._CustomAttributes[attributeTypeInternal];
                    if (attributeDecl.Parameters.Count != 1 || !(attributeDecl.Parameters[0] is IMetadatumDefinitionParameter<string>) || attributeDecl.Parameters[0] is IMetadatumDefinitionNamedParameter)
                    {
                        attributeDecl.Parameters.Clear();
                        attributeDecl.Parameters.Add(value.ToString());
                    }
                    else
                        ((IMetadatumDefinitionParameter<string>)(attributeDecl.Parameters[0])).Value = value.ToString();
                }
                else
                {
                    if (toAdd == null)
                        toAdd = new List<MetadatumDefinitionParameterValueCollection>();
                    toAdd.Add(new MetadatumDefinitionParameterValueCollection(attributeTypeInternal) { { value.ToString() } });
                }
            }
        }

        private Dictionary<EventHandler<EventArgsR1R2<string, string>>, EventHandler<DeclarationNameChangedEventArgs>> nameChangedLookup = new Dictionary<EventHandler<EventArgsR1R2<string,string>>,EventHandler<DeclarationNameChangedEventArgs>>();

        public event EventHandler<EventArgsR1R2<string, string>> AssemblyNameChanged
        {
            add
            {
                lock (nameChangedLookup)
                {
                    if (this.owner == null)
                        return;
                    if (!nameChangedLookup.ContainsKey(value))
                    {
                        EventHandler<DeclarationNameChangedEventArgs> handler = (sender, args) =>
                            value(sender, new EventArgsR1R2<string, string>(args.OldName, args.NewName));
                        this.owner.Renamed += handler;
                        nameChangedLookup.Add(value, handler);
                    }
                }
            }
            remove
            {
                lock (nameChangedLookup)
                {
                    if (this.owner == null)
                        return;
                    if (nameChangedLookup.ContainsKey(value))
                    {
                        var handler = nameChangedLookup[value];
                        nameChangedLookup.Remove(value);
                        this.owner.Renamed -= handler;
                    }
                }
            }
        }

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
        public event EventHandler<EventArgsR1R2<IVersion, IVersion>> FileVersionChanged;

        /// <summary>
        /// Occurs when the assembly version changes.
        /// </summary>
        public event EventHandler<EventArgsR1R2<IVersion, IVersion>> AssemblyVersionChanged;

        #endregion

        #region IAssemblyInformation Members

        IVersion IAssemblyInformation.FileVersion
        {
            get { return this.FileVersion; }
        }

        IVersion IAssemblyInformation.AssemblyVersion
        {
            get { return this.AssemblyVersion; }
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
                if (this.cultureIdentifier != null)
                    this.cultureIdentifier = null;
            }
            finally
            {
                GC.SuppressFinalize(this);
            }
        }


    }
}
