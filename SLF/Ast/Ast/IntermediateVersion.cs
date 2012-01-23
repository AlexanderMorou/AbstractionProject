using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities;
using AllenCopeland.Abstraction.Slf.Abstract;

namespace AllenCopeland.Abstraction.Slf.Ast
{
    public class IntermediateVersion :
        IIntermediateVersion
        
    {
        private static readonly DateTime buildEpoch = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Local);
        private int build;
        private int revision;

        public IntermediateVersion(int major, int minor, int build, int revision)
        {
            this.Major = major;
            this.Minor = minor;
            this.Build = build;
            this.Revision = revision;
        }
        public IntermediateVersion(int major, int minor, int build)
        {
            this.Major = major;
            this.Minor = minor;
            this.Build = build;
            this.AutoIncrementRevision = true;
        }

        public IntermediateVersion(int major, int minor)
        {
            this.Major = major;
            this.Minor = minor;
            this.AutoIncrementBuild = true;
            this.AutoIncrementRevision = true;
        }

        public int Major { get; set; }
        public int Minor { get; set; }
        public bool AutoIncrementBuild { get; set; }
        public bool AutoIncrementRevision { get; set; }
        public int Build
        {
            get
            {
                if (AutoIncrementBuild)
                    return (int)(DateTime.Now - buildEpoch).TotalDays;
                else
                    return this.build;
            }
            set
            {
                if (AutoIncrementBuild)
                    throw new InvalidOperationException();
                else
                    this.build = value;
            }
        }


        public int Revision
        {
            get
            {
                
                if (AutoIncrementBuild || AutoIncrementRevision)
                    return (int) ((DateTime.Now - DateTime.Today).TotalSeconds / 2);
                else
                    return this.revision;
            }
            set
            {
                if (AutoIncrementBuild || AutoIncrementRevision)
                    throw new InvalidOperationException();
                else
                    this.revision = value;
            }
        }

        public override string ToString()
        {
            if (this.AutoIncrementBuild)
                return string.Format("{0}.{1}.*", this.Major, this.Minor);
            else if (this.AutoIncrementRevision)
                return string.Format("{0}.{1}.{2}.*", this.Major, this.Minor, this.Build);
            else
                return string.Format("{0}.{1}.{2}.{3}", this.Major, this.Minor, this.Build, this.Revision);
        }

    }
}
