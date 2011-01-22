using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public interface IBoundLocalReferenceExpression :
        ILocalReferenceExpression,
        IBoundMemberReference
    {

    }
}
