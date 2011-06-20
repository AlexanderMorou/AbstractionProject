using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Compilers;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateGateway
    {
        /// <summary>
        /// The standard clear-text capable pattern for C&#9839;
        /// </summary>
        internal static readonly IAnonymousTypePatternAid CleanCSharpPattern = new DefaultATPatternAid(AnonymousTypeDisplayStyles.CSharp | AnonymousTypeDisplayStyles.Clean);
        /// <summary>
        /// The standard pattern for C&#9839;
        /// </summary>
        internal static readonly IAnonymousTypePatternAid CSharpPattern = new DefaultATPatternAid(AnonymousTypeDisplayStyles.CSharp);
        /// <summary>
        /// The standard clear-text capable pattern for visual basic.
        /// </summary>
        internal static readonly IAnonymousTypePatternAid CleanVBPattern = new DefaultATPatternAid(AnonymousTypeDisplayStyles.VisualBasic | AnonymousTypeDisplayStyles.Clean);
        /// <summary>
        /// The standard pattern for visual basic.
        /// </summary>
        internal static readonly IAnonymousTypePatternAid VBPattern = new DefaultATPatternAid(AnonymousTypeDisplayStyles.VisualBasic);
        private class DefaultATPatternAid :
            AnonymousTypePatternAid
        {
            private AnonymousTypeDisplayStyles displayStyle;

            public DefaultATPatternAid(AnonymousTypeDisplayStyles displayStyle)
            {
                this.displayStyle = displayStyle;
            }

            #region Language/Clean Checks

            private bool IsCSharp
            {
                get
                {
                    return (displayStyle & AnonymousTypeDisplayStyles.CSharp) == AnonymousTypeDisplayStyles.CSharp;
                }
            }
            private bool IsVisualBasic
            {
                get
                {
                    return (displayStyle & AnonymousTypeDisplayStyles.VisualBasic) == AnonymousTypeDisplayStyles.VisualBasic;
                }
            }
            private bool IsClean
            {
                get
                {
                    return (displayStyle & AnonymousTypeDisplayStyles.Clean) == AnonymousTypeDisplayStyles.Clean;
                }
            }
            #endregion

            #region Pattern Selections
            private string TypePattern
            {
                get
                {
                    const string typePattern_ThisBasePattern = "AnonymousType";
                    const string typePattern_CSharpPattern = "<>{0}__" + typePattern_ThisBasePattern + "{1}";
                    const string typePattern_VBPattern = "VB$" + typePattern_ThisBasePattern + "_{0}";
                    const string typePattern_CleanVBPattern = "VB" + typePattern_ThisBasePattern + "_{0}";
                    const string typePattern_CleanCSharpPattern = "__cs{0}" + typePattern_ThisBasePattern + "{1}__";
                    if (IsClean)
                        if (IsVisualBasic)
                            return typePattern_CleanVBPattern;
                        else /* (IsCSharp) */
                            return typePattern_CleanCSharpPattern;
                    else if (IsVisualBasic)
                        return typePattern_VBPattern;
                    else /* (IsCSharp) */
                        return typePattern_CSharpPattern;
                }
            }

            private string FieldPattern
            {
                get
                {
                    const string fieldPattern_CSharpPattern = "{{0}}{0}__Field";
                    const string fieldPattern_CleanCSharpPattern = "{0}field_{{0}}";
                    const string fieldPattern_VBPattern = "${0}";
                    const string fieldPattern_CleanVBPattern = "_{0}";
                    if (IsClean)
                        if (IsVisualBasic)
                            return fieldPattern_CleanVBPattern;
                        else /* (IsCSharp) */
                            return fieldPattern_CleanCSharpPattern;
                    else if (IsVisualBasic)
                        return fieldPattern_VBPattern;
                    else /* (IsCSharp) */
                        return fieldPattern_CSharpPattern;
                }

            }

            private string TypeParamPattern
            {
                get
                {
                    const string typeParamPattern_CSharpPattern = "<{0}>j__TPar";
                    const string typeParamPattern_VBPattern = "T{0}";
                    const string typeParamPattern_CleanCSharpPattern = "__T{0}";
                    if (IsVisualBasic)
                        return typeParamPattern_VBPattern;
                    else if (IsClean)
                        return typeParamPattern_CleanCSharpPattern;
                    return typeParamPattern_CSharpPattern;
                }
            }
            #endregion
            public override string GetTypeName(IAnonymousType target)
            {
                if (IsVisualBasic)
                    return string.Format(TypePattern, target.Index);
                else
                    return string.Format(TypePattern, "f", target.Index);
            }

            public override string GetTypeParameter(AnonymousTypeMember associatedMember)
            {
                if (IsCSharp)
                    return string.Format(TypeParamPattern, associatedMember.Name);
                else
                    return string.Format(TypeParamPattern, associatedMember.Position);
            }

            public override string GetAnonymousField(AnonymousTypeMember associatedMember)
            {
                const string fieldPattern_CSharpPatternIO = "i";
                const string fieldPattern_CleanCSharpPatternIO = "initOnly";
                if (IsCSharp)
                    return string.Format(string.Format(FieldPattern, associatedMember.Immutable ? IsClean ? fieldPattern_CleanCSharpPatternIO : fieldPattern_CSharpPatternIO : string.Empty), associatedMember.Name);
                else
                    return string.Format(FieldPattern, associatedMember.Name);
            }

            public override string GetAutoGeneratedProperty(AnonymousTypeMember associatedMember)
            {
                return associatedMember.Name;
            }
        }
    }
}