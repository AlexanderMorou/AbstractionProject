using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
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
        /// <param name="statement">The <see cref="IEnumerationBlockStatement"/> to visit.</param>
        void Visit(IEnumerationBlockStatement statement);
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
        /// Visits the comment <paramref name="statement"/> provided.
        /// </summary>
        /// <param name="statement">The <see cref="ICommentStatement"/>
        /// to visit.</param>
        void Visit(ICommentStatement statement);
    }
}
