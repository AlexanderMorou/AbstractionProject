﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using resourcesA = AllenCopeland.Abstraction.Utilities.Properties.Resources;
using resourcesB = AllenCopeland.Abstraction.Slf.Abstract.Properties.Resources;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
namespace AllenCopeland.Abstraction.Slf.Oil
{
    /* *
     * The irritating aspect of ensuring that a type acts
     * like it should.
     * */
    public class DataSizeType :
        IntermediateStructType<DataSizeType>,
        IDataSizeType
    {
        private int fieldRefCount;
        private int dataSize;
        public DataSizeType(IPrivateImplementationDetails parent, int dataSize)
            : base(parent.Parent)
        {
            //Cheap method, but effective.
            this.Lock();
        }

        protected override DataSizeType GetNewPartial(DataSizeType root, IIntermediateTypeParent parent)
        {
            //You can try to create a partial of this, but it yields null.
            return null;
        }

        protected override string OnGetName()
        {
            //{Name = "$ArrayType$xx" FullName = "$ArrayType$xx"}
            return string.Format("$ArrayType${0}", dataSize);
        }

        protected override void OnSetName(string value)
        {
            throw new InvalidOperationException();
        }

        internal void AddReference()
        {
            /* *
             * Ref counting for the sake of determining whether
             * the assembly needs a data type of this particular size.
             * */
            fieldRefCount++;
        }
        internal bool RemoveReference()
        {
            /* *
             * Whether the data size type should
             * be deleted, since no fields refer to it.
             * */
            return (--fieldRefCount) <= 0;
        }

        internal bool IsNeeded
        {
            get
            {
                return this.fieldRefCount > 0;
            }
        }

        #region IDataSizeType Members

        public int DataSize
        {
            get { return this.dataSize; }
        }

        #endregion
    }
}