using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
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
        /// <summary>
        /// Creates a new <see cref="LowerBoundTargetAttribute"/> with the
        /// <paramref name="bounds"/> provided.
        /// </summary>
        /// <param name="bounds">The <see cref="Int32"/> series
        /// which designates the lower bound nature of the array used,
        /// per dimension.</param>
        public LowerBoundTargetAttribute(params int[] bounds)
        {
            this.Bounds = bounds;
        }

        /// <summary>
        /// Returns the <see cref="Int32"/> array designating the lower bounds of the
        /// parameter, return-type, or field marked with the attribute.
        /// </summary>
        public int[] Bounds { get; private set; }

        /// <summary>
        /// Returns whether the bounds, specified by the <see cref="LowerBoundTargetAttribute"/>,
        /// are nonstandard.
        /// </summary>
        public bool IsNonstandardArray
        {
            get
            {
                foreach (int i in this.Bounds)
                    if (i != 0)
                        return true;
                return false;
            }
        }
    }
}
