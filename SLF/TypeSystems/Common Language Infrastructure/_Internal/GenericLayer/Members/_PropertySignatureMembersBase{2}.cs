using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract class _PropertySignatureMembersBase<TProperty, TPropertyParent> :
        _GroupedMembersBase<TPropertyParent, TProperty, IPropertySignatureMemberDictionary<TProperty, TPropertyParent>>,
        IPropertySignatureMemberDictionary<TProperty, TPropertyParent>,
        IPropertySignatureMemberDictionary
        where TProperty :
            class,
            IPropertySignatureMember<TProperty, TPropertyParent>
        where TPropertyParent :
            class,
            IPropertySignatureParentType<TProperty, TPropertyParent>
    {
        public _PropertySignatureMembersBase(_FullMembersBase master, IPropertySignatureMemberDictionary<TProperty, TPropertyParent> originalSet, TPropertyParent parent)
            : base(master, originalSet, (IGenericType)parent)
        {

        }
    }
}
