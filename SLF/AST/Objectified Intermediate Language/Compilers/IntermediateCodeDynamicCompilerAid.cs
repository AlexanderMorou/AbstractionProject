using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using System.Reflection;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil.Properties;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf.Cst;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public sealed class IntermediateCompilerAid<TRootNode> :
        IIntermediateCodeDynamicCompilerAid<TRootNode>
        where TRootNode :
            IConcreteNode
    {

        internal IntermediateCompilerAid(IIntermediateCompiler<TRootNode> compiler)
        {
            this.Compiler = compiler;
        }

        #region IIntermediateCodeDynamicCompilerAid Members

        /// <summary>
        /// Creates an <see cref="AssemblyBuilder"/> for the provided
        /// <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly">The <see cref="IIntermediateAssembly"/> which is being compiled.</param>
        /// <param name="options">The <see cref="IIntermediateCodeDynamicCompilerOptions"/> which helps direct the 
        /// compile process.</param>
        /// <returns>A new <see cref="AssemblyBuilder"/> for the provided <paramref name="assembly"/>.</returns>
        public AssemblyBuilder CreateBuilder(IIntermediateAssembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");
            AssemblyName assemblyName = new AssemblyName(assembly.Name);
            AssemblyBuilder result = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save);
            return result;
        }

        /// <summary>
        /// Creates a new type for the given <paramref name="type"/> with the
        /// <paramref name="options"/>.
        /// </summary>
        /// <param name="type">The <see cref="IIntermediateType"/> to create a <see cref="TypeBuilder"/> for.</param>
        /// <param name="options">The <see cref="IIntermediateCodeDynamicCompilerOptions"/> which helps direct the 
        /// compile process.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">thrown when the <see cref="IIntermediateCodeDynamicCompilerOptions.CurrentModule"/> is null on
        /// <paramref name="options"/>.-or- when <paramref name="type"/> is an unknown kind of type.-or- when
        /// the <paramref name="type"/> provided is a nested type, and its parent hasn't been created yet.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="type"/> or
        /// <paramref name="options"/> is null.</exception>
        public TypeBuilder CreateTypeBuilder(IIntermediateType type)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (this.Compiler.CurrentModule == null)
                throw new InvalidOperationException("The CurrentModule the type belongs to is null.");
            TypeAttributes ta = TypeAttributes.Class;
            Type baseType = typeof(object);
            if (type is IIntermediateDelegateType)
                baseType = typeof(MulticastDelegate);
            else if (type is IIntermediateEnumType)
            {
                baseType = typeof(Enum);
                ta |= TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.Serializable | TypeAttributes.Sealed;
            }
            else if (type is IIntermediateInterfaceType)
                ta = TypeAttributes.Interface | TypeAttributes.Abstract | TypeAttributes.AnsiClass | TypeAttributes.AutoClass;
            else if (type is IIntermediateStructType)
            {
                baseType = typeof(ValueType);
                ta |= TypeAttributes.BeforeFieldInit | TypeAttributes.Sealed;
            }
            else if (!(type is IIntermediateClassType))
                throw new ArgumentException("type");
            else if (type.BaseType is ICompiledType)
            {
                var cBT = (ICompiledType)type.BaseType;
                baseType = cBT.UnderlyingSystemType;
            }
            bool parentIsType = type.Parent is IIntermediateType;
            switch (type.AccessLevel)
            {
                case AccessLevelModifiers.InternalProtected:
                    if (parentIsType)
                        ta |= TypeAttributes.NestedFamANDAssem;
                    else
                        ta |= TypeAttributes.NotPublic;
                    break;
                case AccessLevelModifiers.Internal:
                    if (parentIsType)
                        ta |= TypeAttributes.NestedAssembly;
                    else
                        ta |= TypeAttributes.NotPublic;
                    break;
                case AccessLevelModifiers.Private:
                    if (parentIsType)
                        ta |= TypeAttributes.NestedPrivate;
                    else
                        ta |= TypeAttributes.NotPublic;
                    break;
                case AccessLevelModifiers.PrivateScope:
                    if (parentIsType)
                        ta |= TypeAttributes.NestedPrivate;
                    else
                        ta |= TypeAttributes.NotPublic;
                    break;
                case AccessLevelModifiers.Public:
                    if (parentIsType)
                        ta |= TypeAttributes.NestedPublic;
                    else
                        ta |= TypeAttributes.Public;
                    break;
                case AccessLevelModifiers.Protected:
                    if (parentIsType)
                        ta |= TypeAttributes.NestedFamily;
                    else
                        ta |= TypeAttributes.NotPublic;
                    break;
                case AccessLevelModifiers.ProtectedInternal:
                    if (parentIsType)
                        ta |= TypeAttributes.NestedFamORAssem;
                    else
                        ta |= TypeAttributes.NotPublic;
                    break;
                default:
                    break;
            }
            if (type.Parent != null && type.Parent is IIntermediateType && !this.Compiler.ActiveTypes.ContainsKey((IIntermediateType)type.Parent))
                throw new ArgumentException("type");
            TypeBuilder result = null;
            if (type.Parent != null && type.Parent is IIntermediateType)
                result = this.Compiler.CurrentModule.DefineType(type.FullName, ta, this.Compiler.ActiveTypes[(IIntermediateType)type.Parent]);
            else
                result = this.Compiler.CurrentModule.DefineType(type.FullName, ta);
            return result;
        }

        public CustomAttributeBuilder CreateCustomAttributeBuilder(ICustomAttributeDefinition attributes)
        {
            throw new NotImplementedException();
        }

        public MethodBuilder CreateMethod(IIntermediateMethodMember method)
        {
            MethodBuilder result = null;
            MethodAttributes attributes = (MethodAttributes)0;
            bool parentIsType = method.Parent is IIntermediateType;
            switch (method.AccessLevel)
            {
                case AccessLevelModifiers.InternalProtected:
                    if (parentIsType)
                        attributes = MethodAttributes.FamANDAssem;
                    else
                        attributes = MethodAttributes.Assembly;
                    break;
                case AccessLevelModifiers.Internal:
                    attributes = MethodAttributes.Assembly;
                    break;
                case AccessLevelModifiers.Private:
                    attributes = MethodAttributes.Private;
                    break;
                case AccessLevelModifiers.PrivateScope:
                    attributes = MethodAttributes.PrivateScope;
                    break;
                case AccessLevelModifiers.Public:
                    attributes = MethodAttributes.Public;
                    break;
                case AccessLevelModifiers.Protected:
                    if (parentIsType)
                        attributes = MethodAttributes.Family;
                    else
                        attributes = MethodAttributes.Private;
                    break;
                case AccessLevelModifiers.ProtectedInternal:
                    if (parentIsType)
                        attributes = MethodAttributes.FamORAssem;
                    else
                        attributes = MethodAttributes.Assembly;
                    break;
                default:
                    attributes = MethodAttributes.PrivateScope;
                    break;
            }
            if (parentIsType)
            {
                if (this.Compiler.CurrentType == null)
                    throw new InvalidOperationException(ResourceFormatter.FormatException_InvalidOperation_CompilerState("current type null"));
                result = this.Compiler.CurrentType.DefineMethod(method.Name, attributes);
            }
            else if (method.Parent is IIntermediateModule)
            {
                if (this.Compiler.CurrentModule == null)
                    throw new InvalidOperationException(ResourceFormatter.FormatException_InvalidOperation_CompilerState("current module is null."));
                result = this.Compiler.CurrentModule.DefineGlobalMethod(method.Name, attributes, typeof(void), new Type[0]);
            }
            return result;
        }

        public MethodBuilder CreateMethod(IIntermediateMethodSignatureMember method)
        {
            throw new NotImplementedException();
        }

        public PropertyBuilder CreateProperty(IIntermediatePropertyMember property)
        {
            throw new NotImplementedException();
        }

        public PropertyBuilder CreateProperty(IIntermediatePropertySignatureMember property)
        {
            throw new NotImplementedException();
        }

        public PropertyBuilder CreateIndexer(IIntermediateIndexerMember indexer)
        {
            throw new NotImplementedException();
        }

        public PropertyBuilder CreateIndexer(IIntermediateIndexerSignatureMember indexer)
        {
            throw new NotImplementedException();
        }

        public EventBuilder CreateEvent(IIntermediateEventMember tEvent)
        {
            throw new NotImplementedException();
        }

        public EventBuilder CreateEvent(IIntermediateEventSignatureMember tEvent)
        {
            throw new NotImplementedException();
        }

        public FieldBuilder CreateField(IIntermediateFieldMember field)
        {
            throw new NotImplementedException();
        }

        public ModuleBuilder CreateModule(IIntermediateModule module)
        {
            
            throw new NotImplementedException();
        }

        #endregion

        #region ICompilerAid<IIntermediateCompiler> Members

        public IIntermediateCompiler<TRootNode> Compiler { get; private set; }

        #endregion

        #region ICompilerAid Members

        ICompiler ICompilerAid.Compiler
        {
            get { return this.Compiler; }
        }

        #endregion

    }
}
