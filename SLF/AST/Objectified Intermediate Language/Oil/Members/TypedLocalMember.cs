using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    public partial class TypedLocalMember :
        LocalMember,
        ITypedLocalMember
    {
        /// <summary>
        /// Creates a new <see cref="TypedLocalMember"/>
        /// with the <paramref name="name"/>, <paramref name="parent"/> and <paramref name="localType"/>
        /// provided.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <param name="localType"></param>
        public TypedLocalMember(string name, IBlockStatementParent parent, IType localType)
            : base(name, parent, LocalTypingKind.Explicit)
        {
            this.LocalType = localType;
        }
        #region ITypedLocalMember Members

        public IType LocalType { get; set; }

        #endregion

        public override IBoundLocalReferenceExpression GetReference()
        {
            return new ReferenceExpression(this);
        }

        public override string ToString()
        {
            if (this.InitializationExpression != null)
                if (this.LocalType != null)
                    return string.Format("{0} {1} = {2}", this.LocalType, this.Name == null ? "?" : this.Name, this.InitializationExpression);
                else
                    return string.Format("? {0} = {1}", this.Name == null ? "?" : this.Name, this.InitializationExpression);
            else
                if (this.LocalType != null)
                    return string.Format("{0} {1}", this.LocalType, this.Name == null ? "?" : this.Name);
                else
                    return string.Format("? {0}", this.Name == null ? "?" : this.Name);
        }
    }
}
