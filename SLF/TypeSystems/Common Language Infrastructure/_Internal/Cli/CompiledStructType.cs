using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal sealed partial class CompiledStructType :
        CompiledGenericInstantiableTypeBase<IStructCtorMember, IStructEventMember, IStructFieldMember, IStructIndexerMember, IStructMethodMember, IStructPropertyMember, IStructType>,
        ICompiledStructType
    {
        /// <summary>
        /// Creates a new <see cref="CompiledStructType"/> with the 
        /// <paramref name="underlyingSystemType"/> provided.
        /// </summary>
        /// <param name="underlyingSystemType">The <see cref="System.Type"/> from which the current
        /// <see cref="CompiledStructType"/> is based.</param>
        internal CompiledStructType(System.Type underlyingSystemType)
            : base(underlyingSystemType)
        {
        }

        /// <summary>
        /// Obtains the <see cref="IStructType"/> relative to the
        /// <paramref name="typeParameters"/> provided.
        /// </summary>
        /// <param name="typeParameters">The <see cref="IType"/>
        /// series from which to create the generic type.</param>
        /// <returns>An <see cref="IStructType"/>
        /// instance which replaces the type-parameters
        /// contained within the <see cref="IStructType"/>.</returns>
        /// <remarks>Performs no type-parameter check.</remarks>
        protected override IStructType OnMakeGenericType(ITypeCollectionBase typeParameters)
        {
            return new _StructTypeBase(this, typeParameters);
        }

        /// <summary>
        /// Obtains a <see cref="IStructCtorMember"/> 
        /// for the <paramref name="info"/> provided.
        /// </summary>
        /// <param name="info">
        /// The <see cref="ConstructorInfo"/> to obtain the 
        /// <see cref="IStructCtorMember"/>
        /// for.</param>
        /// <returns>A new <see cref="IStructCtorMember"/> 
        /// instance with the <paramref name="info"/> provided.</returns>
        protected override IStructCtorMember GetConstructor(ConstructorInfo info)
        {
            return new ConstructorMember(info, this);
        }

        /// <summary>
        /// Obtains a <see cref="IStructMethodMember"/>
        /// for the <paramref name="info"/> provided.
        /// </summary>
        /// <param name="info">The <see cref="MethodInfo"/> 
        /// to obtain the <see cref="IStructMethodMember"/>
        /// for.</param>
        /// <returns>A new <see cref="IStructMethodMember"/> 
        /// instance with the <paramref name="info"/>
        /// provided.</returns>
        protected override IStructMethodMember GetMethod(MethodInfo info)
        {
            return new MethodMember(info, this);
        }

        /// <summary>
        /// Obtains a <see cref="IStructPropertyMember"/> for 
        /// the <paramref name="info"/> provided.
        /// </summary>
        /// <param name="info">The <see cref="PropertyInfo"/>
        /// to obtain the <see cref="IStructPropertyMember"/> for.
        /// </param>
        /// <returns>A new <see cref="IStructPropertyMember"/>
        /// instance with the <paramref name="info"/>
        /// provided.</returns>
        protected override IStructPropertyMember GetProperty(System.Reflection.PropertyInfo info)
        {
            return new PropertyMember(info, this);
        }

        /// <summary>
        /// Obtains a <see cref="IStructFieldMember"/> for the 
        /// <paramref name="FieldInfo"/> provided.
        /// </summary>
        /// <param name="info">The <see cref="FieldInfo"/> 
        /// to obtain the <see cref="IStructFieldMember"/>
        /// for.</param>
        /// <returns>A new <see cref="IStructFieldMember"/>
        /// with the <paramref name="info"/> provided.</returns>
        protected override IStructFieldMember GetField(System.Reflection.FieldInfo info)
        {
            return new FieldMember(info, this);
        }

        /// <summary>
        /// Obtains a <see cref="IStructEventMember"/> for the
        /// <paramref name="info"/> provided.
        /// </summary>
        /// <param name="info">The <see cref="EventInfo"/>
        /// to obtain the <see cref="IStructEventMember"/>
        /// for.</param>
        /// <returns>A new <see cref="IStructEventMember"/>
        /// with the <paramref name="info"/> provided.</returns>
        protected override IStructEventMember GetEvent(EventInfo info)
        {
            return new EventMember(info, this);
        }

        /// <summary>
        /// Obtains a <see cref="IStructIndexerMember"/> for the
        /// <paramref name="info"/> provided.
        /// </summary>
        /// <param name="info">The <see cref="PropertyInfo"/>
        /// to obtain the <see cref="IStructIndexerMember"/>
        /// for.</param>
        /// <returns>A new <see cref="IStructIndexerMember"/> 
        /// with the <paramref name="info"/> provided.</returns>
        protected override IStructIndexerMember GetIndexer(PropertyInfo info)
        {
            return new IndexerMember(info, this);
        }

        #region ICompiledStructType Members

        public new IStructInterfaceMapping GetInterfaceMap(IInterfaceType type)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected override IInterfaceMemberMapping<IStructMethodMember, IInterfaceMethodMember, IStructPropertyMember, IInterfacePropertyMember, IStructEventMember, IInterfaceEventMember, IStructIndexerMember, IInterfaceIndexerMember, IStructType, IInterfaceType> OnGetInterfaceMap(IInterfaceType type)
        {
            return this.GetInterfaceMap(type);
        }

        /// <summary>
        /// Returns the <see cref="TypeKind"/> the <see cref="CompiledStructType"/>
        /// is.
        /// </summary>
        /// <remarks>Returns <see cref="TypeKind.Struct"/>.</remarks>
        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Struct; }
        }

        protected override sealed IArrayType OnMakeArray(int rank)
        {
            if (this.UnderlyingSystemType == typeof(TypedReference) ||
                this.UnderlyingSystemType == typeof(ArgIterator))
                throw new InvalidOperationException("Type cannot be an array.");
            return base.OnMakeArray(rank);
        }
    }
}
