using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
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
}
