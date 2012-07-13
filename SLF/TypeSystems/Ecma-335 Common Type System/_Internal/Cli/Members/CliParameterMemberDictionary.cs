using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Slf.Cli.Metadata;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract class CliParameterMemberDictionary<TParent, TParameter> :
        CliMetadataDrivenDictionary<IGeneralMemberUniqueIdentifier, ICliMetadataParameterTableRow, TParameter>,
        IParameterMemberDictionary<TParent, TParameter>,
        IParameterMemberDictionary
        where TParent :
            IParameterParent<TParent, TParameter>
        where TParameter :
            class,
            IParameterMember<TParent>
    {
        private _ICliManager manager;
        private int methodIndex;
        private uint parameterStartIndex = 0;
        private ICliMetadataRoot metadataRoot;
        private TParent parent;
        private uint parameterCount;

        public CliParameterMemberDictionary(_ICliManager manager, int methodIndex, ICliMetadataRoot metadataRoot)
            : base(DeriveCount(methodIndex, metadataRoot))
        {
            this.metadataRoot = metadataRoot;
            this.methodIndex = methodIndex;
            this.manager = manager;
            var method = metadataRoot.TableStream.MethodDefinitionTable[methodIndex];
            this.parameterStartIndex = method.ParameterStartIndex;
            var nextMethod = metadataRoot.TableStream.MethodDefinitionTable[methodIndex+1];
            this.parameterCount = nextMethod.ParameterStartIndex - this.parameterStartIndex;
        }

        private static int DeriveCount(int methodIndex, ICliMetadataRoot metadataRoot)
        {
            if (methodIndex == 0)
                return 0;
            if (metadataRoot.TableStream.MethodDefinitionTable != null)
                return metadataRoot.TableStream.MethodDefinitionTable[methodIndex].Parameters.Count;
            return 0;
        }

        protected override ICliMetadataParameterTableRow GetMetadataAt(int index)
        {
            return this.metadataRoot.TableStream.ParameterTable[(int)(this.parameterStartIndex + index)];
        }

        protected override TParameter CreateElementFrom(ICliMetadataParameterTableRow metadata)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the <typeparamref name="TParent"/>
        /// which owns the <see cref="CliParameterMemberDictionary{TParent, TParameter}"/>.
        /// </summary>
        public TParent Parent { get { return this.parent; } }

        #region IParameterMemberDictionary Members

        IParameterParent IParameterMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        public ITypeCollectionBase ParameterTypes
        {
            get { return new CliSignatureTypeCollection(this.manager, this.metadataRoot.TableStream.MethodDefinitionTable[methodIndex].Signature); }
        }

        #endregion

        #region IMemberDictionary Members

        IMemberParent IMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        int IMemberDictionary.IndexOf(IMember member)
        {
            if (member is TParameter)
                return this.IndexOf((TParameter)member);
            return -1;
        }

        #endregion

    }
}
