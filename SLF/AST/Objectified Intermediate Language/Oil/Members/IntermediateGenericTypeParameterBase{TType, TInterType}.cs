using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    public class IntermediateGenericTypeParameterBase<TTypeIdentifier, TType, TIntermediateType> :
        IntermediateGenericParameterBase<IGenericTypeParameter<TTypeIdentifier, TType>, IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>, TType, TIntermediateType>,
        IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>
        where TTypeIdentifier :
            IGenericTypeUniqueIdentifier<TTypeIdentifier>
        where TType :
            IGenericType<TTypeIdentifier, TType>
        where TIntermediateType :
            class,
            IIntermediateGenericType<TTypeIdentifier, TType, TIntermediateType>,
            TType
    {

        /// <summary>
        /// Creates a new <see cref="IntermediateGenericTypeParameterBase{TTypeIdentifier, TType, TIntermediateType}"/>
        /// with the <paramref name="name"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the unique identifier of the
        /// <see cref="IntermediateGenericTypeParameterBase{TTypeIdentifier, TType, TIntermediateType}"/>.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateType"/>
        /// which contains the <see cref="IntermediateGenericTypeParameterBase{TTypeIdentifier, TType, TIntermediateType}"/>.</param>
        public IntermediateGenericTypeParameterBase(string name, TIntermediateType parent)
            : base(name, parent)
        {
        }

        #region IGenericTypeParameter Members

        IGenericType IGenericTypeParameter.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        protected override bool Equals(IGenericTypeParameter<TTypeIdentifier, TType> other)
        {
            return object.ReferenceEquals(other, this);
        }

        /// <summary>
        /// Returns/sets the <see cref="Int32"/> value representing the ordinal index of the 
        /// <see cref="IntermediateGenericTypeParameterBase{TTypeIdentifier, TType, TIntermediateType}"/>.
        /// </summary>
        public override int Position
        {
            get
            {
                /* *
                 * Just to be sure.
                 * */
                if (this.Parent.IsGenericConstruct)
                    return base.Position + this.Parent.GenericParameters.Count - this.Parent.TypeParameters.Count;
                return base.Position;
            }
            set
            {
                if (this.Parent.IsGenericConstruct)
                {
                    int gpC = this.Parent.GenericParameters.Count;
                    int baseLine = (gpC - this.Parent.TypeParameters.Count);
                    if (value < baseLine || value >= gpC)
                        throw new ArgumentOutOfRangeException("value");
                    base.Position = value - baseLine;
                }
                else
                    base.Position = value;
            }
        }
    }
}
