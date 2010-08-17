using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
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
    /// <paramref name="TIntermediateType"/>, <see cref="IIntermediateTypeParent"/></typeparam>
    public abstract class IntermediateGenericSegmentableType<TType, TIntermediateType, TInstanceIntermediateType> :
        IntermediateGenericTypeBase<TType, TIntermediateType>,
        IIntermediateSegmentableType<TType, TIntermediateType>
        where TType :
            class,
            IGenericType<TType>
        where TIntermediateType :
            class,
            IIntermediateGenericType<TType, TIntermediateType>,
            IIntermediateSegmentableType<TType, TIntermediateType>,
            TType
        where TInstanceIntermediateType :
            IntermediateGenericSegmentableType<TType, TIntermediateType, TInstanceIntermediateType>,
            TIntermediateType
    {
        private TInstanceIntermediateType rootType;
        private IIntermediateSegmentableDeclarationPartCollection<TIntermediateType> parts;
        /// <summary>
        /// Creates a new <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}"/> with the 
        /// <paramref name="name"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The name of the current <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}"/>.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/> which contains the current <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}"/>.</param>
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
        /// Creates a new <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// with the <paramref name="root"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="rootType">The root <typeparamref name="TInstanceIntermediateType"/>
        /// from which the current <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// is a part of.</param>
        /// <param name="parent">The <see cref="IIntermediateTypeParent"/> in which the 
        /// current <see cref="IntermediateGenericSegmentableType{TType, TIntermediateType, TInstanceIntermediateType}"/> 
        /// is contained.</param>
        /// <exception cref="System.ArgumentNullException">thrown when the <paramref name="root"/> or
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

        protected override void Disposed(bool dispose)
        {
            try
            {

                this.rootType = null;
            }
            finally
            {
                base.Disposed(dispose);
            }
        }

        #region IIntermediateSegmentableDeclaration<TIntermediateType> Members

        public IIntermediateSegmentableDeclarationPartCollection<TIntermediateType> Parts
        {
            get {
                if (this.parts == null)
                    this.parts = new IntermediateSegmentableTypePartCollection<TType, TIntermediateType, TInstanceIntermediateType>(((TInstanceIntermediateType)(this)), GetNewPartial);
                return this.parts;
            }
        }

        public TIntermediateType GetRoot()
        {
            if (this.IsRoot)
                return ((TIntermediateType)((object)(this)));
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

        public override AllenCopeland.Abstraction.Slf.Oil.Modules.IIntermediateModule DeclaringModule
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

        protected override IIntermediateGenericParameterDictionary<IGenericTypeParameter<TType>, IIntermediateGenericTypeParameter<TType, TIntermediateType>, TType, TIntermediateType> InitializeTypeParameters()
        {
            if (this.IsRoot)
                return base.InitializeTypeParameters();
            else
                return this.GetRoot().TypeParameters;
        }
    }
}
