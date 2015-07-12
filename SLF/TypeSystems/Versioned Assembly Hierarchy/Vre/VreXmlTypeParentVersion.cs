using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Vre
{
    public abstract class VreXmlTypeParentVersion<TEnvironment, TVersion, TIdentityManager> :
        IVreTypeParentVersion<TEnvironment, TVersion, TIdentityManager>
        where TEnvironment :
            VreXmlEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        /// <summary>
        /// Data member for <see cref="Version"/>.
        /// </summary>
        private TVersion _version;
        /// <summary>
        /// Data member for <see cref="Types"/>.
        /// </summary>
        private ControlledDictionary<string, IVreTypeVersion<TEnvironment, TVersion, TIdentityManager>> _types;

        /// <summary>
        /// Creates a new <see cref="VreXmlTypeParentVersion{TEnvironment, TVersion, TIdentityManager}"/> with
        /// the <paramref name="version"/> provided.
        /// </summary>
        /// <param name="version">The <typeparamref name="TVersion"/> from which the <see cref="VreXmlTypeParentVersion{TEnvironment, TVersion, TIdentityManager}"/>
        /// is derived.</param>
        protected VreXmlTypeParentVersion(TVersion version)
        {
            this._version = version;
        }

        /// <summary>
        /// Returns the <see cref="IControlledDictionary{TKey, TValue}"/> which contains the 
        /// types of the <see cref="VreXmlTypeParentVersion{TEnvironment, TVersion, TIdentityManager}"/>.
        /// </summary>
        public IControlledDictionary<string, IVreTypeVersion<TEnvironment, TVersion, TIdentityManager>> Types
        {
            get
            {
                return this._types ?? (this._types = this.InitializeTypes());
            }
        }

        private ControlledDictionary<string, IVreTypeVersion<TEnvironment, TVersion, TIdentityManager>> InitializeTypes()
        {
            if (this._version == null)
                return null;
            return new ControlledDictionary<string, IVreTypeVersion<TEnvironment, TVersion, TIdentityManager>>(
                (from type in this.Version.AllTypes
                 where type.Versions.ContainsKey(this.Version)
                 let vType = type.Versions[this.Version]
                 where vType != null
                 select new { type.Name, vType }).ToDictionary(k => k.Name, v => v.vType));
        }

        /// <summary>
        /// Returns the <typeparamref name="TVersion"/> from which the
        /// <see cref="VreXmlTypeVersion{TEnvironment, TVersion, TIdentityManager}"/> was derived.
        /// </summary>
        public TVersion Version
        {
            get { return this._version; }
        }

        protected abstract bool FilterType(IVreTypeVersion<TEnvironment, TVersion, TIdentityManager> type);


        public virtual void Dispose()
        {
            if (this._types != null)
            {
                this._types._Clear();
                this._types = null;
            }
            this._version = null;
        }


    }
}
