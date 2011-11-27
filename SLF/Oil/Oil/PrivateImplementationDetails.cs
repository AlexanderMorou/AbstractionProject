using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public class PrivateImplementationDetails :
        PrivateImplementationDetails<PrivateImplementationDetails>
    {
        public PrivateImplementationDetails(IIntermediateAssembly parent) : base(parent) { }
        public PrivateImplementationDetails(PrivateImplementationDetails root, IIntermediateAssembly parent) : base(root, parent) { }
        protected override PrivateImplementationDetails GetNewPartial(PrivateImplementationDetails root, IIntermediateTypeParent parent)
        {
            if (parent is IIntermediateAssembly)
                return new PrivateImplementationDetails(root, (IIntermediateAssembly)parent);
            else
                throw new ArgumentException("parent must be an intermediate assembly.","parent");
        }
    }
    public abstract class PrivateImplementationDetails<TInstanceIntermediateType> :
        IntermediateClassType<TInstanceIntermediateType>,
        IPrivateImplementationDetails
        where TInstanceIntermediateType :
            PrivateImplementationDetails<TInstanceIntermediateType>
    {
        private Guid detailGuid;
        private ControlledStateDictionary<int, DataSizeType> dataSizeTypes = new ControlledStateDictionary<int, DataSizeType>();
        internal PrivateImplementationDetails(IIntermediateAssembly parent)
            : base("PrivateImplementationDetails", parent)
        {
            DetailGuid = Guid.NewGuid();
        }
        protected PrivateImplementationDetails(TInstanceIntermediateType root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }

        public Guid DetailGuid
        {
            get
            {
                if (this.IsRoot)
                    return this.detailGuid;
                else
                    return this.GetRoot().DetailGuid;
            }
            set
            {
                if (this.IsRoot)
                    this.detailGuid = value;
                else
                    this.GetRoot().DetailGuid = value;
            }
        }

        protected override string OnGetName()
        {
            return string.Format(CultureInfo.CurrentCulture, "<{0}>{{{1}}}", base.OnGetName(), DetailGuid);
        }

        protected override void OnSetName(string value)
        {
            base.OnSetName(value);
        }

        #region IPrivateImplementationDetails Members

        public IAnonymousTypeDictionary AnonymousTypes
        {
            get { throw new NotImplementedException(); }
        }

        #endregion


        #region IPrivateImplementationDetails Members

        public IDataSizeType GetSizeDataType(int dataSize)
        {
            if (!dataSizeTypes.ContainsKey(dataSize))
                dataSizeTypes._Add(dataSize, new DataSizeType(this, dataSize));
            return dataSizeTypes[key: dataSize];
        }

        #endregion

        internal void KillSizeDataType(int dataSize)
        {
            if (this.dataSizeTypes.ContainsKey(dataSize))
            {
                var current = this.dataSizeTypes[key: dataSize];
                if (!current.IsNeeded)
                {
                    current.Dispose();
                    this.dataSizeTypes._Remove(key: dataSize);
                }
            }
        }

    }
}
