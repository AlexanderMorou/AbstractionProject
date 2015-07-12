using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Vre
{
    public enum VreEnvironmentSubversionSource
    {
        /// <summary>
        /// Denotes the version is the initial version of a given <see cref="IVreEnvironment{TEnvironment, TVersion}"/>.
        /// </summary>
        InitialVersion,
        /// <summary>
        /// Denotes the version is a breaking version from the previous version of a given <see cref="IVreEnvironmentVersion{TEnvironment, TVersion}"/>.
        /// </summary>
        BreakingVersion,
        /// <summary>
        /// Denotes the version is a service pack extension upon the noted version of a given <see cref="IVreEnvironmentVersion{TEnvironment, TVersion}"/>.
        /// </summary>
        ServicePackExtension,
        /// <summary>
        /// Denotes the version is an extension/minor version of either an <see cref="InitialVersion"/> or a <see cref="BreakingVersion"/>.
        /// </summary>
        VersionExtension,
    }
}
