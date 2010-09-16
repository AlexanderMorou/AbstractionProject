using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Utilities.Events;
using System.Threading.Tasks;
/*----------------------------------------\
| Copyright © 2009 Allen Copeland Jr.     |
|-----------------------------------------|
| The Abstraction Project's code is prov- |
| -ided under a contract-release basis.   |
| DO NOT DISTRIBUTE and do not use beyond |
| the contract terms.                     |
\--------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    public abstract class IntermediateMethodSignatureMemberDictionary<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> :
        IntermediateGroupedMethodSignatureMemberDictionary<IMethodSignatureParameterMember<TSignature, TSignatureParent>, IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
        IIntermediateMethodSignatureMemberDictionary<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignature, TSignatureParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
        where TSignatureParent :
            IMethodSignatureParent<TSignature, TSignatureParent>
        where TIntermediateSignatureParent :
            IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
            TSignatureParent
    {
        protected IntermediateMethodSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateSignatureParent parent)
            : base(master, parent)
        {
        }
        protected IntermediateMethodSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateSignatureParent parent, IntermediateMethodSignatureMemberDictionary<TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> root)
            : base(master, parent, root)
        {
        }
    }

    public abstract class IntermediateGroupedMethodSignatureMemberDictionary<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> :
        IntermediateGroupedSignatureMemberDictionary<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>,
        IIntermediateMethodSignatureMemberDictionary<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
        IIntermediateMethodSignatureMemberDictionary
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TIntermediateSignatureParameter :
            TSignatureParameter,
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignatureParent :
            TSignatureParent,
            IIntermediateSignatureParent<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
    {
        protected IntermediateGroupedMethodSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateSignatureParent parent)
            : base(master, parent)
        {
        }
        protected IntermediateGroupedMethodSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateSignatureParent parent, IntermediateGroupedMethodSignatureMemberDictionary<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> root)
            : base(master, parent, root)
        {
        }
        protected TIntermediateSignature GetNewMethod(string name)
        {
            return this.OnGetNewMethod(name);
        }

        void method_TypeParameterAddOrRemove(object sender, EventArgsR1<IIntermediateMethodSignatureGenericTypeParameterMember> e)
        {
            base.IncrementVersion();
        }

        void method_Renamed(object sender, DeclarationNameChangedEventArgs e)
        {
            base.IncrementVersion();
        }
        protected abstract TIntermediateSignature OnGetNewMethod(string name);
        private TIntermediateSignature GetNewMethodWithParameters(string name, TypedNameSeries parameters)
        {
            return this.GetNewMethodWithParametersAndTypeParameters(name, parameters, GenericParameterData.EmptySet);
        }

        /* *
         * Lovely method name, huh?
         * */
        private TIntermediateSignature GetNewMethodWithParametersAndTypeParameters(string name, TypedNameSeries parameters, GenericParameterData[] typeParameters)
        {
            var method = this.GetNewMethod(name);
            if (typeParameters.Length > 0)
                method.TypeParameters.AddRange(typeParameters);
            if (parameters.Count > 0)
            {
                TypedName[] adjustedParameters = new TypedName[parameters.Count];
                Parallel.For(0, parameters.Count, i =>
                    {
                        TypedName currentItem = parameters[i];
                        IType paramType = currentItem.AscertainType(method);
                        paramType = AdjustTypeReference(paramType, currentItem.Direction);
                        adjustedParameters[i] = new TypedName(currentItem.Name, paramType);
                    });
                method.Parameters.AddRange(adjustedParameters);
            }
            return method;
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

        protected internal override void _Add(string key, TSignature value)
        {
            var method = (TIntermediateSignature)value;
            method.Renamed += method_Renamed;
            method.TypeParameterAdded += new EventHandler<EventArgsR1<IIntermediateMethodSignatureGenericTypeParameterMember>>(method_TypeParameterAddOrRemove);
            method.TypeParameterRemoved += new EventHandler<EventArgsR1<IIntermediateMethodSignatureGenericTypeParameterMember>>(method_TypeParameterAddOrRemove);
            method.ParameterAdded += new EventHandler<EventArgsR1<TIntermediateSignatureParameter>>(method_ParameterAddedOrRemoved);
            method.ParameterRemoved += new EventHandler<EventArgsR1<TIntermediateSignatureParameter>>(method_ParameterAddedOrRemoved);
            base._Add(key, value);
        }

        void method_ParameterAddedOrRemoved(object sender, EventArgsR1<TIntermediateSignatureParameter> e)
        {
            base.IncrementVersion();
        }

        protected internal override bool _Remove(int index)
        {
            if (index > 0 && index < this.Count)
            {
                var method = (TIntermediateSignature)base[index].Value;
                if (method != null)
                {
                    method.TypeParameterAdded -= new EventHandler<Utilities.Events.EventArgsR1<IIntermediateMethodSignatureGenericTypeParameterMember>>(method_TypeParameterAddOrRemove);
                    method.TypeParameterRemoved -= new EventHandler<Utilities.Events.EventArgsR1<IIntermediateMethodSignatureGenericTypeParameterMember>>(method_TypeParameterAddOrRemove);
                    method.ParameterAdded -= new EventHandler<EventArgsR1<TIntermediateSignatureParameter>>(method_ParameterAddedOrRemoved);
                    method.ParameterRemoved -= new EventHandler<EventArgsR1<TIntermediateSignatureParameter>>(method_ParameterAddedOrRemoved);
                    method.Renamed -= method_Renamed;
                }
            }
            return base._Remove(index);
        }

        #region IIntermediateMethodSignatureMemberDictionary<TSignatureParameter,TIntermediateSignatureParameter,TSignature,TIntermediateSignature,TSignatureParent,TIntermediateSignatureParent> Members

        public TIntermediateSignature Add(string name)
        {
            var method = this.GetNewMethod(name);
            method.ReturnType = IntermediateGateway.CommonlyUsedTypeReferences.Void;
            this.AddDeclaration(method);
            return method;
        }

        public TIntermediateSignature Add(string name, TypedNameSeries parameters)
        {
            var method = this.GetNewMethodWithParameters(name, parameters);
            method.ReturnType = IntermediateGateway.CommonlyUsedTypeReferences.Void;
            this.AddDeclaration(method);
            return method;
        }

        public TIntermediateSignature Add(string name, TypedNameSeries parameters, params GenericParameterData[] typeParameters)
        {
            var method = this.GetNewMethodWithParametersAndTypeParameters(name, parameters, typeParameters);
            method.ReturnType = IntermediateGateway.CommonlyUsedTypeReferences.Void;
            this.AddDeclaration(method);
            return method;
        }

        public TIntermediateSignature Add(TypedName nameAndReturn)
        {
            TIntermediateSignature method = this.Add(nameAndReturn.Name);
            method.ReturnType = nameAndReturn.AscertainType(method);
            return method;
        }

        public TIntermediateSignature Add(TypedName nameAndReturn, TypedNameSeries parameters)
        {
            TIntermediateSignature method = this.Add(nameAndReturn.Name, parameters);
            method.ReturnType = nameAndReturn.AscertainType(method);
            return method;
        }

        public TIntermediateSignature Add(TypedName nameAndReturn, TypedNameSeries parameters, params GenericParameterData[] typeParameters)
        {
            TIntermediateSignature method = this.Add(nameAndReturn.Name, parameters, typeParameters);
            method.ReturnType = nameAndReturn.AscertainType(method);
            return method;
        }

        #endregion

        #region IMethodSignatureMemberDictionary<TSignatureParameter,TSignature,TSignatureParent> Members

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, bool strict, ITypeCollection search)
        {
            return this.Find(name, null, strict, search);
        }

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection search)
        {
            return this.Find(name, null, true, search);
        }

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, bool strict, params IType[] search)
        {
            return this.Find(name, null, strict, search);
        }

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, params IType[] search)
        {
            return this.Find(name, null, true, search);
        }

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, bool strict, ITypeCollection search)
        {
            return CLIGateway.FindCache<TSignature, TSignatureParameter, TSignatureParent>(genericParameters, ((IMethodSignatureMemberDictionary<TSignatureParameter, TSignature, TSignatureParent>)this).Values, name, search, strict);
        }

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, ITypeCollection search)
        {
            return this.Find(name, genericParameters, true, search);
        }

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, bool strict, params IType[] search)
        {
            return CLIGateway.FindCache<TSignature, TSignatureParameter, TSignatureParent>(genericParameters, ((IMethodSignatureMemberDictionary<TSignatureParameter, TSignature, TSignatureParent>)this).Values, name, search, strict);
        }

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, params IType[] search)
        {
            return this.Find(name, genericParameters, true, search);
        }

        #endregion

        #region IIntermediateMethodSignatureMemberDictionary Members

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(string name)
        {
            return this.Add(name);
        }

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(string name, TypedNameSeries parameters)
        {
            return this.Add(name, parameters);
        }

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(string name, TypedNameSeries parameters, params GenericParameterData[] typeParameters)
        {
            return this.Add(name, parameters, typeParameters);
        }

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(TypedName nameAndReturn)
        {
            return this.Add(nameAndReturn);
        }

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(TypedName nameAndReturn, TypedNameSeries parameters)
        {
            return this.Add(nameAndReturn, parameters);
        }

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(TypedName nameAndReturn, TypedNameSeries parameters, params GenericParameterData[] typeParameters)
        {
            return this.Add(nameAndReturn, parameters, typeParameters);
        }

        #endregion

        #region IMethodSignatureMemberDictionary Members

        int IMethodSignatureMemberDictionary.IndexOf(IMethodSignatureMember method)
        {
            if (!(method is TSignature))
                return -1;
            return this.IndexOf((TSignature)(method));
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, ITypeCollection genericParameters, bool strict, ITypeCollection search)
        {
            return ((IFilteredSignatureMemberDictionary)(this.Find(name, genericParameters, strict, search)));
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, ITypeCollection genericParameters, ITypeCollection search)
        {
            return ((IFilteredSignatureMemberDictionary)(this.Find(name, genericParameters, search)));
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, ITypeCollection genericParameters, bool strict, params IType[] search)
        {
            return ((IFilteredSignatureMemberDictionary)(this.Find(name, genericParameters, strict, search)));
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, ITypeCollection genericParameters, params IType[] search)
        {
            return ((IFilteredSignatureMemberDictionary)(this.Find(name, genericParameters, search)));
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, bool strict, ITypeCollection search)
        {
            return ((IFilteredSignatureMemberDictionary)(this.Find(name, strict, search)));
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, ITypeCollection search)
        {
            return ((IFilteredSignatureMemberDictionary)(this.Find(name, search)));
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, bool strict, params IType[] search)
        {
            return ((IFilteredSignatureMemberDictionary)(this.Find(name, strict, search)));
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, params IType[] search)
        {
            return ((IFilteredSignatureMemberDictionary)(this.Find(name, search)));
        }

        #endregion

    }
    public abstract class IntermediateMethodSignatureMemberDictionary<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> :
        IntermediateSignatureMemberDictionary<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>,
        IIntermediateMethodSignatureMemberDictionary<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>,
        IIntermediateMethodSignatureMemberDictionary
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TIntermediateSignatureParameter :
            TSignatureParameter,
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent>
        where TSignatureParent :
            ISignatureParent<TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignatureParent :
            TSignatureParent,
            IIntermediateSignatureParent<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>
    {
        protected IntermediateMethodSignatureMemberDictionary(TIntermediateSignatureParent parent)
            : base(parent)
        {
        }

        protected IntermediateMethodSignatureMemberDictionary(TIntermediateSignatureParent parent, IntermediateMethodSignatureMemberDictionary<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TSignatureParent, TIntermediateSignatureParent> toWrap)
            : base(parent, toWrap)
        {
        }

        protected abstract TIntermediateSignature GetNewMethod(string name);

        private TIntermediateSignature GetNewMethodWithParameters(string name, TypedNameSeries parameters)
        {
            var method = this.GetNewMethod(name);
            foreach (var item in parameters)
            {
                IType paramType = GetTypeReference(item);
                paramType = AdjustTypeReference(paramType, item.Direction);
                method.Parameters.Add(item.Name, paramType, item.Direction);
            }
            return method;
        }

        /* *
         * Lovely method name, huh?
         * */
        private TIntermediateSignature GetNewMethodWithParametersAndTypeParameters(string name, TypedNameSeries parameters, GenericParameterData[] typeParameters)
        {
            var method = this.GetNewMethod(name);
            method.TypeParameters.AddRange(typeParameters);
            if (parameters.Count > 0)
            {
                TypedName[] adjustedParameters = new TypedName[parameters.Count];
                Parallel.For(0, parameters.Count, i =>
                {
                    TypedName currentItem = parameters[i];
                    IType paramType = GetTypeReference(currentItem, p => method.TypeParameters.ContainsKey(p) ? method.TypeParameters[p] : null);
                    paramType = AdjustTypeReference(paramType, currentItem.Direction);
                    adjustedParameters[i] = new TypedName(currentItem.Name, paramType);
                });
                method.Parameters.AddRange(adjustedParameters);
            }
            return method;
        }

        private static IType AdjustTypeReference(IType paramType, ParameterDirection parameterDirection)
        {
            if (paramType == null)
                return null;
            if (parameterDirection == ParameterDirection.In)
                return paramType;
            else
                return paramType.MakeByReference();
        }

        #region IIntermediateMethodSignatureMemberDictionary<TSignatureParameter,TIntermediateSignatureParameter,TSignature,TIntermediateSignature,TSignatureParent,TIntermediateSignatureParent> Members

        public TIntermediateSignature Add(string name)
        {
            var method = this.GetNewMethod(name);
            method.ReturnType = IntermediateGateway.CommonlyUsedTypeReferences.Void;
            this._Add(method.UniqueIdentifier, method);
            return method;
        }

        public TIntermediateSignature Add(string name, TypedNameSeries parameters)
        {
            var method = this.GetNewMethodWithParameters(name, parameters);
            method.ReturnType = IntermediateGateway.CommonlyUsedTypeReferences.Void;
            this._Add(method.UniqueIdentifier, method);
            return method;
        }

        public TIntermediateSignature Add(string name, TypedNameSeries parameters, params GenericParameterData[] typeParameters)
        {
            var method = this.GetNewMethodWithParametersAndTypeParameters(name, parameters, typeParameters);
            method.ReturnType = IntermediateGateway.CommonlyUsedTypeReferences.Void;
            this._Add(method.UniqueIdentifier, method);
            return method;
        }

        public TIntermediateSignature Add(TypedName nameAndReturn)
        {
            TIntermediateSignature method = this.Add(nameAndReturn.Name);
            method.ReturnType = GetTypeReference(nameAndReturn);
            return method;
        }


        private IType GetTypeReference(TypedName source, Func<string, IType> altGetter = null)
        {
            /* *
             * Attempts to do an early type-resolution on potential symbols
             * passed in.
             * */
            switch (source.Source)
            {
                case TypedNameSource.TypeReference:
                    return (source.Reference);
                case TypedNameSource.SymbolReference:
                    if (this.Parent is IGenericType)
                    {
                        IGenericType t = ((IGenericType)(this.Parent));
                        for (; t != null; t = (((t.DeclaringType != null) && (t.DeclaringType is IGenericType)) ? (IGenericType)t.DeclaringType : null))
                            if (t.TypeParameters.ContainsKey(source.SymbolReference))
                                return ((IGenericTypeParameter)t.TypeParameters[source.SymbolReference]);
                        IType q = null;
                        if (altGetter != null)
                            q = altGetter(source.SymbolReference);
                        if (q != null)
                            return (q);
                        else
                            return (source.SymbolReference.GetSymbolType());
                    }
                    break;
            }
            return null;
        }


        public TIntermediateSignature Add(TypedName nameAndReturn, TypedNameSeries parameters)
        {
            TIntermediateSignature method = this.Add(nameAndReturn.Name, parameters);
            method.ReturnType = GetTypeReference(nameAndReturn);
            return method;
        }

        public TIntermediateSignature Add(TypedName nameAndReturn, TypedNameSeries parameters, params GenericParameterData[] typeParameters)
        {
            TIntermediateSignature method = this.Add(nameAndReturn.Name, parameters, typeParameters);
            method.ReturnType = GetTypeReference(nameAndReturn, p => method.TypeParameters.ContainsKey(p) ? method.TypeParameters[p] : null);
            return method;
        }

        #endregion

        #region IMethodSignatureMemberDictionary<TSignatureParameter,TSignature,TSignatureParent> Members

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, bool strict, ITypeCollection search)
        {
            return this.Find(name, null, strict, search);
        }

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection search)
        {
            return this.Find(name, null, true, search);
        }

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, bool strict, params IType[] search)
        {
            return this.Find(name, null, strict, search);
        }

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, params IType[] search)
        {
            return this.Find(name, null, true, search);
        }

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, bool strict, ITypeCollection search)
        {
            return CLIGateway.FindCache<TSignature, TSignatureParameter, TSignatureParent>(genericParameters, ((IMethodSignatureMemberDictionary<TSignatureParameter, TSignature, TSignatureParent>)this).Values, name, search, strict);
        }

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, ITypeCollection search)
        {
            return this.Find(name, genericParameters, true, search);
        }

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, bool strict, params IType[] search)
        {
            return CLIGateway.FindCache<TSignature, TSignatureParameter, TSignatureParent>(genericParameters, ((IMethodSignatureMemberDictionary<TSignatureParameter, TSignature, TSignatureParent>)this).Values, name, search, strict);
        }

        public IFilteredSignatureMemberDictionary<TSignature, TSignatureParameter, TSignatureParent> Find(string name, ITypeCollection genericParameters, params IType[] search)
        {
            return this.Find(name, genericParameters, true, search);
        }

        #endregion

        #region IIntermediateMethodSignatureMemberDictionary Members

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(string name)
        {
            return this.Add(name);
        }

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(string name, TypedNameSeries parameters)
        {
            return this.Add(name, parameters);
        }

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(string name, TypedNameSeries parameters, params GenericParameterData[] typeParameters)
        {
            return this.Add(name, parameters, typeParameters);
        }

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(TypedName nameAndReturn)
        {
            return this.Add(nameAndReturn);
        }

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(TypedName nameAndReturn, TypedNameSeries parameters)
        {
            return this.Add(nameAndReturn, parameters);
        }

        IIntermediateMethodSignatureMember IIntermediateMethodSignatureMemberDictionary.Add(TypedName nameAndReturn, TypedNameSeries parameters, params GenericParameterData[] typeParameters)
        {
            return this.Add(nameAndReturn, parameters, typeParameters);
        }

        #endregion

        #region IMethodSignatureMemberDictionary Members

        int IMethodSignatureMemberDictionary.IndexOf(IMethodSignatureMember method)
        {
            if (!(method is TSignature))
                return -1;
            return this.IndexOf((TSignature)(method));
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, ITypeCollection genericParameters, bool strict, ITypeCollection search)
        {
            return ((IFilteredSignatureMemberDictionary)(this.Find(name,genericParameters, strict, search)));
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, ITypeCollection genericParameters, ITypeCollection search)
        {
            return ((IFilteredSignatureMemberDictionary)(this.Find(name, genericParameters, search)));
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, ITypeCollection genericParameters, bool strict, params IType[] search)
        {
            return ((IFilteredSignatureMemberDictionary)(this.Find(name, genericParameters, strict, search)));
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, ITypeCollection genericParameters, params IType[] search)
        {
            return ((IFilteredSignatureMemberDictionary)(this.Find(name, genericParameters, search)));
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, bool strict, ITypeCollection search)
        {
            return ((IFilteredSignatureMemberDictionary)(this.Find(name, strict, search)));
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, ITypeCollection search)
        {
            return ((IFilteredSignatureMemberDictionary)(this.Find(name, search)));
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, bool strict, params IType[] search)
        {
            return ((IFilteredSignatureMemberDictionary)(this.Find(name, strict, search)));
        }

        IFilteredSignatureMemberDictionary IMethodSignatureMemberDictionary.Find(string name, params IType[] search)
        {
            return ((IFilteredSignatureMemberDictionary)(this.Find(name, search)));
        }

        #endregion
    }
}
