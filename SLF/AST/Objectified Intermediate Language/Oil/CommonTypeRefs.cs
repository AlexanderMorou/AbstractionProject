using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using Microsoft.VisualBasic.CompilerServices;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
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
        private static IType parameterArrayAttribute;
        /// <summary>
        /// Data member for <see cref="Delegate"/>.
        /// </summary>
        private static IType @delegate;
        /// <summary>
        /// Data member for <see cref="MulticastDelegate"/>.
        /// </summary>
        private static IType multicastDelegate;

        /// <summary>
        /// Data member for <see cref="CommonTypeRefs.Object"/>.
        /// </summary>
        private static IType @object;

        /// <summary>
        /// Data member for <see cref="CommonTypeRefs.Void"/>.
        /// </summary>
        private static IType @void;

        /// <summary>
        /// Data member for <see cref="StandardModuleAttribute"/>
        /// </summary>
        private static IType standardModuleAttribute;

        /// <summary>
        /// Data member for <see cref="ExtensionAttribute"/>.
        /// </summary>
        private static IType extensionAttribute;

        /// <summary>
        /// Data member for <see cref="ValueType"/>.
        /// </summary>
        private static IType valueType;

        /// <summary>
        /// Data member for <see cref="Enum"/>.
        /// </summary>
        private static IType @enum;

        /// <summary>
        /// Data member for <see cref="String"/>.
        /// </summary>
        private static IType @string;

        /// <summary>
        /// Data member for <see cref="Boolean"/>.
        /// </summary>
        private static IType boolean;
        #endregion

        #region IType Dispose section
        static void void_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.@void != null)
            {
                CommonTypeRefs.@void.Disposed -= new EventHandler(void_Disposed);
                CommonTypeRefs.@void = null;
            }
        }

        static void parameterArrayAttribute_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.parameterArrayAttribute != null)
            {
                CommonTypeRefs.parameterArrayAttribute.Disposed -= new EventHandler(void_Disposed);
                CommonTypeRefs.parameterArrayAttribute = null;
            }
        }

        static void object_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.@object != null)
            {
                CommonTypeRefs.@object.Disposed -= new EventHandler(void_Disposed);
                CommonTypeRefs.@object = null;
            }
        }

        static void extensionAttribute_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.extensionAttribute != null)
            {
                CommonTypeRefs.extensionAttribute.Disposed -= new EventHandler(void_Disposed);
                CommonTypeRefs.extensionAttribute = null;
            }
        }

        static void standardModuleAttribute_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.standardModuleAttribute != null)
            {
                CommonTypeRefs.standardModuleAttribute.Disposed -= new EventHandler(void_Disposed);
                CommonTypeRefs.standardModuleAttribute = null;
            }
        }

        static void valueType_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.valueType != null)
            {
                CommonTypeRefs.valueType.Disposed -= new EventHandler(void_Disposed);
                CommonTypeRefs.valueType = null;
            }
        }

        static void enum_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.@enum != null)
            {
                CommonTypeRefs.@enum.Disposed -= new EventHandler(enum_Disposed);
                CommonTypeRefs.@enum = null;
            }
        }

        static void delegate_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.@delegate != null)
            {
                CommonTypeRefs.@delegate.Disposed -= new EventHandler(delegate_Disposed);
                CommonTypeRefs.@delegate = null;
            }
        }

        static void multicastDelegate_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.multicastDelegate != null)
            {
                CommonTypeRefs.multicastDelegate.Disposed -= new EventHandler(multicastDelegate_Disposed);
                CommonTypeRefs.multicastDelegate = null;
            }
        }

        static void string_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.@string != null)
            {
                CommonTypeRefs.@string.Disposed -= new EventHandler(string_Disposed);
                CommonTypeRefs.@string = null;
            }
        }

        static void boolean_Disposed(object sender, EventArgs e)
        {
            if (CommonTypeRefs.boolean != null)
            {
                CommonTypeRefs.boolean.Disposed -= new EventHandler(boolean_Disposed);
                CommonTypeRefs.boolean = null;
            }
        }
        #endregion

        /// <summary>
        /// Returns the <see cref="IType"/> reference wrapper
        /// for the <see cref="System.Runtime.CompilerServices.ExtensionAttribute"/>
        /// system type.
        /// </summary>
        public static IType ExtensionAttribute
        {
            get
            {
                if (CommonTypeRefs.extensionAttribute == null)
                {
                    CommonTypeRefs.extensionAttribute = typeof(ExtensionAttribute).GetTypeReference();
                    CommonTypeRefs.extensionAttribute.Disposed += new EventHandler(CommonTypeRefs.extensionAttribute_Disposed);
                }
                return CommonTypeRefs.extensionAttribute;
            }
        }

        /// <summary>
        /// Returns the <see cref="IType"/> reference wrapper for the 
        /// <see cref="Microsoft.VisualBasic.CompilerServices.StandardModuleAttribute"/>
        /// system type.
        /// </summary>
        public static IType StandardModuleAttribute
        {
            get
            {
                if (CommonTypeRefs.standardModuleAttribute == null)
                {
                    CommonTypeRefs.standardModuleAttribute = typeof(StandardModuleAttribute).GetTypeReference();
                    CommonTypeRefs.standardModuleAttribute.Disposed += new EventHandler(CommonTypeRefs.standardModuleAttribute_Disposed);
                }
                return CommonTypeRefs.standardModuleAttribute;
            }
        }

        /// <summary>
        /// Returns the <see cref="IType"/> reference wrapper for the <see cref="System.ParamArrayAttribute"/>
        /// system type.
        /// </summary>
        public static IType ParameterArrayAttribute
        {
            get
            {
                if (CommonTypeRefs.parameterArrayAttribute == null)
                {
                    CommonTypeRefs.parameterArrayAttribute = typeof(ParamArrayAttribute).GetTypeReference();
                    CommonTypeRefs.parameterArrayAttribute.Disposed += new EventHandler(CommonTypeRefs.parameterArrayAttribute_Disposed);
                }
                    
                return CommonTypeRefs.parameterArrayAttribute;
            }
        }

        /// <summary>
        /// Returns the <see cref="IType"/> reference wrapper for the <see cref="System.Object"/>
        /// system type.
        /// </summary>
        public static IType Object
        {
            get
            {
                if (CommonTypeRefs.@object == null)
                {
                    CommonTypeRefs.@object = typeof(Object).GetTypeReference();
                    CommonTypeRefs.@object.Disposed += new EventHandler(CommonTypeRefs.object_Disposed);
                }
                return CommonTypeRefs.@object;
            }
        }

        /// <summary>
        /// Returns the <see cref="IType"/> reference wrapper for the <see cref="System.Void"/>
        /// system type.
        /// </summary>
        public static IType Void
        {
            get
            {
                if (CommonTypeRefs.@void == null)
                {
                    CommonTypeRefs.@void = typeof(void).GetTypeReference();
                    CommonTypeRefs.@void.Disposed += new EventHandler(CommonTypeRefs.void_Disposed);
                }
                return CommonTypeRefs.@void;
            }
        }

        /// <summary>
        /// Returns the <see cref="IType"/> reference wrapper for the <see cref="ValueType"/>
        /// system type.
        /// </summary>
        public static IType ValueType
        {
            get
            {
                if (CommonTypeRefs.valueType == null)
                {
                    CommonTypeRefs.valueType = typeof(ValueType).GetTypeReference();
                    CommonTypeRefs.valueType.Disposed += new EventHandler(CommonTypeRefs.valueType_Disposed);
                }
                return CommonTypeRefs.valueType;
            }
        }

        /// <summary>
        /// Returns the <see cref="IType"/> reference wrapper for the <see cref="Enum"/>
        /// system type.
        /// </summary>
        public static IType Enum
        {
            get
            {
                if (CommonTypeRefs.@enum == null)
                {
                    CommonTypeRefs.@enum = typeof(Enum).GetTypeReference();
                    CommonTypeRefs.@enum.Disposed += new EventHandler(CommonTypeRefs.enum_Disposed);
                }
                return CommonTypeRefs.@enum;
            }
        }

        /// <summary>
        /// Returns the <see cref="IType"/> reference wrapper for the <see cref="Delegate"/>
        /// system type.
        /// </summary>
        public static IType Delegate
        {
            get
            {
                if (CommonTypeRefs.@delegate == null)
                {
                    CommonTypeRefs.@delegate = typeof(Delegate).GetTypeReference();
                    CommonTypeRefs.@delegate.Disposed += new EventHandler(CommonTypeRefs.delegate_Disposed);
                }
                return CommonTypeRefs.@delegate;
            }
        }

        /// <summary>
        /// Returns the <see cref="IType"/> reference wrapper for the <see cref="MulticastDelegate"/>
        /// system type.
        /// </summary>
        public static IType MulticastDelegate
        {
            get
            {
                if (CommonTypeRefs.multicastDelegate == null)
                {
                    CommonTypeRefs.multicastDelegate = typeof(MulticastDelegate).GetTypeReference();
                    CommonTypeRefs.multicastDelegate.Disposed += new EventHandler(CommonTypeRefs.multicastDelegate_Disposed);
                }
                return CommonTypeRefs.multicastDelegate;
            }
        }


        /// <summary>
        /// Returns the <see cref="IType"/> reference wrapper for the <see cref="System.String"/>
        /// system type.
        /// </summary>
        public static IType String
        {
            get
            {
                if (CommonTypeRefs.@string == null)
                {
                    CommonTypeRefs.@string = typeof(string).GetTypeReference();
                    CommonTypeRefs.@string.Disposed += new EventHandler(string_Disposed);
                }
                return CommonTypeRefs.@string;
            }
        }
        /// <summary>
        /// Returns the <see cref="IType"/> reference wrapper for the <see cref="System.Boolean"/>
        /// system type.
        /// </summary>
        public static IType Boolean
        {
            get
            {
                if (CommonTypeRefs.boolean == null)
                {
                    CommonTypeRefs.boolean = typeof(bool).GetTypeReference();
                    CommonTypeRefs.boolean.Disposed += new EventHandler(boolean_Disposed);
                }
                return CommonTypeRefs.boolean;
            }
        }

    }
}
