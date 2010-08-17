using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides a base class for intermediate field members.
    /// </summary>
    /// <typeparam name="TField">The type of field in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateField">The type of field in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TFieldParent">The type which owns the fields
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateFieldParent">The type which owns the fields
    /// in the intermediate abstract syntax tree.</typeparam>
    public abstract class IntermediateFieldMemberBase<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> :
        IntermediateMemberBase<TFieldParent, TIntermediateFieldParent>,
        IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
        where TField :
            IFieldMember<TField, TFieldParent>
        where TIntermediateField :
            TField,
            IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
        where TFieldParent :
            IFieldParent<TField, TFieldParent>
        where TIntermediateFieldParent :
            TFieldParent,
            IIntermediateFieldParent<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>
    {
        protected IntermediateFieldMemberBase(string name, TIntermediateFieldParent parent)
            : base(parent)
        {
            base.AssignName(name);
        }

        #region IIntermediateFieldMember Members

        public virtual IType FieldType { get; set; }

        #endregion
    }
}
