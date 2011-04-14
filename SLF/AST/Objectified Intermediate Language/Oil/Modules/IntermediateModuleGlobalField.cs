using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Oil.Members;
/*---------------------------------------------------------------------\
| Copyright © 2011 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Modules
{
    /// <summary>
    /// Provides a base implementation of a global field
    /// defined on an intermediate module.
    /// </summary>
    public class IntermediateModuleGlobalField :
        IntermediateFieldMemberBase<IModuleGlobalField, IIntermediateModuleGlobalField, IModule, IIntermediateModule>,
        IIntermediateModuleGlobalField
    {
        private DataSizeType fieldType;
        private Byte[] data;
        /// <summary>
        /// Creates a new <see cref="IntermediateModuleGlobalField"/> instance
        /// with the <paramref name="name"/> and <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value
        /// representing the name of the <see cref="IntermediateModuleGlobalField"/>.</param>
        /// <param name="parent">The <see cref="IIntermediateModule"/> to 
        /// which the <see cref="IntermediateModuleGlobalField"/> belongs.</param>
        public IntermediateModuleGlobalField(string name, IIntermediateModule parent)
            : base(name, parent)
        {
        }

        public override Abstract.IType FieldType
        {
            get
            {
                return this.fieldType;
            }
            set
            {
                throw new InvalidOperationException("Cannot alter the field type of a field which stores information within the .sdata of the pe header.");
            }
        }

        public Byte[] Data
        {
            get
            {
                return this.data;
            }
            set
            {
                if (value==null)
                    throw new ArgumentNullException("data cannot be null");
                if (value.Length == 0)
                    throw new ArgumentException("data cannot be empty.");
                if (this.fieldType != null)
                {
                    if (this.data != null &&
                        this.data.Length != value.Length)
                    {
                        this.fieldType.RemoveReference();
                        if (!this.fieldType.IsNeeded)
                        {
                            var pidDetails = (this.Parent == null ? null : this.Parent.Parent == null ? null : this.Parent.Parent.PrivateImplementationDetails == null ? null : this.Parent.Parent.PrivateImplementationDetails) as PrivateImplementationDetails;
                            pidDetails.KillSizeDataType(this.data.Length);
                        }
                    }
                }
                this.data = value;
            }
        }
    }
}
