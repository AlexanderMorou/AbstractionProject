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
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
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
                : base(parent, parent.IdentityManager)
            {
            }
            protected override IInterfaceMethodMember OnMakeGenericMethod(IControlledTypeCollection genericReplacements)
            {
                return new _InterfaceTypeBase._MethodsBase._Method(this, genericReplacements);
            }
        }

        private class EventMember :
            IntermediateEventSignatureMemberBase<IInterfaceEventMember, IIntermediateInterfaceEventMember, IInterfaceType, IIntermediateInterfaceType, EventMember.Method>,
            IIntermediateInterfaceEventMember
        {
            public EventMember(IIntermediateInterfaceType parent)
                : base(parent)
            {
            }

            internal class Method :
                MethodMember
            {
                private EventMethodType methodType;
                internal Method(IIntermediateInterfaceType parent, EventMethodType methodType)
                    : base(parent)
                {
                    this.methodType = methodType;
                }

                public EventMethodType MethodType { get { return this.methodType; } }
            }

            protected override Method GetMethodSignatureMember(EventMethodType type)
            {
                return new Method(this.Parent, type);
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
                        return TypeSystemIdentifiers.GetGenericSignatureIdentifier(this.Name);
                    }
                }

                protected override IType OnGetReturnType()
                {
                    switch (this.MethodType)
                    {
                        case PropertyMethodType.GetMethod:
                            return this.Owner.PropertyType;
                        case PropertyMethodType.SetMethod:
                            return this.IdentityManager.ObtainTypeReference(RuntimeCoreType.VoidType);
                        default:
                            return base.OnGetReturnType();
                    }
                }

                protected override void Dispose(bool disposing)
                {
                    try
                    {
                        this.Owner = null;
                    }
                    finally
                    {
                        base.Dispose(disposing);
                    }
                }

                IParameterMemberDictionary IIntermediatePropertySignatureMethodMember.Parameters
                {
                    get { return (IParameterMemberDictionary)this.Parameters; }
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
                        : base(parent, parent.Parent.IdentityManager)
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
                            if (this.IsDisposed)
                                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                            else
                                return this.valueParameter = (_ValueParameter)this.Parameters["value"];
                        return valueParameter;
                    }
                }

                #endregion

                protected override IntermediateParameterMemberDictionary<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>, IIntermediateMethodSignatureParameterMember<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>> InitializeParameters()
                {
                    var result = base.InitializeParameters();
                    result.Unlock();
                    result._Add(TypeSystemIdentifiers.GetMemberIdentifier("value"), this.valueParameter = new _ValueParameter(this));
                    result.Lock();
                    return result;
                }

                public override IGeneralGenericSignatureMemberUniqueIdentifier UniqueIdentifier
                {
                    get
                    {
                        return TypeSystemIdentifiers.GetGenericSignatureIdentifier(this.Name, this.Owner.PropertyType);
                    }
                }

                protected override void Dispose(bool disposing)
                {
                    try
                    {
                        this.valueParameter = null;
                    }
                    finally
                    {
                        base.Dispose(disposing);
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

            public override TResult Visit<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context)
            {
                return visitor.Visit(this, context);
            }
        }

        private class IndexerMember :
            IntermediateIndexerSignatureMember<IInterfaceIndexerMember, IIntermediateInterfaceIndexerMember, IInterfaceType, IIntermediateInterfaceType, IndexerMember.IndexerMethod>,
            IIntermediateInterfaceIndexerMember
        {

            internal IndexerMember(string name, TInstanceType parent)
                : base(name, parent, parent.IdentityManager)
            {
            }

            internal IndexerMember(TInstanceType parent)
                : base(parent, parent.IdentityManager)
            {
                
            }

            protected override void OnParameterAdded(EventArgsR1<IIntermediateIndexerSignatureParameterMember<IInterfaceIndexerMember, IIntermediateInterfaceIndexerMember, IInterfaceType, IIntermediateInterfaceType>> e)
            {
                if (this.CanRead && this.IsGetMethodInitialized)
                {
                    var gm = (IndexerMethod)this.GetMethod;
                    if (gm._AreParametersInitialized)
                    {
                        var gmParams = gm.Parameters;
                        gmParams._Add(e.Arg1.UniqueIdentifier, new IndexerMethod.IndexerDependentParameter((IndexerMethod.ParameterMember)e.Arg1, gm));
                    }
                }
                if (this.CanWrite && this.IsSetMethodInitialized)
                {
                    var sm = (IndexerSetMethod)this.SetMethod;
                    if (sm._AreParametersInitialized)
                    {
                        var smParams = sm.Parameters;
                        var valueParam = (IndexerSetMethod._ValueParameter)((IIntermediatePropertySignatureSetMethodMember)sm).ValueParameter;
                        smParams._Remove(valueParam.UniqueIdentifier);
                        smParams._Add(e.Arg1.UniqueIdentifier, new IndexerMethod.IndexerDependentParameter((IndexerMethod.ParameterMember)e.Arg1, sm));
                        smParams._Add(valueParam.UniqueIdentifier, valueParam);
                    }
                }
                base.OnParameterAdded(e);
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

                protected override bool AreParametersInitialized
                {
                    get
                    {
                        return true;
                    }
                }

                internal class IndexerDependentParameter :
                    ParameterMember
                {
                    private ParameterMember original;
                    internal IndexerDependentParameter(ParameterMember original, MethodMember parent)
                        : base(parent, parent.IdentityManager)
                    {
                        this.original = original;
                    }
                    public override IType ParameterType
                    {
                        get
                        {
                            return this.original.ParameterType;
                        }
                        set
                        {
                            throw new InvalidOperationException("Cannot set the type of a parameter of a method of an indexer, set the indexer's parameter type.");
                        }
                    }
                    protected override string OnGetName()
                    {
                        return this.original.Name;
                    }
                    protected override void OnSetName(string name)
                    {
                        throw new InvalidOperationException("Cannot set the name of a parameter of a method of an indexer, set the indexer's parameter name.");
                    }
                    public override ParameterCoercionDirection Direction
                    {
                        get
                        {
                            return this.original.Direction;
                        }
                        set
                        {
                            throw new InvalidOperationException("Cannot set the direction of a parameter of a method of an indexer, set the indexer's parameter direction.");
                        }
                    }

                    protected override MetadataDefinitionCollection InitializeCustomAttributes()
                    {
                        return new MetadataDefinitionCollection(this.original, this.identityManager);
                    }

                    public override IGeneralMemberUniqueIdentifier UniqueIdentifier
                    {
                        get
                        {
                            return this.original.UniqueIdentifier;
                        }
                    }

                    protected override void Dispose(bool disposing)
                    {
                        this.original.Dispose();
                        base.Dispose(disposing);
                    }
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

                protected override IType OnGetReturnType()
                {
                    switch (this.MethodType)
                    {
                        case PropertyMethodType.GetMethod:
                            return this.Owner.PropertyType;
                        case PropertyMethodType.SetMethod:
                            return this.IdentityManager.ObtainTypeReference(RuntimeCoreType.VoidType);
                        default:
                            return base.OnGetReturnType();
                    }
                }

                public override IGeneralGenericSignatureMemberUniqueIdentifier UniqueIdentifier
                {
                    get
                    {
                        return TypeSystemIdentifiers.GetGenericSignatureIdentifier(this.Name);
                    }
                }

                protected override void Dispose(bool disposing)
                {
                    try
                    {
                        this.Owner = null;
                    }
                    finally
                    {
                        base.Dispose(disposing);
                    }
                }

                IParameterMemberDictionary IIntermediatePropertySignatureMethodMember.Parameters
                {
                    get { return (IParameterMemberDictionary)this.Parameters; }
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

                internal class _ValueParameter :
                    ParameterMember
                {

                    public _ValueParameter(IndexerSetMethod parent)
                        : base(parent, parent.IdentityManager)
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
                            if (this.IsDisposed)
                                throw new InvalidOperationException(Utilities.Properties.Resources.ObjectStateThrowMessage);
                            else
                                return this.valueParameter = (_ValueParameter)this.Parameters["value"];
                        return valueParameter;
                    }
                }

                #endregion

                protected override IntermediateParameterMemberDictionary<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IMethodSignatureParameterMember<IInterfaceMethodMember, IInterfaceType>, IIntermediateMethodSignatureParameterMember<IInterfaceMethodMember, IIntermediateInterfaceMethodMember, IInterfaceType, IIntermediateInterfaceType>> InitializeParameters()
                {
                    var result = base.InitializeParameters();
                    result.Unlock();
                    result._Add(TypeSystemIdentifiers.GetMemberIdentifier("value"), this.valueParameter = new _ValueParameter(this));
                    result.Lock();
                    return result;
                }

                public override IGeneralGenericSignatureMemberUniqueIdentifier UniqueIdentifier
                {
                    get
                    {
                        return TypeSystemIdentifiers.GetGenericSignatureIdentifier(this.Name, this.Owner.PropertyType);
                    }
                }

                protected override void Dispose(bool disposing)
                {
                    try
                    {
                        this.valueParameter = null;
                    }
                    finally
                    {
                        base.Dispose(disposing);
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

            public override TResult Visit<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context)
            {
                return visitor.Visit(this, context);
            }
        }

    }
}
