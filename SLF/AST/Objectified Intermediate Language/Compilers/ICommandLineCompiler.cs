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
    /// Defines properties and methods for working with a command line type compiler.
    /// </summary>
    public interface ICommandLineCompiler :
        ICompiler
    {
        /// <summary>
        /// Instruct the command line compiler to process the command sequence itself
        /// with the <paramref name="arguments"/> provided.
        /// </summary>
        /// <param name="arguments">A series of <see cref="String"/> elements
        /// of compiler commands.</param>
        /// <returns>A <see cref="ICommandLineCompilerResults"/> file that indicates the results
        /// of the action.</returns>
        ICommandLineCompilerResults Main(params string[] arguments);
        /// <summary>
        /// Instructs the command line compiler to begin accepting argument commands.
        /// </summary>
        void BeginArgumentSeries();
        /// <summary>
        /// Instructs the command line compiler to end accepting argument commands.
        /// </summary>
        void EndArgumentSeries();
        /// <summary>
        /// Instruct the command line compiler to process the command sequence inserted
        /// by <see cref="BeginArgumentSeries"/> and <see cref="EndArgumentSeries"/>.
        /// </summary>
        /// <returns>A <see cref="ICommandLineCompilerResults"/> file that indicates the results
        /// of the action.</returns>
        ICommandLineCompilerResults Main();
        /// <summary>
        /// Injects a source file command into the <see cref="ICommandLineCompiler"/>.
        /// </summary>
        /// <param name="sourceFile">The file to inject as a source file command.</param>
        /// <exception cref="System.InvalidOperationException">thrown when the <see cref="ICommandLineCompiler.State"/>
        /// is not <see cref="CommandLineCompilerState.AcceptingCommands"/>
        /// </exception>
        void InjectSourceFileCommand(string sourceFile);
        /// <summary>
        /// Injects a command to reference a resource file.
        /// </summary>
        /// <param name="resourceFile">The path to the resource file to inject.</param>
        /// <exception cref="System.InvalidOperationException">thrown when the <see cref="ICommandLineCompiler.State"/>
        /// is not <see cref="CommandLineCompilerState.AcceptingCommands"/>
        /// </exception>
        void InjectResourceFileCommand(string resourceFile);
        /// <summary>
        /// Injects a command to reference an assembly.
        /// </summary>
        /// <param name="assemblyPath">The <see cref="String"/> path to 
        /// the assembly to reference.</param>
        /// <exception cref="System.InvalidOperationException">thrown when the <see cref="ICommandLineCompiler.State"/>
        /// is not <see cref="CommandLineCompilerState.AcceptingCommands"/>
        /// </exception>
        void InjectReferenceCommand(string assemblyPath);
        /// <summary>
        /// Injects a command that determines whether the assembly should be
        /// optimized.
        /// </summary>
        /// <param name="optimize">Whether or not to optimize the code of the resulting assembly.</param>
        /// <exception cref="System.InvalidOperationException">thrown when the <see cref="ICommandLineCompiler.State"/>
        /// is not <see cref="CommandLineCompilerState.AcceptingCommands"/>
        /// </exception>
        void InjectOptimizationCommand(bool optimize);
        /// <summary>
        /// Injects a command that defines the level of debugger support
        /// the resulted assembly should have.
        /// </summary>
        /// <param name="supportLevel">The level of <see cref="DebugSupport"/>.</param>
        void InjectDebugInfoCommand(DebugSupport supportLevel);
        /// <summary>
        /// Injects a command that delineates a new module is to be started from here onwards
        /// until the next module command or the end of sequence is reached.
        /// </summary>
        /// <param name="moduleName">The <see cref="String"/> name of the module</param>
        /// <param name="moduleKind">The type of module.</param>
        /// <exception cref="System.InvalidOperationException">thrown when the <see cref="ICommandLineCompiler.State"/>
        /// is not <see cref="CommandLineCompilerState.AcceptingCommands"/>
        /// </exception>
        void InjectNewModuleStartCommand(string moduleName, AssemblyOutputType moduleKind);
        /// <summary>
        /// Injects a command that instructs the compiler where the entry point is.
        /// </summary>
        /// <param name="typeName">The name of the type in the code which indicates where the
        /// entry point method is.</param>
        /// <exception cref="System.InvalidOperationException">thrown when the <see cref="ICommandLineCompiler.State"/>
        /// is not <see cref="CommandLineCompilerState.AcceptingCommands"/>
        /// </exception>
        void InjectEntryPointCommand(string typeName);
        /// <summary>
        /// Injects a command that instructs the compiler what to set the base address to.
        /// </summary>
        /// <param name="baseAddress">A <see cref="UInt32"/> value indicating the base address
        /// of the class library.</param>
        /// <remarks><para>Valid only on <see cref="AssemblyOutputType.ClassLibrary"/>
        /// assemblies.</para><para>The low-order word is rounded.</para></remarks>
        /// <exception cref="System.InvalidOperationException">thrown when the <see cref="ICommandLineCompiler.State"/>
        /// is not <see cref="CommandLineCompilerState.AcceptingCommands"/>
        /// </exception>
        void InjectBaseAddressCommand(uint baseAddress);
        /// <summary>
        /// Injects a general case command that is already pre-formatted.
        /// </summary>
        /// <param name="command">The <see cref="String"/> command to inject.</param>
        /// <exception cref="System.InvalidOperationException">thrown when the <see cref="ICommandLineCompiler.State"/>
        /// is not <see cref="CommandLineCompilerState.AcceptingCommands"/>
        /// </exception>
        void InjectCommand(string command);
        /// <summary>
        /// Returns the current state of the <see cref="CommandLineCompiler"/>.
        /// </summary>
        CommandLineCompilerState State { get; }
        /// <summary>
        /// Returns a <see cref="ICommandLineCompilerAid"/> to assist in building
        /// properly formed commands for the current <see cref="ICommandLineCompiler"/>.
        /// </summary>
        new ICommandLineCompilerAid Aid { get; }
    }
}
