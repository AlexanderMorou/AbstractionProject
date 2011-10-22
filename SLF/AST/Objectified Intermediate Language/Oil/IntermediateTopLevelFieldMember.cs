using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public class IntermediateTopLevelFieldMember :
        IntermediateFieldMemberBase<ITopLevelFieldMember, IIntermediateTopLevelFieldMember, INamespaceParent, IIntermediateNamespaceParent>,
        IIntermediateTopLevelFieldMember
    {
        private IIntermediateModule declaringModule;
        public IntermediateTopLevelFieldMember(string name, IIntermediateNamespaceParent parent)
            : base(name, parent)
        {

        }

        #region IIntermediateTopLevelFieldMember Members

        public IIntermediateModule DeclaringModule
        {
            get
            {
                return this.declaringModule;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                if (value.Parent == null)
                    throw new ArgumentException("Module parent cannot be null.", "value");
                if (this.DeclaringModule != null)
                {
                    if (this.DeclaringModule.Parent != value.Parent)
                        throw new ArgumentException("Cannot be a different assembly.", "value");
                }
                else if (this.Parent.Assembly != value.Parent)
                    throw new ArgumentException("Cannot be from a different assembly.");
                this.declaringModule = value;
            }
        }

        #endregion

        #region ITopLevelFieldMember Members

        IModule ITopLevelFieldMember.DeclaringModule
        {
            get { return this.DeclaringModule; }
        }

        #endregion
    }
}
