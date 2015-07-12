using System;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// The target of a series of constructor cascade arguments.
    /// </summary>
    [Serializable]
    public enum ConstructorCascadeTarget
    {
        /// <summary>
        /// The constructor cascade isn't used.
        /// </summary>
        Undefined,
        /// <summary>
        /// The constructor cascade should occur on the base-type.
        /// </summary>
        Base,
        /// <summary>
        /// The constructor cascade should occur on the current type.
        /// </summary>
        This
    }
    /* *
     * The difference between the constructor and constructor signature members:
     * One has code, the other does not, and one has cascade target awareness, and
     * the other does not for the same reasons.
     * */

    /// <summary>
    /// Defines generic properties and methods for working with an intermediate
    /// constructor member.
    /// </summary>
    /// <typeparam name="TCtor">The type of the constructor in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCtor">The type of the constructor in the intermediate type system.</typeparam>
    /// <typeparam name="TType">The type of the owning <see cref="ICreatableParent{TCtor, TIntermediateType}"/> in 
    /// the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The type of the owning <see cref="IIntermediateCreatableParent{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    public interface IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType> :
        IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>,
        IIntermediateConstructorMember
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            TCtor,
            IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TType :
            ICreatableParent<TCtor, TType>
        where TIntermediateType :
            TType,
            IIntermediateCreatableParent<TCtor, TIntermediateCtor, TType, TIntermediateType>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with an intermediate constructor member.
    /// </summary>
    public interface IIntermediateConstructorMember :
        IIntermediateConstructorSignatureMember,
        ITopBlockStatement
    {
        /// <summary>
        /// Returns the <see cref="ConstructorCascadeTarget"/>
        /// which designates whether the <see cref="CascadeMembers"/> 
        /// target the base class or the active class.
        /// </summary>
        ConstructorCascadeTarget CascadeTarget { get; set; }
        /// <summary>
        /// The <see cref="ICallParameterSet"/>
        /// which denotes the expressions to use to call the
        /// parent/local constructor.
        /// </summary>
        ICallParameterSet CascadeMembers { get; }
        /// <summary>
        /// Returns whether the <see cref="IIntermediateConstructorMember"/> is static.
        /// </summary>
        bool IsStaticConstructor { get; }
    }
}
