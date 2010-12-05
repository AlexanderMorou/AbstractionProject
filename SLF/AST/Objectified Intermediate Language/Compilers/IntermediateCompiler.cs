using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Oil;
using System.Reflection.Emit;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Languages;
using System.IO;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Statements;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public enum RewriteSectors
    {
        /// <summary>
        /// The expression is a lambda expression.
        /// </summary>
        /// <remarks>
        ///     C&#9839;: (Identifier | '(' Identifier (',' Identifier)* ')' | '(' TypedIdentifier (',' TypedIdentifier)* ')') "=>"
        ///         (Expression | StatementBlock)
        ///     VB: "Function" '(' TypedIdentifier (',' TypedIdentifier) ')' Expression
        /// </remarks>
        LambdaExpression = ExpressionKind.ExpansionRequiredSector.LambdaExpression,
        /// <summary>
        /// The expression is a ternary conditional operation.
        /// </summary>
        ConditionalOperation = ExpressionKind.ExpansionRequiredSector.ConditionalOperation,
        /// <summary>
        /// An expression which is merely a forward from a conditional expression.
        /// </summary>
        ConditionalForwardTerm = ExpressionKind.ExpansionRequiredSector.ConditionalForwardTerm,
        /// <summary>
        /// An series of expressions evaluated in verbatim order.
        /// </summary>
        CommaExpression = ExpressionKind.ExpansionRequiredSector.CommaExpression,
        /// <summary>
        /// An expression which is a language integrated query used to
        /// manipulate and build new sequenes.
        /// </summary>
        LinqExpression = ExpressionKind.ExpansionRequiredSector.LinqExpression,
        /// <summary>
        /// An expression which modifies a wrapped expression prior
        /// to being sent to the recipient of the wrapped expression.
        /// </summary>
        WorkspaceExpression = ExpressionKind.ExpansionRequiredSector.WorkspaceExpression,
        /// <summary>
        /// An expression which creates an array.
        /// </summary>
        /// <remarks>In the case of a rewrite, native numeric data-types
        /// are enumerated and consolidated into a byte-array and cast into a
        /// field for loading by the assembly.</remarks>
        CreateArray = ExpressionKind.ExpansionRequiredSector.CreateArray,
        /// <summary>
        /// An expression which awaits the result of an asynchronous task.
        /// </summary>
        AwaitExpression = ExpressionKind.ExpansionRequiredSector.AwaitExpression,
        /// <summary>
        /// A statement which awaits the result of an asynchronous task.
        /// </summary>
        AwaitStatement = StatementKinds.AwaitStatement,
        /// <summary>
        /// A statement which yields a series of return values as a part of
        /// an iterator state machine.
        /// </summary>
        YieldFunctionality = StatementKinds.YieldBreakStatement | StatementKinds.YieldReturnStatement,
    }
    internal class IntermediateCompiler :
        IntermediateCompiler<IConcreteNode>
    {
        public override IHighLevelLanguageProvider<IConcreteNode> Provider
        {
            get { throw new NotSupportedException(); }
        }
    }
    public abstract partial class IntermediateCompiler<TRootNode> :
        IIntermediateCompiler<TRootNode>
        where TRootNode :
            IConcreteNode
    {
        
        private ICompilerOptions options;
        private IIntermediateCompilerAid<TRootNode> aid;

        public IntermediateCompiler()
        {
            this.ActiveTypes = new ControlledStateDictionary<IIntermediateType, TypeBuilder>();
        }

        #region IIntermediateCompiler Members

        /// <summary>
        /// Returns the current <see cref="AssemblyBuilder"/> for the <see cref="IIntermediateAssembly"/> being compiled.
        /// </summary>
        public AssemblyBuilder CurrentAssembly { get; internal set; }

        /// <summary>
        /// Returns the current <see cref="ModuleBuilder"/> for the <see cref="IIntermediateModule"/> being created.
        /// </summary>
        public ModuleBuilder CurrentModule { get; internal set; }

        /// <summary>
        /// Returns the current <see cref="TypeBuilder"/> for the <see cref="IIntermediateType"/> being created.
        /// </summary>
        public TypeBuilder CurrentType { get; internal set; }

        /// <summary>
        /// Returns a dictionary containing the currently built types.
        /// </summary>
        public ControlledStateDictionary<IIntermediateType, TypeBuilder> ActiveTypes { get; private set; }

        IControlledStateDictionary<IIntermediateType, TypeBuilder> IIntermediateCompiler<TRootNode>.ActiveTypes
        {
            get
            {
                return this.ActiveTypes;
            }
        }

        #endregion

        #region ICompiler<IIntermediateCompilerAid,IIntermediateCodeDynamicCompilerOptions> Members

        public IIntermediateCompilerAid<TRootNode> Aid
        {
            get
            {
                if (this.aid == null)
                    this.aid = new IntermediateCompilerAid<TRootNode>(this);
                return this.aid;
            }
        }
        
        #endregion

        #region ICompiler Members

        ICompilerAid ICompiler.Aid
        {
            get { return this.Aid; }
        }

        public ICompilerOptions Options
        {
            get {
                if (this.options == null)
                    this.options = new CompilerOptions(this.Provider.Language);
                return this.options;
            }
        }
        #endregion

        #region IIntermediateCompiler<TRootNode> Members

        public abstract IHighLevelLanguageProvider<TRootNode> Provider { get; }

        #endregion

        #region ICompiler Members

        ILanguageProvider ICompiler.Provider
        {
            get { return this.Provider; }
        }

        #endregion

        #region IIntermediateCompiler<TRootNode> Members

        public ICompilerResults Compile(TRootNode[] source, ICompilerContext context = null)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            IIntermediateAssembly result = null;
            for (int i = 0; i < source.Length; i++) 
            {
                if (result == null)
                {
                    result = this.Provider.ASTTranslator.Process(source[i]);
                    if (context != null && !(string.IsNullOrEmpty(context.AssemblyName)))
                        result.Name = context.AssemblyName;
                    else if (this.Options.Target != null)
                        result.Name = Path.GetFileNameWithoutExtension(this.Options.Target);
                }
                else
                    this.Provider.ASTTranslator.Process(source[i], result);
            }
            if (result == null)
                throw new ArgumentException("source must contain at least one element to compile", "source");
            else
                result.References.AddRange(context.References);
            return this.Compile(source);
        }

        public virtual ICompilerResults Compile(IIntermediateAssembly source)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Determines whether the <paramref name="expansionElement"/> needs rewritten
        /// </summary>
        /// <param name="expansionElement"></param>
        /// <returns>true if the <paramref name="expansionElement"/> needs rewritten in the current
        /// context.</returns>
        protected virtual bool NeedsToRewrite(RewriteSectors expansionElement)
        {
            return true;
        }
    }
}
