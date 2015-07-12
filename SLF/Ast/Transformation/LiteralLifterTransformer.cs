using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;

namespace AllenCopeland.Abstraction.Slf.Transformation
{
    internal class LiteralLifterTransformer :
        IPrimitiveTransformer
    {
        #region IPrimitiveVisitor<TransformationKind> Members

        public TransformationKind Visit(IPrimitiveExpression<bool> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<char> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<string> expression, ITransformationContext context)
        {
            return TransformationKind.Replace;
        }

        public TransformationKind Visit(IPrimitiveExpression<byte> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<sbyte> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<ushort> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<short> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<uint> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<int> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<ulong> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<long> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<float> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<double> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind Visit(IPrimitiveExpression<decimal> expression, ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        public TransformationKind VisitNull(ITransformationContext context)
        {
            return TransformationKind.Ignore;
        }

        #endregion


        #region IIntermediateTransformer Members

        public TransformerKind Kind
        {
            get { return TransformerKind.Obfuscator; }
        }

        #endregion
    }
}
