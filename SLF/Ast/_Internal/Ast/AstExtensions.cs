using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf._Internal.Ast
{
    internal static class AstExtensions
    {
        internal static string EscapeStringOrCharCILAndCS(this string toEscape, bool isString = true)
        {
            StringBuilder sb = new StringBuilder((int)((float)(toEscape.Length + 8) * 1.1));
            if (isString)
                sb.Append(@"""");
            else
                sb.Append("'");
            for (int i = 0; i < toEscape.Length; i++)
            {
                char c = toEscape[i];
                //bool b = (i == (toEscape.Length - 1));
                switch (c)
                {
                    case '"':
                        if (!isString)
                            goto default;
                        sb.Append(@"\""");
                        break;
                    case '\'':
                        if (isString)
                            goto default;
                        sb.Append(@"\'");
                        break;
                    case '\\':
                        sb.Append(@"\\");
                        break;
                    case '\r':
                        sb.Append(@"\r");
                        break;
                    case '\n':
                        sb.Append(@"\n");
                        break;
                    case '\0':
                        sb.Append(@"\0");
                        break;
                    case '\x85':
                        sb.Append("\\x85");
                        break;
                    default:
                        if (c > 255)
                        {
                            var baseHexVal = string.Format("{0:x}", (int)(c));
                            while (baseHexVal.Length < 4)
                                baseHexVal = "0" + baseHexVal;
                            sb.AppendFormat("\\u{0}", baseHexVal);
                        }
                        else
                            sb.Append(c);
                        break;
                }
            }
            if (isString)
                sb.Append(@"""");
            else
                sb.Append("'");
            return sb.ToString();
        }

        public static IIntermediateCliManager CreateIdentityManager(ICliRuntimeEnvironmentInfo runtimeEnvironment)
        {
            return new IntermediateCliManager(runtimeEnvironment);
        }


        public static IIntermediateCliManager CreateIdentityManager(CliFrameworkPlatform platform, CliFrameworkVersion version = CliGateway.CurrentVersion, bool resolveCurrent = true, bool useCoreLibrary = true, bool useGlobalAccessCache = true)
        {
            return CreateIdentityManager(CliGateway.GetRuntimeEnvironmentInfo(platform, version, resolveCurrent, useCoreLibrary, useGlobalAccessCache));
        }

        public static IIntermediateCliManager CreateIdentityManager(CliFrameworkPlatform platform, CliFrameworkVersion version, bool resolveCurrent, bool useCoreLibrary, bool useGlobalAccessCache, params string[] additionalResolutionPaths)
        {
            return CreateIdentityManager(CliGateway.GetRuntimeEnvironmentInfo(platform, version, resolveCurrent, useCoreLibrary, useGlobalAccessCache, additionalResolutionPaths));
        }
    }
}
