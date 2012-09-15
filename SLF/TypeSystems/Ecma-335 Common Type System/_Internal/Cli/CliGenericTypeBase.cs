using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Properties;

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

        protected CliGenericTypeBase(CliAssembly assembly, ICliMetadataTypeDefinitionTableRow metadataEntry)
            : base(assembly, metadataEntry)
        {
        }

        #region IGenericType<TIdentifier,TType> Members

        public TType MakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            throw new NotImplementedException();
        }

        public TType MakeGenericClosure(params IType[] typeParameters)
        {
            throw new NotImplementedException();
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
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IGenericParamParent Members

        public bool IsGenericDefinition
        {
            get { return true; }
        }

        IGenericParameterDictionary IGenericParamParent.TypeParameters
        {
            get { throw new NotImplementedException(); }
        }

        IGenericParamParent IGenericParamParent.MakeGenericClosure(ITypeCollectionBase typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        IGenericParamParent IGenericParamParent.MakeGenericClosure(params IType[] typeParameters)
        {
            return this.MakeGenericClosure(typeParameters);
        }

        public bool ContainsGenericParameters
        {
            get { throw new NotImplementedException(); }
        }

        public ILockedTypeCollection GenericParameters
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        protected override TIdentifier OnGetUniqueIdentifier()
        {
            return (TIdentifier)CliCommon.ObtainTypeIdentifier(this.MetadataEntry, this.Assembly.UniqueIdentifier);
        }

        #region IGenericType Members

        IGenericType IGenericType.MakeGenericClosure(ITypeCollectionBase typeParameters)
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
    }
}
