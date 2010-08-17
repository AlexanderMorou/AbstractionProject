using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Parsers.StateMachines;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Parsers.Tokens
{
    public class CSharpKeywordToken :
        Token,
        ICSharpKeywordToken
    {
        private ulong tokenCase;
        private CSharpKeywordCases section;

        public CSharpKeywordToken(FileLocale location, CSharpKeywordCases section, ulong tokenCase)
            : base(location)
        {
            this.tokenCase = tokenCase;
            this.section = section;
        }

        #region ICSharpKeywordToken Members
        public override uint Length
        {
            get
            { 
                return 
                    section == 
                        CSharpKeywordCases.CaseA ? 
                        CSharpKeywordStateMachine.GetLength((CSharpKeywords1)this.tokenCase) : 
                    section == 
                        CSharpKeywordCases.CaseB ?
                        CSharpKeywordStateMachine.GetLength((CSharpKeywords2)this.tokenCase) :
                        0; 
            }
        }

        public CSharpKeywordCases Section
        {
            get
            {
                return this.section;
            }
        }

        public CSharpKeywords1 KeywordA
        {
            get
            {
                if (this.section == CSharpKeywordCases.CaseA)
                    return (CSharpKeywords1)this.tokenCase;
                else
                    return CSharpKeywords1.None;
            }
        }
        public CSharpKeywords2 KeywordB
        {
            get
            {
                if (this.section == CSharpKeywordCases.CaseB)
                    return (CSharpKeywords2)this.tokenCase;
                else
                    return CSharpKeywords2.None;
            }
        }

        public CSharpKeywordCases Case
        {
            get { return this.section; }
        }

        #endregion

        public override string ToString()
        {
            switch (this.section)
            {
                case CSharpKeywordCases.CaseA:
                    return KeywordA.ToString();
                case CSharpKeywordCases.CaseB:
                    return KeywordB.ToString();
            }
            return "None";
        }

    }
}
