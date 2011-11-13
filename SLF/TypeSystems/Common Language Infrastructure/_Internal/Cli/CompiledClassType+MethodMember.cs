using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
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
            private bool? isAsync;
            private bool? hasBaseDefinition;
            private bool? hasPreviousDefinition;
            private IClassMethodMember baseDefinition;
            public MethodMember(MethodInfo methodInfo, CompiledClassType @class)
                : base(methodInfo, @class)
            {
            }

            #region IInstantiableMember Members

            public ClassMethodMemberFlags InstanceFlags
            {
                get
                {
                    ClassMethodMemberFlags imfs = ClassMethodMemberFlags.None;
                    if (this.IsStatic)
                        imfs |= ClassMethodMemberFlags.Static;
                    if (this.IsVirtual)
                        imfs |= ClassMethodMemberFlags.Virtual;
                    if (this.IsOverride)
                        imfs |= ClassMethodMemberFlags.Override;
                    if (this.IsFinal)
                        imfs |= ClassMethodMemberFlags.Final;
                    if (this.IsHideBySignature)
                        imfs |= ClassMethodMemberFlags.HideBySignature;
                    if (this.IsAbstract)
                        imfs |= ClassMethodMemberFlags.Abstract;
                    if (this.IsAsynchronous)
                        imfs |= ClassMethodMemberFlags.Async;
                    if (this.IsExtensionMethod)
                        imfs |= ClassMethodMemberFlags.Extension;
                    return imfs;
                }
            }
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
                        ((this.MemberInfo.Attributes & MethodAttributes.NewSlot) == MethodAttributes.ReuseSlot);
                }
            }

            public bool IsAbstract
            {
                get { return this.MemberInfo.IsAbstract; }
            }

            ExtendedInstanceMemberFlags IExtendedInstanceMember.InstanceFlags
            {
                get
                {
                    return ((ExtendedInstanceMemberFlags)this.InstanceFlags) & ExtendedInstanceMemberFlags.FlagsMask;
                }
            }

            ExtendedMethodMemberFlags IExtendedMethodMember.InstanceFlags
            {
                get
                {
                    return (ExtendedMethodMemberFlags)this.InstanceFlags & ExtendedMethodMemberFlags.FlagsMask;
                }
            }
            #endregion

            protected override IClassMethodMember OnMakeGenericClosure(ITypeCollectionBase genericReplacements)
            {
                return new _ClassTypeBase._MethodsBase._Method(this, genericReplacements);
            }

            #region IClassMethodMember Members
            private bool? isExtensionMethod;
            private IClassMethodMember previousDefinition;
            public bool IsExtensionMethod
            {
                get
                {

                    if (this.isExtensionMethod == null)
                        if (this.Parent.SpecialModifier == SpecialClassModifier.TypeExtensionSource && this.IsStatic)
                            isExtensionMethod = this.MemberInfo.IsDefined(typeof(ExtensionAttribute), false);
                        else
                            isExtensionMethod = false;
                    return this.isExtensionMethod.Value;
                }
            }

            #endregion
            /// <summary>
            /// Returns whether the <see cref="CompiledStructType.MethodMember"/>
            /// is an asynchronous method.
            /// </summary>
            public bool IsAsynchronous
            {
                get
                {
                    if (this.isAsync == null)
                    {
                        var returnType = this.ReturnType;
                        if (returnType == CommonTypeRefs.Void && this.Name.Length >= 5)
                        {
                            if (this.Name.Substring(this.Name.Length - 5).ToLower() == "async")
                                this.isAsync = true;
                            else
                                this.isAsync = false;
                        }
                        else if (returnType == CommonTypeRefs.Task)
                            this.isAsync = true;
                        else if (returnType.ElementClassification == TypeElementClassification.GenericTypeDefinition && returnType.ElementType != null && returnType.ElementType == CommonTypeRefs.TaskOfT)
                            this.isAsync = true;
                        else
                            this.isAsync = false;
                    }
                    return this.isAsync.Value;
                }
            }

            #region IInstanceMember Members

            InstanceMemberFlags IInstanceMember.InstanceFlags
            {
                get { return (InstanceMemberFlags)this.InstanceFlags & InstanceMemberFlags.FlagsMask; }
            }

            #endregion

            #region IClassMethodMember Members

            public IClassMethodMember PreviousDefinition
            {
                get
                {
                    if (this.hasPreviousDefinition == null)
                    {
                        if (!this.IsOverride)
                        {
                            this.hasPreviousDefinition = false;
                            throw new InvalidOperationException();
                        }
                        try
                        {
                            this.previousDefinition = this.ObtainPreviousDefinition();
                            this.hasPreviousDefinition = true;
                            return previousDefinition;
                        }
                        catch (InvalidOperationException e)
                        {
                            this.hasPreviousDefinition = false;
                            throw e;
                        }
                    }
                    else if (hasPreviousDefinition.Value)
                        return this.previousDefinition;
                    else
                        throw new InvalidOperationException();
                }
            }

            public IClassMethodMember BaseDefinition
            {
                get
                {
                    if (hasBaseDefinition == null){
                        if (!this.IsOverride)
                        {
                            this.hasBaseDefinition=false;
                            throw new InvalidOperationException();
                        }
                        //Obtain the base definition from the member info.
                        var baseMember = this.MemberInfo.GetBaseDefinition();
                        //Obtain the type containing the base member.
                        IClassType baseDefinitionType = baseMember.DeclaringType.GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
                        //Iterate through its methods.
                        var baseMethodParameterTypes = 
                            (from p in baseMember.GetParameters()
                             select p.ParameterType.GetTypeReference()).ToArray();
                        foreach (IClassMethodMember member in baseDefinitionType.Methods.Values)
                            //When the method handles equal one another: Match found.
                            if (member.Name == baseMember.Name &&
                                member.Parameters.ParameterTypes.SequenceEqual(baseMethodParameterTypes))
                            {
                                this.baseDefinition = member;
                                hasBaseDefinition = true;
                                return this.baseDefinition;
                            }
                    throw new InvalidOperationException("Missing base member");
                    }
                    else
                    {
                        if (hasBaseDefinition.Value)
                            return this.baseDefinition;
                        else
                            throw new InvalidOperationException();
                    }
                }
            }

            #endregion

        }
    }
}
