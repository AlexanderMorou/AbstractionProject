using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Types
{
    /// <summary>
    /// Defines properties and methods for working with a constructor argument on an
    /// attribute declaration of a type/member.
    /// </summary>
    public interface IAttributeConstructorParameter :
        ITypeReferenceable,
        IDisposable
    {
        /// <summary>
        /// Returns/sets the value of the <see cref="IAttributeConstructorParameter"/>.
        /// </summary>
        IExpression Value { get; set; }

        CodeAttributeArgument GenerateCodeDom(ICodeDOMTranslationOptions options);

    }
}
