using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using AllenCopeland.Abstraction.OldCodeGen.Statements;
using System.Reflection;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    /// <summary>
    /// Defines properties and methods for working with a property member of a declared type
    /// </summary>
    public interface IPropertyMember :
        IPropertySignatureMember<IMemberParentType>,
        IImplementedMember,
        ICodeBodyTableMember
    {
        new IPropertyReferenceExpression GetReference();
        new IPropertyReferenceExpression GetReference(IMemberParentExpression source);
        /// <summary>
        /// Returns the get body of the property.
        /// </summary>
        IPropertyBodyMember GetPart { get; }
        /// <summary>
        /// Returns the set body of the property.
        /// </summary>
        IPropertySetBodyMember SetPart { get; }
    }
}
