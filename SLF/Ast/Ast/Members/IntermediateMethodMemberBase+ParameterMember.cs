using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    partial class IntermediateMethodMemberBase<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>
        where TMethod :
            class,
            IMethodMember<TMethod, TMethodParent>
        where TIntermediateMethod :
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
            TMethod
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
        where TIntermediateMethodParent :
            IIntermediateMethodParent<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
            TMethodParent
    {
        /// <summary>
        /// Provides a parameter member for a method member.
        /// </summary>
        internal protected new class ParameterMember :
            IntermediateMethodSignatureMemberBase<IMethodParameterMember<TMethod, TMethodParent>, IIntermediateMethodParameterMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>.ParameterMember,
            IIntermediateMethodParameterMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>
        {
            /// <summary>
            /// Creates a new <see cref="ParameterMember"/> with the 
            /// <paramref name="parent"/> provided.
            /// </summary>
            /// <param name="parent">The <typeparamref name="TIntermediateMethod"/>
            /// which contains the <see cref="ParameterMember"/>.</param>
            public ParameterMember(TIntermediateMethod parent)
                : base(parent, parent.Assembly)
            {
            }

            #region IMethodParameterMember Members

            IMethodMember IMethodParameterMember.Parent
            {
                get { return base.Parent; }
            }

            #endregion
        }
        
        /// <summary>
        /// Provides a base class for the intermediate method signature member parameters to derive from
        /// when there's a parameter from which the current mirrors.
        /// </summary>
        /// <typeparam name="TAltParent">The kind of parent which contains the 
        /// set of parameters that the current member mirrors a copy of one of.</typeparam>
        /// <typeparam name="TIntermediateAltParent">The kind of parent which
        /// contains the set of parameters that the current member mirrors a copy
        /// of one of, in the intermediate context.</typeparam>
        /// <typeparam name="TAltParameter">The kind of parameter that is mirrored by the 
        /// <see cref="ParameterMember{TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter}"/>.
        /// </typeparam>
        /// <typeparam name="TIntermediateAltParameter">The kind of intermediate parameter that is 
        /// mirrored by the <see cref="ParameterMember{TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter}"/>.
        /// </typeparam>
        protected class ParameterMember<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter> :
            IntermediateMethodSignatureMemberBase<IMethodParameterMember<TMethod, TMethodParent>, IIntermediateMethodParameterMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>, TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>.ParameterMember<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, ParameterMember<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter>>,
            IIntermediateMethodParameterMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>
            where TAltParent :
                IParameterParent<TAltParent, TAltParameter>
            where TIntermediateAltParent :
                IIntermediateParameterParent<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter>,
                TAltParent
            where TAltParameter :
                IParameterMember<TAltParent>
            where TIntermediateAltParameter :
                class,
                IIntermediateParameterMember<TAltParent, TIntermediateAltParent>,
                TAltParameter
        {
            internal ParameterMember(TIntermediateAltParameter original, TIntermediateMethod parent)
                : base(original, parent, parent.Assembly)
            {
            }

            #region IMethodParameterMember Members

            IMethodMember IMethodParameterMember.Parent
            {
                get { return this.Parent; }
            }

            #endregion


        }

    }
}
