using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    public class EventReferenceExpression : 
        IEventReferenceExpression
    {

        public EventReferenceExpression(IMemberParentReferenceExpression source, string name)
        {
            this.Source = source;
            this.Name = name;
        }

        #region IEventReferenceExpression Members

        /// <summary>
        /// Returns the <see cref="Name"/> 
        /// of the expression the <see cref="EventReferenceExpression"/>
        /// points to.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/> 
        /// which contains the source information to accessing
        /// the event.
        /// </summary>
        public IMemberParentReferenceExpression Source { get; private set; }

        #endregion

        #region IExpression Members

        public ExpressionKind Type
        {
            get {
                return ExpressionKinds.EventReference;
            }
        }

        public void Visit(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}.{1}", this.Source, this.Name);
        }
    }
}
