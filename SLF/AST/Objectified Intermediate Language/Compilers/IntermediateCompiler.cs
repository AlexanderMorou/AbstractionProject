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

namespace AllenCopeland.Abstraction.Slf.Compilers
{
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
            return this.Compile(source);
        }

        public virtual ICompilerResults Compile(IIntermediateAssembly source)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
