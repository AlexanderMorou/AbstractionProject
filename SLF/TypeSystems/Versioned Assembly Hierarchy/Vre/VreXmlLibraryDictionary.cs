using AllenCopeland.Abstraction.Slf._Internal.Vre;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AllenCopeland.Abstraction.Slf.Vre
{
    public class VreXmlLibraryDictionary<TEnvironment, TVersion, TIdentityManager> :
        ControlledDictionary<string, IVreLibrary<TEnvironment, TVersion, TIdentityManager>>,
        IVreLibraryDictionary<TEnvironment, TVersion, TIdentityManager>
        where TEnvironment :
            VreXmlEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        private TEnvironment _environment;
        private bool initialized;

        public VreXmlLibraryDictionary(TEnvironment environment)
        {
            this._environment = environment;
        }
        public TEnvironment Environment
        {
            get { return this._environment; }
        }

        #region Lazy Initialization Overrides

        public override bool Contains(KeyValuePair<string, IVreLibrary<TEnvironment, TVersion, TIdentityManager>> item)
        {
            this.CheckInitialization();
            return base.Contains(item);
        }

        public override bool ContainsKey(string key)
        {
            this.CheckInitialization();
            return base.ContainsKey(key);
        }

        public override void CopyTo(KeyValuePair<string, IVreLibrary<TEnvironment, TVersion, TIdentityManager>>[] array, int arrayIndex)
        {
            this.CheckInitialization();
            base.CopyTo(array, arrayIndex);
        }

        protected override void CopyToArray(Array array, int arrayIndex = 0)
        {
            this.CheckInitialization();
            base.CopyToArray(array, arrayIndex);
        }

        public override int Count
        {
            get
            {
                this.CheckInitialization();
                return base.Count;
            }
        }

        protected override KeysCollection InitializeKeysCollection()
        {
            this.CheckInitialization();
            return base.InitializeKeysCollection();
        }

        protected override ValuesCollection InitializeValuesCollection()
        {
            this.CheckInitialization();
            return base.InitializeValuesCollection();
        }

        protected override IEnumerator<KeyValuePair<string, IVreLibrary<TEnvironment, TVersion, TIdentityManager>>> OnGetEnumerator()
        {
            this.CheckInitialization();
            return base.OnGetEnumerator();
        }
        protected override KeyValuePair<string, IVreLibrary<TEnvironment, TVersion, TIdentityManager>> OnGetThis(int index)
        {
            this.CheckInitialization();
            return base.OnGetThis(index);
        }

        protected override IVreLibrary<TEnvironment, TVersion, TIdentityManager> OnGetThis(string key)
        {
            this.CheckInitialization();
            return base.OnGetThis(key);
        }

        public override KeyValuePair<string, IVreLibrary<TEnvironment, TVersion, TIdentityManager>>[] ToArray()
        {
            this.CheckInitialization();
            return base.ToArray();
        }

        public override bool TryGetValue(string key, out IVreLibrary<TEnvironment, TVersion, TIdentityManager> value)
        {
            this.CheckInitialization();
            return base.TryGetValue(key, out value);
        }

        #endregion

        private void CheckInitialization()
        {
            if (!this.initialized)
            {
                initialized = true;
                var libraries = XmlExtensions.ParseDictionary<Dictionary<string, IVreLibrary<TEnvironment, TVersion, TIdentityManager>>, string, IVreLibrary<TEnvironment, TVersion, TIdentityManager>, XmlElement>(this.Environment.XmlNode.DocumentElement, this.Environment.XmlNamespaceManager, "Libraries", "Library", GetLibraryFromDetail, keyDetail => keyDetail.Extra.Item2.Name);
                base._AddRange(libraries);
            }
        }

        private IVreLibrary<TEnvironment, TVersion, TIdentityManager> GetLibraryFromDetail(PluralSingleExtraDetail<XmlElement> valueDetail)
        {
            return new VreXmlLibrary<TEnvironment, TVersion, TIdentityManager>(valueDetail.Extra, this.Environment);
        }

    }
}
