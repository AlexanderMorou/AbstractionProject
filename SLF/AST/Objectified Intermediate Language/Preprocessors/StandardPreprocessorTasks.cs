using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Preprocessors
{
    public enum PreprocessorResolutionScope
    {
        Local,
        Type,
        Global,
        ObjectCreation,
        Workspace,
        Member,
        Extension,
    }
    public enum PreprocessorGenerationKind
    {
        AnonymousTypes,
        Queries,
        LambdaExtraction,
        Iterators,
        DuckTypingMarshalling,
        DuckTypingDefinitions,
    }
    public enum PreprocessorTaskCategory
    {
        Aggregate,
        Resolution,
        Generation,
        Custom,
    }
}
