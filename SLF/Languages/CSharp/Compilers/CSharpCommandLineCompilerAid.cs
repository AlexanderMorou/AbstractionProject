using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using AllenCopeland.Abstraction.Utilities.Arrays;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Provides a CSharp command line compiler aid for building properly formed
    /// command strings for the CSharp compiler.
    /// </summary>
    public class CSharpCommandLineCompilerAid :
        ICommandLineCompilerAid
    {
        #region ICommandLineCompilerAid Members

        public string GetSourceCommand(string file, ICompilerOptions options)
        {
            return string.Format(CultureInfo.CurrentCulture, "\"{0}\"", file);
        }

        public string GetOptimizationCommand(ICompilerOptions options)
        {
            return string.Format(CultureInfo.CurrentCulture, "/optimize:{0}", options.Optimize ? "+" : "-");
        }

        public string GetDebugCommand(ICompilerOptions options)
        {
            const string debugCommandBase = "/debug{0}";
            switch (options.DebugSupport)
            {
                case DebugSupport.Full:
                    return string.Format(CultureInfo.CurrentCulture, debugCommandBase, ":full");
                case DebugSupport.PDBOnly:
                    return string.Format(CultureInfo.CurrentCulture, debugCommandBase, ":pdbonly");
                default:
                    return string.Format(CultureInfo.CurrentCulture, debugCommandBase, "-");
            }
        }

        public string GetResourceCommand(string resourceFile, ICompilerOptions options)
        {
            return string.Format(CultureInfo.CurrentCulture, "/resource:\"{0}\"", resourceFile);
        }

        public string GetReferenceCommand(string reference, ICompilerOptions options)
        {
            return string.Format(CultureInfo.CurrentCulture, "/reference:\"{0}\"", reference);
        }

        public string[] GetSourcesCommand(string[] files, ICompilerOptions options)
        {
            if (files == null)
                throw new ArgumentNullException("files");

            string[] result = new string[files.Length];
            for (int i = 0; i < files.Length; i++)
                result[i] = GetSourceCommand(files[i], options);
            return result;
        }

        public string[] GetResourcesCommand(string[] resourceFiles, ICompilerOptions options)
        {
            if (resourceFiles == null)
                throw new ArgumentNullException("resourceFiles");

            string[] result = new string[resourceFiles.Length];
            for (int i = 0; i < resourceFiles.Length; i++)
                result[i] = GetResourceCommand(resourceFiles[i], options);
            return result;
        }

        public string[] GetReferencesCommand(string[] references, ICompilerOptions options)
        {
            if (references == null)
                throw new ArgumentNullException("references");

            string[] result = new string[references.Length];
            for (int i = 0; i < references.Length; i++)
                result[i] = GetResourceCommand(references[i], options);
            return result;
        }

        public string[] GetModuleCommand(string moduleName, AssemblyOutputType type, string[] sourceFiles, string[] resourceFiles, ICompilerOptions options)
        {
            string target = options.Target;
            if (type ==  AssemblyOutputType.Module)
                target = string.Format("{0}.{1}.dll", target.Substring(0, target.LastIndexOf('.')), moduleName);
            string moduleType = "/target:module";
            switch (type)
            {
                case AssemblyOutputType.ClassLibrary:
                    moduleType = "/target:library";
                    break;
                case AssemblyOutputType.ConsoleApplication:
                    moduleType = "/target:exe";
                    break;
                case AssemblyOutputType.WindowsApplication:
                    moduleType = "/target:winexe";
                    break;
            }
            return Tweaks.MergeArrays<string>(new string[] { moduleType, GetOutputCommand(target, options) }, GetSourcesCommand(sourceFiles, options));
        }

        public string GetEntryPointCommand(string entryTypeName, ICompilerOptions options)
        {
            if (entryTypeName == null)
                throw new ArgumentNullException("entryTypeName");
            return string.Format(CultureInfo.CurrentCulture, "/main:{0}", entryTypeName);
        }

        public string GetSetWarningLevel(ICompilerOptions options)
        {
            byte warnID = 0;
            switch (options.WarnLevel)
            {
                case WarningLevel.Level1:
                    warnID = 1;
                    break;
                case WarningLevel.Level2Full:
                case WarningLevel.Level2:
                    warnID = 2;
                    break;
                case WarningLevel.Level3Full:
                case WarningLevel.Level3:
                    warnID = 3;
                    break;
                case WarningLevel.Level4Full:
                case WarningLevel.Level4:
                    warnID = 4;
                    break;
            }
            return string.Format(CultureInfo.CurrentCulture, "/warn:{0}", warnID.ToString());
        }

        public string GetBaseAddressCommand(uint baseAddress, ICompilerOptions options)
        {
            return string.Format(CultureInfo.CurrentCulture, "/baseaddress:{0:X}", baseAddress);
        }

        public string GetOutputCommand(string target, ICompilerOptions options)
        {
            return string.Format(CultureInfo.CurrentCulture, "/out:\"{0}\"", target);
        }

        public string GetOutputCommand(ICompilerOptions options)
        {
            return GetOutputCommand(options.Target, options);
        }

        #endregion

        #region ICompilerAid<ICommandLineCompiler> Members

        public ICommandLineCompiler Compiler
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region ICompilerAid Members

        ICompiler ICompilerAid.Compiler
        {
            get { return this.Compiler; }
        }

        #endregion
    }
}
