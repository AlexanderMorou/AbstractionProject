using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    partial class IntermediateClassType<TInstanceIntermediateType>
    {
        public partial class ImplementedInterfacesDictionary :
            ImplementedInterfacesDictionary<IClassEventMember, IIntermediateClassEventMember, IClassIndexerMember, IIntermediateClassIndexerMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IClassType, IIntermediateClassType>,
            IIntermediateClassImplementedInterfaces
        {
            public ImplementedInterfacesDictionary(TInstanceIntermediateType owner)
                : base(owner)
            {
            }

            #region IIntermediateClassImplementedInterfaces Members

            public new IIntermediateClassInterfaceMapping ImplementInterface(IInterfaceType @interface, bool insertPlaceholders = false)
            {
                return (IIntermediateClassInterfaceMapping)base.ImplementInterface(@interface, insertPlaceholders);
            }

            public new IIntermediateClassInterfaceMapping this[IInterfaceType @interface]
            {
                get { return (IIntermediateClassInterfaceMapping)base[@interface]; }
            }

            #endregion

            protected override IIntermediateInterfaceMemberMapping<IClassEventMember, IIntermediateClassEventMember, IClassIndexerMember, IIntermediateClassIndexerMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IClassType, IIntermediateClassType> OnCreateMemberMapping(IInterfaceType @interface, bool insertPlaceholders)
            {
                throw new NotImplementedException();
            }
        }
    }
}
