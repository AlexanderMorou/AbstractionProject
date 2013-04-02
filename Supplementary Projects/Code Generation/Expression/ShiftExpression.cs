using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.OldCodeGen.Expression
{
    public class ShiftExpression :
        IShiftExpression
    {
        /// <summary>
        /// Data member for <see cref="LeftSide"/>.
        /// </summary>
        private IExpression leftSide;
        /// <summary>
        /// Data member for <see cref="Direction"/>.
        /// </summary>
        private ShiftDirection direction;
        /// <summary>
        /// Data member for <see cref="RightSide"/>.
        /// </summary>
        private IExpression rightSide;

        public IExpression LeftSide
        {
            get
            {
                return this.leftSide;
            }
            set
            {
                this.leftSide = value;
            }
        }

        public ShiftDirection Direction
        {
            get
            {
                return this.direction;
            }
            set
            {
                this.direction = value;
            }
        }

        public IExpression RightSide
        {
            get
            {
                return this.rightSide;
            }
            set
            {
                this.rightSide = value;
            }
        }


        public System.CodeDom.CodeExpression GenerateCodeDom(Translation.ICodeDOMTranslationOptions options)
        {
            return null;
        }

        public void GatherTypeReferences(ref Types.ITypeReferenceCollection result, Translation.ICodeTranslationOptions options)
        {
            if (this.leftSide != null)
                this.leftSide.GatherTypeReferences(ref result, options);
            if (this.rightSide != null)
                this.rightSide.GatherTypeReferences(ref result, options);
        }
    }
}
