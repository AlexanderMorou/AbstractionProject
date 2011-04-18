using System;
using AllenCopeland.Abstraction.Slf.Abstract;
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
        private IDataSizeType fieldType;
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

        public override IType FieldType
        {
            get
            {
                return this.fieldType;
            }
            set
            {
                throw new InvalidOperationException("Cannot alter the field type of a field which stores information within the .sdata of the PE.");
            }
        }

        private DataSizeType _FieldType
        {
            get
            {
                return this.fieldType as DataSizeType;
            }
        }

        /// <summary>
        /// Returns/sets the <see cref="Byte"/> array which denotes
        /// the information associated to the field in the .sdata portion
        /// of the PE.
        /// </summary>
        public Byte[] Data
        {
            get
            {
                return this.data;
            }
            set
            {
                PrivateImplementationDetails piDetails = this.PIDetails;
                if (value==null)
                    throw new ArgumentNullException("data cannot be null");
                if (value.Length == 0)
                    throw new ArgumentException("data cannot be empty.");
                if (this._FieldType != null)
                {
                    if (this.data != null &&
                        this.data.Length != value.Length)
                    {
                        this._FieldType.RemoveReference();
                        if (!this._FieldType.IsNeeded)
                        {
                            if (piDetails != null)
                                piDetails.KillSizeDataType(this.data.Length);
                        }
                    }
                    this.fieldType = null;
                }
                this.data = value;
                if (piDetails != null)
                {
                    this.fieldType = piDetails.GetSizeDataType(data.Length);
                    /* *
                     * Guaranteed to be a DataSizeType.
                     * */
                    this._FieldType.AddReference();
                }
            }
        }

        private PrivateImplementationDetails PIDetails
        {
            get
            {
                if (this.Parent == null)
                    return null;
                if (this.Parent.Parent == null)
                    return null;
                return this.Parent.Parent.PrivateImplementationDetails as PrivateImplementationDetails;
            }
        }
    }
}
