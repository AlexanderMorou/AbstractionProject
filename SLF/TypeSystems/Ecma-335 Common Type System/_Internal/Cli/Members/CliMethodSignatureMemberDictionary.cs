using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract class CliMethodSignatureMemberDictionary<TSignatureParameter, TSignature, TSignatureParent> :
        CliMetadataDrivenDictionary<IGeneralGenericSignatureMemberUniqueIdentifier, int, TSignature>,
        IMethodSignatureMemberDictionary<TSignatureParameter, TSignature, TSignatureParent>,
        IMethodSignatureMemberDictionary
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            class,
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
    {
        private TSignatureParent parent;
        private CliFullMemberDictionary master;
        IGeneralGenericSignatureMemberUniqueIdentifier[] uniqueIdentifiers;
        internal CliMethodSignatureMemberDictionary(TSignatureParent parent, CliFullMemberDictionary master)
            : base()
        {
            this.parent = parent;
            this.master = master;
            var segmentedData = master.ObtainSubset<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature>(CliMemberType.Method).SplitSet();
            this.Initialize(segmentedData.Item1);
            this.uniqueIdentifiers = segmentedData.Item2;
        }

        //#region IMemberDictionary<TSignatureParent,IGeneralGenericSignatureMemberUniqueIdentifier,TSignature> Members

        public TSignatureParent Parent
        {
            get { return parent; }
        }

        //#endregion

        protected override TSignature CreateElementFrom(int index, int metadata)
        {
            return (TSignature)this.master.Values[metadata].Entry;
        }

        protected override IGeneralGenericSignatureMemberUniqueIdentifier GetIdentifierFrom(int index, int metadata)
        {
            return this.uniqueIdentifiers[metadata];
        }

        //#region IMethodSignatureMemberDictionary Members

        int IMethodSignatureMemberDictionary.IndexOf(IMethodSignatureMember method)
        {
            if (method is TSignature)
                return this.IndexOf((TSignature) method);
            return -1;
        }

        //#endregion


        //#region IMemberDictionary Members

        IMemberParent IMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        public int IndexOf(IMember member)
        {
            if (member is TSignature)
                return this.IndexOf((TSignature) member);
            return -1;
        }

        //#endregion

        IMasterDictionary ISubordinateDictionary.Master
        {
            get { return (IMasterDictionary)this.master; }
        }
    }
}
