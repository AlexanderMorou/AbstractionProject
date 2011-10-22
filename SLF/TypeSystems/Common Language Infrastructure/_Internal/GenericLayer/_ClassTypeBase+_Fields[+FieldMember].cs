using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _ClassTypeBase
    {
        private class _FieldMembersBase :
            _FieldMembersBase<IClassFieldMember, IClassType>
        {
            internal _FieldMembersBase(_FullMembersBase master, IFieldMemberDictionary<IClassFieldMember, IClassType> originalSet, _ClassTypeBase parent)
                : base(master, originalSet, parent)
            {

            }

            private class _FieldMemberBase :
                _FieldMemberBase<IClassFieldMember, IClassType>,
                IClassFieldMember
            {
                internal _FieldMemberBase(IClassFieldMember original, IClassType parent)
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

                #region IScopedDeclaration Members

                public AccessLevelModifiers AccessLevel
                {
                    get { return this.Original.AccessLevel; }
                }

                #endregion
            }


            protected override IClassFieldMember ObtainWrapper(IClassFieldMember item)
            {
                return new _FieldMemberBase(item, this.Parent);
            }
        }
    }
}
