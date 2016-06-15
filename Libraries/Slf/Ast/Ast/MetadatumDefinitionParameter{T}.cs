using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
        PrimitiveExpression<T>,
        IMetadatumDefinitionParameter<T>
    {
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
            : base(value)
        {
            this.valueType = valueType;
            this.Owner = owner;
        }

        #region IDisposable Members

        public virtual void Dispose()
        {
            try
            {
                this.Owner = null;
                base.Value = default(T);
            }
            finally
            {
                GC.SuppressFinalize(this);
            }
        }

        #endregion

        public IType ParameterType
        {
            get { return this.valueType; }
        }

        #region IPrimitiveExpression Members

        object IPrimitiveExpression.Value
        {
            get
            {
                return this.Value;
            }
            set
            {
                if (value is T)
                    base.Value = (T)value;
                throw new ArgumentException(string.Format("Invalid type for value, should be '{0}', but got a(n) '{1}'.", typeof(T), value == null ? "null" : value.GetType().FullName), "value");
            }
        }

        object IMetadatumDefinitionParameter.Value
        {
            get
            {
                return this.Value;
            }
            set
            {
                if (value is T)
                    base.Value = (T)value;
                throw new ArgumentException(string.Format("Invalid type for value, should be '{0}', but got a(n) '{1}'.", typeof(T), value == null ? "null" : value.GetType().FullName), "value");
            }
        }

        #endregion
    }
}
