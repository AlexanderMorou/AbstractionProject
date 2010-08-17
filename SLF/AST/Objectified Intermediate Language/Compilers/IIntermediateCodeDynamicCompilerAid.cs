using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Defines properties and methods for working with a compiler aid translating
    /// intermediate code via Reflection.Emit.
    /// </summary>
    public interface IIntermediateCodeDynamicCompilerAid :
        ICompilerAid<IIntermediateCodeDynamicCompiler>
    {
        /// <summary>
        /// Creates an <see cref="AssemblyBuilder"/> for the provided
        /// <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly">The <see cref="IIntermediateAssembly"/> which is being compiled.</param>
        /// <returns>A new <see cref="AssemblyBuilder"/> for the provided <paramref name="assembly"/>.</returns>
        AssemblyBuilder CreateBuilder(IIntermediateAssembly assembly);
        /// <summary>
        /// Creates a new <see cref="TypeBuilder"/> for the given <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The <see cref="IIntermediateType"/> to create a <see cref="TypeBuilder"/> for.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">thrown when the <see cref="IIntermediateCodeDynamicCompilerOptions.CurrentModule"/> is null.-or- when <paramref name="type"/> is an unknown kind of type.-or- when
        /// the <paramref name="type"/> provided is a nested type, and its parent hasn't been created yet.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="type"/> is null.</exception>
        TypeBuilder CreateTypeBuilder(IIntermediateType type);
        /// <summary>
        /// Creates a <see cref="CustomAttributeBuilder"/> for the <paramref name="attribute"/> provided.
        /// </summary>
        /// <param name="attributes">The <see cref="ICustomAttributeDefinition"/> which describes the attribute to create.</param>
        /// <returns></returns>
        CustomAttributeBuilder CreateCustomAttributeBuilder(ICustomAttributeDefinition attributes);
        /// <summary>
        /// Creates a <see cref="MethodBuilder"/> for the <paramref name="method"/> provided.
        /// </summary>
        /// <param name="method">The <see cref="IIntermediateMethodMember"/> which needs a <see cref="MethodBuilder"/> created for it.</param>
        /// <returns></returns>
        MethodBuilder CreateMethod(IIntermediateMethodMember method);
        MethodBuilder CreateMethod(IIntermediateMethodSignatureMember method);
        PropertyBuilder CreateProperty(IIntermediatePropertyMember property);
        PropertyBuilder CreateProperty(IIntermediatePropertySignatureMember property);
        PropertyBuilder CreateIndexer(IIntermediateIndexerMember indexer);
        PropertyBuilder CreateIndexer(IIntermediateIndexerSignatureMember indexer);
        EventBuilder CreateEvent(IIntermediateEventMember tEvent);
        EventBuilder CreateEvent(IIntermediateEventSignatureMember tEvent);
        FieldBuilder CreateField(IIntermediateFieldMember field);
        ModuleBuilder CreateModule(IIntermediateModule module);
    }
}
