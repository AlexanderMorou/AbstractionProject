using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Statements
{
    public class BlockStatementLabelDictionary :
        ControlledStateDictionary<string, ILabelStatement>,
        IBlockStatementLabelDictionary
    {
        private IBlockStatementParent parent;

        public BlockStatementLabelDictionary(IBlockStatementParent parent)
        {
            this.parent = parent;
        }

        internal ILabelStatement Add(string name)
        {
            ILabelStatement result;
            if (parent.ScopeLabels.TryGetValue(name, out result))
                return result;
            result = new LabelStatement(parent, name);
            base._Add(name, result);
            return result;
        }

        internal void Add(ILabelStatement label)
        {
            ILabelStatement attemptLabel ;
            if (parent.ScopeLabels.TryGetValue(label.Name, out attemptLabel))
                if (attemptLabel != label)
                    throw new ArgumentException("Label exists!");
            this._Add(label.Name, label);
        }
    }
}
