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
                    this.options = new CompilerOptions();
                return this.options;
            }
        }
        #endregion

        #region IIntermediateCompiler<TRootNode> Members


        public abstract IHighLevelLanguage<TRootNode> Language { get; }


        public abstract IHighLevelLanguageProvider<TRootNode> Provider { get; }

        #endregion

        #region ICompiler Members

        ILanguage ICompiler.Language
        {
            get { return this.Language; }
        }

        ILanguageProvider ICompiler.Provider
        {
            get { return this.Provider; }
        }

        #endregion


    }
}
