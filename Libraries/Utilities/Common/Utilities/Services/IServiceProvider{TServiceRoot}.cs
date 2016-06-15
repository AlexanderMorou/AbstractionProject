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
        /// Obtains a <typeparamref name="TService"/> by its <paramref name="service"/> <see cref="Guid"/>.
        /// </summary>
        /// <typeparam name="TService">The type of <see cref="IService{TRootService}"/> to retrieve.</typeparam>
        /// <param name="service">The <see cref="Guid"/> unique to the service requested.</param>
        /// <returns>The <typeparamref name="TService"/> by the <paramref name="service"/> <see cref="Guid"/> provided.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the type of <typeparamref name="TService"/> is not compatible with the <paramref name="service"/> requested.</exception>
        /// <exception cref="NotSupportedException">Thrown when the <paramref name="service"/> is not supported.</exception>
        TService GetService<TService>(Guid service)
            where TService :
                TServiceRoot;

        /// <summary>
        /// Makes an attempt to retrieve a <typeparamref name="TService"/> instance based on its <paramref name="serviceGuid"/>.
        /// </summary>
        /// <typeparam name="TService">The type of <typeparamref name="TServiceRoot"/> to try and retrieve.</typeparam>
        /// <param name="serviceGuid">The <see cref="Guid"/> which denotes the unique identifier of the service to retrieve.</param>
        /// <param name="service">The <typeparamref name="TService"/> instance which accepts the service instance should it be supported.</param>
        /// <returns>true, if the <see cref="IServiceProvider{TServiceRoot}"/> supports the <paramref name="service"/>; false, otherwise.</returns>
        bool TryGetService<TService>(Guid serviceGuid, out TService service)
            where TService :
                TServiceRoot;
    }
}
