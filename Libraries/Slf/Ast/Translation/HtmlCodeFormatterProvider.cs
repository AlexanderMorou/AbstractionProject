using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Utilities;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Translation
{
    public class HtmlCodeFormatterProvider :
        IIntermediateCodeTranslatorFormatterProvider
    {
        public static readonly HtmlCodeFormatterProvider Singleton = new HtmlCodeFormatterProvider();

        private HtmlCodeFormatterProvider() { }

        private class Formatter :
            IIntermediateCodeTranslatorFormatter
        {
            IIntermediateCodeTranslator translator;
            private bool endingDocument;
            private MemoryStream innerStream;
            private StreamWriter streamWriter;
            private IndentedTextWriter indentedWriter;
            private Dictionary<IntermediateSpanTranslationClasses, Color> formattingColors = CreateFormattingColors();
            private Stack<IntermediateSpanTranslationClasses> spanStack = new Stack<IntermediateSpanTranslationClasses>();
            private Color backgroundColor1 = Color.FromArgb(42, 40, 34);
            private Color backgroundColor2 = Color.FromArgb(52, 50, 41);
            private Stack referenceStack = new Stack();
            private bool HasNameHandler
            {
                get
                {
                    return this.translator != null && this.translator.NameProvider != null;
                }
            }

            internal Formatter(IIntermediateCodeTranslator translator)
            {
                this.translator = translator;
            }

            private static Dictionary<IntermediateSpanTranslationClasses, Color> CreateFormattingColors()
            {
                var result = new Dictionary<IntermediateSpanTranslationClasses, Color>();
                result[IntermediateSpanTranslationClasses.Keyword] = Color.FromArgb(0x00D68052);
                result[IntermediateSpanTranslationClasses.Identifier] = Color.FromArgb(0x00D1CDC9);
                result[IntermediateSpanTranslationClasses.LiteralString] = Color.FromArgb(0x000076EC);
                result[IntermediateSpanTranslationClasses.LiteralStringAlternate] = Color.FromArgb(0x0010C2EF);
                result[IntermediateSpanTranslationClasses.LiteralNumber] = Color.FromArgb(0x0022CDFF);
                result[IntermediateSpanTranslationClasses.LiteralCharacter] = result[IntermediateSpanTranslationClasses.Keyword];
                result[IntermediateSpanTranslationClasses.Comment] = Color.FromArgb(0x007B8766);
                result[IntermediateSpanTranslationClasses.Operator] = Color.FromArgb(0x00939393);
                result[IntermediateSpanTranslationClasses.UserType] = Color.FromArgb(0x00B18C67);
                result[IntermediateSpanTranslationClasses.UserStructType] = Color.FromArgb(0x0089AA85);
                result[IntermediateSpanTranslationClasses.UserInterfaceType] = Color.FromArgb(0x00C8A0A0);
                result[IntermediateSpanTranslationClasses.UserEnumType] = Color.FromArgb(0x00D0A384);
                result[IntermediateSpanTranslationClasses.UserDelegateType] = Color.FromArgb(0x00B18C67);
                result[IntermediateSpanTranslationClasses.EventReference] = Color.FromArgb(0x00B18C67);
                result[IntermediateSpanTranslationClasses.PropertyReference] = result[IntermediateSpanTranslationClasses.UserType];
                result[IntermediateSpanTranslationClasses.FieldReference] = Color.FromArgb(0x00AAA080);
                result[IntermediateSpanTranslationClasses.ConstructorName] = result[IntermediateSpanTranslationClasses.UserStructType];
                result[IntermediateSpanTranslationClasses.GenericParameterReference] = result[IntermediateSpanTranslationClasses.UserInterfaceType];
                result[IntermediateSpanTranslationClasses.Symbol] = result[IntermediateSpanTranslationClasses.LiteralNumber];
                result[IntermediateSpanTranslationClasses.IndexerReference] = result[IntermediateSpanTranslationClasses.PropertyReference];
                result[IntermediateSpanTranslationClasses.Label] = Color.FromArgb(0x00029DCF);
                result[IntermediateSpanTranslationClasses.MethodReference] = Color.FromArgb(0x00A8A8C0);
                result[IntermediateSpanTranslationClasses.ParameterReference] = result[IntermediateSpanTranslationClasses.Identifier];
                result[IntermediateSpanTranslationClasses.LocalReference] = result[IntermediateSpanTranslationClasses.Identifier];
                return result;
            }

            private void CreateStream()
            {
                this.innerStream = new MemoryStream();
                this.streamWriter = new StreamWriter(innerStream);
                this.indentedWriter = new IndentedTextWriter(this.streamWriter);
            }

            private void DestroyStream()
            {
                this.indentedWriter.Dispose();
                this.streamWriter = null;
                this.innerStream.Dispose();
                this.innerStream = null;
            }

            #region IIntermediateCodeTranslatorFormatter Members

            public void BeginBlock(IntermediateBlockTranslationClasses blockClass)
            {
                switch (blockClass)
                {
                    case IntermediateBlockTranslationClasses.BodyBlock:
                        break;
                    case IntermediateBlockTranslationClasses.EvenLineBlock:
                        break;
                    case IntermediateBlockTranslationClasses.OddLineBlock:
                        break;
                    case IntermediateBlockTranslationClasses.SectionHeaderBlock:
                        break;
                    case IntermediateBlockTranslationClasses.SectionBodyBlock:
                        break;
                    default:
                        break;
                }
            }

            public void BeginSpan(IntermediateSpanTranslationClasses spanClass)
            {
                Color color;
                if (formattingColors.TryGetValue(spanClass, out color))
                    this.HandleWriteInternal(string.Format("<span style=\"color:#{0};\">", GetColorCssRgb(color)));
                this.spanStack.Push(spanClass);
            }

            private static string GetColorCssRgb(Color color)
            {
                return new byte[] { color.B, color.G, color.R }.FormatHexadecimal();
            }

            public void DenoteNewLine()
            {

                this.translator.WriteLine();
            }

            public void EndSpan()
            {
                if (spanStack.Count == 0)
                    return;
                if (formattingColors.ContainsKey(spanStack.Pop()))
                    this.HandleWriteInternal("</span>");
            }

            public IntermediateSpanTranslationClasses CurrentSpanClass { get { return this.spanStack.Count == 0 ? IntermediateSpanTranslationClasses.None : this.spanStack.Peek(); } }

            public void EndBlock()
            {
            }

            public void BeginSection()
            {
            }

            public void EndSection()
            {
            }

            public void BeginDocument(IIntermediateAssembly target)
            {
                this.CreateStream();
                this.HandleWriteInternal("<!DOCTYPE html>");
                this.HandleWriteInternal("<html>");
                this.HandleWriteLine();
                string targetFileName;
                TryGetAssemblyFileName(out targetFileName);
                if (targetFileName != null)
                    targetFileName = Path.GetFileNameWithoutExtension(targetFileName);
                else
                    targetFileName = string.Empty;
                this.HandleWriteInternal(@"    <head>
        <title>" + targetFileName + @"</title>
        <style>
            a
            {
                text-decoration:none;
            }
            a:hover.ref
            {
                text-decoration:none;
                border-bottom:1px dotted #CFCFCF;
                color:inherit;
            }
        </style>
    </head>");
                this.HandleWriteLine();
                this.HandleWriteInternal(string.Format("<body style=\"background-color:#{0};\">", GetColorCssRgb(backgroundColor1)));
                this.HandleWriteLine();
                this.HandleWriteInternal("<pre>");
                this.HandleWriteLine();
            }

            public void EndDocument()
            {
                while (this.spanStack.Count > 0)
                    this.EndSpan();
                this.HandleWriteInternal("</pre>");
                this.HandleWriteLine();
                this.HandleWriteInternal("</body>");
                this.HandleWriteLine();
                this.HandleWriteInternal("</html>");
                this.endingDocument = true;
                this.indentedWriter.Flush();
                this.innerStream.Seek(0, SeekOrigin.Begin);
                StreamReader sr = new StreamReader(this.innerStream);
                this.translator.Write(sr.ReadToEnd());
                sr.Close();
                sr.Dispose();
                this.DestroyStream();
                this.endingDocument = false;
            }

            #endregion

            #region IIntermediateDeclarationReferenceHandler Members

            private void DefineDeclaration(IIntermediateMember declaration)
            {
                if (HasNameHandler)
                {
                    this.EndSpan();
                    this.HandleWriteInternal(string.Format("<a class=\"def\" name=\"{0}\">", declaration.Accept(this.translator.NameProvider, IntermediateNameRequestDetails.ReferenceName)));
                }
            }

            public void DefineDeclaration(IIntermediateConstructorMember declaration)
            {
                this.DefineDeclaration((IIntermediateMember)declaration);
            }

            public void DefineDeclaration(IIntermediateConstructorSignatureMember declaration)
            {
                this.DefineDeclaration((IIntermediateMember)declaration);
            }

            public void DefineDeclaration(IIntermediateEventMember declaration)
            {
                this.DefineDeclaration((IIntermediateMember)declaration);
            }

            public void DefineDeclaration(IIntermediateEventSignatureMember declaration)
            {
                this.DefineDeclaration((IIntermediateMember)declaration);
            }

            public void DefineDeclaration(IIntermediateFieldMember declaration)
            {
                this.DefineDeclaration((IIntermediateMember)declaration);
            }

            public void DefineDeclaration(IIntermediateIndexerMember declaration)
            {
                this.DefineDeclaration((IIntermediateMember)declaration);
            }

            public void DefineDeclaration(IIntermediateIndexerSignatureMember declaration)
            {
                this.DefineDeclaration((IIntermediateMember)declaration);
            }

            public void DefineDeclaration(ILambdaTypeInferredExpressionParameterMember declaration)
            {
                this.DefineDeclaration((IIntermediateMember)declaration);
            }

            public void DefineDeclaration(ILinqRangeVariable declaration)
            {
                this.DefineDeclaration((IIntermediateMember)declaration);
            }

            public void DefineDeclaration(ILocalMember declaration)
            {
                this.DefineDeclaration((IIntermediateMember)declaration);
            }

            public void DefineDeclaration(IIntermediateMethodMember declaration)
            {
                this.DefineDeclaration((IIntermediateMember)declaration);
            }

            public void DefineDeclaration(IIntermediateMethodSignatureMember declaration)
            {
                this.DefineDeclaration((IIntermediateMember)declaration);
            }

            public void DefineDeclaration(IIntermediateParameterMember declaration)
            {
                this.DefineDeclaration((IIntermediateMember)declaration);
            }

            public void DefineDeclaration(IIntermediatePropertyMember declaration)
            {
                this.DefineDeclaration((IIntermediateMember)declaration);
            }

            public void DefineDeclaration(IIntermediatePropertySignatureMember declaration)
            {
                this.DefineDeclaration((IIntermediateMember)declaration);
            }

            private void DefineDeclaration(IIntermediateType declaration)
            {
                if (HasNameHandler)
                {
                    this.EndSpan();
                    this.HandleWriteInternal(string.Format("<a class=\"def\" name=\"{0}\">", declaration.Accept(this.translator.NameProvider, IntermediateNameRequestDetails.ReferenceName)));
                }
            }

            public void DefineDeclaration(IIntermediateClassType declaration)
            {
                this.DefineDeclaration((IIntermediateType)declaration);
            }

            public void DefineDeclaration(IIntermediateDelegateType declaration)
            {
                this.DefineDeclaration((IIntermediateType)declaration);
            }

            public void DefineDeclaration(IIntermediateEnumType declaration)
            {
                this.DefineDeclaration((IIntermediateType)declaration);
            }

            public void DefineDeclaration(IIntermediateInterfaceType declaration)
            {
                this.DefineDeclaration((IIntermediateType)declaration);
            }

            public void DefineDeclaration(IIntermediateStructType declaration)
            {
                this.DefineDeclaration((IIntermediateType)declaration);
            }

            public void EndDeclarationDefinition()
            {
                if (this.HasNameHandler)
                {
                    this.EndSpan();
                    this.HandleWriteInternal("</a>");
                }
            }

            public void ReferenceDeclaration(IIntermediateClassType declaration)
            {
                ReferenceDeclaration((IIntermediateType)declaration);
            }

            private void ReferenceDeclaration(IIntermediateType declaration)
            {
                if (this.HasNameHandler)
                {
                    EndSpan();
                    string name = declaration.Accept(this.translator.NameProvider, IntermediateNameRequestDetails.TargetFileName);
                    TranslateRelativePath(name);
                    referenceStack.Push(declaration);
                }
            }

            private void ReferenceDeclaration(IIntermediateMember declaration)
            {
                if (this.HasNameHandler)
                {
                    EndSpan();
                    string name = declaration.Accept(this.translator.NameProvider, IntermediateNameRequestDetails.TargetFileName);
                    TranslateRelativePath(name);
                    referenceStack.Push(declaration);
                }
            }

            private void TranslateRelativePath(string name)
            {
                if (name.StartsWith(".\\"))
                    name = name.Substring(2);
                var first = this.translator.BuildTrail.First();
                string currentOutputFilename;
                if (!name.StartsWith("#") && TryGetAssemblyFileName(out currentOutputFilename))
                {
                    var path = currentOutputFilename == null ? null : Path.GetDirectoryName(currentOutputFilename);
                    if (path != null && name.ToLower().StartsWith(path.ToLower()))
                    {
                        var subName = name.Substring(path.Length + (path == string.Empty ? 0 : 1));
                        this.HandleWriteInternal(string.Format("<a class=\"ref\" href=\"{0}\">", subName.Replace('\\', '/')));
                    }
                    else
                    {
                        var subName = name;
                        subName = @"..\".Repeat(currentOutputFilename.Count(c => c == Path.DirectorySeparatorChar)) + subName;
                        this.HandleWriteInternal(string.Format("<a class=\"ref\" href=\"{0}\">", subName.Replace('\\', '/')));
                    }
                }
                else
                    this.HandleWriteInternal(string.Format("<a class=\"ref\" href=\"{0}\">", name.Replace('\\', '/')));
            }

            public bool TryGetAssemblyFileName(out string fileName)
            {
                var first = this.translator.BuildTrail.First();
                if (first is IIntermediateAssembly)
                {
                    fileName = ((IIntermediateAssembly)first).Accept(this.translator.NameProvider, IntermediateNameRequestDetails.TargetFileName);
                    if (fileName.Substring(0, 2) == @".\")
                        fileName = fileName.Substring(2);
                    return true; 
                }
                else
                {
                    fileName = null;
                    return false;
                }
            }


            public void ReferenceDeclaration(IIntermediateConstructorMember declaration)
            {
                ReferenceDeclaration((IIntermediateMember)declaration);
            }

            public void ReferenceDeclaration(IIntermediateConstructorSignatureMember declaration)
            {
                ReferenceDeclaration((IIntermediateMember)declaration);
            }

            public void ReferenceDeclaration(IIntermediateDelegateType declaration)
            {
                ReferenceDeclaration((IIntermediateType)declaration);
            }

            public void ReferenceDeclaration(IIntermediateEnumType declaration)
            {
                ReferenceDeclaration((IIntermediateType)declaration);
            }

            public void ReferenceDeclaration(IIntermediateEventMember declaration)
            {
                ReferenceDeclaration((IIntermediateMember)declaration);
            }

            public void ReferenceDeclaration(IIntermediateEventSignatureMember declaration)
            {
                ReferenceDeclaration((IIntermediateMember)declaration);
            }

            public void ReferenceDeclaration(IIntermediateFieldMember declaration)
            {
                ReferenceDeclaration((IIntermediateMember)declaration);
            }

            public void ReferenceDeclaration(IIntermediateIndexerMember declaration)
            {
                ReferenceDeclaration((IIntermediateMember)declaration);
            }

            public void ReferenceDeclaration(IIntermediateIndexerSignatureMember declaration)
            {
                ReferenceDeclaration((IIntermediateMember)declaration);
            }

            public void ReferenceDeclaration(IIntermediateInterfaceType declaration)
            {
                ReferenceDeclaration((IIntermediateType)declaration);
            }

            public void ReferenceDeclaration(ILambdaTypeInferredExpressionParameterMember declaration)
            {
                ReferenceDeclaration((IIntermediateMember)declaration);
            }

            public void ReferenceDeclaration(ILinqRangeVariable declaration)
            {
                ReferenceDeclaration((IIntermediateMember)declaration);
            }

            public void ReferenceDeclaration(ILocalMember declaration)
            {
                ReferenceDeclaration((IIntermediateMember)declaration);
            }

            public void ReferenceDeclaration(IIntermediateMethodMember declaration)
            {
                ReferenceDeclaration((IIntermediateMember)declaration);
            }

            public void ReferenceDeclaration(IIntermediateMethodSignatureMember declaration)
            {
                ReferenceDeclaration((IIntermediateMember)declaration);
            }

            public void ReferenceDeclaration(IIntermediateParameterMember declaration)
            {
                ReferenceDeclaration((IIntermediateMember)declaration);
            }

            public void ReferenceDeclaration(IIntermediatePropertyMember declaration)
            {
                ReferenceDeclaration((IIntermediateMember)declaration);
            }

            public void ReferenceDeclaration(IIntermediatePropertySignatureMember declaration)
            {
                ReferenceDeclaration((IIntermediateMember)declaration);
            }

            public void ReferenceDeclaration(IIntermediateStructType declaration)
            {
                ReferenceDeclaration((IIntermediateType)declaration);
            }

            public void ReferenceDeclaration(IClassType declaration)
            {
                referenceStack.Push(declaration);
            }

            public void ReferenceDeclaration(IConstructorMember declaration)
            {
                referenceStack.Push(declaration);
            }

            public void ReferenceDeclaration(IDelegateType declaration)
            {
                referenceStack.Push(declaration);
            }

            public void ReferenceDeclaration(IEnumType declaration)
            {
                referenceStack.Push(declaration);
            }

            public void ReferenceDeclaration(IEventMember declaration)
            {
                referenceStack.Push(declaration);
            }

            public void ReferenceDeclaration(IEventSignatureMember declaration)
            {
                referenceStack.Push(declaration);
            }

            public void ReferenceDeclaration(IFieldMember declaration)
            {
                referenceStack.Push(declaration);
            }

            public void ReferenceDeclaration(IIndexerMember declaration)
            {
                referenceStack.Push(declaration);
            }

            public void ReferenceDeclaration(IIndexerSignatureMember declaration)
            {
                referenceStack.Push(declaration);
            }

            public void ReferenceDeclaration(IInterfaceType declaration)
            {
                referenceStack.Push(declaration);
            }

            public void ReferenceDeclaration(IMethodMember declaration)
            {
                referenceStack.Push(declaration);
            }

            public void ReferenceDeclaration(IMethodSignatureMember declaration)
            {
                referenceStack.Push(declaration);
            }

            public void ReferenceDeclaration(IParameterMember declaration)
            {
                referenceStack.Push(declaration);
            }

            public void ReferenceDeclaration(IPropertyMember declaration)
            {
                referenceStack.Push(declaration);
            }

            public void ReferenceDeclaration(IPropertySignatureMember declaration)
            {
                referenceStack.Push(declaration);
            }

            public void ReferenceDeclaration(IStructType declaration)
            {
                referenceStack.Push(declaration);
            }

            public void EndReferenceDeclaration()
            {
                EndSpan();
                var lastReference = referenceStack.Count == 0 ? null : referenceStack.Pop();
                if (lastReference == null)
                    return;
                if (lastReference is IIntermediateType || lastReference is IIntermediateMember)
                {
                    this.HandleWriteInternal("</a>");
                }
            }

            public bool HandlesWrite
            {
                get
                {
                    if (endingDocument || this.innerStream == null || !this.innerStream.CanWrite)
                        return false;
                    return true;
                }
            }

            public void Indent()
            {
                this.indentedWriter.Indent++;
            }

            public void Dedent()
            {
                this.indentedWriter.Indent--;
            }

            public void HandleWrite(string text)
            {
                this.HandleWriteInternal(text.HtmlEncode());
            }

            public void HandleWriteInternal(string text)
            {
                this.indentedWriter.Write(text);
            }

            public void HandleWriteLine()
            {
                this.indentedWriter.WriteLine();
            }

            #endregion

            public IEnumerable<Tuple<IntermediateSpanTranslationClasses, Color>> GetColors
            {
                get
                {
                    foreach (var color in formattingColors)
                        yield return Tuple.Create(color.Key, color.Value);
                }
            }

        }

        #region IIntermediateCodeTranslatorFormatterProvider Members

        public IIntermediateCodeTranslatorFormatter GetFormatterFor(IIntermediateCodeTranslator translator)
        {
            return new Formatter(translator);
        }

        #endregion

    }
}
