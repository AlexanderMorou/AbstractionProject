using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    internal class CoreVisualBasicProvider :
        VisualBasicProvider<ICoreVisualBasicAssembly, ICoreVisualBasicProvider>,
        ICoreVisualBasicProvider
    {
        /// <summary>
        /// Creates a new <see cref="ICoreVisualBasicAssembly"/>
        /// with the <paramref name="name"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing part of the identity of the assembly.</param>
        /// <returns>A new <see cref="ICoreVisualBasicAssembly"/>
        /// with the <paramref name="name"/> provided.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when 
        /// <paramref name="name"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when
        /// <paramref name="name"/> is <see cref="String.Empty"/>.</exception>
        public override ICoreVisualBasicAssembly CreateAssembly(string name)
        {
            return new CoreVisualBasicAssembly(name);
        }

        public CoreVisualBasicProvider(VisualBasicVersion version)
            : base(version)
        {
        }
    }
}
