using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Properties;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Threading.Tasks;
using System.Linq;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    partial class IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            class, 
            IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>,
            TCtor
        where TEvent :
            IEventMember<TEvent, TType>
        where TIntermediateEvent :
            class,
            IIntermediateEventMember<TEvent, TIntermediateEvent, TType, TIntermediateType>,
            TEvent
        where TIntermediateEventMethod :
            class,
            TIntermediateMethod,
            IIntermediateEventMethodMember
        where TField :
            IFieldMember<TField, TType>,
            IInstanceMember
        where TIntermediateField :
            IIntermediateFieldMember<TField, TIntermediateField, TType, TIntermediateType>,
            TField,
            IIntermediateInstanceMember
        where TIndexer :
            IIndexerMember<TIndexer, TType>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TType, TIntermediateType>
        where TIntermediateIndexerMethod :
            class, 
            TIntermediateMethod,
            IIntermediatePropertyMethodMember
        where TMethod :
            class,
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TIntermediateMethod :
            class,
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TType, TIntermediateType>,
            TMethod
        where TProperty :
            IPropertyMember<TProperty, TType>
        where TIntermediateProperty :
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TType, TIntermediateType>,
            TProperty
        where TIntermediatePropertyMethod :
            class,
            TIntermediateMethod,
            IIntermediatePropertyMethodMember
        where TType :
            class,
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, IGeneralGenericTypeUniqueIdentifier, TType>,
            IGenericType<IGeneralGenericTypeUniqueIdentifier, TType>
        where TIntermediateType :
            class,
            IIntermediateInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, IGeneralGenericTypeUniqueIdentifier, TType, TIntermediateType>,
            IIntermediateGenericType<IGeneralGenericTypeUniqueIdentifier, TType, TIntermediateType>,
            IIntermediateSegmentableType<IGeneralGenericTypeUniqueIdentifier, TType, TIntermediateType>,
            TType
        where TInstanceIntermediateType :
            IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType>,
            TIntermediateType
    {
        /// <summary>
        /// Provides a dictionary for the binary operator members of an instantiable type.
        /// </summary>
        protected sealed class BinaryOperatorDictionary :
            IntermediateGroupedMemberDictionary<TType, TIntermediateType, IBinaryOperatorUniqueIdentifier, IBinaryOperatorCoercionMember<TType>, IIntermediateBinaryOperatorCoercionMember<TType, TIntermediateType>>,
            IIntermediateBinaryOperatorCoercionMemberDictionary<TType, TIntermediateType>,
            IIntermediateBinaryOperatorCoercionMemberDictionary
        {
            private new IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> Parent
            {
                get
                {
                    return (IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType>)(object)base.Parent;
                }
            }

            internal BinaryOperatorDictionary(IntermediateFullMemberDictionary master, IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> parent)
                : base(master,((TIntermediateType)((object)( parent))))
            {
            }

            internal BinaryOperatorDictionary(IntermediateFullMemberDictionary master, IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> parent, BinaryOperatorDictionary root)
                : base(master, ((TIntermediateType)((object)(parent))), root)
            {
            }

            #region IBinaryOperatorCoercionMemberDictionary<TType> Members

            IBinaryOperatorCoercionMember<TType> IBinaryOperatorCoercionMemberDictionary<TType>.this[CoercibleBinaryOperators op, BinaryOpCoercionContainingSide side, IType otherSide]
            {
                get
                {
                    return this[op, side, otherSide];
                }
            }

            IBinaryOperatorCoercionMember<TType> IBinaryOperatorCoercionMemberDictionary<TType>.this[CoercibleBinaryOperators op]
            {
                get
                {
                    return this[op];
                }
            }

            #endregion

            #region IBinaryOperatorCoercionMemberDictionary Members

            IBinaryOperatorCoercionMember IBinaryOperatorCoercionMemberDictionary.this[CoercibleBinaryOperators op, BinaryOpCoercionContainingSide side, IType otherSide]
            {
                get { return this[op, side, otherSide]; }
            }

            IBinaryOperatorCoercionMember IBinaryOperatorCoercionMemberDictionary.this[CoercibleBinaryOperators op]
            {
                get { return this[op]; }
            }

            #endregion

            #region IIntermediateBinaryOperatorCoercionMemberDictionary<TType,TIntermediateType> Members

            public IIntermediateBinaryOperatorCoercionMember<TType, TIntermediateType> Add(CoercibleBinaryOperators op, BinaryOpCoercionContainingSide containingSide, IType otherSide, IType returnType)
            {
                BinaryOperatorMember member = new BinaryOperatorMember(this.Parent);
                member.Operator = op;
                member.ContainingSide = containingSide;
                if (containingSide != BinaryOpCoercionContainingSide.Both)
                    if (otherSide == null)
                        throw new ArgumentNullException("otherSide");
                    else
                        member.OtherSide = otherSide;
                member.ReturnType = returnType;
                this.AddDeclaration(member);
                return member;
            }

            public IIntermediateBinaryOperatorCoercionMember<TType, TIntermediateType> Add(CoercibleBinaryOperators op, IType returnType)
            {
                BinaryOperatorMember member = new BinaryOperatorMember(this.Parent);
                member.Operator = op;
                member.ContainingSide = BinaryOpCoercionContainingSide.Both;
                member.ReturnType = returnType;
                this.AddDeclaration(member);
                return member;
            }

            #endregion

            #region IIntermediateBinaryOperatorCoercionMemberDictionary Members

            IIntermediateBinaryOperatorCoercionMember IIntermediateBinaryOperatorCoercionMemberDictionary.Add(CoercibleBinaryOperators op, BinaryOpCoercionContainingSide containingSide, IType otherSide, IType returnType)
            {
                return this.Add(op, containingSide, otherSide, returnType);
            }

            IIntermediateBinaryOperatorCoercionMember IIntermediateBinaryOperatorCoercionMemberDictionary.Add(CoercibleBinaryOperators op, IType returnType)
            {
                return this.Add(op, returnType);
            }

            #endregion
            /// <summary>
            /// Returns the <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/> which
            /// coerces the <paramref name="op"/> provided with the
            /// parent contained on the <paramref name="side"/> provided with
            /// <paramref name="otherSide"/> provided.
            /// </summary>
            /// <param name="op">The <see cref="CoercibleBinaryOperators"/>
            /// coerced by the <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/> 
            /// to find.</param>
            /// <param name="side">The side at which the containing type
            /// is in the coercion.</param>
            /// <param name="otherSide">The type of the other side in the coercion.</param>
            /// <returns>A <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/> instance
            /// that coerces the <paramref name="op"/> provided.</returns>
            public IIntermediateBinaryOperatorCoercionMember<TType, TIntermediateType> this[CoercibleBinaryOperators op, BinaryOpCoercionContainingSide side, IType otherSide]
            {
                get
                {
                    foreach (var binOpC in this.Values)
                        if (binOpC.Operator == op && binOpC.ContainingSide == side && binOpC.OtherSide.Equals(otherSide))
                            return binOpC;
                    return null;
                }
            }
            /// <summary>
            /// Returns the <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/> which
            /// coerces the <paramref name="op"/> provided where
            /// the containing type is used as both operands.
            /// </summary>
            /// <param name="op">The <see cref="CoercibleBinaryOperators"/>
            /// coerced by the <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/> 
            /// to find.</param>
            /// <returns>A <see cref="IIntermediateBinaryOperatorCoercionMember{TType, TIntermediateType}"/> instance
            /// that coerces the <paramref name="op"/> provided.</returns>
            public IIntermediateBinaryOperatorCoercionMember<TType, TIntermediateType> this[CoercibleBinaryOperators op]
            {
                get
                {
                    foreach (var binOpC in this.Values)
                        if (binOpC.ContainingSide == BinaryOpCoercionContainingSide.Both && op == binOpC.Operator)
                        {
                            return binOpC;
                        }
                    return null;
                }
            }

        }

        protected sealed class ConstructorDictionary :
            IntermediateConstructorMemberDictionary<TCtor, TIntermediateCtor, TType, TIntermediateType>
        {
            private new IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> Parent
            {
                get
                {
                    return (IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType>)(object)base.Parent;
                }
            }

            internal ConstructorDictionary(IntermediateFullMemberDictionary master, IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> parent)
                : base(master, ((TIntermediateType)((object)(parent))))
            {
            }

            internal ConstructorDictionary(IntermediateFullMemberDictionary master, IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> parent, ConstructorDictionary root)
                : base(master, ((TIntermediateType)((object)(parent))), root)
            {
            }

            protected override TIntermediateCtor GetConstructor()
            {
                return ((TIntermediateCtor)((object)(this.Parent.GetNewConstructor())));
            }
        }

        /// <summary>
        /// Provides a dictionary for the intermediate generic segmentable instantiable
        /// type's events.
        /// </summary>
        protected sealed class EventDictionary :
            IntermediateEventMemberDictionary<TEvent, TIntermediateEvent, TType, TIntermediateType>,
            IIntermediateEventMemberDictionary<TEvent, TIntermediateEvent, TType, TIntermediateType>
        {
            private new IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> Parent
            {
                get
                {
                    return (IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType>)(object)base.Parent;
                }
            }
            public EventDictionary(IntermediateFullMemberDictionary master, IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> parent)
                : base(master, ((TIntermediateType)((object)(parent))))
            {
            }
            public EventDictionary(IntermediateFullMemberDictionary master, IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> parent, EventDictionary root)
                : base(master, ((TIntermediateType)((object)(parent))), root)
            {
            }

            protected override TIntermediateEvent GetEvent(TypedName nameAndDelegateType)
            {
                EventMember result = this.Parent.GetNewEvent(nameAndDelegateType);
                return ((TIntermediateEvent)((object)(result)));
            }

            protected override TIntermediateEvent GetEvent(string name, TypedNameSeries eventSignature)
            {
                EventMember result = this.Parent.GetNewEvent(name, eventSignature);
                return ((TIntermediateEvent)((object)(result)));
            }
        }

        protected sealed class IndexerDictionary :
            IntermediateIndexerMemberDictionary<TIndexer, TIntermediateIndexer, TType, TIntermediateType>
        {
            internal IndexerDictionary(IntermediateFullMemberDictionary master, TInstanceIntermediateType parent)
                : base(master, parent)
            {
            }
            internal IndexerDictionary(IntermediateFullMemberDictionary master, TInstanceIntermediateType parent, IndexerDictionary root)
                : base(master, parent, root)
            {
            }
            private new TInstanceIntermediateType Parent
            {
                get
                {
                    return (TInstanceIntermediateType)base.Parent;
                }
            }
            protected override TIntermediateIndexer GetNew(TypedName nameAndReturn, TypedNameSeries parameters, bool canGet = true, bool canSet = true)
            {
                var result = (TIntermediateIndexer)(object)this.Parent.GetNewIndexer(nameAndReturn);

                if (parameters.Count > 0)
                    result.Parameters.AddRange(parameters.ToArray());
                result.CanRead = canGet;
                result.CanWrite = canSet;
                return result;
            }
        }


        protected sealed class MethodDictionary :
            IntermediateMethodMemberDictionary<TMethod, TIntermediateMethod, TType, TIntermediateType>
        {
            public MethodDictionary(IntermediateFullMemberDictionary master, TInstanceIntermediateType parent, ITypeIdentityManager identityManager)
                : base(master, parent, identityManager)
            {
            }
            public MethodDictionary(IntermediateFullMemberDictionary master, TInstanceIntermediateType parent, MethodDictionary root)
                : base(master, parent, root)
            {
            }

            private new TInstanceIntermediateType Parent
            {
                get
                {
                    return (TInstanceIntermediateType)base.Parent;
                }
            }

            protected override TIntermediateMethod OnGetNewMethod(string name)
            {
                return (TIntermediateMethod)(object)this.Parent.GetNewMethod(name);
            }
        }

        protected sealed class TypeCoercionDictionary :
            IntermediateGroupedMemberDictionary<TType, TIntermediateType, ITypeCoercionUniqueIdentifier, ITypeCoercionMember<TType>, IIntermediateTypeCoercionMember<TType, TIntermediateType>>,
            IIntermediateTypeCoercionMemberDictionary<TType, TIntermediateType>,
            IIntermediateTypeCoercionMemberDictionary
        {
            private new TInstanceIntermediateType Parent
            {
                get
                {
                    return (TInstanceIntermediateType)base.Parent;
                }
            }
            internal TypeCoercionDictionary(IntermediateFullMemberDictionary master, IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> parent)
                : base(master, ((TIntermediateType)((object)(parent))))
            {
            }
            internal TypeCoercionDictionary(IntermediateFullMemberDictionary master, IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> parent, TypeCoercionDictionary root)
                : base(master, ((TIntermediateType)((object)(parent))), root)
            {
            }

            #region IIntermediateTypeCoercionMemberDictionary<TType,TIntermediateType> Members

            /// <summary>
            /// Returns the <see cref="IIntermediateTypeCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/>
            /// which meets the <paramref name="requirement"/> in
            /// the <paramref name="direction"/> for
            /// the <paramref name="target"/> specified.
            /// </summary>
            /// <param name="requirement">The <see cref="TypeConversionRequirement"/>
            /// necessary for the coercion, either 
            /// explicit or implicit.</param>
            /// <param name="direction">The coercion direction;
            /// either from or to the type.</param>
            /// <param name="target">
            /// The <see cref="IType"/> which is coerced.</param>
            /// <returns>The <see cref="IIntermediateTypeCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/> 
            /// which met the <paramref name="requirement"/> in
            /// the <paramref name="direction"/> for
            /// the <paramref name="target"/> specified</returns>
            /// <exception cref="System.ArgumentException">
            /// thrown when there is no explicit/implicit coercion
            /// from/to <paramref name="target"/>.
            /// </exception>
            /// <exception cref="System.ArgumentOutOfRangeException">
            /// thrown when either <paramref name="requirement"/> or 
            /// <paramref name="direction"/> is out of range.</exception>
            public IIntermediateTypeCoercionMember<TType, TIntermediateType> this[TypeConversionRequirement requirement, TypeConversionDirection direction, IType target]
            {
                get {
                    if (target == null)
                        throw new ArgumentNullException("target");
                    else if (target is IInterfaceType)
                        throw new ArgumentException(Resources.Exception_Argument_CoercionType_CannotBeInterface);
                    switch (requirement)
                    {
                        case TypeConversionRequirement.Explicit:
                        case TypeConversionRequirement.Implicit:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("requirement");
                    }
                    switch (direction)
                    {
                        case TypeConversionDirection.ToContainingType:
                        case TypeConversionDirection.FromContainingType:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("direction");
                    }
                    foreach (var item in this.Values)
                        if (item.Requirement == requirement && item.Direction == direction && item.CoercionType.Equals(target))
                            return item;
                    throw new ArgumentException("target");
                }
            }

            /// <summary>
            /// Adds a new <see cref="IIntermediateTypeCoercionMember{TCoercionParent, TIntermediateCoercionParent}"/>
            /// with the conversion <paramref name="requirement"/>, <paramref name="direction"/> and target.
            /// </summary>
            /// <param name="requirement">The <see cref="TypeConversionRequirement"/>
            /// which determines how the cast is applied.</param>
            /// <param name="direction">The coercion direction;
            /// either from or to the type.</param>
            /// <param name="target">
            /// The <see cref="IType"/> which is coerced.</param>
            /// <returns>A new <see cref="IIntermediateTypeCoercionMember{TType, TIntermediateType}"/>
            /// instance.</returns>
            /// <exception cref="System.ArgumentException">thrown when <paramref name="target"/>
            /// is an interface.</exception>
            /// <exception cref="System.ArgumentOutOfRangeException">
            /// thrown when either <paramref name="requirement"/> or 
            /// <paramref name="direction"/> is out of range.</exception>
            /// <exception cref="System.ArgumentNullException">thrown when <paramref name="target"/> is null.</exception>
            public IIntermediateTypeCoercionMember<TType, TIntermediateType> Add(TypeConversionRequirement requirement, TypeConversionDirection direction, IType target)
            {
                if (target == null)
                    throw new ArgumentNullException("target");
                else if (target.Type == TypeKind.Interface)
                    throw new ArgumentException(Resources.Exception_Argument_CoercionType_CannotBeInterface);
                TypeCoercionMember member = new TypeCoercionMember(this.Parent);
                switch (requirement)
                {
                    case TypeConversionRequirement.Explicit:
                    case TypeConversionRequirement.Implicit:
                        member.Requirement = requirement;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("requirement");
                }
                switch (direction)
                {
                    case TypeConversionDirection.ToContainingType:
                    case TypeConversionDirection.FromContainingType:
                        member.Direction = direction;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("direction");
                }
                member.CoercionType = target;
                this.AddDeclaration(member);
                return member;
            }

            #endregion

            #region ITypeCoercionMemberDictionary<TType> Members

            /// <summary>
            /// Returns whether the <see cref="ITypeCoercionMemberDictionary"/> 
            /// contains an explicit type coercion to <paramref name="target"/>.
            /// </summary>
            /// <param name="target">The <see cref="IType"/> to 
            /// coerce explicitly to.</param>
            /// <returns>true if the <see cref="ITypeCoercionMemberDictionary"/> 
            /// contains an explicit <see cref="ITypeCoercionMember"/>
            /// to the <paramref name="target"/> type; false otherwise.</returns>
            public bool HasExplicitCoercionTo(IType target)
            {
                if (target ==  null)
                    throw new ArgumentNullException("target");
                else if (target is IInterfaceType)
                    throw new ArgumentException(Resources.Exception_Argument_CoercionType_CannotBeInterface);
                foreach (var item in this.Values)
                    if (item.Requirement == TypeConversionRequirement.Explicit && item.Direction == TypeConversionDirection.ToContainingType && item.CoercionType.Equals(target))
                        return true;
                return false;
            }

            /// <summary>
            /// Returns whether the <see cref="ITypeCoercionMemberDictionary"/> 
            /// contains an implicit type coercion to <paramref name="target"/>.
            /// </summary>
            /// <param name="target">The <see cref="IType"/> to 
            /// coerce implicitly to.</param>
            /// <returns>true if the <see cref="ITypeCoercionMemberDictionary"/> 
            /// contains an implicit <see cref="ITypeCoercionMember"/>
            /// to the <paramref name="target"/> type; false otherwise.</returns>
            public bool HasImplicitCoercionTo(IType target)
            {
                if (target == null)
                    throw new ArgumentNullException("target");
                else if (target is IInterfaceType)
                    throw new ArgumentException(Resources.Exception_Argument_CoercionType_CannotBeInterface);
                foreach (var item in this.Values)
                    if (item.Requirement == TypeConversionRequirement.Implicit && item.Direction == TypeConversionDirection.ToContainingType && item.CoercionType.Equals(target))
                        return true;
                return false;
            }

            /// <summary>
            /// Returns whether the <see cref="ITypeCoercionMemberDictionary"/> 
            /// contains an explicit type coercion from <paramref name="target"/>.
            /// </summary>
            /// <param name="target">The <see cref="IType"/> to 
            /// coerce explicitly from.</param>
            /// <returns>true if the <see cref="ITypeCoercionMemberDictionary"/> 
            /// contains an explicit <see cref="ITypeCoercionMember"/>
            /// from the <paramref name="target"/> type; false otherwise.</returns>
            public bool HasExplicitCoercionFrom(IType target)
            {
                if (target == null)
                    throw new ArgumentNullException("target");
                else if (target is IInterfaceType)
                    throw new ArgumentException(Resources.Exception_Argument_CoercionType_CannotBeInterface);
                foreach (var item in this.Values)
                    if (item.Requirement == TypeConversionRequirement.Explicit && item.Direction == TypeConversionDirection.FromContainingType && item.CoercionType.Equals(target))
                        return true;
                return false;
            }

            /// <summary>
            /// Returns whether the <see cref="ITypeCoercionMemberDictionary"/> 
            /// contains an implicit type coercion from <paramref name="target"/>.
            /// </summary>
            /// <param name="target">The <see cref="IType"/> to 
            /// coerce implicitly from.</param>
            /// <returns>true if the <see cref="ITypeCoercionMemberDictionary"/> 
            /// contains an implicit <see cref="ITypeCoercionMember"/>
            /// from the <paramref name="target"/> type; false otherwise.</returns>
            public bool HasImplicitCoercionFrom(IType target)
            {
                if (target == null)
                    throw new ArgumentNullException("target");
                else if (target is IInterfaceType)
                    throw new ArgumentException(Resources.Exception_Argument_CoercionType_CannotBeInterface);
                foreach (var item in this.Values)
                    if (item.Requirement == TypeConversionRequirement.Implicit && item.Direction == TypeConversionDirection.FromContainingType && item.CoercionType.Equals(target))
                        return true;
                return false;
            }

            ITypeCoercionMember<TType> ITypeCoercionMemberDictionary<TType>.this[TypeConversionRequirement requirement, TypeConversionDirection direction, IType target]
            {
                get {
                    return this[requirement, direction, target];
                }
            }

            #endregion

            #region IIntermediateTypeCoercionMemberDictionary Members

            IIntermediateTypeCoercionMember IIntermediateTypeCoercionMemberDictionary.this[TypeConversionRequirement requirement, TypeConversionDirection direction, IType target]
            {
                get { return this[requirement, direction, target]; }
            }

            IIntermediateTypeCoercionMember IIntermediateTypeCoercionMemberDictionary.Add(TypeConversionRequirement requirement, TypeConversionDirection direction, IType target)
            {
                return this.Add(requirement, direction, target);
            }

            #endregion

            #region ITypeCoercionMemberDictionary Members


            ITypeCoercionMember ITypeCoercionMemberDictionary.this[TypeConversionRequirement requirement, TypeConversionDirection direction, IType target]
            {
                get { return this[requirement, direction, target]; }
            }

            #endregion
        }

        protected sealed class UnaryOperatorDictionary :
            IntermediateGroupedMemberDictionary<TType, TIntermediateType, IUnaryOperatorUniqueIdentifier, IUnaryOperatorCoercionMember<TType>, IIntermediateUnaryOperatorCoercionMember<TType, TIntermediateType>>,
            IIntermediateUnaryOperatorCoercionMemberDictionary<TType, TIntermediateType>,
            IIntermediateUnaryOperatorCoercionMemberDictionary
        {
            private new IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> Parent
            {
                get
                {
                    return (IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType>)(object)base.Parent;
                }
            }

            internal UnaryOperatorDictionary(IntermediateFullMemberDictionary master, IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> parent)
                : base(master, ((TIntermediateType)((object)(parent))))
            {
            }

            internal UnaryOperatorDictionary(IntermediateFullMemberDictionary master, IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> parent, UnaryOperatorDictionary root)
                : base(master, ((TIntermediateType)((object)(parent))), root)
            {
            }

            #region IIntermediateUnaryOperatorCoercionMemberDictionary<TType,TIntermediateType> Members

            public IIntermediateUnaryOperatorCoercionMember<TType, TIntermediateType> Add(CoercibleUnaryOperators op)
            {
                UnaryOperatorMember member = new UnaryOperatorMember(this.Parent);
                this.AddDeclaration(member);
                return member;
            }

            public IIntermediateUnaryOperatorCoercionMember<TType, TIntermediateType> this[CoercibleUnaryOperators op]
            {
                get
                {
                    foreach (var item in this.Values)
                        if (item.Operator == op)
                            return item;
                    return null;
                }
            }

            #endregion

            #region IUnaryOperatorCoercionMemberDictionary<TType> Members

            IUnaryOperatorCoercionMember<TType> IUnaryOperatorCoercionMemberDictionary<TType>.this[CoercibleUnaryOperators op]
            {
                get {
                    return this[op];
                }
            }

            #endregion


            #region IIntermediateUnaryOperatorCoercionMemberDictionary Members

            IIntermediateUnaryOperatorCoercionMember IIntermediateUnaryOperatorCoercionMemberDictionary.Add(CoercibleUnaryOperators op)
            {
                return this.Add(op);
            }

            IIntermediateUnaryOperatorCoercionMember IIntermediateUnaryOperatorCoercionMemberDictionary.this[CoercibleUnaryOperators op]
            {
                get { return this[op]; }
            }

            #endregion

            #region IUnaryOperatorCoercionMemberDictionary Members

            IUnaryOperatorCoercionMember IUnaryOperatorCoercionMemberDictionary.this[CoercibleUnaryOperators op]
            {
                get { return this[op]; }
            }


            public bool ContainsOverload(CoercibleUnaryOperators op)
            {
                foreach (var item in this.Values)
                    if (item.Operator == op)
                        return true;
                return false;
            }

            #endregion

            
        }

        protected class PropertyDictionary :
            IntermediatePropertyMemberDictionary<TProperty, TIntermediateProperty, TType, TIntermediateType>
        {
            protected internal PropertyDictionary(IntermediateFullMemberDictionary master, TInstanceIntermediateType parent)
                : base(master, parent)
            {
            }
            protected internal PropertyDictionary(IntermediateFullMemberDictionary master, TInstanceIntermediateType parent, PropertyDictionary root)
                : base(master, parent, root)
            {
            }
            private new IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> Parent
            {
                get
                {
                    return (IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType>)(object)base.Parent;
                }
            }

            protected override TIntermediateProperty GetProperty(TypedName nameAndType)
            {
                return (TIntermediateProperty)(object)this.Parent.GetNewProperty(nameAndType);
            }
        }

        protected class FieldDictionary :
            IntermediateFieldMemberDictionary<TField, TIntermediateField, TType, TIntermediateType>
        {
            internal protected FieldDictionary(IntermediateFullMemberDictionary master, TInstanceIntermediateType parent)
                : base(master, parent)
            {
            }
            internal protected FieldDictionary(IntermediateFullMemberDictionary master, TInstanceIntermediateType parent, FieldDictionary root)
                : base(master, parent, root)
            {
            }
            private new IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> Parent
            {
                get
                {
                    return (IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType>)(object)base.Parent;
                }
            }


            protected override TIntermediateField GetField(TypedName nameAndType)
            {
                return (TIntermediateField)(object)this.Parent.GetNewField(nameAndType);
            }
        }

    }
}
