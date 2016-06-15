using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Arrays;
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
        CliMetadataDrivenDictionary<TCoercionIdentifier, int, TCoercion>,
        IGroupedMemberDictionary<TCoercionParent, TCoercionIdentifier, TCoercion>,
        IGroupedMemberDictionary
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
        private CliFullMemberDictionary master;
        private TCoercionIdentifier[] filteredIdentifiers;
        public CliCoercionMemberDictionary(TCoercionParent parent, CliFullMemberDictionary master, CliMemberType memberKind)
            : base()
        {
            this.master = master;
            this.Parent = parent;
            var subset = master.ObtainSubset<TCoercionIdentifier, TCoercion>(memberKind).SplitSet();
            this.Initialize(subset.Item1);
            this.filteredIdentifiers = subset.Item2;
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

        protected override sealed TCoercion CreateElementFrom(int index, int metadata)
        {
            return (TCoercion)this.master.Values[metadata].Entry;
        }

        protected override sealed TCoercionIdentifier GetIdentifierFrom(int index, int metadata)
        {
            return this.filteredIdentifiers[index];
        }

        IMemberParent IMemberDictionary.Parent
        {
            get { return this.Parent; }
        }

        int IMemberDictionary.IndexOf(IMember member)
        {
            if (member is TCoercion)
                return this.IndexOf((TCoercion)member);
            return -1;
        }

        IMasterDictionary ISubordinateDictionary.Master
        {
            get { return (IMasterDictionary)this.Parent.Members; }
        }
    }
}
