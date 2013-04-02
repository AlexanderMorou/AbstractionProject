using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    partial class IntermediateMethodSignatureMemberBase<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TSignature :
            class,
            IMethodSignatureMember<TSignature, TParent>
        where TIntermediateSignature :
            IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
            TSignature
        where TParent :
            IMethodSignatureParent<TSignature, TParent>
        where TIntermediateParent :
            IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
            TParent
    {
        /// <summary>
        /// Provides a base class for the intermediate method signature member parameters to derive from.
        /// </summary>
        protected internal new class ParameterMember :
            IntermediateMethodSignatureMemberBase<IMethodSignatureParameterMember<TSignature, TParent>, IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>, TSignature, TIntermediateSignature, TParent, TIntermediateParent>.ParameterMember,
            IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        {
            /// <summary>
            /// Creates a new <see cref="ParameterMember"/> with the 
            /// <paramref name="parent"/> provided.
            /// </summary>
            /// <param name="parent">The <typeparamref name="TIntermediateSignature"/>
            /// which owns the <see cref="ParameterMember"/>.</param>
            /// <param name="identityManager">The <see cref="ITypeIdentityManager"/> which is responsible for 
            /// marshalling type identities within the current type model.</param>
            public ParameterMember(TIntermediateSignature parent, ITypeIdentityManager identityManager)
                : base(parent, identityManager)
            {
            }
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
        protected internal class ParameterMember<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter> :
            IntermediateMethodSignatureMemberBase<IMethodSignatureParameterMember<TSignature, TParent>, IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>, TSignature, TIntermediateSignature, TParent, TIntermediateParent>.ParameterMember<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, ParameterMember<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter>>,
            IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
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
            internal ParameterMember(TIntermediateAltParameter original, TIntermediateSignature parent, ITypeIdentityManager identityManager)
                : base(original, parent, identityManager)
            {
            }
        }
    }
    partial class IntermediateMethodSignatureMemberBase<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignatureParameter :
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
            TSignatureParameter
        where TSignature :
            class,
            IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignature :
            IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
            TSignature
        where TParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TParent>
        where TIntermediateParent :
            IIntermediateSignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TParent, TIntermediateParent>,
            TParent
    {
        /// <summary>
        /// Provides a base for the method signature parameter members to derive from.
        /// </summary>
        protected internal abstract class ParameterMember :
            IntermediateSignatureParameterMemberBase<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TParent, TIntermediateParent>,
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        {
            /// <summary>
            /// Creates a new <see cref="ParameterMember"/> with the 
            /// <paramref name="parent"/> provided.
            /// </summary>
            /// <param name="parent">The <typeparamref name="TIntermediateSignature"/>
            /// which owns the <see cref="ParameterMember"/>.</param>
            /// <param name="identityManager">The <see cref="ITypeIdentityManager"/> which is responsible for 
            /// marshalling type identities within the current type model.</param>
            protected ParameterMember(TIntermediateSignature parent, ITypeIdentityManager identityManager)
                : base(parent, identityManager)
            {
            }

            #region IMethodSignatureParameterMember Members

            IMethodSignatureMember IMethodSignatureParameterMember.Parent
            {
                get { return this.Parent; }
            }

            #endregion
        }

        /// <summary>
        /// Provides a base class for a parameter of a method to mirror
        /// a parameter from a different kind of member.
        /// </summary>
        /// <remarks>Used to aid in indexer/event method
        /// parameter replication.</remarks>
        /// <typeparam name="TAltParent">The kind of parent which contains the 
        /// set of parameters that the current member mirrors a copy of one of.</typeparam>
        /// <typeparam name="TIntermediateAltParent">The kind of parent which
        /// contains the set of parameters that the current member mirrors a copy
        /// of one of, in the intermediate context.</typeparam>
        /// <typeparam name="TAltParameter">The kind of parameter that is mirrored by the 
        /// <see cref="ParameterMember{TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter}"/>.
        /// </typeparam>
        /// <typeparam name="TIntermediateAltParameter">The kind of intermediate parameter that is 
        /// mirrored by the <see cref="ParameterMember{TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter}"/>.
        /// </typeparam>
        /// <typeparam name="TWrapperParameter">The kind of parameter that derives from
        /// <typeparamref name="TIntermediateSignatureParameter"/> that acts
        /// as the mirrored parameter and derives directly from
        /// <see cref="ParameterMember{TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter}"/>.
        /// </typeparam>
        protected internal abstract class ParameterMember<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter> :
            ParameterMember,
            ParameterDictionary<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter>.IWrapperParameter
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
            where TWrapperParameter :
                ParameterMember<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter>,
                TIntermediateSignatureParameter,
                ParameterDictionary<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter>.IWrapperParameter
        {
            /// <summary>
            /// Creates a new <see cref="ParameterMember{TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter}"/>
            /// with the <paramref name="original"/> to wrap, and the <paramref name="parent"/> that contains it.
            /// </summary>
            /// <param name="original">The <typeparamref name="TIntermediateAltParameter"/> that's wrapped by the
            /// <see cref="ParameterMember{TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter}"/>.
            /// </param>
            /// <param name="parent">The <typeparamref name="TIntermediateSignature"/>
            /// which contains the <see cref="ParameterMember{TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter}"/>.</param>
            /// <param name="identityManager">The <see cref="ITypeIdentityManager"/>
            /// which is responsible for maintaining type identity within the current type
            /// model.</param>
            public ParameterMember(TIntermediateAltParameter original, TIntermediateSignature parent, ITypeIdentityManager identityManager)
                : base(parent, identityManager)
            {
                if (original == null)
                    throw new ArgumentNullException("original");
                this.AlternateParameter = original;
            }

            #region IWrapperParameter Members

            /// <summary>
            /// The alternate parameter which is wrapped.
            /// </summary>
            public TIntermediateAltParameter AlternateParameter { get; private set; }

            #endregion

            /// <summary>
            /// Returns/sets the type that the 
            /// <see cref="ParameterMember{TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter}"/>
            /// is defined as.
            /// </summary>
            /// <remarks>Associates to the <see cref="IIntermediateParameterMember.ParameterType"/>
            /// from the <see cref="AlternateParameter"/>.</remarks>
            public override IType ParameterType
            {
                get
                {
                    return this.AlternateParameter.ParameterType;
                }
                set
                {
                    this.AlternateParameter.ParameterType = value;
                }
            }

            /// <summary>
            /// Initializes the <see cref="MetadataDefinitionCollection"/> which
            /// denotes the groups of attributes defined on
            /// the <see cref="ParameterMember{TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter}"/>.
            /// </summary>
            /// <returns>A new <see cref="MetadataDefinitionCollection"/>
            /// instance which refers to the parameters defined on the 
            /// <see cref="ParameterMember{TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter}"/>.</returns>
            /// <remarks>Links directly to the <see cref="IIntermediateMetadataEntity.Metadata"/>
            /// defined on the <see cref="AlternateParameter"/>.</remarks>
            protected override MetadataDefinitionCollection InitializeCustomAttributes()
            {
                return new MetadataDefinitionCollection((MetadataDefinitionCollection)this.AlternateParameter.Metadata, this, this.identityManager);
            }

            /// <summary>
            /// Returns/sets the direction the parameter is coerced.
            /// </summary>
            /// <remarks>Links directly to the <see cref="IIntermediateParameterMember.Direction"/> from
            /// the <see cref="AlternateParameter"/>.</remarks>
            public override ParameterCoercionDirection Direction
            {
                get
                {
                    return this.AlternateParameter.Direction;
                }
                set
                {
                    this.AlternateParameter.Direction = value;
                }
            }

            public override IGeneralMemberUniqueIdentifier UniqueIdentifier
            {
                get
                {
                    return this.AlternateParameter.UniqueIdentifier;
                }
            }

            protected override string OnGetName()
            {
                return this.AlternateParameter.Name;
            }

            protected override void OnSetName(string name)
            {
                this.AlternateParameter.Name = name;
            }
        }
    }
}
