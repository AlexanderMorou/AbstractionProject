using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Translation
{
    public interface IIntermediateCodeNameProvider :
        IIntermediateDeclarationVisitor<string, IntermediateNameRequestDetails>,
        IIntermediateTypeVisitor<string, IntermediateNameRequestDetails>,
        IIntermediateMemberVisitor<string, IntermediateNameRequestDetails>
    {
    }
}
