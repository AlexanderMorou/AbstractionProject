using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Oil;
using System.Reflection.Emit;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public partial class IntermediateCodeDynamicCompiler :
        IIntermediateCodeDynamicCompiler
    {
        private ICompilerOptions options;
        private IIntermediateCodeDynamicCompilerAid aid;
        internal Dictionary<IIntermediateType, TypeBuilder> activeTypes = new Dictionary<IIntermediateType, TypeBuilder>();

        public IntermediateCodeDynamicCompiler()
        {
            this.ActiveTypes = new ControlledStateDictionary<IIntermediateType, TypeBuilder>(this.activeTypes);
        }

        #region IIntermediateCodeDynamicCompiler Members

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
        public IControlledStateDictionary<IIntermediateType, TypeBuilder> ActiveTypes { get; private set; }

        #endregion

        #region ICompiler<IIntermediateCodeDynamicCompilerAid,IIntermediateCodeDynamicCompilerOptions> Members

        public IIntermediateCodeDynamicCompilerAid Aid
        {
            get
            {
                if (this.aid == null)
                    this.aid = new IntermediateCodeDynamicCompilerAid(this);
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

        public ICompilerResults Compile(IIntermediateAssembly assembly)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
