using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    /// <summary>
    /// Defines the attribute usage from an <see cref="AttributeUsageAttribute"/>.
    /// </summary>
    internal struct MetadatumUsage
    {
        public readonly bool AllowMultiple;
        public readonly bool Inherited;
        public MetadatumUsage(bool allowMultiple, bool inherited)
        {
            this.AllowMultiple = allowMultiple;
            this.Inherited = inherited;
        }
    }
}
