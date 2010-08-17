using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using System.Runtime.CompilerServices;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
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
        internal IntermediateClassType(string name, IIntermediateTypeParent parent)
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
        /// <paramref name="IIntermediateTypeParent"/>.
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
    /// Provides an implementation of an intermediate class type.
    /// </summary>
    public abstract partial class IntermediateClassType<TInstanceIntermediateType> :
        IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>,
        IIntermediateClassType
        where TInstanceIntermediateType :
            IntermediateClassType<TInstanceIntermediateType>
    {
        private IClassType baseType;
        private IntermediateImplementedInterfaces _implementedInterfaces;
        private ITypeCollection implementedInterfaces;
        private bool isStatic;
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

        protected override IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>.
                           ConstructorMember GetNewConstructor(TypedNameSeries parameters)
        {
            throw new NotImplementedException();
        }

        protected override IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>.
                           EventMember GetNewEvent(TypedName nameAndDelegateType)
        {
            throw new NotImplementedException();
        }

        protected override IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>.
                           EventMember GetNewEvent(string name, TypedNameSeries eventSignature)
        {
            throw new NotImplementedException();
        }

        protected override IntermediateGenericSegmentableInstantiableType<IClassCtorMember, IIntermediateClassCtorMember, IClassEventMember, IIntermediateClassEventMember, IntermediateClassEventMember<TInstanceIntermediateType>.EventMethodMember, IClassFieldMember, IIntermediateClassFieldMember, IClassIndexerMember, IIntermediateClassIndexerMember, IntermediateClassIndexerMember<TInstanceIntermediateType>.IndexerMethodMember, IClassMethodMember, IIntermediateClassMethodMember, IClassPropertyMember, IIntermediateClassPropertyMember, IntermediateClassPropertyMember<TInstanceIntermediateType>.PropertyMethodMember, IClassType, IIntermediateClassType, TInstanceIntermediateType>.
                           MethodMember GetNewMethod(string name)
        {
            var result = new IntermediateClassMethodMember<TInstanceIntermediateType>((TInstanceIntermediateType)this);
            result.Name = name;
            return result;
        }

        /// <summary>
        /// Creates a new <see cref="IClassType"/> instance as a generic instance of the current
        /// <see cref="IntermediateClassType"/>.
        /// </summary>
        /// <param name="typeParameters">The <see cref="ITypeCollectionBase"/> which contains the type-parameters
        /// to construct the generic type.</param>
        /// <returns>A new <see cref="IClassType"/> instance as a generic instance of the <see cref="IntermediateClassType"/>.
        /// </returns>
        protected override IClassType OnMakeGenericType(ITypeCollectionBase typeParameters)
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

        protected override ILockedTypeCollection OnGetImplementedInterfaces()
        {
            if (this._implementedInterfaces == null)
                this._implementedInterfaces = new IntermediateImplementedInterfaces(this.ImplementedInterfaces);
            return this._implementedInterfaces;
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

        public SpecialClassModifier SpecialModifier
        {
            get
            {
                if (!this.IsRoot)
                    return this.GetRoot().SpecialModifier;
                if (this.isStatic)
                {
                    if (this.IsDefined(IntermediateGateway.CommonlyUsedTypeReferences.StandardModuleAttribute, false))
                        if (this.IsDefined(IntermediateGateway.CommonlyUsedTypeReferences.ExtensionAttribute, false))
                            return SpecialClassModifier.ExtensionTarget;
                        else
                            return SpecialClassModifier.Module;
                    else if (this.IsDefined(IntermediateGateway.CommonlyUsedTypeReferences.ExtensionAttribute, false))
                        return SpecialClassModifier.ExtensionTarget;
                    else
                        return SpecialClassModifier.Static;
                }
                else
                    return SpecialClassModifier.None;
            }
            set
            {
                if (!this.IsRoot)
                {
                    this.GetRoot().SpecialModifier = value;
                    return;
                }
                const int NONE = 0;
                const int STATIC = 1;
                const int MODULE = 2;
                const int EXTENSION = 4;

                if (value == SpecialModifier)
                    return;
                this.isStatic = (value != SpecialClassModifier.None);
                if ((value == SpecialClassModifier.Static) ||
                    (value == SpecialClassModifier.None))
                {
                    int current = NONE;
                    if (this.IsDefined(IntermediateGateway.CommonlyUsedTypeReferences.StandardModuleAttribute, false))
                        if (this.IsDefined(IntermediateGateway.CommonlyUsedTypeReferences.ExtensionAttribute, false))
                            current = EXTENSION | MODULE;
                        else
                            current = MODULE;
                    else if (this.IsDefined(typeof(ExtensionAttribute).GetTypeReference(), false))
                        current = EXTENSION;
                    else
                        current = STATIC;
                    if ((current & EXTENSION) == EXTENSION)
                        this.CustomAttributes.Remove(this.CustomAttributes[IntermediateGateway.CommonlyUsedTypeReferences.ExtensionAttribute]);
                    if ((current & MODULE) == MODULE)
                        this.CustomAttributes.Remove(this.CustomAttributes[IntermediateGateway.CommonlyUsedTypeReferences.StandardModuleAttribute]);
                }
                else if (value == SpecialClassModifier.Module)
                {
                    if (!this.IsDefined(IntermediateGateway.CommonlyUsedTypeReferences.StandardModuleAttribute, false))
                        this.CustomAttributes.Add(new CustomAttributeDefinition.ParameterValueCollection(IntermediateGateway.CommonlyUsedTypeReferences.StandardModuleAttribute));
                    if (this.IsDefined(IntermediateGateway.CommonlyUsedTypeReferences.ExtensionAttribute, false))
                        this.CustomAttributes.Remove(this.CustomAttributes[IntermediateGateway.CommonlyUsedTypeReferences.ExtensionAttribute]);
                }
                else if (value == SpecialClassModifier.ExtensionTarget)
                    if (!this.IsDefined(IntermediateGateway.CommonlyUsedTypeReferences.ExtensionAttribute, false))
                        this.CustomAttributes.Add(new CustomAttributeDefinition.ParameterValueCollection(IntermediateGateway.CommonlyUsedTypeReferences.ExtensionAttribute));
            }
        }

        public new IClassType BaseType
        {
            get
            {
                return this.baseType;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                if (value.Type == TypeKind.Dynamic)
                    throw new ArgumentOutOfRangeException("value", "Cannot have a dynamic type base.");
                this.baseType = value;
            }
        }

        #endregion

        #region IClassType Members

        public new IClassInterfaceMapping GetInterfaceMap(IInterfaceType type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns whether the <see cref="IClassType"/> has the attribute defined.
        /// </summary>
        /// <param name="attributeType">The type of attribute to check the 
        /// existence of.</param>
        /// <param name="inherit">Whether to check the inherited attributes of the
        /// <see cref="IClassType"/>.</param>
        /// <returns>true if the <paramref name="attributeType"/>
        /// is present on the current <see cref="IClassType"/> or one of
        /// its bases; false, otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="attributeType"/> is null.</exception>
        public bool IsDefined(IType attributeType, bool inherit)
        {
            if (!this.IsRoot)
                return this.GetRoot().IsDefined(attributeType, inherit);
            if (inherit && this.BaseType != null)
                return this.CustomAttributes.Contains(attributeType) || 
                       this.BaseType.IsDefined(attributeType, true);
            else
                return this.CustomAttributes.Contains(attributeType);
        }

        #endregion

        /// <summary>
        /// The <see cref="ITypeCollection"/> which represents the interfaces implemented by the
        /// <see cref="IntermediateClassType"/>.
        /// </summary>
        public new ITypeCollection ImplementedInterfaces
        {
            get
            {
                if (this.implementedInterfaces == null)
                    this.implementedInterfaces = new TypeCollection();
                return this.implementedInterfaces;
            }
        }

        protected override IndexerMember GetNewIndexer(TypedName nameAndReturn)
        {
            return new IntermediateClassIndexerMember<TInstanceIntermediateType>(nameAndReturn.Name, (TInstanceIntermediateType)(object)this)
            {
                PropertyType = nameAndReturn.Source == TypedNameSource.TypeReference ?
                               nameAndReturn.Reference :
                               nameAndReturn.Source == TypedNameSource.SymbolReference ?
                               nameAndReturn.SymbolReference.GetSymbolType() :
                               null
            };
        }

        protected override PropertyMember GetNewProperty(TypedName nameAndType)
        {
            return new IntermediateClassPropertyMember<TInstanceIntermediateType>(nameAndType.Name, (TInstanceIntermediateType)this) {
                PropertyType = nameAndType.Source == TypedNameSource.TypeReference ?
                   nameAndType.Reference :
                   nameAndType.Source == TypedNameSource.SymbolReference ?
                   nameAndType.SymbolReference.GetSymbolType() :
                   null
            };
        }
    }
}
