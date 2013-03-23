using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal class CliFlagsEnumeration :
        ControlledDictionary<CliFlagsEnumerationEntry, IFieldMember>
    {
        public IEnumerable<IFieldMember> GetFields(CliFlagsEnumerationEntry value)
        {
            foreach (var efPair in this)
            {
                var element = efPair.Key;
                var intersection = value & element;
                if (intersection.Equals(element))
                    yield return efPair.Value;
            }
            yield break;
        }
    }
}
