﻿using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Oil;
using System.Reflection;
using System.Runtime.CompilerServices;
using AllenCopeland.Abstraction.Slf.CompilerServices;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public sealed class IntermediateSegmentableTypePartCollection<TType, TIntermediateType, TSpecificIntermediateType> :
        IntermediateSegmentableDeclarationParts<TIntermediateType, TSpecificIntermediateType>,
        IIntermediateSegmentableDeclarationPartCollection<TIntermediateType>
        where TType :
            class,
            IType<TType>
        where TIntermediateType :
            class,
            IIntermediateSegmentableType<TType, TIntermediateType>,
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