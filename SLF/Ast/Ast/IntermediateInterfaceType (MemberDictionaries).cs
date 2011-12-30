using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System.Threading.Tasks;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    partial class IntermediateInterfaceType<TInstanceType>
        where TInstanceType :
            IntermediateInterfaceType<TInstanceType>
    {
        private class IndexerMembers :
            IntermediateIndexerSignatureMemberDictionary<IInterfaceIndexerMember, IIntermediateInterfaceIndexerMember, IInterfaceType, IIntermediateInterfaceType>
        {
            public IndexerMembers(IntermediateFullMemberDictionary master, TInstanceType parent)
                : base(master, parent)
            {

            }
            public IndexerMembers(IntermediateFullMemberDictionary master, TInstanceType parent, IndexerMembers root)
                : base(master, parent, root)
            {

            }

            protected override IIntermediateInterfaceIndexerMember GetNew(TypedName nameAndReturn, TypedNameSeries parameters, bool canGet = true, bool canSet = true)
            {
                var result = new IndexerMember(nameAndReturn.Name, (TInstanceType)this.Parent);
                if (parameters.Count > 0)
                {
                    TypedName[] adjustedParameters = new TypedName[parameters.Count];
                    Parallel.For(0, parameters.Count, i =>
                    {
                        TypedName currentItem = parameters[i];
                        IType paramType = currentItem.GetTypeRef();
                        if (paramType.ContainsSymbols())
                            paramType = paramType.SimpleSymbolDisambiguation(result);
                        paramType = AdjustTypeReference(paramType, currentItem.Direction);
                        adjustedParameters[i] = new TypedName(currentItem.Name, paramType);
                    });
                    result.Parameters.AddRange(adjustedParameters);
                }
                result.CanRead = canGet;
                result.CanWrite = canSet;
                return result;
            }
        }
        private static IType AdjustTypeReference(IType paramType, ParameterDirection parameterDirection)
        {
            if (paramType == null)
                return null;
            if (parameterDirection == ParameterDirection.In)
                return paramType;
            else if (paramType.ElementClassification != TypeElementClassification.Reference)
                return paramType.MakeByReference();
            return paramType;
        }

        private class PropertyMembers :
            IntermediatePropertySignatureMemberDictionary<IInterfacePropertyMember, IIntermediateInterfacePropertyMember, IInterfaceType, IIntermediateInterfaceType>
        {
            public PropertyMembers(IntermediateFullMemberDictionary master, TInstanceType parent)
                : base(master, parent)
            {

            }
            public PropertyMembers(IntermediateFullMemberDictionary master, TInstanceType parent, PropertyMembers root)
                : base(master, parent, root)
            {

            }

            protected override IIntermediateInterfacePropertyMember OnGetProperty(TypedName nameAndType)
            {
                return new PropertyMember(nameAndType.Name, (TInstanceType)this.Parent) { PropertyType = nameAndType.GetTypeRef() };
            }
        }

        private class EventMembers :
            IntermediateEventSignatureMemberDictionary<IInterfaceEventMember, IIntermediateInterfaceEventMember, IInterfaceType, IIntermediateInterfaceType>
        {
            public EventMembers(IntermediateFullMemberDictionary master, TInstanceType parent)
                : base(master, parent)
            {

            }
            public EventMembers(IntermediateFullMemberDictionary master, TInstanceType parent, EventMembers root)
                : base(master, parent, root)
            {

            }

            protected override IIntermediateInterfaceEventMember GetEvent(TypedName typedName)
            {
                if ((typedName.Source == TypedNameSource.TypeReference &&
                     typedName.TypeReference is IDelegateType) ||
                    typedName.Source == TypedNameSource.SymbolReference)
                    return new EventMember(this.Parent) { Name = typedName.Name, SignatureType = (IDelegateType)typedName.TypeReference };
                else
                    throw new ArgumentException("typedName");
            }
        }
        private class MethodMembers :
            IntermediateGroupedMethodSignatureMemberDictionary<IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>, IIntermediateMethodSignatureParameterMember<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>, IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>,
            IIntermediateMethodSignatureMemberDictionary<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>
        {

            public MethodMembers(IntermediateFullMemberDictionary master, TInstanceType parent)
                : base(master, parent)
            {
            }

            public MethodMembers(IntermediateFullMemberDictionary master, TInstanceType parent, MethodMembers root)
                : base(master, parent, root)
            {
            }

            protected override IIntermediateInterfaceMethodMember OnGetNewMethod(string name)
            {
                return new MethodMember(this.Parent) { Name = name };
            }

        }
    }
}
