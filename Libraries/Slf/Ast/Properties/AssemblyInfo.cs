using AllenCopeland.Abstraction;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Transformation;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Abstract Syntax Tree core for the Static Language Framework")]
[assembly: AssemblyDescription("The Abstract Syntax Tree for the Abstraction Project")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("None")]
[assembly: AssemblyProduct("Abstraction - Static Language Framework")]
[assembly: AssemblyCopyright("Copyright © Allen Copeland Jr. 2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: CLSCompliant(false)]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

// [assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("54677673-3bcc-41cb-9c5e-4e809f55caee")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("0.5.0.0")]
[assembly: AssemblyFileVersion("0.5.0.0")]
#if DEBUG
#region IntermediateTreeTransformer
[assembly: VisitorImplementationTarget(
    "IntermediateTreeTransformer",
    ContextualVisitor   = true,
    Namespace           = "AllenCopeland.Abstraction.Slf.Transformation")]
[assembly: VisitorImplementationInheritance(
    "IntermediateTreeTransformer",
    ContextualVisitor       = true,
    YieldingVisitor         = true,
    TResult                 = typeof(TransformationImpact),
    VisitorBuilderHandler   = typeof(IntermediateCodeTransformerCodeImpactHandler),
    VisitRefactorName       = "CalculateRefactorImpact",
    InheritedVisitors       = new []
    {
        "ExpressionVisitor",
        "LinqBodyVisitor",
        "LinqClauseVisitor",
        "PrimitiveVisitor",
        "ScopeCoercionVisitor",
        "StatementVisitor",
        "IntermediateDeclarationVisitor",
        "IntermediateMemberVisitor",
        "IntermediateTypeVisitor",
    })]
[assembly: VisitorImplementationInheritance("IntermediateTreeTransformer",
    ContextualVisitor       = true,
    YieldingVisitor         = true,
    TResult                 = typeof(IExpression),
    VisitRefactorName       = "Transform",
    VisitorBuilderHandler   = typeof(IntermediateCodeTransformerTransformHandler),
    InheritedVisitors       = new []
    {
        "ExpressionVisitor",
    })]
[assembly: VisitorImplementationInheritance("IntermediateTreeTransformer",
    ContextualVisitor       = true,
    YieldingVisitor         = true,
    TResult                 = typeof(ILinqBody),
    VisitRefactorName       = "Transform",
    VisitorBuilderHandler   = typeof(IntermediateCodeTransformerTransformHandler),
    InheritedVisitors       = new[]
    {
        "LinqBodyVisitor",
    })]
[assembly: VisitorImplementationInheritance("IntermediateTreeTransformer",
    ContextualVisitor       = true,
    YieldingVisitor         = true,
    TResult                 = typeof(ILinqClause),
    VisitRefactorName       = "Transform",
    VisitorBuilderHandler   = typeof(IntermediateCodeTransformerTransformHandler),
    InheritedVisitors       = new[]
    {
        "LinqClauseVisitor",
    })]
[assembly: VisitorImplementationInheritance("IntermediateTreeTransformer",
    ContextualVisitor       = true,
    YieldingVisitor         = true,
    TResult                 = typeof(IPrimitiveExpression),
    VisitRefactorName       = "Transform",
    VisitorBuilderHandler   = typeof(IntermediateCodeTransformerTransformHandler),
    InheritedVisitors       = new []
    {
        "PrimitiveVisitor",
    })]
[assembly: VisitorImplementationInheritance("IntermediateTreeTransformer",
    ContextualVisitor       = true,
    YieldingVisitor         = true,
    TResult                 = typeof(IScopeCoercion),
    VisitRefactorName       = "Transform",
    VisitorBuilderHandler   = typeof(IntermediateCodeTransformerTransformHandler),
    InheritedVisitors       = new []
    {
        "ScopeCoercionVisitor",
    })]
[assembly: VisitorImplementationInheritance("IntermediateTreeTransformer",
    ContextualVisitor       = true,
    YieldingVisitor         = true,
    TResult                 = typeof(IStatement),
    VisitRefactorName       = "Transform",
    VisitorBuilderHandler   = typeof(IntermediateCodeTransformerTransformHandler),
    InheritedVisitors       = new []
    {
        "StatementVisitor",
    })]
[assembly: VisitorImplementationInheritance("IntermediateTreeTransformer",
    ContextualVisitor       = true,
    YieldingVisitor         = true,
    TResult                 = typeof(IIntermediateDeclaration),
    VisitRefactorName       = "Transform",
    VisitorBuilderHandler   = typeof(IntermediateCodeTransformerTransformHandler),
    InheritedVisitors       = new []
    {
        "IntermediateDeclarationVisitor",
    })]
[assembly: VisitorImplementationInheritance("IntermediateTreeTransformer",
    ContextualVisitor       = true,
    YieldingVisitor         = true,
    TResult                 = typeof(IIntermediateMember),
    VisitRefactorName       = "Transform",
    VisitorBuilderHandler   = typeof(IntermediateCodeTransformerTransformHandler),
    InheritedVisitors       = new []
    {
        "IntermediateMemberVisitor",
    })]
[assembly: VisitorImplementationInheritance("IntermediateTreeTransformer",
    ContextualVisitor       = true,
    YieldingVisitor         = true,
    TResult                 = typeof(IIntermediateType),
    VisitRefactorName       = "Transform",
    VisitorBuilderHandler   = typeof(IntermediateCodeTransformerTransformHandler),
    InheritedVisitors       = new []
    {
        "IntermediateTypeVisitor",
    })]
#endregion

#region IntermediateCodeTranslatorBase
[assembly: VisitorImplementationTarget(
    "IntermediateCodeTranslatorBase",
    Namespace = "AllenCopeland.Abstraction.Slf.Translation")]
[assembly: VisitorImplementationInheritance("IntermediateCodeTranslatorBase",
    VisitRefactorAbstract = true,
    VisitRefactorName = "Translate",
    InheritedVisitors = new[] { "IntermediateTreeVisitor" })]
#endregion

#region IntermediateTypeReferenceGatherer
[assembly: VisitorImplementationTarget(
    "IntermediateTypeReferenceGatherer",
    Namespace = "AllenCopeland.Abstraction.Slf.Translation")]
[assembly: VisitorImplementationActionDetail("IntermediateTypeReferenceGatherer",
    typeof(IType),
    "CheckType",
    "CheckTypes")]

[assembly: VisitorImplementationInheritance("IntermediateTypeReferenceGatherer",
    InheritedVisitors = new [] { "IntermediateTreeVisitor" },
    VisitorBuilderHandler = typeof(IntermediateTypeReferenceGathererHandler),
    VisitRefactorName = "Gather")]
#endregion

 [ assembly: InternalsVisibleTo("_abs.slf.languages.vb.net")]
  [assembly: InternalsVisibleTo("_abs.slf.languages.csharp")]
 [assembly: InternalsVisibleTo("_abs.slf.languages.oilexer")]
                    [assembly: InternalsVisibleTo("Oilexer")]
        [assembly: InternalsVisibleTo("Abstraction.BugTest")]
[assembly: InternalsVisibleTo("_abs.supplementary.cts.test")]
           [assembly: InternalsVisibleTo("_abs.slf.cli.ast")]
#else
[assembly: InternalsVisibleTo("Oilexer, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
[assembly: InternalsVisibleTo("_abs.slf.languages.csharp, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
[assembly: InternalsVisibleTo("_abs.slf.languages.oilexer, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
[assembly: InternalsVisibleTo("_abs.slf.languages.vb.net, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
[assembly: InternalsVisibleTo("Abstraction.BugTest, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
[assembly: InternalsVisibleTo("_abs.supplementary.cts.test, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
[assembly: InternalsVisibleTo("_abs.slf.cli.ast, PublicKey=00240000048000009400000006020000002400005253413100040000010001009982a9fd0d9f7efc18f1da44cf7a85d43b20fd87abb0e46719ec4bdc1b41acb42ca2c032d667030f4b0ba8db26c3b8952d776743b5f1c23d4b956ccbd80d3200b25c611f4300ad09d361c12ef2801ac5f731c63a2248474cc17c5de83572d8bcd5240e925ac8cf391b2b6cdd18c73ab922ff5ea1871cdcd0917a60b88606a996")]
#endif
[assembly: ComVisible(false)]
