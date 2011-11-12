using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
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

                internal _Method(IClassMethodMember original, ITypeCollectionBase genericParameters)
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

                protected override IClassMethodMember OnMakeGenericMethod(ITypeCollectionBase genericReplacements)
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
                        /* 
                        //A product of over-thought.
                        if (parentA == parentB)
                        {
                            if (this.IsGenericConstruct)
                            {
                                if (this.IsGenericDefinition)
                                    return baseDefinition;
                                else
                                    /* *
                                     * The current instance is a closed generic method; thus
                                     * resulting in a base definition that must also be 
                                     * a closed generic method.
                                     * * /
                                    return this.BaseDefinition.MakeGenericClosure(this.GenericParameters.ToCollection());
                            }
                            else
                                return baseDefinition;
                        }
                        else
                        {
                            /* *
                             * Occurs when the parent decided upon is a generic type, like the
                             * current member's parent.
                             * *
                             * This complicates things a little.
                             * * /
                            if (this.IsGenericConstruct)
                            {
                                /* *
                                 * Only consider the type-parameters when necessary.
                                 * *
                                 * Especially important for the cache system used in the 
                                 * CLI type system wrapper.
                                 * * /
                            }
                        }
                        */
                    }
                }

                #endregion

            }
        }
    }
}
