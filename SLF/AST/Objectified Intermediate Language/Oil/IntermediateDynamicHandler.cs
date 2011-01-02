using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using System.Reflection.Emit;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf.Oil
{
    internal sealed partial class IntermediateDynamicHandler :
        IntermediateAssembly<IntermediateDynamicHandler>,
        IIntermediateDynamicHandler
    {
        private Guid DynamicHandlerGuid = Guid.NewGuid();
        private AssemblyBuilder builder;
        private AssemblyName assemblyName;
        private ModuleBuilder rootModule;
        private bool autoCollect;
        private IIntermediateMethodMemberDictionary<IIntermediateDynamicMethod, IIntermediateDynamicMethod, IIntermediateDynamicHandler, IIntermediateDynamicHandler> methods;

        internal IntermediateDynamicHandler(bool autoCollect = false)
            : base(string.Empty)
        {
            this.autoCollect = autoCollect;
            base.Name = string.Format("DynamicHandler<{0}>", DynamicHandlerGuid.ToString());
            this.assemblyName = new AssemblyName(this.Name);
        }

        private IntermediateDynamicHandler(IntermediateDynamicHandler rootHandler)
            : base(rootHandler)
        {

        }

        protected internal AssemblyName AssemblyName
        {
            get
            {
                if (this.IsRoot)
                    return this.assemblyName;
                else
                    return this.GetRoot().assemblyName;
            }
        }

        public AssemblyBuilder Builder
        {
            get
            {
                if (this.IsRoot)
                {
                    if (this.builder == null)
                        this.builder = AppDomain.CurrentDomain.DefineDynamicAssembly(this.AssemblyName, this.autoCollect ? AssemblyBuilderAccess.RunAndCollect : AssemblyBuilderAccess.Run);
                    return this.builder;
                }
                else
                    return this.GetRoot().Builder;
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (this.IsRoot)
                {
                    if (this.methods != null)
                        this.methods.Dispose();
                    this.assemblyName = null;
                    this.rootModule = null;
                    this.builder = null;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #region IIntermediateMethodParent<IIntermediateDynamicMethod,IIntermediateDynamicMethod,IIntermediateDynamicHandler,IIntermediateDynamicHandler> Members

        public IIntermediateMethodMemberDictionary<IIntermediateDynamicMethod, IIntermediateDynamicMethod, IIntermediateDynamicHandler, IIntermediateDynamicHandler> Methods
        {
            get {
                if (this.IsRoot)
                {
                    if (this.methods == null)
                        this.methods = new MethodDictionary(this);
                    return this.methods;
                }
                else
                    return this.GetRoot().Methods;
            }
        }

        #endregion

        #region IIntermediateMethodParent Members

        IIntermediateMethodMemberDictionary IIntermediateMethodParent.Methods
        {
            get { return (IIntermediateMethodMemberDictionary)this.Methods; }
        }

        #endregion

        #region IMethodParent Members

        IMethodMemberDictionary IMethodParent.Methods
        {
            get { return (IMethodMemberDictionary)this.Methods; }
        }

        #endregion

        #region IMethodParent<IIntermediateDynamicMethod,IIntermediateDynamicHandler> Members

        IMethodMemberDictionary<IIntermediateDynamicMethod, IIntermediateDynamicHandler> IMethodParent<IIntermediateDynamicMethod, IIntermediateDynamicHandler>.Methods
        {
            get { return this.Methods; }
        }

        #endregion

        protected override IntermediateDynamicHandler GetNewPart()
        {
            return new IntermediateDynamicHandler(this);
        }
    }
}
