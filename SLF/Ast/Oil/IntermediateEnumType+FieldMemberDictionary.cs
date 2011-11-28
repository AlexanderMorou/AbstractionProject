using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides a base class for an intermediate enumeration's field member dictionary.
    /// </summary>
    partial class IntermediateEnumType
    {
        public class FieldMemberDictionary :
            IntermediateGroupedMemberDictionary<IEnumType, IIntermediateEnumType, IGeneralMemberUniqueIdentifier, IEnumFieldMember, IIntermediateEnumFieldMember>,
            IIntermediateEnumFieldMemberDictionary,
            IFieldMemberDictionary
        {
            public new IntermediateEnumType Parent
            {
                get
                {
                    return ((IntermediateEnumType)(base.Parent));
                }
            }

            /// <summary>
            /// Creates a new <see cref="FieldMemberDictionary"/>
            /// with the <paramref name="master"/> and <paramref name="parent"/> provided.
            /// </summary>
            /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/> which groups the 
            /// elements of the <see cref="FieldMemberDictionary"/>
            /// with the <paramref name="parent"/>s other members.</param>
            /// <param name="parent">The <see cref="IntermediateEnumType"/> which contains the 
            /// <see cref="FieldMemberDictionary"/></param>
            /// <exception cref="System.ArgumentNullException">thrown when <paramref name="master"/> is null; or
            /// when <paramref name="parent"/> is null.</exception>
            public FieldMemberDictionary(IntermediateFullMemberDictionary master, IntermediateEnumType parent)
                : base(master, parent)
            {
            }

            #region IIntermediateEnumFieldMemberDictionary Members

            private IIntermediateEnumFieldMember Add<TValue>(string name, TValue value)
            {
                var result = new FieldMember(name, this.Parent);
                result.Value = new IntermediateEnumType.FieldMember.ConstantValue<TValue>(value);
                this._Add(result.UniqueIdentifier, result);
                return result;
            }

            public IIntermediateEnumFieldMember Add(string name, sbyte value)
            {
                return Add<sbyte>(name, value);
            }

            public IIntermediateEnumFieldMember Add(string name, byte value)
            {
                return Add<byte>(name, value);
            }

            public IIntermediateEnumFieldMember Add(string name, short value)
            {
                return Add<short>(name, value);
            }

            public IIntermediateEnumFieldMember Add(string name, ushort value)
            {
                return Add<ushort>(name, value);
            }

            public IIntermediateEnumFieldMember Add(string name, int value)
            {
                return Add<int>(name, value);
            }

            public IIntermediateEnumFieldMember Add(string name, uint value)
            {
                return Add<uint>(name, value);
            }

            public IIntermediateEnumFieldMember Add(string name, long value)
            {
                return Add<long>(name, value);
            }

            public IIntermediateEnumFieldMember Add(string name, ulong value)
            {
                return Add<ulong>(name, value);
            }

            public IIntermediateEnumFieldMember Add(string name, IExpression value)
            {
                IIntermediateEnumFieldMember result = new FieldMember(name, this.Parent);
                result.Value = new FieldMember.ExpressionValue(value);
                this._Add(result.UniqueIdentifier, result);
                return result;
            }

            public IIntermediateEnumFieldMember Add(string name)
            {
                IIntermediateEnumFieldMember result = new FieldMember(name, this.Parent);
                this._Add(result.UniqueIdentifier, result);
                return result;
            }


            #endregion

            #region IFieldMemberDictionary Members

            IFieldParent IFieldMemberDictionary.Parent
            {
                get { return this.Parent; }
            }

            #endregion



        }
    }
}
