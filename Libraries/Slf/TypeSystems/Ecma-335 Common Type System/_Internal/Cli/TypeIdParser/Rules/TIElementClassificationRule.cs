using AllenCopeland.Abstraction.Slf.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Rules
{
    internal class TIElementClassificationRule :
        ITIElementClassificationRule
    {
        public TIElementClassificationRule(TypeElementClassification classification)
        {
            this.Classification = classification;
        }

        public TIElementClassificationRule(int rank)
        {
            this.Rank = rank;
            this.Classification = TypeElementClassification.Array;
        }

        public TIElementClassificationRule()
            : this(1)
        {
        }
        #region ITIElementClassificationRule Members

        public TypeElementClassification Classification { get; private set; }

        public int Rank { get; private set; }

        #endregion

        public override string ToString()
        {
            switch (this.Classification)
            {
                case TypeElementClassification.Array:
                    return string.Format("[{0}]", ','.Repeat(this.Rank - 1));
                case TypeElementClassification.Pointer:
                    return "*";
                case TypeElementClassification.Reference:
                    return "&";
            }
            return null;
        }
    }
}
