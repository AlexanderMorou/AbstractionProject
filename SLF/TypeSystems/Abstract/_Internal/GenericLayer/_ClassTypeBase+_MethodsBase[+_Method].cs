using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _ClassTypeBase
    {
        internal class _MethodsBase :
            _MethodMembersBase<IClassMethodMember, IClassType>
        {
            internal _MethodsBase(_FullMembersBase master, IMethodMemberDictionary<IClassMethodMember, IClassType> originalSet, _ClassTypeBase parent)
                : base(master, originalSet, parent)
            {
            }

            protected override IClassMethodMember ObtainWrapper(IClassMethodMember item)
            {
                return new _Method(this.Parent, item);
            }

            internal class _Method :
                _MethodMemberBase<IClassMethodMember, IClassType>,
                IClassMethodMember
            {
                internal _Method(IClassType parent, IClassMethodMember original)
                    : base(parent, original)
                {
                }

                internal _Method(IClassMethodMember original, IControlledTypeCollection genericParameters)
                    : base(original, genericParameters)
                {
                }

                #region IExtendedInstanceMember Members

                public bool IsAsynchronous
                {
                    get
                    {
                        return this.Original.IsAsynchronous;
                    }
                }

                public ClassMethodMemberFlags InstanceFlags
                {
                    get
                    {
                        return this.Original.InstanceFlags;
                    }
                }

                ExtendedMethodMemberFlags IExtendedMethodMember.InstanceFlags
                {
                    get
                    {
                        return (ExtendedMethodMemberFlags)this.InstanceFlags & ExtendedMethodMemberFlags.FlagsMask;
                    }
                }

                ExtendedInstanceMemberFlags IExtendedInstanceMember.InstanceFlags
                {
                    get { return (ExtendedInstanceMemberFlags)this.InstanceFlags ^ ExtendedInstanceMemberFlags.FlagsMask; }
                }

                public bool IsStatic
                {
                    get { return this.Original.IsStatic; }
                }

                public bool IsAbstract
                {
                    get { return this.Original.IsAbstract; }
                }

                public bool IsVirtual
                {
                    get { return this.Original.IsVirtual; }
                }

                public bool IsHideBySignature
                {
                    get { return this.Original.IsHideBySignature; }
                }

                public bool IsFinal
                {
                    get { return this.Original.IsFinal; }
                }

                public bool IsOverride
                {
                    get { return this.Original.IsOverride; }
                }

                #endregion

                protected override IClassMethodMember OnMakeGenericMethod(IControlledTypeCollection genericReplacements)
                {
                    return new _Method(this, genericReplacements);
                }

                #region IClassMethodMember Members

                public bool IsExtensionMethod
                {
                    get
                    {
                        //Generic methods cannot be extension methods.
                        return false;
                    }
                }

                #endregion

                #region IInstanceMember Members
                InstanceMemberFlags IInstanceMember.InstanceFlags
                {
                    get
                    {
                        return ((IInstanceMember)(this.Original)).InstanceFlags;
                    }
                }
                #endregion


                #region IClassMethodMember Members

                public IClassMethodMember PreviousDefinition
                {
                    get
                    {
                        if (!this.IsOverride)
                            throw new InvalidOperationException();
                        var originalPreviousDefinition = this.Original.PreviousDefinition;
                        /* *
                         * This lookup sucks.
                         * *
                         * ToDo: Make the BaseDefinition lookup not suck.
                         * */
                        IClassType parentB = this.Parent;
                        IClassType parentA = Original.Parent;
                        for (; parentA != originalPreviousDefinition.Parent;
                            parentA = parentA.BaseType, parentB = parentB.BaseType) ;
                        var previousDefinition = parentB.Methods.Values[parentA.Methods.IndexOf(originalPreviousDefinition)];
                        if (this.IsGenericConstruct && !this.IsGenericDefinition)
                            return previousDefinition.MakeGenericClosure(this.GenericParameters.ToLockedCollection());
                        else
                            return previousDefinition;
                    }
                }

                public IClassMethodMember BaseDefinition
                {
                    get {
                        if (!this.IsOverride)
                            throw new InvalidOperationException();
                        /* *
                         * This lookup sucks.
                         * *
                         * ToDo: Make the BaseDefinition lookup not suck.
                         * */
                        var baseDefinition = this.Original.BaseDefinition;
                        IClassType parentB = this.Parent;
                        IClassType parentA = Original.Parent;
                        for (; parentA != baseDefinition.Parent;
                            parentA = parentA.BaseType, parentB = parentB.BaseType) ;
                        var current = parentB.Methods.Values[parentA.Methods.IndexOf(baseDefinition)];
                        if (this.IsGenericConstruct && !this.IsGenericDefinition)
                            return current.MakeGenericClosure(this.GenericParameters.ToLockedCollection());
                        else
                            return current;
                    }
                }

                #endregion

            }
        }
    }
}
