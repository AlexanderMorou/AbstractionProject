using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli.Metadata.Tables;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal abstract class CliGenericParameterDictionary<TGenericParameter, TParent> :
        CliMetadataDrivenDictionary<IGenericParameterUniqueIdentifier, ICliMetadataGenericParameterTableRow, TGenericParameter>,
        IGenericParameterDictionary<TGenericParameter, TParent>
        where TGenericParameter :
            class,
            IGenericParameter<TGenericParameter, TParent>
        where TParent :
            IGenericParamParent<TGenericParameter, TParent>
    {
        private TParent parent;

        internal CliGenericParameterDictionary(IControlledCollection<ICliMetadataGenericParameterTableRow> genericParameters, TParent parent)
            : base()
        {
            this.parent = parent;
            base.Initialize(genericParameters);
        }

        public TParent Parent
        {
            get { return this.parent; }
        }

    }
}
