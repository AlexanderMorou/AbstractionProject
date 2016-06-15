using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Arrays;
using AllenCopeland.Abstraction.Utilities;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AllenCopeland.Abstraction.Slf.Vre
{
    /// <summary>
    /// Denotes a runtime environment which is versioned such that the libraries which make up the
    /// environment's runtime library can be observed in a versionable manner.
    /// </summary>
    public abstract class VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager> :
        VreXmlElement,
        IVreEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>,
        IComparable<TVersion>,
        IEquatable<TVersion>
        where TEnvironment :
            VreXmlEnvironment<TEnvironment, TVersion, TIdentityManager>
        where TVersion :
            VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
        where TIdentityManager :
            class,
            IIdentityManager
    {
        /// <summary>
        /// Data member for <see cref="SubversionSource"/>.
        /// </summary>
        private VreEnvironmentSubversionSource subversionSource;

        /// <summary>
        /// Data member for <see cref="PreviousVersion"/>.
        /// </summary>
        private TVersion previousVersion;
        
        /// <summary>
        /// Data member for <see cref="NextVersion"/>.
        /// </summary>
        private TVersion nextVersion;

        /// <summary>
        /// Data member for <see cref="Environment."/>
        /// </summary>
        private TEnvironment environment;

        private IControlledCollection<string> versionStrings;

        private TIdentityManager identityManager;
        private IEnumerable<string> hintPaths;
        private IEnumerable<IVreType<TEnvironment, TVersion, TIdentityManager>> allDeprecatedTypes;
        private IEnumerable<IVreType<TEnvironment, TVersion, TIdentityManager>> deprecatedTypes;
        private IEnumerable<IVreType<TEnvironment, TVersion, TIdentityManager>> allTypes;

        /// <summary>
        /// Creates a new <see cref="VreXmlEnvironmentVersion"/> with
        /// the <paramref name="xmlNode"/> provided.
        /// </summary>
        /// <param name="xmlNode">The <see cref="XmlElement"/> from
        /// which the <see cref="VreXmlEnvironmentVersion"/> is derived.
        /// </param>
        public VreXmlEnvironmentVersion(TEnvironment environment, XmlElement xmlNode, TIdentityManager identityManager) : 
            base(xmlNode) 
        { 
            this.subversionSource = VreEnvironmentSubversionSource.InitialVersion;
            this.environment = environment; 
            this.identityManager = identityManager; 
        }

        public VreXmlEnvironmentVersion(TEnvironment environment, TVersion previousVersion, XmlElement derivedVersion, TIdentityManager identityManager) :
            base(derivedVersion)
        {
            this.identityManager = identityManager;
            this.environment = environment;
            this.previousVersion = previousVersion;
            if (previousVersion != null)
                previousVersion.nextVersion = (TVersion)this;
            switch (derivedVersion.Name)
            {
                case "vre:BreakingVersion":
                    this.subversionSource = VreEnvironmentSubversionSource.BreakingVersion;
                    break;
                case "vre:MinorVersion":
                    this.subversionSource = VreEnvironmentSubversionSource.VersionExtension;
                    break;
                case "vre:ServicePack":
                    this.subversionSource = VreEnvironmentSubversionSource.ServicePackExtension;
                    break;
            }
        }

        /// <summary>
        /// Returns the <see cref="VreXmlEnvironment"/> the <see cref="TVersion"/> is associated
        /// to.
        /// </summary>
        public TEnvironment Environment { get { return this.environment; } }
        /// <summary>
        /// Returns the <see cref="TVersion"/> which denotes the previous version within
        /// the <see cref="VreXmlEnvironment"/>.
        /// </summary>
        public TVersion PreviousVersion { get { return this.previousVersion; } }

        /// <summary>
        /// Returns the <see cref="VreEnvironmentSubversionSource"/> which denotes the kind of subversion
        /// represented by the current <see cref="TVersion"/>.
        /// </summary>
        public VreEnvironmentSubversionSource SubversionSource { get { return this.subversionSource; } }

        /// <summary>
        /// Returns the <see cref="TVersion"/> that is the current Major version of the
        /// <see cref="VreXmlEnvironment"/>.
        /// </summary>
        /// <remarks>Can be the current instance.</remarks>
        public TVersion MajorVersion
        {
            get
            {
                return SelectVersion(MajorVersionPredicate);
            }
        }

        /// <summary>
        /// Returns the <see cref="TVersion"/> which is the first or initial version
        /// of the <see cref="VreXmlEnvironment"/>.
        /// </summary>
        public TVersion InitialVersion
        {
            get
            {
                return SelectVersion(current => current.subversionSource == VreEnvironmentSubversionSource.InitialVersion);
            }
        }

        /// <summary>
        /// Returns the <see cref="TVersion"/> which is the Previous Major version of
        /// the <see cref="VreXmlEnvironment"/>.
        /// </summary>
        public TVersion PreviousMajorVersion
        {
            get
            {
                var majorVersion = this.MajorVersion;
                if (majorVersion.PreviousVersion == null)
                    return null;
                return majorVersion.PreviousVersion.MajorVersion;
            }
        }

        /// <summary>
        /// Returns the <see cref="TVersion"/> which is the next version of the 
        /// <see cref="VreXmlEnvironment"/>
        /// </summary>
        public TVersion NextVersion
        {
            get
            {
                return this.nextVersion;
            }
        }

        /// <summary>
        /// Returns the <see cref="TVersion"/> which represents the next major version
        /// of the <see cref="VreXmlEnvironment"/>.
        /// </summary>
        public TVersion NextMajorVersion
        {
            get
            {
                if (this.nextVersion == null)
                    return null;
                return this.NextVersion.SelectVersion(MajorVersionPredicate, false);
            }
        }

        /// <summary>
        /// Returns whether the <paramref name="current"/> <see cref="TVersion"/>
        /// is a major version.
        /// </summary>
        /// <param name="current">
        /// The <see cref="TVersion"/> to check for major version status.</param>
        /// <returns></returns>
        private static bool MajorVersionPredicate(TVersion current)
        {
            return current.subversionSource == VreEnvironmentSubversionSource.BreakingVersion ||
                   current.subversionSource == VreEnvironmentSubversionSource.InitialVersion;
        }

        /// <summary>
        /// Iterates through the nodes of the <see cref="TVersion"/> stopping when 
        /// <paramref name="predicate"/> on a given <see cref="TVersion"/> yields 
        /// true.
        /// </summary>
        /// <param name="predicate">
        /// The <see cref="Predicate{T}"/> which controls yield behavior.</param>
        /// <param name="moveBackwards">Whether we're searching backwards or forwards.</param>
        /// <returns>
        /// The <see cref="TVersion"/> stopping when <paramref name="predicate"/> on 
        /// a given <see cref="TVersion"/> yields true; or null, if none of the relevant
        /// instances yield true through <paramref name="predicate"/>.
        /// </returns>
        /// <remarks><paramref name="moveBackwards"/>: true searches the <see cref="previousVersion"/>
        /// fields until <paramref name="predicate"/> is true; otherwise, it searches using the <see cref="nextVersion"/>
        /// fields until <paramref name="predicate"/> is true.  If all relevant <see cref="TVersion"/>
        /// instances are observed and <paramref name="predicate"/> never yields true, the result is null.</remarks>
        private TVersion SelectVersion(Predicate<TVersion> predicate, bool moveBackwards = true)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            var current = (TVersion)this;
            while (current != null)
            {
                if (predicate(current))
                    return current;
                if (moveBackwards)
                    current = current.PreviousVersion;
                else
                    current = current.NextVersion;
            }
            return null;
        }

        public static string GetXmlVersionString(XmlElement derivedVersion)
        {
            var subversionSource = VreEnvironmentSubversionSource.InitialVersion;
            switch (derivedVersion.Name)
            {
                case "vre:BreakingVersion":
                    subversionSource = VreEnvironmentSubversionSource.BreakingVersion;
                    break;
                case "vre:MinorVersion":
                    subversionSource = VreEnvironmentSubversionSource.VersionExtension;
                    break;
                case "vre:ServicePack":
                    subversionSource = VreEnvironmentSubversionSource.ServicePackExtension;
                    break;
            }
            switch (subversionSource)
            {
                case VreEnvironmentSubversionSource.InitialVersion:
                case VreEnvironmentSubversionSource.BreakingVersion:
                case VreEnvironmentSubversionSource.VersionExtension:
                    return derivedVersion.GetAttribute("Id");
                case VreEnvironmentSubversionSource.ServicePackExtension:
                    return string.Format("{0} {1}", derivedVersion.GetAttribute("BuiltUpon"), derivedVersion.GetAttribute("ServicePack"));
                default:
                    return null;
            }

        }

        /// <summary>
        /// Returns the <see cref="String"/> value denoting the
        /// qualifier of the <see cref="TVersion"/>.
        /// </summary>
        public string VersionQualifier 
        { 
            get 
            {
                switch (this.subversionSource)
                {
                    case VreEnvironmentSubversionSource.InitialVersion:
                    case VreEnvironmentSubversionSource.BreakingVersion:
                    case VreEnvironmentSubversionSource.VersionExtension:
                        return this.XmlNode.GetAttribute("Id"); 
                    case VreEnvironmentSubversionSource.ServicePackExtension:
                        return string.Format("{0} {1}", this.XmlNode.GetAttribute("BuiltUpon"),this.XmlNode.GetAttribute("ServicePack")); 
                    default:
                        return null;
                }
            }
        }

        public string VersionText
        {
            get
            {
                switch (this.subversionSource)
                {
                    case VreEnvironmentSubversionSource.InitialVersion:
                    case VreEnvironmentSubversionSource.BreakingVersion:
                    case VreEnvironmentSubversionSource.VersionExtension:
                        return this.XmlNode.GetAttribute("Id");
                    case VreEnvironmentSubversionSource.ServicePackExtension:
                        return this.XmlNode.GetAttribute("BuiltUpon");
                    default:
                        return null;
                }
            }
        }

        public bool IsServicePack { get { return this.XmlNode.Name == "vre:ServicePack"; } }

        public int? ServicePackLevel
        {
            get
            {
                string spLevel;
                return this.IsServicePack ? string.IsNullOrEmpty(spLevel = this.XmlNode.GetAttribute("ServicePack")) ? null : (int?)int.Parse(spLevel.Substring(2)) : null;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.Environment.Name, this.VersionQualifier);
        }

        public IEnumerable<string> HintPaths
        {
            get
            {
                return this.hintPaths ?? (this.hintPaths = GetHintPaths(this.XmlNode, this.Environment.XmlNamespaceManager).ToArray());
            }
        }

        public static IEnumerable<string> GetHintPaths(XmlElement xmlElement, XmlNamespaceManager xmlNamespaceManager)
        {
            var hintPathsRoot = xmlElement.SelectSingleNode("./vre:HintPaths", xmlNamespaceManager);
            if (hintPathsRoot == null)
                yield break;
            foreach (XmlElement element in hintPathsRoot.SelectNodes("./vre:HintPath", xmlNamespaceManager))
            {
                var value = element.GetAttribute("Path");
                if (!string.IsNullOrEmpty(value) && Directory.Exists(value))
                    yield return value;
            }
        }

        internal void Delete()
        {
            if (this.previousVersion != null)
                this.previousVersion.nextVersion = this.nextVersion;
            if (this.nextVersion != null)
                this.nextVersion.previousVersion = this.previousVersion;
        }

        public IEnumerable<string> VersionStrings
        {
            get
            {
                return from s in (this.versionStrings ?? (this.versionStrings = new ControlledCollection<string>(this.InitializeVersionStrings().OrderBy(s => s).ToArray())))
                       select s;
            }
        }

        private IEnumerable<string> InitializeVersionStrings()
        {
            var majorVersion = this.MajorVersion;
            if (majorVersion == null)
                yield break;
            string baseVersionString = majorVersion.XmlNode.GetAttribute("VersionString");
            string alternateVersionStrings = majorVersion.XmlNode.GetAttribute("AlternateVersionStrings");
            if (!string.IsNullOrEmpty(alternateVersionStrings))
            {
                string[] alternates = alternateVersionStrings.Replace(System.Environment.NewLine, string.Empty).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var alternate in alternates)
                    yield return alternate;
            }
            if (!string.IsNullOrEmpty(baseVersionString))
                yield return baseVersionString;
        }

        /// <summary>
        /// Returns the <see cref="TIdentityManager"/> responsible for assembly and type identity resolution.
        /// </summary>
        public TIdentityManager IdentityManager { get { return this.identityManager; } }

        public override void Dispose()
        {
            if (this.identityManager != null)
            {
                this.identityManager.Dispose();
                this.identityManager = null;
            }
            base.Dispose();
        }

        public int CompareTo(TVersion other)
        {
            if (other == null)
                return 1;
            if (object.ReferenceEquals(this, other))
                return 0;
            if (other.environment != this.environment)
                if (this.environment != null && other.environment == null)
                    return 1;
                else if (this.environment == null && other.environment == null)
                    return -1;
                else if (this.environment == null && other.environment == null)
                    return 0;
                else
                    return this.environment.Name.CompareTo(other.environment.Name);
            if (this.Equals(other))
                return 0;
            var previousTarget = this.previousVersion;
            var nextTarget = this.nextVersion;
            bool bnen, fnen;
            while ((bnen = (previousTarget != null)) | 
                   (fnen = (nextTarget != null)))
            {
                if (bnen)
                {
                    if (previousTarget.Equals(other))
                        return 1;
                    previousTarget = previousTarget.previousVersion;
                }
                if (fnen)
                {
                    if (nextTarget.Equals(other))
                        return -1;
                    nextTarget = nextTarget.nextVersion;
                }
            }
            throw new InvalidOperationException("Discontinuous versioning series.");
        }

        public static bool operator >=(VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager> left, TVersion right)
        {
            if (object.ReferenceEquals(left, right))
                return true;
            if (object.ReferenceEquals(left, null) && !object.ReferenceEquals(right, null))
                return false;
            else if (!object.ReferenceEquals(left, null) && object.ReferenceEquals(right, null))
                return true;
            return left.CompareTo(right) >= 0;
        }

        public static bool operator <=(VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager> left, TVersion right)
        {
            if (object.ReferenceEquals(left, right))
                return true;
            if (object.ReferenceEquals(left, null) && !object.ReferenceEquals(right, null))
                return true;
            else if (!object.ReferenceEquals(left, null) && object.ReferenceEquals(right, null))
                return true;
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager> left, TVersion right)
        {
            if (object.ReferenceEquals(left, right))
                return false;
            if (object.ReferenceEquals(left, null) && !object.ReferenceEquals(right, null))
                return false;
            else if (!object.ReferenceEquals(left, null) && object.ReferenceEquals(right, null))
                return true;
            return left.CompareTo(right) > 0;
        }

        public static bool operator <(VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager> left, TVersion right)
        {
            if (object.ReferenceEquals(left, right))
                return false;
            if (object.ReferenceEquals(left, null) && !object.ReferenceEquals(right, null))
                return true;
            else if (!object.ReferenceEquals(left, null) && object.ReferenceEquals(right, null))
                return false;
            return left.CompareTo(right) < 0;
        }

        public static bool operator ==(VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager> left, TVersion right)
        {
            if (object.ReferenceEquals(left, right))
                return true;
            if (object.ReferenceEquals(left, null) && !object.ReferenceEquals(right, null))
                return false;
            else if (!object.ReferenceEquals(left, null) && object.ReferenceEquals(right, null))
                return false;
            return left.CompareTo(right) == 0;
        }

        public static bool operator !=(VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager> left, TVersion right)
        {
            if (object.ReferenceEquals(left, right))
                return false;
            if (object.ReferenceEquals(left, null) ^ object.ReferenceEquals(right, null))
                return true;
            return left.CompareTo(right) != 0;
        }

        public static implicit operator TVersion(VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager> other)
        {
            return (TVersion)other;
        }

        public bool Equals(TVersion other)
        {
            if (other == null)
                return false;
            if (object.ReferenceEquals(other, this))
                return true;
            if (other.environment != this.environment)
                return false;
            return other.SubversionSource == this.SubversionSource &&
                   other.VersionStrings.SequenceEqual(this.VersionStrings) &&
                   other.IsServicePack == this.IsServicePack &&
                   other.ServicePackLevel == this.ServicePackLevel &&
                   other.VersionQualifier == this.VersionQualifier;
        }

        public override int GetHashCode()
        {
            return
                (this.environment == null ? 0 : this.environment.GetHashCode()) ^ 
                (this.identityManager == null ? 0 : this.identityManager.GetHashCode()) ^ 
                 this.HintPaths.Select(h=>h.GetHashCode()).Aggregate((a, b) => a^b) ^ 
                 this.IsServicePack.GetHashCode() ^
                 this.VersionStrings.Select(h => h.GetHashCode()).Aggregate((a, b) => a ^ b) ^
                 this.VersionQualifier.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as TVersion);
        }


        public IEnumerable<IVreType<TEnvironment, TVersion, TIdentityManager>> DeprecatedTypes
        {
            get
            {
                return this.deprecatedTypes ?? (this.deprecatedTypes = this.InitializeDeprecatedTypes().SinglePass());
            }
        }

        public IEnumerable<IVreType<TEnvironment, TVersion, TIdentityManager>> AllTypes
        {
            get
            {
                return this.allTypes ?? (this.allTypes = this.InitializeAllTypes().SinglePass());
            }
        }

        private IEnumerable<IVreType<TEnvironment, TVersion, TIdentityManager>> InitializeAllTypes()
        {
            return from ns in GetNamespaceParents(this.Environment)
                   from type in ns.Types.Values
                   where this <= type.Introduced
                   select type;
        }

        public IEnumerable<IVreType<TEnvironment, TVersion, TIdentityManager>> AllDeprecatedTypes
        {
            get
            {
                return this.allDeprecatedTypes ?? (this.allDeprecatedTypes = this.InitializeAllDeprecatedTypes().SinglePass());
            }
        }

        private IEnumerable<IVreType<TEnvironment, TVersion, TIdentityManager>> InitializeAllDeprecatedTypes()
        {
            foreach (var ns in GetNamespaceParents(this.Environment))
                foreach (var type in ns.Types.Values)
                    if (type.Deprecated.Contains((TVersion)this))
                        yield return type;
        }



        private IEnumerable<IVreType<TEnvironment, TVersion, TIdentityManager>> InitializeDeprecatedTypes()
        {
            IEnumerable<IVreType<TEnvironment, TVersion, TIdentityManager>> previousDeprecated;
            if (this.PreviousVersion != null)
                previousDeprecated = this.previousVersion.AllDeprecatedTypes;
            else
                previousDeprecated = new IVreType<TEnvironment, TVersion, TIdentityManager>[0];
            foreach (var ns in GetNamespaceParents(this.Environment))
                foreach (var type in ns.Types.Values)
                    if (!previousDeprecated.Contains(type) && type.Deprecated.Contains((TVersion)this))
                        yield return type;
        }

        private static IEnumerable<IVreNamespaceParent<TEnvironment, TVersion, TIdentityManager>> GetNamespaceParents(IVreNamespaceParent<TEnvironment, TVersion, TIdentityManager> origin)
        {
            yield return origin;
            foreach (var ns in origin.Namespaces.Values)
                foreach (var childspace in GetNamespaceParents(ns))
                    yield return childspace;
        }


        public IVreLibraryDictionaryVersion<TEnvironment, TVersion, TIdentityManager> Libraries
        {
            get 
            {
                throw new NotImplementedException();
            }
        }
    }
}
