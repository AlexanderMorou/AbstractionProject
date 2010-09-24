using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a series of <typeparamref name="TProperty"/> 
    /// signature members.
    /// </summary>
    /// <typeparam name="TProperty">The type of property signature used in the current implementation.</typeparam>
    /// <typeparam name="TPropertyParent">The type of parent that contains the <see cref="IPropertySignatureMember"/> 
    /// instances in the current implementation.</typeparam>
    public interface IPropertySignatureMemberDictionary<TProperty, TPropertyParent> :
        IGroupedMemberDictionary<TPropertyParent, TProperty>
        where TProperty :
            IPropertySignatureMember<TProperty, TPropertyParent>
        where TPropertyParent :
            IPropertySignatureParentType<TProperty, TPropertyParent>
    {

    }
    /// <summary>
    /// Defines properties and methods for working with a series of 
    /// <see cref="IPropertySignatureMember"/> instances.
    /// </summary>
    public interface IPropertySignatureMemberDictionary :
        IGroupedMemberDictionary
    {
    }
}
