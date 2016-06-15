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
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Blobs;

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
        private TParent parent;
        private ICliMetadataMethodSignature signature;
        private IMethodSignatureMember activeMethod;
        public CliParameterMemberDictionary(_ICliManager manager, uint methodIndex, ICliMetadataRoot metadataRoot, TParent parent, IMethodSignatureMember activeMethod)
            : this(manager, methodIndex, metadataRoot, parent)
        {
            this.activeMethod = activeMethod;
        }
        public CliParameterMemberDictionary(_ICliManager manager, uint methodIndex, ICliMetadataRoot metadataRoot, TParent parent, int dropoff = 0)
        {
            this.manager = manager;
            this.parent = parent;
            var method = metadataRoot.TableStream.MethodDefinitionTable[(int)methodIndex];
            this.signature = method.Signature;
            bool skipFirst = false;
            if (method.Parameters.Count > 0 &&
                method.Parameters[0].Sequence == 0)
                skipFirst = true;
            if (dropoff != 0)
                if (skipFirst)
                    this.Initialize(method.Parameters.Skip(1).Drop(dropoff).ToArray());
                else
                    this.Initialize(method.Parameters.Drop(dropoff).ToArray());
            else if (skipFirst)
                this.Initialize(method.Parameters.Skip(1).ToArray());
            else
                this.Initialize(method.Parameters);
        }

        //private static int DeriveCount(int methodIndex, ICliMetadataRoot metadataRoot)
        //{
        //    if (methodIndex == 0)
        //        return 0;
        //    if (metadataRoot.TableStream.MethodDefinitionTable != null)
        //    {
        //        var method = metadataRoot.TableStream.MethodDefinitionTable[methodIndex];
        //        var parameterStartIndex = method.ParameterStartIndex;
        //        var nextMethod = metadataRoot.TableStream.MethodDefinitionTable[methodIndex + 1];
        //        uint parameterCount;
        //        if (nextMethod == null)
        //            parameterCount = (uint) (metadataRoot.TableStream.ParameterTable.Count - parameterStartIndex);
        //        else
        //            parameterCount = nextMethod.ParameterStartIndex - parameterStartIndex;
        //        return (int)parameterCount;
        //    }
        //    return 0;
        //}

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

        public IControlledTypeCollection ParameterTypes
        {
            get { return new CliSignatureTypeCollection(this.manager, this.signature, ((object)this.parent) == this.activeMethod ? this.activeMethod.Parent as IType : this.parent as IType, this.activeMethod); }
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

        protected override IGeneralMemberUniqueIdentifier GetIdentifierFrom(int index, ICliMetadataParameterTableRow metadata)
        {
            return TypeSystemIdentifiers.GetMemberIdentifier(metadata.Name);
        }

    }
}
