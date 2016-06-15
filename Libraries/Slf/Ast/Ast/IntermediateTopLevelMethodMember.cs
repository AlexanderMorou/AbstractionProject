using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Modules;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public class IntermediateTopLevelMethodMember :
        IntermediateMethodMemberBase<ITopLevelMethodMember, IIntermediateTopLevelMethodMember, INamespaceParent, IIntermediateNamespaceParent>,
        IIntermediateTopLevelMethodMember
    {
        public IntermediateTopLevelMethodMember(string name, IIntermediateNamespaceParent parent)
            : base(name, parent, parent.Assembly)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private IIntermediateModule declaringModule;

        public override IIntermediateAssembly Assembly
        {
            get {
                if (this.Parent == null)
                    return null;
                return this.Parent.Assembly;
            }
        }

        protected override ITopLevelMethodMember OnMakeGenericMethod(IControlledTypeCollection genericReplacements)
        {
            return new _TopLevelMethod(this, genericReplacements);
        }

        #region IIntermediateTopLevelMethodMember Members

        public IIntermediateModule DeclaringModule
        {
            get
            {
                if (this.Parent == null)
                    return null;
                if (this.declaringModule == null)
                    return this.Assembly.ManifestModule;
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

        public string FullName
        {
            get
            {
                var nsP = Parent as INamespaceDeclaration;
                if (nsP != null)
                {
                    return string.Format("{0}.{1}", nsP.FullName, this.Name);
                }
                else
                    return this.Name;
            }
        }

        #endregion

        #region ITopLevelMethodMember Members

        IModule ITopLevelMethodMember.DeclaringModule
        {
            get { return this.DeclaringModule; }
        }

        #endregion

    }
}
