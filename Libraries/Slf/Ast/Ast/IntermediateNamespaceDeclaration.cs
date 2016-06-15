using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System.ComponentModel;
using AllenCopeland.Abstraction.Utilities.Properties;
//using AllenCopeland.Abstraction.Slf.Cli;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides a base implementation of an intermediate namespace
    /// declaration.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Always)]
    public class IntermediateNamespaceDeclaration :
        IntermediateSegmentableDeclarationBase<IGeneralDeclarationUniqueIdentifier, IIntermediateNamespaceDeclaration, IntermediateNamespaceDeclaration>,
        IIntermediateNamespaceDeclaration
    {
        private IGeneralDeclarationUniqueIdentifier uniqueIdentifier;
        /// <summary>
        /// Data member for <see cref="Classes"/>.
        /// </summary>
        private IntermediateClassTypeDictionary classes;
        /// <summary>
        /// Data member for <see cref="Delegates"/>.
        /// </summary>
        private IntermediateDelegateTypeDictionary delegates;
        /// <summary>
        /// Data member for <see cref="Enums"/>.
        /// </summary>
        private IntermediateEnumTypeDictionary enums;
        /// <summary>
        /// Data member for <see cref="Interfaces"/>.
        /// </summary>
        private IntermediateInterfaceTypeDictionary interfaces;
        /// <summary>
        /// Data member for <see cref="Structs"/>.
        /// </summary>
        private IntermediateStructTypeDictionary structs;
        /// <summary>
        /// Data member for <see cref="Types"/>.
        /// </summary>
        private IntermediateFullTypeDictionary types;
        /// <summary>
        /// Data member for <see cref="Parent"/>.
        /// </summary>
        private IIntermediateNamespaceParent parent;
        /// <summary>
        /// Data member for <see cref="Namespaces"/>.
        /// </summary>
        private IIntermediateNamespaceDictionary namespaces;
        /// <summary>
        /// Data member for <see cref="_Members"/>
        /// </summary>
        private IntermediateFullMemberDictionary members;
        /// <summary>
        /// Data member for <see cref="Methods"/>.
        /// </summary>
        private IntermediateMethodMemberDictionary<ITopLevelMethodMember, IIntermediateTopLevelMethodMember, INamespaceParent, IIntermediateNamespaceParent> methods;
        /// <summary>
        /// Data member for <see cref="Fields"/>.
        /// </summary>
        private IntermediateFieldMemberDictionary<ITopLevelFieldMember, IIntermediateTopLevelFieldMember, INamespaceParent, IIntermediateNamespaceParent> fields;
        /// <summary>
        /// Data member for <see cref="ScopeCoercions"/>.
        /// </summary>
        private IScopeCoercionCollection scopeCoercions;

        /// <summary>
        /// Creates a new <see cref="IntermediateNamespaceDeclaration"/>
        /// with the <paramref name="name"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing the
        /// <see cref="IntermediateNamespaceDeclaration"/>'s name.</param>
        /// <param name="parent">The <see cref="IIntermediateNamespaceParent"/>
        /// which contains the <see cref="IntermediateNamespaceDeclaration"/>.</param>
        public IntermediateNamespaceDeclaration(string name, IIntermediateNamespaceParent parent)
            : base()
        {
            base.AssignName(name);
            this.parent = parent;
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateNamespaceDeclaration"/>
        /// instance with the root namespace declaration provided.
        /// </summary>
        /// <param name="rootDeclaration">The <see cref="IntermediateNamespaceDeclaration"/>
        /// which is the root instance.</param>
        /// <param name="parent">The <see cref="IIntermediateNamespaceParent"/>
        /// which contains the <see cref="IntermediateNamespaceDeclaration"/>.</param>
        public IntermediateNamespaceDeclaration(IntermediateNamespaceDeclaration rootDeclaration, IIntermediateNamespaceParent parent)
            : base(rootDeclaration)
        {
            this.parent = parent;
        }


        #region IIntermediateNamespaceDeclaration Members
        /// <summary>
        /// Visits a <see cref="IIntermediateDeclarationVisitor"/>.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateDeclarationVisitor"/> to visit.</param>
        public void Accept(IIntermediateDeclarationVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        /// Returns the <see cref="IIntermediateAssembly"/> associated
        /// to the <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        public IIntermediateAssembly Assembly
        {
            get
            {
                if (this.Parent == null)
                    return null;
                return this.Parent.Assembly;
            }
        }

        /// <summary>
        /// Returns the <see cref="IIntermediateNamespaceParent"/>
        /// which contains the <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        public IIntermediateNamespaceParent Parent
        {
            get
            {
                return this.parent;
            }
        }

        #endregion

        /// <summary>
        /// Returns the <see cref="String"/> value representing part or
        /// all of the unique identifier that makes up the 
        /// <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        /// <returns>A string value representing the name of the 
        /// <see cref="IntermediateNamespaceDeclaration"/>.</returns>
        /// <remarks>Overridden to accomodate for partial instance
        /// awareness.</remarks>
        protected override string OnGetName()
        {
            if (this.IsRoot)
            {
                return base.OnGetName();
            }
            else
                return this.GetRoot().Name;
        }

        /// <summary>
        /// Returns the unique identifier for the current 
        /// <see cref="IntermediateNamespaceDeclaration"/> where 
        /// <see cref="IntermediateDeclarationBase{TIdentifier}.Name"/> is not enough
        /// to distinguish between two 
        /// <see cref="IntermediateNamespaceDeclaration"/> entities.
        /// </summary>
        public override IGeneralDeclarationUniqueIdentifier UniqueIdentifier
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.uniqueIdentifier == null)
                        this.uniqueIdentifier = TypeSystemIdentifiers.GetDeclarationIdentifier(this.FullName);
                    return this.uniqueIdentifier;
                }
            }
        }

        #region IIntermediateNamespaceParent Members

        /// <summary>
        /// Returns the namespaces associated to the
        /// <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        public IIntermediateNamespaceDictionary Namespaces
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.namespaces == null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                        else
                            this.namespaces = this.InitializeNamespaces();
                    return this.namespaces;
                }
            }
        }

        #endregion

        #region IIntermediateTypeParent Members

        /// <summary>
        /// Returns the <see cref="IScopeCoercionCollection"/>
        /// associated to the scope coercions of the <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        public IScopeCoercionCollection ScopeCoercions
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.scopeCoercions == null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                        else
                            this.scopeCoercions = new ScopeCoercionCollection();
                    return this.scopeCoercions;
                }
            }
        }

        /// <summary>
        /// Returns the classes associated
        /// to the <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        public IIntermediateClassTypeDictionary Classes
        {
            get
            {
                this.CheckClasses();
                return this.classes;
            }
        }

        /// <summary>
        /// Returns the delegates associated
        /// to the <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        public IIntermediateDelegateTypeDictionary Delegates
        {
            get
            {
                this.CheckDelegates();
                return this.delegates;
            }
        }

        /// <summary>
        /// Returns the enumerations associated
        /// to the <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        public IIntermediateEnumTypeDictionary Enums
        {
            get
            {
                this.CheckEnums();
                return this.enums;
            }
        }

        /// <summary>
        /// Returns the interfaces associated
        /// to the <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        public IIntermediateInterfaceTypeDictionary Interfaces
        {
            get
            {
                this.CheckInterfaces();
                return this.interfaces;
            }
        }

        /// <summary>
        /// Returns the data structures associated
        /// to the <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        public IIntermediateStructTypeDictionary Structs
        {
            get
            {
                this.CheckStructs();
                return this.structs;
            }
        }

        /// <summary>
        /// Returns the full set of types associated to
        /// the <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
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

        private IntermediateFullTypeDictionary _Types
        {
            get
            {
                this.Check_Types();
                return this.types;
            }
        }

        #region ITypeParent Members

        IClassTypeDictionary ITypeParent.Classes
        {
            get
            {
                return this.Classes;
            }
        }

        IDelegateTypeDictionary ITypeParent.Delegates
        {
            get
            {
                return this.Delegates;
            }
        }

        IEnumTypeDictionary ITypeParent.Enums
        {
            get
            {
                return this.Enums;
            }
        }

        IInterfaceTypeDictionary ITypeParent.Interfaces
        {
            get
            {
                return this.Interfaces;
            }
        }

        IStructTypeDictionary ITypeParent.Structs
        {
            get
            {
                return this.Structs;
            }
        }

        IFullTypeDictionary ITypeParent.Types
        {
            get
            {
                return this.Types;
            }
        }

        IAssembly ITypeParent.Assembly
        {
            get
            {
                return this.Assembly;
            }
        }

        #endregion

        #region INamespaceParent Members

        INamespaceDictionary INamespaceParent.Namespaces
        {
            get
            {
                return this.Namespaces;
            }
        }

        #endregion

        #region INamespaceDeclaration Members

        /// <summary>
        /// Returns the <see cref="IntermediateNamespaceDeclaration"/>'s
        /// full name.
        /// </summary>
        public string FullName
        {
            get
            {
                return this.GetFullName();
            }
        }

        IAssembly INamespaceDeclaration.Assembly
        {
            get
            {
                return this.Assembly;
            }
        }

        INamespaceParent INamespaceDeclaration.Parent
        {
            get
            {
                return this.Parent;
            }
        }

        #endregion

        #region Initializers

        /// <summary>
        /// Initializes the child namespaces associated to the
        /// <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        /// <returns>A new <see cref="IIntermediateNamespaceDictionary"/>
        /// instance.</returns>
        protected virtual IIntermediateNamespaceDictionary InitializeNamespaces()
        {

            if (this.IsRoot)
                return new IntermediateNamespaceDictionary(this);
            else
                return new IntermediateNamespaceDictionary(this, (IntermediateNamespaceDictionary)this.GetRoot().Namespaces);
        }

        /// <summary>
        /// Initializes the child classes of the 
        /// <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        /// <returns>A new <see cref="IntermediateClassTypeDictionary"/>
        /// instance.</returns>
        protected virtual IntermediateClassTypeDictionary InitializeClasses()
        {
            if (this.IsRoot)
                return new IntermediateClassTypeDictionary(this, this._Types);
            else
                return new IntermediateClassTypeDictionary(this, this._Types, (IntermediateClassTypeDictionary)this.GetRoot().Classes);
        }

        /// <summary>
        /// Initializes the child delegates of the
        /// <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        /// <returns>A new <see cref="IntermediateDelegateTypeDictionary"/>
        /// instance.</returns>
        protected virtual IntermediateDelegateTypeDictionary InitializeDelegates()
        {
            if (this.IsRoot)
                return new IntermediateDelegateTypeDictionary(this, this._Types);
            else
                return new IntermediateDelegateTypeDictionary(this, this._Types, (IntermediateDelegateTypeDictionary)this.GetRoot().Delegates);
        }


        /// <summary>
        /// Initializes the child enumerations of the
        /// <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        /// <returns>A new <see cref="IntermediateEnumTypeDictionary"/>
        /// instance.</returns>
        protected virtual IntermediateEnumTypeDictionary InitializeEnums()
        {
            if (this.IsRoot)
                return new IntermediateEnumTypeDictionary(this, this._Types);
            else
                return new IntermediateEnumTypeDictionary(this, this._Types, (IntermediateEnumTypeDictionary)this.GetRoot().Enums);
        }

        /// <summary>
        /// Initializes the child interfaces of the
        /// <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        /// <returns>A new <see cref="IntermediateInterfaceTypeDictionary"/>
        /// instance.</returns>
        protected virtual IntermediateInterfaceTypeDictionary InitializeInterfaces()
        {
            if (this.IsRoot)
                return new IntermediateInterfaceTypeDictionary(this, this._Types);
            else
                return new IntermediateInterfaceTypeDictionary(this, this._Types, (IntermediateInterfaceTypeDictionary)this.GetRoot().Interfaces);
        }

        /// <summary>
        /// Initializes the child data structures of the
        /// <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        /// <returns>A new <see cref="IntermediateStructTypeDictionary"/>
        /// instance.</returns>
        protected virtual IntermediateStructTypeDictionary InitializeStructs()
        {
            if (this.IsRoot)
                return new IntermediateStructTypeDictionary(this, this._Types);
            else
                return new IntermediateStructTypeDictionary(this, this._Types, (IntermediateStructTypeDictionary)this.GetRoot().Structs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual IntermediateFieldMemberDictionary<ITopLevelFieldMember, IIntermediateTopLevelFieldMember, INamespaceParent, IIntermediateNamespaceParent> InitializeFields()
        {
            if (this.IsRoot)
                return new IntermediateTopLevelFieldMemberDictionary(this._Members, this);
            else
                return new IntermediateTopLevelFieldMemberDictionary(this._Members, this, this.GetRoot().Fields as IntermediateTopLevelFieldMemberDictionary);

        }

        protected virtual IntermediateMethodMemberDictionary<ITopLevelMethodMember, IIntermediateTopLevelMethodMember, INamespaceParent, IIntermediateNamespaceParent> InitializeMethods()
        {
            if (this.IsRoot)
                return new IntermediateTopLevelMethodMemberDictionary(this._Members, this);
            else
                return new IntermediateTopLevelMethodMemberDictionary(this._Members, this, this.GetRoot().Methods as IntermediateTopLevelMethodMemberDictionary);
        }

        /// <summary>
        /// Initializes the full types container to a default state if the 
        /// current <see cref="IntermediateNamespaceDeclaration"/> is 
        /// the root instance; otherwise, 
        /// </summary>
        /// <returns>A new <see cref="IntermediateFullTypeDictionary"/>
        /// instance.</returns>
        private IntermediateFullTypeDictionary InitializeTypes()
        {
            if (this.IsRoot)
                return new IntermediateFullTypeDictionary(this);
            else
                return new IntermediateFullTypeDictionary(this, this.GetRoot()._Types);
        }

        #endregion

        /// <summary>
        /// Instructs the <see cref="IntermediateNamespaceDeclaration"/>
        /// that the <paramref name="name"/> has been changed.
        /// </summary>
        /// <param name="name">The <see cref="String"/>
        /// value representing the new name of the 
        /// <see cref="IntermediateNamespaceDeclaration"/>.</param>
        /// <remarks>Overridden to accomodate for partial instance
        /// awareness.</remarks>
        protected override void OnSetName(string name)
        {
            if (this.IsRoot)
                base.OnSetName(name);
            else
                this.GetRoot().Name = name;
        }

        private static void SuspendCheck<TTypeIdentifier, TType, TIntermediateType>(IntermediateTypeDictionary<TTypeIdentifier, TType, TIntermediateType> dictionary, int suspendLevel)
            where TTypeIdentifier :
                ITypeUniqueIdentifier,
                IGeneralTypeUniqueIdentifier
            where TType :
                IType<TTypeIdentifier, TType>
            where TIntermediateType :
                class,
                IIntermediateType,
                TType
        {
            if (suspendLevel <= 0)
                return;
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");
            for (int i = 0; i < suspendLevel; i++)
                dictionary.Suspend();
        }

        #region Member Check Methods

        private void CheckClasses()
        {
            lock (this.SyncObject)
                if (this.classes == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.classes = this.InitializeClasses();
                    SuspendCheck(this.classes, this.suspendLevel);
                }
        }

        private void CheckDelegates()
        {
            lock (this.SyncObject)
                if (this.delegates == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.delegates = this.InitializeDelegates();
                    SuspendCheck(this.delegates, this.suspendLevel);
                }
        }

        private void CheckEnums()
        {
            lock (this.SyncObject)
                if (this.enums == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.enums = this.InitializeEnums();
                    SuspendCheck(this.enums, this.suspendLevel);
                }
        }

        private void CheckInterfaces()
        {
            lock (this.SyncObject)
                if (this.interfaces == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.interfaces = this.InitializeInterfaces();
                    SuspendCheck(this.interfaces, this.suspendLevel);
                }
        }

        private void CheckStructs()
        {
            lock (this.SyncObject)
                if (this.structs == null)
                {
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.structs = this.InitializeStructs();
                    SuspendCheck(this.structs, this.suspendLevel);
                }
        }

        private void Check_Types()
        {
            lock (this.SyncObject)
                if (this.types == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.types = this.InitializeTypes();
        }

        private void CheckFields()
        {
            lock (this.SyncObject)
                if (this.fields == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.fields = this.InitializeFields();
        }

        private void CheckMethods()
        {
            lock (this.SyncObject)
                if (this.methods == null)
                    if (this.IsDisposed)
                        throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                    else
                        this.methods = this.InitializeMethods();
        }

        #endregion

        /// <summary>
        /// Returns the <see cref="String"/> representation of the
        /// <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.FullName;
        }

        /// <summary>
        /// Disposes the <see cref="IntermediateNamespaceDeclaration"/>
        /// </summary>
        /// <param name="disposing">whether to dispose the managed 
        /// resources as well as the unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    lock (this.SyncObject)
                    {
                        this.parent = null;
                        if (this.methods != null)
                        {
                            this.methods.Dispose();
                            this.methods = null;
                        }
                        if (this.fields != null)
                        {
                            this.fields.Dispose();
                            this.fields = null;
                        }
                        if (this.members != null)
                        {
                            if (this.IsRoot)
                                this.members.Dispose();
                            else
                                this.members.ConditionalRemove(this);
                            this.members = null;
                        }
                        if (this.classes != null)
                        {
                            this.classes.Dispose();
                            this.classes = null;
                        }
                        if (this.enums != null)
                        {
                            this.enums.Dispose();
                            this.enums = null;
                        }
                        if (this.delegates != null)
                        {
                            this.delegates.Dispose();
                            this.delegates = null;
                        }
                        if (this.interfaces != null)
                        {
                            this.interfaces.Dispose();
                            this.interfaces = null;
                        }
                        if (this.structs != null)
                        {
                            this.structs.Dispose();
                            this.structs = null;
                        }
                        if (this.types != null)
                        {
                            if (this.IsRoot)
                                this.types.Dispose();
                            else
                                this.types.ConditionalRemove(this);
                            this.types = null;
                        }
                        if (this.namespaces != null)
                        {
                            this.namespaces.Dispose();
                            this.namespaces = null;
                        }
                        this.scopeCoercions = null;
                    }
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// Obtains a new <see cref="IntermediateNamespaceDeclaration"/> associated to the partial instance being created.
        /// </summary>
        /// <returns>A new <see cref="IntermediateNamespaceDeclaration"/> associated to the partial instance being created.</returns>
        protected override IntermediateNamespaceDeclaration GetNewPartial()
        {
            return new IntermediateNamespaceDeclaration(this, ObtainParentPartial());
        }

        /// <summary>
        /// Obtains a partial element of the parent which
        /// contains the <see cref="IntermediateNamespaceDeclaration"/> 
        /// if the parent is segmentable; otherwise, the current
        /// <see cref="Parent"/> is retrieved.
        /// </summary>
        /// <returns>A <see cref="IIntermediateNamespaceParent"/>
        /// that will contain the new partial obtained through
        /// <see cref="GetNewPartial"/>.</returns>
        protected IIntermediateNamespaceParent ObtainParentPartial()
        {

            var sParent = this.Parent as IIntermediateSegmentableDeclaration;
            /* *
             * The point of segmenting a namespace is to enable 
             * it to span multiple files.  As such, to ensure that
             * full support for this is enabled, all parents are broken
             * up as well, which means parent namespaces, assemblies,
             * and other namespace parents which allow partials.
             * *
             * To that end, just try to create a new partial of 
             * the parent and go from there.
             * *
             * An extra type-check on the new parent is added to
             * ensure they follow the rules of the architecture,
             * in case they do not, this code should still not 
             * fail.
             * */
            if (sParent != null)
                sParent = sParent.Parts.Add();
            var pParent = (sParent as IIntermediateNamespaceParent) ?? this.Parent;
            return pParent;
        }

        private int suspendLevel = 0;

        /// <summary>
        /// Suspends the duality in the type layout where members 
        /// inserted in methods, properties, events and so on are 
        /// dually inserted in a verbatim-order master set.
        /// </summary>
        /// <remarks>Incremental function, all resumes must
        /// be invoked prior to resuming the duality.</remarks>
        public void SuspendDualLayout()
        {
            lock (this.SyncObject)
            {
                this.suspendLevel++;
                if (this.methods != null)
                    this.methods.Suspend();
                if (this.fields != null)
                    this.fields.Suspend();
                if (this.classes != null)
                    this.classes.Suspend();
                if (this.delegates != null)
                    this.delegates.Suspend();
                if (this.enums != null)
                    this.enums.Suspend();
                if (this.interfaces != null)
                    this.interfaces.Suspend();
                if (this.structs != null)
                    this.structs.Suspend();
            }
        }

        /// <summary>
        /// Resumes the duality in the type layout where members
        /// inserted in methods, properties, events, and so on are
        /// dually inserted in a verbatim-order master set.
        /// </summary>
        /// <remarks>Incremental function, all resumes must
        /// be invoked prior to resuming the duality.</remarks>
        public void ResumeDualLayout()
        {
            lock (this.SyncObject)
            {
                if (this.suspendLevel == 0)
                    return;
                this.suspendLevel--;
                if (this.methods != null)
                    this.methods.Resume();
                if (this.fields != null)
                    this.fields.Resume();
                if (this.classes != null)
                    this.classes.Resume();
                if (this.delegates != null)
                    this.delegates.Resume();
                if (this.enums != null)
                    this.enums.Resume();
                if (this.interfaces != null)
                    this.interfaces.Resume();
                if (this.structs != null)
                    this.structs.Resume();
            }
        }

        /// <summary>
        /// Returns a series of string values which relate to the 
        /// identifiers contained within the <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        public IEnumerable<IGeneralDeclarationUniqueIdentifier> AggregateIdentifiers
        {
            get
            {
                return this.GetNamespaceParentIdentifiers(this.namespaces != null, this.types != null, this.members != null);
            }
        }

        #region IIntermediateNamespaceParent Members


        /// <summary>
        /// Returns the <see cref="IIntermediateFullMemberDictionary"/> containing the 
        /// grouped series of members associated to the
        /// <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        public IIntermediateFullMemberDictionary Members
        {
            get
            {
                this.CheckFields();
                this.CheckMethods();

                return this._Members;
            }
        }

        private IntermediateFullMemberDictionary _Members
        {
            get
            {
                lock (this.SyncObject)
                {
                    if (this.members == null)
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                        else
                            this.members = new IntermediateFullMemberDictionary();
                    return this.members;
                }
            }
        }

        #endregion

        #region IIntermediateFieldParent<ITopLevelFieldMember,IIntermediateTopLevelFieldMember,INamespaceParent,IIntermediateNamespaceParent> Members

        /// <summary>
        /// Returns the <see cref="IIntermediateFieldMemberDictionary{TField, TIntermediateField, TFieldParent, TIntermediateFieldParent}"/> 
        /// containing the fields defined on the current 
        /// <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        public IIntermediateFieldMemberDictionary<ITopLevelFieldMember, IIntermediateTopLevelFieldMember, INamespaceParent, IIntermediateNamespaceParent> Fields
        {
            get
            {
                this.CheckFields();
                lock (this.SyncObject)
                    return this.fields;
            }
        }

        #endregion

        #region IFieldParent<ITopLevelFieldMember,INamespaceParent> Members

        IFieldMemberDictionary<ITopLevelFieldMember, INamespaceParent> IFieldParent<ITopLevelFieldMember, INamespaceParent>.Fields
        {
            get { return this.Fields; }
        }

        #endregion

        #region IFieldParent Members

        IFieldMemberDictionary IFieldParent.Fields
        {
            get { return (IFieldMemberDictionary)this.Fields; }
        }

        #endregion

        #region IIntermediateFieldParent Members

        IIntermediateFieldMemberDictionary IIntermediateFieldParent.Fields
        {
            get { return (IIntermediateFieldMemberDictionary)this.Fields; }
        }

        #endregion

        #region IIntermediateMethodParent<ITopLevelMethodMember,IIntermediateTopLevelMethodMember,INamespaceParent,IIntermediateNamespaceParent> Members

        /// <summary>
        /// Returns the <see cref="IIntermediateMethodMemberDictionary{TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent}"/> 
        /// containing the methods defined on the current 
        /// <see cref="IntermediateNamespaceDeclaration"/>.
        /// </summary>
        public IIntermediateMethodMemberDictionary<ITopLevelMethodMember, IIntermediateTopLevelMethodMember, INamespaceParent, IIntermediateNamespaceParent> Methods
        {
            get
            {
                this.CheckMethods();
                lock (this.SyncObject)
                    return this.methods;
            }
        }

        #endregion

        #region IIntermediateMethodParent Members

        IIntermediateMethodMemberDictionary IIntermediateMethodParent.Methods
        {
            get { return (IIntermediateMethodMemberDictionary)this.Methods; }
        }

        #endregion

        #region IMethodParent Members

        IMethodMemberDictionary IMethodParent.Methods
        {
            get { return (IMethodMemberDictionary)this.Methods; }
        }

        #endregion

        #region IMethodParent<ITopLevelMethodMember,INamespaceParent> Members

        IMethodMemberDictionary<ITopLevelMethodMember, INamespaceParent> IMethodParent<ITopLevelMethodMember, INamespaceParent>.Methods
        {
            get { return this.Methods; }
        }

        #endregion

        #region INamespaceParent Members

        IFullMemberDictionary INamespaceParent.Members
        {
            get
            {
                return this.Members;
            }
        }

        #endregion

        protected override string OnGetIdentityName()
        {
            return ThrowHelper.GetArgumentExceptionWord(ExceptionWordId.@namespace);
        }

        public IIntermediateIdentityManager IdentityManager { get { return this.Parent.IdentityManager; } }

        public IEnumerable<IType> GetTypes()
        {
            lock (this.SyncObject)
                return this.types.GetTypes().Concat(this.namespaces.GetTypes());
        }

        protected override void ClearIdentifier()
        {
            lock (this.SyncObject)
                if (this.uniqueIdentifier != null)
                    this.uniqueIdentifier = null;
        }

        /// <summary>
        /// Visits the <paramref name="visitor"/> provided.
        /// </summary>
        /// <typeparam name="TResult">The type of value to return for the visitor.</typeparam>
        /// <typeparam name="TContext">The type of context passed to the visitor.</typeparam>
        /// <param name="visitor">The <see cref="IIntermediateDeclarationVisitor{TResult, TContext}"/>
        /// which should receive the <see cref="IntermediateNamespaceDeclaration"/> as a visitor.</param>
        /// <param name="context">The <typeparamref name="TContext"/> relative to the current
        /// implementation.</param>
        public virtual TResult Accept<TResult, TContext>(IIntermediateDeclarationVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public bool HasScopeCoercions
        {
            get
            {
                return this.scopeCoercions != null && this.scopeCoercions.Count > 0;
            }
        }

        public bool HasClasses
        {
            get
            {
                return this.classes != null && this.classes.Count > 0;
            }
        }

        public bool HasDelegates
        {
            get
            {
                return this.delegates != null && this.delegates.Count > 0;
            }
        }

        public bool HasEnums
        {
            get
            {
                return this.enums != null && this.enums.Count > 0;
            }
        }

        public bool HasInterfaces
        {
            get
            {
                return this.interfaces != null && this.interfaces.Count > 0;
            }
        }

        public bool HasStructs
        {
            get
            {
                return this.structs != null && this.structs.Count > 0;
            }
        }

        public bool HasTypes
        {
            get
            {
                return this.types != null && this.types.Count > 0;
            }
        }

        public bool HasFields
        {
            get
            {
                return this.fields != null && this.fields.Count > 0;
            }
        }

        public bool HasMethods
        {
            get
            {
                return this.methods != null && this.methods.Count > 0;
            }
        }
    }
}
