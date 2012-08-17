using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract
{
    internal class _Version :
        IVersion
    {
        public _Version(Version version)
        {
            this.Major = version.Major;
            this.Minor = version.Minor;
            this.Build = version.Build == -1 ? 0 : version.Build;
            this.Revision = version.Revision == -1 ? 0 : version.Revision;
        }

        public _Version(int major, int minor, int build, int revision) {
            this.Major = major;
            this.Minor = minor;
            this.Build = build;
            this.Revision = revision;
        }

        //#region IVersion Members

        public int Major { get; set; }

        public int Minor { get; set; }

        public int Build { get; set; }

        public int Revision { get; set; }

        //#endregion

        public static implicit operator _Version(Version source)
        {
            return new _Version(source);
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}.{3}", this.Major, this.Minor, this.Build, this.Revision);
        }

        public override bool Equals(object obj)
        {
            if (obj is IVersion)
            {
                var iv = obj as IVersion;
                return iv.Major == this.Major &&
                       iv.Minor == this.Minor &&
                       iv.Revision == this.Revision &&
                       iv.Build == this.Build;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.Major ^ this.Minor << 3 ^ this.Revision ^ ~this.Build;
        }
    }
}
