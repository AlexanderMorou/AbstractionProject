using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil.VisualBasic.My;
using AllenCopeland.Abstraction.Slf.Languages;
namespace AllenCopeland.Abstraction.Slf.Oil.VisualBasic
{
    /// <summary>
    /// 
    /// </summary>
    public class VisualBasicAssembly :
        IntermediateAssembly<VisualBasicAssembly>,
        IVisualBasicAssembly
    {
        private IVisualBasicProvider provider;
        public VisualBasicAssembly(string name)
            : this(name, VisualBasicLanguage.Singleton.GetProvider())
        {
        }

        private VisualBasicAssembly(VisualBasicAssembly root)
            : base(root)
        {
            
        }

        internal protected VisualBasicAssembly(string name, IVisualBasicProvider provider)
            : base(name)
        {
            this.provider = provider;
        }

        protected override VisualBasicAssembly GetNewPart()
        {
            return new VisualBasicAssembly(this);
        }


        #region IVisualBasicAssembly Members

        public IMyNamespaceDeclaration MyNamespace
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IIntermediateAssembly<IVisualBasicLanguage,IVisualBasicStart,IVisualBasicProvider> Members

        public IVisualBasicLanguage Language
        {
            get { return this.Provider.Language; }
        }

        public IVisualBasicProvider Provider
        {
            get {
                if (this.IsRoot)
                    return this.provider;
                else
                    return this.GetRoot().provider;
            }
        }

        #endregion

        static VisualBasicAssembly()
        {
            VisualBasicAssemblyBridge.Register();
        }
    }
}
