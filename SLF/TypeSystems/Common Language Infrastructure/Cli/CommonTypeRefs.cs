using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.ComponentModel;
using System.CodeDom.Compiler;
using AllenCopeland.Abstraction.Slf.Compilers;
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
        #region Data members for Commonly Used GenericParameter References
        /// <summary>
        /// Data member for <see cref="ParameterArrayAttribute"/>.
        /// </summary>
        private IClassType parameterArrayAttribute;
        /// <summary>
        /// Data member for <see cref="Delegate"/>.
        /// </summary>
        private IClassType @delegate;
        /// <summary>
        /// Data member for <see cref="MulticastDelegate"/>.
        /// </summary>
        private IClassType multicastDelegate;

        /// <summary>
        /// Data member for <see cref="this.Object"/>.
        /// </summary>
        private IClassType @object;

        /// <summary>
        /// Data member for <see cref="this.Void"/>.
        /// </summary>
        private IStructType @void;

        /// <summary>
        /// Data member for <see cref="ExtensionAttribute"/>.
        /// </summary>
        private IClassType extensionAttribute;

        /// <summary>
        /// Data member for <see cref="ValueType"/>.
        /// </summary>
        private IClassType valueType;

        /// <summary>
        /// Data member for <see cref="Enum"/>.
        /// </summary>
        private IClassType @enum;

        /// <summary>
        /// Data member for <see cref="String"/>.
        /// </summary>
        private IClassType @string;

        /// <summary>
        /// Data member for <see cref="Boolean"/>.
        /// </summary>
        private IStructType boolean;

        /// <summary>
        /// Data member for <see cref="CompilerGeneratedAttribute"/>
        /// </summary>
        private IClassType compilerGeneratedAttribute;

        private IClassType task;
        private IClassType taskOfT;
        /// <summary>
        /// Data member for <see cref="EditorBrowsableAttribute"/>
        /// </summary>
        private IClassType editorBrowsableAttribute;
        /// <summary>
        /// Data member for <see cref="ClassIsModuleAttribute"/>.
        /// </summary>
        private IClassType classIsModuleAttribute;
        /// <summary>
        /// Data member for <see cref="ClassIsHiddenAttribute"/>
        /// </summary>
        private IClassType classIsHiddenAttribute;

        private ICliManager manager;

        private static Dictionary<ICliManager, CommonTypeRefs> managerCache;
        #endregion

        private CommonTypeRefs(ICliManager manager)
        {
            this.manager = manager;
        }

        internal static CommonTypeRefs GetCommonTypeRefs(ICliManager manager)
        {
            if (!managerCache.ContainsKey(manager))
                managerCache.Add(manager, new CommonTypeRefs(manager));
            return managerCache[manager];
        }

        #region IType Dispose section
        private void void_Disposed(object sender, EventArgs e)
        {
            if (this.@void != null)
            {
#if DEBUG
                Debug.WriteLine("Void Disposed.");
#endif
                this.@void.Disposed -= new EventHandler(void_Disposed);
                this.@void = null;
            }
        }

        private void parameterArrayAttribute_Disposed(object sender, EventArgs e)
        {
            if (this.parameterArrayAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("ParameterArrayAttribute Disposed.");
#endif
                this.parameterArrayAttribute.Disposed -= new EventHandler(parameterArrayAttribute_Disposed);
                this.parameterArrayAttribute = null;
            }
        }

        private void object_Disposed(object sender, EventArgs e)
        {
            if (this.@object != null)
            {
#if DEBUG
                Debug.WriteLine("Object Disposed.");
#endif
                this.@object.Disposed -= new EventHandler(object_Disposed);
                this.@object = null;
            }
        }

        private void extensionAttribute_Disposed(object sender, EventArgs e)
        {
            if (this.extensionAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("ExtensionAttribute Disposed.");
#endif
                this.extensionAttribute.Disposed -= new EventHandler(extensionAttribute_Disposed);
                this.extensionAttribute = null;
            }
        }

        private void valueType_Disposed(object sender, EventArgs e)
        {
            if (this.valueType != null)
            {
#if DEBUG
                Debug.WriteLine("ValueType Disposed.");
#endif
                this.valueType.Disposed -= valueType_Disposed;
                this.valueType = null;
            }
        }

        private void enum_Disposed(object sender, EventArgs e)
        {
            if (this.@enum != null)
            {
#if DEBUG
                Debug.WriteLine("Enum Disposed.");
#endif
                this.@enum.Disposed -= enum_Disposed;
                this.@enum = null;
            }
        }

        private void delegate_Disposed(object sender, EventArgs e)
        {
            if (this.@delegate != null)
            {
#if DEBUG
                Debug.WriteLine("Delegate Disposed.");
#endif
                this.@delegate.Disposed -= delegate_Disposed;
                this.@delegate = null;
            }
        }

        private void multicastDelegate_Disposed(object sender, EventArgs e)
        {
            if (this.multicastDelegate != null)
            {
#if DEBUG
                Debug.WriteLine("MulticastDelegate Disposed.");
#endif
                this.multicastDelegate.Disposed -= multicastDelegate_Disposed;
                this.multicastDelegate = null;
            }
        }

        private void string_Disposed(object sender, EventArgs e)
        {
            if (this.@string != null)
            {
#if DEBUG
                Debug.WriteLine("String Disposed.");
#endif
                this.@string.Disposed -= string_Disposed;
                this.@string = null;
            }
        }

        private void boolean_Disposed(object sender, EventArgs e)
        {
            if (this.boolean != null)
            {
#if DEBUG
                Debug.WriteLine("Boolean Disposed.");
#endif
                this.boolean.Disposed -= boolean_Disposed;
                this.boolean = null;
            }
        }

        private void compilerGeneratedAttribute_Disposed(object sender, EventArgs e)
        {
            if (this.compilerGeneratedAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("CompilerGeneratedAttribute Disposed.");
#endif
                this.compilerGeneratedAttribute.Disposed -= compilerGeneratedAttribute_Disposed;
                this.compilerGeneratedAttribute = null;
            }
        }


        private void task_Disposed(object sender, EventArgs e)
        {
            if (this.task != null)
            {
#if DEBUG
                Debug.WriteLine("Task Disposed.");
#endif
                this.task.Disposed -= task_Disposed;
                this.task = null;
            }
        }

        private void taskOfT_Disposed(object sender, EventArgs e)
        {
            if (this.taskOfT != null)
            {
#if DEBUG
                Debug.WriteLine("Task<TResult> Disposed.");
#endif
                this.taskOfT.Disposed -= taskOfT_Disposed;
                this.taskOfT = null;
            }
        }

        private void editorBrowsableAttribute_Disposed(object sender, EventArgs e)
        {
            if (this.editorBrowsableAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("EditorBrowsableAttribute Disposed.");
#endif
                this.editorBrowsableAttribute.Disposed -= editorBrowsableAttribute_Disposed;
                this.editorBrowsableAttribute = null;
            }
        }

        private void classIsModuleAttribute_Disposed(object sender, EventArgs e)
        {
            if (this.classIsModuleAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("ClassIsModuleAttribute Disposed.");
#endif
                this.classIsModuleAttribute.Disposed -= classIsModuleAttribute_Disposed;
                this.classIsModuleAttribute = null;
            }
        }

        private void classIsHiddenAttribute_Disposed(object sender, EventArgs e)
        {
            if (this.classIsHiddenAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("ClassIsHiddenAttribute Disposed.");
#endif
                this.classIsHiddenAttribute.Disposed -= classIsHiddenAttribute_Disposed;
                this.classIsHiddenAttribute = null;
            }
        }

        #endregion

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper
        /// for the <see cref="System.Runtime.CompilerServices.ExtensionAttribute"/>
        /// system type.
        /// </summary>
        public IClassType ExtensionAttribute
        {
            get
            {
                if (this.extensionAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("ExtensionAttribute initialized.");
#endif
                    this.extensionAttribute = (IClassType) this.manager.ObtainTypeReference(typeof(ExtensionAttribute));
                    this.extensionAttribute.Disposed += this.extensionAttribute_Disposed;
                }
                return this.extensionAttribute;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="System.ParamArrayAttribute"/>
        /// system type.
        /// </summary>
        public IClassType ParameterArrayAttribute
        {
            get
            {
                if (this.parameterArrayAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("ParameterArrayAttribute initialized.");
#endif
                    this.parameterArrayAttribute = (IClassType) this.manager.ObtainTypeReference(typeof(ParamArrayAttribute));
                    this.parameterArrayAttribute.Disposed += this.parameterArrayAttribute_Disposed;
                }

                return this.parameterArrayAttribute;
            }
        }


        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="System.Object"/>
        /// system type.
        /// </summary>
        public IClassType Object
        {
            get
            {
                if (this.@object == null)
                {
#if DEBUG
                    Debug.WriteLine("Object initialized.");
#endif
                    this.@object = (IClassType) this.manager.ObtainTypeReference(typeof(Object));
                    this.@object.Disposed += this.object_Disposed;
                }
                return this.@object;
            }
        }

        /// <summary>
        /// Returns the <see cref="IArrayType"/> reference wrapper for the <see cref="System.Object"/>
        /// system type as an array.
        /// </summary>
        public IArrayType ObjectArray
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
        public IStructType Void
        {
            get
            {
                if (this.@void == null)
                {
#if DEBUG
                    Debug.WriteLine("Void initialized.");
#endif
                    this.@void = (IStructType) this.manager.ObtainTypeReference(typeof(void));
                    this.@void.Disposed += this.void_Disposed;
                }
                return this.@void;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="ValueType"/>
        /// system type.
        /// </summary>
        public IClassType ValueType
        {
            get
            {
                if (this.valueType == null)
                {
#if DEBUG
                    Debug.WriteLine("ValueType initialized.");
#endif
                    this.valueType = (IClassType) this.manager.ObtainTypeReference(typeof(ValueType));
                    this.valueType.Disposed += this.valueType_Disposed;
                }
                return this.valueType;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="Enum"/>
        /// system type.
        /// </summary>
        public IClassType Enum
        {
            get
            {
                if (this.@enum == null)
                {
#if DEBUG
                    Debug.WriteLine("Enum initialized.");
#endif
                    this.@enum = (IClassType) this.manager.ObtainTypeReference(typeof(Enum));
                    this.@enum.Disposed += this.enum_Disposed;
                }
                return this.@enum;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="Delegate"/>
        /// system type.
        /// </summary>
        public IClassType Delegate
        {
            get
            {
                if (this.@delegate == null)
                {
#if DEBUG
                    Debug.WriteLine("Delegate initialized.");
#endif
                    this.@delegate = (IClassType) this.manager.ObtainTypeReference(typeof(Delegate));
                    this.@delegate.Disposed += this.delegate_Disposed;
                }
                return this.@delegate;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="MulticastDelegate"/>
        /// system type.
        /// </summary>
        public IClassType MulticastDelegate
        {
            get
            {
                if (this.multicastDelegate == null)
                {
#if DEBUG
                    Debug.WriteLine("MulticastDelegate initialized.");
#endif
                    this.multicastDelegate = (IClassType) this.manager.ObtainTypeReference(typeof(MulticastDelegate));
                    this.multicastDelegate.Disposed += this.multicastDelegate_Disposed;
                }
                return this.multicastDelegate;
            }
        }


        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="System.String"/>
        /// system type.
        /// </summary>
        public IClassType String
        {
            get
            {
                if (this.@string == null)
                {
#if DEBUG
                    Debug.WriteLine("String initialized.");
#endif
                    this.@string = (IClassType) this.manager.ObtainTypeReference(typeof(string));
                    this.@string.Disposed += string_Disposed;
                }
                return this.@string;
            }
        }


        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the
        /// <see cref="System.Runtime.CompilerServices.CompilerGeneratedAttribute"/>
        /// system type.
        /// </summary>
        public IClassType CompilerGeneratedAttribute
        {
            get
            {
                if (this.compilerGeneratedAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("CompilerGeneratedAttribute initialized.");
#endif
                    this.compilerGeneratedAttribute = (IClassType) this.manager.ObtainTypeReference(typeof(CompilerGeneratedAttribute));
                    this.compilerGeneratedAttribute.Disposed += compilerGeneratedAttribute_Disposed;
                }
                return this.compilerGeneratedAttribute;
            }
        }

        /// <summary>
        /// Returns the <see cref="IArrayType"/> reference wrapper for the <see cref="System.String"/>
        /// system type as an array.
        /// </summary>
        public IArrayType StringArray
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
        public IStructType Boolean
        {
            get
            {
                if (this.boolean == null)
                {
#if DEBUG
                    Debug.WriteLine("Boolean initialized.");
#endif
                    this.boolean = (IStructType) this.manager.ObtainTypeReference(typeof(bool));
                    this.boolean.Disposed += new EventHandler(boolean_Disposed);
                }
                return this.boolean;
            }
        }

        /// <summary>
        /// Returns the <see cref="IArrayType"/> reference wrapper for the <see cref="System.Boolean"/>
        /// system type as an array.
        /// </summary>
        public IArrayType BooleanArray
        {
            get
            {
                return Boolean.MakeArray();
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="Task"/>
        /// system type.
        /// </summary>
        public IClassType Task
        {
            get
            {
                if (this.task == null)
                {
#if DEBUG
                    Debug.WriteLine("Task initialized");
#endif
                    this.task = (IClassType) this.manager.ObtainTypeReference(typeof(Task));
                    this.task.Disposed += new EventHandler(task_Disposed);
                }
                return task;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="Task{T}"/>
        /// system type.
        /// </summary>
        public IClassType TaskOfT
        {
            get
            {
                if (this.taskOfT == null)
                {
#if DEBUG
                    Debug.WriteLine("Task<TResult> initialized");
#endif
                    this.taskOfT = (IClassType)this.manager.ObtainTypeReference(typeof(Task<>));
                    this.taskOfT.Disposed += new EventHandler(taskOfT_Disposed);
                }
                return taskOfT;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the
        /// <see cref="EditorBrowsableAttribute"/> system type.
        /// </summary>
        public IClassType EditorBrowsableAttribute
        {
            get
            {
                if (this.editorBrowsableAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("EditorBrowsableAttribute initialized");
#endif
                    this.editorBrowsableAttribute = (IClassType) this.manager.ObtainTypeReference(typeof(EditorBrowsableAttribute));
                    this.editorBrowsableAttribute.Disposed += new EventHandler(editorBrowsableAttribute_Disposed);
                }
                return this.editorBrowsableAttribute;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the 
        /// <see cref="ClassIsModuleAttribute"/> metadata type.
        /// </summary>
        public IClassType ClassIsModuleAttribute
        {
            get
            {
                if (this.classIsModuleAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("ClassIsModuleAttribute initialized");
#endif
                    this.classIsModuleAttribute = (IClassType) this.manager.ObtainTypeReference(typeof(ClassIsModuleAttribute));
                    this.classIsModuleAttribute.Disposed += new EventHandler(classIsModuleAttribute_Disposed);
                }
                return this.classIsModuleAttribute;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the 
        /// <see cref="ClassIsHiddenAttribute"/> metadata type.
        /// </summary>
        public IClassType ClassIsHiddenAttribute
        {
            get
            {
                if (this.classIsHiddenAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("ClassIsHiddenAttribute initialized");
#endif
                    this.classIsHiddenAttribute = (IClassType) this.manager.ObtainTypeReference(typeof(ClassIsHiddenAttribute));
                    this.classIsHiddenAttribute.Disposed += new EventHandler(classIsHiddenAttribute_Disposed);
                }
                return this.classIsHiddenAttribute;
            }
        }

    }
}
