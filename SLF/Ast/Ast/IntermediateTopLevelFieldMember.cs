﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Modules;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public class IntermediateTopLevelFieldMember :
        IntermediateFieldMemberBase<ITopLevelFieldMember, IIntermediateTopLevelFieldMember, INamespaceParent, IIntermediateNamespaceParent>,
        IIntermediateTopLevelFieldMember
    {
        private IIntermediateModule declaringModule;
        private IGeneralMemberUniqueIdentifier uniqueIdentifier;
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

        public override IGeneralMemberUniqueIdentifier UniqueIdentifier
        {
            get
            {
                if (this.uniqueIdentifier == null)
                    this.uniqueIdentifier = AstIdentifier.Member(this.Name);
                return this.uniqueIdentifier;
            }
        }

        protected override void OnIdentifierChanged(IGeneralMemberUniqueIdentifier oldIdentifier, DeclarationChangeCause cause)
        {
            if (this.uniqueIdentifier != null)
                this.uniqueIdentifier = null;
            base.OnIdentifierChanged(oldIdentifier, cause);
        }
    }
}