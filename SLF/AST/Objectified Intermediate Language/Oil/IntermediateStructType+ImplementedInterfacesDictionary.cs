using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Oil
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

            protected override IIntermediateSignatureMemberMapping<IStructEventMember, IInterfaceEventMember, IIntermediateStructEventMember, IIntermediateInterfaceEventMember, IStructIndexerMember, IInterfaceIndexerMember, IIntermediateStructIndexerMember, IIntermediateInterfaceIndexerMember, IStructMethodMember, IInterfaceMethodMember, IIntermediateStructMethodMember, IIntermediateInterfaceMethodMember, IStructPropertyMember, IInterfacePropertyMember, IIntermediateStructPropertyMember, IIntermediateInterfacePropertyMember, IStructType, IInterfaceType, IIntermediateStructType, IIntermediateInterfaceType> OnCreateMemberMapping(IInterfaceType @interface, bool insertPlaceholders)
            {
                throw new NotImplementedException();
            }
        }
    }
}
