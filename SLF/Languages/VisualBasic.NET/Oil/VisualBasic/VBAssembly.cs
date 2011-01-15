using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil.VisualBasic.My;
namespace AllenCopeland.Abstraction.Slf.Oil.VisualBasic
{
    /// <summary>
    /// 
    /// </summary>
    public class VBAssembly :
        IntermediateAssembly<VBAssembly>,
        IVBAssembly
    {
        public VBAssembly(string name)
            : base(name)
        {
        }

        public VBAssembly(VBAssembly root)
            : base(root)
        {
            
        }

        protected override VBAssembly GetNewPart()
        {
            return new VBAssembly(this);
        }


        #region IVBAssembly Members

        public IMyNamespaceDeclaration MyNamespace
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

    }
}
