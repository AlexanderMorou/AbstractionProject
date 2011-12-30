using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast.Properties;
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
        private class MethodMember :
            IntermediateMethodSignatureMemberBase<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>,
            IIntermediateInterfaceMethodMember
        {
            public MethodMember(IIntermediateInterfaceType parent)
                : base(parent)
            {
            }
            protected override IInterfaceMethodMember OnMakeGenericMethod(ITypeCollectionBase genericReplacements)
            {
                return new _InterfaceTypeBase._MethodsBase._Method(this, genericReplacements);
            }
        }
        private class EventMember :
            IntermediateEventSignatureMemberBase<IInterfaceEventMember, IIntermediateInterfaceEventMember, IInterfaceType, IIntermediateInterfaceType>,
            IIntermediateInterfaceEventMember
        {
            public EventMember(IIntermediateInterfaceType parent)
                : base(parent)
            {
            }
        }

        private class PropertyMember :
            IntermediatePropertySignatureMember<IInterfacePropertyMember, IIntermediateInterfacePropertyMember, IInterfaceType, IIntermediateInterfaceType, PropertyMember.PropertyMethod>,
            IIntermediateInterfacePropertyMember
        {

            internal PropertyMember(string name, TInstanceType parent)
                : base(name, parent)
            {
            }

            internal class PropertyMethod :
                MethodMember,
                IIntermediatePropertySignatureMethodMember
            {
                public PropertyMethod(PropertyMember owner)
                    : base(owner.Parent)
                {
                    this.Owner = owner;
                }

                protected new PropertyMember Owner { get; private set; }

                #region IPropertySignatureMethodMember Members

                public virtual PropertyMethodType MethodType
                {
                    get { return PropertyMethodType.GetMethod; }
                }

                #endregion

                protected override string OnGetName()
                {
                    switch (this.MethodType)
                    {
                        case PropertyMethodType.GetMethod:
                            return string.Format("get_{0}", this.Owner.Name);
                        case PropertyMethodType.SetMethod:
                            return string.Format("set_{0}", this.Owner.Name);
                        default:
                            throw new InvalidOperationException();
                    }
                }


                protected override IntermediateParameterMemberDictionary<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>, IIntermediateMethodSignatureParameterMember<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>> InitializeParameters()
                {
                    var result = base.InitializeParameters();
                    result.Lock();
                    return result;
                }

                public override IGeneralGenericSignatureMemberUniqueIdentifier UniqueIdentifier
                {
                    get
                    {
                        return AstIdentifier.GenericSignature(this.Name);
                    }
                }
            }

            internal sealed class PropertySetMethod :
                PropertyMethod,
                IIntermediatePropertySignatureSetMethodMember
            {
                private _ValueParameter valueParameter;

                internal PropertySetMethod(PropertyMember owner)
                    : base(owner)
                {

                }

                private class _ValueParameter :
                    ParameterMember
                {

                    public _ValueParameter(PropertySetMethod parent)
                        : base(parent)
                    {
                    }

                    private PropertySetMethod _Parent { get { return (PropertySetMethod)base.Parent; } }
                    protected override void OnSetName(string name)
                    {
                        throw new NotSupportedException();
                    }

                    protected override string OnGetName()
                    {
                        return "value";
                    }

                    public override IType ParameterType
                    {
                        get
                        {
                            return this._Parent.Owner.PropertyType;
                        }
                        set
                        {
                            this._Parent.Owner.PropertyType = value;
                        }
                    }
                }

                public override PropertyMethodType MethodType
                {
                    get
                    {
                        return PropertyMethodType.SetMethod;
                    }
                }

                #region IIntermediatePropertySignatureSetMethodMember Members

                public IIntermediateSignatureParameterMember ValueParameter
                {
                    get
                    {
                        if (this.valueParameter == null)
                            return this.Parameters["value"];
                        return valueParameter;
                    }
                }

                #endregion

                protected override IntermediateParameterMemberDictionary<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>, IIntermediateMethodSignatureParameterMember<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>> InitializeParameters()
                {
                    var result = base.InitializeParameters();
                    result.Unlock();
                    result._Add(AstIdentifier.Member("value"), this.valueParameter = new _ValueParameter(this));
                    result.Lock();
                    return result;
                }

                public override IGeneralGenericSignatureMemberUniqueIdentifier UniqueIdentifier
                {
                    get
                    {
                        return AstIdentifier.GenericSignature(this.Name, this.Owner.PropertyType);
                    }
                }
            }

            protected override PropertyMethod GetMethodSignatureMember(PropertyMethodType methodType)
            {
                switch (methodType)
                {
                    case PropertyMethodType.SetMethod:
                        return new PropertySetMethod(this);
                    default:
                    case PropertyMethodType.GetMethod:
                        return new PropertyMethod(this);
                }
            }

            public override void Visit(IIntermediateMemberVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        private class IndexerMember :
            IntermediateIndexerSignatureMember<IInterfaceIndexerMember, IIntermediateInterfaceIndexerMember, IInterfaceType, IIntermediateInterfaceType, IndexerMember.IndexerMethod>,
            IIntermediateInterfaceIndexerMember
        {

            internal IndexerMember(string name, TInstanceType parent)
                : base(name, parent)
            {
            }

            internal IndexerMember(TInstanceType parent)
                : base(parent)
            {
                
            }

            internal class IndexerMethod :
                MethodMember,
                IIntermediatePropertySignatureMethodMember
            {
                public IndexerMethod(IndexerMember owner)
                    : base(owner.Parent)
                {
                    this.Owner = owner;
                }

                protected new IndexerMember Owner { get; private set; }

                #region IIndexerSignatureMethodMember Members

                public virtual PropertyMethodType MethodType
                {
                    get { return PropertyMethodType.GetMethod; }
                }

                #endregion

                protected override string OnGetName()
                {
                    switch (this.MethodType)
                    {
                        case PropertyMethodType.GetMethod:
                            return string.Format("get_{0}", this.Owner.Name);
                        case PropertyMethodType.SetMethod:
                            return string.Format("set_{0}", this.Owner.Name);
                        default:
                            throw new InvalidOperationException();
                    }
                }


                protected override IntermediateParameterMemberDictionary<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>, IIntermediateMethodSignatureParameterMember<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>> InitializeParameters()
                {
                    var result = base.InitializeParameters();
                    result.Lock();
                    return result;
                }

                public override IGeneralGenericSignatureMemberUniqueIdentifier UniqueIdentifier
                {
                    get
                    {
                        return AstIdentifier.GenericSignature(this.Name);
                    }
                }
            }

            internal sealed class IndexerSetMethod :
                IndexerMethod,
                IIntermediatePropertySignatureSetMethodMember
            {
                private _ValueParameter valueParameter;

                internal IndexerSetMethod(IndexerMember owner)
                    : base(owner)
                {

                }

                private class _ValueParameter :
                    ParameterMember
                {

                    public _ValueParameter(IndexerSetMethod parent)
                        : base(parent)
                    {
                    }

                    private IndexerSetMethod _Parent { get { return (IndexerSetMethod)base.Parent; } }
                    protected override void OnSetName(string name)
                    {
                        throw new NotSupportedException();
                    }

                    protected override string OnGetName()
                    {
                        return "value";
                    }

                    public override IType ParameterType
                    {
                        get
                        {
                            return this._Parent.Owner.PropertyType;
                        }
                        set
                        {
                            this._Parent.Owner.PropertyType = value;
                        }
                    }
                }

                public override PropertyMethodType MethodType
                {
                    get
                    {
                        return PropertyMethodType.SetMethod;
                    }
                }

                #region IIntermediateIndexerSignatureSetMethodMember Members

                public IIntermediateSignatureParameterMember ValueParameter
                {
                    get
                    {
                        if (this.valueParameter == null)
                            return this.Parameters["value"];
                        return valueParameter;
                    }
                }

                #endregion

                protected override IntermediateParameterMemberDictionary<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>, IIntermediateMethodSignatureParameterMember<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>> InitializeParameters()
                {
                    var result = base.InitializeParameters();
                    result.Unlock();
                    result._Add(AstIdentifier.Member("value"), this.valueParameter = new _ValueParameter(this));
                    result.Lock();
                    return result;
                }

                public override IGeneralGenericSignatureMemberUniqueIdentifier UniqueIdentifier
                {
                    get
                    {
                        return AstIdentifier.GenericSignature(this.Name, this.Owner.PropertyType);
                    }
                }
            }

            protected override IndexerMethod GetMethodSignatureMember(PropertyMethodType methodType)
            {
                switch (methodType)
                {
                    case PropertyMethodType.SetMethod:
                        return new IndexerSetMethod(this);
                    default:
                    case PropertyMethodType.GetMethod:
                        return new IndexerMethod(this);
                }
            }

            public override void Visit(IIntermediateMemberVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

    }
}
