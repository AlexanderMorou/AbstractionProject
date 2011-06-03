using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides a base intermediate constructor member dictionary.
    /// </summary>
    /// <typeparam name="TCtor">The type of the constructor in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCtor">The type of the constructor in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TType">The type of the owning <see cref="ICreatableType{TCtor, TIntermediateType}"/> in 
    /// the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The type of the owning <see cref="IIntermediateCreatableType{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    public abstract class IntermediateConstructorMemberDictionary<TCtor, TIntermediateCtor, TType, TIntermediateType> :
        IntermediateConstructorSignatureMemberDictionary<TCtor, TIntermediateCtor, TType, TIntermediateType>,
        IIntermediateConstructorMemberDictionary<TCtor, TIntermediateCtor, TType, TIntermediateType>,
        IIntermediateConstructorMemberDictionary
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>,
            TCtor
        where TType :
            ICreatableType<TCtor, TType>
        where TIntermediateType :
            class,
            IIntermediateCreatableType<TCtor, TIntermediateCtor, TType, TIntermediateType>,
            TType
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateConstructorMemberDictionary{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// with the <paramref name="master"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMember, TIntermediateMember}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateType"/>
        /// which contains the <see cref="IntermediateConstructorMemberDictionary{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>.</param>
        protected IntermediateConstructorMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateType parent) :
            base(master, parent)
        {
        }
        /// <summary>
        /// Creates a new <see cref="IntermediateGroupedSignatureMemberDictionary{TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/> with the 
        /// <paramref name="master"/>, <paramref name="parent"/> and <paramref name="root"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMember, TIntermediateMember}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateType"/>
        /// which contains the <see cref="IntermediateConstructorMemberDictionary{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>.</param>
        /// <param name="root">The <see cref="IntermediateConstructorMemberDictionary{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
        /// which the current is based upon.</param>
        protected IntermediateConstructorMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateType parent, IntermediateConstructorMemberDictionary<TCtor, TIntermediateCtor, TType, TIntermediateType> root) :
            base(master, parent, root)
        {
        }

        #region IIntermediateConstructorMemberDictionary<TCtor,TIntermediateCtor,TType,TIntermediateType> Members

        /// <summary>
        /// Adds a <typeparamref name="TIntermediateCtor"/>
        /// with the <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="parameters">The <see cref="TypedNameSeries"/>
        /// which identifies all of the parameters by name and type.</param>
        /// <returns>A <typeparamref name="TIntermediateCtor"/> instance with
        /// the <paramref name="parameters"/> specified.</returns>
        /// <exception cref="System.ArgumentException">The provided set of <paramref name="parameters"/>
        /// exists already, or a name of one of the parameters is null.</exception>
        public new TIntermediateCtor Add(TypedNameSeries parameters)
        {
            TIntermediateCtor item = this.GetConstructor();
            TypedName[] adjustedParameters = new TypedName[parameters.Count];
            Parallel.For(0, adjustedParameters.Length, i =>
            {
                var currentParam = parameters[i];
                var kind = currentParam.GetTypeRef();
                if (kind.ContainsSymbols())
                    kind = kind.SimpleSymbolDisambiguation(this.Parent);

                adjustedParameters[i] = new TypedName(currentParam.Name, kind, currentParam.Direction);
            });
            item.Parameters.AddRange(adjustedParameters);
            if (this.ContainsKey(item.UniqueIdentifier))
                throw new ArgumentException("parameters");
            this.AddDeclaration(item);
            return item;
        }

        /// <summary>
        /// Adds a <typeparamref name="TIntermediateCtor"/>
        /// with the <paramref name="parameters"/> provided.
        /// </summary>
        /// <param name="parameters">An array of <see cref="TypedName"/>
        /// instances which identify all parameters by type and name.</param>
        /// <returns>A <typeparamref name="TIntermediateCtor"/> instance with
        /// the <paramref name="parameters"/> specified.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="parameters"/> is null</exception>
        public new TIntermediateCtor Add(params TypedName[] parameters)
        {
            return this.Add(new TypedNameSeries(parameters));
        }

        #endregion

        #region IIntermediateConstructorMemberDictionary Members

        IIntermediateConstructorMember IIntermediateConstructorMemberDictionary.Add(TypedNameSeries parameters)
        {
            return this.Add(parameters);
        }

        IIntermediateConstructorMember IIntermediateConstructorMemberDictionary.Add(params TypedName[] parameters)
        {
            return this.Add(parameters);
        }

        #endregion

    }
}
