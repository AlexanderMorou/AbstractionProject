using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using AllenCopeland.Abstraction.OldCodeGen.Types;
using AllenCopeland.Abstraction.OldCodeGen.Utilities.Collections;

namespace AllenCopeland.Abstraction.OldCodeGen
{
    /// <summary>
    /// The partial namespace containers
    /// of an <see cref="IIntermediateProject"/>.
    /// </summary>
    public class IntermediateProjectPartials :
        ControlledStateCollection<IIntermediateProject>,
        IIntermediateProjectPartials,
        ISegmentableDeclarationTargetPartials
    {
        /// <summary>
        /// Data member for <see cref="RootDeclaration"/>.
        /// </summary>
        private IIntermediateProject rootDeclaration;

        public IntermediateProjectPartials(IIntermediateProject rootDeclaration)
        {
            this.rootDeclaration = rootDeclaration;
        }

        #region ISegmentableDeclarationTargetPartials<IIntermediateProject> Members

        public IIntermediateProject RootDeclaration
        {
            get {
                return this.rootDeclaration;
            }
        }

        public IIntermediateProject AddNew()
        {
            IIntermediateProject iip = new IntermediateProject(this.RootDeclaration);
            if (this.RootDeclaration.ParentTarget != null)
                iip.ParentTarget = this.RootDeclaration.ParentTarget;
            lock (this.baseList)
                this.baseList.Add(iip);
            return iip;
        }

        public IIntermediateProject AddNew(IDeclarationTarget parentTarget)
        {
            IIntermediateProject iip = new IntermediateProject(this.RootDeclaration);
            iip.ParentTarget = parentTarget;
            lock (this.baseList)
                this.baseList.Add(iip);
            return iip;
        }

        #endregion

        #region ISegmentableDeclarationTargetPartials Members

        ISegmentableDeclarationTarget ISegmentableDeclarationTargetPartials.RootDeclaration
        {
            get { return this.RootDeclaration; }
        }

        ISegmentableDeclarationTarget ISegmentableDeclarationTargetPartials.AddNew()
        {
            return this.AddNew();
        }

        ISegmentableDeclarationTarget ISegmentableDeclarationTargetPartials.AddNew(IDeclarationTarget parentTarget)
        {
            return this.AddNew(parentTarget);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            foreach (IIntermediateProject iip in this)
                iip.Dispose();
            this.baseList.Clear();
            this.baseList = null;
            this.rootDeclaration = null;
        }

        #endregion

        #region ISegmentableDeclarationTargetPartials Members


        void ISegmentableDeclarationTargetPartials.Remove(ISegmentableDeclarationTarget partial)
        {
            if (!(partial is IIntermediateProject))
                throw new ArgumentException("partial");
        }

        #endregion

        #region ISegmentableDeclarationTargetPartials<IIntermediateProject> Members


        public void Remove(IIntermediateProject partial)
        {
            if (this.Contains(partial))
            {
                partial.Dispose();
                this.baseList.Remove(partial);
            }
        }

        #endregion
    }
}
