using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    partial class IntermediateDelegateType
    {
        /// <summary>
        /// Provides a parameter member dictionary for a delegate type.
        /// </summary>
        public sealed class ParameterDictionary :
            IntermediateParameterMemberDictionary<IDelegateType, IIntermediateDelegateType, IDelegateTypeParameterMember, IIntermediateDelegateTypeParameterMember>,
            IIntermediateDelegateTypeParameterDictionary
        {
            internal ParameterDictionary(IntermediateDelegateType parent)
                : base(parent)
            {
            }

            protected override IIntermediateDelegateTypeParameterMember GetNewParameter(string name, IType parameterType, ParameterCoercionDirection direction)
            {
                ParameterMember result = new ParameterMember(Parent) { Direction = direction, ParameterType = parameterType };
                result.AssignName(name);
                return result;
            }

            private new IntermediateDelegateType Parent
            {
                get
                {
                    return (IntermediateDelegateType)base.Parent;
                }
            }
        }

        internal new partial class TypeParameterDictionary :
            IntermediateGenericTypeBase<IGeneralGenericTypeUniqueIdentifier, IDelegateType, IIntermediateDelegateType>.TypeParameterDictionary,
            IIntermediateDelegateTypeParameterTypeDictionary
        {
            internal TypeParameterDictionary(IntermediateDelegateType parent)
                : base(parent)
            {
            }

            #region IIntermediateDelegateTypeParameterTypeDictionary Members

            public new IIntermediateDelegateTypeParameterType Add(string name)
            {
                return (IIntermediateDelegateTypeParameterType)base.Add(name);
            }

            public new IIntermediateDelegateTypeParameterType Add(GenericParameterData genericParameterData)
            {
                return (IIntermediateDelegateTypeParameterType)base.Add(genericParameterData);
            }

            public new IIntermediateDelegateTypeParameterType[] AddRange(params GenericParameterData[] genericParameterData)
            {
                return base.AddRange(genericParameterData).Cast<IIntermediateDelegateTypeParameterType>().ToArray();
            }

            public new IIntermediateDelegateTypeParameterType this[string name]
            {
                get { return (IIntermediateDelegateTypeParameterType)base[name]; }
            }

            public new IIntermediateDelegateTypeParameterType this[IGenericParameterUniqueIdentifier uniqueIdentifier]
            {
                get { return (IIntermediateDelegateTypeParameterType)base[uniqueIdentifier]; }
            }

            #endregion

            protected override IIntermediateGenericTypeParameter<IGeneralGenericTypeUniqueIdentifier, IDelegateType, IIntermediateDelegateType> GetNew(string name)
            {
                return new TypeParameter(name, (IntermediateDelegateType)this.Parent);
            }

        }
    }
}
