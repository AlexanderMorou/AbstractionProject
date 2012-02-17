using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Properties;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    partial class CSharpCompilerMessages
    {
        /// <summary><para>Formats the C&#9839; compiler error &#35;453:</para><para>The type '<paramref name="usedType"/>' must be a non-nullable value type in order to use it as parameter '<paramref name="typeParameter"/>' in the generic type or method '<paramref name="genericTarget"/>'.</para></summary>
        /// <param name="usedType">The <see cref="String"/> representing the type that was used in place of the <paramref name="typeParameter"/>.</param>
        /// <param name="typeParameter">The name of the type-parameter that has the struct constraint.</param>
        /// <param name="genericTarget">The <see cref="String"/> value representing the name of the type which
        /// contains the <paramref name="typeParameter"/>.</param>
        /// <returns>A <see cref="String"/> value representing the C&#9839; compiler error &#35;453 formatted with the <paramref name="usedType"/>,
        /// <paramref name="typeParameter"/> and <paramref name="genericTarget"/> provided.</returns>
        public static string FormatCS0453(string usedType, string typeParameter, string genericTarget)
        {
            return string.Format(Resources.CSharpErrors_CS0453, usedType, typeParameter, genericTarget);
        }
    }
}
