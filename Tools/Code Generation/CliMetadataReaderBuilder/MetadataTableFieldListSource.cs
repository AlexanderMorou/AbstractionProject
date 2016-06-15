using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CliMetadataReader
{
    public enum MetadataTableFieldListSource
    {
        /// <summary>
        /// The resulted list exists as a set of the
        /// items that the row targets with a separate
        /// field.
        /// </summary>
        FieldRef,
        /// <summary>
        /// The resulted list exists as a set of the
        /// items that target the list container in 
        /// question.
        /// </summary>
        SourceTableRow,
    }
}
