using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Translation;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Translation
{
    public partial class CSharpProjectTranslator
    {
        private IIntermediateCodeTranslatorOptions options;
        private IIntermediateCodeNameProvider nameProvider;
        public CSharpProjectTranslator(IIntermediateCodeTranslatorOptions options)
        {
            if (options == null)
                throw new ArgumentNullException("options");
            this.options = options;
        }

        public string[] WriteProject(IIntermediateAssembly assembly, string rootFolder, string extension = ".cs")
        {
            List<string> fileTracker = new List<string>();
            Dictionary<IIntermediateAssembly, string> fileNameLookup = new Dictionary<IIntermediateAssembly, string>();
            this.nameProvider = new NameProvider(this, fileNameLookup);
            var information =
                (from part in new[] { assembly }.Concat(assembly.Parts)
                 let partFile = CSharpProjectTranslator.AssemblyFileVisitor.GetFileInfo(part, this, fileTracker)
                 where partFile.YieldsFile
                 let fnTrans = GetTranslatorAndFinalName(rootFolder, extension, partFile.FileName, part, fileNameLookup)
                 select new { Part = part, FileName = partFile.FileName, FinalName = fnTrans.Item2, Translator = fnTrans.Item1 }).ToArray();
#if !DO_NOT_WRITE_MULTITHREADED
            Parallel.ForEach(information, async assemblyFileInfo =>
#else
            foreach (var assemblyFileInfo in information)
#endif
            {
                CodeTranslator translator = assemblyFileInfo.Translator;
                var fileStream = new FileStream(assemblyFileInfo.FinalName, FileMode.Create, FileAccess.Write, FileShare.Read);
                var streamWriter = new StreamWriter(fileStream);
                IndentedTextWriter itw ;
                if (options.IndentationSpaceCount != null)
                    itw = new IndentedTextWriter(streamWriter, new string(' ', options.IndentationSpaceCount.Value));
                else
                    itw = new IndentedTextWriter(streamWriter);
                translator.Target = itw;
                translator.Translate(assemblyFileInfo.Part);
#if !DO_NOT_WRITE_MULTITHREADED
                await streamWriter.FlushAsync();
#else
                streamWriter.Flush();
#endif
                streamWriter.Dispose();
                fileStream.Close();
                fileStream.Dispose();
            }
#if !DO_NOT_WRITE_MULTITHREADED
            );
#endif
            return information.Select(anon1 => anon1.FinalName).ToArray();
        }

        private Tuple<CodeTranslator, string> GetTranslatorAndFinalName(string rootFolder, string extension, string fullPath, IIntermediateAssembly part, Dictionary<IIntermediateAssembly, string> fileNameLookup)
        {
            var translator = new CodeTranslator(this, fileNameLookup);

            string fileName = Path.GetFileName(fullPath);
            fullPath = fullPath.Substring(0, fullPath.IndexOf(fileName));
            string path = string.IsNullOrEmpty(fullPath) ? rootFolder : Path.Combine(rootFolder, Path.GetDirectoryName(fullPath));
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var preEmptFn = Path.Combine(path, fileName);
            if (options.ShortenFilenames)
            {
                if (fileName.Contains('.'))
                {
                    var split = fileName.Split('.');
                    if (split.Length > 2)
                        fileName = string.Format("{0}.(...).{1}", split[0], split[split.Length - 1]);
                }
            }
            fileName = Path.Combine(path, fileName);
            int offset = 0;
            string partialFileName = Path.Combine(fullPath, fileName);
            while (fileNameLookup.Values.Contains(partialFileName + extension))
            {
                partialFileName = string.Format("{0}-{1}", partialFileName, offset);
                fileName = string.Format("{0}-{1}", Path.Combine(path, fileName), ++offset);
            }

            fileName += extension;
            partialFileName += extension;

            fileNameLookup.Add(part, partialFileName);
            return Tuple.Create(translator, fileName);
        }

    }
}
