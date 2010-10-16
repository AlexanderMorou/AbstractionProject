using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Cst;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public class CSharpCompiler :
        IntermediateCompiler<ICSharpCompilationUnit>
    {

        internal static readonly ReadOnlyCollection<Type> AutoFormTypes = new ReadOnlyCollection<Type>
        (
            new List<Type>
            (
                new Type[]
                    {
                        typeof(byte),
                        typeof(sbyte),
                        typeof(ushort),
                        typeof(short),
                        typeof(uint),
                        typeof(int),
                        typeof(ulong),
                        typeof(long),
                        typeof(void),
                        typeof(bool),
                        typeof(char),
                        typeof(decimal),
                        typeof(float),
                        typeof(double),
                        typeof(object),
                        typeof(string)
                    }
            )
        );

        public CSharpCompiler()
            : base()
        {
        }

        public override IHighLevelLanguage<ICSharpCompilationUnit> Language
        {
            get { return CSharpGateway.Language; }
        }

        public override IHighLevelLanguageProvider<ICSharpCompilationUnit> Provider
        {
            get { throw new NotImplementedException(); }
        }
    }
}
