using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /* *
     * The difference between the constructor and constructor signature members:
     * One has code, the other does not.
     * */

    /// <summary>
    /// Defines generic properties and methods for working with an intermediate
    /// constructor member.
    /// </summary>
    /// <typeparam name="TCtor">The type of the constructor in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateCtor">The type of the constructor in the intermediate type system.</typeparam>
    /// <typeparam name="TType">The type of the owning <see cref="ICreatableType{TCtor, TIntermediateType}"/> in 
    /// the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The type of the owning <see cref="IIntermediateCreatableType{TCtor, TIntermediateCtor, TType, TIntermediateType}"/>
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
            ICreatableType<TCtor, TType>
        where TIntermediateType :
            TType,
            IIntermediateCreatableType<TCtor, TIntermediateCtor, TType, TIntermediateType>
    {
    }
    /// <summary>
    /// Defines properties and methods for working with an intermediate constructor member.
    /// </summary>
    public interface IIntermediateConstructorMember :
        IIntermediateConstructorSignatureMember,
        ITopBlockStatement
    {
    }
}
