using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
/*---------------------------------------------------------------------\
| Copyright © 2010 Allen Copeland Jr.                                  |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _StructTypeBase
    {
        private class _FieldMembersBase :
            _FieldMembersBase<IStructFieldMember, IStructType>
        {
            internal _FieldMembersBase(_FullMembersBase master, IFieldMemberDictionary<IStructFieldMember, IStructType> originalSet, _StructTypeBase parent)
                : base(master, originalSet, parent)
            {

            }

            private class _FieldMemberBase :
                _FieldMemberBase<IStructFieldMember, IStructType>,
                IStructFieldMember
            {
                internal _FieldMemberBase(IStructFieldMember original, IStructType parent)
                    : base(original, parent)
                {
                }

                #region IInstanceMember Members

                public InstanceMemberFlags InstanceFlags
                {
                    get { return Original.InstanceFlags; }
                }

                public bool IsStatic
                {
                    get { return Original.IsStatic; }
                }

                public bool IsHideBySignature
                {
                    get { return Original.IsHideBySignature; }
                }

                #endregion

                public override string UniqueIdentifier
                {
                    get { return this.Name; }
                }

                #region IScopedDeclaration Members

                public AccessLevelModifiers AccessLevel
                {
                    get { return this.Original.AccessLevel; }
                }

                #endregion
            }


            protected override IStructFieldMember ObtainWrapper(IStructFieldMember item)
            {
                return new _FieldMemberBase(item, this.Parent);
            }
        }
    }
}
