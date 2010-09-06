using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Utilities.Events;
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
            TIntermediateSignature method = this.OnGetNewMethod(name);
            method.Renamed += method_Renamed;
            method.TypeParameterAdded += new EventHandler<Utilities.Events.EventArgsR1<IIntermediateMethodSignatureGenericTypeParameterMember>>(method_TypeParameterAddOrRemove);
            method.TypeParameterRemoved += new EventHandler<Utilities.Events.EventArgsR1<IIntermediateMethodSignatureGenericTypeParameterMember>>(method_TypeParameterAddOrRemove);
            return method;
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
            var method = this.GetNewMethod(name);
            foreach (var item in parameters)
            {
                IType paramType = null;
                SetTypeReference(item, p => paramType = p);
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
            for (int i = 0; i < typeParameters.Length; i++)
            {
                method.TypeParameters.Add(typeParameters[i]);
            }
            foreach (var item in parameters)
            {
                IType paramType = null;
                SetTypeReference(item, p => paramType = p, p => method.TypeParameters.ContainsKey(p) ? method.TypeParameters[p] : null);
                paramType = AdjustTypeReference(paramType, item.Direction);
                method.Parameters.Add(item.Name, paramType, item.Direction);
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

        protected override bool RemoveImpl(string key)
        {
            if (base.ContainsKey(key))
            {
                var method = (TIntermediateSignature)base[key];
                if (method != null)
                {
                    method.TypeParameterAdded -= new EventHandler<Utilities.Events.EventArgsR1<IIntermediateMethodSignatureGenericTypeParameterMember>>(method_TypeParameterAddOrRemove);
                    method.TypeParameterRemoved -= new EventHandler<Utilities.Events.EventArgsR1<IIntermediateMethodSignatureGenericTypeParameterMember>>(method_TypeParameterAddOrRemove);
                    method.Renamed -= method_Renamed;
                }
            }
            return base.RemoveImpl(key);
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
            SetTypeReference(nameAndReturn, p => method.ReturnType = p);
            return method;
        }

        private void SetTypeReference(TypedName source, Action<IType> setter, Func<string, IType> altGetter = null)
        {
            /* *
             * Attempts to do an early type-resolution on potential symbols
             * passed in.
             * */
            switch (source.Source)
            {
                case TypedNameSource.TypeReference:
                    setter(source.Reference);
                    break;
                case TypedNameSource.SymbolReference:
                    if (this.Parent is IGenericType)
                    {
                        IGenericType t = ((IGenericType)(this.Parent));
                        bool found = false;
                        for (; t != null; t = (((t.DeclaringType != null) && (t.DeclaringType is IGenericType)) ? (IGenericType)t.DeclaringType : null))
                        {
                            if (t.TypeParameters.ContainsKey(source.SymbolReference))
                            {
                                setter((IGenericTypeParameter)t.TypeParameters[source.SymbolReference]);
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            IType q = null;
                            if (altGetter != null)
                                q = altGetter(source.SymbolReference);
                            if (q != null)
                                setter(q);
                            else
                                setter(source.SymbolReference.GetSymbolType());
                        }
                    }
                    break;
            }
        }

        public TIntermediateSignature Add(TypedName nameAndReturn, TypedNameSeries parameters)
        {
            TIntermediateSignature method = this.Add(nameAndReturn.Name, parameters);
            SetTypeReference(nameAndReturn, p => method.ReturnType = p);
            return method;
        }

        public TIntermediateSignature Add(TypedName nameAndReturn, TypedNameSeries parameters, params GenericParameterData[] typeParameters)
        {
            TIntermediateSignature method = this.Add(nameAndReturn.Name, parameters, typeParameters);
            SetTypeReference(nameAndReturn, p => method.ReturnType = p, p => method.TypeParameters.ContainsKey(p) ? method.TypeParameters[p] : null);
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
            throw new NotImplementedException();
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
                IType paramType = null;
                SetTypeReference(item, p => paramType = AdjustTypeReference(p, item.Direction), null);
                method.Parameters.Add(item.Name, paramType, item.Direction);
            }
            return method;
        }

        private TIntermediateSignature GetNewMethodWithParametersAndTypeParameters(string name, TypedNameSeries parameters, GenericParameterData[] typeParameters)
        {
            var method = this.GetNewMethod(name);
            for (int i = 0; i < typeParameters.Length; i++)
            {
                method.TypeParameters.Add(typeParameters[i]);
            }
            if (parameters.Count > 0)
            {
                Func<String, IType> altGetter = typeParameterName => method.TypeParameters.ContainsKey(typeParameterName) ? method.TypeParameters[typeParameterName] : null;
                foreach (var item in parameters)
                {
                    IType paramType = null;
                    SetTypeReference(item, incomingType => paramType = AdjustTypeReference(incomingType, item.Direction), altGetter);
                    method.Parameters.Add(item.Name, paramType, item.Direction);
                }
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
            this.Add(method.UniqueIdentifier, method);
            return method;
        }

        public TIntermediateSignature Add(string name, TypedNameSeries parameters)
        {
            var method = this.GetNewMethodWithParameters(name, parameters);
            this.Add(method.UniqueIdentifier, method);
            return method;
        }

        public TIntermediateSignature Add(string name, TypedNameSeries parameters, params GenericParameterData[] typeParameters)
        {
            var method = this.GetNewMethodWithParametersAndTypeParameters(name, parameters, typeParameters);
            this.Add(method.UniqueIdentifier, method);
            return method;
        }

        public TIntermediateSignature Add(TypedName nameAndReturn)
        {
            TIntermediateSignature method = this.Add(nameAndReturn.Name);
            SetTypeReference(nameAndReturn, p => method.ReturnType = p, null);
            return method;
        }

        private void SetTypeReference(TypedName source, Action<IType> setter, Func<string, IType> altGetter)
        {
            switch (source.Source)
            {
                case TypedNameSource.TypeReference:
                    setter(source.Reference);
                    break;
                case TypedNameSource.SymbolReference:
                    if (this.Parent is IGenericType)
                    {
                        IGenericType t = ((IGenericType)(this.Parent));
                        bool found = false;
                        for (; t != null; t = (((t.DeclaringType != null) && (t.DeclaringType is IGenericType)) ? (IGenericType)t.DeclaringType : null))
                        {
                            if (t.TypeParameters.ContainsKey(source.SymbolReference))
                            {
                                setter((IGenericTypeParameter)t.TypeParameters[source.SymbolReference]);
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            IType q = null;
                            if (altGetter != null)
                                q = altGetter(source.SymbolReference);
                            if (q != null)
                                setter(q);
                            else
                                setter(source.SymbolReference.GetSymbolType());
                        }
                    }
                    break;
            }
        }

        public TIntermediateSignature Add(TypedName nameAndReturn, TypedNameSeries parameters)
        {
            TIntermediateSignature method = this.Add(nameAndReturn.Name, parameters);
            SetTypeReference(nameAndReturn, p => method.ReturnType = p, null);
            return method;
        }

        public TIntermediateSignature Add(TypedName nameAndReturn, TypedNameSeries parameters, params GenericParameterData[] typeParameters)
        {
            TIntermediateSignature method = this.Add(nameAndReturn.Name, parameters, typeParameters);
            SetTypeReference(nameAndReturn, p => method.ReturnType = p, p => method.TypeParameters.ContainsKey(p) ? method.TypeParameters[p] : null);
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
            throw new NotImplementedException();
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
