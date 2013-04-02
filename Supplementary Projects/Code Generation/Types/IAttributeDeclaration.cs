using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Types
{
    /// <summary>
    /// Defines properties and methods for working with an attribute declaration on a type/member.
    /// </summary>
    public interface IAttributeDeclaration :
        ITypeReferenceable
    {
        string Name { get; set; }
        ITypeReference AttributeType { get; set; }
        IAttributeConstructorParameters Parameters { get; }
        CodeAttributeDeclaration GenerateCodeDom(ICodeDOMTranslationOptions options);
    }
}
