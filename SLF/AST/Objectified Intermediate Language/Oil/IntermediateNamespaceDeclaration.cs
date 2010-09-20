using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Oil;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public class IntermediateNamespaceDeclaration :
        IntermediateSegmentableDeclarationBase<IIntermediateNamespaceDeclaration, IntermediateNamespaceDeclaration>,
        IIntermediateNamespaceDeclaration
    {
        /// <summary>
        /// Data member fro <see cref="Classes"/>.
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
        public IntermediateNamespaceDeclaration(string name, IIntermediateNamespaceParent parent)
            : base()
        {
            base.Name = name;
            this.parent = parent;
        }
        /// <summary>
        /// Creates a new <see cref="IIntermediateNamespaceDeclaration"/>
        /// instance with the root namespace declaration provided.
        /// </summary>
        /// <param name="rootDeclaration">The <see cref="IntermediateNamespaceDeclaration"/>
        /// which is the root instance.</param>
        public IntermediateNamespaceDeclaration(IntermediateNamespaceDeclaration rootDeclaration, IIntermediateNamespaceParent parent)
            : base(rootDeclaration)
        {
            this.parent = parent;
        }


        #region IIntermediateNamespaceDeclaration Members

        public IIntermediateAssembly Assembly
        {
            get
            {
                if (this.Parent == null)
                    return null;
                return this.Parent.Assembly;
            }
        }

        public IIntermediateNamespaceParent Parent
        {
            get
            {
                return this.parent;
            }
        }

        #endregion

        protected override string OnGetName()
        {
            if (this.IsRoot)
            {
                return base.OnGetName();
            }
            else
                return this.GetRoot().Name;
        }

        public override string UniqueIdentifier
        {
            get { return this.Name; }
        }


        #region IIntermediateNamespaceParent Members

        public IIntermediateNamespaceDictionary Namespaces
        {
            get
            {
                if (this.IsRoot)
                {
                    if (this.namespaces == null)
                        this.namespaces = this.InitializeNamespaces();
                    return this.namespaces;
                }
                else
                    return this.GetRoot().Namespaces;
            }
        }

        #endregion

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

        public string FullName
        {
            get {
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

        protected virtual IIntermediateNamespaceDictionary InitializeNamespaces()
        {

            if (this.IsRoot)
                return new IntermediateNamespaceDictionary(this);
            else
                return new IntermediateNamespaceDictionary(this, (IntermediateNamespaceDictionary)this.GetRoot().Namespaces);
        }

        protected virtual IntermediateClassTypeDictionary InitializeClasses()
        {
            if (this.IsRoot)
                return new IntermediateClassTypeDictionary(this, this._Types);
            else
                return new IntermediateClassTypeDictionary(this, this._Types, (IntermediateClassTypeDictionary)this.GetRoot().Classes);
        }

        protected virtual IntermediateDelegateTypeDictionary InitializeDelegates()
        {
            if (this.IsRoot)
                return new IntermediateDelegateTypeDictionary(this, this._Types);
            else
                return new IntermediateDelegateTypeDictionary(this, this._Types, (IntermediateDelegateTypeDictionary)this.GetRoot().Delegates);
        }

        protected virtual IntermediateEnumTypeDictionary InitializeEnums()
        {
            if (this.IsRoot)
                return new IntermediateEnumTypeDictionary(this, this._Types);
            else
                return new IntermediateEnumTypeDictionary(this, this._Types, (IntermediateEnumTypeDictionary)this.GetRoot().Enums);
        }

        protected virtual IntermediateInterfaceTypeDictionary InitializeInterfaces()
        {
            if (this.IsRoot)
                return new IntermediateInterfaceTypeDictionary(this, this._Types);
            else
                return new IntermediateInterfaceTypeDictionary(this, this._Types, (IntermediateInterfaceTypeDictionary)this.GetRoot().Interfaces);
        }

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
        /// <returns></returns>
        private IntermediateFullTypeDictionary InitializeTypes()
        {
            if (this.IsRoot)
                return new IntermediateFullTypeDictionary(this);
            else
                return new IntermediateFullTypeDictionary(this, ((IntermediateNamespaceDeclaration)(this.GetRoot()))._Types);
        }

        #endregion

        protected override void OnSetName(string value)
        {
            if (this.IsRoot)
                base.OnSetName(value);
            else
                this.GetRoot().Name = value;
        }

        private static void SuspendCheck<TType, TIntermediateType>(IntermediateTypeDictionary<TType, TIntermediateType> dictionary, int suspendLevel)
            where TType :
                IType<TType>
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
            if (this.classes == null)
            {
                this.classes = this.InitializeClasses();
                SuspendCheck(this.classes, this.suspendLevel);
            }
        }

        private void CheckDelegates()
        {
            if (this.delegates == null)
            {
                this.delegates = this.InitializeDelegates();
                SuspendCheck(this.delegates, this.suspendLevel);
            }
        }

        private void CheckEnums()
        {
            if (this.enums == null)
            {
                this.enums = this.InitializeEnums();
                SuspendCheck(this.enums, this.suspendLevel);
            }
        }

        private void CheckInterfaces()
        {
            if (this.interfaces == null)
            {
                this.interfaces = this.InitializeInterfaces();
                SuspendCheck(this.interfaces, this.suspendLevel);
            }
        }

        private void CheckStructs()
        {
            if (this.structs == null)
            {
                this.structs = this.InitializeStructs();
                SuspendCheck(this.structs, this.suspendLevel);
            }
        }

        private void Check_Types()
        {
            if (this.types == null)
                this.types = this.InitializeTypes();
        }
        #endregion

        public override string ToString()
        {
            return this.FullName;
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
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
                    this.types = null;
                if (this.namespaces != null)
                {
                    this.namespaces.Dispose();
                    this.namespaces = null;
                }

            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        protected override IntermediateNamespaceDeclaration GetNewPartial()
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
            var pParent = (sParent == null ? this.Parent : (sParent as IIntermediateNamespaceParent) ?? this.Parent);
            return new IntermediateNamespaceDeclaration(this, pParent);
        }
        private int suspendLevel = 0;

        public void SuspendDualLayout()
        {
            this.suspendLevel++;
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

        public void ResumeDualLayout()
        {
            if (this.suspendLevel == 0)
                return;
            this.suspendLevel--;
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
}
