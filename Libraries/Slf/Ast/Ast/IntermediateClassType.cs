using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
//using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System.ComponentModel;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides a base implementation of an intermediate class type.
    /// </summary>
    public sealed class IntermediateClassType :
        IntermediateClassType<IntermediateClassType>
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateClassType"/> with the 
        /// <paramref name="name"/> and <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="name">The <see cref="System.String"/> which represents the 
        /// <see cref="IntermediateClassType"/>'s name.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/> which contains the 
        /// <see cref="IntermediateClassType"/>.</param>
        public IntermediateClassType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateClassType"/> partial instance with the 
        /// <paramref name="root"/> instance and the <paramref name="parent"/> which contains
        /// the new partial instance.
        /// </summary>
        /// <param name="root">The <see cref="IntermediateClassType"/> which roots the 
        /// <see cref="IntermediateClassType"/>.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/> which contains the new
        /// partial instance.</param>
        public IntermediateClassType(IntermediateClassType root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateClassType"/> with the 
        /// <see cref="IIntermediateTypeParent"/>.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/></param>
        public IntermediateClassType(IIntermediateTypeParent parent)
            : base(parent)
        {

        }

        /// <summary>
        /// Creates a new <see cref="IntermediateClassType"/> with the
        /// <paramref name="root"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="root">The <see cref="IntermediateClassType"/> which is the root instance
        /// of the series of <see cref="IntermediateClassType"/>.</param>
        /// <param name="parent">The <see cref="IntermediateClassType"/> which contains the 
        /// newly created <see cref="IntermediateClassType"/> instance.</param>
        /// <returns>A new <see cref="IntermediateClassType"/> as a partial of the <paramref name="root"/>
        /// instance and a child of the <paramref name="parent"/>.</returns>
        protected override IntermediateClassType GetNewPartial(IntermediateClassType root, IIntermediateTypeParent parent)
        {
            return new IntermediateClassType(root, parent);
        }
    }
    /// <summary>
    /// Provides a generic implementation of an intermediate class type.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Always)]
    public abstract partial class IntermediateClassType<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>,
        IIntermediateClassType
        where TInstanceIntermediateType :
            IntermediateClassType<TInstanceIntermediateType>
    {
        private IClassType baseType;
        private IIntermediateClassCtorMember defaultCtor;
        /// <summary>
        /// Creates a new <see cref="IntermediateClassType{TInstanceIntermediateType}"/> with the 
        /// <paramref name="root"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="root">The <see cref="IntermediateClassType{TInstanceIntermediateType}"/> from which the 
        /// current <see cref="IntermediateClassType{TInstanceIntermediateType}"/> is a part of.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/> which contains this
        /// part of the <see cref="IntermediateClassType{TInstanceIntermediateType}"/>.</param>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="parent"/> is not
        /// the root's parent, or another part of the root's parent.</exception>
        internal protected IntermediateClassType(TInstanceIntermediateType root, IIntermediateTypeParent parent)
            : base(root, parent)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateClassType"/> with the <paramref name="name"/>
        /// and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the name of the 
        /// <see cref="IntermediateClassType{TInstanceIntermediateType}"/>.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/> to which the 
        /// <see cref="IntermediateClassType{TInstanceIntermediateType}"/> belongs.</param>
        internal protected IntermediateClassType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateClassType{TInstanceIntermediateType}"/> with the
        /// <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/> to which the 
        /// <see cref="IntermediateClassType{TInstanceIntermediateType}"/> belongs.</param>
        internal protected IntermediateClassType(IIntermediateTypeParent parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Obtains a new <see cref="IntermediateClassCtorMember{TInstanceIntermediateType}"/> with no parameters.
        /// </summary>
        /// <returns>A new <see cref="IntermediateClassCtorMember{TInstanceIntermediateType}"/>, if successful.</returns>
        /// <remarks>Required by design, due to further inheritance on constructors being necessary.</remarks>
        protected override IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>.
                           ConstructorMember GetNewConstructor()
        {
            return new IntermediateClassCtorMember<TInstanceIntermediateType>((TInstanceIntermediateType)(object)this);
        }

        protected override IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>.ConstructorMember GetTypeInitializer()
        {
            return new IntermediateClassCtorMember<TInstanceIntermediateType>((TInstanceIntermediateType)(object)this, true);
        }

        /// <summary>
        /// Obtains a new <see cref="IntermediateClassEventMember{TInstanceIntermediateType}"/> which designates the 
        /// name and delegate type of the new event to add.
        /// </summary>
        /// <param name="nameAndDelegateType">The <see cref="String"/> name of the
        /// event and the <see cref="IType"/> related to its delegate type.</param>
        /// <returns>A new <see cref="IntermediateClassEventMember{TInstanceIntermediateType}"/>.</returns>
        protected override IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>.
                           EventMember GetNewEvent(TypedName nameAndDelegateType)
        {
            var type = nameAndDelegateType.GetTypeRef();
            if (!(type is IDelegateType))
                throw new ArgumentException("nameAndDelegateType");
            var result = new IntermediateClassEventMember<TInstanceIntermediateType>(((TInstanceIntermediateType)(object)this))
            {
                SignatureSource = EventSignatureSource.Delegate,
                SignatureType = (IDelegateType)type,
                Name = nameAndDelegateType.Name
            };
            return result;
        }

        /// <summary>
        /// Obtains a new <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}.EventMember"/> which designates
        /// the <paramref name="name"/> and <paramref name="eventSignature"/> of
        /// the event to add.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the event.</param>
        /// <param name="eventSignature">The series of names and types which relate to the
        /// auto-generated delegate that's associated to the event.</param>
        /// <returns>A new <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}.EventMember"/>.</returns>
        protected override IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>.
                           EventMember GetNewEvent(string name, TypedNameSeries eventSignature)
        {
            var result = new IntermediateClassEventMember<TInstanceIntermediateType>(((TInstanceIntermediateType)(object)this))
            { 
                SignatureSource = EventSignatureSource.Declared,
                Name = name
            };
            foreach (var param in eventSignature)
                result.Parameters.Add(param.Name, param.GetTypeRef(), param.Direction);
            return result;
        }

        /// <summary>
        /// Obtains a new <see cref="IntermediateClassEventMember{TInstanceIntermediateType}"/> which designates the name
        /// of the method member to add.
        /// </summary>
        /// <param name="name">The <see cref="String"/> name of the method to obtain.</param>
        /// <returns>A new <see cref="IntermediateClassEventMember{TInstanceIntermediateType}"/> instance that derives from 
        /// <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}.MethodMember"/>.</returns>
        protected override IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>.
                           MethodMember GetNewMethod(string name)
        {
            var result = new IntermediateClassMethodMember<TInstanceIntermediateType>((TInstanceIntermediateType)this);
            result.AssignName(name);
            return result;
        }

        protected override IndexerMember GetNewIndexer(TypedName nameAndReturn)
        {
            return new IntermediateClassIndexerMember<TInstanceIntermediateType>(nameAndReturn.Name, (TInstanceIntermediateType)(object)this)
            {
                PropertyType = nameAndReturn.Source == TypedNameSource.TypeReference ?
                               nameAndReturn.TypeReference :
                               nameAndReturn.Source == TypedNameSource.SymbolReference ?
                               nameAndReturn.SymbolReference.GetSymbolType() :
                               null
            };
        }

        protected override PropertyMember GetNewProperty(TypedName nameAndType)
        {
            return new IntermediateClassPropertyMember<TInstanceIntermediateType>(nameAndType.Name, (TInstanceIntermediateType)this)
            {
                PropertyType = nameAndType.Source == TypedNameSource.TypeReference ?
                   nameAndType.TypeReference :
                   nameAndType.Source == TypedNameSource.SymbolReference ?
                   nameAndType.SymbolReference.GetSymbolType() :
                   null
            };
        }

        /// <summary>
        /// Creates a new <see cref="IClassType"/> instance as a generic instance of the current
        /// <see cref="IntermediateClassType"/>.
        /// </summary>
        /// <param name="typeParameters">The <see cref="IControlledTypeCollection"/> which contains the type-parameters
        /// to construct the generic type.</param>
        /// <returns>A new <see cref="IClassType"/> instance as a generic instance of the <see cref="IntermediateClassType"/>.
        /// </returns>
        protected override IClassType OnMakeGenericClosure(IControlledTypeCollection typeParameters)
        {
            return new _ClassTypeBase(this, typeParameters);
        }

        /// <summary>
        /// Determines whether the <paramref name="other"/> <see cref="IClassType"/>
        /// equals the current <see cref="IntermediateClassType"/>.
        /// </summary>
        /// <param name="other">The <see cref="IClassType"/> to compare to.</param>
        /// <returns>true if the <paramref name="other"/> <see cref="IClassType"/>
        /// equals the <see cref="IntermediateClassType"/>; false, otherwise.</returns>
        protected override bool Equals(IClassType other)
        {
            return other == this;
        }

        /// <summary>
        /// Returns the <see cref="TypeKind"/> that the <see cref="IntermediateClassType"/> is.
        /// </summary>
        protected override TypeKind TypeImpl
        {
            get {return TypeKind.Class; }
        }

        protected override bool IsSubclassOfImpl(IType other)
        {
            return false;
        }

        protected override IType BaseTypeImpl
        {
            get { return this.BaseType; }
        }

        #region IIntermediateClassType Members
        private SpecialClassModifier specialModifier;
        public virtual SpecialClassModifier SpecialModifier
        {
            get
            {
                if (this.IsRoot)
                    return specialModifier;
                else
                    return this.GetRoot().specialModifier;
            }
            set
            {
                if (this.IsRoot)
                    this.specialModifier = value;
                else
                    this.GetRoot().specialModifier = value;
            }
        }

        public new virtual IClassType BaseType
        {
            get
            {
                if (this.IsRoot)
                    return this.baseType;
                else
                    return this.GetRoot().baseType;
            }
            set
            {
                if (this.IsRoot)
                {
                    if (value == null)
                        throw new ArgumentNullException("value");
                    if (value.Type == TypeKind.Dynamic)
                        throw new ArgumentOutOfRangeException("value", "Cannot have a dynamic type base.");
                    this.baseType = value;
                }
                else
                    this.GetRoot().baseType = value;
            }
        }

        #endregion

        #region IClassType Members

        public new IClassInterfaceMapping GetInterfaceMap(IInterfaceType type)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected override FieldMember GetNewField(TypedName nameAndType)
        {
            //var fieldCreatorService
            var member = new IntermediateClassFieldMember<TInstanceIntermediateType>(nameAndType.Name, (TInstanceIntermediateType)this);
            var kind = nameAndType.GetTypeRef();
            if (kind.ContainsSymbols())
                member.FieldType = kind.SimpleSymbolDisambiguation(member);
            else
                member.FieldType = kind;
            return member;
        }

        public override void Accept(IIntermediateTypeVisitor visitor)
        {
            visitor.Visit(this);
        }
        /// <summary>
        /// Visits the <paramref name="visitor"/> provided.
        /// </summary>
        /// <typeparam name="TResult">The type of value to return for the visitor.</typeparam>
        /// <typeparam name="TContext">The type of context passed to the visitor.</typeparam>
        /// <param name="visitor">The <see cref="IIntermediateTypeVisitor"/> to
        /// receive the <see cref="IntermediateClassType{TInstanceIntermediateType}"/> as a visitor.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        public override TResult Accept<TResult, TContext>(IIntermediateTypeVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        IIntermediateInstantiableTypeImplementedInterfaces<IClassEventMember, IIntermediateClassEventMember, IClassIndexerMember, IIntermediateClassIndexerMember, 
                                                           IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember,
                                                           IClassType, IIntermediateClassType> IIntermediateClassType.ImplementedInterfaces
        {
            get
            {
                return this.ImplementedInterfaces;
            }
        }

        public override void Dispose()
        {
            try
            {
                this.baseType = null;
            }
            finally
            {
                base.Dispose();
            }
        }

        public IIntermediateClassCtorMember DefaultConstructor
        {
            get
            {
                if (this.defaultCtor == null)
                {
                    if (base.AreConstructorsInitialized)
                    {
                        foreach (var item in this.Constructors.Values)
                            if (item.Parameters.Count == 0)
                            {
                                this.defaultCtor = item;
                                goto returnPoint;
                            }
                    }
                    this.defaultCtor = this.Constructors.Add(TypedNameSeries.Empty);
                }
            returnPoint:
                return this.defaultCtor;
            }
        }

        protected override ImplementedInterfacesDictionary<IClassEventMember, IIntermediateClassEventMember, IClassIndexerMember, IIntermediateClassIndexerMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IClassType, IIntermediateClassType> InitializeImplementedInterfaces()
        {
            return new ImplementedInterfacesDictionary((TInstanceIntermediateType)this);
        }

        protected override string OnGetIdentityName()
        {
            return ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.@class);
        }
    }
}
