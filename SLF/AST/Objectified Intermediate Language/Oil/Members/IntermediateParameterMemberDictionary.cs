using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Threading.Tasks;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
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
        IntermediateMemberDictionary<TParent, TIntermediateParent, TParameter, TIntermediateParameter>,
        IIntermediateParameterMemberDictionary<TParent, TIntermediateParent, TParameter, TIntermediateParameter>,
        IIntermediateParameterMemberDictionary
        where TParent :
            IParameterParent<TParent, TParameter>
        where TIntermediateParent :
            TParent,
            IIntermediateParameterParent<TParent, TIntermediateParent, TParameter, TIntermediateParameter>
        where TParameter :
            IParameterMember<TParent>
        where TIntermediateParameter :
            TParameter,
            IIntermediateParameterMember<TParent, TIntermediateParent>
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateParameterMemberDictionary{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/> which contains the
        /// <see cref="IntermediateParameterMemberDictionary{TParent, TIntermediateParent, TParameter, TIntermediateParameter}"/>.</param>
        public IntermediateParameterMemberDictionary(TIntermediateParent parent)
            : base(parent)
        {
        }


        #region IIntermediateParameterMemberDictionary<TParent,TIntermediateParent,TParameter,TIntermediateParameter> Members

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
            return this.Add(name, parameterType, ParameterDirection.In);
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
        public TIntermediateParameter Add(string name, IType parameterType, ParameterDirection direction)
        {
            TIntermediateParameter item = this.GetNewParameter(name, parameterType, direction);
            this.Add(item.UniqueIdentifier, item);
            return item;
        }

        public new TIntermediateParameter Add(TypedName parameterInfo)
        {
            if (parameterInfo.Source == TypedNameSource.SymbolReference)
                return this.Add(parameterInfo.Name, parameterInfo.Reference, parameterInfo.Direction);
            else
                return this.Add(parameterInfo.Name, parameterInfo.SymbolReference.GetSymbolType(), parameterInfo.Direction);
        }

        public new TIntermediateParameter[] AddRange(params TypedName[] parameterInfo)
        {
            if (parameterInfo == null)
                throw new ArgumentNullException("parameterInfo");
            TIntermediateParameter[] result = new TIntermediateParameter[parameterInfo.Length];
            Parallel.For(0, parameterInfo.Length, i =>
                {
                    var currentParamInfo = parameterInfo[i];
                    switch (currentParamInfo.Source)
                    {
                        case TypedNameSource.TypeReference:
                            result[i] = this.GetNewParameter(currentParamInfo.Name, currentParamInfo.Reference, currentParamInfo.Direction);
                            break;
                        case TypedNameSource.SymbolReference:
                            result[i] = this.GetNewParameter(currentParamInfo.Name, currentParamInfo.SymbolReference.GetSymbolType(), currentParamInfo.Direction);
                            break;
                        case TypedNameSource.InvalidReference:
                        default:
                            throw new ArgumentException("parameterInfo");
                    }
                });
            foreach (var element in result)
                this.Add(element.Name, element);
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
        protected abstract TIntermediateParameter GetNewParameter(string name, IType parameterType, ParameterDirection direction);

        #region IParameterMemberDictionary Members

        IParameterParent IParameterMemberDictionary.Parent
        {
            get { return base.Parent; }
        }

        #endregion

        #region IIntermediateParameterMemberDictionary Members

        IIntermediateParameterParent IIntermediateParameterMemberDictionary.Parent
        {
            get { return base.Parent; }
        }

        IIntermediateParameterMember IIntermediateParameterMemberDictionary.Add(string name, IType parameterType)
        {
            return Add(name, parameterType);
        }

        IIntermediateParameterMember IIntermediateParameterMemberDictionary.Add(string name, IType parameterType, ParameterDirection direction)
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

        internal bool Changed { get; set; }

        protected override void AddImpl(KeyValuePair<string, TParameter> expression)
        {
            this.Changed = true;
            base.AddImpl(expression);
        }

        protected override bool RemoveImpl(string key)
        {
            this.Changed = true;
            return base.RemoveImpl(key);
        }

        protected override void Clear()
        {
            this.Changed = true;
            base.Clear();
        }

    }
}
