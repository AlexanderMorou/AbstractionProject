using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Languages.Expressions
{
    public interface IExpressionService<TProvider, TLanguage> :
        ILanguageService<TLanguage, TProvider>,
        IExpressionService
        where TProvider :
            ILanguageProvider<TLanguage, TProvider>
        where TLanguage :
            ILanguage<TLanguage, TProvider>
    {

    }
    public interface IExpressionService :
        ILanguageService
    {
        IExpression BinaryOperation(INaryOperandExpression left, BinaryOperationKind op, INaryOperandExpression right);
    }
}
