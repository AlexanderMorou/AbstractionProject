using System;
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
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
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
        private bool readOnly;
        private bool constant;
        private IIntermediateModule declaringModule;
        private IGeneralMemberUniqueIdentifier uniqueIdentifier;

        /// <summary>
        /// Creates a new <see cref="IntermediateTopLevelFieldMember"/> with the
        /// <paramref name="name"/> and <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// denoting the unique name of the field.</param>
        /// <param name="parent">The <see cref="IIntermediateNamespaceParent"/> which contains the
        /// <see cref="IntermediateTopLevelFieldMember"/>.</param>
        public IntermediateTopLevelFieldMember(string name, IIntermediateNamespaceParent parent)
            : base(name, parent)
        {

        }

        #region IIntermediateTopLevelFieldMember Members

        /// <summary>
        /// The <see cref="IIntermediateModule"/> which
        /// contains the <see cref="IntermediateTopLevelFieldMember"/>.
        /// </summary>
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
                    this.uniqueIdentifier = TypeSystemIdentifiers.GetMemberIdentifier(this.Name);
                return this.uniqueIdentifier;
            }
        }

        protected override void OnIdentifierChanged(IGeneralMemberUniqueIdentifier oldIdentifier, DeclarationChangeCause cause)
        {
            if (this.uniqueIdentifier != null)
                this.uniqueIdentifier = null;
            base.OnIdentifierChanged(oldIdentifier, cause);
        }

        protected override IIntermediateIdentityManager IdentityManager
        {
            get { return this.Parent.IdentityManager; }
        }

        protected override bool OnGetReadonly()
        {
            return this.ReadOnly;
        }

        protected override bool OnGetConstant()
        {
            return this.Constant;
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateTopLevelFieldMember"/> is read-only.
        /// </summary>
        /// <remarks>Read-only fields can only be initialized during the 
        /// constructor phase of a type or instance.</remarks>
        public new bool ReadOnly
        {
            get
            {
                return this.readOnly;
            }
            set
            {
                if (value && constant)
                    constant = false;
                this.readOnly = value;
            }
        }

        /// <summary>
        /// Returns/sets whether the <see cref="IntermediateTopLevelFieldMember"/> is a constant value.
        /// </summary>
        /// <remarks>Constant values are evaluated at compile-time and folded into
        /// a single value of the appropriate data-type.</remarks>
        public new bool Constant
        {
            get
            {
                return this.constant;
            }
            set
            {
                if (value && readOnly)
                    this.readOnly = false;
                this.constant = value;
            }
        }

        protected override IIntermediateAssembly Assembly
        {
            get { return this.Parent.Assembly; }
        }
    }
}
