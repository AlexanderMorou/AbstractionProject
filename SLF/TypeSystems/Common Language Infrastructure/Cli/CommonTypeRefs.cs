using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic;
using System.Diagnostics;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Cli
{
    /// <summary>
    /// Provides a series of commonly used type references for caching purposes.
    /// </summary>
    public class CommonTypeRefs
    {
        #region Data members for Commonly Used Type References
        /// <summary>
        /// Data member for <see cref="ParameterArrayAttribute"/>.
        /// </summary>
        private static IClassType parameterArrayAttribute;
        /// <summary>
        /// Data member for <see cref="Delegate"/>.
        /// </summary>
        private static IClassType @delegate;
        /// <summary>
        /// Data member for <see cref="MulticastDelegate"/>.
        /// </summary>
        private static IClassType multicastDelegate;

        /// <summary>
        /// Data member for <see cref="CommonTypeRefs.Object"/>.
        /// </summary>
        private static IClassType @object;

        /// <summary>
        /// Data member for <see cref="CommonTypeRefs.Void"/>.
        /// </summary>
        private static IStructType @void;

        /// <summary>
        /// Data member for <see cref="StandardModuleAttribute"/>
        /// </summary>
        private static IClassType standardModuleAttribute;

        /// <summary>
        /// Data member for <see cref="ExtensionAttribute"/>.
        /// </summary>
        private static IClassType extensionAttribute;

        /// <summary>
        /// Data member for <see cref="ValueType"/>.
        /// </summary>
        private static IClassType valueType;

        /// <summary>
        /// Data member for <see cref="Enum"/>.
        /// </summary>
        private static IClassType @enum;

        /// <summary>
        /// Data member for <see cref="String"/>.
        /// </summary>
        private static IClassType @string;

        /// <summary>
        /// Data member for <see cref="Boolean"/>.
        /// </summary>
        private static IStructType boolean;

        /// <summary>
        /// Data member for <see cref="HideModuleNameAttribute"/>
        /// </summary>
        private static IClassType hideModuleNameAttribute;

        /// <summary>
        /// Data member for <see cref="CompilerGeneratedAttribute"/>
        /// </summary>
        private static IClassType compilerGeneratedAttribute;
        #endregion

        #region IType Dispose section
        static void void_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.@void != null)
            {
#if DEBUG
                Debug.WriteLine("Void Disposed.");
#endif
                CommonTypeRefs.@void.Disposed -= new EventHandler(void_Disposed);
                CommonTypeRefs.@void = null;
            }
        }

        static void parameterArrayAttribute_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.parameterArrayAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("ParameterArrayAttribute Disposed.");
#endif
                CommonTypeRefs.parameterArrayAttribute.Disposed -= new EventHandler(parameterArrayAttribute_Disposed);
                CommonTypeRefs.parameterArrayAttribute = null;
            }
        }

        static void object_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.@object != null)
            {
#if DEBUG
                Debug.WriteLine("Object Disposed.");
#endif
                CommonTypeRefs.@object.Disposed -= new EventHandler(object_Disposed);
                CommonTypeRefs.@object = null;
            }
        }

        static void extensionAttribute_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.extensionAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("ExtensionAttribute Disposed.");
#endif
                CommonTypeRefs.extensionAttribute.Disposed -= new EventHandler(extensionAttribute_Disposed);
                CommonTypeRefs.extensionAttribute = null;
            }
        }

        static void standardModuleAttribute_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.standardModuleAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("StandardModuleAttribute Disposed.");
#endif
                CommonTypeRefs.standardModuleAttribute.Disposed -= standardModuleAttribute_Disposed;
                CommonTypeRefs.standardModuleAttribute = null;
            }
        }

        static void valueType_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.valueType != null)
            {
#if DEBUG
                Debug.WriteLine("ValueType Disposed.");
#endif
                CommonTypeRefs.valueType.Disposed -= valueType_Disposed;
                CommonTypeRefs.valueType = null;
            }
        }

        static void enum_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.@enum != null)
            {
#if DEBUG
                Debug.WriteLine("Enum Disposed.");
#endif
                CommonTypeRefs.@enum.Disposed -= enum_Disposed;
                CommonTypeRefs.@enum = null;
            }
        }

        static void delegate_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.@delegate != null)
            {
#if DEBUG
                Debug.WriteLine("Delegate Disposed.");
#endif
                CommonTypeRefs.@delegate.Disposed -= delegate_Disposed;
                CommonTypeRefs.@delegate = null;
            }
        }

        static void multicastDelegate_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.multicastDelegate != null)
            {
#if DEBUG
                Debug.WriteLine("MulticastDelegate Disposed.");
#endif
                CommonTypeRefs.multicastDelegate.Disposed -= multicastDelegate_Disposed;
                CommonTypeRefs.multicastDelegate = null;
            }
        }

        static void string_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.@string != null)
            {
#if DEBUG
                Debug.WriteLine("String Disposed.");
#endif
                CommonTypeRefs.@string.Disposed -= string_Disposed;
                CommonTypeRefs.@string = null;
            }
        }

        static void boolean_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.boolean != null)
            {
#if DEBUG
                Debug.WriteLine("Boolean Disposed.");
#endif
                CommonTypeRefs.boolean.Disposed -= boolean_Disposed;
                CommonTypeRefs.boolean = null;
            }
        }

        static void hideModuleNameAttribute_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.hideModuleNameAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("HideModuleNameAttribute Disposed.");
