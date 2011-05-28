using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Oil.VisualBasic;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Defines properties and methods for working with the 
    /// <see cref="IVisualBasicLanguage">Visual Basic.NET language</see>.
    /// </summary>
    public interface IVisualBasicLanguage :
        IVersionedHighLevelLanguage<VisualBasicVersion, IVisualBasicStart>
    {
        new IVisualBasicProvider GetProvider();
        /// <summary>
        /// Returns a new <see cref="IVisualBasicProvider"/> associated to the
        /// <see cref="IVisualBasicLanguage"/>.
        /// </summary>
        /// <param name="version">The <see cref="VisualBasicVersion"/>
        /// value which denotes what version of the visual basic 
        /// language to return the provider for.</param>
        /// <returns>A new <see cref="IVisualBasicProvider"/> for the current
        /// <see cref="IVisualBasicLanguage"/>.</returns>
        new IVisualBasicProvider GetProvider(VisualBasicVersion version);
    }
}
