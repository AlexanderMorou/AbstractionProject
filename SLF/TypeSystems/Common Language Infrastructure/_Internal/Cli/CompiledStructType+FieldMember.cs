using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
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
        /// <summary>
        /// Provides a base implementation of an 
        /// <see cref="ICompiledFieldMember"/> which 
        /// is a child to an <see cref="IClassType"/>.
        /// </summary>
        private class FieldMember :
            CompiledFieldMemberBase<IStructFieldMember, IStructType>,
            IStructFieldMember,
            ICompiledFieldMember
        {
            /// <summary>
            /// Creates a new <see cref="FieldMember"/>
            /// with the <paramref name="memberInfo"/> 
            /// and <paramref name="parent"/> provided.
            /// </summary>
            /// <param name="memberInfo">
            /// The <see cref="FieldInfo"/> related to the
            /// <see cref="FieldMember"/>'s data.</param>
            /// <param name="parent">
            /// The <typeparamref name="CompiledStructType"/> 
            /// which contains the <see cref="FieldMember"/>.
            /// </param>
            public FieldMember(FieldInfo memberInfo, CompiledStructType parent)
                : base(memberInfo, parent)
            {
            }

            #region IInstantiableMember Members

            /// <summary>
            /// Returns whether the <see cref="CompiledStructType.FieldMember"/> is
            /// static.
            /// </summary>
            public bool IsStatic
            {
                get { return this.MemberInfo.IsStatic; }
            }

            /// <summary>
            /// Returns whether the <see cref="CompiledStructType.FieldMember"/>
            /// hides the original definition completely.
            /// </summary>
            /// <remarks>Returns true if the declaring type of the parent
            /// that contains the field member contains a member of the same name;
            /// false otherwise.</remarks>
            public bool IsHideBySignature
            {
                get
                {
                    if (this.Parent.DeclaringType == null)
                        return false;
                    else
                        return this.Parent.DeclaringType.Members.Values.Any(m => m.Entry.Name == this.Name);
                }
            }

            public InstanceMemberFlags InstanceFlags
            {
                get
                {
                    InstanceMemberFlags imfs = InstanceMemberFlags.None;
                    if (this.IsStatic)
                        imfs |= InstanceMemberFlags.Static;
                    if (this.IsHideBySignature)
                        imfs |= InstanceMemberFlags.HideBySignature;
                    return imfs;
                }
            }
            #endregion

            #region IScopedDeclaration Members

            public AccessLevelModifiers AccessLevel
            {
                get { return this.MemberInfo.GetAccessModifiers(); }
            }

            #endregion

        }
    }
}
