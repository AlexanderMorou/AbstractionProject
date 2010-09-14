using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Parsers.Oilexer;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer.Tokens;
using AllenCopeland.Abstraction.Slf.Languages.Oilexer;
using AllenCopeland.Abstraction.Slf.Compilers.Oilexer;
using System.Threading.Tasks;
using System.IO;

namespace AllenCopeland.Abstraction.Slf.Languages.Oilexer.Rules
{
    internal class SyntacticalParser
    {
        private ParserBuilder builder;
        GrammarVocabulary fullVocab = null;
        GrammarVocabulary eofEntry = null;
        internal SyntacticalParser(ParserBuilder builder)
        {
            this.builder = builder;
        }
        public void Parse(string filename)
        {
            var lexicalAnalyzer = builder.LexicalAnalyzer;
#if FALSE
            lexicalAnalyzer.Open(filename);
            bool hasNext = true;
            //List<RegularLanguageScanData.Entry> tokens = new List<RegularLanguageScanData.Entry>();
            if (this.fullVocab == null)
                this.fullVocab = new GrammarVocabulary(builder.GrammarSymbols, (from symbol in builder.GrammarSymbols
                                                                                where symbol is IGrammarTokenSymbol
                                                                                select symbol).ToArray());
            if (eofEntry == null)
                eofEntry = new GrammarVocabulary(builder.GrammarSymbols, (from symbol in builder.GrammarSymbols
                                                                          let tokenSymbol = symbol as IGrammarTokenSymbol
                                                                          where tokenSymbol != null
                                                                          where tokenSymbol.Source is ITokenEofEntry
                                                                          select tokenSymbol).First());
            do
            {

                var currentToken = builder.LexicalAnalyzer.NextToken(fullVocab);
                var longest = (from entry in currentToken
                               orderby entry.Length descending
                               select entry).FirstOrDefault();
                if (longest == null)
                    hasNext = false;
                else if (longest.ID.Equals(eofEntry))
                    hasNext = false;
                else
                {
                    //if (!((longest.ID.GetSymbols().First() as IGrammarTokenSymbol).Source.Unhinged))
                    //    tokens.Add(longest);
                    lexicalAnalyzer.MoveTo(currentToken.Location + longest.Length);
                }
            } while (hasNext);
            lexicalAnalyzer.Close();
#endif
        }

        private static Stack<T> Pusher<T>(T root)
        {
            var result = new Stack<T>();
            result.Push(root);
            return result;
        }

        //public void Parse(string file)
        //{
        //    var startRuleState = SyntacticalStreamState.ObtainRule(builder.StartEntry, builder);
        //    var lexicalAnalyzer = builder.LexicalAnalyzer;
        //    lexicalAnalyzer.Open(file);
        //    lexicalAnalyzer.Close();
        //}

    }
}
