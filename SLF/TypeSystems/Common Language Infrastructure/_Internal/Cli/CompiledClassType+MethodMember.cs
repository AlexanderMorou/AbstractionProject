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
using System.Runtime.CompilerServices;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledClassType
    {
        class MethodMember :
            CompiledMethodMemberBase<IClassMethodMember, IClassType>,
            IClassMethodMember,
            ICompiledMethodMember
        {
            public MethodMember(MethodInfo methodInfo, CompiledClassType @class)
                : base(methodInfo, @class)
            {
            }

            #region IInstantiableMember Members

            /// <summary>
            /// Returns whether the <see cref="CompiledClassType.MethodMember"/> is
            /// static.
            /// </summary>
            public bool IsStatic
            {
                get { return this.MemberInfo.IsStatic; }
            }

            /// <summary>
            /// Returns whether the <see cref="CompiledClassType.MethodMember"/> is
            /// virtual (can be overridden).
            /// </summary>
            public bool IsVirtual
            {
                /* *
                 * To ensure that the member is strictly a 'virtual' member in the 
                 * way people refer to virtual; that is it is the original definition:
                 * newslot & virtual & HideBySignature.  Key point being 'newslot'.
                 * Override members are HideBySignature, virtual, but no newslot.
                 * Non virtual members that can't be overridden don't have 'virtual' set,
                 * obviously.
                 * */
                get { return this.IsHideBySignature && this.MemberInfo.IsVirtual && ((this.MemberInfo.Attributes & MethodAttributes.NewSlot) == MethodAttributes.NewSlot); }
            }

            /// <summary>
            /// Returns whether the <see cref="CompiledClassType.MethodMember"/>
            /// hides the original definition completely.
            /// </summary>
            public bool IsHideBySignature
            {
                get { return this.MemberInfo.IsHideBySig; }
            }

            /// <summary>
            /// Returns whether the <see cref="CompiledClassType.MethodMember"/>
            /// finalizes the member removing the overrideable 
            /// status.
            /// </summary>
            public bool IsFinal
            {
                get { return this.MemberInfo.IsFinal; }
            }

            public bool IsOverride
            {
                /* *
                 * It's only an overridden member if it has the virtual, and HideBySignature 
                 * attributes set; newslot is reserved for the virtual (original) 
                 * definition.
                 * */
                get
                {
                    return 
                        this.MemberInfo.IsHideBySig &&
                        this.MemberInfo.IsVirtual && 
                        ((this.MemberInfo.Attributes & MethodAttributes.NewSlot) != MethodAttributes.NewSlot);
                }
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
                    if (this.IsVirtual && !IsOverride)
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

            protected override IClassMethodMember OnMakeGenericClosure(ITypeCollectionBase genericReplacements)
            {
                return new _ClassTypeBase._MethodsBase._Method(this, genericReplacements);
            }

            #region IClassMethodMember Members

            public bool IsExtensionMethod
            {
                get
                {
                    if (this.Parent.SpecialModifier == SpecialClassModifier.ExtensionTarget && this.IsStatic)
                        return this.MemberInfo.IsDefined(typeof(ExtensionAttribute), false);
                    return false;
                }
            }

            #endregion

            #region IInstanceMember Members

            InstanceMemberFlags IInstanceMember.InstanceFlags
            {
                get { return (InstanceMemberFlags)(this.InstanceFlags & (ExtendedInstanceMemberFlags)(InstanceMemberFlags.InstanceMemberFlagsMask)); }
            }

            #endregion

            #region IClassMethodMember Members

            public IClassMethodMember BaseDefinition
            {
                get
                {
                    if (!this.IsOverride)
                        throw new InvalidOperationException();
                    //Obtain the base definition from the member info.
                    var baseMember = this.MemberInfo.GetBaseDefinition();
                    //Obtain the type containing the base member.
                    IClassType p = (IClassType)baseMember.DeclaringType.GetTypeReference();
                    //Iterate through its methods.
                    foreach (ICompiledMethodMember member in p.Methods.Values)
                        //When the method handles equal one another: Match found.
                        if (member.MemberInfo.MethodHandle == baseMember.MethodHandle)
                            return ((IClassMethodMember)(baseMember));
                    throw new InvalidOperationException("Missing base member");
                }
            }

            #endregion

        }
    }
}
