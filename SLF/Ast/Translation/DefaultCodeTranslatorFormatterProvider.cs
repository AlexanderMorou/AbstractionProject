using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Translation
{
    public class DefaultCodeTranslatorFormatterProvider :
        IIntermediateCodeTranslatorFormatterProvider
    {
        private class DefaultCodeTranslatorFormatter :
            IIntermediateCodeTranslatorFormatter
        {
            private IIntermediateCodeTranslator translator;

            public DefaultCodeTranslatorFormatter(IIntermediateCodeTranslator translator)
            {
                this.translator = translator;
            }

            #region IIntermediateCodeTranslatorFormatter Members

            public void BeginBlock(IntermediateBlockTranslationClasses blockClass)
            {
            }

            public void BeginSpan(IntermediateSpanTranslationClasses spanClass)
            {
            }

            public void DenoteNewLine()
            {
                translator.WriteLine();
            }

            public void EndSpan()
            {
            }

            public void EndBlock()
            {
            }

            public void BeginSection()
            {
            }

            public void EndSection()
            {
            }

            public void DefineDeclaration(IIntermediateConstructorMember declaration)
            {
            }

            public void DefineDeclaration(IIntermediateConstructorSignatureMember declaration)
            {
            }

            public void DefineDeclaration(IIntermediateEventMember declaration)
            {
            }

            public void DefineDeclaration(IIntermediateEventSignatureMember declaration)
            {
            }

            public void DefineDeclaration(IIntermediateFieldMember declaration)
            {
            }

            public void DefineDeclaration(IIntermediateIndexerMember declaration)
            {
            }

            public void DefineDeclaration(IIntermediateIndexerSignatureMember declaration)
            {
            }

            public void DefineDeclaration(IIntermediateMethodMember declaration)
            {
            }

            public void DefineDeclaration(IIntermediateMethodSignatureMember declaration)
            {
            }

            public void DefineDeclaration(IIntermediatePropertyMember declaration)
            {
            }

            public void DefineDeclaration(IIntermediatePropertySignatureMember declaration)
            {
            }

            public void ReferenceDeclaration(IIntermediateConstructorMember declaration)
            {
            }

            public void ReferenceDeclaration(IIntermediateConstructorSignatureMember declaration)
            {
            }

            public void ReferenceDeclaration(IIntermediateEventMember declaration)
            {
            }

            public void ReferenceDeclaration(IIntermediateEventSignatureMember declaration)
            {
            }

            public void ReferenceDeclaration(IIntermediateFieldMember declaration)
            {
            }

            public void ReferenceDeclaration(IIntermediateIndexerMember declaration)
            {
            }

            public void ReferenceDeclaration(IIntermediateIndexerSignatureMember declaration)
            {
            }

            public void ReferenceDeclaration(IIntermediateMethodMember declaration)
            {
            }

            public void ReferenceDeclaration(IIntermediateMethodSignatureMember declaration)
            {
            }

            public void ReferenceDeclaration(IIntermediatePropertyMember declaration)
            {
            }

            public void ReferenceDeclaration(IIntermediatePropertySignatureMember declaration)
            {
            }

            public void ReferenceDeclaration(IConstructorMember declaration)
            {
            }

            public void ReferenceDeclaration(IEventMember declaration)
            {
            }

            public void ReferenceDeclaration(IEventSignatureMember declaration)
            {
            }

            public void ReferenceDeclaration(IFieldMember declaration)
            {
            }

            public void ReferenceDeclaration(IIndexerMember declaration)
            {
            }

            public void ReferenceDeclaration(IIndexerSignatureMember declaration)
            {
            }

            public void ReferenceDeclaration(IMethodMember declaration)
            {
            }

            public void ReferenceDeclaration(IMethodSignatureMember declaration)
            {
            }

            public void ReferenceDeclaration(IPropertyMember declaration)
            {
            }

            public void ReferenceDeclaration(IPropertySignatureMember declaration)
            {
            }

            public void EndDeclarationDefinition()
            {
            }

            public void EndReferenceDeclaration()
            {
            }

            public void DefineDeclaration(IIntermediateClassType declaration)
            {
            }

            public void DefineDeclaration(IIntermediateDelegateType declaration)
            {
            }

            public void DefineDeclaration(IIntermediateEnumType declaration)
            {
            }

            public void DefineDeclaration(IIntermediateInterfaceType declaration)
            {
            }

            public void DefineDeclaration(IIntermediateStructType declaration)
            {
            }

            public void ReferenceDeclaration(IIntermediateClassType declaration)
            {
            }

            public void ReferenceDeclaration(IIntermediateDelegateType declaration)
            {
            }

            public void ReferenceDeclaration(IIntermediateEnumType declaration)
            {
            }

            public void ReferenceDeclaration(IIntermediateInterfaceType declaration)
            {
            }

            public void ReferenceDeclaration(IIntermediateStructType declaration)
            {
            }

            public void ReferenceDeclaration(IClassType declaration)
            {
            }

            public void ReferenceDeclaration(IDelegateType declaration)
            {
            }

            public void ReferenceDeclaration(IEnumType declaration)
            {
            }

            public void ReferenceDeclaration(IInterfaceType declaration)
            {
            }

            public void ReferenceDeclaration(IStructType declaration)
            {
            }

            public void DefineDeclaration(ILambdaTypeInferredExpressionParameterMember declaration)
            {
            }

            public void DefineDeclaration(IIntermediateParameterMember declaration)
            {
            }

            public void ReferenceDeclaration(ILambdaTypeInferredExpressionParameterMember declaration)
            {
            }

            public void ReferenceDeclaration(IIntermediateParameterMember declaration)
            {
            }

            public void ReferenceDeclaration(IParameterMember declaration)
            {
            }

            public void DefineDeclaration(ILinqRangeVariable declaration)
            {
            }

            public void ReferenceDeclaration(ILinqRangeVariable declaration)
            {
            }

            public void DefineDeclaration(ILocalMember declaration)
            {
            }

            public void ReferenceDeclaration(ILocalMember declaration)
            {
            }

            public void BeginDocument()
            {
            }

            public void EndDocument()
            {
            }

            public void BeginDocument(IIntermediateAssembly target)
            {
                
            }

            public bool HandlesWrite
            {
                get { return false; }
            }

            public void HandleWrite(string text)
            {
                throw new NotSupportedException();
            }

            public void HandleWriteLine()
            {
                throw new NotSupportedException();
            }

            public void Indent()
            {
                throw new NotSupportedException();
            }

            public void Dedent()
            {
                throw new NotSupportedException();
            }

            #endregion


            public IntermediateSpanTranslationClasses CurrentSpanClass
            {
                get { return IntermediateSpanTranslationClasses.None; }
            }
        }

        #region IIntermediateCodeTranslatorFormatterProvider Members

        public IIntermediateCodeTranslatorFormatter GetFormatterFor(IIntermediateCodeTranslator translator)
        {
            return new DefaultCodeTranslatorFormatter(translator);
        }

        #endregion
    }
}
