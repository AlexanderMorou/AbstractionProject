using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    public enum CSharpLanguageVersion
    {
        /// <summary>
        /// CSharp version 1 is the introductory version of the language.
        /// </summary>
        Version1,
        /// <summary>
        /// CSharp version 2 support provided the initial implementation of
        /// generics.
        /// </summary>
        Version2,
        /// <summary>
        /// CSharp version 3 support enables LINQ, Extension methods and
        /// anonymous types and methods.
        /// </summary>
        Version3,
        /// <summary>
        /// CSharp version 4 support enables a Dynamic typing model,
        /// better COM inerop, and primary interop assembly embedding.
        /// </summary>
        Version4,
        /// <summary>
        /// CSharp version 5 support enables the concept of asynchronous 
        /// coding models.
        /// </summary>
        Version5,
        /// <summary>
        /// CSharp version 6 support furthers the concept of asynchronous coding models
        /// and adds the concept of string interpolation, exception filters, auto property
        /// initializers, nullable conditional access operator, nameof expressions,
        /// expression bodied methods, and using static static coercions.
        /// </summary>
        Version6,
        /// <summary>
        /// Represents the current CSharp version.
        /// </summary>
        CurrentVersion = Version5,
    }
}
