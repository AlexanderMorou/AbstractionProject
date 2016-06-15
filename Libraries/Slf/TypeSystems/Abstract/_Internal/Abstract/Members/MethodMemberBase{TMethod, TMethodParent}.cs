using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    /// <summary>
    /// Provides a root partial implementation of an <see cref="IMethodMember{TMethod, TMethodParent}"/>.
    /// </summary>
    /// <typeparam name="TMethod">The type of <see cref="IMethodMember{TMethod, TMethodParent}"/> in the
    /// current implementation.</typeparam>
    /// <typeparam name="TMethodParent">The type of <see cref="IMethodParent{TMethod, TMethodParent}"/> in the current
    /// implementation.</typeparam>
    internal abstract class MethodMemberBase<TMethod, TMethodParent> :
        MethodSignatureMemberBase<IMethodParameterMember<TMethod, TMethodParent>, TMethod, TMethodParent>,
        IMethodMember<TMethod, TMethodParent>
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {
        //private IGenericParameterDictionary<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember> typeParameters;

        public MethodMemberBase(TMethodParent parent)
            : base(parent)
        {
        }

        #region IGenericParamParent Members

        IGenericParameterDictionary IGenericParamParent.TypeParameters
        {
            get { return (IGenericParameterDictionary)this.TypeParameters; }
        }

        #endregion

        #region IScopedDeclaration Members

        /// <summary>
        /// Returns the access level of the <see cref="MethodMemberBase{TMethod, TMethodParent}"/>.
        /// </summary>
        public AccessLevelModifiers AccessLevel
        {
            get { return this.AccessLevelImpl; }
        }

        protected abstract AccessLevelModifiers AccessLevelImpl { get; }

        #endregion

    }
}
