using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using System.Threading.Tasks;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledStructType
    {
        internal class MethodMember :
            CompiledMethodMemberBase<IStructMethodMember, IStructType>,
            IStructMethodMember,
            ICompiledMethodMember
        {
            public MethodMember(MethodInfo methodInfo, CompiledStructType @struct)
                : base(methodInfo, @struct)
            {
            }

            private new ICompiledStructType Parent { get { return (ICompiledStructType) base.Parent; } }

            #region IInstantiableMember Members

            private bool? isAsync;
            /// <summary>
            /// Returns whether the <see cref="CompiledStructType.MethodMember"/>
            /// is an asynchronous method.
            /// </summary>
            public bool IsAsynchronous
            {
                get
                {
                    if (isAsync == null)
                    {
                        var returnType = this.ReturnType;
                        if (returnType == this.Parent.Manager.ObtainTypeReference(TypeSystemSpecialIdentity.VoidType) && this.Name.Length >= 5)
                        {
                            if (this.Name.Substring(this.Name.Length - 5).ToLower() == "async")
                                this.isAsync = true;
                        }
                        else if (returnType == this.Parent.Manager.ObtainTypeReference(TypeSystemSpecialIdentity.AsynchronousTask))
                            this.isAsync = true;
                        else if (returnType.ElementClassification == TypeElementClassification.GenericTypeDefinition && returnType.ElementType == this.Parent.Manager.ObtainTypeReference(TypeSystemSpecialIdentity.AsynchronousTaskOfT))
                            this.isAsync = true;
                        else
                            this.isAsync = false;
                    }
                    return this.isAsync.Value;
                }
            }
            /// <summary>
            /// Returns whether the <see cref="CompiledStructType.MethodMember"/> is
            /// static.
            /// </summary>
            public bool IsStatic
            {
                get { return this.MemberInfo.IsStatic; }
            }

            /// <summary>
            /// Returns whether the <see cref="CompiledStructType.MethodMember"/> is
            /// virtual (can be overridden).
            /// </summary>
            public bool IsVirtual
            {
                get { return this.MemberInfo.IsVirtual; }
            }

            /// <summary>
            /// Returns whether the <see cref="CompiledStructType.MethodMember"/>
            /// hides the original definition completely.
            /// </summary>
            public bool IsHideBySignature
            {
                get { return this.MemberInfo.IsHideBySig; }
            }

            /// <summary>
            /// Returns whether the <see cref="CompiledStructType.MethodMember"/>
            /// finalizes the member removing the overrideable 
            /// status.
            /// </summary>
            public bool IsFinal
            {
                get { return this.MemberInfo.IsFinal; }
            }

            public bool IsOverride
            {
                get { return this.MemberInfo.GetBaseDefinition() != this.MemberInfo; }
            }

            public bool IsAbstract
            {
                get { return this.MemberInfo.IsAbstract; }
            }

            ExtendedInstanceMemberFlags IExtendedInstanceMember.InstanceFlags
            {
                get
                {
                    return ((ExtendedInstanceMemberFlags)(this.InstanceFlags) & ExtendedInstanceMemberFlags.FlagsMask);
                }
            }

            public ExtendedMethodMemberFlags InstanceFlags
            {
                get
                {
                    ExtendedMethodMemberFlags imfs = ExtendedMethodMemberFlags.None;
                    if (this.IsStatic)
                        imfs |= ExtendedMethodMemberFlags.Static;
                    if (this.IsVirtual)
                        imfs |= ExtendedMethodMemberFlags.Virtual;
                    if (this.IsOverride)
                        imfs |= ExtendedMethodMemberFlags.Override;
                    if (this.IsFinal)
                        imfs |= ExtendedMethodMemberFlags.Final;
                    if (this.IsHideBySignature)
                        imfs |= ExtendedMethodMemberFlags.HideBySignature;
                    if (this.IsAbstract)
                        imfs |= ExtendedMethodMemberFlags.Abstract;
                    if (this.IsAsynchronous)
                        imfs |= ExtendedMethodMemberFlags.Async;
                    return imfs;
                }
            }

            #endregion

            protected override IStructMethodMember OnMakeGenericClosure(ITypeCollectionBase genericReplacements)
            {
                return new _StructTypeBase._MethodsBase._Method(this, genericReplacements);
            }
            #region IInstanceMember Members

            InstanceMemberFlags IInstanceMember.InstanceFlags
            {
                get { return ((InstanceMemberFlags)this.InstanceFlags) & InstanceMemberFlags.FlagsMask; }
            }

            #endregion

            #region IStructMethodMember Members

            public IClassMethodMember BaseDefinition
            {
                get {
                    if (!this.IsOverride)
                        throw new InvalidOperationException();
                    MethodInfo q = this.MemberInfo.GetBaseDefinition();
                    IClassType p = (IClassType)this.Parent.Manager.ObtainTypeReference(q.DeclaringType);
                    foreach (ICompiledMethodMember member in p.Methods.Values)
                        if (member.MemberInfo == q)
                            return (IClassMethodMember)member;
                    //Shouldn't occur.
                    throw new InvalidOperationException("Missing base declaration.");
                }
            }

            #endregion


        }
    }
}
