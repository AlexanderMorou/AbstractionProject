 /* ----------------------------------------------------------\
 |  This code was generated by Allen Copeland's Abstraction.  |
 |  Version: 0.5.0.0                                          |
 |------------------------------------------------------------|
 |  To ensure the code works properly,                        |
 |  please do not make any changes to the file.               |
 |------------------------------------------------------------|
 |  The specific language is C♯                               |
 |  Sub-tool Name: C♯ Code Translator                         |
 |  Sub-tool Version: 1.0.0.0                                 |
 \---------------------------------------------------------- */
using AllenCopeland.Abstraction.Slf.Ast.Cli.Metadata;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections.ObjectModel;
namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Metadata.Tables
{
    /// <summary>
    /// Provides a mutable row class for a mutable table which defines the information about
    /// the types within the image's metadata.
    /// </summary>
    class CliMetadataTypeDefinitionMutableTableRow
    {
        private ICliMetadataMutableRoot metadataRoot;
        private Collection<ICliMetadataEventMutableTableRow> events;
        private CovariantReadOnlyCollection<AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataEventTableRow, ICliMetadataEventMutableTableRow> _events;
        private Collection<ICliMetadataPropertyMutableTableRow> properties;
        private CovariantReadOnlyCollection<AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataPropertyTableRow, ICliMetadataPropertyMutableTableRow> _properties;
        private Collection<ICliMetadataFieldMutableTableRow> fields;
        private CovariantReadOnlyCollection<AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataFieldTableRow, ICliMetadataFieldMutableTableRow> _fields;
        private Collection<ICliMetadataMethodDefinitionMutableTableRow> methods;
        private CovariantReadOnlyCollection<AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataMethodDefinitionTableRow, ICliMetadataMethodDefinitionMutableTableRow> _methods;
        /// <summary>
        /// Returns the root of the metadata from which the current <see cref="CliMetadataTypeDefinitionLockedTableRow"/>
        /// was derived.
        /// </summary>
        public ICliMetadataMutableRoot MetadataRoot
        {
            get
            {
                return this.metadataRoot;
            }
        }
        /// <summary>
        /// Returns returns the events for the current type definition.
        /// </summary>
        public Collection<ICliMetadataEventMutableTableRow> Events
        {
            get
            {
                if (this.events == null)
                    this.events = new Collection<ICliMetadataEventMutableTableRow>();
                return this.events;
            }
        }
        IControlledCollection<AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataEventTableRow> AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataTypeDefinitionTableRow.Events
        {
            get
            {
                if (this._events == null)
                    this._events = new CovariantReadOnlyCollection<AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataEventTableRow, ICliMetadataEventMutableTableRow>(new ControlledCollection<ICliMetadataEventMutableTableRow>(this.Events));
                return this._events;
            }
        }
        /// <summary>
        /// Returns returns the properties for the current type definition.
        /// </summary>
        public Collection<ICliMetadataPropertyMutableTableRow> Properties
        {
            get
            {
                if (this.properties == null)
                    this.properties = new Collection<ICliMetadataPropertyMutableTableRow>();
                return this.properties;
            }
        }
        IControlledCollection<AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataPropertyTableRow> AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataTypeDefinitionTableRow.Properties
        {
            get
            {
                if (this._properties == null)
                    this._properties = new CovariantReadOnlyCollection<AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataPropertyTableRow, ICliMetadataPropertyMutableTableRow>(new ControlledCollection<ICliMetadataPropertyMutableTableRow>(this.Properties));
                return this._properties;
            }
        }
        /// <summary>
        /// Returns returns the fields for the current type.
        /// </summary>
        public Collection<ICliMetadataFieldMutableTableRow> Fields
        {
            get
            {
                if (this.fields == null)
                    this.fields = new Collection<ICliMetadataFieldMutableTableRow>();
                return this.fields;
            }
        }
        IControlledCollection<AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataFieldTableRow> AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataTypeDefinitionTableRow.Fields
        {
            get
            {
                if (this._fields == null)
                    this._fields = new CovariantReadOnlyCollection<AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataFieldTableRow, ICliMetadataFieldMutableTableRow>(new ControlledCollection<ICliMetadataFieldMutableTableRow>(this.Fields));
                return this._fields;
            }
        }
        /// <summary>
        /// Returns returns the methods for the current type.
        /// </summary>
        public Collection<ICliMetadataMethodDefinitionMutableTableRow> Methods
        {
            get
            {
                if (this.methods == null)
                    this.methods = new Collection<ICliMetadataMethodDefinitionMutableTableRow>();
                return this.methods;
            }
        }
        IControlledCollection<AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataMethodDefinitionTableRow> AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataTypeDefinitionTableRow.Methods
        {
            get
            {
                if (this._methods == null)
                    this._methods = new CovariantReadOnlyCollection<AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables.ICliMetadataMethodDefinitionTableRow, ICliMetadataMethodDefinitionMutableTableRow>(new ControlledCollection<ICliMetadataMethodDefinitionMutableTableRow>(this.Methods));
                return this._methods;
            }
        }
    };
};