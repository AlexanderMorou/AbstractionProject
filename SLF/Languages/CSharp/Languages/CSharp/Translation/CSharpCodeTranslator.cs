using System;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Lambda;
using AllenCopeland.Abstraction.Slf.Ast.Expressions.Linq;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System.Collections.Generic;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Slf.Languages;
using AllenCopeland.Abstraction.Numerics;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Translation;
using AllenCopeland.Abstraction.Utilities.Arrays;
/*---------------------------------------------------------------------\
| Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Translation
{
    public class CSharpCodeTranslator :
        IntermediateCodeTranslatorBase
    {
        private static RuntimeCoreType[] autoformTypes = new[] {
                                                    RuntimeCoreType.Byte, 
                                                    RuntimeCoreType.SByte,
                                                    RuntimeCoreType.UInt16,
                                                    RuntimeCoreType.Int16,
                                                    RuntimeCoreType.UInt32,
                                                    RuntimeCoreType.Int32,
                                                    RuntimeCoreType.UInt64,
                                                    RuntimeCoreType.Int64,
                                                    RuntimeCoreType.VoidType, 
                                                    RuntimeCoreType.Boolean, 
                                                    RuntimeCoreType.Char, 
                                                    RuntimeCoreType.Decimal,
                                                    RuntimeCoreType.Single, 
                                                    RuntimeCoreType.Double,
                                                    RuntimeCoreType.RootType,
                                                    RuntimeCoreType.String 
                                                };
        private IIntermediateCodeNameProvider nameProvider;
        private readonly Dictionary<string, CSharpKeywords> KeywordLookup;
        private readonly Dictionary<CSharpKeywords, string> KeywordReverseLookup;
        private BreakScannerVisitor<IIntermediateCodeTranslatorOptions> breakScanner = new BreakScannerVisitor<IIntermediateCodeTranslatorOptions>();
        private string subToolVersion;

        private static Dictionary<string, CSharpKeywords> GenerateKeywordLookup()
        {
            var result = new Dictionary<string, CSharpKeywords>();
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_02_As, CSharpKeywords.As);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_02_Do, CSharpKeywords.Do);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_02_If, CSharpKeywords.If);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_02_In, CSharpKeywords.In);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_02_Is, CSharpKeywords.Is);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_03_For, CSharpKeywords.For);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_03_Get, CSharpKeywords.Get);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_03_Int, CSharpKeywords.Int);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_03_Let, CSharpKeywords.Let);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_03_New, CSharpKeywords.New);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_03_Out, CSharpKeywords.Out);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_03_Ref, CSharpKeywords.Ref);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_03_Set, CSharpKeywords.Set);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_03_Try, CSharpKeywords.Try);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_03_Var, CSharpKeywords.Var);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_04_Base, CSharpKeywords.Base);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_04_Bool, CSharpKeywords.Bool);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_04_Byte, CSharpKeywords.Byte);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_04_Case, CSharpKeywords.Case);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_04_Char, CSharpKeywords.Char);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_04_Else, CSharpKeywords.Else);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_04_Enum, CSharpKeywords.Enum);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_04_From, CSharpKeywords.From);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_04_Goto, CSharpKeywords.Goto);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_04_Join, CSharpKeywords.Join);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_04_Lock, CSharpKeywords.Lock);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_04_Long, CSharpKeywords.Long);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_04_Null, CSharpKeywords.Null);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_04_This, CSharpKeywords.This);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_04_True, CSharpKeywords.True);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_04_Type, CSharpKeywords.Type);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_04_Uint, CSharpKeywords.Uint);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_04_Void, CSharpKeywords.Void);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Async, CSharpKeywords.Async);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Await, CSharpKeywords.Await);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Break, CSharpKeywords.Break);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Catch, CSharpKeywords.Catch);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Class, CSharpKeywords.Class);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Const, CSharpKeywords.Const);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Event, CSharpKeywords.Event);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_False, CSharpKeywords.False);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Field, CSharpKeywords.Field);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Fixed, CSharpKeywords.Fixed);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Float, CSharpKeywords.Float);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Group, CSharpKeywords.Group);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Param, CSharpKeywords.Param);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Sbyte, CSharpKeywords.Sbyte);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Short, CSharpKeywords.Short);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Throw, CSharpKeywords.Throw);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Ulong, CSharpKeywords.Ulong);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Using, CSharpKeywords.Using);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Where, CSharpKeywords.Where);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_While, CSharpKeywords.While);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_05_Yield, CSharpKeywords.Yield);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_Double, CSharpKeywords.Double);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_Equals, CSharpKeywords.Equals);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_Extern, CSharpKeywords.Extern);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_Method, CSharpKeywords.Method);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_Module, CSharpKeywords.Module);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_Object, CSharpKeywords.Object);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_Params, CSharpKeywords.Params);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_Public, CSharpKeywords.Public);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_Return, CSharpKeywords.Return);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_Sealed, CSharpKeywords.Sealed);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_Select, CSharpKeywords.Select);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_Sizeof, CSharpKeywords.Sizeof);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_Static, CSharpKeywords.Static);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_String, CSharpKeywords.String);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_Struct, CSharpKeywords.Struct);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_Switch, CSharpKeywords.Switch);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_Typeof, CSharpKeywords.Typeof);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_Unsafe, CSharpKeywords.Unsafe);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_06_Ushort, CSharpKeywords.Ushort);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_07_Checked, CSharpKeywords.Checked);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_07_Decimal, CSharpKeywords.Decimal);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_07_Default, CSharpKeywords.Default);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_07_Dynamic, CSharpKeywords.Dynamic);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_07_Finally, CSharpKeywords.Finally);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_07_Foreach, CSharpKeywords.Foreach);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_07_OrderBy, CSharpKeywords.OrderBy);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_07_Partial, CSharpKeywords.Partial);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_07_Private, CSharpKeywords.Private);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_07_Virtual, CSharpKeywords.Virtual);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_08_Abstract, CSharpKeywords.Abstract);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_08_Assembly, CSharpKeywords.Assembly);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_08_Continue, CSharpKeywords.Continue);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_08_Delegate, CSharpKeywords.Delegate);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_08_Explicit, CSharpKeywords.Explicit);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_08_Implicit, CSharpKeywords.Implicit);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_08_Internal, CSharpKeywords.Internal);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_08_Operator, CSharpKeywords.Operator);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_08_Override, CSharpKeywords.Override);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_08_Property, CSharpKeywords.Property);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_08_Readonly, CSharpKeywords.Readonly);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_08_Volatile, CSharpKeywords.Volatile);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_09___arglist, CSharpKeywords.__ArgList);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_09___makeref, CSharpKeywords.__MakeRef);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_09___reftype, CSharpKeywords.__RefType);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_09_Ascending, CSharpKeywords.Ascending);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_09_Interface, CSharpKeywords.Interface);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_09_Namespace, CSharpKeywords.Namespace);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_09_Protected, CSharpKeywords.Protected);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_09_Unchecked, CSharpKeywords.Unchecked);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_10___refvalue, CSharpKeywords.__RefValue);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_10_Descending, CSharpKeywords.Descending);
            result.Add(CSharpCodeTranslator_Resources.CSharpKeyWord_10_Stackalloc, CSharpKeywords.Stackalloc);
            return result;
        }

        public CSharpCodeTranslator(IIntermediateCodeNameProvider nameProvider = null)
        {
            this.KeywordLookup = GenerateKeywordLookup();
            this.KeywordReverseLookup = KeywordLookup.ToDictionary(k => k.Value, v => v.Key);
        }


        protected override IIntermediateCodeTranslatorOptions InitializeOptions()
        {
            return new IntermediateCodeTranslatorOptions(this.FormatProvider ?? new DefaultCodeTranslatorFormatterProvider());
        }

        public override void Translate(BinaryOperationKind kind)
        {
            this.Translate(kind, true);
        }

        private void Translate(BinaryOperationKind kind, bool space)
        {
            this.Write(" ");
            switch (kind)
            {
                case BinaryOperationKind.Assign:
                    this.WriteOperator("=");
                    break;
                case BinaryOperationKind.AssignMultiply:
                    this.WriteOperator("*=");
                    break;
                case BinaryOperationKind.AssignModulus:
                    this.WriteOperator("%=");
                    break;
                case BinaryOperationKind.AssignDivide:
                    this.WriteOperator("/=");
                    break;
                case BinaryOperationKind.AssignAdd:
                    this.WriteOperator("+=");
                    break;
                case BinaryOperationKind.AssignSubtract:
                    this.WriteOperator("-=");
                    break;
                case BinaryOperationKind.AssignLeftShift:
                    this.WriteOperator("<<=");
                    break;
                case BinaryOperationKind.AssignRightShift:
                    this.WriteOperator(">>=");
                    break;
                case BinaryOperationKind.AssignBitwiseAnd:
                    this.WriteOperator("&=");
                    break;
                case BinaryOperationKind.AssignBitwiseOr:
                    this.WriteOperator("|=");
                    break;
                case BinaryOperationKind.AssignBitwiseExclusiveOr:
                    this.WriteOperator("^=");
                    break;
                case BinaryOperationKind.LogicalOr:
                    this.WriteOperator("||");
                    break;
                case BinaryOperationKind.LogicalAnd:
                    this.WriteOperator("&&");
                    break;
                case BinaryOperationKind.BitwiseOr:
                    this.WriteOperator("|");
                    break;
                case BinaryOperationKind.BitwiseExclusiveOr:
                    this.WriteOperator("^");
                    break;
                case BinaryOperationKind.BitwiseAnd:
                    this.WriteOperator("&");
                    break;
                case BinaryOperationKind.Inequality:
                    this.WriteOperator("!=");
                    break;
                case BinaryOperationKind.Equality:
                    this.WriteOperator("==");
                    break;
                case BinaryOperationKind.LessThan:
                    this.WriteOperator("<");
                    break;
                case BinaryOperationKind.LessThanOrEqualTo:
                    this.WriteOperator("<=");
                    break;
                case BinaryOperationKind.GreaterThan:
                    this.WriteOperator(">");
                    break;
                case BinaryOperationKind.GreaterThanOrEqualTo:
                    this.WriteOperator(">=");
                    break;
                case BinaryOperationKind.TypeCheck:
                    this.WriteOperator("is");
                    break;
                case BinaryOperationKind.TypeCastOrNull:
                    this.WriteOperator("as");
                    break;
                case BinaryOperationKind.LeftShift:
                    this.WriteOperator("<<");
                    break;
                case BinaryOperationKind.RightShift:
                    this.WriteOperator(">>");
                    break;
                case BinaryOperationKind.Add:
                    this.WriteOperator("+");
                    break;
                case BinaryOperationKind.Subtract:
                    this.WriteOperator("-");
                    break;
                case BinaryOperationKind.Multiply:
                    this.WriteOperator("*");
                    break;
                case BinaryOperationKind.Modulus:
                    this.WriteOperator("%");
                    break;
                case BinaryOperationKind.StrictDivision:
                case BinaryOperationKind.IntegerDivision:
                case BinaryOperationKind.FlexibleDivision:
                    this.WriteOperator("/");
                    break;
                default:
                    return;
            }
            if (space)
                this.Write(" ");
        }

        public override void Translate(IIndexerReferenceExpression expression)
        {
            base.Translate(expression.Source);
            this.WriteOperator("[");
            this.Translate(expression.Parameters);
            this.WriteOperator("]");
        }

        public override void Translate(IConditionalExpression expression)
        {
            expression.CheckPart.Visit(this);
            if (expression.Type == ExpressionKind.ConditionalForwardTerm)
                return;
            this.WriteOperator(" ? ");
            expression.TruePart.Visit(this);
            this.WriteOperator(" : ");
            expression.FalsePart.Visit(this);
        }

        public override void Translate(ITypeCastExpression expression)
        {
            this.WriteOperator("((");
            this.Translate(expression.CastType);
            this.WriteOperator(")(");
            expression.Target.Visit(this);
            this.WriteOperator("))");
        }

        public override void Translate(ITypeOfExpression expression)
        {
            this.WriteKeyword(CSharpKeywords.Typeof);
            this.WriteOperator("(");
            this.Translate(expression.ReferenceType);
            this.WriteOperator(")");
        }

        public override void Translate(ITypeReferenceExpression expression)
        {
            this.Translate(expression.ReferenceType);
        }

        public override void Translate(IVariadicTypeCastExpression expression)
        {

        }

        public override void Translate(ICommentExpression expression)
        {
            string result = GetCSharpCommentText(expression.Comment.Replace("\t", "    "), false, true);
            string[] resultLines = result.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            bool first = true;
            foreach (string s in resultLines)
            {
                if (first)
                    first = false;
                else
                    this.DenoteNewLine();
                base.Write(IntermediateSpanTranslationClasses.Comment, s);
                
            }
        }

        public override void Translate(IDecoratingExpression expression)
        {
            if (expression == null)
                return;
            if (expression.Decorations == null || expression.Decorations.Count == 0)
                expression.ContainedExpression.Visit(this);
            else
            {
                var leftSide = ObtainDecorations(expression, DecorationDisplaySide.LeftSide);
                var rightSide = ObtainDecorations(expression, DecorationDisplaySide.RightSide);
                bool leftNewLine = leftSide.Any(k => k is INewLineExpression),
                     leftComment = leftSide.Any(k => k is ICommentExpression),
                     rightNewLine = rightSide.Any(k => k is INewLineExpression),
                     rightComment = rightSide.Any(k => k is ICommentExpression);

                if (leftSide.Length > 0)
                {
                    foreach (var element in leftSide)
                        element.Visit(this);
                    if (leftNewLine && !leftNewLine)
                        this.Write(" ");
                }
                if (leftNewLine || rightNewLine)
                    this.IncreaseIndent();

                    expression.ContainedExpression.Visit(this);
                if (leftNewLine || rightNewLine)
                    this.DecreaseIndent();
                if (rightSide.Length > 0)
                {
                    if (rightComment)
                        this.Write(" ");
                    foreach (var element in rightSide)
                        element.Visit(this);
                }
            }
        }

        private static IDecorationExpression[] ObtainDecorations(IDecoratingExpression expression, DecorationDisplaySide side)
        {
            return (from d in expression.Decorations
                    where d.Side == side
                    select d).ToArray();
        }

        public override void Translate(INewLineExpression expression)
        {
            this.DenoteNewLine();
        }

        public override void Translate(ISymbolExpression expression)
        {
            this.Write(IntermediateSpanTranslationClasses.Symbol, expression.Symbol);
        }

        public override void Translate(IStaticGetMemberHandleExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ISpecialReferenceExpression expression)
        {
            switch (expression.Kind)
            {
                case SpecialReferenceKind.Self:
                case SpecialReferenceKind.This:
                    this.WriteKeyword(CSharpKeywords.This);
                    break;
                case SpecialReferenceKind.Base:
                    this.WriteKeyword(CSharpKeywords.Base);
                    break;
            }
        }

        public override void Translate(IPropertyReferenceExpression expression)
        {
            if (expression.Source != null)
            {
                expression.Source.Visit(this);
                this.WriteOperator(".");
            }
            this.Write(IntermediateSpanTranslationClasses.PropertyReference, expression.Name);
        }

        public override void Translate(IParenthesizedExpression expression)
        {
            this.WriteOperator("(");
            expression.Parenthesized.Visit(this);
            this.WriteOperator(")");
        }

        public override void Translate(INamedParameterExpression expression)
        {
            this.Write(IntermediateSpanTranslationClasses.ParameterReference, expression.Name);
            this.WriteOperator(": ");
            expression.Expression.Visit(this);
        }

        public override void Translate<TSignatureParameter, TSignature, TParent>(IMethodReferenceStub<TSignatureParameter, TSignature, TParent> expressionSegment)
        {
            this.ReferenceDeclaration(expressionSegment.Member);
            this.Write(IntermediateSpanTranslationClasses.MethodReference, expressionSegment.Name);
            this.EndReferenceDeclaration();
            if (expressionSegment.GenericParameters != null && expressionSegment.GenericParameters.Count > 0 && expressionSegment.Member.IsGenericConstruct)
            {
                this.WriteOperator("<");
                bool first = true;
                foreach (var gParam in expressionSegment.GenericParameters)
                {
                    if (first)
                        first = false;
                    else
                        this.WriteOperator(", ");
                    this.Translate(gParam);
                }
                this.WriteOperator(">");
            }
        }

        public override void Translate(IMethodReferenceStub expressionSegment)
        {
            this.Write(IntermediateSpanTranslationClasses.MethodReference, expressionSegment.Name);
            if (expressionSegment.GenericParameters != null && expressionSegment.GenericParameters.Count > 0)
            {
                this.WriteOperator("<");
                bool first = true;
                foreach (var gParam in expressionSegment.GenericParameters)
                {
                    if (first)
                        first = false;
                    else
                        this.WriteOperator(", ");
                    this.Translate(gParam);
                }
                this.WriteOperator(">");
            }
        }

        public override void Translate(IMethodPointerReferenceExpression expression)
        {
            if (expression.Reference != null)
            {
                if (expression.Reference.Source != null)
                {
                    expression.Reference.Source.Visit(this);
                    this.WriteOperator(".");
                }
                expression.Reference.Visit(this);
                //this.Write(IntermediateSpanTranslationClasses.MethodReference, expression.Reference.Name);
            }
        }

        public override void Translate(IMethodInvokeExpression expression)
        {
            expression.Reference.Visit(this);
            this.WriteOperator("(");
            this.Translate(expression.Parameters);
            this.WriteOperator(")");
        }

        public override void Translate(IBoundLocalReferenceExpression expression)
        {
            this.ReferenceDeclaration(expression.Member);
            this.Write(IntermediateSpanTranslationClasses.LocalReference, expression.Name);
            this.EndReferenceDeclaration();
        }

        public override void Translate(ILocalReferenceExpression expression)
        {
            this.Write(IntermediateSpanTranslationClasses.LocalReference, expression.Name);
        }

        public override void Translate(IFieldReferenceExpression expression)
        {
            if (expression.Source != null)
            {
                expression.Source.Visit(this);
                this.WriteOperator(".");
            }

            this.Write(IntermediateSpanTranslationClasses.FieldReference, expression.Name);
        }

        public override void Translate(IExpressionToCommaTypeReferenceFusionExpression expression)
        {
            expression.Left.Visit(this);
            this.WriteOperator("<");
            this.Translate(expression.Right);
            this.WriteOperator(">");
        }

        public override void Translate(IExpressionToCommaFusionExpression expression)
        {
            expression.Left.Visit(this);
            this.WriteOperator("(");
            this.Translate(expression.Right);
            this.WriteOperator(")");
        }

        public override void Translate(IExpressionFusionExpression expression)
        {
            expression.Left.Visit(this);
            this.WriteOperator(".");
            expression.Right.Visit(this);
        }

        public override void Translate(IEventInvokeExpression expression)
        {
            expression.Reference.Visit(this);
            this.WriteOperator("(");
            this.Translate(expression.Parameters);
            this.WriteOperator(")");
        }

        public override void Translate(IDirectionExpression expression)
        {
            switch (expression.Direction)
            {
                case ParameterCoercionDirection.Out:
                    this.WriteKeyword(CSharpKeywords.Out);
                    this.Write(" ");
                    break;
                case ParameterCoercionDirection.Reference:
                    this.WriteKeyword(CSharpKeywords.Ref);
                    this.Write(" ");
                    break;
                default:
                    break;
            }
            expression.Directed.Visit(this);
        }

        public override void Translate(IDelegateReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IDelegateMethodPointerReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IDelegateInvokeExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IDelegateHolderReferenceExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TProperty, TPropertyParent>(ICreateInstancePropertyAssignment<TProperty, TPropertyParent> expression)
        {
            this.Write(IntermediateSpanTranslationClasses.PropertyReference, expression.Name);
            this.WriteOperator(" = ");
            expression.AssignValue.Visit(this);
        }

        public override void Translate<TField, TFieldParent>(ICreateInstanceFieldAssignment<TField, TFieldParent> expression)
        {
            this.Write(IntermediateSpanTranslationClasses.FieldReference, expression.Name);
            this.WriteOperator(" = ");
            expression.AssignValue.Visit(this);
        }

        public override void Translate(ICreateInstanceUnboundMemberAssignment expression)
        {
            this.Write(IntermediateSpanTranslationClasses.Symbol, expression.Name);
            this.WriteOperator(" = ");
            expression.AssignValue.Visit(this);
        }

        public override void Translate(ICreateInstanceExpression expression)
        {
            this.Translate((IConstructorInvokeExpression)expression);
            this.WriteOperator("{");
            bool first = true;
            foreach (var assignment in expression.MemberAssignments.Values)
            {
                if (first)
                    first = false;
                else
                    this.WriteOperator(", ");
                assignment.Visit(this);
            }
            this.WriteOperator("}");
        }

        public override void Translate(ICreateArrayExpression expression)
        {
            this.WriteKeyword(CSharpKeywords.New);
            this.Write(" ");
            this.Translate(expression.ArrayType);
            Stack<int> ranks = new Stack<int>();
            for (IType current = expression.ArrayType; current.ElementClassification == TypeElementClassification.Array && current is IArrayType; current = current.ElementType)
            {
                IArrayType currentArray = (IArrayType)current;
                ranks.Push(currentArray.ArrayRank);
            }
            this.WriteOperator("[");
            if (expression.Sizes.Count > 0)
                this.Translate(expression.Sizes);
            this.WriteOperator("]");

            while (ranks.Count > 0)
            {
                var rank = ranks.Pop();
                this.WriteOperator("[");
                this.Write(IntermediateSpanTranslationClasses.Operator, ','.Repeat(rank - 1));
                this.WriteOperator("]");
            }
        }

        public override void Translate(ICreateArrayNestedDetailExpression expression)
        {
            this.WriteOperator("{");
            this.DenoteNewLine();
            this.Translate(expression.Details);
            this.WriteOperator("}");
        }

        public override void Translate(ICreateArrayDetailExpression expression)
        {
            this.Translate((ICreateArrayExpression)expression);
            var detailsPart = ((ICreateArrayNestedDetailExpression)(expression));
            if (detailsPart.Details.Count > 0)
                this.Translate(detailsPart);
        }

        public override void Translate(ICommaExpression expression)
        {
            throw new NotImplementedException();
        }

        public override void Translate(IAnonymousMethodWithParametersExpression expression)
        {
            this.WriteKeyword(CSharpKeywords.Delegate);
            this.Write(" ");
            TranslateHeaderInternal(expression);
            TranslateBodyInternal(expression);
        }

        public override void Translate(IAnonymousMethodExpression expression)
        {
            this.WriteKeyword(CSharpKeywords.Delegate);
            this.Write(" ");
            this.DenoteNewLine();
            this.Translate((IBlockStatement)(expression));
        }

        public override void Translate(ILambdaTypedStatementExpression expression)
        {
            TranslateHeaderInternal(expression);
            this.WriteOperator(" =>");
            TranslateBodyInternal(expression);
        }

        public override void Translate(ILambdaTypeInferredStatementExpression expression)
        {
            TranslateHeaderInternal(expression);
            this.WriteOperator(" =>");
            TranslateBodyInternal(expression);
        }

        public override void Translate(ILambdaTypedSimpleExpression expression)
        {
            TranslateHeaderInternal(expression);
            this.WriteOperator(" => ");
            TranslateBodyInternal(expression);
        }

        public override void Translate(ILambdaTypeInferredSimpleExpression expression)
        {
            TranslateHeaderInternal(expression);
            this.WriteOperator(" => ");
            TranslateBodyInternal(expression);
        }

        private void TranslateHeaderInternal(ILambdaTypedExpression expression)
        {
            this.WriteOperator("(");
            this.Translate(expression.Parameters);
            this.WriteOperator(")");
        }

        private void TranslateHeaderInternal(ILambdaTypeInferredExpression expression)
        {
            if (expression.Parameters.Count == 1)
            {
                var onlyParam = expression.Parameters.Values[0];
                base.DefineDeclaration(onlyParam);
                this.Write(IntermediateSpanTranslationClasses.ParameterReference, onlyParam.Name);
                base.EndDeclarationDefinition();
            }
            else
            {
                this.WriteOperator("(");
                bool first = true;
                foreach (var parameter in expression.Parameters.Values)
                {
                    if (first)
                        first = false;
                    else
                        this.WriteOperator(", ");
                    base.DefineDeclaration(parameter);
                    this.Write(IntermediateSpanTranslationClasses.ParameterReference, parameter.Name);
                    base.EndDeclarationDefinition();
                }
                this.WriteOperator(")");
            }
        }

        private void TranslateBodyInternal(ILambdaSimpleExpression expression)
        {
            this.Write(" ");
            expression.Block.Expression.Visit(this);
        }

        private void TranslateBodyInternal(ILambdaStatementExpression expression)
        {
            this.DenoteNewLine();
            this.WriteOperator("{");
            this.DenoteNewLine();
            this.IncreaseIndent();
            this.Translate((IControlledCollection<IStatement>)expression);
            this.DecreaseIndent();
            this.WriteOperator("}");
        }

        public override void Translate(IParameterReferenceExpression expression)
        {
            this.Write(IntermediateSpanTranslationClasses.ParameterReference, expression.Name);
        }

        public override void Translate<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter>(IParameterReferenceExpression<TParameterParent, TIntermediateParameterParent, TParameter, TIntermediateParameter> expression)
        {
            this.ReferenceDeclaration(expression.ReferenceTarget);
            this.Write(IntermediateSpanTranslationClasses.ParameterReference, expression.Name);
            this.EndReferenceDeclaration();
        }

        public override void Translate(IConstructorInvokeExpression expression)
        {
            this.WriteKeyword(CSharpKeywords.New);
            this.Write(" ");
            expression.Reference.Visit(this);
            this.WriteOperator("(");
            bool first = true;
            foreach (var param in expression.Parameters)
            {
                if (first)
                    first = false;
                else
                    this.WriteOperator(", ");
                param.Visit(this);
            }
            this.WriteOperator(")");
        }

        private void WriteKeyword(CSharpKeywords keyword)
        {
            this.Write(IntermediateSpanTranslationClasses.Keyword, KeywordReverseLookup[keyword]);
        }

        public override void Translate(IConstructorPointerReferenceExpression ctorPointerReference)
        {
            this.Translate(ctorPointerReference.Reference.InstanceType);
        }

        public override void Translate(ILinqExpression expression)
        {
            this.DenoteNewLine();
            this.IncreaseIndent();
            expression.From.Visit(this);
            foreach (var clause in expression.Body.Clauses)
                clause.Visit(this);
            this.DenoteNewLine();
            expression.Body.Visit(this);
            this.DecreaseIndent();
        }

        public override void Translate(IAssignmentExpression expression)
        {
            if (expression.Operation == AssignmentOperation.Term)
            {
                if (expression.Associativity == BinaryOperationAssociativity.Left)
                    expression.RightSide.Visit(this);
                else
                    expression.LeftSide.Visit(this);
            }
            else
            {
                expression.LeftSide.Visit(this);
                this.Translate(expression.OperationKind);
                expression.RightSide.Visit(this);
            }
        }

        public override void Translate(ILinqRangeVariableReference expression)
        {
            this.ReferenceDeclaration(expression.Target);
            this.Write(IntermediateSpanTranslationClasses.LocalReference, expression.Target.Name);
            this.EndReferenceDeclaration();
        }

        public override void Translate(IEventReferenceExpression expression)
        {
            this.Write(IntermediateSpanTranslationClasses.LocalReference, expression.Name);
        }

        public override void Translate<TEvent, TEventParameter, TEventParent>(IEventReferenceExpression<TEvent, TEventParameter, TEventParent> expression)
        {
            this.ReferenceDeclaration(expression.Member);
            this.Write(IntermediateSpanTranslationClasses.LocalReference, expression.Name);
            this.EndReferenceDeclaration();
        }

        public override void Translate(ILinqSelectBody expression)
        {
            this.WriteKeyword(CSharpKeywords.Select);
            this.Write(" ");
            expression.Selection.Visit(this);
        }

        public override void Translate(ILinqGroupBody expression)
        {
            this.WriteKeyword(CSharpKeywords.Group);
            this.Write(" ");
            expression.Selection.Visit(this);
            this.WriteKeyword(" by ");
            expression.Key.Visit(this);
        }

        public override void Translate(ILinqFusionSelectBody expression)
        {
            this.WriteKeyword(CSharpKeywords.Select);
            this.Write(" ");
            expression.Selection.Visit(this);
            this.WriteKeyword(" into ");
            expression.Target.Visit(this);
            this.DenoteNewLine();
        }

        public override void Translate(ILinqFusionGroupBody expression)
        {
            this.WriteKeyword(CSharpKeywords.Group);
            this.Write(" ");
            expression.Selection.Visit(this);
            this.WriteKeyword(" by ");
            expression.Key.Visit(this);
            this.WriteKeyword(" into ");
            expression.Target.Visit(this);
            this.DenoteNewLine();
        }

        private void TranslateFrom(ILinqRangeVariable rangeVariable, IExpression rangeSource)
        {
            this.WriteKeyword(CSharpKeywords.From);
            this.Write(" ");
            rangeVariable.Visit(this);
            this.WriteKeyword(" in ");
            rangeSource.Visit(this);
            this.DenoteNewLine();
        }

        public override void Translate(ILinqFromClause linqClause)
        {
            this.TranslateFrom(linqClause.RangeVariable, linqClause.RangeSource);
        }

        public override void Translate(ILinqJoinClause linqClause)
        {
            this.TranslateJoin(linqClause.RangeVariable, linqClause.RangeSource, linqClause.LeftSelector, linqClause.RightSelector);
        }

        private void TranslateJoin(ILinqRangeVariable rangeVariable, IExpression rangeSource, IExpression leftSelector, IExpression rightSelector)
        {
            this.WriteKeyword(CSharpKeywords.Join);
            this.Write(" ");
            rangeVariable.Visit(this);
            this.WriteKeyword(" in ");
            rangeSource.Visit(this);
            this.WriteKeyword(" on ");
            leftSelector.Visit(this);
            this.WriteKeyword(" equals ");
            rightSelector.Visit(this);
            this.DenoteNewLine();
        }

        public override void Translate(ILinqLetClause linqClause)
        {
            this.WriteKeyword(CSharpKeywords.Let);
            this.Write(" ");
            linqClause.RangeVariable.Visit(this);
            this.WriteOperator(" = ");
            linqClause.RangeSource.Visit(this);
        }

        public override void Translate(ILinqOrderByClause linqClause)
        {
            this.WriteKeyword(CSharpKeywords.OrderBy);
            this.Write(" ");
            bool first = true;
            foreach (var ordering in linqClause.Orderings)
            {
                if (first)
                    first = false;
                else
                {
                    this.Write(", ");
                    this.DenoteNewLine();
                }
                ordering.OrderingKey.Visit(this);
                switch (ordering.Direction)
                {
                    case LinqOrderByDirection.Ascending:
                        this.WriteKeyword(" ascending");
                        break;
                    case LinqOrderByDirection.Descending:
                        this.WriteKeyword(" descending");
                        break;
                }
            }
        }

        public override void Translate(ILinqTypedFromClause linqClause)
        {
            this.TranslateFrom(linqClause.RangeVariable, linqClause.RangeSource);
        }

        public override void Translate(ILinqTypedJoinClause linqClause)
        {
            this.TranslateJoin(linqClause.RangeVariable, linqClause.RangeSource, linqClause.LeftSelector, linqClause.RightSelector);
        }

        public override void Translate(ILinqWhereClause linqClause)
        {
            if (linqClause == null)
                return;
            this.WriteKeyword(CSharpKeywords.Where);
            this.Write(" ");
            if (linqClause.Condition != null)
                linqClause.Condition.Visit(this);
        }

        public override void Translate(IPrimitiveExpression<bool> expression)
        {
            if (expression.Value)
                this.WriteKeyword(CSharpKeywords.True);
            else
                this.WriteKeyword(CSharpKeywords.False);
        }

        public override void Translate(IPrimitiveExpression<char> expression)
        {
            this.Write(IntermediateSpanTranslationClasses.LiteralCharacter, IntermediateGateway.EscapeStringOrCharCILAndCS(expression.Value.ToString(), false));
        }

        public override void Translate(IPrimitiveExpression<string> expression)
        {
            if (expression == null)
                return;
            this.Write(IntermediateSpanTranslationClasses.LiteralString, IntermediateGateway.EscapeStringOrCharCILAndCS(expression.Value.ToString(), true));
        }

        public override void Translate(IPrimitiveExpression<byte> expression)
        {
            this.Write(IntermediateSpanTranslationClasses.LiteralNumber, string.Format("{0}", expression.Value.ToString()));
        }

        public override void Translate(IPrimitiveExpression<sbyte> expression)
        {
            this.Write(IntermediateSpanTranslationClasses.LiteralNumber, string.Format("{0}", expression.Value.ToString()));
        }

        public override void Translate(IPrimitiveExpression<ushort> expression)
        {
            this.Write(IntermediateSpanTranslationClasses.LiteralNumber, string.Format("{0}", expression.Value.ToString()));
        }

        public override void Translate(IPrimitiveExpression<short> expression)
        {
            this.Write(IntermediateSpanTranslationClasses.LiteralNumber, string.Format("{0}", expression.Value.ToString()));
        }

        public override void Translate(IPrimitiveExpression<uint> expression)
        {
            this.Write(IntermediateSpanTranslationClasses.LiteralNumber, string.Format("{0}U", expression.Value.ToString()));
        }

        public override void Translate(IPrimitiveExpression<int> expression)
        {
            this.Write(IntermediateSpanTranslationClasses.LiteralNumber, expression.Value.ToString());
        }

        public override void Translate(IPrimitiveExpression<ulong> expression)
        {
            this.Write(IntermediateSpanTranslationClasses.LiteralNumber, string.Format("{0}UL", expression.Value.ToString()));
        }

        public override void Translate(IPrimitiveExpression<long> expression)
        {
            this.Write(IntermediateSpanTranslationClasses.LiteralNumber, string.Format("{0}L", expression.Value.ToString()));
        }

        public override void Translate(IPrimitiveExpression<float> expression)
        {
            this.Write(IntermediateSpanTranslationClasses.LiteralNumber, string.Format("{0}F", expression.Value.ToString()));
        }

        public override void Translate(IPrimitiveExpression<double> expression)
        {
            this.Write(IntermediateSpanTranslationClasses.LiteralNumber, string.Format("{0}D", expression.Value.ToString()));
        }

        public override void Translate(IPrimitiveExpression<decimal> expression)
        {
            this.Write(IntermediateSpanTranslationClasses.LiteralNumber, string.Format("{0}M", expression.Value.ToString()));
        }

        public override void TranslateNull()
        {
            this.WriteKeyword(CSharpKeywords.Null);
        }

        public override void Translate(IBlockStatement statement)
        {
            TranslateBlockInternal(statement);
        }

        private void TranslateBlockInternal(IBlockStatementParent statement, bool canOmitBraces = false, bool mustOmitBraces = false, bool increaseIndent = true)
        {
            if (!(canOmitBraces && statement.Count == 1) && !mustOmitBraces)
            {
                this.WriteOperator("{");
                this.DenoteNewLine();
            }
            if (increaseIndent)
                this.IncreaseIndent();
            foreach (var local in from local in statement.Locals.Values
                                  where local.AutoDeclare
                                  select local)
                local.GetDeclarationStatement().Visit(this);
            base.Translate((IControlledCollection<IStatement>)statement);
            if (increaseIndent)
                this.DecreaseIndent();
            if (!(canOmitBraces && statement.Count == 1) && !mustOmitBraces)
            {
                if (statement.Count > 0 && statement[statement.Count - 1] is ILabelStatement)
                {
                    this.IncreaseIndent();
                    this.WriteOperator(";");
                    this.DecreaseIndent();
                    this.DenoteNewLine();
                }
                this.WriteOperator("}");
                this.DenoteNewLine();
            }
        }

        public override void Translate(IBreakStatement statement)
        {
            this.WriteKeyword(CSharpKeywords.Break);
            this.WriteOperator(";");
            this.DenoteNewLine();
        }

        public override void Translate(ICallMethodStatement statement)
        {
            statement.Target.Visit(this);
            this.WriteOperator(";");
            this.DenoteNewLine();
        }

        public override void Translate(IConditionBlockStatement statement)
        {
            this.WriteKeyword(CSharpKeywords.If);
            this.Write(" ");
            this.WriteOperator("(");
            statement.Condition.Visit(this);
            this.WriteOperator(")");
            this.DenoteNewLine();
            this.TranslateBlockInternal(statement, true);
            if (statement.HasNext)
            {
                if (statement.Next is IConditionBlockStatement)
                {
                    this.WriteKeyword(CSharpKeywords.Else);
                    this.Write(" ");
                }
                statement.Next.Visit(this);
            }
        }

        public override void Translate(ICallFusionStatement statement)
        {
            statement.Target.Visit(this);
            this.WriteOperator(";");
            this.DenoteNewLine();
        }

        public override void Translate(IConditionContinuationStatement statement)
        {
            if (statement.Count == 0)
                return;
            this.WriteKeyword(CSharpKeywords.Else);
            bool skipIndent = statement.Count == 1 && statement[0] is IConditionBlockStatement;
            if (statement.Count != 1 || !skipIndent)
                this.DenoteNewLine();
            else if (skipIndent)
                this.Write(" ");
            this.TranslateBlockInternal(statement, true, increaseIndent: !skipIndent);
        }

        public override void Translate(IEnumerateSetBreakableBlockStatement statement)
        {
            if (statement == null || statement.Source == null || statement.Local == null)
                return;
            this.WriteKeyword(CSharpKeywords.Foreach);
            this.Write(" ");
            this.WriteOperator("(");
            var initExp = statement.Local.InitializationExpression;
            statement.Local.InitializationExpression = null;
            TranslateLocalDeclarationInternal(statement.Local.GetDeclarationStatement(), false);
            statement.Local.InitializationExpression = initExp;
            this.WriteKeyword(" in ");
            statement.Source.Visit(this);
            this.WriteOperator(")");
            this.DenoteNewLine();
            this.TranslateBlockInternal(statement, true);
        }

        public override void Translate(IExplicitlyTypedLocalVariableDeclarationStatement statement)
        {
            this.TranslateLocalDeclarationInternal(statement);
        }

        public override void Translate(IExpressionStatement statement)
        {
            if (statement.Expression != null)
                statement.Expression.Visit(this);
            this.WriteOperator(";");
            this.DenoteNewLine();
        }

        public override void Translate(IGoToCaseStatement statement)
        {
            if (statement == null || statement.Target == null)
                return;
            if (statement.Target.IsDefault)
            {
                this.WriteKeyword(CSharpKeywords.Goto);
                this.Write(" ");
                this.WriteKeyword(CSharpKeywords.Default);
                this.WriteOperator(";");
            }
            else
            {
                this.WriteKeyword(CSharpKeywords.Goto);
                this.Write(" ");
                this.WriteKeyword(CSharpKeywords.Case);
                this.Write(" ");
                var firstCase = statement.Target.Cases.FirstOrDefault();
                firstCase.Visit(this);
                this.WriteOperator(";");
            }
            this.DenoteNewLine();
        }

        public override void Translate(IGoToStatement statement)
        {
            this.WriteKeyword(CSharpKeywords.Goto);
            this.Write(" ");
            this.Write(IntermediateSpanTranslationClasses.Label, statement.Target.Name);
            this.WriteOperator(";");
            this.DenoteNewLine();
        }

        public override void Translate(IJumpTarget statement)
        {
        }

        public override void Translate(IIterationDeclarationBlockStatement statement)
        {
            if (statement == null || statement.Condition == null ||
                statement.LocalDeclaration == null || statement.Iterations == null)
                return;
            this.WriteKeyword(CSharpKeywords.For);
            this.Write(" ");
            this.WriteOperator("(");
            this.TranslateLocalDeclarationInternal(statement.LocalDeclaration, false);
            this.WriteOperator("; ");
            statement.Condition.Visit(this);
            this.WriteOperator("; ");
            this.Translate(statement.Iterations);
            this.WriteOperator(")");
            this.DenoteNewLine();
            this.TranslateBlockInternal(statement, true);
        }

        public override void Translate(IIterationBlockStatement statement)
        {
            if (statement == null || statement.Condition == null ||
                statement.Initializers == null || statement.Iterations == null)
                return;
            this.WriteKeyword(CSharpKeywords.For);
            this.Write(" ");
            this.WriteOperator("(");
            this.Translate(statement.Initializers);
            this.WriteOperator("; ");
            statement.Condition.Visit(this);
            this.WriteOperator("; ");
            this.Translate(statement.Iterations);
            this.WriteOperator(")");
            this.DenoteNewLine();
            this.TranslateBlockInternal(statement, true);
        }

        public override void Translate(IJumpStatement statement)
        {

        }

        public override void Translate(ILabelStatement statement)
        {
            this.DecreaseIndent();
            this.Write(IntermediateSpanTranslationClasses.Label, statement.Name);
            this.WriteOperator(":");
            this.DenoteNewLine();
            this.IncreaseIndent();
        }

        public override void Translate(IReturnStatement statement)
        {
            this.WriteKeyword(CSharpKeywords.Return);
            if (statement.ReturnValue != null)
            {
                this.Write(" ");
                statement.ReturnValue.Visit(this);
            }
            this.WriteOperator(";");
            this.DenoteNewLine();
        }

        public override void Translate(ISimpleIterationBlockStatement statement)
        {
            if (statement == null || statement.Target == null ||
                statement.Start == null || statement.End == null)
                return;
            this.WriteKeyword(CSharpKeywords.For);
            this.Write(" ");
            this.WriteOperator("(");
            bool first = true;
            var firstDecl = statement.Target.DeclaredLocals.FirstOrDefault();
            if (firstDecl == null)
                this.Translate(new CommentExpression("No declaration defined.", DecorationDisplaySide.RightSide));
            else if (statement.Target.DeclaredLocals.Count > 1)
                this.Translate(new CommentExpression("ISimpleIterationBlockStatement had more than one declaration, remainder ignored!", DecorationDisplaySide.RightSide));

            var initExp = firstDecl.InitializationExpression;
            firstDecl.InitializationExpression = null;
            if (statement.Target.DeclaredLocals.Count > 1)
                this.TranslateLocalDeclarationInternal(new LocalDeclarationsStatement(firstDecl.AsEnumerable(), null), false);
            else
                this.TranslateLocalDeclarationInternal(statement.Target, false);
            firstDecl.InitializationExpression = initExp;
            this.WriteOperator("=");
            statement.Start.Visit(this);
            this.WriteOperator("; ");
            firstDecl.Visit(this);
            this.Translate(statement.EndExclusive ? BinaryOperationKind.LessThan : BinaryOperationKind.LessThanOrEqualTo);
            this.WriteOperator(";");
            firstDecl.Visit(this);
            this.WriteOperator("++");
            this.WriteOperator(")");
            this.TranslateBlockInternal(statement, true);
            this.Translate((IBreakStatement)null);
        }

        public override void Translate(ISwitchCaseBlockStatement statement)
        {
            if (statement.Cases.Count > 1 && statement.Cases.All(k => k is IPrimitiveExpression && !(k is IPrimitiveExpression<string>)))
            {
                foreach (var caseChunk in statement.Cases.ToArray().Chunk(15))
                {
                    bool first = true;
                    foreach (var @case in caseChunk)
                    {
                        if (first)
                            first = false;
                        else
                            this.Write(" ");
                        this.WriteKeyword(CSharpKeywords.Case);
                        this.Write(" ");
                        @case.Visit(this);
                        this.WriteOperator(":");
                    }
                    this.DenoteNewLine();
                }
            }
            else
            {
                foreach (var @case in statement.Cases)
                {
                    this.WriteKeyword(CSharpKeywords.Case);
                    this.Write(" ");
                    @case.Visit(this);
                    this.WriteOperator(":");
                    this.DenoteNewLine();
                }
            }
            if (statement.IsDefault)
            {
                this.WriteKeyword(CSharpKeywords.Default);
                this.WriteOperator(":");
                this.DenoteNewLine();
            }
            this.TranslateBlockInternal(statement, false, true);
            if (!this.ScanForLogicalBreaks((IBreakableBlockStatement)statement))
            {
                this.IncreaseIndent();
                this.WriteKeyword(CSharpKeywords.Break);
                this.WriteOperator(";");
                this.WriteLine();
                this.DecreaseIndent();
            }
        }

        private bool ScanForLogicalBreaks(IBreakableBlockStatement breakableBlockStatement)
        {
            return breakableBlockStatement.Visit(breakScanner, this.Options);
        }

        public override void Translate(ISwitchStatement statement)
        {
            this.WriteKeyword(CSharpKeywords.Switch);
            this.Write(" ");
            this.WriteOperator("(");
            statement.Selection.Visit(this);
            this.WriteOperator(")");
            this.DenoteNewLine();
            this.WriteOperator("{");
            this.DenoteNewLine();
            this.IncreaseIndent();
            foreach (var @case in statement)
                @case.Visit(this);
            this.DecreaseIndent();
            this.WriteOperator("}");
            this.DenoteNewLine();
        }

        public override void Translate(ITryStatement statement)
        {
            this.WriteKeyword(CSharpKeywords.Try);
            this.DenoteNewLine();
            this.WriteOperator("{");
            this.DenoteNewLine();
            this.IncreaseIndent();
            this.Translate((IControlledCollection<IStatement>)statement);
            this.DecreaseIndent();
            this.WriteOperator("}");
            this.DenoteNewLine();
            if (statement.ClauseCount > 0)
            {
                foreach (var typedClause in ((IControlledDictionary<IType, ITypedCatchExceptionBlockStatement>)statement))
                {
                    var type = typedClause.Key;
                    var clause = typedClause.Value;
                    this.WriteKeyword(CSharpKeywords.Catch);
                    this.Write(" ");
                    this.WriteOperator("(");
                    if (clause is ITypeNamedCatchExceptionBlockStatement)
                        ((ITypeNamedCatchExceptionBlockStatement)clause).Visit(this);
                    else
                        this.Translate(type);
                    this.WriteOperator(")");
                    this.DenoteNewLine();
                    clause.Visit(this);
                }
            }
            if (statement.HasCatchAll)
            {
                this.WriteKeyword(CSharpKeywords.Catch);
                this.DenoteNewLine();
                statement.CatchAll.Visit(this);
            }
            if (statement.HasFinally)
            {
                this.WriteKeyword(CSharpKeywords.Finally);
                this.DenoteNewLine();
                statement.Finally.Visit(this);
            }
        }

        public override void Translate(ILocalDeclarationsStatement statement)
        {
            TranslateLocalDeclarationInternal(statement);
        }

        private void TranslateLocalDeclarationInternal(ILocalDeclarationsStatement statement, bool endLine = true)
        {
            var first = statement.DeclaredLocals.FirstOrDefault();
            if (first == null)
                return;
            if (statement.DeclaredLocals.Select(l => l.TypingMethod).All(tk => tk == first.TypingMethod) && statement.DeclaredLocals.Select(GetLocalType).All(t => GetLocalType(first) == t))
            {
                TranslateLocalDeclarationInternal(first, true, statement.DeclaredLocals.Count == 1 && endLine);
                foreach (var local in statement.DeclaredLocals.Skip(1))
                {
                    this.WriteOperator(", ");
                    TranslateLocalDeclarationInternal(local, false, false);
                }
                if (statement.DeclaredLocals.Count > 1 && endLine)
                {
                    this.WriteOperator(";");
                    this.DenoteNewLine();
                }
            }
            else if (!endLine)
            {
                this.TranslateLocalDeclarationInternal(first, true, false);
                this.Translate(new CommentExpression("Multiple typing methods or type encountered in local declarations statement.", DecorationDisplaySide.RightSide));
            }
            else
                foreach (var local in statement.DeclaredLocals)
                    this.TranslateLocalDeclarationInternal(first, true, true);
        }

        private static IType GetLocalType(ILocalMember l)
        {
            return ((l.TypingMethod == LocalTypingKind.Explicit) && (l is ITypedLocalMember)) ? ((ITypedLocalMember)l).LocalType : null;
        }

        private void TranslateLocalDeclarationInternal(ILocalMember member, bool emitType, bool endLine)
        {
            if (emitType)
            {
                if (member.TypingMethod == LocalTypingKind.Dynamic)
                    this.WriteKeyword(CSharpKeywords.Dynamic);
                else if (member.TypingMethod == LocalTypingKind.Implicit)
                    this.WriteKeyword(CSharpKeywords.Var);
                else
                    this.Translate(((ITypedLocalMember)member).LocalType);
                this.Write(" ");
            }
            this.DefineDeclaration(member);
            this.Write(IntermediateSpanTranslationClasses.LocalReference, member.Name);
            this.EndReferenceDeclaration();
            if (member.InitializationExpression != null)
            {
                this.WriteOperator(" = ");
                member.InitializationExpression.Visit(this);
            }
            if (endLine)
            {
                this.WriteOperator(";");
                this.DenoteNewLine();
            }
        }

        public override void Translate(IChangeEventHandlerStatement statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate(ICommentStatement statement)
        {

            string result = GetCSharpCommentText(BreakdownWrap(statement.Comment.Replace("\t", "    ")), false);
            string[] resultLines = result.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (string s in resultLines)
            {
                base.Write(IntermediateSpanTranslationClasses.Comment, s);
                this.DenoteNewLine();
            }
        }

        public override void Translate(IIntermediateAssembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");
            if (Options.AllowPartials || assembly.IsRoot)
            {
                DateTime emitStart = DateTime.Now;
                this.BuildTrailPush(assembly);
                try
                {
                    base.BeginDocument(assembly);
                    this.TranslateComment(this.GeneratedMessageText);
                    var referencesGatherer = new IntermediateTypeReferenceGatherer(this.Options.AllowPartials);
                    var gatheredTypes = referencesGatherer.GatherTypes(assembly).ToArray();
                    var namespaceNames = (from t in gatheredTypes
                                          where !t.NamespaceName.IsEmptyOrNull()
                                          select t.NamespaceName).Distinct().ToArray();
                    var scopeCoercions = assembly.ScopeCoercions.Where(k => k is INamespaceInclusionScopeCoercion).Cast<INamespaceInclusionScopeCoercion>();
                    foreach (var namespaceName in namespaceNames)
                        if (scopeCoercions.Any(k => k.Namespace == namespaceName))
                            continue;
                        else if (!referencesGatherer.ObservedNamespaces.Any(k => k.FullName == namespaceName))
                            assembly.ScopeCoercions.Add(namespaceName);
                    var tc = Options.AllowPartials ? assembly.Types.GetCountFor(assembly) : assembly.Types.Count;
                    var nc = Options.AllowPartials ? assembly.Namespaces.GetCountFor(assembly) : assembly.Namespaces.Count;
                    Translate(assembly, tc, nc, false);
                    DateTime emitEnd = DateTime.Now;
                    this.TranslateComment(string.Format("Time taken to generate: {0}", emitEnd - emitStart));
                    base.EndDocument();
                }
                finally
                {
                    this.BuildTrailPop();
                }
            }
        }

        public override void Translate(IIntermediateNamespaceDeclaration @namespace)
        {

            if (Options.AllowPartials || @namespace.IsRoot)
            {
                var tc = Options.AllowPartials ? @namespace.Types.GetCountFor(@namespace) : @namespace.Types.Count;
                var nc = Options.AllowPartials ? @namespace.Namespaces.GetCountFor(@namespace) : @namespace.Namespaces.Count;
                /* *
                 * Empty namespace
                 * */
                if ((tc == 0) &&
                    (nc == 0))
                    return;
                if (tc > 0)
                {
                    this.WriteKeyword(CSharpKeywords.Namespace);
                    this.Write(" ");

                    var namespacePath = new Stack<IIntermediateNamespaceDeclaration>();
                    /* *
                     * Reduce the namespace name shown to only what's necessary.
                     * */
                    bool first = true;
                    for (IIntermediateNamespaceDeclaration current = @namespace; current != null; current = current.Parent as IIntermediateNamespaceDeclaration)
                    {
                        if (first)
                            first = false;
                        else if (BuildTrail.Contains(current))
                            break;
                        namespacePath.Push(current);
                    }
                    first = true;
                    foreach (var namespacePoint in namespacePath)
                        if (!BuildTrail.Contains(namespacePoint))
                        {
                            if (first)
                                first = false;
                            else
                                this.WriteOperator(".");
                            this.Write(IntermediateSpanTranslationClasses.Identifier, namespacePoint.Name);
                        }
                    this.DenoteNewLine();
                    this.WriteOperator("{");
                    this.DenoteNewLine();
                    this.BuildTrailPush(@namespace);
                }
                Translate(@namespace, tc, nc);
                if (tc > 0)
                {
                    this.BuildTrailPop();
                    this.WriteOperator("};");
                    this.DenoteNewLine();
                }
            }
        }

        private void Translate(IIntermediateNamespaceParent nsk, int tc, int nc, bool indent = true)
        {
            foreach (var nsInclude in nsk.ScopeCoercions)
                nsInclude.Visit(this);
            if (tc > 0 && indent)
                IncreaseIndent();
            if (nc > 0)
            {
                var childspaces = @nsk.Namespaces.ExclusivelyOnParent().ToArray();
                foreach (var childspace in childspaces)
                    childspace.Value.Visit(this);
            }
            if (tc > 0)
                Translate((IIntermediateTypeParent)@nsk);
            if (tc > 0 && indent)
                DecreaseIndent();
        }

        private void Translate(IIntermediateTypeParent typeParent)
        {
            bool alpha = (Options.ElementOrderingMethod & TranslationOrderKind.Alphabetic) == TranslationOrderKind.Alphabetic;
            bool specific = (Options.ElementOrderingMethod & TranslationOrderKind.Specific) == TranslationOrderKind.Specific;
            bool verbatim = (Options.ElementOrderingMethod & TranslationOrderKind.Verbatim) == TranslationOrderKind.Verbatim;
            if (verbatim)
            {
                foreach (var type in Options.AllowPartials ? typeParent.Types.ExclusivelyOnParent() : from q in typeParent.Types
                                                                                                      select new KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IIntermediateType>>(q.Key, new MasterDictionaryEntry<IIntermediateType>(q.Value.Subordinate, (IIntermediateType)q.Value.Entry)))
                    type.Value.Entry.Visit(this);
            }
            else if (alpha && specific)
            {
                Dictionary<DeclarationTranslationOrder, int> translationOrder = GetDistinctTranslationOrder();
                foreach (var declaration in from declaration in
                                                (from t in
                                                     Options.AllowPartials ? typeParent.Types.ExclusivelyOnParent() : from q in typeParent.Types
                                                                                                                      select new KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IIntermediateType>>(q.Key, new MasterDictionaryEntry<IIntermediateType>(q.Value.Subordinate, (IIntermediateType)q.Value.Entry))
                                                 select t.Value.Entry).ToArray()
                                            orderby translationOrder[GetEntityOrder(declaration)],
                                                    declaration.Name
                                            select declaration)
                    declaration.Visit(this);
            }
            else if (alpha)
            {
                foreach (var declaration in from declaration in
                                                (from t in
                                                     Options.AllowPartials ? typeParent.Types.ExclusivelyOnParent() : from q in typeParent.Types
                                                                                                                      select new KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IIntermediateType>>(q.Key, new MasterDictionaryEntry<IIntermediateType>(q.Value.Subordinate, (IIntermediateType)q.Value.Entry))
                                                 select t.Value.Entry).ToArray()
                                            orderby declaration.Name
                                            select declaration)
                    declaration.Visit(this);
            }
            else
            {
                Dictionary<DeclarationTranslationOrder, int> translationOrder = GetDistinctTranslationOrder();
                foreach (var declaration in from declaration in
                                                (from t in
                                                     Options.AllowPartials ? typeParent.Types.ExclusivelyOnParent() : from q in typeParent.Types
                                                                                                                      select new KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IIntermediateType>>(q.Key, new MasterDictionaryEntry<IIntermediateType>(q.Value.Subordinate, (IIntermediateType)q.Value.Entry))
                                                 select t.Value.Entry).ToArray()
                                            orderby translationOrder[GetEntityOrder(declaration)]
                                            select declaration)
                    declaration.Visit(this);
            }
        }

        public override void Translate(IIntermediateClassType @class)
        {
            if (Options.AllowPartials || @class.IsRoot)
            {
                var tc = Options.AllowPartials ? @class.Types.GetCountFor(@class) : @class.Types.Count;
                var mc = Options.AllowPartials ? @class.Members.GetCountFor(@class) : @class.Members.Count;
                if ((tc > 0 || mc > 0) && Options.AllowPartials || @class.IsRoot)
                {
                    BuildTrailPush(@class);
                    if (@class.IsRoot)
                    {
                        this.TranslateDocumentationComments(@class);
                        this.Translate(@class.Metadata, MetadatumTargets.Class, false);
                    }

                    this.Translate(@class.AccessLevel, @class);
                    if ((@class.SpecialModifier & SpecialClassModifier.Static) == SpecialClassModifier.Static)
                    {
                        this.WriteKeyword(CSharpKeywords.Static);
                        this.Write(" ");
                    }
                    else if ((@class.SpecialModifier & SpecialClassModifier.Abstract) == SpecialClassModifier.Abstract)
                    {
                        this.WriteKeyword(CSharpKeywords.Abstract);
                        this.Write(" ");
                    }
                    else if ((@class.SpecialModifier & SpecialClassModifier.Sealed) == SpecialClassModifier.Sealed)
                    {
                        this.WriteKeyword(CSharpKeywords.Sealed);
                        this.Write(" ");
                    }
                    if (Options.AllowPartials && (!@class.IsRoot || @class.Parts.Count > 0))
                    {
                        this.WriteKeyword(CSharpKeywords.Partial);
                        this.Write(" ");
                    }
                    this.WriteKeyword(CSharpKeywords.Class);
                    this.Write(" ");
                    this.DefineDeclaration(@class);
                    this.Write(IntermediateSpanTranslationClasses.UserType, @class.Name);
                    this.EndDeclarationDefinition();
                    if (@class.IsGenericConstruct && @class.TypeParameters.Count > 0)
                    {
                        this.WriteOperator("<");
                        bool first = true;
                        foreach (var parameter in @class.TypeParameters.Values)
                        {
                            if (first)
                                first = false;
                            else
                                this.WriteOperator(", ");
                            parameter.Visit(this);
                        }
                        this.WriteOperator(">");
                    }
                    var baseRequired = @class.BaseType != null && @class.BaseType != @class.IdentityManager.ObtainTypeReference(RuntimeCoreType.RootType);
                    var classAsIClass = (IClassType)@class;
                    if (baseRequired || classAsIClass.GetDirectlyImplementedInterfaces().Count > 0)
                    {
                        this.WriteOperator(" :");
                        this.DenoteNewLine();
                        IncreaseIndent();
                        bool first = true;
                        if (baseRequired)
                        {
                            this.Translate(@class.BaseType);
                            first = false;
                        }
                        foreach (var implInter in classAsIClass.GetDirectlyImplementedInterfaces())
                        {
                            if (first)
                                first = false;
                            else
                            {
                                this.WriteOperator(", ");
                                this.DenoteNewLine();
                            }
                            this.Translate(implInter);
                        }
                        DecreaseIndent();
                    }
                    this.DenoteNewLine();
                    if (@class.IsGenericConstruct)
                    {
                        this.IncreaseIndent();
                        var lastParam = @class.TypeParameters.Values.LastOrDefault();
                        foreach (var parameter in @class.TypeParameters.Values)
                            this.TranslateConstraints(parameter, lastParam == parameter);
                        this.DecreaseIndent();
                    }
                    this.WriteOperator("{");
                    this.DenoteNewLine();
                    IncreaseIndent();
                    bool alpha = (Options.ElementOrderingMethod & TranslationOrderKind.Alphabetic) == TranslationOrderKind.Alphabetic;
                    bool specific = (Options.ElementOrderingMethod & TranslationOrderKind.Specific) == TranslationOrderKind.Specific;
                    bool verbatim = (Options.ElementOrderingMethod & TranslationOrderKind.Verbatim) == TranslationOrderKind.Verbatim;
                    if (verbatim)
                    {
                        Translate((IIntermediateTypeParent)@class);
                        foreach (var member in Options.AllowPartials ? @class.Members.ExclusivelyOnParent(@class) : from q in @class.Members
                                                                                                                    select new KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IIntermediateMember>>(q.Key, new MasterDictionaryEntry<IIntermediateMember>(q.Value.Subordinate, (IIntermediateMember)q.Value.Entry)))
                            member.Value.Entry.Visit(this);
                    }
                    else if (alpha && specific)
                    {
                        Dictionary<DeclarationTranslationOrder, int> translationOrder = GetDistinctTranslationOrder();
                        foreach (var declaration in from declaration in
                                                        GetAllClassDeclarations(@class).ToArray()
                                                    orderby translationOrder[GetEntityOrder(declaration)],
                                                            declaration.Name
                                                    select declaration)
                            if (declaration is IIntermediateMember)
                                ((IIntermediateMember)declaration).Visit(this);
                            else if (declaration is IIntermediateType)
                                ((IIntermediateType)(declaration)).Visit(this);
                    }
                    else if (alpha)
                    {
                        foreach (var declaration in from declaration in
                                                        GetAllClassDeclarations(@class).ToArray()
                                                    orderby declaration.Name
                                                    select declaration)
                            if (declaration is IIntermediateMember)
                                ((IIntermediateMember)declaration).Visit(this);
                            else if (declaration is IIntermediateType)
                                ((IIntermediateType)(declaration)).Visit(this);
                    }
                    else
                    {
                        Dictionary<DeclarationTranslationOrder, int> translationOrder = GetDistinctTranslationOrder();
                        foreach (var declaration in from declaration in
                                                        GetAllClassDeclarations(@class).ToArray()
                                                    orderby translationOrder[GetEntityOrder(declaration)]
                                                    select declaration)
                            if (declaration is IIntermediateMember)
                                ((IIntermediateMember)declaration).Visit(this);
                            else if (declaration is IIntermediateType)
                                ((IIntermediateType)(declaration)).Visit(this);
                    }
                    DecreaseIndent();
                    this.WriteOperator("};");
                    this.DenoteNewLine();
                    BuildTrailPop();
                }
            }
        }

        private IEnumerable<IIntermediateDeclaration> GetAllStructDeclarations(IIntermediateStructType @struct)
        {
            return (from m in
                        Options.AllowPartials ? @struct.Members.ExclusivelyOnParent(@struct) : from q in @struct.Members
                                                                                               select new KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IIntermediateMember>>(q.Key, new MasterDictionaryEntry<IIntermediateMember>(q.Value.Subordinate, (IIntermediateMember)q.Value.Entry))
                    select m.Value.Entry).Concat<IIntermediateDeclaration>(
                                                                        from t in
                                                                            Options.AllowPartials ? @struct.Types.ExclusivelyOnParent() : from q in @struct.Types
                                                                                                                                          select new KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IIntermediateType>>(q.Key, new MasterDictionaryEntry<IIntermediateType>(q.Value.Subordinate, (IIntermediateType)q.Value.Entry))
                                                                        select t.Value.Entry);
        }
        private IEnumerable<IIntermediateDeclaration> GetAllClassDeclarations(IIntermediateClassType @class)
        {
            return (from m in
                        Options.AllowPartials ? @class.Members.ExclusivelyOnParent(@class) : from q in @class.Members
                                                                                             select new KeyValuePair<IGeneralMemberUniqueIdentifier, MasterDictionaryEntry<IIntermediateMember>>(q.Key, new MasterDictionaryEntry<IIntermediateMember>(q.Value.Subordinate, (IIntermediateMember)q.Value.Entry))
                    select m.Value.Entry).Concat<IIntermediateDeclaration>(
                                                                        from t in
                                                                            Options.AllowPartials ? @class.Types.ExclusivelyOnParent() : from q in @class.Types
                                                                                                                                         select new KeyValuePair<IGeneralTypeUniqueIdentifier, MasterDictionaryEntry<IIntermediateType>>(q.Key, new MasterDictionaryEntry<IIntermediateType>(q.Value.Subordinate, (IIntermediateType)q.Value.Entry))
                                                                        select t.Value.Entry);
        }

        private Dictionary<DeclarationTranslationOrder, int> GetDistinctTranslationOrder()
        {
            var distinctOrders = this.Options.TranslationOrder.Distinct().ToArray();
            var remaining = fullOrderSet.Except(distinctOrders).ToArray();
            Dictionary<DeclarationTranslationOrder, int> relativeIndex = new Dictionary<DeclarationTranslationOrder, int>();
            int index = 0;
            foreach (var distOrder in distinctOrders)
                relativeIndex.Add(distOrder, index++);
            foreach (var remainingOrder in remaining.Except(new[] { DeclarationTranslationOrder.Remaining }))
                relativeIndex.Add(remainingOrder, index);
            if (!relativeIndex.ContainsKey(DeclarationTranslationOrder.Remaining))
                relativeIndex.Add(DeclarationTranslationOrder.Remaining, ++index);
            return relativeIndex;
        }

        private DeclarationTranslationOrder GetEntityOrder(IDeclaration declaration)
        {
            if (declaration is IBinaryOperatorCoercionMember)
                return DeclarationTranslationOrder.BinaryOperatorCoercions;
            else if (declaration is IClassType)
                return DeclarationTranslationOrder.Classes;
            else if (declaration is IConstructorMember)
                return DeclarationTranslationOrder.Constructors;
            else if (declaration is IIntermediateConstructorSignatureMember)
                return DeclarationTranslationOrder.Constructors;
            else if (declaration is IDelegateType)
                return DeclarationTranslationOrder.Delegates;
            else if (declaration is IEnumType)
                return DeclarationTranslationOrder.Enums;
            else if (declaration is IEventSignatureMember)
                return DeclarationTranslationOrder.Enums;
            else if (declaration is IFieldMember)
                return DeclarationTranslationOrder.Fields;
            else if (declaration is IIndexerSignatureMember)
                return DeclarationTranslationOrder.Indexers;
            else if (declaration is IInterfaceType)
                return DeclarationTranslationOrder.Interfaces;
            else if (declaration is IMethodSignatureMember)
                return DeclarationTranslationOrder.Methods;
            else if (declaration is IPropertySignatureMember)
                return DeclarationTranslationOrder.Properties;
            else if (declaration is IStructType)
                return DeclarationTranslationOrder.Structs;
            else if (declaration is ITypeCoercionMember)
                return DeclarationTranslationOrder.TypeCoercions;
            else if (declaration is IUnaryOperatorCoercionMember)
                return DeclarationTranslationOrder.UnaryOperatorCoercions;
            else
                return DeclarationTranslationOrder.Remaining;
        }

        private void Translate(AccessLevelModifiers accessLevelModifiers, IIntermediateScopedDeclaration target)
        {
            bool allowPrivate = false;
            if (target is IType)
            {
                var type = (IType)target;
                if (type.Parent is IType)
                    allowPrivate = true;
            }
            else if (target is IMethodSignatureMember)
            {
                var method = (IMethodSignatureMember)target;
                if (method.Parent is IType)
                    allowPrivate = true;
            }
            else if (target is IFieldMember)
            {
                var field = (IFieldMember)target;
                if (field.Parent is IType)
                    allowPrivate = true;
            }
            else if (target is IPropertyMember)
            {
                var property = (IPropertyMember)target;
                if (property.Parent is IType)
                    allowPrivate = true;
            }
            else if (target is IIndexerMember)
            {
                var indexer = (IIndexerMember)target;
                if (indexer.Parent is IType)
                    allowPrivate = true;
            }
            else if (target is IEventMember)
            {
                var @event = (IEventMember)target;
                if (@event.Parent is IType)
                    allowPrivate = true;
            }
            switch (accessLevelModifiers)
            {
                case AccessLevelModifiers.ProtectedOrInternal:
                case AccessLevelModifiers.ProtectedAndInternal:
                    if (allowPrivate)
                    {
                        this.WriteKeyword(CSharpKeywords.Internal);
                        this.Write(" ");
                        this.WriteKeyword(CSharpKeywords.Protected);
                        this.Write(" ");
                    }
                    else
                        goto case AccessLevelModifiers.Internal;
                    break;
                case AccessLevelModifiers.Internal:
                    this.WriteKeyword(CSharpKeywords.Internal);
                    this.Write(" ");
                    break;
                case AccessLevelModifiers.Private:
                case AccessLevelModifiers.PrivateScope:
                    if (!allowPrivate)
                        goto case AccessLevelModifiers.Internal;
                    this.WriteKeyword(CSharpKeywords.Private);
                    this.Write(" ");
                    break;
                case AccessLevelModifiers.Public:
                    this.WriteKeyword(CSharpKeywords.Public);
                    this.Write(" ");
                    break;
                case AccessLevelModifiers.Protected:
                    if (allowPrivate)
                    {
                        this.WriteKeyword(CSharpKeywords.Protected);
                        this.Write(" ");
                    }
                    else
                        goto case AccessLevelModifiers.Internal;
                    break;
            }
        }

        public override void Translate(IIntermediateDelegateType @delegate)
        {
            this.TranslateDocumentationComments(@delegate);
            this.Translate(@delegate.Metadata, MetadatumTargets.Delegate, false);
            this.Translate(@delegate.AccessLevel, @delegate);
            this.WriteKeyword(CSharpKeywords.Delegate);
            this.Write(" ");
            this.Translate(@delegate.ReturnType);
            this.Write(" ");
            this.DefineDeclaration(@delegate);
            this.Write(IntermediateSpanTranslationClasses.UserDelegateType, @delegate.Name);
            this.EndDeclarationDefinition();
            if (@delegate.IsGenericConstruct && @delegate.TypeParameters.Count > 0)
            {
                this.WriteOperator("<");
                bool first = true;
                foreach (var parameter in @delegate.TypeParameters.Values)
                {
                    if (first)
                        first = false;
                    else
                        this.WriteOperator(", ");
                    parameter.Visit(this);
                }
                this.WriteOperator(">");
            }
            this.WriteOperator("(");
            this.Translate(@delegate.Parameters);
            this.WriteOperator(")");
            if (@delegate.IsGenericConstruct)
            {
                this.WriteLine();
                this.IncreaseIndent();
                var lastParam = @delegate.TypeParameters.Values.LastOrDefault();
                foreach (var parameter in @delegate.TypeParameters.Values)
                    this.TranslateConstraints(parameter, lastParam == parameter);
                this.DecreaseIndent();
            }
            this.WriteOperator(";");
        }

        public override void Translate(IIntermediateEnumType @enum)
        {
            this.TranslateDocumentationComments(@enum);
            this.Translate(@enum.Metadata, MetadatumTargets.Enum, false);
            this.Translate(@enum.AccessLevel, @enum);
            this.WriteKeyword(CSharpKeywords.Enum);
            this.Write(" ");
            this.DefineDeclaration(@enum);
            this.Write(IntermediateSpanTranslationClasses.UserEnumType, @enum.Name);
            this.EndDeclarationDefinition();
            switch (@enum.ValueType)
            {
                case EnumerationBaseType.Byte:
                case EnumerationBaseType.SByte:
                case EnumerationBaseType.Int16:
                case EnumerationBaseType.UInt16:
                case EnumerationBaseType.Int32:
                case EnumerationBaseType.UInt32:
                case EnumerationBaseType.Int64:
                case EnumerationBaseType.UInt64:
                    WriteOperator(" :");
                    this.DenoteNewLine();
                    IncreaseIndent();
                    switch (@enum.ValueType)
                    {
                        case EnumerationBaseType.Byte:
                            WriteKeyword(CSharpKeywords.Byte);
                            break;
                        case EnumerationBaseType.SByte:
                            WriteKeyword(CSharpKeywords.Sbyte);
                            break;
                        case EnumerationBaseType.Int16:
                            WriteKeyword(CSharpKeywords.Short);
                            break;
                        case EnumerationBaseType.UInt16:
                            WriteKeyword(CSharpKeywords.Ushort);
                            break;
                        case EnumerationBaseType.Int32:
                            WriteKeyword(CSharpKeywords.Int);
                            break;
                        case EnumerationBaseType.UInt32:
                            WriteKeyword(CSharpKeywords.Uint);
                            break;
                        case EnumerationBaseType.Int64:
                            WriteKeyword(CSharpKeywords.Long);
                            break;
                        case EnumerationBaseType.UInt64:
                            WriteKeyword(CSharpKeywords.Ulong);
                            break;
                    }
                    DecreaseIndent();
                    break;
            }
            this.DenoteNewLine();
            this.WriteOperator("{");
            this.DenoteNewLine();
            this.IncreaseIndent();
            bool first = true;
            foreach (var field in @enum.Fields.Values)
            {
                if (first)
                    first = false;
                else
                {
                    this.WriteOperator(",");
                    this.DenoteNewLine();
                }
                field.Visit(this);
            }
            this.DecreaseIndent();
            this.DenoteNewLine();
            this.WriteOperator("};");
            this.DenoteNewLine();
        }

        public override void Translate(IIntermediateInterfaceType @interface)
        {
            if (Options.AllowPartials || @interface.IsRoot)
            {
                var tc = Options.AllowPartials ? @interface.Types.GetCountFor(@interface) : @interface.Types.Count;
                var mc = Options.AllowPartials ? @interface.Members.GetCountFor(@interface) : @interface.Members.Count;
                if ((tc > 0 || mc > 0) && Options.AllowPartials || @interface.IsRoot)
                {
                    BuildTrailPush(@interface);
                    if (@interface.IsRoot)
                        this.TranslateDocumentationComments(@interface);
                    this.Translate(@interface.AccessLevel, @interface);
                    if (Options.AllowPartials && (!@interface.IsRoot || @interface.Parts.Count > 0))
                    {
                        this.WriteKeyword(CSharpKeywords.Partial);
                        this.Write(" ");
                    }
                    this.WriteKeyword(CSharpKeywords.Interface);
                    this.Write(" ");
                    this.DefineDeclaration(@interface);
                    this.Write(IntermediateSpanTranslationClasses.UserInterfaceType, @interface.Name);
                    if (@interface.IsGenericConstruct && @interface.TypeParameters.Count > 0)
                    {
                        this.WriteOperator("<");
                        bool first = true;
                        foreach (var parameter in @interface.TypeParameters.Values)
                        {
                            if (first)
                                first = false;
                            else
                                this.WriteOperator(", ");
                            parameter.Visit(this);
                        }
                        this.WriteOperator(">");
                    }
                    if (@interface.ImplementedInterfaces.Count > 0)
                    {
                        this.WriteOperator(" :");
                        this.DenoteNewLine();
                        IncreaseIndent();
                        bool first = true;
                        foreach (var implInter in @interface.GetDirectlyImplementedInterfaces())
                        {
                            if (first)
                                first = false;
                            else
                            {
                                this.WriteOperator(", ");
                                this.DenoteNewLine();
                            }
                            this.Translate(implInter);
                        }
                        DecreaseIndent();
                    }
                    this.DenoteNewLine();
                    if (@interface.IsGenericConstruct)
                    {
                        this.IncreaseIndent();
                        var lastParam = @interface.TypeParameters.Values.LastOrDefault();
                        foreach (var parameter in @interface.TypeParameters.Values)
                            this.TranslateConstraints(parameter, lastParam == parameter);
                        this.DecreaseIndent();
                    }
                    this.WriteOperator("{");
                    this.DenoteNewLine();
                    IncreaseIndent();
                    bool alpha = (Options.ElementOrderingMethod & TranslationOrderKind.Alphabetic) == TranslationOrderKind.Alphabetic;
                    bool specific = (Options.ElementOrderingMethod & TranslationOrderKind.Specific) == TranslationOrderKind.Specific;
                    bool verbatim = (Options.ElementOrderingMethod & TranslationOrderKind.Verbatim) == TranslationOrderKind.Verbatim;
                    if (verbatim)
                    {
                        foreach (var type in @interface.Types.Values)
                            if (type.Entry.Parent == @interface ||
                                !Options.AllowPartials)
                                type.Entry.Visit(this);
                        foreach (var member in @interface.Members.Values)
                            if (member.Entry.Parent == @interface ||
                                !Options.AllowPartials)
                                member.Entry.Visit(this);
                    }
                    else if (alpha && specific)
                    {
                        Dictionary<DeclarationTranslationOrder, int> translationOrder = GetDistinctTranslationOrder();
                        foreach (var declaration in from declaration in
                                                        (from m in @interface.Members.Values
                                                         select m.Entry).Concat<IIntermediateDeclaration>(
                                                            from t in @interface.Types.Values
                                                            select t.Entry).ToArray()
                                                    orderby translationOrder[GetEntityOrder(declaration)],
                                                            declaration.Name
                                                    select declaration)
                            if (declaration is IIntermediateMember)
                                ((IIntermediateMember)declaration).Visit(this);
                            else if (declaration is IIntermediateType)
                                ((IIntermediateType)(declaration)).Visit(this);
                    }
                    else if (alpha)
                    {
                        foreach (var declaration in from declaration in
                                                        (from m in @interface.Members.Values
                                                         select m.Entry).Concat<IIntermediateDeclaration>(
                                                            from t in @interface.Types.Values
                                                            select t.Entry).ToArray()
                                                    orderby declaration.Name
                                                    select declaration)
                            if (declaration is IIntermediateMember)
                                ((IIntermediateMember)declaration).Visit(this);
                            else if (declaration is IIntermediateType)
                                ((IIntermediateType)(declaration)).Visit(this);
                    }
                    else
                    {
                        Dictionary<DeclarationTranslationOrder, int> translationOrder = GetDistinctTranslationOrder();
                        foreach (var declaration in from declaration in
                                                        (from m in @interface.Members.Values
                                                         select m.Entry).Concat<IIntermediateDeclaration>(
                                                            from t in @interface.Types.Values
                                                            select t.Entry).ToArray()
                                                    orderby translationOrder[GetEntityOrder(declaration)]
                                                    select declaration)
                            if (declaration is IIntermediateMember)
                                ((IIntermediateMember)declaration).Visit(this);
                            else if (declaration is IIntermediateType)
                                ((IIntermediateType)(declaration)).Visit(this);
                    }
                    DecreaseIndent();
                    this.WriteOperator("};");
                    this.DenoteNewLine();
                    BuildTrailPop();
                }
            }
        }

        public override void Translate(IIntermediateStructType @struct)
        {
            if (Options.AllowPartials || @struct.IsRoot)
            {
                var tc = Options.AllowPartials ? @struct.Types.GetCountFor(@struct) : @struct.Types.Count;
                var mc = Options.AllowPartials ? @struct.Members.GetCountFor(@struct) : @struct.Members.Count;
                if ((tc > 0 || mc > 0) && Options.AllowPartials || @struct.IsRoot)
                {
                    this.BuildTrailPush(@struct);
                    if (@struct.IsRoot)
                        this.TranslateDocumentationComments(@struct);
                    this.Translate(@struct.AccessLevel, @struct);
                    if (Options.AllowPartials && (!@struct.IsRoot || @struct.Parts.Count > 0))
                    {
                        this.WriteKeyword(CSharpKeywords.Partial);
                        this.Write(" ");
                    }
                    this.WriteKeyword(CSharpKeywords.Struct);
                    this.Write(" ");
                    this.DefineDeclaration(@struct);
                    this.Write(IntermediateSpanTranslationClasses.UserStructType, @struct.Name);
                    this.EndDeclarationDefinition();
                    if (@struct.IsGenericConstruct && @struct.TypeParameters.Count > 0)
                    {
                        this.WriteOperator("<");
                        bool first = true;
                        foreach (var parameter in @struct.TypeParameters.Values)
                        {
                            if (first)
                                first = false;
                            else
                                this.WriteOperator(", ");
                            parameter.Visit(this);
                        }
                        this.WriteOperator(">");
                    }
                    var structAsIStruct = (IStructType)@struct;
                    if (structAsIStruct.ImplementedInterfaces.Count > 0)
                    {
                        this.WriteOperator(" :");
                        this.DenoteNewLine();
                        IncreaseIndent();
                        bool first = true;
                        foreach (var implInter in structAsIStruct.GetDirectlyImplementedInterfaces())
                        {
                            if (first)
                                first = false;
                            else
                            {
                                this.WriteOperator(", ");
                                this.DenoteNewLine();
                            }
                            this.Translate(implInter);
                        }
                        DecreaseIndent();
                    }
                    this.DenoteNewLine();
                    if (@struct.IsGenericConstruct)
                    {
                        this.IncreaseIndent();
                        var lastParam = @struct.TypeParameters.Values.LastOrDefault();
                        foreach (var parameter in @struct.TypeParameters.Values)
                            this.TranslateConstraints(parameter, lastParam == parameter);
                        this.DecreaseIndent();
                    }
                    this.WriteOperator("{");
                    this.DenoteNewLine();
                    IncreaseIndent();
                    bool alpha = (Options.ElementOrderingMethod & TranslationOrderKind.Alphabetic) == TranslationOrderKind.Alphabetic;
                    bool specific = (Options.ElementOrderingMethod & TranslationOrderKind.Specific) == TranslationOrderKind.Specific;
                    bool verbatim = (Options.ElementOrderingMethod & TranslationOrderKind.Verbatim) == TranslationOrderKind.Verbatim;
                    if (verbatim)
                    {
                        foreach (var type in @struct.Types.Values)
                            if (type.Entry.Parent == @struct ||
                                !Options.AllowPartials)
                                type.Entry.Visit(this);
                        foreach (var member in @struct.Members.Values)
                            if (member.Entry.Parent == @struct ||
                                !Options.AllowPartials)
                                member.Entry.Visit(this);
                    }
                    else if (alpha && specific)
                    {
                        Dictionary<DeclarationTranslationOrder, int> translationOrder = GetDistinctTranslationOrder();
                        foreach (var declaration in from declaration in
                                                        GetAllStructDeclarations(@struct).ToArray()
                                                    orderby translationOrder[GetEntityOrder(declaration)],
                                                            declaration.Name
                                                    select declaration)
                            if (declaration is IIntermediateMember)
                                ((IIntermediateMember)declaration).Visit(this);
                            else if (declaration is IIntermediateType)
                                ((IIntermediateType)(declaration)).Visit(this);
                    }
                    else if (alpha)
                    {
                        foreach (var declaration in from declaration in
                                                        GetAllStructDeclarations(@struct).ToArray()
                                                    orderby declaration.Name
                                                    select declaration)
                            if (declaration is IIntermediateMember)
                                ((IIntermediateMember)declaration).Visit(this);
                            else if (declaration is IIntermediateType)
                                ((IIntermediateType)(declaration)).Visit(this);
                    }
                    else
                    {
                        Dictionary<DeclarationTranslationOrder, int> translationOrder = GetDistinctTranslationOrder();
                        foreach (var declaration in from declaration in
                                                        GetAllStructDeclarations(@struct).ToArray()
                                                    orderby translationOrder[GetEntityOrder(declaration)]
                                                    select declaration)
                            if (declaration is IIntermediateMember)
                                ((IIntermediateMember)declaration).Visit(this);
                            else if (declaration is IIntermediateType)
                                ((IIntermediateType)(declaration)).Visit(this);
                    }
                    DecreaseIndent();
                    this.WriteOperator("};");
                    this.DenoteNewLine();
                    this.BuildTrailPop();
                }
            }
        }


        public override void Write(IntermediateSpanTranslationClasses spanClass, string text)
        {
            switch (spanClass)
            {
                case IntermediateSpanTranslationClasses.Identifier:
                case IntermediateSpanTranslationClasses.UserType:
                case IntermediateSpanTranslationClasses.UserDelegateType:
                case IntermediateSpanTranslationClasses.UserEnumType:
                case IntermediateSpanTranslationClasses.UserInterfaceType:
                case IntermediateSpanTranslationClasses.UserStructType:
                case IntermediateSpanTranslationClasses.ConstructorName:
                case IntermediateSpanTranslationClasses.MethodReference:
                case IntermediateSpanTranslationClasses.ParameterReference:
                case IntermediateSpanTranslationClasses.PropertyReference:
                case IntermediateSpanTranslationClasses.EventReference:
                case IntermediateSpanTranslationClasses.IndexerReference:
                case IntermediateSpanTranslationClasses.FieldReference:
                case IntermediateSpanTranslationClasses.LocalReference:
                case IntermediateSpanTranslationClasses.Symbol:
                case IntermediateSpanTranslationClasses.Label:
                case IntermediateSpanTranslationClasses.GenericParameterReference:
                    CSharpKeywords reverseLookupResult;
                    if (this.KeywordLookup.TryGetValue(text, out reverseLookupResult))
                    {
                        switch (reverseLookupResult)
                        {
                            case CSharpKeywords.__ArgList:
                            case CSharpKeywords.__MakeRef:
                            case CSharpKeywords.__RefType:
                            case CSharpKeywords.__RefValue:
                            case CSharpKeywords.Abstract:
                            case CSharpKeywords.As:
                            case CSharpKeywords.Base:
                            case CSharpKeywords.Bool:
                            case CSharpKeywords.Break:
                            case CSharpKeywords.Byte:
                            case CSharpKeywords.Case:
                            case CSharpKeywords.Catch:
                            case CSharpKeywords.Char:
                            case CSharpKeywords.Checked:
                            case CSharpKeywords.Class:
                            case CSharpKeywords.Const:
                            case CSharpKeywords.Continue:
                            case CSharpKeywords.Decimal:
                            case CSharpKeywords.Default:
                            case CSharpKeywords.Delegate:
                            case CSharpKeywords.Do:
                            case CSharpKeywords.Double:
                            case CSharpKeywords.Else:
                            case CSharpKeywords.Enum:
                            case CSharpKeywords.Event:
                            case CSharpKeywords.Explicit:
                            case CSharpKeywords.Extern:
                            case CSharpKeywords.False:
                            case CSharpKeywords.Finally:
                            case CSharpKeywords.Fixed:
                            case CSharpKeywords.Float:
                            case CSharpKeywords.For:
                            case CSharpKeywords.Foreach:
                            case CSharpKeywords.Goto:
                            case CSharpKeywords.If:
                            case CSharpKeywords.Implicit:
                            case CSharpKeywords.In:
                            case CSharpKeywords.Int:
                            case CSharpKeywords.Interface:
                            case CSharpKeywords.Internal:
                            case CSharpKeywords.Is:
                            case CSharpKeywords.Join:
                            case CSharpKeywords.Lock:
                            case CSharpKeywords.Long:
                            case CSharpKeywords.Namespace:
                            case CSharpKeywords.New:
                            case CSharpKeywords.Null:
                            case CSharpKeywords.Object:
                            case CSharpKeywords.Operator:
                            case CSharpKeywords.OrderBy:
                            case CSharpKeywords.Out:
                            case CSharpKeywords.Override:
                            case CSharpKeywords.Params:
                            case CSharpKeywords.Private:
                            case CSharpKeywords.Protected:
                            case CSharpKeywords.Public:
                            case CSharpKeywords.Readonly:
                            case CSharpKeywords.Ref:
                            case CSharpKeywords.Return:
                            case CSharpKeywords.Sbyte:
                            case CSharpKeywords.Sealed:
                            case CSharpKeywords.Short:
                            case CSharpKeywords.Sizeof:
                            case CSharpKeywords.Stackalloc:
                            case CSharpKeywords.Static:
                            case CSharpKeywords.String:
                            case CSharpKeywords.Struct:
                            case CSharpKeywords.Switch:
                            case CSharpKeywords.This:
                            case CSharpKeywords.Throw:
                            case CSharpKeywords.True:
                            case CSharpKeywords.Try:
                            case CSharpKeywords.Type:
                            case CSharpKeywords.Typeof:
                            case CSharpKeywords.Uint:
                            case CSharpKeywords.Ulong:
                            case CSharpKeywords.Unchecked:
                            case CSharpKeywords.Unsafe:
                            case CSharpKeywords.Ushort:
                            case CSharpKeywords.Using:
                            case CSharpKeywords.Virtual:
                            case CSharpKeywords.Void:
                            case CSharpKeywords.Volatile:
                            case CSharpKeywords.Where:
                            case CSharpKeywords.While:
                            case CSharpKeywords.Yield:
                                this.WriteSpanCheck(spanClass);
                                this.Write("@");
                                this.Write(text);
                                return;
                        }
                    }
                    break;
            }
            base.Write(spanClass, text);
        }



        private void TranslateConstraints<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> parameter, bool last = false)
            where TGenericParameter :
                IGenericParameter<TGenericParameter, TParent>
            where TIntermediateGenericParameter :
                TGenericParameter,
                IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
            where TParent :
                IGenericParamParent<TGenericParameter, TParent>
            where TIntermediateParent :
                TParent,
                IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
        {
            bool needsWhere = true;
            bool first = true;
            if (parameter.SpecialConstraint == GenericTypeParameterSpecialConstraint.Class)
            {
                needsWhere = false;
                InjectWhere(parameter);
                first = false;
                this.WriteKeyword(CSharpKeywords.Class);
            }
            else if (parameter.SpecialConstraint == GenericTypeParameterSpecialConstraint.Struct)
            {
                needsWhere = false;
                InjectWhere(parameter);
                first = false;
                this.WriteKeyword(CSharpKeywords.Struct);
            }
            if (parameter.Constraints.Count > 0)
            {
                if (needsWhere)
                {
                    needsWhere = false;
                    InjectWhere(parameter);
                }
                foreach (var constraint in parameter.Constraints)
                {
                    if (first)
                        first = false;
                    else
                    {
                        this.WriteOperator(",");
                        this.DenoteNewLine();
                    }
                    this.Translate(constraint);
                }

            }
            if (parameter.Constructors.ContainsKey(TypeSystemIdentifiers.GetCtorSignatureIdentifier(new IType[0])))
            {
                if (needsWhere)
                {
                    needsWhere = false;
                    InjectWhere(parameter);
                }
                this.WriteKeyword(CSharpKeywords.New);
                this.WriteOperator("()");
                this.DenoteNewLine();
            }
            if (!needsWhere)
            {
                this.DecreaseIndent();
                if (!last)
                    this.WriteLine();
            }

        }

        private void InjectWhere<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> parameter)
            where TGenericParameter :
                IGenericParameter<TGenericParameter, TParent>
            where TIntermediateGenericParameter :
                TGenericParameter,
                IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
            where TParent :
                IGenericParamParent<TGenericParameter, TParent>
            where TIntermediateParent :
                TParent,
                IIntermediateGenericParameterParent<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>
        {
            this.WriteKeyword(CSharpKeywords.Where);
            this.Write(" ");
            this.Write(IntermediateSpanTranslationClasses.Identifier, parameter.Name);
            this.WriteOperator(":");
            this.DenoteNewLine();
            this.IncreaseIndent();
        }

        public override void Translate<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent>(IIntermediateGenericParameter<TGenericParameter, TIntermediateGenericParameter, TParent, TIntermediateParent> parameter)
        {
            this.Write(IntermediateSpanTranslationClasses.GenericParameterReference, parameter.Name);
        }

        public override void Translate(ILocalMember local)
        {
            this.ReferenceDeclaration(local);
            this.Write(IntermediateSpanTranslationClasses.LocalReference, local.Name);
            this.EndReferenceDeclaration();
        }

        public override void Translate<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorSignatureMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
        {
            this.Translate(ctor.AccessLevel, ctor);
            this.ReferenceDeclaration(ctor);
            this.Write(IntermediateSpanTranslationClasses.ConstructorName, ctor.Parent.Name);
            this.EndReferenceDeclaration();
            this.WriteOperator("(");
            this.Translate(ctor.Parameters);
            this.WriteOperator(");");
            this.DenoteNewLine();

        }

        public override void Translate<TCtor, TIntermediateCtor, TType, TIntermediateType>(IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType> ctor)
        {
            this.TranslateDocumentationComments(ctor);
            this.Translate(ctor.AccessLevel, ctor);
            if (ctor.IsStaticConstructor)
            {
                WriteKeyword(CSharpKeywords.Static);
                Write(" ");
            }
            this.DefineDeclaration(ctor);
            this.Write(IntermediateSpanTranslationClasses.ConstructorName, ctor.Parent.Name);
            this.EndDeclarationDefinition();
            this.WriteOperator("(");
            this.Translate(ctor.Parameters);
            this.WriteOperator(")");
            this.DenoteNewLine();
            if (ctor.CascadeTarget != ConstructorCascadeTarget.Undefined)
            {
                this.IncreaseIndent();
                this.WriteOperator(": ");
                switch (ctor.CascadeTarget)
                {
                    case ConstructorCascadeTarget.Base:
                        this.WriteKeyword(CSharpKeywords.Base);
                        break;
                    case ConstructorCascadeTarget.This:
                        this.WriteKeyword(CSharpKeywords.This);
                        break;
                }
                this.WriteOperator("(");
                if (ctor.CascadeMembers != null)
                {
                    bool first = true;
                    foreach (var cascadeParam in ctor.CascadeMembers)
                    {
                        if (first)
                            first = false;
                        else
                            this.WriteOperator(", ");
                        cascadeParam.Visit(this);
                    }
                }
                this.WriteOperator(")");
                this.DenoteNewLine();
                this.DecreaseIndent();
            }
            this.TranslateBlockInternal(ctor);
        }

        public override void Translate<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
        {
        }

        public override void Translate<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent>(IIntermediateEventSignatureMember<TEvent, TIntermediateEvent, TEventParent, TIntermediateEventParent> @event)
        {
        }

        public override void Translate<TCoercionParent, TIntermediateCoercionParent>(IIntermediateTypeCoercionMember<TCoercionParent, TIntermediateCoercionParent> typeCoercion)
        {
            this.TranslateDocumentationComments(typeCoercion);
            this.Translate(typeCoercion.AccessLevel, typeCoercion);
            this.WriteKeyword(CSharpKeywords.Static);
            this.Write(" ");
            switch (typeCoercion.Requirement)
            {
                case TypeConversionRequirement.Explicit:
                    this.WriteKeyword(CSharpKeywords.Explicit);
                    this.Write(" ");
                    break;
                case TypeConversionRequirement.Implicit:
                    this.WriteKeyword(CSharpKeywords.Implicit);
                    this.Write(" ");
                    break;
            }
            this.WriteKeyword(CSharpKeywords.Operator);
            this.Write(" ");
            switch (typeCoercion.Direction)
            {
                case TypeConversionDirection.ToContainingType:
                    this.Translate(typeCoercion.Parent);
                    break;
                case TypeConversionDirection.FromContainingType:
                    this.Translate(typeCoercion.CoercionType);
                    break;
            }
            this.WriteOperator("(");
            switch (typeCoercion.Direction)
            {
                case TypeConversionDirection.ToContainingType:
                    this.Translate(typeCoercion.CoercionType);
                    break;
                case TypeConversionDirection.FromContainingType:
                    this.Translate(typeCoercion.Parent);
                    break;
            }
            this.Write(" ");
            this.DefineDeclaration(typeCoercion.Incoming);
            this.Write(IntermediateSpanTranslationClasses.LocalReference, typeCoercion.Incoming.Name);
            this.EndDeclarationDefinition();
            this.WriteOperator(")");
            this.WriteLine();
            this.TranslateBlockInternal(typeCoercion);
        }

        public override void Translate<TCoercionParent, TIntermediateCoercionParent>(IIntermediateBinaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent> binaryCoercion)
        {
            this.TranslateDocumentationComments(binaryCoercion);
            this.Translate(binaryCoercion.AccessLevel, binaryCoercion);
            this.WriteKeyword(CSharpKeywords.Static);
            this.Write(" ");
            this.Translate(binaryCoercion.ReturnType);
            string operatorText = string.Empty;
            this.Write(" ");
            this.WriteKeyword(CSharpKeywords.Operator);
            switch (binaryCoercion.Operator)
            {
                case CoercibleBinaryOperators.Add:
                    Translate(BinaryOperationKind.Add, false);
                    break;
                case CoercibleBinaryOperators.Subtract:
                    Translate(BinaryOperationKind.Subtract, false);
                    break;
                case CoercibleBinaryOperators.Multiply:
                    Translate(BinaryOperationKind.Multiply, false);
                    break;
                case CoercibleBinaryOperators.Divide:
                    Translate(BinaryOperationKind.StrictDivision, false);
                    break;
                case CoercibleBinaryOperators.Modulus:
                    Translate(BinaryOperationKind.Modulus, false);
                    break;
                case CoercibleBinaryOperators.BitwiseAnd:
                    Translate(BinaryOperationKind.BitwiseAnd, false);
                    break;
                case CoercibleBinaryOperators.BitwiseOr:
                    Translate(BinaryOperationKind.BitwiseOr, false);
                    break;
                case CoercibleBinaryOperators.ExclusiveOr:
                    Translate(BinaryOperationKind.BitwiseExclusiveOr, false);
                    break;
                case CoercibleBinaryOperators.LeftShift:
                    Translate(BinaryOperationKind.LeftShift, false);
                    break;
                case CoercibleBinaryOperators.RightShift:
                    Translate(BinaryOperationKind.RightShift, false);
                    break;
                case CoercibleBinaryOperators.IsEqualTo:
                    Translate(BinaryOperationKind.Equality, false);
                    break;
                case CoercibleBinaryOperators.IsNotEqualTo:
                    Translate(BinaryOperationKind.Inequality, false);
                    break;
                case CoercibleBinaryOperators.LessThan:
                    Translate(BinaryOperationKind.LessThan, false);
                    break;
                case CoercibleBinaryOperators.GreaterThan:
                    Translate(BinaryOperationKind.GreaterThan, false);
                    break;
                case CoercibleBinaryOperators.LessThanOrEqualTo:
                    Translate(BinaryOperationKind.LessThanOrEqualTo, false);
                    break;
                case CoercibleBinaryOperators.GreaterThanOrEqualTo:
                    Translate(BinaryOperationKind.GreaterThanOrEqualTo, false);
                    break;
            }
            this.WriteOperator("(");
            switch (binaryCoercion.ContainingSide)
            {
                case BinaryOpCoercionContainingSide.LeftSide:
                    this.Translate((IType)binaryCoercion.Parent);
                    this.Write(" ");
                    this.DefineDeclaration(binaryCoercion.LeftSide);
                    this.Write(IntermediateSpanTranslationClasses.LocalReference, binaryCoercion.LeftSide.Name);
                    this.EndDeclarationDefinition();
                    this.WriteOperator(", ");
                    this.Translate(binaryCoercion.OtherSide);
                    this.Write(" ");
                    this.DefineDeclaration(binaryCoercion.RightSide);
                    this.Write(IntermediateSpanTranslationClasses.LocalReference, binaryCoercion.RightSide.Name);
                    this.EndDeclarationDefinition();
                    break;
                case BinaryOpCoercionContainingSide.RightSide:
                    this.Translate(binaryCoercion.OtherSide);
                    this.Write(" ");
                    this.DefineDeclaration(binaryCoercion.LeftSide);
                    this.Write(IntermediateSpanTranslationClasses.LocalReference, binaryCoercion.LeftSide.Name);
                    this.EndDeclarationDefinition();
                    this.WriteOperator(", ");
                    this.Translate((IType)binaryCoercion.Parent);
                    this.Write(" ");
                    this.DefineDeclaration(binaryCoercion.RightSide);
                    this.Write(IntermediateSpanTranslationClasses.LocalReference, binaryCoercion.RightSide.Name);
                    this.EndDeclarationDefinition();
                    break;
                case BinaryOpCoercionContainingSide.Both:
                    this.Translate((IType)binaryCoercion.Parent);
                    this.Write(" ");
                    this.DefineDeclaration(binaryCoercion.LeftSide);
                    this.Write(IntermediateSpanTranslationClasses.LocalReference, binaryCoercion.LeftSide.Name);
                    this.EndDeclarationDefinition();
                    this.WriteOperator(", ");
                    this.Translate((IType)binaryCoercion.Parent);
                    this.Write(" ");
                    this.DefineDeclaration(binaryCoercion.RightSide);
                    this.Write(IntermediateSpanTranslationClasses.LocalReference, binaryCoercion.RightSide.Name);
                    this.EndDeclarationDefinition();
                    break;
            }
            this.WriteOperator(")");
            this.WriteLine();
            this.TranslateBlockInternal(binaryCoercion);
        }


        public override void Translate<TCoercionParent, TIntermediateCoercionParent>(IIntermediateUnaryOperatorCoercionMember<TCoercionParent, TIntermediateCoercionParent> unaryCoercion)
        {
        }

        public override void Translate<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent>(IIntermediateFieldMember<TField, TIntermediateField, TFieldParent, TIntermediateFieldParent> field)
        {
            this.TranslateDocumentationComments(field);
            if (field is IIntermediateScopedDeclaration)
            {
                IIntermediateScopedDeclaration scopedDecl = (IIntermediateScopedDeclaration)field;
                this.Translate(scopedDecl.AccessLevel, scopedDecl);
            }
            if (field.ReadOnly)
            {
                this.WriteKeyword(CSharpKeywords.Readonly);
                this.Write(" ");
            }
            if (field is IIntermediateInstanceMember)
            {
                var instanceField = (IIntermediateInstanceMember)field;
                if (instanceField.IsStatic)
                {
                    this.WriteKeyword(CSharpKeywords.Static);
                    this.Write(" ");
                }
            }
            this.Translate(field.FieldType);
            this.Write(" ");
            this.DefineDeclaration(field);
            this.Write(IntermediateSpanTranslationClasses.FieldReference, field.Name);
            this.EndDeclarationDefinition();
            if (field.InitializationExpression != null)
            {
                this.WriteOperator(" = ");
                field.InitializationExpression.Visit(this);
            }
            this.WriteOperator(";");
            this.WriteLine();
        }

        public override void Translate(IIntermediateEnumFieldMember field)
        {
            this.TranslateDocumentationComments(field);
            this.DefineDeclaration(field);
            this.Write(IntermediateSpanTranslationClasses.FieldReference, field.Name);
            if (field.Value != null && field.Value.ValueType != EnumValueType.Automatic)
            {
                this.WriteOperator(" = ");
                ((IExpression)(field.Value)).Visit(this);
            }
            this.EndDeclarationDefinition();
        }

        public override void Translate<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexer)
        {
            this.TranslateDocumentationComments(indexer);
            this.Translate(indexer.Metadata, MetadatumTargets.Indexer, false);
            this.Translate(indexer.AccessLevel, indexer);
            if (indexer.IsAbstract)
            {
                this.WriteKeyword(CSharpKeywords.Abstract);
                this.Write(" ");
            }
            this.Translate(indexer.PropertyType);
            this.Write(" ");
            this.DefineDeclaration(indexer);
            this.WriteKeyword(CSharpKeywords.This);
            this.EndDeclarationDefinition();
            this.WriteOperator("[");
            this.Translate(indexer.Parameters);
            this.WriteOperator("]");
            this.DenoteNewLine();
            this.WriteOperator("{");
            this.DenoteNewLine();
            this.IncreaseIndent();
            if (indexer.CanRead)
            {
                this.WriteKeyword(CSharpKeywords.Get);
                if (indexer.IsAbstract)
                    this.WriteOperator(";");
                else
                {
                    this.DenoteNewLine();
                    this.TranslateBlockInternal(indexer.GetMethod);
                }
            }
            if (indexer.CanWrite)
            {
                this.WriteKeyword(CSharpKeywords.Set);
                if (indexer.IsAbstract)
                    this.WriteOperator(";");
                else
                {
                    this.DenoteNewLine();
                    this.TranslateBlockInternal(indexer.SetMethod);
                }
            }
            this.DecreaseIndent();
            this.WriteOperator("}");
            this.DenoteNewLine();
        }

        public override void Translate<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent>(IIntermediateIndexerSignatureMember<TIndexer, TIntermediateIndexer, TIndexerParent, TIntermediateIndexerParent> indexerSignature)
        {
            this.TranslateDocumentationComments(indexerSignature);
            this.Translate(indexerSignature.Metadata, MetadatumTargets.Indexer, false);
            this.Translate(indexerSignature.PropertyType);
            this.Write(" ");
            this.DefineDeclaration(indexerSignature);
            this.WriteKeyword(CSharpKeywords.This);
            this.EndDeclarationDefinition();
            this.WriteOperator("[");
            this.Translate(indexerSignature.Parameters);
            this.WriteOperator("]");
            this.Write(" ");
            this.WriteOperator("{");
            if (indexerSignature.CanRead || indexerSignature.CanWrite)
                this.Write(" ");
            if (indexerSignature.CanRead)
            {
                this.WriteKeyword(CSharpKeywords.Get);
                this.WriteOperator(";");
            }
            if (indexerSignature.CanRead && indexerSignature.CanWrite)
                this.Write(" ");
            if (indexerSignature.CanWrite)
            {
                this.WriteKeyword(CSharpKeywords.Set);
                this.WriteOperator(";");
            }
            this.Write(" ");
            this.WriteOperator("}");
            this.DenoteNewLine();
        }

        public override void Translate<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>(IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> method)
        {
            IInterfaceType privateImplementation = null;
            if (method is IIntermediateClassMethodMember)
                privateImplementation = (IInterfaceType)((IIntermediateClassMethodMember)(method)).Implementations.FirstOrDefault();
            else if (method is IIntermediateStructMethodMember)
                privateImplementation = (IInterfaceType)((IIntermediateStructMethodMember)(method)).Implementations.FirstOrDefault();
            this.TranslateInternal(method, () =>
            {
                this.Translate(method.AccessLevel, method);
                this.TranslateFlagsInternal(method);
            },
            () =>
            {
                if (privateImplementation != null)
                {
                    this.Translate(privateImplementation);
                    this.WriteOperator(".");
                }
            });
            if (method is IClassMethodMember)
            {
                var classMethod = (IClassMethodMember)method;
                if (classMethod.IsAbstract)
                {
                    this.WriteOperator(";");
                    this.DenoteNewLine();
                    return;
                }
            }
            this.DenoteNewLine();
            this.TranslateBlockInternal(method);
        }

        private void TranslateFlagsInternal<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>(IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent> method)
            where TMethod :
                IMethodMember<TMethod, TMethodParent>
            where TIntermediateMethod :
                IIntermediateMethodMember<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
                TMethod
            where TMethodParent :
                IMethodParent<TMethod, TMethodParent>
            where TIntermediateMethodParent :
                IIntermediateMethodParent<TMethod, TIntermediateMethod, TMethodParent, TIntermediateMethodParent>,
                TMethodParent
        {
            if (method is IIntermediateClassMethodMember)
                this.TranslateFlagsInternal(((IIntermediateClassMethodMember)method).ImplicitAttributes);
            else if (method is IClassMethodMember)
                this.TranslateFlagsInternal(((IClassMethodMember)method).Attributes);
            else if (method is IExtendedMethodMember)
                this.TranslateFlagsInternal((ClassMethodMemberFlags)((IExtendedMethodMember)method).Attributes);
            else if (method is IInstanceMember)
                this.TranslateFlagsInternal((ClassMethodMemberFlags)((IInstanceMember)method).Attributes);
        }

        private void TranslateFlagsInternal(ClassMethodMemberFlags flags)
        {
            if ((flags & ClassMethodMemberFlags.Static) == ClassMethodMemberFlags.Static)
            {
                this.WriteKeyword(CSharpKeywords.Static);
                this.Write(" ");
            }
            if ((flags & ClassMethodMemberFlags.Virtual) == ClassMethodMemberFlags.Virtual)
            {
                this.WriteKeyword(CSharpKeywords.Virtual);
                this.Write(" ");
            }
            if ((flags & ClassMethodMemberFlags.Abstract) == ClassMethodMemberFlags.Abstract)
            {
                this.WriteKeyword(CSharpKeywords.Abstract);
                this.Write(" ");
            }
            if ((flags & ClassMethodMemberFlags.Override) == ClassMethodMemberFlags.Override)
            {
                this.WriteKeyword(CSharpKeywords.Override);
                this.Write(" ");
            }
            if ((flags & ClassMethodMemberFlags.Final) == ClassMethodMemberFlags.Final)
            {
                this.WriteKeyword(CSharpKeywords.Sealed);
                this.Write(" ");
            }
            if ((flags & ClassMethodMemberFlags.Async) == ClassMethodMemberFlags.Async)
            {
                this.WriteKeyword(CSharpKeywords.Async);
                this.Write(" ");
            }
        }

        private void TranslateInternal<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>(
            IIntermediateMethodSignatureMember<
                TSignatureParameter,    TIntermediateSignatureParameter, 
                TSignature,             TIntermediateSignature, 
                TParent,                TIntermediateParent> methodSignature, 
            Action postAccessibility = null,
            Action preNameSpecifier = null)
            where TSignatureParameter :
                IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
            where TIntermediateSignatureParameter :
                TSignatureParameter,
                IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
            where TSignature :
                IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
            where TIntermediateSignature :
                TSignature,
                IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
            where TParent :
                ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TParent>
            where TIntermediateParent :
                TParent,
                IIntermediateSignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TParent, TIntermediateParent>
        {
            this.TranslateDocumentationComments(methodSignature);
            this.Translate(methodSignature.Metadata, MetadatumTargets.Method, false);
            this.Translate(methodSignature.ReturnTypeMetadata, MetadatumTargets.ReturnValue, true);
            if (postAccessibility != null)
                postAccessibility();
            this.Translate(methodSignature.ReturnType);
            this.Write(" ");
            if (preNameSpecifier != null)
                preNameSpecifier();
            this.DefineDeclaration(methodSignature);
            this.Write(IntermediateSpanTranslationClasses.MethodReference, methodSignature.Name);
            this.EndDeclarationDefinition();
            if (methodSignature.IsGenericConstruct)
            {
                this.WriteOperator("<");
                bool first = true;
                foreach (var parameter in methodSignature.TypeParameters.Values)
                {
                    if (first)
                        first = false;
                    else
                        this.WriteOperator(", ");
                    parameter.Visit(this);
                }
                this.WriteOperator(">");
            }
            this.WriteOperator("(");
            this.Translate(methodSignature.Parameters);
            this.WriteOperator(")");
            if (methodSignature.IsGenericConstruct)
            {
                if (!this.AnyTypeParametersHaveConstraints(methodSignature.TypeParameters))
                    return;
                this.WriteLine();
                this.IncreaseIndent();
                var lastParam = methodSignature.TypeParameters.Values.LastOrDefault();
                foreach (var parameter in methodSignature.TypeParameters.Values)
                    this.TranslateConstraints(parameter, lastParam == parameter);
                this.DecreaseIndent();
                /* Unless we're in something that has a method body attached, we don't want a new line after the last
                 * type-parameter. */
                if (methodSignature is IInstanceMember)
                    if (methodSignature is IClassMethodMember)
                    {
                        var classMethod = (IClassMethodMember)methodSignature;
                        if (!classMethod.IsAbstract)
                            this.WriteLine();
                    }
                    else
                        this.WriteLine();
            }
        }

        private bool AnyTypeParametersHaveConstraints(
            IIntermediateGenericParameterDictionary<
                IMethodSignatureGenericTypeParameterMember, IIntermediateMethodSignatureGenericTypeParameterMember, 
                IMethodSignatureMember, IIntermediateMethodSignatureMember> typeParameters)
        {
            foreach (var typeParameter in typeParameters.Values)
            {
                if (typeParameter.SpecialConstraint != GenericTypeParameterSpecialConstraint.None)
                    return true;
                if (typeParameter.ImplementedInterfaces.Count > 0)
                    return true;
            }
            return false;
        }



        public override void Translate<TSignature, TIntermediateSignature, TParent, TIntermediateParent>(IIntermediateMethodSignatureMember<TSignature, TIntermediateSignature, TParent, TIntermediateParent> methodSignature)
        {
            this.TranslateInternal(methodSignature);
            this.WriteOperator(";");
            this.WriteLine();
        }

        public override void Translate<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertySignatureMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> propertySignature)
        {
            this.TranslateDocumentationComments(propertySignature);
            this.Translate(propertySignature.Metadata, MetadatumTargets.Property, false);
            this.Translate(propertySignature.PropertyType);
            this.Write(" ");
            this.DefineDeclaration(propertySignature);
            this.Write(IntermediateSpanTranslationClasses.PropertyReference, propertySignature.Name);
            this.EndDeclarationDefinition();
            this.Write(" ");
            this.WriteOperator("{");
            if (propertySignature.CanRead || propertySignature.CanWrite)
                this.Write(" ");
            if (propertySignature.CanRead)
            {
                this.WriteKeyword(CSharpKeywords.Get);
                this.WriteOperator(";");
            }
            if (propertySignature.CanRead && propertySignature.CanWrite)
                this.Write(" ");
            if (propertySignature.CanWrite)
            {
                this.WriteKeyword(CSharpKeywords.Set);
                this.WriteOperator(";");
            }
            this.Write(" ");
            this.WriteOperator("}");
            this.DenoteNewLine();
        }

        private void TranslateComment(string commentBody)
        {
            var text = GetCSharpCommentText(commentBody, false, false);
            var lines = text.Split(new[] { "\r\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                this.Write(IntermediateSpanTranslationClasses.Comment, line);
                this.WriteLine();
            }
        }
        private void TranslateDocumentationComment(string docCommentBody)
        {
            var text = GetCSharpCommentText(docCommentBody, true);
            var lines = text.Split(new[] { "\r\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                this.Write(IntermediateSpanTranslationClasses.Comment, line);
                this.WriteLine();
            }
        }

        private void TranslateDocumentationComments(IIntermediateMember target, Action betweenSummaryRemarks = null)
        {
            if (target.SummaryText != null)
                this.TranslateDocumentationComment(string.Format("<summary>\r\n{0}\r\n</summary>", ResolveDocumentationCommentLookups(target.SummaryText.HtmlEncode(false))));
            if (betweenSummaryRemarks != null)
                betweenSummaryRemarks();
            if (target.RemarksText != null)
                this.TranslateDocumentationComment(string.Format("<remarks>\r\n{0}\r\n</remarks>", ResolveDocumentationCommentLookups(target.RemarksText.HtmlEncode(false))));
        }

        private void TranslateDocumentationComments(IIntermediateType target)
        {
            if (target.SummaryText != null)
                this.TranslateDocumentationComment(string.Format("<summary>\r\n{0}\r\n</summary>", ResolveDocumentationCommentLookups(target.SummaryText.HtmlEncode(false))));
            if (target is IIntermediateGenericType)
            {
                var gTarget = (IIntermediateGenericType)target;
                var tParamSummaries = (from IIntermediateGenericParameter tParam in gTarget.TypeParameters.Values
                                       where !tParam.SummaryText.IsEmptyOrNull()
                                       select string.Format("<typeparam name=\"{0}\">\r\n{1}\r\n</typeparam>", tParam.Name, ResolveDocumentationCommentLookups(tParam.SummaryText.HtmlEncode(false)))).ToArray();
                if (tParamSummaries.Length > 0)
                    foreach (var tParamSummary in tParamSummaries)
                        this.TranslateDocumentationComment(tParamSummary);
            }
            if (target.RemarksText != null)
                this.TranslateDocumentationComment(string.Format("<remarks>\r\n{0}\r\n</remarks>", ResolveDocumentationCommentLookups(target.RemarksText.HtmlEncode(false))));
        }

        private void TranslateDocumentationComments(IIntermediateSignatureMember target, Action beforeParameters = null, Action afterParameters = null)
        {
            TranslateDocumentationComments((IIntermediateMember)target,
            () =>
            {
                if (beforeParameters != null)
                    beforeParameters();
                var paramSummaries = (from IIntermediateParameterMember p in target.Parameters.Values
                                      where !p.SummaryText.IsEmptyOrNull()
                                      select string.Format("<param name=\"{0}\">\r\n{1}\r\n</param>", p.Name, ResolveDocumentationCommentLookups(p.SummaryText.HtmlEncode(false)))).ToArray();
                if (paramSummaries.Length > 0)
                    foreach (var paramSummary in paramSummaries)
                        this.TranslateDocumentationComment(paramSummary);
                if (afterParameters != null)
                    afterParameters();
            });
        }

        private void TranslateDocumentationComments(IIntermediateMethodSignatureMember target)
        {
            TranslateDocumentationComments((IIntermediateSignatureMember)target,
                () =>
                {
                    var tParamSummaries = (from IIntermediateGenericParameter tParam in target.TypeParameters.Values
                                           where !tParam.SummaryText.IsEmptyOrNull()
                                           select string.Format("<typeparam name=\"{0}\">\r\n{1}\r\n</typeparam>", tParam.Name, ResolveDocumentationCommentLookups(tParam.SummaryText.HtmlEncode(false)))).ToArray();
                    if (tParamSummaries.Length > 0)
                        foreach (var tParamSummary in tParamSummaries)
                            this.TranslateDocumentationComment(tParamSummary);
                });
            if (target.ReturnsText != null)
                this.TranslateDocumentationComment(string.Format("<returns>\r\n{0}\r\n</returns>", ResolveDocumentationCommentLookups(target.ReturnsText.HtmlEncode(false))));
        }
        public override void Translate<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent>(IIntermediatePropertyMember<TProperty, TIntermediateProperty, TPropertyParent, TIntermediatePropertyParent> property)
        {
            this.TranslateDocumentationComments(property);
            this.Translate(property.Metadata, MetadatumTargets.Property, false);
            this.Translate(property.AccessLevel, property);
            if (property.IsAbstract)
            {
                this.WriteKeyword(CSharpKeywords.Abstract);
                this.Write(" ");
            }
            if (property is IIntermediateInstanceMember)
            {
                var instanceProperty = (IIntermediateInstanceMember)property;
                if (instanceProperty.IsStatic)
                {
                    this.WriteKeyword(CSharpKeywords.Static);
                    this.Write(" ");
                }
            }
            this.Translate(property.PropertyType);
            this.Write(" ");
            this.DefineDeclaration(property);
            this.Write(IntermediateSpanTranslationClasses.PropertyReference, property.Name);
            this.EndDeclarationDefinition();
            this.DenoteNewLine();
            this.WriteOperator("{");
            this.DenoteNewLine();
            this.IncreaseIndent();
            if (property.CanRead)
            {
                if (property.GetMethod.AccessLevel.CompareTo(property.AccessLevel) < 0)
                    this.Translate(property.GetMethod.AccessLevel, property.GetMethod);
                this.WriteKeyword(CSharpKeywords.Get);
                if (property.IsAbstract)
                    this.WriteOperator(";");
                else
                {

                    this.DenoteNewLine();
                    this.TranslateBlockInternal(property.GetMethod);
                }
            }
            if (property.CanWrite)
            {
                if (property.SetMethod.AccessLevel.CompareTo(property.AccessLevel) < 0)
                    this.Translate(property.SetMethod.AccessLevel, property.SetMethod);
                this.WriteKeyword(CSharpKeywords.Set);
                if (property.IsAbstract)
                    this.WriteOperator(";");
                else
                {
                    this.DenoteNewLine();
                    this.TranslateBlockInternal(property.SetMethod);
                }
            }
            this.DecreaseIndent();
            this.WriteOperator("}");
            this.DenoteNewLine();
        }


        public override void Translate<TParent, TIntermediateParent>(IIntermediateParameterMember<TParent, TIntermediateParent> parameter)
        {
            switch (parameter.Direction)
            {
                case ParameterCoercionDirection.Out:
                    this.WriteKeyword(CSharpKeywords.Out);
                    this.Write(" ");
                    break;
                case ParameterCoercionDirection.Reference:
                    this.WriteKeyword(CSharpKeywords.Ref);
                    this.Write(" ");
                    break;
            }
            this.Translate(parameter.ParameterType);
            this.Write(" ");
            this.DefineDeclaration(parameter);
            this.Write(IntermediateSpanTranslationClasses.ParameterReference, parameter.Name);
            this.EndDeclarationDefinition();
        }

        public override void Translate(ILinqRangeVariable rangeVariable)
        {
            this.DefineDeclaration(rangeVariable);
            this.Write(IntermediateSpanTranslationClasses.LocalReference, rangeVariable.Name);
            this.EndDeclarationDefinition();
        }

        public override void Translate(ILinqTypedRangeVariable rangeVariable)
        {
            this.Translate(rangeVariable.RangeType);
            this.Write(" ");
            this.DefineDeclaration(rangeVariable);
            this.Write(IntermediateSpanTranslationClasses.LocalReference, rangeVariable.Name);
            this.EndDeclarationDefinition();
        }

        public override void Translate(INamedInclusionScopeCoercion namedInclusion)
        {
            this.WriteKeyword(CSharpKeywords.Using);
            this.Write(" ");
            string[] breakdown = namedInclusion.IncludedName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < breakdown.Length; i++)
            {
                if (i > 0)
                    this.WriteOperator(".");
                var currentIdentifier = breakdown[i];
                this.Write(IntermediateSpanTranslationClasses.Identifier, currentIdentifier);
            }
        }

        public override void Translate(INamedInclusionRenameScopeCoercion renamedInclusion)
        {
            this.WriteKeyword(CSharpKeywords.Using);
            this.Write(" ");

            this.Write(IntermediateSpanTranslationClasses.Identifier, renamedInclusion.NewName);
            this.WriteOperator(" = ");
            string[] breakdown = renamedInclusion.IncludedName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < breakdown.Length; i++)
            {
                if (i > 0)
                    this.WriteOperator(".");
                var currentIdentifier = breakdown[i];
                this.Write(IntermediateSpanTranslationClasses.Identifier, currentIdentifier);
            }
        }

        public override void Translate(INamespaceInclusionScopeCoercion namespaceInclusion)
        {
            this.WriteKeyword(CSharpKeywords.Using);
            this.Write(" ");
            WriteNamespaceName(namespaceInclusion.Namespace);
            this.WriteOperator(";");
            this.DenoteNewLine();
        }

        private void WriteNamespaceName(string name)
        {
            string[] breakdown = name.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < breakdown.Length; i++)
            {
                if (i > 0)
                    this.WriteOperator(".");
                var currentIdentifier = breakdown[i];
                this.Write(IntermediateSpanTranslationClasses.Identifier, currentIdentifier);
            }
        }

        public override void Translate(INamespaceInclusionRenameScopeCoercion renamedNamespaceInclusion)
        {
            this.WriteKeyword(CSharpKeywords.Using);
            this.Write(" ");

            this.Write(IntermediateSpanTranslationClasses.Identifier, renamedNamespaceInclusion.NewName);
            this.WriteOperator(" = ");
            WriteNamespaceName(renamedNamespaceInclusion.Namespace);
            this.WriteOperator(";");
            this.DenoteNewLine();
        }

        public override void Translate(ITypeInclusionScopeCoercion typeInclusion)
        {
            this.WriteKeyword(CSharpKeywords.Using);
            this.Write(" ");
            WriteNamespaceName(typeInclusion.IncludedType.NamespaceName);
            this.WriteOperator(".");
            this.Translate(typeInclusion.IncludedType);
            this.WriteOperator(";");
            this.DenoteNewLine();
        }

        public override void Translate(ITypeInclusionRenameScopeCoercion renamedTypeInclusion)
        {
            this.WriteKeyword(CSharpKeywords.Using);
            this.Write(" ");
            this.Write(IntermediateSpanTranslationClasses.Identifier, renamedTypeInclusion.NewName);
            this.WriteOperator(" = ");
            WriteNamespaceName(renamedTypeInclusion.IncludedType.NamespaceName);
            this.WriteOperator(".");
            this.Translate(renamedTypeInclusion.IncludedType);
            this.WriteOperator(";");
            this.DenoteNewLine();
        }

        public override void Translate(IStaticInclusionScopeCoercion staticInclusion)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent>(IBoundChangeEventSignatureHandlerStatement<TEvent, TEventParameter, TEventParent, TSignatureParameter, TSignature, TSignatureParent> statement)
        {
            throw new NotImplementedException();
        }

        public override void Translate<TProperty, TPropertyParent>(IPropertyReferenceExpression<TProperty, TPropertyParent> expression)
        {
            if (expression.Source != null)
            {
                expression.Source.Visit(this);
                this.WriteOperator(".");
            }
            this.ReferenceDeclaration(expression.Member);
            this.Write(IntermediateSpanTranslationClasses.PropertyReference, expression.Name);
            this.EndReferenceDeclaration();
        }

        public override void Translate<TPropertySignature, TPropertySignatureParent>(IPropertySignatureReferenceExpression<TPropertySignature, TPropertySignatureParent> expression)
        {
            if (expression.Source != null)
            {
                expression.Source.Visit(this);
                this.WriteOperator(".");
            }
            this.ReferenceDeclaration(expression.Member);
            this.Write(IntermediateSpanTranslationClasses.PropertyReference, expression.Name);
            this.EndReferenceDeclaration();
        }

        public override void Translate<TField, TFieldParent>(IFieldReferenceExpression<TField, TFieldParent> expression)
        {
            if (expression.Source != null)
            {
                expression.Source.Visit(this);
                this.WriteOperator(".");
            }
            this.ReferenceDeclaration(expression.Member);
            this.Write(IntermediateSpanTranslationClasses.FieldReference, expression.Name);
            this.EndReferenceDeclaration();
        }

        public override void Translate<T>(IExpressionCollection<T> expressionCollection)
        {
            bool first = true;
            foreach (var expression in expressionCollection)
            {
                if (first)
                    first = false;
                else
                    this.WriteOperator(", ");
                expression.Visit(this);
            }
        }

        private static void DiscernOpFlags(UnaryOperation original, out bool bitInvert, out bool boolInvert, out bool negate, out bool postOp, out bool preOp, out bool decrement, out bool increment)
        {
            /* *
             * Rediscern the flags based upon logic that cannot be expressed
             * in mere bits.
             * */
            bitInvert = ((original & UnaryOperation.BitwiseInversion) == UnaryOperation.BitwiseInversion);
            boolInvert = ((original & UnaryOperation.BooleanInversion) == UnaryOperation.BooleanInversion)
                                && !bitInvert;
            negate = ((original & UnaryOperation.SignInversion) == UnaryOperation.SignInversion)
                                && !boolInvert;
            postOp = ((original & UnaryOperation.PostAction) == UnaryOperation.PostAction);
            preOp = ((original & UnaryOperation.PreAction) == UnaryOperation.PreAction)
                                && !postOp;
            decrement = ((original & UnaryOperation.Decrement) == UnaryOperation.Decrement)
                                && (preOp || postOp);
            increment = ((original & UnaryOperation.Increment) == UnaryOperation.Increment)
                                && !decrement
                                && (preOp || postOp);
        }

        public override void Translate(IUnaryOperationExpression expression)
        {
            if (expression.Type == ExpressionKind.UnaryForwardTerm)
                expression.Term.Visit(this);
            else
            {
                bool bitInvert, boolInvert, negate, postOp, preOp, decrement, increment;
                DiscernOpFlags(expression.Operation, out bitInvert, out boolInvert, out negate, out postOp, out preOp, out decrement, out increment);
                string preops = null;
                if (bitInvert)
                    if (negate)
                        preops = "~-";
                    else
                        preops = "~";
                else if (boolInvert)
                    preops = "!";
                else if (negate)
                    preops = "-";
                if (preOp)
                    if (decrement)
                        preops = "--" + preops;
                    else if (increment)
                        preops = "++" + preops;
                if (preops != null)
                    this.Write(IntermediateSpanTranslationClasses.Operator, preops);
                expression.Term.Visit(this);
                if (postOp)
                    if (decrement)
                        this.WriteOperator("--");
                    else if (increment)
                        this.WriteOperator("++");
            }
        }

        private static bool NeedsNamespacePrinted(IType targetType, IEnumerable<IScopeCoercionCollection> scopeCoercionSet, IEnumerable<IIntermediateNamespaceDeclaration> buildTrailLimit)
        {
            var coreType = targetType.IdentityManager.ObtainCoreType(targetType);
            if (coreType == RuntimeCoreType.None || !autoformTypes.Contains(coreType))
            {
                string namespaceName = targetType.NamespaceName;
                if (!(targetType.Parent is INamespaceDeclaration))
                    return false;
                if (buildTrailLimit.Any(k => (k.FullName != null && k.FullName.StartsWith(namespaceName)) || (k.FullName == null && namespaceName == null)))
                    return false;
                foreach (var scopeCoercions in scopeCoercionSet)
                {
                    foreach (var scopeCoercion in scopeCoercions)
                    {
                        if (scopeCoercion is INamedInclusionScopeCoercion)
                        {
                            var nisc = (INamedInclusionScopeCoercion)scopeCoercion;
                            if (nisc.IncludedName == namespaceName)
                                return false;
                        }
                        else if (scopeCoercion is INamespaceInclusionScopeCoercion)
                        {
                            var nisc = (INamespaceInclusionScopeCoercion)scopeCoercion;
                            if (nisc.Namespace == namespaceName)
                                return false;
                        }
                    }
                }
                return !string.IsNullOrEmpty(namespaceName);
            }
            else
                return false;
        }

        public override void Translate(IType type)
        {
            if (type.IsGenericConstruct && type is IGenericType)
            {
                var gType = (IGenericType)type;
                if (gType.GenericParameters.Count > 0 && gType.ElementClassification == TypeElementClassification.None)
                {
                    this.TranslateInternal(gType.MakeGenericClosure(gType.GenericParameters));
                    return;
                }
            }

            this.TranslateInternal(type);
        }

        private void TranslateInternal(IType type, bool ignoreParent = false)
        {
            IntermediateSpanTranslationClasses identifierClass = IntermediateSpanTranslationClasses.None;
            var scopeCoercionSet = from d in BuildTrail
                                   where d is IIntermediateTypeParent
                                   select ((IIntermediateTypeParent)d).ScopeCoercions;
            var buildTrailLimit = BuildTrail.Where(k => k is IIntermediateNamespaceDeclaration).Cast<IIntermediateNamespaceDeclaration>();
            switch (type.ElementClassification)
            {
                case TypeElementClassification.None:
                    if (!ignoreParent)
                        if (type.Parent is IType && ((type.Parent is IIntermediateType && !BuildTrail.Contains((IIntermediateType)type.Parent)) || !(type.Parent is IIntermediateType)) && !(type is IGenericParameter))
                        {
                            Translate((IType)type.Parent);
                            this.WriteOperator(".");
                        }
                    switch (type.Type)
                    {
                        case TypeKind.Class:
                            if (type.IsGenericTypeParameter)
                                identifierClass = IntermediateSpanTranslationClasses.GenericParameterReference;
                            else
                            {
                                this.ReferenceDeclaration((IClassType)type);
                                identifierClass = IntermediateSpanTranslationClasses.UserType;
                            }
                            break;
                        case TypeKind.Delegate:
                            this.ReferenceDeclaration((IDelegateType)type);
                            identifierClass = IntermediateSpanTranslationClasses.UserDelegateType;
                            break;
                        case TypeKind.Enumeration:
                            this.ReferenceDeclaration((IEnumType)type);
                            identifierClass = IntermediateSpanTranslationClasses.UserEnumType;
                            break;
                        case TypeKind.Interface:
                            this.ReferenceDeclaration((IInterfaceType)type);
                            identifierClass = IntermediateSpanTranslationClasses.UserInterfaceType;
                            break;
                        case TypeKind.Struct:
                            this.ReferenceDeclaration((IStructType)type);
                            identifierClass = IntermediateSpanTranslationClasses.UserStructType;
                            break;
                        case TypeKind.Dynamic:
                            this.WriteKeyword(CSharpKeywords.Dynamic);
                            return;
                        default:
                            break;
                    }
                    if (NeedsNamespacePrinted(type, scopeCoercionSet, buildTrailLimit))
                    {
                        var buildTrailNamespace = (from btn in buildTrailLimit
                                                   where type.NamespaceName.StartsWith(btn.FullName + ".")
                                                   select btn).FirstOrDefault();
                        string[] breakdown;
                        /* *
                         * When it does need the namespace look for a namespace
                         * within the build trail which most matches the type's namespace.
                         * *
                         * Limit the name to what follows, in most cases this shouldn't
                         * occur except when AllowPartials = false, because the limited
                         * set of usings it generates are to the top-most namespace with
                         * elements within it.
                         * */
                        if (buildTrailNamespace != null)
                            breakdown = type.NamespaceName.Substring(buildTrailNamespace.FullName.Length + 1).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        else
                            breakdown = type.NamespaceName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < breakdown.Length; i++)
                        {
                            if (i > 0)
                                this.WriteOperator(".");
                            var currentIdentifier = breakdown[i];
                            this.Write(IntermediateSpanTranslationClasses.Identifier, currentIdentifier);
                        }
                        this.WriteOperator(".");
                    }

                    var coreType = type.IdentityManager.ObtainCoreType(type);
                    if (coreType == RuntimeCoreType.None || !autoformTypes.Contains(coreType))
                        this.Write(identifierClass, type.Name);
                    else
                    {
                        switch (coreType)
                        {
                            case RuntimeCoreType.Byte:
                                this.WriteKeyword(CSharpKeywords.Byte);
                                break;
                            case RuntimeCoreType.SByte:
                                this.WriteKeyword(CSharpKeywords.Sbyte);
                                break;
                            case RuntimeCoreType.UInt16:
                                this.WriteKeyword(CSharpKeywords.Ushort);
                                break;
                            case RuntimeCoreType.Int16:
                                this.WriteKeyword(CSharpKeywords.Short);
                                break;
                            case RuntimeCoreType.UInt32:
                                this.WriteKeyword(CSharpKeywords.Uint);
                                break;
                            case RuntimeCoreType.Int32:
                                this.WriteKeyword(CSharpKeywords.Int);
                                break;
                            case RuntimeCoreType.UInt64:
                                this.WriteKeyword(CSharpKeywords.Ulong);
                                break;
                            case RuntimeCoreType.Int64:
                                this.WriteKeyword(CSharpKeywords.Long);
                                break;
                            case RuntimeCoreType.VoidType:
                                this.WriteKeyword(CSharpKeywords.Void);
                                break;
                            case RuntimeCoreType.Boolean:
                                this.WriteKeyword(CSharpKeywords.Bool);
                                break;
                            case RuntimeCoreType.Char:
                                this.WriteKeyword(CSharpKeywords.Char);
                                break;
                            case RuntimeCoreType.Decimal:
                                this.WriteKeyword(CSharpKeywords.Decimal);
                                break;
                            case RuntimeCoreType.Single:
                                this.WriteKeyword(CSharpKeywords.Float);
                                break;
                            case RuntimeCoreType.Double:
                                this.WriteKeyword(CSharpKeywords.Double);
                                break;
                            case RuntimeCoreType.RootType:
                                this.WriteKeyword(CSharpKeywords.Object);
                                break;
                            case RuntimeCoreType.String:
                                this.WriteKeyword(CSharpKeywords.String);
                                break;
                        }
                    }
                    this.Formatter.EndReferenceDeclaration();
                    break;
                case TypeElementClassification.Array:
                    IArrayType iat = (IArrayType)type;
                    this.TranslateInternal(iat.ElementType);
                    this.WriteOperator("[");
                    this.Write(IntermediateSpanTranslationClasses.Operator, ','.Repeat(iat.ArrayRank - 1));
                    this.WriteOperator("]");
                    break;
                case TypeElementClassification.Nullable:
                    this.TranslateInternal(type.ElementType);
                    this.WriteOperator("?");
                    break;
                case TypeElementClassification.Pointer:
                    this.TranslateInternal(type.ElementType);
                    this.WriteOperator("*");
                    break;
                case TypeElementClassification.Reference:
                    this.TranslateInternal(type.ElementType);
                    this.WriteOperator("&");
                    break;
                case TypeElementClassification.GenericTypeDefinition:
                    if (type is IGenericType)
                    {
                        IGenericType gType = (IGenericType)type;
                        if (gType.ElementClassification == TypeElementClassification.GenericTypeDefinition && type.IdentityManager is ICliManager && gType.GenericParameters.Count == 1)
                        {
                            var cliM = (ICliManager)type.IdentityManager;
                            if (gType.ElementType == cliM.ObtainTypeReference(CliRuntimeCoreType.NullableType, type.Assembly))
                            {
                                var fParam = gType.GenericParameters.FirstOrDefault();
                                if (fParam != null && fParam.IsNullable)
                                {
                                    this.TranslateInternal(fParam);
                                    WriteOperator("?");
                                    break;
                                }
                            }
                        }
                        if (gType.ElementClassification == TypeElementClassification.GenericTypeDefinition)
                        {
                            if (gType.Parent is IType)
                            {
                                TranslateInternal((IType)type.Parent);
                                this.WriteOperator(".");
                            }
                            this.TranslateInternal(gType.ElementType, true);
                            int gParamCount = gType.GenericParameters.Count;
                            if (gType.Parent is IGenericType)
                            {
                                var gParent = (IGenericType)gType.Parent;
                                gParamCount -= gParent.GenericParameters.Count;
                            }
                            if (gParamCount > 0)
                            {
                                this.WriteOperator("<");
                                this.Translate((IControlledTypeCollection)gType.GenericParameters);
                                this.WriteOperator(">");
                            }
                        }
                    }
                    break;
            }
        }

        public override void Translate(IControlledTypeCollection collection)
        {
            bool first = true;
            foreach (var type in collection)
            {
                if (first)
                    first = false;
                else
                    this.WriteOperator(", ");
                this.Translate(type);
            }
        }

        protected override void Translate<TParent, TIntermediateParent, TParameter, TIntermediateParameter>(IIntermediateParameterMemberDictionary<TParent, TIntermediateParent, TParameter, TIntermediateParameter> parameters)
        {
            bool first = true;

            foreach (var parameter in parameters.Values)
            {
                if (first)
                {
                    /* Classes can have extensions, but only when static.  Failing that, they should emit a comment noting as such. */
                    if (parameter.Parent is IIntermediateClassMethodMember)
                    {
                        var mParent = (IIntermediateClassMethodMember)parameter.Parent;
                        if (mParent.IsExtensionMethod)
                        {
                            if ((mParent.Parent.SpecialModifier & SpecialClassModifier.Static) == SpecialClassModifier.Static)
                                this.WriteKeyword(CSharpKeywords.This);
                            else
                                this.Translate(new CommentExpression("Extension methods must be in static classes.", DecorationDisplaySide.LeftSide));
                            this.Write(" ");
                        }
                    }
                    first = false;
                }
                else
                    this.WriteOperator(", ");
                this.Translate(parameter);
            }
        }


        private void Translate(IMetadataDefinitionCollection metadataDefinitionCollection, MetadatumTargets target, bool requiresTarget = false)
        {
            if (((int)(target)).CountBits() != 1)
                throw new ArgumentException("Expected a single (MetadatumTargets) target.", "target");
            bool first = true;
            foreach (var metadataDefinition in metadataDefinitionCollection)
            {
                if (first)
                    first = false;
                this.Translate(metadataDefinition, target, requiresTarget);
            }
            if (!first)
                this.WriteLine();
        }

        private void Translate(IMetadataDefinition metadataDefinition, MetadatumTargets target, bool requiresTarget = false)
        {
            if (metadataDefinition.Count > 0)
            {
                this.WriteOperator("[");
                if (requiresTarget)
                {
                    switch (target)
                    {
                        case MetadatumTargets.Assembly:
                            this.WriteKeyword(CSharpKeywords.Assembly);
                            break;
                        case MetadatumTargets.Class:
                        case MetadatumTargets.Delegate:
                        case MetadatumTargets.Enum:
                        case MetadatumTargets.Interface:
                        case MetadatumTargets.Struct:
                            this.WriteKeyword(CSharpKeywords.Type);
                            break;
                        case MetadatumTargets.Event:
                            this.WriteKeyword(CSharpKeywords.Event);
                            break;
                        case MetadatumTargets.Field:
                            this.WriteKeyword(CSharpKeywords.Field);
                            break;
                        case MetadatumTargets.Indexer:
                        case MetadatumTargets.Property:
                            this.WriteKeyword(CSharpKeywords.Property);
                            break;
                        case MetadatumTargets.Constructor:
                        case MetadatumTargets.Method:
                            this.WriteKeyword(CSharpKeywords.Method);
                            break;
                        case MetadatumTargets.Parameter:
                            this.WriteKeyword(CSharpKeywords.Param);
                            break;
                        case MetadatumTargets.ReturnValue:
                            this.WriteKeyword(CSharpKeywords.Return);
                            break;
                        case MetadatumTargets.Module:
                            this.WriteKeyword(CSharpKeywords.Module);
                            break;
                        case MetadatumTargets.All:
                            break;
                        default:
                            break;
                    }
                    this.WriteOperator(": ");
                }
                bool first = true;
                foreach (var metadatum in metadataDefinition)
                {
                    if (first)
                        first = false;
                    else
                        this.WriteOperator(", ");
                    this.Translate(metadatum);
                }
                this.WriteOperator("]");
            }
        }

        private void Translate(IMetadatumDefinition metadatum)
        {
            this.Translate(metadatum.Type);
            this.WriteOperator("(");
            bool first = true;
            var unnamed = (from p in metadatum.Parameters
                           where !(p is IMetadatumDefinitionNamedParameter)
                           select p);
            var named = (from p in metadatum.Parameters
                         where (p is IMetadatumDefinitionNamedParameter)
                         select (IMetadatumDefinitionNamedParameter)p);
            foreach (var parameter in unnamed)
            {
                if (first)
                    first = false;
                else
                    this.WriteOperator(", ");
                parameter.Visit(this);
            }
            foreach (var parameter in named)
            {
                if (first)
                    first = false;
                else
                    this.WriteOperator(", ");
                this.Write(IntermediateSpanTranslationClasses.ParameterReference, parameter.Name);
                this.WriteOperator(" = ");
                parameter.Visit(this);
            }
            this.WriteOperator(")");
        }

        public IIntermediateCodeTranslatorFormatterProvider FormatProvider { get; set; }


        public override void Translate(IMetadatumDefinitionExpressionParameter expression)
        {
            if (expression.Value != null)
                expression.Value.Visit(this);
        }

        protected override IIntermediateCodeNameProvider InitializeNameProvider()
        {
            return this.nameProvider;
        }

        public override string SubToolVersion
        {
            get
            {
                if (subToolVersion == null)
                    subToolVersion = typeof(CSharpCodeTranslator).Assembly.GetName().Version.ToString();
                return this.subToolVersion;
            }
        }

        public override string SubToolName
        {
            get { return "C\u266F Code Translator"; }
        }

        public override string Language
        {
            get { return "C\u266F"; }
        }

        public override void Translate(IUsingBlockStatement statement)
        {
            if (statement == null)
                throw new ArgumentNullException("statement");
            this.WriteKeyword(CSharpKeywords.Using);
            this.Write(" ");
            this.WriteOperator("(");
            this.TranslateLocalDeclarationInternal(statement.ResourceAcquisition, false);
            this.WriteOperator(")");
            this.DenoteNewLine();
            this.TranslateBlockInternal(statement, false, false, statement.Count > 1 || !(statement[0] is IUsingBlockStatement || statement[0] is IUsingExpressionBlockStatement));
        }

        public override void Translate(IUsingExpressionBlockStatement statement)
        {
            if (statement == null)
                throw new ArgumentNullException("statement");
            this.WriteKeyword(CSharpKeywords.Using);
            this.Write(" ");
            this.WriteOperator("(");
            statement.ResourceAcquisition.Visit(this);
            this.WriteOperator(")");
            this.DenoteNewLine();
            this.TranslateBlockInternal(statement, true, increaseIndent: statement.Count > 1 || !(statement[0] is IUsingBlockStatement || statement[0] is IUsingExpressionBlockStatement));
        }

        public override void Translate(IThrowStatement statement)
        {
            if (statement == null)
                throw new ArgumentNullException("statement");
            this.WriteKeyword(CSharpKeywords.Throw);
            this.Write(" ");
            statement.ThrowTarget.Visit(this);
            this.Write(";");
            this.DenoteNewLine();
        }

        public override void Translate(ILockStatement statement)
        {
            if (statement == null)
                throw new ArgumentNullException("statement");
            this.WriteKeyword(CSharpKeywords.Lock);
            this.Write(" ");
            this.WriteOperator("(");
            statement.MonitorLock.Visit(this);
            this.WriteOperator(")");
            this.DenoteNewLine();
            this.TranslateBlockInternal(statement, true);
        }

        public override void Translate(IDefaultValueExpression defaultValueExpression)
        {
            if (defaultValueExpression == null)
                throw new ArgumentNullException("defaultValueExpression");
            this.WriteKeyword(CSharpKeywords.Default);
            this.WriteOperator("(");
            this.Translate(defaultValueExpression.TypeToDefault);
            this.WriteOperator(")");
        }

        public override void Translate(IYieldReturnStatement statement)
        {
            this.WriteKeyword(CSharpKeywords.Yield);
            this.Write(" ");
            this.WriteKeyword(CSharpKeywords.Return);
            this.Write(" ");
            statement.YieldedResult.Visit(this);
            this.Write(";");
            this.DenoteNewLine();
        }

        public override void Translate(IYieldBreakStatement statement)
        {
            this.WriteKeyword(CSharpKeywords.Yield);
            this.Write(" ");
            this.WriteKeyword(CSharpKeywords.Break);
            this.Write(";");
            this.DenoteNewLine();
        }

        public override void Translate(IWhileStatement statement)
        {
            this.WriteKeyword(CSharpKeywords.While);
            this.Write(" ");
            this.WriteOperator("(");
            statement.Condition.Visit(this);
            this.WriteOperator(")");
            this.DenoteNewLine();
            this.TranslateBlockInternal(statement, true, false, true);
        }

    }
}
