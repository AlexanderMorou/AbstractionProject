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
    internal abstract class _PropertyMembersBase<TProperty, TPropertyParent> :
        _GroupedMembersBase<TPropertyParent, TProperty, IPropertyMemberDictionary<TProperty, TPropertyParent>>,
        IPropertyMemberDictionary<TProperty, TPropertyParent>,
        IPropertyMemberDictionary
        where TProperty :
            class,
            IPropertyMember<TProperty, TPropertyParent>
        where TPropertyParent :
            class,
            IPropertyParentType<TProperty, TPropertyParent>
    {
        public _PropertyMembersBase(_FullMembersBase master, IPropertyMemberDictionary<TProperty, TPropertyParent> originalSet, TPropertyParent parent)
            : base(master, originalSet, parent)
        {

        }

        #region IPropertyMemberDictionary Members

        IPropertyParentType IPropertyMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        #endregion

    }
}
