using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public interface IIntermediateTypeVisitor
    {
        /// <summary>
        /// Visits the <paramref name="class"/> provided.
        /// </summary>
        /// <param name="class">The <see cref="IIntermediateClassType"/>
        /// to visit.</param>
        void Visit(IIntermediateClassType @class);
        /// <summary>
        /// Visits the <paramref name="delegate"/> provided.
        /// </summary>
        /// <param name="delegate">The <see cref="IIntermediateDelegateType"/>
        /// to visit.</param>
        void Visit(IIntermediateDelegateType @delegate);
        /// <summary>
        /// Visits the <paramref name="enum"/> provided.
        /// </summary>
        /// <param name="enum">The <see cref="IIntermediateEnumType"/>
        /// to visit.</param>
        void Visit(IIntermediateEnumType @enum);
        /// <summary>
        /// Visits the <paramref name="interface"/> provided.
        /// </summary>
        /// <param name="interface">The <see cref="IIntermediateInterfaceType"/>
        /// to visit.</param>
        void Visit(IIntermediateInterfaceType @interface);
        /// <summary>
        /// Visits the <paramref name="struct"/> provided.
        /// </summary>
        /// <param name="struct">The <see cref="IIntermediateStructType"/>
        /// to visit.</param>
        void Visit(IIntermediateStructType @struct);
    }
}
