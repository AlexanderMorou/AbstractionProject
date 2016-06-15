using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    public interface ILanguageAsynchronousQueryService :
        ILanguageService
    {
        /// <summary>
        /// Returns whether the <see cref="IType"/> is indictive of a 
        /// method which is asynchronous.
        /// </summary>
        /// <param name="returnType">The <see cref="IType"/>
        /// to enquire about.</param>
        /// <returns></returns>
        bool IsReturnAsynchronous(IType returnType);
        bool IsAsynchronousPattern(IMethodSignatureMember possibleAsyncMethod);
    }
}
