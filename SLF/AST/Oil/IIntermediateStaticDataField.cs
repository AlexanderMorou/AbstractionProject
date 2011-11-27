using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public interface IIntermediateStaticDataField :
        IIntermediateMember<IAssembly, IIntermediateAssembly>,
        IStaticDataField
    {
        /// <summary>
        /// Obtains a reference expression which refers to the current
        /// <see cref="IIntermediateFieldMember"/> with the <paramref name="source"/>
        /// which leads up to it.
        /// </summary>
        /// <returns>A <see cref="IFieldReferenceExpression"/> which refers to the current
        /// <see cref="IIntermediateFieldMember"/> with the <paramref name="source"/>
        /// which leads up to it.</returns>
        IFieldReferenceExpression GetReference();
        /// <summary>
        /// Returns/sets the <see cref="String"/> associated to the
        /// documentation summary related to the current 
        /// <see cref="IIntermediateEnumFieldMember"/>.
        /// </summary>
        string Summary { get; set; }

        /// <summary>
        /// Returns/sets the <see cref="Byte"/> array of information which
        /// is stored within the <see cref="IIntermediateStaticDataField"/>.
        /// </summary>
        /// <remarks>The data used here determines the resultant data-type associated with
        /// the <see cref="IIntermediateStaticDataField"/>.</remarks>
        public byte[] Data { get; set; }
    }
}
