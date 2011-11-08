using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using Microsoft.VisualBasic.CompilerServices;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    /// <summary>
    /// Provides a base implementation of a compiled class enabling access
    /// to a <see cref="System.Type"/>'s members as a class.
    /// </summary>
    internal sealed partial class CompiledClassType :
        CompiledGenericInstantiableTypeBase 
        /* ^-- Compiled, generic and instantiable, a mouthfull for sure.*/
            <IClassCtorMember, IClassEventMember, IClassFieldMember, 
             IClassIndexerMember, IClassMethodMember, IClassPropertyMember,
             IClassType>,
        ICompiledClassType
    {
        private bool checkedSpecialModifier;
        private SpecialClassModifier specialModifier;

        /// <summary>
        /// Creates a new <see cref="CompiledClassType"/> with the <paramref name="underlyingSystemType"/>
        /// provided
        /// </summary>
        /// <param name="underlyingSystemType">The <see cref="System.Type"/> from which the current
        /// <see cref="CompiledClassType"/> is based.</param>
        internal CompiledClassType(Type underlyingSystemType)
            : base(underlyingSystemType)
        {
            if (!underlyingSystemType.IsClass)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.underlyingSystemType, ExceptionMessageId.CompiledType_NotProperKind, ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.@class));
        }

        /// <summary>
        /// Creates a new generic <see cref="IClassType"/> from the current generic type definition
        /// </summary>
        /// <param name="typeParameters">The <see cref="ITypeCollection"/> 
        /// of type-replacements.
        /// </param>
        /// <returns>A new <see cref="IClassType"/> as a closed generic type.</returns>
        protected override IClassType OnMakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            return new _ClassTypeBase(this, typeParameters);
        }

        #region GetMember Methods
        /// <summary>
        /// Obtains a <see cref="IClassCtorMember"/> 
        /// for the <paramref name="info"/> provided.
        /// </summary>
        /// <param name="info">
        /// The <see cref="ConstructorInfo"/> to obtain the 
        /// <see cref="IClassCtorMember"/>
        /// for.</param>
        /// <returns>A new <see cref="IClassCtorMember"/> 
        /// instance with the <paramref name="info"/> provided.</returns>
        protected override IClassCtorMember GetConstructor(ConstructorInfo info)
        {
            return new ConstructorMember(info, this);
        }

        /// <summary>
        /// Obtains a <see cref="IClassMethodMember"/>
        /// for the <paramref name="info"/> provided.
        /// </summary>
        /// <param name="info">The <see cref="MethodInfo"/> 
        /// to obtain the <see cref="IClassMethodMember"/>
        /// for.</param>
        /// <returns>A new <see cref="IClassMethodMember"/> 
        /// instance with the <paramref name="info"/>
        /// provided.</returns>
        protected override IClassMethodMember GetMethod(MethodInfo info)
        {
            return new MethodMember(info, this);
        }

        /// <summary>
        /// Obtains a <see cref="IClassPropertyMember"/> for 
        /// the <paramref name="info"/> provided.
        /// </summary>
        /// <param name="info">The <see cref="PropertyInfo"/>
        /// to obtain the <see cref="IClassPropertyMember"/> for.
        /// </param>
        /// <returns>A new <see cref="IClassPropertyMember"/>
        /// instance with the <paramref name="info"/>
        /// provided.</returns>
        protected override IClassPropertyMember GetProperty(PropertyInfo info)
        {
            return new PropertyMember(info, this);
        }

        /// <summary>
        /// Obtains a <see cref="IClassFieldMember"/> for the 
        /// <paramref name="info"/> provided.
        /// </summary>
        /// <param name="info">The <see cref="FieldInfo"/> 
        /// to obtain the <see cref="IClassFieldMember"/>
        /// for.</param>
        /// <returns>A new <see cref="IClassFieldMember"/>
        /// with the <paramref name="info"/> provided.</returns>
        protected override IClassFieldMember GetField(FieldInfo info)
        {
            return new FieldMember(info, this);
        }

        /// <summary>
        /// Obtains a <see cref="IClassEventMember"/> for the
        /// <paramref name="info"/> provided.
        /// </summary>
        /// <param name="info">The <see cref="EventInfo"/>
        /// to obtain the <see cref="IClassEventMember"/>
        /// for.</param>
        /// <returns>A new <see cref="IClassEventMember"/>
        /// with the <paramref name="info"/> provided.</returns>
        protected override IClassEventMember GetEvent(EventInfo info)
        {
            return new EventMember(info, this);
        }

        /// <summary>
        /// Obtains a <see cref="IClassIndexerMember"/> for the
        /// <paramref name="info"/> provided.
        /// </summary>
        /// <param name="info">The <see cref="PropertyInfo"/>
        /// to obtain the <see cref="IClassIndexerMember"/>
        /// for.</param>
        /// <returns>A new <see cref="IClassIndexerMember"/> 
        /// with the <paramref name="info"/> provided.</returns>
        protected override IClassIndexerMember GetIndexer(PropertyInfo info)
        {
            return new IndexerMember(info, this);
            //return new IndexerMember();
        }
        #endregion


        protected override ISignatureMemberMapping<IClassMethodMember, IInterfaceMethodMember, IClassPropertyMember, IInterfacePropertyMember, IClassEventMember, IInterfaceEventMember, IClassIndexerMember, IInterfaceIndexerMember, IClassType, IInterfaceType> OnGetInterfaceMap(IInterfaceType type)
        {
            return this.GetInterfaceMap(type);
        }

        #region IClassType Members

        public new IClassInterfaceMapping GetInterfaceMap(IInterfaceType type)
        {
            throw new NotImplementedException();
        }

        public SpecialClassModifier SpecialModifier
        {
            get
            {
                if (!this.checkedSpecialModifier)
                {
                    if (this.UnderlyingSystemType.IsDefined(typeof(ExtensionAttribute), false))
                        this.specialModifier = SpecialClassModifier.TypeExtensionSource;
                    else if (this.UnderlyingSystemType.DeclaringType == null && this.UnderlyingSystemType.IsDefined(typeof(StandardModuleAttribute), false))
                        this.specialModifier = SpecialClassModifier.Module;
                    else if (this.UnderlyingSystemType.IsSealed && this.UnderlyingSystemType.IsAbstract)
                        this.specialModifier = SpecialClassModifier.Static;
                    else
                        this.specialModifier = SpecialClassModifier.None;
                    this.checkedSpecialModifier = true;
                }
                return this.specialModifier;
            }
        }

        public bool IsDefined(IType attributeType, bool inherit)
        {
            if (attributeType == null)
                throw new ArgumentNullException("attributeType");
            foreach (ICustomAttributeInstance attributeInstance in this.CustomAttributes)
                if (attributeType.IsAssignableFrom(attributeInstance.Type))
                    if (inherit || attributeInstance.DeclarationPoint == this)
                        return true;
            return false;
        }

        public new IClassType BaseType
        {
            get { return (IClassType)base.BaseType; }
        }
        #endregion

        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Class; }
        }


    }
}
