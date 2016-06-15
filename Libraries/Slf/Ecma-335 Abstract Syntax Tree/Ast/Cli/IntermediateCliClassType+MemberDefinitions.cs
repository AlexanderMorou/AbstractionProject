using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Cli
{
    partial class IntermediateCliClassType<TInstanceIntermediateType>
    {
        protected override IntermediateClassType<TInstanceIntermediateType>.MethodMember GetNewMethod(string name)
        {
            return new MethodMember(name, (TInstanceIntermediateType)(object)this);
        }
        protected class MethodMember :
            IntermediateClassMethodMember<TInstanceIntermediateType>,
            ICliDeclaration<IGeneralGenericSignatureMemberUniqueIdentifier, ICliMetadataMethodDefinitionTableRow>
        {
            public MethodMember(TInstanceIntermediateType parent)
                : base(parent)
            {
            }

            public MethodMember(string name, TInstanceIntermediateType parent)
                : base(name, parent)
            {
            }

            protected override IntermediateParameterMemberDictionary<IClassMethodMember, IIntermediateClassMethodMember, IMethodParameterMember<IClassMethodMember, IClassType>, IIntermediateMethodParameterMember<IClassMethodMember, IIntermediateClassMethodMember, IClassType, IIntermediateClassType>> InitializeParameters()
            {
                return new ParameterDictionary(this);
            }

            protected class ParameterDictionary :
                IntermediateClassMethodMember<TInstanceIntermediateType>.ParameterDictionary
            {
                public new IntermediateClassMethodMember<TInstanceIntermediateType> Parent { get { return (MethodMember)base.Parent; } }

                public ParameterDictionary(IntermediateClassMethodMember<TInstanceIntermediateType> parent) : base(parent) { }

                protected override IIntermediateMethodParameterMember<IClassMethodMember, IIntermediateClassMethodMember, IClassType, IIntermediateClassType> GetNewParameter(string name, IType parameterType, ParameterCoercionDirection direction)
                {
                    ParameterMember result = new ParameterMember(this.Parent) { Direction = direction, ParameterType = parameterType };
                    result.AssignName(name);
                    return result;
                }
            }

            protected new class ParameterMember :
                IntermediateClassMethodMember<TInstanceIntermediateType>.ParameterMember,
                ICliDeclaration<IGeneralMemberUniqueIdentifier, ICliMetadataParameterTableRow>
            {
                public ParameterMember(IntermediateClassMethodMember<TInstanceIntermediateType> parent)
                    : base(parent)
                {
                }

                public ICliMetadataParameterMutableTableRow MetadataEntry
                {
                    get
                    {
                        return null;
                    }
                }

                ICliMetadataParameterTableRow ICliDeclaration<IGeneralMemberUniqueIdentifier, ICliMetadataParameterTableRow>.MetadataEntry
                {
                    get { return this.MetadataEntry; }
                }

                ICliMetadataTableRow ICliDeclaration.MetadataEntry
                {
                    get { return this.MetadataEntry; }
                }
            }

            public ICliMetadataMethodDefinitionMutableTableRow MetadataEntry
            {
                get
                {
                    return null;
                }
            }

            ICliMetadataMethodDefinitionTableRow ICliDeclaration<IGeneralGenericSignatureMemberUniqueIdentifier, ICliMetadataMethodDefinitionTableRow>.MetadataEntry
            {
                get { return this.MetadataEntry; }
            }

            ICliMetadataTableRow ICliDeclaration.MetadataEntry
            {
                get { return this.MetadataEntry; }
            }
        }
        protected override IntermediateClassType<TInstanceIntermediateType>.PropertyMember GetNewProperty(TypedName nameAndType)
        {
            return new PropertyMember(nameAndType.Name, (TInstanceIntermediateType)(object)this)
            {
                PropertyType = nameAndType.Source == TypedNameSource.TypeReference 
                               ? nameAndType.TypeReference 
                               : nameAndType.Source == TypedNameSource.SymbolReference 
                                 ? nameAndType.SymbolReference.GetSymbolType()
                                 : null
            };
        }
        protected class PropertyMember :
            IntermediateClassPropertyMember<TInstanceIntermediateType>,
            ICliDeclaration<IGeneralMemberUniqueIdentifier, ICliMetadataPropertyTableRow>
        {
            public PropertyMember(TInstanceIntermediateType parent)
                : base(parent)
            {
            }

            public PropertyMember(string name, TInstanceIntermediateType parent)
                : base(name, parent)
            {
            }

            public IIntermediateCliAssembly Assembly { get { return this.Parent.Assembly; } }
            public TInstanceIntermediateType Parent { get { return (TInstanceIntermediateType)base.Parent; } }

            public ICliMetadataPropertyMutableTableRow MetadataEntry
            {
                get
                {
                    return null;
                }
            }

            ICliMetadataPropertyTableRow ICliDeclaration<IGeneralMemberUniqueIdentifier, ICliMetadataPropertyTableRow>.MetadataEntry
            {
                get { return this.MetadataEntry; }
            }

            ICliMetadataTableRow ICliDeclaration.MetadataEntry
            {
                get { return this.MetadataEntry; }
            }

            protected override IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember GetMethodMember(PropertyMethodType methodType)
            {
                switch (methodType)
                {
                    case PropertyMethodType.SetMethod:
                        return new PropertySetMethodMember(this, (TInstanceIntermediateType)this.Parent);
                    default:
                    case PropertyMethodType.GetMethod:
                        return new PropertyMethodMember(PropertyMethodType.GetMethod, this, (TInstanceIntermediateType)this.Parent);
                }
            }

            protected class PropertyMethodMember :
                IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember,
                ICliDeclaration<IGeneralGenericSignatureMemberUniqueIdentifier, ICliMetadataMethodDefinitionTableRow>
            {
                public PropertyMethodMember(PropertyMethodType methodType, PropertyMember owner, TInstanceIntermediateType parent)
                    : base(methodType, owner, parent)
                {
                }
                public PropertyMember Owner { get { return (PropertyMember)base.Owner; } }
                public IIntermediateCliAssembly Assembly { get { return this.Owner.Assembly; } }

                protected override IntermediateParameterMemberDictionary<IClassMethodMember, IIntermediateClassMethodMember, IMethodParameterMember<IClassMethodMember, IClassType>, IIntermediateMethodParameterMember<IClassMethodMember, IIntermediateClassMethodMember, IClassType, IIntermediateClassType>> InitializeParameters()
                {
                    return new MethodMember.ParameterDictionary(this);
                }

                public ICliMetadataMethodDefinitionMutableTableRow MetadataEntry
                {
                    get
                    {
                        return null;
                    }
                }

                ICliMetadataMethodDefinitionTableRow ICliDeclaration<IGeneralGenericSignatureMemberUniqueIdentifier, ICliMetadataMethodDefinitionTableRow>.MetadataEntry
                {
                    get { return this.MetadataEntry; }
                }

                ICliMetadataTableRow ICliDeclaration.MetadataEntry
                {
                    get { return this.MetadataEntry; }
                }

            }

            protected class PropertySetMethodMember :
                IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertySetMethodMember,
                ICliDeclaration<IGeneralGenericSignatureMemberUniqueIdentifier, ICliMetadataMethodDefinitionTableRow>
            {
                public PropertySetMethodMember(PropertyMember owner, TInstanceIntermediateType parent)
                    : base(owner, parent)
                {
                }

                protected override IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertySetMethodMember.ValueParameterMember GetValueParameter()
                {
                    return new ValueParameterMember(this);
                }

                public PropertyMember Owner { get { return (PropertyMember)base.Owner; } }
                public IIntermediateCliAssembly Assembly { get { return this.Owner.Assembly; } }

                public ICliMetadataMethodDefinitionMutableTableRow MetadataEntry
                {
                    get
                    {
                        return null;
                    }
                }

                ICliMetadataMethodDefinitionTableRow ICliDeclaration<IGeneralGenericSignatureMemberUniqueIdentifier, ICliMetadataMethodDefinitionTableRow>.MetadataEntry
                {
                    get { return this.MetadataEntry; }
                }

                ICliMetadataTableRow ICliDeclaration.MetadataEntry
                {
                    get { return this.MetadataEntry; }
                }

                protected internal new class ValueParameterMember :
                    IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertySetMethodMember.ValueParameterMember,
                    ICliDeclaration<IGeneralMemberUniqueIdentifier, ICliMetadataParameterTableRow>
                {
                    public ValueParameterMember(PropertySetMethodMember parent)
                        : base(parent)
                    {
                    }
                    public PropertySetMethodMember Parent { get { return (PropertySetMethodMember)base.Parent; } }
                    public IIntermediateCliAssembly Assembly { get { return this.Parent.Assembly; } }

                    public ICliMetadataParameterMutableTableRow MetadataEntry
                    {
                        get
                        {
                            return null;
                        }
                    }

                    ICliMetadataParameterTableRow ICliDeclaration<IGeneralMemberUniqueIdentifier, ICliMetadataParameterTableRow>.MetadataEntry
                    {
                        get { return this.MetadataEntry; }
                    }

                    ICliMetadataTableRow ICliDeclaration.MetadataEntry
                    {
                        get { return this.MetadataEntry; }
                    }
                }
            }
        }

        protected override IntermediateClassType<TInstanceIntermediateType>.IndexerMember GetNewIndexer(TypedName nameAndReturn)
        {
            return new IndexerMember(nameAndReturn.Name, (TInstanceIntermediateType)(object)this)
            {
                PropertyType = nameAndReturn.Source == TypedNameSource.TypeReference ?
                               nameAndReturn.TypeReference :
                               nameAndReturn.Source == TypedNameSource.SymbolReference ?
                               nameAndReturn.SymbolReference.GetSymbolType() :
                               null
            };
        }

        protected class IndexerMember :
            IntermediateClassIndexerMember<TInstanceIntermediateType>,
            ICliDeclaration<IGeneralSignatureMemberUniqueIdentifier, ICliMetadataPropertyTableRow>
        {
            public IndexerMember(TInstanceIntermediateType parent)
                : base(parent)
            {
            }

            public IndexerMember(string name, TInstanceIntermediateType parent)
                : base(name, parent)
            {
            }

            public IIntermediateCliAssembly Assembly { get { return this.Parent.Assembly; } }
            public TInstanceIntermediateType Parent { get { return (TInstanceIntermediateType)base.Parent; } }

            public ICliMetadataPropertyMutableTableRow MetadataEntry
            {
                get
                {
                    return null;
                }
            }

            ICliMetadataPropertyTableRow ICliDeclaration<IGeneralSignatureMemberUniqueIdentifier, ICliMetadataPropertyTableRow>.MetadataEntry
            {
                get { return this.MetadataEntry; }
            }

            ICliMetadataTableRow ICliDeclaration.MetadataEntry
            {
                get { return this.MetadataEntry; }
            }

            protected override IntermediateParameterMemberDictionary<IClassIndexerMember, IIntermediateClassIndexerMember, IIndexerParameterMember<IClassIndexerMember, IClassType>, IIntermediateIndexerParameterMember<IClassIndexerMember, IIntermediateClassIndexerMember, IClassType, IIntermediateClassType>> InitializeParameters()
            {
                return new ParameterMemberDictionary(this);
            }

            internal override IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember.IndexerDependentParameter GetIndexerDependentParameter(IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember method, IntermediateClassIndexerMember<TInstanceIntermediateType>.ParameterMembersDictionary.ParameterMember indexerParameter)
            {
                return new IndexerMethodMember.IndexerDependentParameter(indexerParameter, method);
            }

            protected class ParameterMemberDictionary :
                ParameterMembersDictionary
            {
                public ParameterMemberDictionary(IndexerMember parent)
                    : base(parent)
                {
                }
                public IIntermediateCliAssembly Assembly { get { return this.Parent.Assembly; } }

                public new IndexerMember Parent { get { return (IndexerMember)base.Parent; } }

                protected override IIntermediateIndexerParameterMember<IClassIndexerMember, IIntermediateClassIndexerMember, IClassType, IIntermediateClassType> GetNewParameter(string name, IType parameterType, ParameterCoercionDirection direction)
                {
                    ParameterMember result = new ParameterMember(Parent) { Direction = direction, ParameterType = parameterType };
                    result.AssignName(name);
                    return result;
                }

                protected internal new class ParameterMember :
                    ParameterMembersDictionary.ParameterMember,
                    ICliDeclaration<IGeneralMemberUniqueIdentifier, ICliMetadataParameterTableRow>
                {
                    public ParameterMember(IndexerMember parent)
                        : base(parent)
                    {

                    }

                    public ICliMetadataParameterMutableTableRow MetadataEntry
                    {
                        get
                        {
                            return null;
                        }
                    }

                    ICliMetadataParameterTableRow ICliDeclaration<IGeneralMemberUniqueIdentifier, ICliMetadataParameterTableRow>.MetadataEntry
                    {
                        get { return this.MetadataEntry; }
                    }

                    ICliMetadataTableRow ICliDeclaration.MetadataEntry
                    {
                        get { return this.MetadataEntry; }
                    }

                }
            }

            protected override IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember GetMethodMember(PropertyMethodType methodType)
            {
                switch (methodType)
                {
                    case PropertyMethodType.SetMethod:
                        return new IndexerSetMethodMember(this);
                    default:
                    case PropertyMethodType.GetMethod:
                        return new IndexerMethodMember(PropertyMethodType.GetMethod, this);
                }
            }

            protected class IndexerSetMethodMember :
                IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerSetMethodMember,
                ICliDeclaration<IGeneralGenericSignatureMemberUniqueIdentifier, ICliMetadataMethodDefinitionTableRow>
            {
                public IndexerSetMethodMember(IndexerMember owner)
                    : base(owner)
                {
                }

                public new IndexerMember Owner { get { return (IndexerMember)base.Owner; } }
                public IIntermediateCliAssembly Assembly { get { return this.Owner.Assembly; } }

                protected override IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember.IndexerValueParameter GetIndexerValueParameter()
                {
                    return new IndexerMethodMember.IndexerValueParameter(this.Owner, this);
                }
                public ICliMetadataMethodDefinitionMutableTableRow MetadataEntry
                {
                    get
                    {
                        return null;
                    }
                }

                ICliMetadataMethodDefinitionTableRow ICliDeclaration<IGeneralGenericSignatureMemberUniqueIdentifier, ICliMetadataMethodDefinitionTableRow>.MetadataEntry
                {
                    get { return this.MetadataEntry; }
                }

                ICliMetadataTableRow ICliDeclaration.MetadataEntry
                {
                    get { return this.MetadataEntry; }
                }
            }

            protected class IndexerMethodMember :
                IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember,
                ICliDeclaration<IGeneralGenericSignatureMemberUniqueIdentifier, ICliMetadataMethodDefinitionTableRow>
            {
                public IndexerMethodMember(PropertyMethodType methodType, IndexerMember owner)
                    : base(methodType, owner)
                {
                }

                public new IndexerMember Owner { get { return (IndexerMember)base.Owner; } }
                public IIntermediateCliAssembly Assembly { get { return this.Owner.Assembly; } }

                protected internal new class IndexerValueParameter :
                    IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember.IndexerValueParameter,
                    ICliDeclaration<IGeneralMemberUniqueIdentifier, ICliMetadataParameterTableRow>
                {
                    public IndexerValueParameter(IndexerMember owner, IIntermediateClassMethodMember parent)
                        : base(owner, parent)
                    {
                    }

                    public new IndexerMember Owner { get { return (IndexerMember)base.Owner; } }
                    public IIntermediateCliAssembly Assembly { get { return this.Owner.Assembly; } }

                    public ICliMetadataParameterMutableTableRow MetadataEntry
                    {
                        get
                        {
                            return null;
                        }
                    }

                    ICliMetadataParameterTableRow ICliDeclaration<IGeneralMemberUniqueIdentifier, ICliMetadataParameterTableRow>.MetadataEntry
                    {
                        get { return this.MetadataEntry; }
                    }

                    ICliMetadataTableRow ICliDeclaration.MetadataEntry
                    {
                        get { return this.MetadataEntry; }
                    }


                }
                public ICliMetadataMethodDefinitionMutableTableRow MetadataEntry
                {
                    get
                    {
                        return null;
                    }
                }

                ICliMetadataMethodDefinitionTableRow ICliDeclaration<IGeneralGenericSignatureMemberUniqueIdentifier, ICliMetadataMethodDefinitionTableRow>.MetadataEntry
                {
                    get { return this.MetadataEntry; }
                }

                ICliMetadataTableRow ICliDeclaration.MetadataEntry
                {
                    get { return this.MetadataEntry; }
                }

                protected internal new class IndexerDependentParameter :
                    IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember.IndexerDependentParameter
                {
                    public IndexerDependentParameter(ParameterMembersDictionary.ParameterMember original, IIntermediateClassMethodMember parent)
                        : base(original, parent)
                    {
                    }
                }
            }
        }

        protected override IntermediateClassType<TInstanceIntermediateType>.EventMember GetNewEvent(string name, TypedNameSeries eventSignature)
        {
            var result = new EventMember(((TInstanceIntermediateType)(object)this))
            {
                SignatureSource = EventSignatureSource.Declared,
                Name = name
            };
            foreach (var param in eventSignature)
                result.Parameters.Add(param.Name, param.GetTypeRef(), param.Direction);
            return result;
        }

        protected class EventMember :
            IntermediateClassEventMember<TInstanceIntermediateType>,
            ICliDeclaration<IGeneralSignatureMemberUniqueIdentifier, ICliMetadataEventTableRow>
        {
            public EventMember(TInstanceIntermediateType parent)
                : base(parent)
            {
            }

            public new class EventMethodMember :
                IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember
            {
                public EventMethodMember(EventMember parent, EventMethodType methodType)
                    : base(parent, methodType)
                {
                }
            }

            public ICliMetadataEventMutableTableRow MetadataEntry
            {
                get
                {
                    return null;
                }
            }

            ICliMetadataEventTableRow ICliDeclaration<IGeneralSignatureMemberUniqueIdentifier, ICliMetadataEventTableRow>.MetadataEntry
            {
                get { return this.MetadataEntry; }
            }

            ICliMetadataTableRow ICliDeclaration.MetadataEntry
            {
                get { return this.MetadataEntry; }
            }

            protected override IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember GetMethodMember(EventMethodType type)
            {
                return new EventMethodMember(this, type);
            }
        }
    }
}
