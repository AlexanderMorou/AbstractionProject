using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    /// <summary>
    /// Defines properties and methods for visiting various kinds of statements.
    /// </summary>
    public interface IStatementVisitor
    {
        /// <summary>
        /// Visits the block <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IBlockStatement"/> to visit.</param>
        void Visit(IBlockStatement statement);
        /// <summary>
        /// Visits the break <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IBreakStatement"/> to visit.</param>
        void Visit(IBreakStatement statement);
        /// <summary>
        /// Visits the method call <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ICallMethodStatement"/> to visit.</param>
        void Visit(ICallMethodStatement statement);
        /// <summary>
        /// Visits the condition <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IConditionBlockStatement"/> to visit.</param>
        void Visit(IConditionBlockStatement statement);
        /// <summary>
        /// Visits the call fusion <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ICallFusionStatement"/> to visit.</param>
        void Visit(ICallFusionStatement statement);
        /// <summary>
        /// Visits the condition continuation <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IConditionContinuationStatement"/> to visit.</param>
        void Visit(IConditionContinuationStatement statement);
        /// <summary>
        /// Visits the enumeration block <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IEnumerateSetBreakableBlockStatement"/> to visit.</param>
        void Visit(IEnumerateSetBreakableBlockStatement statement);
        /// <summary>
        /// Visits the explicitly typed local variable declaration <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IExplicitlyTypedLocalVariableDeclarationStatement"/> to visit.</param>
        void Visit(IExplicitlyTypedLocalVariableDeclarationStatement statement);
        /// <summary>
        /// Visits the expression <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IExpressionStatement"/> to visit.</param>
        void Visit(IExpressionStatement statement);
        /// <summary>
        /// Visits the goto <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IGoToStatement"/> to visit.</param>
        void Visit(IGoToStatement statement);
        /// <summary>
        /// Visits the jump target <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IJumpTarget"/> to visit.</param>
        void Visit(IJumpTarget statement);
        /// <summary>
        /// Visits the iteration block <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IIterationBlockStatement"/> to visit.</param>
        void Visit(IIterationBlockStatement statement);
        /// <summary>
        /// Visits the jump <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IJumpStatement"/> to visit.</param>
        void Visit(IJumpStatement statement);
        /// <summary>
        /// Visits the label <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ILabelStatement"/> to visit.</param>
        void Visit(ILabelStatement statement);
        /// <summary>
        /// Visits the return <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IReturnStatement"/> to visit.</param>
        void Visit(IReturnStatement statement);
        /// <summary>
        /// Visits the simple iteration <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ISimpleIterationBlockStatement"/> to visit.</param>
        void Visit(ISimpleIterationBlockStatement statement);
        /// <summary>
        /// Visits the switch case block <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ISwitchCaseBlockStatement"/> to visit.</param>
        void Visit(ISwitchCaseBlockStatement statement);
        /// <summary>
        /// Visits the switch <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ISwitchStatement"/> to visit.</param>
        void Visit(ISwitchStatement statement);
        /// <summary>
        /// Visits the try <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ITryStatement"/> to visit.</param>
        void Visit(ITryStatement statement);
        /// <summary>
        /// Visits the <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ILocalDeclarationStatement"/> to visit.</param>
        void Visit(ILocalDeclarationStatement statement);
        /// <summary>
        /// Visits the change event handler <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IChangeEventHandlerStatement"/> 
        /// to visit.</param>
        void Visit(IChangeEventHandlerStatement statement);

        /// <summary>
        /// Visits the bound change event handler <paramref name="statement"/>
        /// provided.
        /// </summary>
        /// <typeparam name="TEvent">The type of event as it exists in the
        /// abstract type system.</typeparam>
        /// <typeparam name="TEventParameter">The type of parameter
        /// contained within the events.</typeparam>
        /// <typeparam name="TEventParent">The type which owns the properties
        /// in the abstract type system.</typeparam>
        /// <typeparam name="TSignatureParameter">The type of parameter used in the <typeparamref name="TSignature"/>.</typeparam>
        /// <typeparam name="TSignature">The type of signature used as a parent of <typeparamref name="TSignatureParameter"/> instances.</typeparam>
        /// <typeparam name="TSignatureParent">The parent that contains the <typeparamref name="TSignature"/> 
        /// instances.</typeparam>
        /// <param name="statement">The <see cref="IBoundChangeEventSignatureHandlerStatement{TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent}"/> to visit.</param>
        void Visit<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> statement)
            where TEvent :
                IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TEventParameter :
                IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>;
        /// <summary>
        /// Visits the comment <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ICommentStatement"/>
        /// to visit.</param>
        void Visit(ICommentStatement statement);
    }
    
    /// <summary>
    /// Defines properties and methods for visiting various kinds of statements.
    /// </summary>
    /// <typeparam name="TResult">The value returned upon visiting the expression.</typeparam>
    /// <typeparam name="TContext">The type of context passed to the visitor.</typeparam>
    public interface IStatementVisitor<TResult, TContext>
    {
        /// <summary>
        /// Visits the block <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IBlockStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IBlockStatement statement, TContext context);
        /// <summary>
        /// Visits the break <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IBreakStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IBreakStatement statement, TContext context);
        /// <summary>
        /// Visits the method call <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ICallMethodStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ICallMethodStatement statement, TContext context);
        /// <summary>
        /// Visits the condition <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IConditionBlockStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IConditionBlockStatement statement, TContext context);
        /// <summary>
        /// Visits the call fusion <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ICallFusionStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ICallFusionStatement statement, TContext context);
        /// <summary>
        /// Visits the condition continuation <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IConditionContinuationStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IConditionContinuationStatement statement, TContext context);
        /// <summary>
        /// Visits the enumeration block <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IEnumerateSetBreakableBlockStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IEnumerateSetBreakableBlockStatement statement, TContext context);
        /// <summary>
        /// Visits the explicitly typed local variable declaration <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IExplicitlyTypedLocalVariableDeclarationStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IExplicitlyTypedLocalVariableDeclarationStatement statement, TContext context);
        /// <summary>
        /// Visits the expression <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IExpressionStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IExpressionStatement statement, TContext context);
        /// <summary>
        /// Visits the goto <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IGoToStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IGoToStatement statement, TContext context);
        /// <summary>
        /// Visits the jump target <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IJumpTarget"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IJumpTarget statement, TContext context);
        /// <summary>
        /// Visits the iteration block <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IIterationBlockStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IIterationBlockStatement statement, TContext context);
        /// <summary>
        /// Visits the jump <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IJumpStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IJumpStatement statement, TContext context);
        /// <summary>
        /// Visits the label <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ILabelStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILabelStatement statement, TContext context);
        /// <summary>
        /// Visits the return <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IReturnStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IReturnStatement statement, TContext context);
        /// <summary>
        /// Visits the simple iteration <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ISimpleIterationBlockStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ISimpleIterationBlockStatement statement, TContext context);
        /// <summary>
        /// Visits the switch case block <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ISwitchCaseBlockStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ISwitchCaseBlockStatement statement, TContext context);
        /// <summary>
        /// Visits the switch <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ISwitchStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ISwitchStatement statement, TContext context);
        /// <summary>
        /// Visits the try <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ITryStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ITryStatement statement, TContext context);
        /// <summary>
        /// Visits the <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ILocalDeclarationStatement"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ILocalDeclarationStatement statement, TContext context);
        /// <summary>
        /// Visits the change event handler <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="IChangeEventHandlerStatement"/> 
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IChangeEventHandlerStatement statement, TContext context);

        /// <summary>
        /// Visits the bound change event handler <paramref name="statement"/>
        /// provided.
        /// </summary>
        /// <typeparam name="TEvent">The type of event as it exists in the
        /// abstract type system.</typeparam>
        /// <typeparam name="TEventParameter">The type of parameter
        /// contained within the events.</typeparam>
        /// <typeparam name="TEventParent">The type which owns the properties
        /// in the abstract type system.</typeparam>
        /// <typeparam name="TSignatureParameter">The type of parameter used in the <typeparamref name="TSignature"/>.</typeparam>
        /// <typeparam name="TSignature">The type of signature used as a parent of <typeparamref name="TSignatureParameter"/> instances.</typeparam>
        /// <typeparam name="TSignatureParent">The parent that contains the <typeparamref name="TSignature"/> 
        /// instances.</typeparam>
        /// <param name="statement">The <see cref="IBoundChangeEventSignatureHandlerStatement{TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent}"/> to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> statement, TContext context)
            where TEvent :
                IEventSignatureMember<TEvent, TEventParameter, TEventParent>
            where TEventParent :
                IEventSignatureParent<TEvent, TEventParameter, TEventParent>
            where TEventParameter :
                IEventSignatureParameterMember<TEvent, TEventParameter, TEventParent>
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
            where TSignatureParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>;
        /// <summary>
        /// Visits the comment <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ICommentStatement"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(ICommentStatement statement, TContext context);
    }
}
