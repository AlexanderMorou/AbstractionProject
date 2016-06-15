using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Cli.Metadata
{
    public interface ICliMetadataMutableStringsHeaderAndHeap :
        ICliMetadataStringsHeaderAndHeap,
        IControlledDictionary<int, string>
    {
        /// <summary>
        /// Makes a dependent <see cref="String"/> <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="String"/> to make a dependency
        /// on.</param>
        /// <returns>A <see cref="UInt32"/> value denoting the dependency
        /// index.</returns>
        uint MakeDependency(string value);
        /// <summary>
        /// Removes the dependency on a given <paramref name="dependencyIndex"/>.
        /// </summary>
        /// <param name="dependencyIndex">The <see cref="UInt32"/> value
        /// previously retrieved from the <see cref="MakeDependency(String)"/>
        /// method.</param>
        void RemoveDependency(uint dependencyIndex);
    }
}
