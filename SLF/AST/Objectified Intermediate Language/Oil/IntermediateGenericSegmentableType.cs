using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Utilities.Properties;
using System.ComponentModel;
using AllenCopeland.Abstraction.Slf.Oil.Modules;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    /// <summary>
    /// Provides a base implementation of an intermediate generic type which can be segmented
    /// into multiple instances.
    /// </summary>
    /// <typeparam name="TType">The type of generic type in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateType">The type of generic segmentable type in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TInstanceIntermediateType">The type which will be instanced by the 
    /// partial declaration system; must contain a public constructor with the following parameters:
    /// <typeparamref name="TIntermediateType"/>, <see cref="IIntermediateTypeParent"/></typeparam>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class IntermediateGenericSegmentableType<TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType> :
        IntermediateGenericTypeBase<TTypeIdentifier, TType, TIntermediateType>,
        IIntermediateSegmentableType<TTypeIdentifier, TType, TIntermediateType>
        where TTypeIdentifier : 
            IGenericTypeUniqueIdentifier<TTypeIdentifier>
        where TType :
            class,
            IGenericType<TTypeIdentifier, TType>
        where TIntermediateType :
            class,
            IIntermediateGenericType<TTypeIdentifier, TType, TIntermediateType>,
            IIntermediateSegmentableType<TTypeIdentifier, TType, TIntermediateType>,
            TType
        where TInstanceIntermediateType :
            IntermediateGenericSegmentableType<TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType>,
            TIntermediateType
    {
        private TInstanceIntermediateType rootType;
        private IIntermediateSegmentableDeclarationPartCollection<TTypeIdentifier, TIntermediateType> parts;
        /// <summary>
        /// Creates a new <see cref="IntermediateGenericSegmentableType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/> with the 
        /// <paramref name="name"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The name of the current <see cref="IntermediateGenericSegmentableType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/> which contains the current <see cref="IntermediateGenericSegmentableType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>, or <paramref name="parent"/>, is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="name"/> is 
        /// <see cref="String.Empty"/></exception>
        protected IntermediateGenericSegmentableType(string name, IIntermediateTypeParent parent)
            : base(name, parent)
        {
        }

        protected IntermediateGenericSegmentableType(IIntermediateTypeParent parent)
            : base(parent)
        {

        }

        /// <summary>
        /// Creates a new <see cref="IntermediateGenericSegmentableType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// with the <paramref name="rootType"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="rootType">The root <typeparamref name="TInstanceIntermediateType"/>
        /// from which the current <see cref="IntermediateGenericSegmentableType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// is a part of.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/> in which the 
        /// current <see cref="IntermediateGenericSegmentableType{TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType}"/> 
        /// is contained.</param>
        /// <exception cref="System.ArgumentNullException">thrown when the <paramref name="rootType"/> or
        /// <paramref name="parent"/> is null.</exception>
        /// <exception cref="System.ArgumentException">thrown when <paramref name="parent"/>
        /// is not the root's parent</exception>
        protected IntermediateGenericSegmentableType(TInstanceIntermediateType rootType, IIntermediateTypeParent parent)
            : base(parent)
        {
            if (rootType == null)
                throw new ArgumentNullException("rootType");
            this.rootType = rootType;
        }

        protected override string OnGetName()
        {
            if (this.IsRoot)
                return base.OnGetName();
            else
                return this.GetRoot().Name;
        }

        protected override void OnSetName(string value)
        {
            if (this.IsRoot)
                base.OnSetName(value);
            else
                this.GetRoot().Name = value;
        }

        public override void Dispose()
        {
            try
            {
                if (this.IsRoot)
                {
                    if (this.parts != null)
                    {
                        foreach (var part in this.parts)
                            part.Dispose();
                        this.parts = null;
                    }
                }
                else
                    this.rootType = null;
            }
            finally
            {
                base.Dispose();
            }
        }

        #region IIntermediateSegmentableDeclaration<TIntermediateType> Members

        public IIntermediateSegmentableDeclarationPartCollection<TTypeIdentifier, TIntermediateType> Parts
        {
            get {
                if (this.IsRoot)
                {
                    if (this.parts == null)
                    {
                        if (this.IsDisposed)
                            throw new InvalidOperationException(Resources.ObjectStateThrowMessage);
                        this.parts = new IntermediateSegmentableTypePartCollection<TTypeIdentifier, TType, TIntermediateType, TInstanceIntermediateType>(((TInstanceIntermediateType)(this)), GetNewPartial);
                    }
                    return this.parts;
                }
                else
                    return this.GetRoot().Parts;
            }
        }

        TIntermediateType IIntermediateSegmentableDeclaration<TTypeIdentifier, TIntermediateType>.GetRoot()
        {
            return this.GetRoot();
        }

        public TInstanceIntermediateType GetRoot()
        {
            if (this.IsRoot)
                return (TInstanceIntermediateType)this;
            else
                return this.rootType;
        }

        #endregion

        #region IIntermediateSegmentableDeclaration Members

        public bool IsRoot
        {
            get { return this.rootType == null; }
        }

        IIntermediateSegmentableDeclaration IIntermediateSegmentableDeclaration.GetRoot()
        {
            return this.GetRoot();
        }

        IIntermediateSegmentableDeclarationPartCollection IIntermediateSegmentableDeclaration.Parts
        {
            get {
                return (IIntermediateSegmentableDeclarationPartCollection)this.Parts;
            }
        }

        #endregion

        protected abstract TInstanceIntermediateType GetNewPartial(TInstanceIntermediateType root, IIntermediateTypeParent parent);

        public override IIntermediateModule DeclaringModule
        {
            get
            {
                if (this.IsRoot)
                    return base.DeclaringModule;
                else
                    return this.GetRoot().DeclaringModule;
            }
            set
            {
                if (this.IsRoot)
                    base.DeclaringModule = value;
                else
                    this.GetRoot().DeclaringModule = value;
            }
        }

        protected override TypeParameterDictionary InitializeTypeParameters()
        {
            if (this.IsRoot)
                return base.InitializeTypeParameters();
            else
                return (TypeParameterDictionary)this.GetRoot().TypeParameters;
        }

        protected bool ArePartsInitialized
        {
            get
            {
                return this.parts == null;
            }
        }
    }
}
