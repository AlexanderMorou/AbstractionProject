using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Defines properties and methods for working with an aid to a command
    /// line compiler.
    /// </summary>
    public interface ICommandLineCompilerAid :
        ICompilerAid<ICommandLineCompiler>
    {
        /// <summary>
        /// Obtains a <see cref="String"/> value representing how to associate
        /// a given file to the compile operation.
        /// </summary>
        /// <param name="file">The file to retrieve the source file command insert for.</param>
        /// <param name="options">The <see cref="ICompilerOptions"/> which helps direct the 
        /// compile process.</param>
        /// <returns></returns>
        /// <remarks>Typically used in conjunction with <see cref="GetModuleCommand("/></remarks>
        string GetSourceCommand(string file, ICompilerOptions options);
        /// <summary>
        /// Obtains a <see cref="String"/> value for the command line compiler to process
        /// the optimization preference defined in the <paramref name="options"/> provided.
        /// </summary>
        /// <param name="options">The <see cref="ICompilerOptions"/> which helps direct the 
        /// compile process.</param>
        /// <returns>A <see cref="String"/> representing the optimization command relative
        /// to the optimization preference in the <paramref name="options"/> provided.</returns>
        string GetOptimizationCommand(ICompilerOptions options);
        /// <summary>
        /// Obtains a <see cref="String"/> value for the command line compiler to process
        /// the debugging info preference defined in the <paramref name="options"/> provided.
        /// </summary>
        /// <returns>A <see cref="String"/> representing the optimization command relative
        /// to the debugging info preference in the <paramref name="options"/> provided.</returns>
        string GetDebugCommand(ICompilerOptions options);
        /// <summary>
        /// Obtains a <see cref="String"/> value for the command line compiler to use a 
        /// <paramref name="resourceFile"/>.
        /// </summary>
        /// <param name="resourceFile">A <see cref="String"/> value to a .resources file.</param>
        /// <param name="options">The <see cref="ICompilerOptions"/> which helps direct the 
        /// compile process.</param>
        /// <returns>A <see cref="String"/> which associates the <paramref name="resourceFile"/>
        /// into the command line for the compiler.</returns>
        string GetResourceCommand(string resourceFile, ICompilerOptions options);
        /// <summary>
        /// Obtains a <see cref="String"/> value for the command line compiler
        /// to use an assembly reference.
        /// </summary>
        /// <param name="reference">A <see cref="String"/> to a compiled assembly
        /// that the output assembly will reference.</param>
        /// <param name="options"></param>
        /// <returns>A <see cref="String"/> representing a reference to an assembly.</returns>
        string GetReferenceCommand(string reference, ICompilerOptions options);
        /// <summary>
        /// Obtains an array of <see cref="String"/> values to reference the <paramref name="files"/>
        /// as sources for the command line compiler to use.
        /// </summary>
        /// <param name="files">The <see cref="String"/> array of the filenames to reference.</param>
        /// <param name="options">The <see cref="ICompilerOptions"/> which helps direct the 
        /// compile process.</param>
        /// <returns>An array of <see cref="String"/> which provide reference to the compiler for
        /// the <paramref name="files"/> provided.</returns>
        string[] GetSourcesCommand(string[] files, ICompilerOptions options);
        /// <summary>
        /// Obtains an array of <see cref="String"/> values to reference the <paramref name="resourceFiles"/>
        /// as resources for the command line compiler to use.
        /// </summary>
        /// <param name="resourceFiles">The <see cref="String"/> array of the filenames to reference as resources.</param>
        /// <param name="options">The <see cref="ICompilerOptions"/> which helps direct the 
        /// compile process.</param>
        /// <returns>An array of <see cref="String"/> which provide resource reference to the compiler for
        /// the <paramref name="resourceFiles"/> provided.</returns>
        string[] GetResourcesCommand(string[] resourceFiles, ICompilerOptions options);
        /// <summary>
        /// Obtains an array of <see cref="String"/> values to link the <paramref name="references"/>
        /// for the command line compiler to use.
        /// </summary>
        /// <param name="resourceFiles">The <see cref="String"/> array of the filenames of assemblies to reference.</param>
        /// <param name="options">The <see cref="ICompilerOptions"/> which helps direct the 
        /// compile process.</param>
        /// <returns>An array of <see cref="String"/> which provide assembly reference to the compiler for
        /// the <paramref name="references"/> provided.</returns>
        string[] GetReferencesCommand(string[] references, ICompilerOptions options);
        /// <summary>
        /// Obtains a series of string values to help build a longer string for the command line compiler
        /// to understand a module.
        /// </summary>
        /// <param name="moduleName">The name of the module to create the command line series for.</param>
        /// <param name="sourceFiles">The file names of the files that make up the module.</param>
        /// <param name="resourceFiles">The resource files that are associated to the module.</param>
        /// <param name="options">The <see cref="ICompilerOptions"/> which helps direct the 
        /// compile process.</param>
        /// <returns>A series of string values that defines the module for the command line
        /// compiler.</returns>
        string[] GetModuleCommand(string moduleName, AssemblyOutputType type, string[] sourceFiles, string[] resourceFiles, ICompilerOptions options);
        /// <summary>
        /// Returns a <see cref="String"/> value instructing the compiler where to find the  
        /// entrypoint method.
        /// </summary>
        /// <param name="entryTypeName">The fullname of the type which contains the
        /// main method.</param>
        /// <param name="options">The <see cref="ICompilerOptions"/> which helps direct the 
        /// compile process.</param>
        /// <returns>A <see cref="String"/> value instructing the compiler where
        /// to obtain its entry point.</returns>
        string GetEntryPointCommand(string entryTypeName, ICompilerOptions options);
        /// <summary>
        /// Obtains a <see cref="String"/> value indicating what warning level to use.
        /// </summary>
        /// <param name="options">The <see cref="ICompilerOptions"/> which helps direct the 
        /// compile process.</param>
        /// <returns>A <see cref="String"/> relative to the current warning level in the
        /// <paramref name="options"/> provided.</returns>
        string GetSetWarningLevel(ICompilerOptions options);
        /// <summary>
        /// Obtains a <see cref="String"/> value indicating where the base address
        /// of a class library is.
        /// </summary>
        /// <param name="baseAddress">The base address to inject.</param>
        /// <param name="options">The <see cref="ICompilerOptions"/> which helps direct the 
        /// compile process.</param>
        /// <returns>A <see cref="String"/> value indicating where the base
        /// address of a class library is.</returns>
        string GetBaseAddressCommand(uint baseAddress, ICompilerOptions options);
        /// <summary>
        /// Obtains a <see cref="String"/> value indicating where to direct
        /// the assembly file(s) to.
        /// </summary>
        /// <param name="target">A <see cref="String"/> representing the 
        /// output for the current module.</param>
        /// <param name="options">The <see cref="ICompilerOptions"/> which helps direct the 
        /// compile process.</param>
        /// <returns>A <see cref="String"/> value indicating where to direct
        /// the assembly file to.</returns>
        string GetOutputCommand(string target, ICompilerOptions options);
        /// <summary>
        /// Obtains a <see cref="String"/>value indicating where to direct 
        /// the assembly file to.
        /// </summary>
        /// <param name="options">The <see cref="ICompilerOptions"/> which helps direct the 
        /// compile process.</param>
        /// <returns>A <see cref="String"/> value indicating where the assembly will be placed;
        /// uses the <paramref name="options"/> for a target.</returns>
        string GetOutputCommand(ICompilerOptions options);
    }
}
