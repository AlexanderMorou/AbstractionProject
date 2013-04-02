using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
using System.CodeDom;
using System.Runtime.Serialization;

namespace AllenCopeland.Abstraction.OldCodeGen.Types
{
    partial class ParameteredDeclaredType<TItem, TDom>
    {
        
        /// <summary>
        /// Type arguments for a <see cref="ParameteredDeclaredType{TItem, TDom}"/>.
        /// </summary>
        [Serializable]
        public class TypeParameterMembers :
            TypeParameterMembers<TDom>
        {
            internal TypeParameterMembers(ParameteredDeclaredType<TItem, TDom> parent)
                : base(parent)
            {
            }
        }
    }
}
