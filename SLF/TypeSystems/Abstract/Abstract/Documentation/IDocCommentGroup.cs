using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract.Documentation
{
    /// <summary>
    /// Defines properties and methods for working with a series of
    /// sections; with a <typeparamref name="TKey"/> provided for
    /// selecting the section, a <typeparamref name="TItem"/> that
    /// denotes the information pertinent to the section, and
    /// the <typeparamref name="TSection"/> that describes the
    /// <typeparamref name="TItem"/>.
    /// </summary>
    /// <typeparam name="TKey">The kind of key used to select
    /// a given <typeparamref name="TSection"/>.</typeparam>
    /// <typeparam name="TItem">The kind of element represented by
    /// the <typeparamref name="TSection"/>.</typeparam>
    /// <typeparam name="TSection">The type of <see cref="IDocCommentSection"/>
    /// that describes the <typeparamref name="TItem"/> elements.</typeparam>
    public interface IDocCommentGroup<TKey, TItem, TSection> :
        IControlledDictionary<TKey, TSection>
        where TItem :
            IDeclaration
        where TSection :
            IDocCommentItemedSection<TItem>
    {
    }
}
