using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using Microsoft.VisualBasic.CompilerServices;
using System.Runtime.CompilerServices;
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
        /// Provides a series of commonly used type references for caching purposes.
        /// </summary>
        public class CommonlyUsedTypeReferences
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
            /// Data member for <see cref="CommonlyUsedTypeReferences.Object"/>.
            /// </summary>
            private static IType @object;

            /// <summary>
            /// Data member for <see cref="CommonlyUsedTypeReferences.Void"/>.
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
                if (CommonlyUsedTypeReferences.@void != null)
                {
                    CommonlyUsedTypeReferences.@void.Disposed -= new EventHandler(void_Disposed);
                    CommonlyUsedTypeReferences.@void = null;
                }
            }

            static void parameterArrayAttribute_Disposed(object sender, EventArgs e)
            {
                if (CommonlyUsedTypeReferences.parameterArrayAttribute != null)
                {
                    CommonlyUsedTypeReferences.parameterArrayAttribute.Disposed -= new EventHandler(void_Disposed);
                    CommonlyUsedTypeReferences.parameterArrayAttribute = null;
                }
            }

            static void object_Disposed(object sender, EventArgs e)
            {
                if (CommonlyUsedTypeReferences.@object != null)
                {
                    CommonlyUsedTypeReferences.@object.Disposed -= new EventHandler(void_Disposed);
                    CommonlyUsedTypeReferences.@object = null;
                }
            }

            static void extensionAttribute_Disposed(object sender, EventArgs e)
            {
                if (CommonlyUsedTypeReferences.extensionAttribute != null)
                {
                    CommonlyUsedTypeReferences.extensionAttribute.Disposed -= new EventHandler(void_Disposed);
                    CommonlyUsedTypeReferences.extensionAttribute = null;
                }
            }

            static void standardModuleAttribute_Disposed(object sender, EventArgs e)
            {
                if (CommonlyUsedTypeReferences.standardModuleAttribute != null)
                {
                    CommonlyUsedTypeReferences.standardModuleAttribute.Disposed -= new EventHandler(void_Disposed);
                    CommonlyUsedTypeReferences.standardModuleAttribute = null;
                }
            }

            static void valueType_Disposed(object sender, EventArgs e)
            {
                if (CommonlyUsedTypeReferences.valueType != null)
                {
                    CommonlyUsedTypeReferences.valueType.Disposed -= new EventHandler(void_Disposed);
                    CommonlyUsedTypeReferences.valueType = null;
                }
            }

            static void enum_Disposed(object sender, EventArgs e)
            {
                if (CommonlyUsedTypeReferences.@enum != null)
                {
                    CommonlyUsedTypeReferences.@enum.Disposed -= new EventHandler(enum_Disposed);
                    CommonlyUsedTypeReferences.@enum = null;
                }
            }

            static void delegate_Disposed(object sender, EventArgs e)
            {
                if (CommonlyUsedTypeReferences.@delegate != null)
                {
                    CommonlyUsedTypeReferences.@delegate.Disposed -= new EventHandler(delegate_Disposed);
                    CommonlyUsedTypeReferences.@delegate = null;
                }
            }

            static void multicastDelegate_Disposed(object sender, EventArgs e)
            {
                if (CommonlyUsedTypeReferences.multicastDelegate != null)
                {
                    CommonlyUsedTypeReferences.multicastDelegate.Disposed -= new EventHandler(multicastDelegate_Disposed);
                    CommonlyUsedTypeReferences.multicastDelegate = null;
                }
            }

            static void string_Disposed(object sender, EventArgs e)
            {
                if (CommonlyUsedTypeReferences.@string != null)
                {
                    CommonlyUsedTypeReferences.@string.Disposed -= new EventHandler(string_Disposed);
                    CommonlyUsedTypeReferences.@string = null;
                }
            }

            static void boolean_Disposed(object sender, EventArgs e)
            {
                if (CommonlyUsedTypeReferences.boolean != null)
                {
                    CommonlyUsedTypeReferences.boolean.Disposed -= new EventHandler(boolean_Disposed);
                    CommonlyUsedTypeReferences.boolean = null;
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
                    if (CommonlyUsedTypeReferences.extensionAttribute == null)
                    {
                        CommonlyUsedTypeReferences.extensionAttribute = typeof(ExtensionAttribute).GetTypeReference();
                        CommonlyUsedTypeReferences.extensionAttribute.Disposed += new EventHandler(CommonlyUsedTypeReferences.extensionAttribute_Disposed);
                    }
                    return CommonlyUsedTypeReferences.extensionAttribute;
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
                    if (CommonlyUsedTypeReferences.standardModuleAttribute == null)
                    {
                        CommonlyUsedTypeReferences.standardModuleAttribute = typeof(StandardModuleAttribute).GetTypeReference();
                        CommonlyUsedTypeReferences.standardModuleAttribute.Disposed += new EventHandler(CommonlyUsedTypeReferences.standardModuleAttribute_Disposed);
                    }
                    return CommonlyUsedTypeReferences.standardModuleAttribute;
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
                    if (CommonlyUsedTypeReferences.parameterArrayAttribute == null)
                    {
                        CommonlyUsedTypeReferences.parameterArrayAttribute = typeof(ParamArrayAttribute).GetTypeReference();
                        CommonlyUsedTypeReferences.parameterArrayAttribute.Disposed += new EventHandler(CommonlyUsedTypeReferences.parameterArrayAttribute_Disposed);
                    }
                    
                    return CommonlyUsedTypeReferences.parameterArrayAttribute;
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
                    if (CommonlyUsedTypeReferences.@object == null)
                    {
                        CommonlyUsedTypeReferences.@object = typeof(Object).GetTypeReference();
                        CommonlyUsedTypeReferences.@object.Disposed += new EventHandler(CommonlyUsedTypeReferences.object_Disposed);
                    }
                    return CommonlyUsedTypeReferences.@object;
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
                    if (CommonlyUsedTypeReferences.@void == null)
                    {
                        CommonlyUsedTypeReferences.@void = typeof(void).GetTypeReference();
                        CommonlyUsedTypeReferences.@void.Disposed += new EventHandler(CommonlyUsedTypeReferences.void_Disposed);
                    }
                    return CommonlyUsedTypeReferences.@void;
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
                    if (CommonlyUsedTypeReferences.valueType == null)
                    {
                        CommonlyUsedTypeReferences.valueType = typeof(ValueType).GetTypeReference();
                        CommonlyUsedTypeReferences.valueType.Disposed += new EventHandler(CommonlyUsedTypeReferences.valueType_Disposed);
                    }
                    return CommonlyUsedTypeReferences.valueType;
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
                    if (CommonlyUsedTypeReferences.@enum == null)
                    {
                        CommonlyUsedTypeReferences.@enum = typeof(Enum).GetTypeReference();
                        CommonlyUsedTypeReferences.@enum.Disposed += new EventHandler(CommonlyUsedTypeReferences.enum_Disposed);
                    }
                    return CommonlyUsedTypeReferences.@enum;
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
                    if (CommonlyUsedTypeReferences.@delegate == null)
                    {
                        CommonlyUsedTypeReferences.@delegate = typeof(Delegate).GetTypeReference();
                        CommonlyUsedTypeReferences.@delegate.Disposed += new EventHandler(CommonlyUsedTypeReferences.delegate_Disposed);
                    }
                    return CommonlyUsedTypeReferences.@delegate;
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
                    if (CommonlyUsedTypeReferences.multicastDelegate == null)
                    {
                        CommonlyUsedTypeReferences.multicastDelegate = typeof(MulticastDelegate).GetTypeReference();
                        CommonlyUsedTypeReferences.multicastDelegate.Disposed += new EventHandler(CommonlyUsedTypeReferences.multicastDelegate_Disposed);
                    }
                    return CommonlyUsedTypeReferences.multicastDelegate;
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
                    if (CommonlyUsedTypeReferences.@string == null)
                    {
                        CommonlyUsedTypeReferences.@string = typeof(string).GetTypeReference();
                        CommonlyUsedTypeReferences.@string.Disposed += new EventHandler(string_Disposed);
                    }
                    return CommonlyUsedTypeReferences.@string;
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
                    if (CommonlyUsedTypeReferences.boolean == null)
                    {
                        CommonlyUsedTypeReferences.boolean = typeof(bool).GetTypeReference();
                        CommonlyUsedTypeReferences.boolean.Disposed += new EventHandler(boolean_Disposed);
                    }
                    return CommonlyUsedTypeReferences.boolean;
                }
            }

        }

    }
}
