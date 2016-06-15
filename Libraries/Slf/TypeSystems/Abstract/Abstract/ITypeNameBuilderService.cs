using AllenCopeland.Abstraction.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Denotes the display mode of type parameters for type identity building.
    /// </summary>
    public enum TypeParameterDisplayMode
    {
        /// <summary>
        /// Denotes the type-parameter display mode used should be relative to how the system defines it.
        /// </summary>
        SystemStandard,
        /// <summary>
        /// Denotes the type-parameter display mode used should be relative to how the runtime defines it for debugging purposes.
        /// </summary>
        DebuggerStandard,
        CommentStandard,
    }
    public interface ITypeNameBuilderService :
        IIdentityService
    {
        /// <summary>
        /// Returns the <see cref="IIdentityManager"/> associated to the
        /// <see cref="ITypeNameBuilderService"/>.
        /// </summary>
        IIdentityManager IdentityManager { get; }
        /// <summary>
        /// Debug method used to display the name of a type in a runtime specific manner.
        /// </summary>
        /// <param name="target">The <see cref="IType"/> to build the type name for.</param>
        /// <param name="shortFormGeneric">Whether the names of the type-parameters
        /// within the type are shortened for use within the brackets
        /// of a generic-type instance vs. their full type-name.</param>
        /// <param name="numericTypeParams">Whether type-parameters are shown as numbers.</param>
        /// <param name="typeParameterDisplayMode">Whether to use angle brackets or square brackets
        /// based upon the call-site's intent.  <see cref="TypeParameterDisplayMode.SystemStandard"/> shows 
        /// square brackets ('[' and ']'), and <see cref="TypeParameterDisplayMode.DebuggerStandard"/>
        /// shows angle brackets ('&lt;' and '&gt;'); however, this may vary depending on runtime implementation.</param>
        /// <returns>A <see cref="String"/> value denoting the identity of the <paramref name="target"/>.</returns>
        string BuildTypeName(IType target, bool shortFormGeneric = false, bool numericTypeParams = false, TypeParameterDisplayMode typeParameterDisplayMode = TypeParameterDisplayMode.SystemStandard);
    }
}
