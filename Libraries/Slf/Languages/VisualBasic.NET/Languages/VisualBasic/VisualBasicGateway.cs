using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    public static class VisualBasicGateway
    {
        /// <summary>
        /// Obtains the string variation of the <paramref name="version"/>
        /// provided.
        /// </summary>
        /// <param name="version">The <see cref="VisualBasicVersion"/>
        /// which denotes the string version to return.</param>
        /// <returns>A <see cref="String"/> value which denotes a friendly format of the 
        /// <paramref name="version"/>.</returns>
        public static string GetStringVersion(this VisualBasicVersion version)
        {
            int major = 0;
            int minor = 0;
            int build = 0;
            int revision = 0;
            switch (version)
            {
                case VisualBasicVersion.Version07:
                    major = 7;
                    build = 3300;
                    break;
                case VisualBasicVersion.Version07_1:
                    major = 7;
                    build = 5000;
                    break;
                case VisualBasicVersion.Version08:
                    major = 8;
                    break;
                case VisualBasicVersion.Version09:
                    /* *
                     * Version 3.5 of the framework, which was a slight
                     * improvement of version 2.0, thus the version doesn't change.
                     * */
                    major = 8;
                    break;
                case VisualBasicVersion.Version10:
                    major = 10;
                    break;
                case VisualBasicVersion.Version11:
                    major = 10;
                    break;
                case VisualBasicVersion.Version12:
                    major = 10;
                    break;
                default:
                    goto case VisualBasicVersion.CurrentVersion;
            }
            return string.Format("{0}.{1}.{2}.{3}", major, minor, build, revision);
        }

        internal static IClassPropertyMember GetProperty(this IClassType top, string name)
        {
            var muid = TypeSystemIdentifiers.GetMemberIdentifier(name);
            for (IClassType current = top; current != null; current = current.BaseType)
            {
                if (current.Properties.ContainsKey(muid))
                    return current.Properties[muid];
            }
            throw new InvalidOperationException(string.Format("No property {0} found.", name));
        }
    }
}
