﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AllenCopeland.Abstraction.Slf.Ast.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AllenCopeland.Abstraction.Slf.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error, the specified type for the coercion type is an interface.  This is illegal by convention.  Try implementing the interface instead..
        /// </summary>
        internal static string Exception_Argument_CoercionType_CannotBeInterface {
            get {
                return ResourceManager.GetString("Exception_Argument_CoercionType_CannotBeInterface", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error, the specified type for the custom attribute definition is not derived from attribute.  Attributes cannot be applied unless they derive from Attribute..
        /// </summary>
        internal static string Exception_Argument_CustomAttribute_Type_MustBeAttribute {
            get {
                return ResourceManager.GetString("Exception_Argument_CustomAttribute_Type_MustBeAttribute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error, the custom attribute type defined in the parameter &apos;{0}&apos; is null..
        /// </summary>
        internal static string Exception_ArgumentNull_CustomAttribute_ctor_data {
            get {
                return ResourceManager.GetString("Exception_ArgumentNull_CustomAttribute_ctor_data", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Compiler in invalid state, {0}..
        /// </summary>
        internal static string Exception_InvalidOperation_CompilerState {
            get {
                return ResourceManager.GetString("Exception_InvalidOperation_CompilerState", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error, the containing type is set to occupy both the left and right parameters of the binary operation.  Alter ContainingSide first..
        /// </summary>
        internal static string Exception_InvalidOperation_ContainingSideIsBoth {
            get {
                return ResourceManager.GetString("Exception_InvalidOperation_ContainingSideIsBoth", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameter count must match..
        /// </summary>
        internal static string Exception_ParameterCount_Mismatch {
            get {
                return ResourceManager.GetString("Exception_ParameterCount_Mismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Delegate already compiled, signature does not match..
        /// </summary>
        internal static string Exception_Precompiled_Mismatch {
            get {
                return ResourceManager.GetString("Exception_Precompiled_Mismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Result type does not match..
        /// </summary>
        internal static string Exception_ReturnType_Mismatch {
            get {
                return ResourceManager.GetString("Exception_ReturnType_Mismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to return.
        /// </summary>
        internal static string Statement_Return_Keyword {
            get {
                return ResourceManager.GetString("Statement_Return_Keyword", resourceCulture);
            }
        }
    }
}