using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using AllenCopeland.Abstraction.OldCodeGen;
using AllenCopeland.Abstraction.OldCodeGen.Types.Members;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace CliMetadataReader
{
    public class MetadataTableEncoding<T> :
        Dictionary<T, MetadataTable>,
        IMetadataTableFieldDataType,
        IMetadataTableFieldEncodingDataType
    {

        /// <summary>
        /// Returns the <see cref="String"/> value representing
        /// the name of the encoding.
        /// </summary>
        public string EncodingName { get; private set; }

        public MetadataTableEncoding(string name)
        {
            this.EncodingName = name;
        }

        public override string ToString()
        {
            return this.EncodingName;
        }

        #region IEnumerable<MetadataTable> Members

        IEnumerator<MetadataTable> IEnumerable<MetadataTable>.GetEnumerator()
        {
            foreach (var value in this.Values)
                yield return value;
        }

        #endregion


        #region IMetadataTableFieldEncodingDataType Members

        public ITypeReference EncodingType
        {
            get { return typeof(T).GetTypeReference(); }
        }

        #endregion

        #region IMetadataTableFieldEncodingDataType Members


        string IMetadataTableFieldEncodingDataType.Name
        {
            get { return this.EncodingName; }
        }

        public IStatementBlockLocalMember WordSizeLocal { get; set; }

        bool IMetadataTableFieldEncodingDataType.Contains(MetadataTable target) { return this.Values.Contains(target); }

        #endregion

        public byte BitEncodingSize
        {
            get
            {
                return (byte)(Math.Ceiling(Math.Log(this.Count) / Math.Log(2)));
            }
        }


        #region IMetadataTableFieldDataType Members

        public FieldDataKind DataKind
        {
            get { return FieldDataKind.Encoding; }
        }

        public IInterfaceType EncodingCommonType { get; set; }

        #endregion


        #region IMetadataTableFieldEncodingDataType Members


        IEnumerable<Tuple<IExpression, MetadataTable>> IMetadataTableFieldEncodingDataType.Values
        {
            get {
                var typeReference = this.EncodingType.GetTypeExpression();
                foreach (var key in this.Keys)
                    yield return new Tuple<IExpression, MetadataTable>(typeReference.GetField(key.ToString()), this[key]);

            }
        }

        #endregion
    }
}
