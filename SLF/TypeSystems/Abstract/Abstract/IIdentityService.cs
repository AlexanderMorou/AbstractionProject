using AllenCopeland.Abstraction.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public interface IIdentityService : 
        IService<IIdentityService>
    {
        /// <summary>
        /// Returns the <see cref="IIdentityManager"/> on which the 
        /// <see cref="IIdentityService"/> provides the service for.
        /// </summary>
        new IIdentityManager Provider { get; }
    }
}
