using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
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
        /// Initializes the <see cref="IntermediateParameterParentMemberBase{TParentIdentifier, TParent, TIntermediateParent, TParameter, TIntermediateParameter, TGrandParent, TIntermediateGrandParent}.Parameters"/> property.
        /// </summary>
        /// <returns>An instance of <see cref="ParameterDictionary"/>.</returns>
        protected override IntermediateParameterMemberDictionary<TSignature, TIntermediateSignature, IMethodSignatureParameterMember<TSignature, TParent>, IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>> InitializeParameters()
        {
            return new ParameterDictionary(((TIntermediateSignature)(object)(this)));
        }
        /// <summary>
        /// Provides a dictionary for the parameters of the 
        /// <see cref="IntermediateMethodSignatureMemberBase{TSignature, TIntermediateSignature, TParent, TIntermediateParent}"/>.
        /// </summary>
        protected class ParameterDictionary :
            IntermediateParameterMemberDictionary<TSignature, TIntermediateSignature, IMethodSignatureParameterMember<TSignature, TParent>, IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>>
        {
            /// <summary>
            /// Creates a new <see cref="ParameterDictionary"/> with the 
            /// <paramref name="parent"/> provided.
            /// </summary>
            /// <param name="parent">The <typeparamref name="TIntermediateSignature"/> which owns
            /// the <see cref="ParameterDictionary"/>.</param>
            public ParameterDictionary(TIntermediateSignature parent)
                : base(parent)
            {
            }
            /// <summary>
            /// Obtains a <see cref="ParameterMember"/> 
            /// for insertion into the <see cref="ParameterDictionary"/>.
            /// </summary>
            /// <param name="name">The name of the parameter to create.</param>
            /// <param name="parameterType">The type of the parameter to create.</param>
            /// <param name="direction">The direction in which the <see cref="ParameterMember"/>
            /// is coerced.</param>
            /// <returns>A new <see cref="ParameterMember"/> instance.</returns>
            protected override IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> GetNewParameter(string name, IType parameterType, ParameterDirection direction)
            {
                ParameterMember result = new ParameterMember(Parent);
                result.Direction = direction;
                result.ParameterType = parameterType;
                result.Name = name;
                return result;
            }
        }

        protected abstract class ParameterDictionary<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter> :
            IntermediateMethodSignatureMemberBase<IMethodSignatureParameterMember<TSignature, TParent>, IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>, TSignature, TIntermediateSignature, TParent, TIntermediateParent>.ParameterDictionary<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, ParameterMember<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter>>
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

            protected ParameterDictionary(IIntermediateParameterMemberDictionary<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter> sourceData, TIntermediateSignature parent)
                : base(sourceData, parent)
            {
            }

            protected override ParameterMember<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter> GetNewWrapperParameter(TIntermediateAltParameter original)
            {
                return new ParameterMember<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter>(original, this.Parent);
            }

            protected override IIntermediateMethodSignatureParameterMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> GetNewOriginalParameter(string name, IType parameterType, ParameterDirection direction)
            {
                ParameterMember result = new ParameterMember(Parent);
                result.Direction = direction;
                result.ParameterType = parameterType;
                result.Name = name;
                return result;
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
        protected abstract class ParameterDictionary<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter> :
            IntermediateParameterMemberDictionary<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter>
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
            /// Defines a wrapper parameter which wraps a parameter
            /// from the provided dataset.
            /// </summary>
            public interface IWrapperParameter
            {
                /// <summary>
                /// The alternate parameter which is wrapped.
                /// </summary>
                TIntermediateAltParameter AlternateParameter { get; }
            }
            private IIntermediateParameterMemberDictionary<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter> sourceData;

            /// <summary>
            /// Creates a new <see cref="ParameterDictionary{TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter}"/>
            /// with the <paramref name="sourceData"/> from which to wrap, and the 
            /// <paramref name="parent"/> that contains the dataset.
            /// </summary>
            /// <param name="sourceData"></param>
            /// <param name="parent"></param>
            public ParameterDictionary(IIntermediateParameterMemberDictionary<TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter> sourceData, TIntermediateSignature parent)
                : base(parent)
            {
                this.sourceData = sourceData;
                KeyValuePair<string, TSignatureParameter>[] sourceParameters = new KeyValuePair<string, TSignatureParameter>[sourceData.Count];
                int index = 0;
                foreach (var original in sourceData.Values)
                {
                    var wrapper = GetNewWrapperParameter(original);;
                    sourceParameters[index++] = new KeyValuePair<string, TSignatureParameter>(wrapper.UniqueIdentifier, wrapper);
                }
                base._AddRange(sourceParameters);

                sourceData.ItemAdded += SourceItemAdded;
                sourceData.ItemRemoved += SourceItemRemoved;
            }

            void SourceItemRemoved(object sender, EventArgsR1<TIntermediateAltParameter> e)
            {
                var wrapper = this.Values.FirstOrDefault(parameter =>
                {
                    var wrappedParameter = parameter as IWrapperParameter;
                    if (wrappedParameter != null && wrappedParameter.AlternateParameter == e.Arg1)
                        return true;
                    return false;
                });
                if (wrapper != null)
                    this.Remove(wrapper);
            }

            void SourceItemAdded(object sender, EventArgsR1<TIntermediateAltParameter> e)
            {
                if (e == null || e.Arg1 == null)
                    return;
                var wrappedParameter = this.GetNewWrapperParameter(e.Arg1);
                /* *
                 * Use internal add method because it's likely being injected
                 * in a locked state.  Since this dictionary represents a mirror
                 * of an existing set, altering its state to achieve that goal
                 * requires disregarding the locked state of the dictionary.
                 * */
                this._Add(wrappedParameter.UniqueIdentifier, wrappedParameter);
            }

            protected override void Dispose(bool disposing)
            {
                if (this.sourceData != null)
                {
                    sourceData.ItemAdded -= SourceItemAdded;
                    sourceData.ItemRemoved -= SourceItemRemoved;
                    this.sourceData = null;
                }
                base.Dispose(disposing);
            }

            /// <summary>
            /// Obtains a <typeparamref name="TWrapperParameter"/>
            /// </summary>
            /// <param name="original">The original <typeparamref name="TIntermediateAltParameter"/>
            /// from which the current is derived.</param>
            /// <returns>A new <typeparamref name="TWrapperParameter"/>
            /// which wraps the original.</returns>
            protected abstract TWrapperParameter GetNewWrapperParameter(TIntermediateAltParameter original);

            /// <summary>
            /// Obtains a <typeparamref name="TIntermediateSignatureParameter"/>
            /// for insertion into the <see cref="ParameterDictionary{TAltParent, TIntermediateAltParent, TAltParameter, TIntermediateAltParameter, TWrapperParameter}"/>.
            /// </summary>
            /// <param name="name">The name of the parameter to create.</param>
            /// <param name="parameterType">The type of the parameter to create.</param>
            /// <param name="direction">The direction in which the <see cref="ParameterMember"/>
            /// is coerced.</param>
            /// <returns>A new <typeparamref name="TIntermediateSignatureParameter"/> instance.</returns>
            protected override sealed TIntermediateSignatureParameter GetNewParameter(string name, IType parameterType, ParameterDirection direction)
            {
                return this.GetNewOriginalParameter(name, parameterType, direction);
            }

            protected abstract TIntermediateSignatureParameter GetNewOriginalParameter(string name, IType parameterType, ParameterDirection direction);
        }

    }
}
