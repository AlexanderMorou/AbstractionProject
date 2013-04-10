using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Properties;
using AllenCopeland.Abstraction.Slf.Cli;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal abstract partial class CliGenericTypeBase<TIdentifier, TType> :
        CliTypeBase<TIdentifier>,
        IGenericType<TIdentifier, TType>
        where TIdentifier :
            IGenericTypeUniqueIdentifier
        where TType :
            IGenericType<TIdentifier, TType>
    {
        private GenericTypeCache genericCache = null;
        private ILockedTypeCollection genericParameters;
        private TypeParameterDictionary typeParameters;
        protected CliGenericTypeBase(CliAssembly assembly, ICliMetadataTypeDefinitionTableRow metadataEntry)
            : base(assembly, metadataEntry)
        {
        }

        #region IGenericType<TIdentifier,TType> Members

        public TType MakeGenericClosure(IControlledTypeCollection typeParameters)
        {
            LockedTypeCollection lockedTypeParameters = typeParameters.ToLockedCollection();
            IGenericType genericResult;
            lock (this.SyncObject)
            {
                if (this.genericCache != null && genericCache.TryObtainGenericClosure(lockedTypeParameters, out genericResult))
                    return (TType)genericResult;
                else
                {
                    if (this.genericCache == null)
                        this.genericCache = new GenericTypeCache();
                    var result = this.OnMakeGenericClosure(lockedTypeParameters);
                    this.genericCache.RegisterGenericType(result, lockedTypeParameters);
                    return result;
                }
            }
        }

        protected abstract TType OnMakeGenericClosure(LockedTypeCollection lockedTypeParameters);

        public TType MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters.ToCollection());
        }

        #endregion

        #region IType<TIdentifier,TType> Members

        public new TType ElementType
        {
            get { throw new InvalidOperationException(Resources.ObjectStateThrowMessage); }
        }

        #endregion

        #region IGenericParamParent<IGenericTypeParameter<TIdentifier,TType>,TType> Members

        public IGenericParameterDictionary<IGenericTypeParameter<TIdentifier, TType>, TType> TypeParameters
        {
            get
            {
                if (this.typeParameters == null)
                    this.typeParameters = new TypeParameterDictionary(this);
                return this.typeParameters;
            }
        }

        #endregion

        #region IGenericParamParent Members

        public bool IsGenericDefinition
        {
            get { return true; }
        }

        IGenericParameterDictionary IGenericParamParent.TypeParameters
        {
            get { return (IGenericParameterDictionary)this.TypeParameters; }
        }

        IGenericParamParent IGenericParamParent.MakeGenericClosure(IControlledTypeCollection typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        IGenericParamParent IGenericParamParent.MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        public bool ContainsGenericParameters
        {
            get { return this.IsGenericConstruct; }
        }

        public ILockedTypeCollection GenericParameters
        {
            get
            {
                if (this.genericParameters == null)
                {
                    if (this.Parent is IGenericType)
                    {
                        var genericParent = (IGenericType)this.Parent;
                        if (genericParent.IsGenericConstruct)
                            this.genericParameters = new LockedTypeCollection(genericParent.GenericParameters.Concat(this.TypeParameters.Values));
                        else
                            this.genericParameters = new LockedTypeCollection(this.TypeParameters.Values);
                    }
                    else
                        this.genericParameters = new LockedTypeCollection(this.TypeParameters.Values);
                }
                return this.genericParameters;
            }
        }

        #endregion

        protected override TIdentifier OnGetUniqueIdentifier()
        {
            return (TIdentifier)CliCommon.ObtainTypeIdentifier(this.MetadataEntry, this.Assembly.UniqueIdentifier);
        }

        #region IGenericType Members

        IGenericType IGenericType.MakeGenericClosure(IControlledTypeCollection typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        IGenericType IGenericType.MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        #endregion

        #region IMassTargetHandler Members

        public void BeginExodus()
        {
            throw new NotImplementedException();
        }

        public void EndExodus()
        {
            throw new NotImplementedException();
        }

        #endregion

        public override bool Equals(IType other)
        {
            if (other == null)
                return false;
            if (base.Equals(other))
                return true;
            if (this.IsGenericConstruct && other.IsGenericConstruct)
            {
                if (!(other is IGenericType))
                    return false;
                IGenericType otherGeneric = (IGenericType)other;
                if (this.GenericParameters.Count != otherGeneric.GenericParameters.Count)
                    return false;
                if (otherGeneric.IsGenericDefinition)
                    return false;
                return otherGeneric.GenericParameters.SequenceEqual(this.GenericParameters);

            }
            return false;
        }
    }
}
