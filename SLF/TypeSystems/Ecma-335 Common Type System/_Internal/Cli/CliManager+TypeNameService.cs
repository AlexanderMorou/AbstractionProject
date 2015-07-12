using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CliManager
    {
        private class TypeNameService :
            ITypeNameBuilderService
        {
            private CliManager identityManager;
            private long callcount = 0;
            public TypeNameService(CliManager identityManager)
            {
                this.identityManager = identityManager;
            }

            public IIdentityManager IdentityManager
            {
                get {
                    return this.identityManager;
                }
            }

            public string BuildTypeName(IType target, bool shortFormGeneric = false, bool numericTypeParams = false, TypeParameterDisplayMode typeParameterDisplayMode = TypeParameterDisplayMode.SystemStandard)
            {
                callcount++;
                return TypeExtensions.BuildTypeNameInternal(target, shortFormGeneric, numericTypeParams, typeParameterDisplayMode);
            }

            public IIdentityManager Provider
            {
                get { return this.identityManager; }
            }

            IServiceProvider<IIdentityService> IService<IIdentityService>.Provider
            {
                get { return this.IdentityManager; }
            }

            public Guid ServiceGuid
            {
                get { return IdentityServiceGuids.TypeNameBuilderService; }
            }

        }
    }
}
