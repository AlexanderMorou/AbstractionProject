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
    public interface IIntermediateDeclarationReferenceHandler
    {
        /// <summary>
        /// Creates the necessary spans to denote the declaration of an intermediate
        /// class type.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateClassType"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(IIntermediateClassType declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of an intermediate
        /// constructor member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateConstructorMember"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(IIntermediateConstructorMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of an intermediate
        /// constructor signature member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateConstructorSignatureMember"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(IIntermediateConstructorSignatureMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of an intermediate
        /// delegate type.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateDelegateType"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(IIntermediateDelegateType declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of an intermediate
        /// enumeration type.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateEnumType"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(IIntermediateEnumType declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of an intermediate
        /// event member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateEventMember"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(IIntermediateEventMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of an intermediate
        /// event signature member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateEventSignatureMember"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(IIntermediateEventSignatureMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of an intermediate
        /// field member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateFieldMember"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(IIntermediateFieldMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of an intermediate
        /// indexer member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateIndexerMember"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(IIntermediateIndexerMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of an intermediate
        /// indexer signature member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateIndexerSignatureMember"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(IIntermediateIndexerSignatureMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of an intermediate
        /// interface type.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateInterfaceType"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(IIntermediateInterfaceType declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of an intermediate
        /// parameter member.
        /// </summary>
        /// <param name="declaration">The <see cref="ILambdaTypeInferredExpressionParameterMember"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(ILambdaTypeInferredExpressionParameterMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of a language integrated
        /// query range variable.
        /// </summary>
        /// <param name="declaration">The <see cref="ILinqRangeVariable"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(ILinqRangeVariable declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of a
        /// local variable.
        /// </summary>
        /// <param name="declaration">The <see cref="ILocalMember"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(ILocalMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of an intermediate
        /// method member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateMethodMember"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(IIntermediateMethodMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of an intermediate
        /// method signature member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateMethodSignatureMember"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(IIntermediateMethodSignatureMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of an intermediate
        /// parameter member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateParameterMember"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(IIntermediateParameterMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of an intermediate
        /// property member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediatePropertyMember"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(IIntermediatePropertyMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of an intermediate
        /// property signature member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediatePropertySignatureMember"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(IIntermediatePropertySignatureMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the declaration of an intermediate
        /// structure type.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateStructType"/>
        /// to denote the declaration of.</param>
        void DefineDeclaration(IIntermediateStructType declaration);
        void EndDeclarationDefinition();

        /// <summary>
        /// Creates the necessary spans to denote the reference to an intermediate
        /// class type.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateClassType"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIntermediateClassType declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an intermediate
        /// constructor member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateConstructorMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIntermediateConstructorMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an intermediate
        /// constructor member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateConstructorSignatureMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIntermediateConstructorSignatureMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an intermediate
        /// delegate type.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateDelegateType"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIntermediateDelegateType declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an intermediate
        /// enumeration type.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateEnumType"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIntermediateEnumType declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an intermediate
        /// event member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateEventMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIntermediateEventMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an intermediate
        /// event signature member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateEventSignatureMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIntermediateEventSignatureMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an intermediate
        /// field member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateFieldMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIntermediateFieldMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an intermediate
        /// indexer member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateIndexerMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIntermediateIndexerMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an intermediate
        /// indexer signature member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateIndexerSignatureMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIntermediateIndexerSignatureMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an intermediate
        /// interface type.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateInterfaceType"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIntermediateInterfaceType declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an intermediate
        /// parameter member.
        /// </summary>
        /// <param name="declaration">The <see cref="ILambdaTypeInferredExpressionParameterMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(ILambdaTypeInferredExpressionParameterMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to a language integrated
        /// query range variable.
        /// </summary>
        /// <param name="declaration">The <see cref="ILinqRangeVariable"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(ILinqRangeVariable declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to a local
        /// variable.
        /// </summary>
        /// <param name="declaration">The <see cref="ILocalMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(ILocalMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an intermediate
        /// method member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateMethodMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIntermediateMethodMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an intermediate
        /// method member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateMethodSignatureMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIntermediateMethodSignatureMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an intermediate
        /// parameter member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateParameterMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIntermediateParameterMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an intermediate
        /// property member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediatePropertyMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIntermediatePropertyMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an intermediate
        /// property signature member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediatePropertySignatureMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIntermediatePropertySignatureMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an intermediate
        /// structure type.
        /// </summary>
        /// <param name="declaration">The <see cref="IIntermediateStructType"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIntermediateStructType declaration);

        /// <summary>
        /// Creates the necessary spans to denote the reference to a
        /// class type.
        /// </summary>
        /// <param name="declaration">The <see cref="IClassType"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IClassType declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to a
        /// constructor member.
        /// </summary>
        /// <param name="declaration">The <see cref="IConstructorMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IConstructorMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to a
        /// delegate type.
        /// </summary>
        /// <param name="declaration">The <see cref="IDelegateType"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IDelegateType declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to a
        /// enumeration type.
        /// </summary>
        /// <param name="declaration">The <see cref="IEnumType"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IEnumType declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an
        /// event member.
        /// </summary>
        /// <param name="declaration">The <see cref="IEventMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IEventMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an
        /// event signature member.
        /// </summary>
        /// <param name="declaration">The <see cref="IEventSignatureMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IEventSignatureMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to a
        /// field member.
        /// </summary>
        /// <param name="declaration">The <see cref="IFieldMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IFieldMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an
        /// indexer member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIndexerMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIndexerMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to an
        /// indexer signature member.
        /// </summary>
        /// <param name="declaration">The <see cref="IIndexerSignatureMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IIndexerSignatureMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to a
        /// interface type.
        /// </summary>
        /// <param name="declaration">The <see cref="IInterfaceType"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IInterfaceType declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to a
        /// method member.
        /// </summary>
        /// <param name="declaration">The <see cref="IMethodMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IMethodMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to a
        /// method signature member.
        /// </summary>
        /// <param name="declaration">The <see cref="IMethodSignatureMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IMethodSignatureMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to a
        /// parameter member.
        /// </summary>
        /// <param name="declaration">The <see cref="IParameterMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IParameterMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to a
        /// property member.
        /// </summary>
        /// <param name="declaration">The <see cref="IPropertyMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IPropertyMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to a
        /// property signature member.
        /// </summary>
        /// <param name="declaration">The <see cref="IPropertySignatureMember"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IPropertySignatureMember declaration);
        /// <summary>
        /// Creates the necessary spans to denote the reference to a
        /// structure type.
        /// </summary>
        /// <param name="declaration">The <see cref="IStructType"/>
        /// to denote a reference to.</param>
        void ReferenceDeclaration(IStructType declaration);
        void EndReferenceDeclaration();
    }
}
