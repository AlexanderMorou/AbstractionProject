using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables
{
    public enum MethodExceptionClauseFlags
    {
        Exception = 0x0000,
        Filter    = 0x0001,
        Finally   = 0x0002,
        Fault     = 0x0004,
    }
    /// <summary>
    /// Defines properties and methods for working with a method exception clause.
    /// </summary>
    public interface ICliMetadataMethodExceptionClause
    {
        /// <summary>
        /// Returns the <see cref="MethodExceptionClauseFlags"/> which denote
        /// the kind of exception referenced by the current 
        /// <see cref="ICliMetadataMethodExceptionClause"/>.
        /// </summary>
        MethodExceptionClauseFlags Flags { get; }
    }
}
