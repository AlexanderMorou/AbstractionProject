using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil.Properties;
namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateDynamicHandler
    {
        private class MethodDictionary :
            IntermediateMethodMemberDictionary<IIntermediateDynamicMethod, IIntermediateDynamicMethod, IIntermediateDynamicHandler, IIntermediateDynamicHandler>
        {

            private new IntermediateDynamicHandler Parent { get { return (IntermediateDynamicHandler)base.Parent; } }

            internal MethodDictionary(IIntermediateDynamicHandler parent)
                : base(null, parent)
            {
            }

            protected override IIntermediateDynamicMethod OnGetNewMethod(string name)
            {
                return new IntermediateDynamicMethod(name, this.Parent);
            }

        }
    }
}
