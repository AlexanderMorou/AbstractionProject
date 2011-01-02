using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    partial class IntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent, TMethodMember>
        where TIndexer :
            IIndexerMember<TIndexer, TIndexerParent>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        where TIndexerParent :
            IIndexerParent<TIndexer, TIndexerParent>
        where TIntermediateIndexerParent :
            TIndexerParent,
            IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
        where TMethodMember :
            class,
            IIntermediatePropertyMethodMember
    {
        protected partial class ParameterMembersDictionary :
            IntermediateParameterMemberDictionary<TIndexer, TIntermediateIndexer, IIndexerParameterMember<TIndexer, TIndexerParent>, IIntermediateIndexerParameterMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>>
        {
            /// <summary>
            /// Creates a new <see cref="ParameterMembersDictionary"/> with
            /// the <paramref name="parent"/> provided.
            /// </summary>
            /// <param name="parent">The <typeparamref name="TIntermediateIndexer"/>
            /// which contains the <see cref="ParameterMembersDictionary"/>.</param>
            public ParameterMembersDictionary(TIntermediateIndexer parent)
                : base(parent)
            {
            }
            /// <summary>
            /// Obtains a new <see cref="IIntermediateIndexerParameterMember{TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent}"/>
            /// instance with the <paramref name="name"/>, <paramref name="parameterType"/> and 
            /// <paramref name="direction"/> provided.
            /// </summary>
            /// <param name="name">The <see cref="String"/> value representing the unique name of the parameter.</param>
            /// <param name="parameterType">The <see cref="IType"/> representing the kind of values acceptable for the type.</param>
            /// <param name="direction">The <see cref="ParameterDirection"/> which determines whether the argument must be passed
            /// by reference, by value or must be initialized before the indexer exits.</param>
            /// <returns></returns>
            protected override IIntermediateIndexerParameterMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> GetNewParameter(string name, IType parameterType, ParameterDirection direction)
            {
                return new ParameterMember(this.Parent)
                {
                    ParameterType = parameterType,
                    Name = name,
                    Direction = direction
                };
            }
        }
    }
}
