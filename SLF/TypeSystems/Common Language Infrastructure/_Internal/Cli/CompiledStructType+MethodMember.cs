using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledStructType
    {
        private class MethodMember :
            CompiledMethodMemberBase<IStructMethodMember, IStructType>,
            IStructMethodMember,
            ICompiledMethodMember
        {
            public MethodMember(MethodInfo methodInfo, CompiledStructType @struct)
                : base(methodInfo, @struct)
            {
            }

            #region IInstantiableMember Members

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

            public ExtendedInstanceMemberFlags InstanceFlags
            {
                get
                {
                    ExtendedInstanceMemberFlags imfs = ExtendedInstanceMemberFlags.None;
                    if (this.IsStatic)
                        imfs |= ExtendedInstanceMemberFlags.Static;
                    if (this.IsVirtual)
                        imfs |= ExtendedInstanceMemberFlags.Virtual;
                    if (this.IsOverride)
                        imfs |= ExtendedInstanceMemberFlags.Override;
                    if (this.IsFinal)
                        imfs |= ExtendedInstanceMemberFlags.Final;
                    if (this.IsHideBySignature)
                        imfs |= ExtendedInstanceMemberFlags.HideBySignature;
                    if (this.IsAbstract)
                        imfs |= ExtendedInstanceMemberFlags.Abstract;
                    return imfs;
                }
            }
            #endregion

            protected override IStructMethodMember OnMakeGenericMethod(ITypeCollectionBase genericReplacements)
            {
                return new _StructTypeBase._MethodsBase._Method(this, genericReplacements);
            }
            #region IInstanceMember Members

            InstanceMemberFlags IInstanceMember.InstanceFlags
            {
                get { return (InstanceMemberFlags)this.InstanceFlags; }
            }

            #endregion

            #region IStructMethodMember Members

            public IClassMethodMember BaseDefinition
            {
                get {
                    if (!this.IsOverride)
                        throw new InvalidOperationException();
                    MethodInfo q = this.MemberInfo.GetBaseDefinition();
                    var p = q.DeclaringType.GetTypeReference();
                    foreach (ICompiledMethodMember member in ((IClassType)(p)).Methods.Values)
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
