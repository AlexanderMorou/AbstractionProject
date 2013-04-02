using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using System.Runtime.InteropServices;

namespace CliMetadataReader
{
    public class MetadataTableTypeDataType :
        IMetadataTableFieldDataType
    {
        public ITypeReference DataType { get; private set; }

        public bool SelfSufficientType { get; private set; }

        public MetadataTableTypeDataType(ITypeReference dataType)
        {
            this.DataType = dataType;
        }

        public MetadataTableTypeDataType(ITypeReference dataType, bool selfSufficient)
        {
            this.DataType = dataType;
            this.SelfSufficientType = selfSufficient;
        }

        public MetadataTableTypeDataType(ITypeReference dataType, ITypeReference castType)
        {
            this.DataType = dataType;
            this.CastType = castType;
        }

        public ITypeReference CastType { get; private set; }

        public override string ToString()
        {
            if (this.CastType != null)
                return string.Format("{1} as {0}", this.CastType, this.DataType);
            else
                return this.DataType.ToString();
        }


        #region IMetadataTableFieldDataType Members

        public FieldDataKind DataKind
        {
            get
            {
                if (this.SelfSufficientType)
                    return FieldDataKind.SelfsufficientDataType;
                else if (this.CastType != null)
                    return FieldDataKind.CastDataType;
                else
                    return FieldDataKind.DataType;
            }
        }

        #endregion

        public int GetSize()
        {
            if (this.CastType != null)
                return Marshal.SizeOf(((IExternTypeReference) this.CastType).TypeInstance.Type);
            else
                return Marshal.SizeOf(((IExternTypeReference) this.DataType).TypeInstance.Type);
        }
    }
}
