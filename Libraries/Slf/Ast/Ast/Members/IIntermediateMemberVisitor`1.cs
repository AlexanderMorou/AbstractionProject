 /* ----------------------------------------------------------------\
 |  This code was generated by Allen Copeland's Abstraction.        |
 |  Version: 0.5.0.0                                                |
 |------------------------------------------------------------------|
 |  To ensure the code works properly,                              |
 |  please do not make any changes to the file.                     |
 |------------------------------------------------------------------|
 |  The specific language is C♯ (Runtime Version: 4.0.30319.42000)  |
 |  Sub-tool Name: C♯ Code Translator                               |
 |  Sub-tool Version: 1.0.0.0                                       |
 \---------------------------------------------------------------- */
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
  /// <summary>
  /// Represents a basic visitor for intermediate members which yields a result of <typeparamref name="TResult"/>.
  /// </summary>
  /// <typeparam name="TResult">
  /// Denotes the type of result the members of the <see cref="AllenCopeland.Abstraction.Slf.Ast.Members.IIntermediateMemberVisitor{TResult}"/>
  /// should yield.
  /// </typeparam>
  public interface IIntermediateMemberVisitor<TResult>
  {
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqRangeVariable"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(ILinqRangeVariable intermediateMember);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq.ILinqTypedRangeVariable"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(ILinqTypedRangeVariable intermediateMember);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Members.IIntermediateBinaryOperatorCoercionMember{TCoercionParent,TIntermediateCoercionParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit<TCoercionParent, TIntermediateCoercionParent>(IIntermediateBinaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent> intermediateMember)
      where TCoercionParent:
        ICoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
      where TIntermediateCoercionParent:
        IIntermediateCoercibleType<IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TCoercionParent>, IIntermediateBinaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>,
        TCoercionParent;
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Members.IIntermediateConstructorMember{TCtor,TIntermediateCtor,TType,TIntermediateType}"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType> intermediateMember)
      where TCtor:
        IConstructorMember<TCtor, TType>
      where TIntermediateCtor:
        TCtor,
        IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
      where TType:
        ICreatableParent<TCtor, TType>
      where TIntermediateType:
        TType,
        IIntermediateCreatableParent<TCtor, TIntermediateCtor, TType, TIntermediateType>;
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Members.IIntermediateConstructorSignatureMember{TCtor,TIntermediateCtor,TType,TIntermediateType}"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType> intermediateMember)
      where TCtor:
        IConstructorMember<TCtor, TType>
      where TIntermediateCtor:
        TCtor,
        IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
      where TType:
        ICreatableParent<TCtor, TType>
      where TIntermediateType:
        TType,
        IIntermediateCreatableSignatureParent<TCtor, TIntermediateCtor, TType, TIntermediateType>;
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Members.IIntermediateEnumFieldMember"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(IIntermediateEnumFieldMember intermediateMember);
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Members.IIntermediateEventMember{TEvent,TIntermediateEvent,TEventParent,TIntermediateEventParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> intermediateMember)
      where TEvent:
        IEventMember<TEvent, TEventParent>
      where TIntermediateEvent:
        TEvent,
        IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
      where TEventParent:
        IEventParent<TEvent, TEventParent>
      where TIntermediateEventParent:
        TEventParent,
        IIntermediateEventParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>;
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Members.IIntermediateEventSignatureMember{TEvent,TIntermediateEvent,TEventParent,TIntermediateEventParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> intermediateMember)
      where TEvent:
        IEventSignatureMember<TEvent, TEventParent>
      where TIntermediateEvent:
        TEvent,
        IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>
      where TEventParent:
        IEventSignatureParent<TEvent, TEventParent>
      where TIntermediateEventParent:
        TEventParent,
        IIntermediateEventSignatureParent<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>;
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Members.IIntermediateFieldMember{TField,TIntermediateField,TFieldParent,TIntermediateFieldParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>(IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> intermediateMember)
      where TField:
        IFieldMember<TField, TFieldParent>
      where TIntermediateField:
        TField,
        IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
      where TFieldParent:
        IFieldParent<TField, TFieldParent>
      where TIntermediateFieldParent:
        TFieldParent,
        IIntermediateFieldParent<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>;
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Members.IIntermediateIndexerMember{TIndexer,TIntermediateIndexer,TIndexerParent,TIntermediateIndexerParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> intermediateMember)
      where TIndexer:
        IIndexerMember<TIndexer, TIndexerParent>
      where TIntermediateIndexer:
        TIndexer,
        IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
      where TIndexerParent:
        IIndexerParent<TIndexer, TIndexerParent>
      where TIntermediateIndexerParent:
        TIndexerParent,
        IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>;
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Members.IIntermediateIndexerSignatureMember{TIndexer,TIntermediateIndexer,TIndexerParent,TIntermediateIndexerParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> intermediateMember)
      where TIndexer:
        IIndexerSignatureMember<TIndexer, TIndexerParent>
      where TIntermediateIndexer:
        TIndexer,
        IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>
      where TIndexerParent:
        IIndexerSignatureParent<TIndexer, TIndexerParent>
      where TIntermediateIndexerParent:
        TIndexerParent,
        IIntermediateIndexerSignatureParent<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>;
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Members.IIntermediateMethodMember{TMethod,TIntermediateMethod,TMethodParent,TIntermediateMethodParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>(IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> intermediateMember)
      where TMethod:
        IMethodMember<TMethod, TMethodParent>
      where TIntermediateMethod:
        IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
        TMethod
      where TMethodParent:
        IMethodParent<TMethod, TMethodParent>
      where TIntermediateMethodParent:
        IIntermediateMethodParent<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
        TMethodParent;
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Members.IIntermediateMethodSignatureMember{TSignature,TIntermediateSignature,TParent,TIntermediateParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit<TSignature, TIntermediateSignature, TParent, TIntermediateParent>(IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> intermediateMember)
      where TSignature:
        IMethodSignatureMember<TSignature, TParent>
      where TIntermediateSignature:
        TSignature,
        IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent>
      where TParent:
        IMethodSignatureParent<TSignature, TParent>
      where TIntermediateParent:
        TParent,
        IIntermediateMethodSignatureParent<TSignature, TIntermediateSignature, TParent, TIntermediateParent>;
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Members.IIntermediateParameterMember{TParent,TIntermediateParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit<TParent, TIntermediateParent>(IIntermediateParameterMember<TParent, TIntermediateParent> intermediateMember)
      where TParent:
        IParameterParent
      where TIntermediateParent:
        TParent,
        IIntermediateParameterParent;
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Members.IIntermediatePropertyMember{TProperty,TIntermediateProperty,TPropertyParent,TIntermediatePropertyParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> intermediateMember)
      where TProperty:
        IPropertyMember<TProperty, TPropertyParent>
      where TIntermediateProperty:
        TProperty,
        IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
      where TPropertyParent:
        IPropertyParent<TProperty, TPropertyParent>
      where TIntermediatePropertyParent:
        TPropertyParent,
        IIntermediatePropertyParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>;
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Members.IIntermediatePropertySignatureMember{TProperty,TIntermediateProperty,TPropertyParent,TIntermediatePropertyParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> intermediateMember)
      where TProperty:
        IPropertySignatureMember<TProperty, TPropertyParent>
      where TIntermediateProperty:
        TProperty,
        IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>
      where TPropertyParent:
        IPropertySignatureParent<TProperty, TPropertyParent>
      where TIntermediatePropertyParent:
        TPropertyParent,
        IIntermediatePropertySignatureParent<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>;
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Members.IIntermediateTypeCoercionMember{TCoercionParent,TIntermediateCoercionParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit<TCoercionParent, TIntermediateCoercionParent>(IIntermediateTypeCoercionMember<TCoercionParent, TIntermediateCoercionParent> intermediateMember)
      where TCoercionParent:
        ICoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, TCoercionParent>
      where TIntermediateCoercionParent:
        IIntermediateCoercibleType<ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TCoercionParent>, IIntermediateTypeCoercionMember<TCoercionParent, TIntermediateCoercionParent>, TCoercionParent, TIntermediateCoercionParent>,
        TCoercionParent;
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Members.IIntermediateUnaryOperatorCoercionMember{TCoercionParent,TInterCoercionParent}"/>
    /// relevant to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit<TCoercionParent, TInterCoercionParent>(IIntermediateUnaryOperatorCoercionMember<TCoercionParent, TInterCoercionParent> intermediateMember)
      where TCoercionParent:
        ICoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, TCoercionParent>
      where TInterCoercionParent:
        IIntermediateCoercibleType<IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TCoercionParent>, IIntermediateUnaryOperatorCoercionMember<TCoercionParent, TInterCoercionParent>, TCoercionParent, TInterCoercionParent>,
        TCoercionParent;
    /// <summary>
    /// Returns a <typeparamref name="TResult"/> after it visits the <paramref name="intermediateMember"/>
    /// provided.
    /// </summary>
    /// <param name="intermediateMember">
    /// The <see cref="AllenCopeland.Abstraction.Slf.Ast.Members.ILocalMember"/> relevant
    /// to the visit.
    /// </param>
    /// <returns>
    /// Returns the value of <typeparamref name="TResult"/> relative to the implementation
    /// of the visitor.
    /// </returns>
    TResult Visit(ILocalMember intermediateMember);
  };
};