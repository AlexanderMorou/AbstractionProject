using AllenCopeland.Abstraction.Slf._Internal.Ast.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Cli
{
    public static class IntermediateCliGateway
    {
        private static string __CurrentGlobalManagerName = string.Format("GlobalManager{0}", Guid.NewGuid().ToString());
        private static List<string> _globalResolutionPaths = new List<string>();
        internal static event EventHandler<EventArgs<string>> GlobalResolutionPathAdded;
        internal static event EventHandler<EventArgs<string>> GlobalResolutionPathRemoved;
        private static object syncObject = new object();
        public static IIntermediateCliManager GlobalManager
        {
            get
            {
                var result = CallContext.GetData(__CurrentGlobalManagerName) as IIntermediateCliManager;
                if (result == null)
                {
                    CallContext.SetData(__CurrentGlobalManagerName, result = GetGlobalIdentityManager());
                    result.Disposed += GlobalManager_Disposed;
                }
                return result;
            }
        }

        private static IntermediateCliManager GetGlobalIdentityManager()
        {
            var result = (IntermediateCliManager)CreateIdentityManager(CliFrameworkPlatform.AbstractionPlatform, CliFrameworkVersion.CurrentVersion, true, true, true, _globalResolutionPaths == null ? new string[0] : _globalResolutionPaths.ToArray());
            result.RegisterForGlobalPaths();
            return result;
        }

        public static void AddGlobalResolutionPath(string path)
        {
            lock (syncObject)
            {
                if (_globalResolutionPaths == null)
                    _globalResolutionPaths = new List<string>();
                if (Directory.Exists(path))
                    _globalResolutionPaths.Add(path);
                OnGlobalResolutionPathAdded(path);
            }
        }

        private static void OnGlobalResolutionPathAdded(string path)
        {
            lock (syncObject)
            {
                var eventCopy = GlobalResolutionPathAdded;
                if (eventCopy != null)
                    eventCopy(typeof(IntermediateCliGateway), new EventArgs<string>(path));
            }
        }

        private static void OnGlobalResolutionPathRemoved(string path)
        {
            lock (syncObject)
            {
                var eventCopy = GlobalResolutionPathRemoved;
                if (eventCopy != null)
                    eventCopy(typeof(IntermediateCliGateway), new EventArgs<string>(path));
            }
        }

        public static void RemoveGlobalResolutionPath(string path)
        {
            lock (syncObject)
            {
                if (_globalResolutionPaths != null)
                    if (_globalResolutionPaths.Remove(path))
                        OnGlobalResolutionPathRemoved(path);
            }
        }

        public static IEnumerable<string> GlobalResolutionPaths
        {
            get
            {
                if (_globalResolutionPaths == null)
                    yield break;
                foreach (var path in _globalResolutionPaths)
                    yield return path;
            }
        }

        private static void GlobalManager_Disposed(object sender, EventArgs e)
        {
            var cliManagerSender = sender as IIntermediateCliManager;
            if (cliManagerSender != null)
            {
                cliManagerSender.Disposed -= GlobalManager_Disposed;
                CallContext.SetData(__CurrentGlobalManagerName, null);
            }
        }

        public static ITypeCastExpression Cast(this IExpression target, Type castType)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (castType == null)
                throw new ArgumentNullException("castType");
            return target.Cast(castType.GetTypeReference());
        }

        public static ICreateInstanceExpression GetNewExpression(this Type target, params IExpression[] parameters)
        {
            return GlobalManager.ObtainTypeReference(target).GetNewExpression(parameters);
        }

        public static TypedName WithName(this Type target, string name)
        {
            return new TypedName(name, target.GetTypeReference());
        }

        public static ITypeReferenceExpression GetTypeExpression(this Type type)
        {
            return GlobalManager.ObtainTypeReference(type).GetTypeExpression();
        }

        public static IType GetTypeReference(this Type target)
        {
            return target.GetTypeReference(GlobalManager);
        }

        public static TType GetTypeReference<TType>(this Type target, params IType[] genericParameters)
            where TType :
                IType
        {
            if (genericParameters == null)
                throw new ArgumentNullException("genericParameters");
            var result = target.GetTypeReference<TType>(GlobalManager);

            if (result is IGenericType && genericParameters.Length > 0)
                return (TType)((IGenericType)result).MakeGenericClosure(genericParameters);
            else if (genericParameters.Length > 0)
                throw new ArgumentException(string.Format("The specified type '{0}' is not a generic type and generic parameters were provided.", target.FullName), "genericParameters");
            return result;
        }

        public static IIntermediateCliManager CreateIdentityManager(IIntermediateCliRuntimeEnvironmentInfo runtimeEnvironment)
        {
            return new IntermediateCliManager(runtimeEnvironment);
        }

        public static IIntermediateCliManager CreateIdentityManager(CliFrameworkPlatform platform, CliFrameworkVersion version = CliGateway.CurrentVersion, bool resolveCurrent = true, bool useCoreLibrary = true, bool useGlobalAccessCache = true)
        {
            return CreateIdentityManager(IntermediateCliGateway.GetRuntimeEnvironmentInfo(platform, version, resolveCurrent, useCoreLibrary, useGlobalAccessCache));
        }

        public static IIntermediateCliManager CreateIdentityManager(CliFrameworkPlatform platform, CliFrameworkVersion version, bool resolveCurrent, bool useCoreLibrary, bool useGlobalAccessCache, params string[] additionalResolutionPaths)
        {
            return CreateIdentityManager(IntermediateCliGateway.GetRuntimeEnvironmentInfo(platform, version, resolveCurrent, useCoreLibrary, useGlobalAccessCache, additionalResolutionPaths));
        }

        public static IIntermediateCliRuntimeEnvironmentInfo GetRuntimeEnvironmentInfo(CliFrameworkPlatform platform, CliFrameworkVersion version = CliFrameworkVersion.CurrentVersion, bool resolveCurrent = true, bool useCoreLibrary = true, bool useGlobalAccessCache = true)
        {
            return new IntermediateCliRuntimeEnvironmentInfo(resolveCurrent, platform, version, useCoreLibrary, useGlobalAccessCache);
        }

        public static IIntermediateCliRuntimeEnvironmentInfo GetRuntimeEnvironmentInfo(CliFrameworkPlatform platform, CliFrameworkVersion version, bool resolveCurrent, bool useCoreLibrary, bool useGlobalAccessCache, params string[] additionalResolutionPaths)
        {
            return new IntermediateCliRuntimeEnvironmentInfo(resolveCurrent, platform, version, additionalResolutionPaths, useCoreLibrary, useGlobalAccessCache);
        }

        public static ICreateInstanceExpression GetNewExpression(this Type target, ICliManager identityManager, params IExpression[] parameters)
        {
            return identityManager.ObtainTypeReference(target).GetNewExpression(parameters);
        }
    }
}
