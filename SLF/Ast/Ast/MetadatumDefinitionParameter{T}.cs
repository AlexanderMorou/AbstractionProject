using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides a base class for custom attribute constructor parameters.
    /// </summary>
    /// <typeparam name="T">The type of value the <see cref="MetadatumDefinitionParameter{T}"/>
    /// is.</typeparam>
    public class MetadatumDefinitionParameter<T> :
        IMetadatumDefinitionParameter<T>
    {
        private T value;
        private IType valueType;
        internal MetadatumDefinitionParameterCollection Owner { get; private set; }
        /// <summary>
        /// Creates a new <see cref="MetadatumDefinitionParameter{T}"/> with the
        /// <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <typeparamref name="T"/> instance
        /// which the <see cref="MetadatumDefinitionParameter{T}"/> is typed as.</param>
        /// <param name="owner">The <see cref="MetadatumDefinitionParameterCollection"/>
        /// which contains the <see cref="MetadatumDefinitionParameter{T}"/>.</param>
        /// <param name="valueType">The <see cref="IType"/>
        /// which represents the type of <paramref name="value"/>
        /// within the current typing model.</param>
        internal MetadatumDefinitionParameter(T value, MetadatumDefinitionParameterCollection owner, IType valueType)
        {
            this.value = value;
            this.valueType = valueType;
            this.Owner = owner;
        }

        #region IMetadatumDefinitionParameter<T> Members

        /// <summary>
        /// Returns/sets the <typeparamref name="T"/> 
        /// value defined on one of the 
        /// <see cref="MetadatumDefinition"/>'s
        /// constructor argument(s).
        /// </summary>
        public T Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (this.value.Equals(value))
                    return;
                this.value = value;
                if (this.Owner != null)
                    this.Owner.OnItemValueChanged(this);
            }
        }

        #endregion

        #region IMetadatumDefinitionParameter Members

        object IMetadatumDefinitionParameter.Value
        {
            get { return this.Value; }
        }

        #endregion

        #region IDisposable Members

        public virtual void Dispose()
        {
            try
            {
                this.Owner = null;
                this.value = default(T);
            }
            finally
            {
                GC.SuppressFinalize(this);
            }
        }

        #endregion
        public override string ToString()
        {
            return this.Value.ToString();
        }

        public IType ParameterType
        {
            get { return this.valueType; }
        }
    }
}
