using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public sealed class IntermediateSegmentableTypePartCollection<TTypeIdentifier, TType, TIntermediateType, TSpecificIntermediateType> :
        IntermediateSegmentableDeclarationParts<TTypeIdentifier, TIntermediateType, TSpecificIntermediateType>,
        IIntermediateSegmentableDeclarationPartCollection<TTypeIdentifier, TIntermediateType>
        where TTypeIdentifier :
            ITypeUniqueIdentifier<TTypeIdentifier>
        where TType :
            class,
            IType<TTypeIdentifier, TType>
        where TIntermediateType :
            class,
            IIntermediateSegmentableType<TTypeIdentifier, TType, TIntermediateType>,
            TType
        where TSpecificIntermediateType :
            class,
            TIntermediateType
    {

        public IntermediateSegmentableTypePartCollection(TSpecificIntermediateType root, Func<TSpecificIntermediateType, IIntermediateTypeParent, TSpecificIntermediateType> creator)
            : base(root, new GetNewTHolder(root, creator).GetNewT)
        {
        }

        private class GetNewTHolder
        {
            private TSpecificIntermediateType root;
            private Func<TSpecificIntermediateType, IIntermediateTypeParent, TSpecificIntermediateType> creator;
            /* *
             * Lambda closure and associated local hoist avoided here
             * to simplify the underlying model.  No need to include
             * extra steps to assign the locals.
             * */
            internal GetNewTHolder(TSpecificIntermediateType root, Func<TSpecificIntermediateType, IIntermediateTypeParent, TSpecificIntermediateType> creator)
            {
                this.root = root;
                this.creator = creator;
            }

            internal TSpecificIntermediateType GetNewT()
            {
                var sParent = root.Parent as IIntermediateSegmentableDeclaration;
                /* *
                 * The point of segmenting a type is to enable it to 
                 * span multiple files.  As such, to ensure that full
                 * support for this is enabled all parents are broken
                 * up as well, which means namespaces, assemblies, and
                 * other intermediate types which allow nesting.
                 * *
                 * To that end, just try to create a new partial of 
                 * the parent and go from there.
                 * *
                 * An extra type-check on the new parent is added to
                 * ensure they follow the rules of the architecture,
                 * in case they do not, this code should still not 
                 * fail.
                 * */
                if (sParent != null)
                    sParent = sParent.Parts.Add();
                var pParent = (sParent == null ? this.root.Parent : (sParent as IIntermediateTypeParent) ?? this.root.Parent);
                return creator(this.root, pParent);
            }
        }
    }
}
