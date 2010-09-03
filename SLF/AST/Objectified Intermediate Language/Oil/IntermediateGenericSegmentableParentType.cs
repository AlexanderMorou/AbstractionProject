using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf._Internal.Ast;

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a generic base class for intermediate types which are generic,
    /// can span multiple instances, and can contain types of their own.
    /// </summary>
    /// <typeparam name="TType">The kind of type as it exists in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The kind of type as it exists in the 
    /// intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TInstanceIntermediateType">The direct kind of type
    /// used to instantiate the partial elements within the system.</typeparam>
    public abstract class IntermediateGenericSegmentableParentType<TType, TIntermediateType, TInstanceIntermediateType> :
        IntermediateGenericSegmentableType<TType, TIntermediateType, TInstanceIntermediateType>,
        IIntermediateTypeParent
        where TType :
            class,
            IGenericType<TType>,
            ITypeParent
        where TIntermediateType :
            class,
            IIntermediateGenericType<TType, TIntermediateType>,
            IIntermediateSegmentableType<TType, TIntermediateType>,
            IIntermediateTypeParent,
            TType
        where TInstanceIntermediateType :
            IntermediateGenericSegmentableType<TType, TIntermediateType, TInstanceIntermediateType>,
            TIntermediateType
    {
        #region IntermediateGenericSegmentableParentType data members

        #region Nested Type Data Members
        /// <summary>
        /// Data member fro <see cref="Classes"/>.
        /// </summary>
        private IIntermediateClassTypeDictionary classes;
        /// <summary>
        /// Data member for <see cref="Delegates"/>.
        /// </summary>
        private IIntermediateDelegateTypeDictionary delegates;
        /// <summary>
        /// Data member for <see cref="Enums"/>.
        /// </summary>
        private IIntermediateEnumTypeDictionary enums;
        /// <summary>
        /// Data member for <see cref="Interfaces"/>.
        /// </summary>
        private IIntermediateInterfaceTypeDictionary interfaces;
        /// <summary>
        /// Data member for <see cref="Structs"/>.
        /// </summary>
        private IIntermediateStructTypeDictionary structs;
        /// <summary>
        /// Data member for <see cref="Types"/>.
        /// </summary>
        private IntermediateFullTypeDictionary types;
        #endregion

        #endregion

        protected IntermediateGenericSegmentableParentType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }

        protected IntermediateGenericSegmentableParentType(IIntermediateTypeParent parent)
            : base(parent)
        {

        }
        protected IntermediateGenericSegmentableParentType(TInstanceIntermediateType rootType, IIntermediateTypeParent parent)
            : base(rootType, parent)
        {
        }


        #region IIntermediateTypeParent Members

        public IIntermediateClassTypeDictionary Classes
        {
            get
            {
                this.CheckClasses();
                return this.classes;
            }
        }

        public IIntermediateDelegateTypeDictionary Delegates
        {
            get
            {
                this.CheckDelegates();
                return this.delegates;
            }
        }

        public IIntermediateEnumTypeDictionary Enums
        {
            get
            {
                this.CheckEnums();
                return this.enums;
            }
        }

        public IIntermediateInterfaceTypeDictionary Interfaces
        {
            get
            {
                this.CheckInterfaces();
                return this.interfaces;
            }
        }

        public IIntermediateStructTypeDictionary Structs
        {
            get
            {
                this.CheckStructs();
                return this.structs;
            }
        }

        private IntermediateFullTypeDictionary _Types
        {
            get
            {
                this.Check_Types();
                return this.types;
            }
        }

        public IIntermediateFullTypeDictionary Types
        {
            get
            {
                this.CheckClasses();
                this.CheckDelegates();
                this.CheckEnums();
                this.CheckInterfaces();
                this.CheckStructs();
                return this._Types;
            }
        }

        #endregion

        #region ITypeParent Members

        IClassTypeDictionary ITypeParent.Classes
        {
            get { return this.Classes; }
        }

        IDelegateTypeDictionary ITypeParent.Delegates
        {
            get { return this.Delegates; }
        }

        IEnumTypeDictionary ITypeParent.Enums
        {
            get { return this.Enums; }
        }

        IInterfaceTypeDictionary ITypeParent.Interfaces
        {
            get { return this.Interfaces; }
        }

        IStructTypeDictionary ITypeParent.Structs
        {
            get { return this.Structs; }
        }

        IFullTypeDictionary ITypeParent.Types
        {
            get { return this.Types; }
        }

        #endregion

        #region Member Check Methods

        private void CheckClasses()
        {
            if (this.classes == null)
                this.classes = this.InitializeClasses();
        }

        private void CheckDelegates()
        {
            if (this.delegates == null)
                this.delegates = this.InitializeDelegates();
        }

        private void CheckEnums()
        {
            if (this.enums == null)
                this.enums = this.InitializeEnums();
        }

        private void CheckInterfaces()
        {
            if (this.interfaces == null)
                this.interfaces = this.InitializeInterfaces();
        }

        private void CheckStructs()
        {
            if (this.structs == null)
                this.structs = this.InitializeStructs();
        }

        private void Check_Types()
        {
            if (this.types == null)
                this.types = this.InitializeTypes();
        }
        #endregion

        #region Initializers

        /// <summary>
        /// Initializes the <see cref="Classes"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateClassTypeDictionary"/> instance.</returns>
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone class type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// class type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TType, TIntermediateType}.Parent"/>
        /// of the dictionary for the current instance.  Add methods called upon the
        /// instance provided here report the proper partial instance as the parent.</remarks>
        protected virtual IntermediateClassTypeDictionary InitializeClasses()
        {
            if (this.IsRoot)
                return new IntermediateClassTypeDictionary(this, this._Types);
            else
                return new IntermediateClassTypeDictionary(this, this._Types, (IntermediateClassTypeDictionary)this.GetRoot().Classes);
        }

        /// <summary>
        /// Initializes the <see cref="Delegates"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateDelegateTypeDictionary"/> instance</returns>
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone delegate type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// delegate type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TType, TIntermediateType}.Parent"/>
        /// of the dictionary for the current instance.  Add methods called upon the
        /// instance provided here report the proper partial instance as the parent.</remarks>
        protected virtual IntermediateDelegateTypeDictionary InitializeDelegates()
        {
            if (this.IsRoot)
                return new IntermediateDelegateTypeDictionary(this, this._Types);
            else
                return new IntermediateDelegateTypeDictionary(this, this._Types, (IntermediateDelegateTypeDictionary)this.GetRoot().Delegates);
        }

        /// <summary>
        /// Initializes the <see cref="Enums"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateEnumTypeDictionary"/> instance</returns>
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone enum type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// enum type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TType, TIntermediateType}.Parent"/>
        /// of the dictionary for the current instance.  Add methods called upon the
        /// instance provided here report the proper partial instance as the parent.</remarks>
        protected virtual IntermediateEnumTypeDictionary InitializeEnums()
        {
            if (this.IsRoot)
                return new IntermediateEnumTypeDictionary(this, this._Types);
            else
                return new IntermediateEnumTypeDictionary(this, this._Types, (IntermediateEnumTypeDictionary)this.GetRoot().Enums);
        }

        /// <summary>
        /// Initializes the <see cref="Interfaces"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateInterfaceTypeDictionary"/> instance</returns>
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone interface type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// interface type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TType, TIntermediateType}.Parent"/>
        /// of the dictionary for the current instance.  Add methods called upon the
        /// instance provided here report the proper partial instance as the parent.</remarks>
        protected virtual IntermediateInterfaceTypeDictionary InitializeInterfaces()
        {
            if (this.IsRoot)
                return new IntermediateInterfaceTypeDictionary(this, this._Types);
            else
                return new IntermediateInterfaceTypeDictionary(this, this._Types, (IntermediateInterfaceTypeDictionary)this.GetRoot().Interfaces);
        }

        /// <summary>
        /// Initializes the <see cref="Structs"/> property.
        /// </summary>
        /// <returns>A new <see cref="IntermediateStructTypeDictionary"/> instance</returns>
        /// <remarks>If <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}.IsRoot"/>
        /// is true, this creates a new standalone struct type dictionary linked to the master
        /// <see cref="Types"/> dictionary; however, if false, it creates a dependent 
        /// struct type dictionary which mirrors the members of the root declaration and all other
        /// partial instances.  Parent target discernment is provided by the 
        /// <see cref="IntermediateTypeDictionary{TType, TIntermediateType}.Parent"/>
        /// of the dictionary for the current instance.  Add methods called upon the
        /// instance provided here report the proper partial instance as the parent.</remarks>
        protected virtual IntermediateStructTypeDictionary InitializeStructs()
        {
            if (this.IsRoot)
                return new IntermediateStructTypeDictionary(this, this._Types);
            else
                return new IntermediateStructTypeDictionary(this, this._Types, (IntermediateStructTypeDictionary)this.GetRoot().Structs);
        }

        /// <summary>
        /// Initializes the full types container to a default state if the 
        /// current <see cref="IntermediateNamespaceDeclaration"/> is 
        /// the root instance; otherwise, 
        /// </summary>
        /// <returns>A new <see cref="IntermediateFullTypeDictionary"/> instance</returns>
        protected virtual IntermediateFullTypeDictionary InitializeTypes()
        {
            if (this.IsRoot)
                return new IntermediateFullTypeDictionary(this);
            else
                return new IntermediateFullTypeDictionary(this, ((IntermediateGenericSegmentableParentType<TType, TIntermediateType, TInstanceIntermediateType>)(object)this.GetRoot())._Types);
        }

        #endregion

        internal override void OnRearrangedInner(int from, int to)
        {
            int baseLine = -this.GenericParameters.Count;
            int realFrom = baseLine + from;
            int realTo = baseLine + to;
            foreach (var element in from subTypeEntry in this._Types.Values
                                    let subType = subTypeEntry.Entry
                                    let genericSubType = subType as _IIntermediateGenericType
                                    where genericSubType != null
                                    select genericSubType)
                element.Rearranged(realFrom, realTo);
            base.OnRearrangedInner(from, to);
        }

        protected override IEnumerable<IDeclaration> OnGetDeclarations()
        {
            return GetTypeParentDeclarations(this);
        }
    }
}
