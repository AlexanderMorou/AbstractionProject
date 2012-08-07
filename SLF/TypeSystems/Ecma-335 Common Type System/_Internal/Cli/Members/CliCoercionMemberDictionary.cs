using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    /// <summary>
    /// Defines properties and methods for working with a series of coercion members
    /// of a given type.
    /// </summary>
    /// <typeparam name="TCoercion">
    /// The type of <see cref="ICoercionMember"/> used 
    /// in the current implementation.</typeparam>
    /// <typeparam name="TCoercionIdentifier">The kind of unique identifier to use
    /// relative to the coercion member, to differentiate it from other
    /// similar coercions, and other members alike.</typeparam>
    /// <typeparam name="TCoercionParentIdentifier">The kind of unique identifier to use relative
    /// to the type, to differentiate it between its sibling types.</typeparam>
    /// <typeparam name="TCoercionParent">
    /// The type of coercible <see cref="IType{TTypeIdentifier, TType}"/> 
    /// that contains <typeparamref name="TCoercion"/> 
    /// members in the current implementation.</typeparam>
    internal abstract class CliCoercionMemberDictionary<TCoercionIdentifier, TCoercion, TCoercionParent> :
        CliMetadataDrivenDictionary<TCoercionIdentifier, ICliMetadataMethodDefinitionTableRow, TCoercion>,
        IGroupedMemberDictionary<TCoercionParent, TCoercionIdentifier, TCoercion>
        where TCoercionIdentifier :
            class,
            IGeneralMemberUniqueIdentifier,
            IMemberUniqueIdentifier
        where TCoercion :
            class,
            ICoercionMember<TCoercionIdentifier, TCoercion, TCoercionParent>
        where TCoercionParent :
            ICoercibleType<TCoercionIdentifier, TCoercion, TCoercionParent>

    {
        public CliCoercionMemberDictionary(ICliMetadataMethodDefinitionTableRow[] filteredSet, TCoercionParent parent)
            : base(filteredSet)
        {
            this.Parent = parent;
        }

        #region ISubordinateDictionary<TCoercionIdentifier,IGeneralMemberUniqueIdentifier,TCoercion,IMember> Members

        public IMasterDictionary<IGeneralMemberUniqueIdentifier, IMember> Master
        {
            get { return this.Parent.Members; }
        }

        #endregion

        #region IMemberDictionary<TCoercionParent,TCoercionIdentifier,TCoercion> Members

        public TCoercionParent Parent { get; private set; }

        #endregion
    }
}
