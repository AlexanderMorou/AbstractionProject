using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;

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
