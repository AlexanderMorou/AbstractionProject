using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a series of <see cref="Guid"/> values relative to the default
    /// services an <see cref="IIdentityManager"/> should support.
    /// </summary>
    public static class IdentityManagerServiceGuids
    {
        /// <summary>
        /// Guid representing the identity service used to 
        /// check the relationship of a given type to the
        /// target platform's metadata implementation.
        /// </summary>
        public static readonly Guid MetadatumService = new Guid("ACA35E18-6B6D-4A60-8CC1-5C1E03FB1835");
        /// <summary>
        /// Guid representing the type name builder service used to construct
        /// the name of a type for debugging purposes.
        /// </summary>
        public static readonly Guid TypeNameBuilderService = new Guid("01791C93-FF68-4298-952C-1FDBBFBF63E1");
    }
}
