using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Translation
{
    /// <summary>
    /// Defines methods for working with a code translator formatter,
    /// which injects text associated to formatting the resulted document.
    /// </summary>
    public interface IIntermediateCodeTranslatorFormatter :
        IIntermediateDeclarationReferenceHandler
    {
        /// <summary>
        /// Denotes the start of a block of the given <paramref name="blockClass"/>.
        /// </summary>
        /// <param name="blockClass">The <see cref="IntermediateBlockTranslationClasses"/>
        /// which denotes the kind of block to start.</param>
        void BeginBlock(IntermediateBlockTranslationClasses blockClass);
        /// <summary>
        /// Denotes the start of a given span of the given <paramref name="spanClass"/>.
        /// </summary>
        /// <param name="spanClass">The <see cref="IntermediateSpanTranslationClasses"/>
        /// which denotes the kind of span to start.</param>
        void BeginSpan(IntermediateSpanTranslationClasses spanClass);
        /// <summary>
        /// Denotes the start of a new line.
        /// </summary>
        void DenoteNewLine();
        /// <summary>
        /// Denotes the end of a span.
        /// </summary>
        void EndSpan();
        /// <summary>
        /// Denotes the end of a block.
        /// </summary>
        void EndBlock();
        /// <summary>
        /// Denotes the beginning of a section.
        /// </summary>
        void BeginSection();
        /// <summary>
        /// Denotes the end of a section.
        /// </summary>
        void EndSection();
        void BeginDocument(IIntermediateAssembly target);
        void EndDocument();
        /// <summary>
        /// Returns whether the <see cref="IIntermediateCodeTranslatorFormatter"/>
        /// buffers the writes itself using its own indented text writer.
        /// </summary>
        bool HandlesWrite { get; }
        void HandleWrite(string text);
        void HandleWriteLine();
        void Indent();
        void Dedent();
        IntermediateSpanTranslationClasses CurrentSpanClass { get; }
    }
}
