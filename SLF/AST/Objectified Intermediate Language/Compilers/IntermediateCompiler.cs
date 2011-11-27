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
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public enum RewriteSectors : ulong
    {
        /// <summary>
        /// The rewrites associated to a lambda expression.
        /// </summary>
        LambdaExpression = ExpressionKind.ExpansionRequiredSector.LambdaExpression,
        /// <summary>
        /// The rewrites associated to a conditional operation.
        /// </summary>
        ConditionalOperation = ExpressionKind.ExpansionRequiredSector.ConditionalOperation,
        /// <summary>
        /// The rewrite associated to a series of comma delimited expressions
        /// when viewed as a group, as opposed to the arguments to a method.
        /// </summary>
        CommaExpression = ExpressionKind.ExpansionRequiredSector.CommaExpression,
        /// <summary>
        /// The rewrite associated to a language integrated query.
        /// </summary>
        LinqExpression = ExpressionKind.ExpansionRequiredSector.LinqExpression,
        /// <summary>
        /// The rewrite associated to a workspace expression.
        /// </summary>
        WorkspaceExpression = ExpressionKind.ExpansionRequiredSector.WorkspaceExpression,
        /// <summary>
        /// The rewrite associated to a primitive array initialization expression.
        /// </summary>
        CreateArray = ExpressionKind.ExpansionRequiredSector.CreateArray,
        /// <summary>
        /// The rewrite associated to await functionality.
        /// </summary>
        AwaitFunctionality = StatementKinds.AwaitStatement | ExpressionKind.ExpansionRequiredSector.AwaitExpression,
        /// <summary>
        /// The rewrite associated to yield state machine functionality.
        /// </summary>
        YieldFunctionality = StatementKinds.YieldBreakStatement | StatementKinds.YieldReturnStatement,
        /// <summary>
        /// The rewrite associated to duck typing on type-parameters.
        /// </summary>
        DuckTyping          = 0x0000000100000000,
        /// <summary>
        /// The rewrite associated to extension methods.
        /// </summary>
        ExtensionMethods    = 0x0000000200000000,
        /// <summary>
        /// The rewrite is associated to anonymous methods
        /// </summary>
        AnonymousMethods    = 0x0000000400000000,
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
