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
    public class VisualBasicAssembly :
        IntermediateAssembly<VisualBasicAssembly>,
        IVisualBasicAssembly
    {
        public VisualBasicAssembly(string name)
            : base(name)
        {
        }

        private VisualBasicAssembly(VisualBasicAssembly root)
            : base(root)
        {
            
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

    }
}
