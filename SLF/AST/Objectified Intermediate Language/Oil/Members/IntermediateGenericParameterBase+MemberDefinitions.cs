using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Properties;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    partial class IntermediateGenericParameterBase<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
        where TGenericParameter :
            class,
            IGenericParameter<TGenericParameter, TParent>
        where TIntermediateGenericParameter :
            class,
            IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>,
            TGenericParameter
        where TParent :
            IGenericParamParent<TGenericParameter, TParent>
        where TIntermediateParent :
            class,
            IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>,
            TParent
    {
        /// <summary>
        /// Provides a constructor member for the <see cref="IntermediateGenericParameterBase{TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent}"/>.
        /// </summary>
        protected class ConstructorMember :
            IntermediateConstructorSignatureMemberBase<IGenericParameterConstructorMember<TGenericParameter>, IIntermediateGenericParameterConstructorMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterConstructorMember<TGenericParameter, TIntermediateGenericParameter>
        {
            /// <summary>
            /// Creates a new <see cref="ConstructorMember"/> with the
            /// <paramref name="parent"/> provided.
            /// </summary>
            /// <param name="parent">The <typeparamref name="TIntermediateGenericParameter"/>
            /// in which the <see cref="ConstructorMember"/> is contained.</param>
            public ConstructorMember(TIntermediateGenericParameter parent)
                : base(parent)
            {
            }

            /// <summary>
            /// Returns <see cref="AccessLevelModifiers.Public"/>;
            /// additionally,
            /// </summary>
            /// <exception cref="System.InvalidOperationException">The acces level cannot be set on generic parameter
            /// constructors; occurs when set through <see cref="IIntermediateScopedDeclaration.AccessLevel"/>.</exception>
            public override AccessLevelModifiers AccessLevel
            {
                get
                {
                    return AccessLevelModifiers.Public;
                }
                set
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public class MethodMember :
            IntermediateMethodSignatureMemberBase<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>,
            IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>
        {
            internal MethodMember(string name, TIntermediateGenericParameter parent)
                : base(name, parent)
            {
            }

            internal MethodMember(TIntermediateGenericParameter parent)
                : base(parent)
            {
            }

            protected override IntermediateParameterMemberDictionary<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, IMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>, IIntermediateMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>> InitializeParameters()
            {
                return base.InitializeParameters();
            }

            protected override IGenericParameterMethodMember<TGenericParameter> OnMakeGenericMethod(ITypeCollectionBase genericReplacements)
            {
                return new _Internal.GenericLayer.Members._GenericParameterMethodMemberBase<TGenericParameter>(this, genericReplacements);
            }

        }

        public class PropertyMember :
            IntermediatePropertySignatureMember<IGenericParameterPropertyMember<TGenericParameter>, IIntermediateGenericParameterPropertyMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter, PropertyMember.PropertyMethodMember>,
            IIntermediateGenericParameterPropertyMember<TGenericParameter, TIntermediateGenericParameter>
        {

            /// <summary>
            /// Creates a new <see cref="PropertyMember"/> with the <paramref name="name"/>,
            /// and <paramref name="parent"/> provided.
            /// </summary>
            /// <param name="name">The <see cref="String"/> value that denotes the unique
            /// name of the property.</param>
            /// <param name="parent">The <typeparamref name="TIntermediateGenericParameter"/>
            /// which contains the <see cref="PropertyMember"/>.</param>
            internal PropertyMember(string name, TIntermediateGenericParameter parent)
                : base(name, parent)
            {

            }

            protected override PropertyMember.PropertyMethodMember GetMethodSignatureMember(PropertyMethodType methodType)
            {
                switch (methodType)
                {
                    case PropertyMethodType.SetMethod:
                        return new PropertySetMethodMember(this.Parent, this);
                    case PropertyMethodType.GetMethod:
                    default:
                        return new PropertyMethodMember(PropertyMethodType.GetMethod, this.Parent, this);
                }
            }

            public override void Visit(IIntermediateMemberVisitor visitor)
            {
                visitor.Visit(this);
            }

            public sealed class PropertySetMethodMember :
                PropertyMethodMember,
                IIntermediatePropertySignatureSetMethodMember
            {
                private IIntermediateSignatureParameterMember valueParameter;

                public PropertySetMethodMember(TIntermediateGenericParameter parent, PropertyMember owner)
                    : base(PropertyMethodType.SetMethod, parent, owner)
                {
                }

                #region IIntermediatePropertySignatureSetMethodMember Members

                public IIntermediateSignatureParameterMember ValueParameter
                {
                    get
                    {
                        if (this.valueParameter == null)
                            return this.Parameters["value"];
                        return this.valueParameter;
                    }
                }

                #endregion

                protected override IntermediateParameterMemberDictionary<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, IMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>, IIntermediateMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>> InitializeParameters()
                {
                    var result = base.InitializeParameters();
                    this.valueParameter = result._Add(new TypedName("value", this.Owner.PropertyType));
                    return result;
                }
            }

            public class PropertyMethodMember :
                MethodMember, 
                IIntermediatePropertySignatureMethodMember
            {
                public PropertyMethodMember(PropertyMethodType methodType, TIntermediateGenericParameter parent, PropertyMember owner)
                    : base(parent)
                {
                    this.Owner = owner;
                    this.MethodType = methodType;
                }
                #region IPropertySignatureMethodMember Members

                public PropertyMethodType MethodType { get; private set; }

                #endregion

                protected override IntermediateParameterMemberDictionary<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, IMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>, IIntermediateMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>> InitializeParameters()
                {
                    var result = base.InitializeParameters();
                    result.Lock();
                    return result;
                }

                protected override IntermediateMethodSignatureMemberBase<IMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>, IIntermediateMethodSignatureParameterMember<IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>, IGenericParameterMethodMember<TGenericParameter>, IIntermediateGenericParameterMethodMember<TGenericParameter, TIntermediateGenericParameter>, TGenericParameter, TIntermediateGenericParameter>.TypeParameterDictionary InitializeTypeParameters()
                {
                    var result = base.InitializeTypeParameters();
                    result.Lock();
                    return result;
                }

                protected override string OnGetName()
                {
                    switch (this.MethodType)
                    {
                        case PropertyMethodType.SetMethod:
                            return string.Format("set_{0}", this.Owner.Name);
                        default:
                        case PropertyMethodType.GetMethod:
                            return string.Format("get_{0}", this.Owner.Name);
                    }
                }

                protected override sealed void OnSetName(string name)
                {
                    throw new NotSupportedException(string.Format("Cannot set the name of the {0} method of a property.", MethodType == PropertyMethodType.SetMethod ? "set" : "get"));
                }

                protected PropertyMember Owner { get; private set; }
            }
        }

    }
}
