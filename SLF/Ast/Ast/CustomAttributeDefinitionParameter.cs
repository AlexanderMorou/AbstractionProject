﻿using System;
using System.Collections.Generic;
using System.Text;
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
    /// <typeparam name="T">The type of value the <see cref="CustomAttributeDefinitionParameter{T}"/>
    /// is.</typeparam>
    public class CustomAttributeDefinitionParameter<T> :
        ICustomAttributeDefinitionParameter<T>
    {
        private T value;
        internal CustomAttributeDefinitionParameterCollection Owner { get; private set; }
        /// <summary>
        /// Creates a new <see cref="CustomAttributeDefinitionParameter{T}"/> with the
        /// <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The <typeparamref name="T"/> instance
        /// which the <see cref="CustomAttributeDefinitionParameter{T}"/> is typed as.</param>
        /// <param name="owner">The <see cref="CustomAttributeDefinitionParameterCollection"/>
        /// which contains the <see cref="CustomAttributeDefinitionParameter{T}"/>.</param>
        internal CustomAttributeDefinitionParameter(T value, CustomAttributeDefinitionParameterCollection owner)
        {
            this.value = value;
            this.Owner = owner;
        }

        #region ICustomAttributeDefinitionParameter<T> Members

        /// <summary>
        /// Returns/sets the <typeparamref name="T"/> 
        /// value defined on one of the 
        /// <see cref="CustomAttributeDefinition"/>'s
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

        #region ICustomAttributeDefinitionParameter Members

        object ICustomAttributeDefinitionParameter.Value
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
    }
}