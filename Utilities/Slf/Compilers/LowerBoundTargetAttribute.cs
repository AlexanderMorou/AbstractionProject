using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    /// <summary>
    /// Attribute which defines the lower-bounds of the target
    /// type the <see cref="LowerBoundTargetAttribute"/>
    /// is applied to.
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.ReturnValue | AttributeTargets.Parameter | AttributeTargets.Field)]
    public class LowerBoundTargetAttribute :
        Attribute
    {
        private int[] bounds;
        /// <summary>
        /// Creates a new <see cref="LowerBoundTargetAttribute"/> with the
        /// <paramref name="bounds"/> provided.
        /// </summary>
        /// <param name="bounds">The <see cref="Int32"/> series
        /// which designates the lower bound nature of the array used,
        /// per dimension.</param>
        public LowerBoundTargetAttribute(params int[] bounds)
        {
            if (bounds == null)
                throw new ArgumentNullException("bounds");
            this.bounds = bounds;
        }

        /// <summary>
        /// Returns the <see cref="Int32"/> array designating the lower bounds of the
        /// parameter, return-type, or field marked with the attribute.
        /// </summary>
        public int[] Bounds
        {
            get
            {
                return this.bounds;
            }
        }

        /// <summary>
        /// Returns whether the bounds, specified by the <see cref="LowerBoundTargetAttribute"/>,
        /// are nonstandard.
        /// </summary>
        public bool IsNonstandardArray
        {
            get
            {
                for (int i = 0; i < this.bounds.Length; i++)
                    if (this.bounds[i] != 0)
                        return true;
                return false;
            }
        }
    }
}
