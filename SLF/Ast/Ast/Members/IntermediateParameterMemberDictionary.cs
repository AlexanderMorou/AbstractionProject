using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Properties;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Provides a parameter member dictionary.
    /// </summary>
    /// <typeparam name="TParent">The type which parents the <typeparamref name="TParameter"/> 
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParent">The type which parents the <typeparamref name="TIntermediateParameter"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TParameter">The type of parameter in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParameter">The type of parameter in the intermediate
    /// abstract syntax tree.</typeparam>
    public abstract class IntermediateParameterMemberDictionary<TParent, TIntermediateParent, TParameter, TIntermediateParameter> :
        IntermediateMemberDictionary<TParent, TIntermediateParent, IGeneralMemberUniqueIdentifier, TParameter, TIntermediateParameter>,
        IIntermediateParameterMemberDictionary<TParent, TIntermediateParent, TParameter, TIntermediateParameter>,
        IIntermediateParameterMemberDictionary
        where TParent :
            IParameterParent<TParent, TParameter>
        where TIntermediateParent :
            TParent,
            IIntermediateParameterParent<TParent, TIntermediateParent, TParameter, TIntermediateParameter>,
            IIntermediateDeclaration
        where TParameter :
            IParameterMember<TParent>
        where TIntermediateParameter :
            TParameter,
            IIntermediateParameterMember<TParent, TIntermediateParent>
    {
        private ParameterMemberDictionaryTypes<TParent, TParameter> parameterTypes;
        /// <summary>
        /// Creates a new <see cref="IntermediateParameterMemberDictionary{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/> which contains the
        /// <see cref="IntermediateParameterMemberDictionary{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>.</param>
        public IntermediateParameterMemberDictionary(TIntermediateParent parent)
            : base(parent)
        {
            /* *
             * Initializes the unique identifier, this is a hack.
             * */
            //var m = parent.UniqueIdentifier;
        }

        protected override bool ShouldDispose(TIntermediateParameter declaration)
        {
            return true;
        }

        #region IIntermediateParameterMemberDictionary<TParent,TIntermediateParent,TParameter,TIntermediateParameter> Members

        public TIntermediateParameter this[string name]
        {
            get
            {
                for (int i = 0, c = this.Count; i < c; i++)
                    if (this.Keys[i].Name == name)
                        return this.Values[i];
                throw new KeyNotFoundException();
            }
        }

        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateParameter"/> instance
        /// with the <paramref name="name"/> and <paramref name="parameterType"/>
        /// provided.
        /// </summary>
        /// <param name="name">The name of the parameter to add.</param>
        /// <param name="parameterType">The type of the parameter to add.</param>
        /// <returns>A new <typeparamref name="TIntermediateParameter"/>
        /// as it exists in the <see cref="IIntermediateParameterMemberDictionary{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>.</returns>
        public TIntermediateParameter Add(string name, IType parameterType)
        {
            return this.Add(name, parameterType, ParameterCoercionDirection.In);
        }

        /// <summary>
        /// Adds a new <typeparamref name="TIntermediateParameter"/> instance
        /// with the <paramref name="name"/>, <paramref name="parameterType"/> and 
        /// <paramref name="direction"/>
        /// provided.
        /// </summary>
        /// <param name="name">The name of the parameter to add.</param>
        /// <param name="parameterType">The type of the parameter to add.</param>
        /// <param name="direction">The direction in which the <typeparamref name="TIntermediateParameter"/>
        /// is coerced.</param>
        /// <returns>A new <typeparamref name="TIntermediateParameter"/>
        /// as it exists in the <see cref="IIntermediateParameterMemberDictionary{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>.</returns>
        public TIntermediateParameter Add(string name, IType parameterType, ParameterCoercionDirection direction)
        {
            LockCheckAndThrow();
            return AddInternal(name, parameterType, direction);
        }

        private void LockCheckAndThrow()
        {
            if (this.Locked)
                throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
        }

        private TIntermediateParameter AddInternal(string name, IType parameterType, ParameterCoercionDirection direction)
        {
            TIntermediateParameter item = this.GetNewParameter(name, parameterType, direction);
            this._Add(item.UniqueIdentifier, item);
            return item;
        }

        public TIntermediateParameter Add(TypedName parameterInfo)
        {
            LockCheckAndThrow();
            var paramKind = IntermediateGateway.AdjustParameterType(parameterInfo, this.Parent);
            return this.Add(parameterInfo.Name, paramKind, parameterInfo.Direction);
        }

        internal TIntermediateParameter _Add(TypedName parameterInfo)
        {
            var paramKind = parameterInfo.GetTypeRef();
            if (paramKind.ContainsSymbols())
                paramKind = paramKind.SimpleSymbolDisambiguation(this.Parent);
            return this.AddInternal(parameterInfo.Name, paramKind, parameterInfo.Direction);
        }

        public TIntermediateParameter[] AddRange(params TypedName[] parameterInfo)
        {
            LockCheckAndThrow();
            if (parameterInfo == null)
                throw new ArgumentNullException("parameterInfo");
            TIntermediateParameter[] result = new TIntermediateParameter[parameterInfo.Length];
            Parallel.For(0, parameterInfo.Length, i =>
                {
                    var currentParamInfo = parameterInfo[i];
                    var paramType = IntermediateGateway.AdjustParameterType(currentParamInfo, this.Parent);
                    result[i] = this.GetNewParameter(currentParamInfo.Name, paramType, currentParamInfo.Direction);
                });
            foreach (var element in result)
                this._Add(element.UniqueIdentifier, element);
            return result;
        }
        
        #endregion

        /// <summary>
        /// Obtains a <typeparamref name="TIntermediateParameter"/> 
        /// for insertion into the <see cref="IntermediateParameterMemberDictionary{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>.
        /// </summary>
        /// <param name="name">The name of the parameter to create.</param>
        /// <param name="parameterType">The type of the parameter to create.</param>
        /// <param name="direction">The direction in which the <typeparamref name="TIntermediateParameter"/>
        /// is coerced.</param>
        /// <returns>A new <typeparamref name="TIntermediateParameter"/> instance.</returns>
        protected abstract TIntermediateParameter GetNewParameter(string name, IType parameterType, ParameterCoercionDirection direction);

        #region IParameterMemberDictionary Members

        IParameterParent IParameterMemberDictionary.Parent
        {
            get { return base.Parent; }
        }

        public IControlledTypeCollection ParameterTypes
        {
            get
            {
                if (this.parameterTypes == null)
                    this.parameterTypes = new ParameterMemberDictionaryTypes<TParent, TParameter>(this);
                return this.parameterTypes;
            }
        }
        #endregion

        #region IIntermediateParameterMemberDictionary Members

        IIntermediateParameterMember IIntermediateParameterMemberDictionary.this[string name]
        {
            get
            {
                return this[name];
            }
        }

        bool IIntermediateParameterMemberDictionary.Remove(IIntermediateParameterMember member)
        {
            if (member == null || member is TIntermediateParameter)
                return this.Remove((TIntermediateParameter)member);
            throw ThrowHelper.ObtainArgumentException(ArgumentWithException.member, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.member), member.GetType().ToString(), typeof(TIntermediateParameter).ToString());
        }
        IIntermediateParameterParent IIntermediateParameterMemberDictionary.Parent
        {
            get { return base.Parent; }
        }

        IIntermediateParameterMember IIntermediateParameterMemberDictionary.Add(string name, IType parameterType)
        {
            return Add(name, parameterType);
        }

        IIntermediateParameterMember IIntermediateParameterMemberDictionary.Add(string name, IType parameterType, ParameterCoercionDirection direction)
        {
            return Add(name, parameterType, direction);
        }

        IIntermediateParameterMember[] IIntermediateParameterMemberDictionary.AddRange(params TypedName[] parameterInfo)
        {
            var resultOriginal = this.AddRange(parameterInfo);
            var result = new IIntermediateParameterMember[resultOriginal.Length];
            Parallel.For(0, resultOriginal.Length, i =>
                result[i] = resultOriginal[i]);
            return result;
        }

        IIntermediateParameterMember IIntermediateParameterMemberDictionary.Add(TypedName parameterInfo)
        {
            return this.Add(parameterInfo);
        }

        #endregion

        protected internal override void _Add(IGeneralMemberUniqueIdentifier key, TParameter value)
        {
            base._Add(key, value);
        }
        protected internal override bool _Remove(int index)
        {
            return base._Remove(index);
        }

        protected internal override void _Clear()
        {
            base._Clear();
        }
    }
}