#endif
                CommonTypeRefs.hideModuleNameAttribute.Disposed -= hideModuleNameAttribute_Disposed;
                CommonTypeRefs.hideModuleNameAttribute = null;
            }
        }
        static void compilerGeneratedAttribute_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.compilerGeneratedAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("CompilerGeneratedAttribute Disposed.");
#endif
                CommonTypeRefs.compilerGeneratedAttribute.Disposed -= compilerGeneratedAttribute_Disposed;
                CommonTypeRefs.compilerGeneratedAttribute = null;
            }
        }
        #endregion

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper
        /// for the <see cref="System.Runtime.CompilerServices.ExtensionAttribute"/>
        /// system type.
        /// </summary>
        public static IClassType ExtensionAttribute
        {
            get
            {
                if (CommonTypeRefs.extensionAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("ExtensionAttribute initialized.");
#endif
                    CommonTypeRefs.extensionAttribute = typeof(ExtensionAttribute).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
                    CommonTypeRefs.extensionAttribute.Disposed += CommonTypeRefs.extensionAttribute_Disposed;
                }
                return CommonTypeRefs.extensionAttribute;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the 
        /// <see cref="Microsoft.VisualBasic.CompilerServices.StandardModuleAttribute"/>
        /// system type.
        /// </summary>
        public static IClassType StandardModuleAttribute
        {
            get
            {
                if (CommonTypeRefs.standardModuleAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("StandardModuleAttribute initialized.");
#endif
                    CommonTypeRefs.standardModuleAttribute = typeof(StandardModuleAttribute).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
                    CommonTypeRefs.standardModuleAttribute.Disposed += CommonTypeRefs.standardModuleAttribute_Disposed;
                }
                return CommonTypeRefs.standardModuleAttribute;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="System.ParamArrayAttribute"/>
        /// system type.
        /// </summary>
        public static IClassType ParameterArrayAttribute
        {
            get
            {
                if (CommonTypeRefs.parameterArrayAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("ParameterArrayAttribute initialized.");
#endif
                    CommonTypeRefs.parameterArrayAttribute = typeof(ParamArrayAttribute).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
                    CommonTypeRefs.parameterArrayAttribute.Disposed += CommonTypeRefs.parameterArrayAttribute_Disposed;
                }
                    
                return CommonTypeRefs.parameterArrayAttribute;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="System.Object"/>
        /// system type.
        /// </summary>
        public static IClassType Object
        {
            get
            {
                if (CommonTypeRefs.@object == null)
                {
#if DEBUG
                    Debug.WriteLine("Object initialized.");
#endif
                    CommonTypeRefs.@object = typeof(Object).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
                    CommonTypeRefs.@object.Disposed += CommonTypeRefs.object_Disposed;
                }
                return CommonTypeRefs.@object;
            }
        }

        /// <summary>
        /// Returns the <see cref="IArrayType"/> reference wrapper for the <see cref="System.Object"/>
        /// system type as an array.
        /// </summary>
        public static IArrayType ObjectArray
        {
            get
            {
                return Object.MakeArray();
            }
        }

        /// <summary>
        /// Returns the <see cref="IStructType"/> reference wrapper for the <see cref="System.Void"/>
        /// system type.
        /// </summary>
        public static IStructType Void
        {
            get
            {
                if (CommonTypeRefs.@void == null)
                {
#if DEBUG
                    Debug.WriteLine("Void initialized.");
#endif
                    CommonTypeRefs.@void = typeof(void).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IStructType>();
                    CommonTypeRefs.@void.Disposed += CommonTypeRefs.void_Disposed;
                }
                return CommonTypeRefs.@void;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="ValueType"/>
        /// system type.
        /// </summary>
        public static IClassType ValueType
        {
            get
            {
                if (CommonTypeRefs.valueType == null)
                {
#if DEBUG
                    Debug.WriteLine("ValueType initialized.");
#endif
                    CommonTypeRefs.valueType = typeof(ValueType).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
                    CommonTypeRefs.valueType.Disposed += CommonTypeRefs.valueType_Disposed;
                }
                return CommonTypeRefs.valueType;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="Enum"/>
        /// system type.
        /// </summary>
        public static IClassType Enum
        {
            get
            {
                if (CommonTypeRefs.@enum == null)
                {
#if DEBUG
                    Debug.WriteLine("Enum initialized.");
#endif
                    CommonTypeRefs.@enum = typeof(Enum).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
                    CommonTypeRefs.@enum.Disposed += CommonTypeRefs.enum_Disposed;
                }
                return CommonTypeRefs.@enum;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="Delegate"/>
        /// system type.
        /// </summary>
        public static IClassType Delegate
        {
            get
            {
                if (CommonTypeRefs.@delegate == null)
                {
#if DEBUG
                    Debug.WriteLine("Delegate initialized.");
#endif
                    CommonTypeRefs.@delegate = typeof(Delegate).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
                    CommonTypeRefs.@delegate.Disposed += CommonTypeRefs.delegate_Disposed;
                }
                return CommonTypeRefs.@delegate;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="MulticastDelegate"/>
        /// system type.
        /// </summary>
        public static IClassType MulticastDelegate
        {
            get
            {
                if (CommonTypeRefs.multicastDelegate == null)
                {
#if DEBUG
                    Debug.WriteLine("MulticastDelegate initialized.");
#endif
                    CommonTypeRefs.multicastDelegate = typeof(MulticastDelegate).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
                    CommonTypeRefs.multicastDelegate.Disposed += CommonTypeRefs.multicastDelegate_Disposed;
                }
                return CommonTypeRefs.multicastDelegate;
            }
        }


        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="System.String"/>
        /// system type.
        /// </summary>
        public static IClassType String
        {
            get
            {
                if (CommonTypeRefs.@string == null)
                {
#if DEBUG
                    Debug.WriteLine("String initialized.");
#endif
                    CommonTypeRefs.@string = typeof(string).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
                    CommonTypeRefs.@string.Disposed += string_Disposed;
                }
                return CommonTypeRefs.@string;
            }
        }


        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the
        /// <see cref="System.Runtime.CompilerServices.CompilerGeneratedAttribute"/>
        /// system type.
        /// </summary>
        public static IClassType CompilerGeneratedAttribute
        {
            get
            {
                if (CommonTypeRefs.compilerGeneratedAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("CompilerGeneratedAttribute initialized.");
#endif
                    CommonTypeRefs.compilerGeneratedAttribute = typeof(CompilerGeneratedAttribute).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
                    CommonTypeRefs.compilerGeneratedAttribute.Disposed += compilerGeneratedAttribute_Disposed;
                }
                return CommonTypeRefs.compilerGeneratedAttribute;
            }
        }

        /// <summary>
        /// Returns the <see cref="IArrayType"/> reference wrapper for the <see cref="System.String"/>
        /// system type as an array.
        /// </summary>
        public static IArrayType StringArray
        {
            get
            {
                return String.MakeArray();
            }
        }

        /// <summary>
        /// Returns the <see cref="IStructType"/> reference wrapper for the <see cref="System.Boolean"/>
        /// system type.
        /// </summary>
        public static IStructType Boolean
        {
            get
            {
                if (CommonTypeRefs.boolean == null)
                {
#if DEBUG
                    Debug.WriteLine("Boolean initialized.");
#endif
                    CommonTypeRefs.boolean = typeof(bool).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IStructType>();
                    CommonTypeRefs.boolean.Disposed += new EventHandler(boolean_Disposed);
                }
                return CommonTypeRefs.boolean;
            }
        }

        /// <summary>
        /// Returns the <see cref="IArrayType"/> reference wrapper for the <see cref="System.Boolean"/>
        /// system type as an array.
        /// </summary>
        public static IArrayType BooleanArray
        {
            get
            {
                return Boolean.MakeArray();
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="HideModuleNameAttribute"/>
        /// system type.
        /// </summary>
        public static IClassType HideModuleNameAttribute
        {
            get
            {
                if (CommonTypeRefs.hideModuleNameAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("HideModuleNameAttribute initialized.");
#endif
                    CommonTypeRefs.hideModuleNameAttribute = typeof(HideModuleNameAttribute).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
                    CommonTypeRefs.hideModuleNameAttribute.Disposed += new EventHandler(hideModuleNameAttribute_Disposed);
                }
                return CommonTypeRefs.hideModuleNameAttribute;
            }
        }

    }
}
