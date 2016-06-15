using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Expressions
{
    public class ConstructorReferenceStub :
        IConstructorReferenceStub
    {
        public ConstructorReferenceStub(IType instanceType)
        {
            this.InstanceType = instanceType;
        }

        #region IConstructorReferenceStub Members

        /// <summary>
        /// Returns/sets the type of instance to create.
        /// </summary>
        public IType InstanceType { get; set; }

        /// <summary>
        /// Obtains a <see cref="IConstructorInvokeExpression"/>
        /// by evaluating the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="parameters">A series of 
        /// <see cref="IExpression"/> elements 
        /// which relate to the data of the 
        /// parameters of the invoke, and the 
        /// types of the parameters.</param>
        /// <returns>A new <see cref="IConstructorInvokeExpression"/> 
        /// relative to the signature and data of 
        /// the <paramref name="parameters"/> 
        /// provided.</returns>
        public IConstructorInvokeExpression Invoke(IExpressionCollection parameters)
        {
            return new ConstructorInvokeExpression(new ConstructorPointerReferenceExpression(this), parameters);
        }

        /// <summary>
        /// Obtains a <see cref="IConstructorInvokeExpression"/>
        /// by evaluating the <paramref name="parameters"/>
        /// provided.
        /// </summary>
        /// <param name="parameters">A series of
        /// <see cref="IExpression"/> elements 
        /// which relate to the data of the 
        /// parameters of the invoke, and the types 
        /// of the parameters.</param>
        /// <returns>A new <see cref="IConstructorInvokeExpression"/> 
        /// relative to the signature and data of 
        /// the <paramref name="parameters"/> 
        /// provided.</returns>
        public IConstructorInvokeExpression Invoke(params IExpression[] parameters)
        {
            return new ConstructorInvokeExpression(new ConstructorPointerReferenceExpression(this), parameters);
        }

        /// <summary>
        /// Obtains a <see cref="IConstructorPointerReferenceExpression"/>
        /// with the <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="signature">The <see cref="ITypeCollection"/>
        /// relative to the type-signature of the <see cref="IMethodPointerReferenceExpression"/>
        /// to obtain.</param>
        /// <returns>A new <see cref="IConstructorPointerReferenceExpression"/>
        /// relative to the <paramref name="signature"/>
        /// provided.</returns>
        public IConstructorPointerReferenceExpression GetPointer(ITypeCollection signature)
        {
            return new ConstructorPointerReferenceExpression(this, signature);
        }

        /// <summary>
        /// Obtains a <see cref="IConstructorPointerReferenceExpression"/>
        /// with the <paramref name="signature"/> provided.
        /// </summary>
        /// <param name="signature">The series if <see cref="IType"/>
        /// elements relative to the type-signature of the 
        /// <see cref="IMethodPointerReferenceExpression"/>
        /// to obtain.</param>
        /// <returns>A new <see cref="IConstructorPointerReferenceExpression"/>
        /// relative to the <paramref name="signature"/>
        /// provided.</returns>
        public IConstructorPointerReferenceExpression GetPointer(params IType[] signature)
        {
            return new ConstructorPointerReferenceExpression(this, signature);
        }

        #endregion
    }
}
