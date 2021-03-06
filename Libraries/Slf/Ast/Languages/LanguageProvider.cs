﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Services;

namespace AllenCopeland.Abstraction.Slf.Languages
{
    /// <summary>
    /// Provides a base implementation of a language provider
    /// which contains services for the language relative to assembly
    /// construction, parsing, compiling, code translation, cst translation
    /// and services specific to the language.
    /// </summary>
    /// <typeparam name="TLanguage">The type of the language that contains
    /// meta-data information about the language and access to creating
    /// new providers.</typeparam>
    /// <typeparam name="TProvider">The type of the provider that is supplied by
    /// the <typeparamref name="TLanguage"/>.</typeparam>
    /// <typeparam name="TIdentityManager">
    /// The type of <see cref="IIdentityManager{TTypeIdentity, TAssemblyIdentity}"/>
    /// from which types and assemblies can be resolved.</typeparam>
    /// <typeparam name="TTypeIdentity">The identity relative to the model
    /// in which the <see cref="IType"/> instances are created from.</typeparam>
    /// <typeparam name="TAssemblyIdentity">The  type used in the containing model
    /// to represent assembly instances.</typeparam>
    public abstract class LanguageProvider<TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity> :
        ILanguageProvider<TLanguage, TProvider>,
        ILanguageProvider
        where TLanguage :
            ILanguage<TLanguage, TProvider>
        where TProvider :
            ILanguageProvider<TLanguage, TProvider>
        where TIdentityManager :
            IIdentityManager<TTypeIdentity, TAssemblyIdentity>,
            IIntermediateIdentityManager

