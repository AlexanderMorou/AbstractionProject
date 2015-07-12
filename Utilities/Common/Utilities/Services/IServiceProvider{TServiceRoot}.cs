using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Utilities.Services
{
    public interface IServiceProvider<TServiceRoot>
        where TServiceRoot :
            IService<TServiceRoot>
    {
        /// <summary>
        /// Returns whether the <see cref="IServiceProvider"/> 
        /// supports the <paramref name="service"/> provided.
        /// </summary>
        /// <param name="service">The <see cref="Guid"/> unique to the
        /// service requested.</param>
        /// <returns>true, if the service is supported; false, otherwise.</returns>
        bool SupportsService(Guid service);
        /// <summary>
        /// Returns whether the <paramref name="service"/>
        /// provided is assignable from the <typeparamref name="TService"/>
        /// provided.
        /// </summary>
        /// <typeparam name="TService">The kind of service to check against the
        /// active service in play.</typeparam>
        /// <param name="service">The <see cref="Guid"/> of the service
        /// to check for.</param>
        /// <returns>true, if the <paramref name="service"/> requested is assignable
        /// from the <typeparamref name="TService"/> provided.</returns>
        bool ServiceIs<TService>(Guid service)
            where TService :
                TServiceRoot;
        /// <summary>
        /// Obtains a <typeparamref name="TService"/> by its
        /// <paramref name="service"/> <see cref="Guid"/>.
        /// </summary>
        /// <typeparam name="TService">The type of <see cref="IService{TRootService}"/>
        /// to retrieve.</typeparam>
        /// <param name="service">The <see cref="Guid"/> unique to the
        /// service requested.</param>
        /// <returns>The <typeparamref name="TService"/> by the <paramref name="service"/>
        /// <see cref="Guid"/> provided.</returns>
        TService GetService<TService>(Guid service)
            where TService :
                TServiceRoot;

        bool TryGetService<TService>(Guid serviceGuid, out TService service)
            where TService :
                TServiceRoot;
    }
}
