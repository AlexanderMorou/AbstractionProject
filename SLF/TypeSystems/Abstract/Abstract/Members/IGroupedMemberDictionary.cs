﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// Defines generic properties and methods for working with a series of <typeparamref name="TItem"/> 
    /// instances as a member of a <typeparamref name="TParent"/> instance which are grouped into a larger
    /// series of <see cref="IFullMemberDictionary"/>.
    /// </summary>
    /// <typeparam name="TParent">The type of the parent that contains the <typeparamref name="TItem"/>
    /// instances in the current implementation.</typeparam>
    /// <typeparam name="TItem">The type of <see cref="IMember{TParent}"/> contained within the 
    /// current <see cref="IGroupedMemberDictionary{TParent, TItem}"/> implementation.</typeparam>
    public interface IGroupedMemberDictionary<TParent, TItem> :
        ISubordinateDictionary<string, TItem, IMember>,
        IGroupedDeclarationDictionary<TItem>,
        IMemberDictionary<TParent, TItem>
        where TParent :
            IMemberParent
        where TItem :
            IMember<TParent>
    {
    }
    /// <summary>
    /// Defines properties and methods for working 
    /// with a series of <see cref="IMember"/> 
    /// instances which are grouped into a larger
    /// series of <see cref="IFullMemberDictionary"/>.
    /// </summary>
    public interface IGroupedMemberDictionary :
        IMemberDictionary
    {
    }
}