    {
        #region Data members

        /// <summary>
        /// Data member for supported services beyond the default services.
        /// </summary>
        private Dictionary<Guid, ILanguageService> supportedServices;
        /// <summary>
        /// Data member representing the default services used to construct classes, delegates, et al.
        /// </summary>
        private Dictionary<Guid, ILanguageService> defaultServices;
        /// <summary>
        /// Data member used to cache services of various types.
        /// </summary>
        private MultikeyedDictionary<Guid, Type, ILanguageService> cachedServices;
        /// <summary>
        /// The identity manager used to resolve assembly and type identities.
        /// </summary>
        private TIdentityManager identityManager;
        #endregion
        protected LanguageProvider(TIdentityManager identityManager)
        {
            var defaultServices = new Dictionary<Guid, ILanguageService>();
            var cachedServices = new MultikeyedDictionary<Guid, Type, ILanguageService>();
            cachedServices[LanguageGuids.Services.ClassServices.ClassCreatorService, typeof(IIntermediateTypeCtorLanguageService<IIntermediateClassType>)] = defaultServices[LanguageGuids.Services.ClassServices.ClassCreatorService] = DefaultLanguageServices.GetBoundIntermediateClassCreatorService(this.Language, this);
            cachedServices[LanguageGuids.Services.IntermediateDelegateCreatorService, typeof(IIntermediateTypeCtorLanguageService<IIntermediateDelegateType>)] = defaultServices[LanguageGuids.Services.IntermediateDelegateCreatorService] = DefaultLanguageServices.GetBoundIntermediateDelegateCreatorService(this.Language, this);
            cachedServices[LanguageGuids.Services.IntermediateEnumCreatorService, typeof(IIntermediateTypeCtorLanguageService<IIntermediateEnumType>)] = defaultServices[LanguageGuids.Services.IntermediateEnumCreatorService] = DefaultLanguageServices.GetBoundIntermediateEnumCreatorService(this.Language, this);
            cachedServices[LanguageGuids.Services.InterfaceServices.InterfaceCreatorService, typeof(IIntermediateTypeCtorLanguageService<IIntermediateInterfaceType>)] = defaultServices[LanguageGuids.Services.InterfaceServices.InterfaceCreatorService] = DefaultLanguageServices.GetBoundIntermediateInterfaceCreatorService(this.Language, this);
            cachedServices[LanguageGuids.Services.StructServices.StructCreatorService, typeof(IIntermediateTypeCtorLanguageService<IIntermediateStructType>)] = defaultServices[LanguageGuids.Services.StructServices.StructCreatorService] = DefaultLanguageServices.GetBoundIntermediateStructCreatorService(this.Language, this);
            cachedServices[LanguageGuids.Services.IntermediateAssemblyCreatorService, typeof(IIntermediateAssemblyCtorLanguageService)] = defaultServices[LanguageGuids.Services.IntermediateAssemblyCreatorService] = DefaultLanguageServices.GetBoundIntermediateAssemblyCreatorService<TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity>(this.Language, (TProvider)(object)this);
            this.defaultServices = defaultServices;
            this.cachedServices = cachedServices;
            supportedServices = new Dictionary<Guid, ILanguageService>();
            this.identityManager = identityManager;
        }

        /// <summary>
        /// Provides the implementation necessary to provide <see cref="ILanguageProvider.CreateAssembly(string)"/>.
        /// </summary>
        /// <param name="name">The <see cref="String"/> that represents part of the uniqueness
        /// aspect of the <see cref="IIntermediateAssembly"/> to create.</param>
        /// <returns>A new <see cref="IIntermediateAssembly"/> with the 
        /// <paramref name="name"/> provided.</returns>
        protected virtual IIntermediateAssembly OnCreateAssembly(string name)
        {
            IIntermediateAssemblyCtorLanguageService service;
            if (this.TryGetServiceImpl(LanguageGuids.Services.IntermediateAssemblyCreatorService, out service))
                return service.New(name);
            throw new NotSupportedException(ThrowHelper.GetArgumentName(ArgumentWithException.name));
        }

        #region Abstract Methods

        /// <summary>
        /// Obtains the language for the <see cref="LanguageProvider{TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/>.
        /// </summary>
        /// <returns>The <typeparamref name="TLanguage"/> instance relative to the current
        /// <see cref="LanguageProvider{TLanguage, TProvider, TIdentityManager, TTypeIdentity, TAssemblyIdentity}"/> instance.</returns>
        protected abstract TLanguage OnGetLanguage();

        #endregion

        #region Service handling

        /// <summary>
        /// Registers a <paramref name="service"/> by the 
        /// <paramref name="serviceGuid"/> provided.
        /// </summary>
        /// <typeparam name="TService">The type of <see cref="ILanguageService{TLanguage, TProvider}"/>
        /// to register.</typeparam>
        /// <param name="serviceGuid">The <see cref="Guid"/> of the service to register
        /// to aid in looking it up.</param>
        /// <param name="service">The <typeparamref name="TService"/> to actually 
        /// register.</param>
        protected void RegisterService<TService>(Guid serviceGuid, TService service)
            where TService :
                class,                
                ILanguageService
        {
            if (service == null)
                throw new ArgumentNullException(ThrowHelper.GetArgumentName(ArgumentWithException.service));
            ILanguageService previousService = null;
            if (!this.supportedServices.TryGetValue(serviceGuid, out previousService))
                this.defaultServices.TryGetValue(serviceGuid, out previousService);

            this.supportedServices[serviceGuid] = service;
            lock (cachedServices)
            {
                var deadCacheEntries = (from entry in this.cachedServices
                                        where entry.Keys.Key1 == serviceGuid &&
                                              !entry.Keys.Key2.IsAssignableFrom(service.GetType())
                                        select entry.Keys).ToArray();
                var replaceableCacheEntries = (from entry in this.cachedServices
                                               where entry.Keys.Key1 == serviceGuid &&
                                                     entry.Keys.Key2 != typeof(TService) &&
                                                     entry.Keys.Key2.IsAssignableFrom(service.GetType())
                                               select entry.Keys).ToArray();
                foreach (var deadEntry in deadCacheEntries)
                    this.cachedServices.Remove(deadEntry.Key1, deadEntry.Key2);
                foreach (var replaceEntry in replaceableCacheEntries)
                    this.cachedServices[replaceEntry.Key1, replaceEntry.Key2] = service;
                this.cachedServices[serviceGuid, typeof(TService)] = service;
            }
        }

        private bool SupportsUserService(Guid service)
        {
            ILanguageService serviceTemp;
            if (supportedServices.TryGetValue(service, out serviceTemp))
                return serviceTemp != null;
            return false;
        }

        private bool SupportsDefaultService(Guid service)
        {
            ILanguageService serviceTemp;
            if (defaultServices.TryGetValue(service, out serviceTemp))
                return serviceTemp != null;
            return false;
        }

        private bool UserServiceIs<TService>(Guid service)
            where TService :
                ILanguageService
        {
            return supportedServices[service] is TService;
        }

        private bool DefaultServiceIs<TService>(Guid service) 
            where TService :
                ILanguageService
        {
            return defaultServices[service] is TService;
        }

        /// <summary>
        /// Provides the root implementation of the 
        /// <see cref="GetService(Guid)"/> method since the constraints
        /// on <see cref="IServiceProvider{ILanguageService}.GetService(Guid)"/> and 
        /// <see cref="ILanguageProvider{TLanguage, TProvider}"/> are different.
        /// </summary>
        /// <typeparam name="TService">The type of <see cref="ILanguageService{TLanguage, TProvider}"/>
        /// to retrieve.</typeparam>
        /// <param name="service">The <see cref="Guid"/> unique to the
        /// service requested.</param>
        /// <returns>The <typeparamref name="TService"/> by the <paramref name="service"/>
        /// <see cref="Guid"/> provided.</returns>
        protected virtual TService GetServiceImpl<TService>(Guid service)
            where TService :
                ILanguageService
        {
            ILanguageService result;
            lock (cachedServices)
            {
                if (this.cachedServices.TryGetValue(service, typeof(TService), out result))
                    return (TService)result;
                else
                    if (this.SupportsUserService(service))
                        if (!UserServiceIs<TService>(service))
                            throw new InvalidOperationException(string.Format("Service of type '{0}' does not support expected type '{1}'.", supportedServices[service].GetType(), typeof(TService)));
                        else
                            result = this.supportedServices[service];
                    else if (this.SupportsDefaultService(service))
                        if (!DefaultServiceIs<TService>(service))
                            throw new InvalidOperationException(string.Format("Service of type '{0}' does not support expected type '{1}'.", defaultServices[service].GetType(), typeof(TService)));
                        else
                            result = this.defaultServices[service];
                    else
                        throw new InvalidOperationException("Service not supported.");
                this.cachedServices.TryAdd(service, typeof(TService), result);
            }
            return (TService)result;
        }

        /// <summary>
        /// Internal implementation of <see cref="TryGetService{TService}"/> with the <paramref name="serviceGuid"/> and
        /// <paramref name="service"/> provided.
        /// </summary>
        /// <typeparam name="TService">The type of the service in question.</typeparam>
        /// <param name="serviceGuid">The <see cref="Guid"/> of the service to attempt to retrieve.</param>
        /// <param name="service">The <typeparamref name="TService"/> to retrieve.</param>
        /// <returns>true, if the service is implemented, and of the proper type (<typeparamref name="TService"/>); false, otherwise.</returns>
        protected virtual bool TryGetServiceImpl<TService>(Guid serviceGuid, out TService service)
            where TService :
                ILanguageService
        {
            ILanguageService result;
            lock (cachedServices)
            {
                if (this.cachedServices.TryGetValue(serviceGuid, typeof(TService), out result))
                {
                    service = (TService)result;
                    return true;
                }
                else if (this.SupportsUserService(serviceGuid))
                    if (!UserServiceIs<TService>(serviceGuid))
                    {
                        service = default(TService);
                        return false;
                    }
                    else
                    {
                        service = (TService)this.supportedServices[serviceGuid];
                        this.cachedServices.TryAdd(serviceGuid, typeof(TService), result);
                        return true;
                    }
                else if (this.SupportsDefaultService(serviceGuid))
                    if (!DefaultServiceIs<TService>(serviceGuid))
                    {
                        service = default(TService);
                        return false;
                    }
                    else
                    {
                        service = (TService)this.defaultServices[serviceGuid];
                        this.cachedServices.TryAdd(serviceGuid, typeof(TService), result);
                        return true;
                    }
                else
                {
                    service = default(TService);
                    return false;
                }
            }
            
        }


        /// <summary>
        /// Provides a basic implementation to check whether the <paramref name="service"/>
        /// is assignable from the <typeparamref name="TService"/> provided.
        /// </summary>
        /// <typeparam name="TService">The kind of service to check against the
        /// active service in play.</typeparam>
        /// <param name="service">The <see cref="Guid"/> of the service
        /// to check for.</param>
        /// <returns>true, if the <paramref name="service"/> requested is assignable
        /// from the <typeparamref name="TService"/> provided.</returns>
        /// <exception cref="InvalidOperationException">The <paramref name="service"/>
        /// provided is not supported.</exception>
        protected virtual bool ServiceIsImpl<TService>(Guid service)
            where TService :
                ILanguageService
        {
            if (this.SupportsUserService(service))
                return UserServiceIs<TService>(service);
            else if (this.SupportsDefaultService(service))
                return DefaultServiceIs<TService>(service);
            throw new InvalidOperationException("Service not supported.");
        }
        #endregion

        #region ILanguageProvider Members

        ILanguage ILanguageProvider.Language
        {
            get { return this.Language; }
        }

        public IAssembly CreateAssembly(string name)
        {
            return this.OnCreateAssembly(name);
        }

        public bool SupportsService(Guid service)
        {
            return SupportsUserService(service) || SupportsDefaultService(service);
        }

        bool IServiceProvider<ILanguageService>.ServiceIs<TService>(Guid service)
        {
            return this.ServiceIsImpl<TService>(service);
        }

        TService IServiceProvider<ILanguageService>.GetService<TService>(Guid service)
        {
            return this.GetServiceImpl<TService>(service);
        }

        #endregion

        #region ILanguageProvider<TLanguage,TProvider> Members

        public TLanguage Language
        {
            get { return this.OnGetLanguage(); }
        }

        /// <summary>
        /// Obtains a <typeparamref name="TService"/> by its
        /// <paramref name="service"/> <see cref="Guid"/>.
        /// </summary>
        /// <typeparam name="TService">The type of <see cref="ILanguageService{TLanguage, TProvider}"/>
        /// to retrieve.</typeparam>
        /// <param name="service">The <see cref="Guid"/> unique to the
        /// service requested.</param>
        /// <returns>The <typeparamref name="TService"/> by the <paramref name="service"/>
        /// <see cref="Guid"/> provided.</returns>
        public TService GetService<TService>(Guid service)
            where TService :
                ILanguageService<TLanguage, TProvider>
        {
            return GetServiceImpl<TService>(service);
        }

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
        public bool ServiceIs<TService>(Guid service)
            where TService :
                ILanguageService<TLanguage, TProvider>
        {
            return this.ServiceIsImpl<TService>(service);
        }

        public bool TryGetService<TService>(Guid serviceGuid, out TService service)
            where TService :
                ILanguageService<TLanguage, TProvider>
        {
            return TryGetServiceImpl<TService>(serviceGuid, out service);
        }

        #endregion

        bool IServiceProvider<ILanguageService>.TryGetService<TService>(Guid serviceGuid, out TService service)
        {
            return TryGetServiceImpl<TService>(serviceGuid, out service);
        }

        public IIdentityManager IdentityManager { get { return this.identityManager; } }
    }
}
