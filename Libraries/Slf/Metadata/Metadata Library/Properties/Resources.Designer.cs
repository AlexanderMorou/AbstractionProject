﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AllenCopeland.Abstraction.Slf._Internal.Metadata.Properties {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AllenCopeland.Abstraction.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Constraints on generic parameter {0} failed on the type provided ({1})..
        /// </summary>
        internal static string AE_GenericParameterFailureException {
            get {
                return ResourceManager.GetString("AE_GenericParameterFailureException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid target container type, must be generated by a compiler..
        /// </summary>
        internal static string AE_TypeMustBeCompilerGenerated {
            get {
                return ResourceManager.GetString("AE_TypeMustBeCompilerGenerated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Provided type must be the child of a generic..
        /// </summary>
        internal static string AE_TypeMustBeGenericChild {
            get {
                return ResourceManager.GetString("AE_TypeMustBeGenericChild", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid target container type, must be a static (abstract sealed) class..
        /// </summary>
        internal static string AE_TypeMustBeStaticClass {
            get {
                return ResourceManager.GetString("AE_TypeMustBeStaticClass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type not generic..
        /// </summary>
        internal static string AE_TypeNotGeneric {
            get {
                return ResourceManager.GetString("AE_TypeNotGeneric", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type provided represents an informational type, and must contain only the generic arguments of declaring generic type..
        /// </summary>
        internal static string AE_TypeParameterInfoError {
            get {
                return ResourceManager.GetString("AE_TypeParameterInfoError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown argument identifier provided..
        /// </summary>
        internal static string AE_UnknownArgument {
            get {
                return ResourceManager.GetString("AE_UnknownArgument", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown argument message identifier provided..
        /// </summary>
        internal static string AE_UnknownArgumentMessage {
            get {
                return ResourceManager.GetString("AE_UnknownArgumentMessage", resourceCulture);
            }
        }
    }
}
