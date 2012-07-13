using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal class CliParameterMember :
        IParameterMember
    {
        private ICliMetadataParameterTableRow metadata;
        private _ICliManager manager;

        #region IParameterMember Members

        public IParameterParent Parent
        {
            get { throw new NotImplementedException(); }
        }

        public IModifiersAndAttributesMetadata Metadata
        {
            get { throw new NotImplementedException(); }
        }

        public IType ParameterType
        {
            get { throw new NotImplementedException(); }
        }

        public ParameterDirection Direction
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IMember Members

        IMemberParent IMember.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IDeclaration Members

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public IGeneralDeclarationUniqueIdentifier UniqueIdentifier
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler Disposed;

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
