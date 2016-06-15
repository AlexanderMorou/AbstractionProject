using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a <see cref="IMassTargetHandler"/> which
    /// distributes across a series of other handlers.
    /// </summary>
    public class MassDisposalAid :
        List<IMassTargetHandler>,
        IMassTargetHandler
    {
        #region IMassTargetHandler Members

        /// <summary>
        /// Begins an exodus upon the elements of the
        /// <see cref="MassDisposalAid"/>.
        /// </summary>
        public void BeginExodus()
        {
            foreach (var element in this)
                if (element == null)
                    continue;
                else
                    element.BeginExodus();
        }

        /// <summary>
        /// Ends an exodus upon the elements of the
        /// <see cref="MassDisposalAid"/>.
        /// </summary>
        public void EndExodus()
        {
            var thisCopy = this.ToArray();
            for (int index = 0; index < thisCopy.Length; index++)
            //Parallel.For(0, thisCopy.Length, index =>
            {
                var thisCurrent = thisCopy[index];
                if (thisCurrent == null)
                    return;
                thisCurrent.EndExodus();
            }//**/);
         }

        #endregion

        #region IDisposable Members
        /// <summary>
        /// Disposes the <see cref="MassDisposalAid"/>.
        /// </summary>
        public void Dispose()
        {
            this.Clear();
        }

        #endregion
    }
}
