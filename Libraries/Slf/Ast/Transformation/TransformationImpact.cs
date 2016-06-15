using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Transformation
{
    [Flags]
    public enum TransformationImpact :
        ulong
    {
        /// <summary>Denotes that the transformation doesn't have any impact.</summary>
        NoImpact                            = 0,
        /// <summary>Performs the act of unlinking due to a mismatch.</summary>
        Unlink                              = 1L << 00,
        /// <summary>Performs the act of linking due to encountering an unbound element.</summary>
        Link                                = 1L << 01,
        /// <summary>Transforms the hierarchy.</summary>
        Transform                           = 1L << 02,
        /// <summary>Denotes the actions are transformative, but intended as a refactor.</summary>
        Refactor                            = 1L << 03,
        /// <summary>Denotes that the transformation replaces the current expression</summary>
        ReplaceExpression                   = 1L << 04,
        ReplaceStatement                    = 1L << 05,
        RenamesLocalVariable                = 1L << 06,
        RemovesLocalVariable                = 1L << 07,
        CreatesLocalVariable                = 1L << 08,
        CreatesBinaryOpCoercion             = 1L << 09,
        ModifiesBinaryOpCoercionSignature   = 1L << 10,
        RenamesField                        = 1L << 11,
        CreatesField                        = 1L << 12,
        ManipulatesControlFlow              = 1L << 13,
        RenamesEvent                        = 1L << 14,
        ModifiesEventSignature              = 1L << 15,
        CreatesEvent                        = 1L << 16,
        DeletesEvent                        = 1L << 17,
        RenamesIndexer                      = 1L << 18,
        ModifiesIndexerSignature            = 1L << 19,
        CreatesIndexer                      = 1L << 20,
        DeletesIndexer                      = 1L << 21,
        RenamesMethod                       = 1L << 22,
        ModifiesMethodSignature             = 1L << 23,
        CreatesMethod                       = 1L << 24,
        DeletesMethod                       = 1L << 25,
        RenamesProperty                     = 1L << 26,
        CreatesProperty                     = 1L << 27,
        DeletesProperty                     = 1L << 28,
        CreatesTypeCoercion                 = 1L << 29,
        ModifiesTypeCoercionSignature       = 1L << 30,
        CreatesUnaryOpCoercion              = 1L << 31,
        ModifiesUnaryOpCoercionSignature    = 1L << 32,
        RenamesClass                        = 1L << 33,
        CreatesClass                        = 1L << 34,
        DeletesClass                        = 1L << 35,
        CreatesDelegate                     = 1L << 36,
        RenamesDelegate                     = 1L << 37,
        DeletesDelegate                     = 1L << 38,
        CreatesEnum                         = 1L << 39,
        RenamesEnum                         = 1L << 40,
        DeletesEnum                         = 1L << 41,
        CreatesInterface                    = 1L << 42,
        RenamesInterface                    = 1L << 43,
        DeletesInterface                    = 1L << 44,
        CreatesStruct                       = 1L << 45,
        RenamesStruct                       = 1L << 46,
        DeletesStruct                       = 1L << 47,
        CreatesNamespace                    = 1L << 48,
        RenamesNamespace                    = 1L << 49,
        DeletesNamespace                    = 1L << 50,
        CreatesPrivateImplementationDetail  = 1L << 51,
        RenamesAssembly                     = 1L << 52,
        AddAssemblyReference                = 1L << 53,
        RemoveAssemblyReference             = 1L << 54,
        ModifiesScopeCoercion               = 1L << 55,
        IntroducesScopeCoercion             = 1L << 56,
        DeletesScopeCoercion                = 1L << 57,
        DeletesExpression                   = 1L << 58,
        DeletesStatement                    = 1L << 59,

    }

    public class TransformationContext
    {
        public Stack<TransformationPathNode> CurrentPath { get; private set; }
        public TransformationContext()
        {
            this.CurrentPath = new Stack<TransformationPathNode>();
        }
    }
    public class TransformationPathNode
    {
        /// <summary>Returns the <see cref="object"/> which denotes the current node.</summary>
        public object Element { get; private set; }
        public TransformationPathNodeType Type { get; private set; }

        public TransformationPathNode(object element, TransformationPathNodeType type) { this.Element = element; this.Type = type; }
    }

    public enum TransformationPathNodeType
    {
        Expression,
        LinqBody,
        LinqClause,
        Statement,
        ScopeCoercion,
        Class,
        Delegate,
        Enum,
        Interface,
        Struct,
        NamespaceDeclaration,
        Assembly,
    }
}
