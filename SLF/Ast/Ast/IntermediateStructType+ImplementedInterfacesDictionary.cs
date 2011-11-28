using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    partial class IntermediateStructType<TInstanceIntermediateType>
    {
        public partial class ImplementedInterfacesDictionary :
            ImplementedInterfacesDictionary<IStructEventMember, IIntermediateStructEventMember, IStructIndexerMember, IIntermediateStructIndexerMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IStructType, IIntermediateStructType>,
            IIntermediateStructImplementedInterfaces
        {
            public ImplementedInterfacesDictionary(TInstanceIntermediateType owner)
                : base(owner)
            {
            }

            #region IIntermediateStructImplementedInterfaces Members

            public new IIntermediateStructInterfaceMapping ImplementInterface(IInterfaceType @interface, bool insertPlaceholders = false)
            {
                return (IIntermediateStructInterfaceMapping)base.ImplementInterface(@interface, insertPlaceholders);
            }

            public new IIntermediateStructInterfaceMapping this[IInterfaceType @interface]
            {
                get { return (IIntermediateStructInterfaceMapping)base[@interface]; }
            }

            #endregion

            protected override IIntermediateInterfaceMemberMapping<IStructEventMember, IIntermediateStructEventMember, IStructIndexerMember, IIntermediateStructIndexerMember, IStructMethodMember, IIntermediateStructMethodMember, IStructPropertyMember, IIntermediateStructPropertyMember, IStructType, IIntermediateStructType> OnCreateMemberMapping(IInterfaceType @interface, bool insertPlaceholders)
            {
                throw new NotImplementedException();
            }
        }
    }
}
