using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    public class MassDisposalAid :
        List<IMassTargetHandler>,
        IMassTargetHandler
    {
        #region IMassTargetHandler Members

        public void BeginExodus()
        {
            foreach (var element in this)
                if (element == null)
                    continue;
                else
                    element.BeginExodus();
        }

        public void EndExodus()
        {
            var thisCopy = this.ToArray();
            Parallel.For(0, thisCopy.Length, index =>
            {
                var thisCurrent = thisCopy[index];
                if (thisCurrent == null)
                    return;
                thisCurrent.EndExodus();
            });
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.Clear();
        }

        #endregion
    }
}
