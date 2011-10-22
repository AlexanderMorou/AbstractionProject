using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class CompiledParameterMemberBase<TParent> :
        ParameterMemberBase<TParent>,
        ICompiledParameterMember
        where TParent :
            IParameterParent
    {
        /// <summary>
        /// Data member for <see cref="MemberInfo"/>
        /// </summary>
        private ParameterInfo parameterInfo;
        private IGeneralMemberUniqueIdentifier uniqueIdentifier;
        protected CompiledParameterMemberBase(ParameterInfo parameterInfo, TParent parent) 
            : base(parent)
        {
            this.parameterInfo = parameterInfo;
        }

        #region ICompiledParameterMember Members

        public ParameterInfo ParameterInfo
        {
            get { return this.parameterInfo; }
        }

        #endregion

        protected override IType ParameterTypeImpl
        {
            get
            {
                return this.ParameterInfo.ParameterType.GetTypeReference();
            }
        }

        public sealed override ParameterDirection Direction
        {
            get
            {
                if (ParameterInfo.IsOut)
                    return ParameterDirection.Out;
                else if (this.ParameterType.ElementClassification == TypeElementClassification.Reference)
                    return ParameterDirection.Reference;
                else
                    return ParameterDirection.In;
            }
        }

        protected sealed override string OnGetName()
        {
            return this.ParameterInfo.Name;
        }

        public sealed override IGeneralMemberUniqueIdentifier UniqueIdentifier
        {
            get {
                if (this.uniqueIdentifier == null)
                    this.uniqueIdentifier = AstIdentifier.Member(this.Name);
                return this.uniqueIdentifier;
            }
        }
        public override string ToString()
        {
            return string.Format("{0} {1}", this.ParameterType.Name, this.Name);
        }

        public override void Dispose()
        {
            this.parameterInfo = null;
            base.Dispose();
        }

        protected override ICustomAttributeCollection InitializeCustomAttributes()
        {
            return new CompiledCustomAttributeCollection(this.parameterInfo.GetCustomAttributes);
        }
    }
}
