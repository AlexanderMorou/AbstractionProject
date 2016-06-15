using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Services
{
    /// <summary>
    /// Defines properties and methods for working with a general service.
    /// </summary>
    public interface IService<TServiceRoot>
        where TServiceRoot :
            IService<TServiceRoot>
    {
        /// <summary>
        /// Returns the <see cref="IServiceProvider{T}"/> on which the 
        /// <see cref="IService{T}"/> provides the service for.
        /// </summary>
        IServiceProvider<TServiceRoot> Provider { get; }
        /// <summary>
        /// Returns the <see cref="Guid"/> unique to the type of the
        /// <see cref="IService"/> in play.
        /// </summary>
        Guid ServiceGuid { get; }
    }
}
