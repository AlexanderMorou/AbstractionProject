using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public interface IUsingExpressionBlockStatement :
        IBlockStatement
    {
        IExpression ResourceAcquisition { get; set; }
    }
    public interface IUsingBlockStatement :
        IBlockStatement
    {
        ILocalDeclarationsStatement ResourceAcquisition { get; set; }
    }
}
