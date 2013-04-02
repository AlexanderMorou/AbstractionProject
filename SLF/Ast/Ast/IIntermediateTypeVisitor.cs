using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public interface IIntermediateTypeVisitor
    {
        /// <summary>
        /// Visits the <paramref name="class"/> provided.
        /// </summary>
        /// <param name="class">The <see cref="IIntermediateClassType"/>
        /// to visit.</param>
        void Visit(IIntermediateClassType @class);
        /// <summary>
        /// Visits the <paramref name="delegate"/> provided.
        /// </summary>
        /// <param name="delegate">The <see cref="IIntermediateDelegateType"/>
        /// to visit.</param>
        void Visit(IIntermediateDelegateType @delegate);
        /// <summary>
        /// Visits the <paramref name="enum"/> provided.
        /// </summary>
        /// <param name="enum">The <see cref="IIntermediateEnumType"/>
        /// to visit.</param>
        void Visit(IIntermediateEnumType @enum);
        /// <summary>
        /// Visits the <paramref name="interface"/> provided.
        /// </summary>
        /// <param name="interface">The <see cref="IIntermediateInterfaceType"/>
        /// to visit.</param>
        void Visit(IIntermediateInterfaceType @interface);
        /// <summary>
        /// Visits the <paramref name="struct"/> provided.
        /// </summary>
        /// <param name="struct">The <see cref="IIntermediateStructType"/>
        /// to visit.</param>
        void Visit(IIntermediateStructType @struct);

        /// <summary>
        /// Visits the <paramref name="parameter"/> provided.
        /// </summary>
        /// <typeparam name="TGenericParameter">The type of generic parameter in the abstract
        /// type system.</typeparam>
        /// <typeparam name="TIntermediateGenericParameter">The type of generic parameter in
        /// the intermediate abstract syntax tree.</typeparam>
        /// <typeparam name="TParent">The type which owns the generic parameters in the abstract
        /// type system.</typeparam>
        /// <typeparam name="TIntermediateParent">The type which owns the generic parameters in the intermediate
        /// abstract syntax tree.</typeparam>
        /// <param name="parameter">The parameter to visit.</param>
        void Visit<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> parameter)
            where TGenericParameter :
                IGenericParameter<TGenericParameter, TParent>
            where TIntermediateGenericParameter :
                TGenericParameter,
                IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
            where TParent :
                IGenericParamParent<TGenericParameter, TParent>
            where TIntermediateParent :
                TParent,
                IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>;
    }
    /// <summary>
    /// Defines methods for a visitor model which yields
    /// a specific kind of result.
    /// </summary>
    /// <typeparam name="TResult">The value returned upon visiting the expression.</typeparam>
    /// <typeparam name="TContext">The type of context passed to the visitor.</typeparam>
    public interface IIntermediateTypeVisitor<TResult, TContext>
    {
        /// <summary>
        /// Visits the <paramref name="class"/> provided.
        /// </summary>
        /// <param name="class">The <see cref="IIntermediateClassType"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IIntermediateClassType @class, TContext context);
        /// <summary>
        /// Visits the <paramref name="delegate"/> provided.
        /// </summary>
        /// <param name="delegate">The <see cref="IIntermediateDelegateType"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IIntermediateDelegateType @delegate, TContext context);
        /// <summary>
        /// Visits the <paramref name="enum"/> provided.
        /// </summary>
        /// <param name="enum">The <see cref="IIntermediateEnumType"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IIntermediateEnumType @enum, TContext context);
        /// <summary>
        /// Visits the <paramref name="interface"/> provided.
        /// </summary>
        /// <param name="interface">The <see cref="IIntermediateInterfaceType"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IIntermediateInterfaceType @interface, TContext context);
        /// <summary>
        /// Visits the <paramref name="struct"/> provided.
        /// </summary>
        /// <param name="struct">The <see cref="IIntermediateStructType"/>
        /// to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit(IIntermediateStructType @struct, TContext context);

        /// <summary>
        /// Visits the <paramref name="parameter"/> provided.
        /// </summary>
        /// <typeparam name="TGenericParameter">The type of generic parameter in the abstract
        /// type system.</typeparam>
        /// <typeparam name="TIntermediateGenericParameter">The type of generic parameter in
        /// the intermediate abstract syntax tree.</typeparam>
        /// <typeparam name="TParent">The type which owns the generic parameters in the abstract
        /// type system.</typeparam>
        /// <typeparam name="TIntermediateParent">The type which owns the generic parameters in the intermediate
        /// abstract syntax tree.</typeparam>
        /// <param name="parameter">The parameter to visit.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        /// <returns>Returns the value of <typeparamref name="TResult"/>
        /// relative to the implementation of the visitor.</returns>
        TResult Visit<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> parameter, TContext context)
            where TGenericParameter :
                IGenericParameter<TGenericParameter, TParent>
            where TIntermediateGenericParameter :
                TGenericParameter,
                IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
            where TParent :
                IGenericParamParent<TGenericParameter, TParent>
            where TIntermediateParent :
                TParent,
                IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>;
    }
}
