using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Slf.Ast.Expressions;
using AllenCopeland.Abstraction.Slf.Ast.Statements;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    public static partial class CSharpCompilerMessages
    {

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 4) &#35;28:</para><para>{0} has the wrong signature to be an entry point </para></summary>
        /// <param name="method">The <see cref="IIntermediateClassMethodMember"/></param>
        public static ICompilerSourceModelWarning<IIntermediateClassMethodMember> WarningCS0028(IIntermediateClassMethodMember method)
        {
            //ToDo: Add location information to methods.
            return new CompilerSourceModelWarning<IIntermediateClassMethodMember>(CS0028, method, null, LineColumnPair.Zero, LineColumnPair.Zero, method.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 4) &#35;28:</para><para>{0} has the wrong signature to be an entry point </para></summary>
        public static ICompilerSourceModelWarning<IIntermediateStructMethodMember> WarningCS0028(IIntermediateStructMethodMember method)
        {
            //ToDo: Add location information to methods.
            return new CompilerSourceModelWarning<IIntermediateStructMethodMember>(CS0028, method, null, LineColumnPair.Zero, LineColumnPair.Zero, method.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 3) &#35;67:</para><para>The event {0} is never used</para></summary>
        /// <param name="event">The <see cref="IIntermediateClassEventMember"/> which is never used.</param>
        public static ICompilerSourceModelWarning<IIntermediateClassEventMember> WarningCS0067(IIntermediateClassEventMember @event)
        {
            return new CompilerSourceModelWarning<IIntermediateClassEventMember>(CS0067, @event, null, LineColumnPair.Zero, LineColumnPair.Zero, @event.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 3) &#35;67:</para><para>The event {0} is never used</para></summary>
        /// <param name="event">The <see cref="IIntermediateStructEventMember"/> which is never used.</param>
        public static ICompilerSourceModelWarning<IIntermediateStructEventMember> WarningCS0067(IIntermediateStructEventMember @event)
        {
            return new CompilerSourceModelWarning<IIntermediateStructEventMember>(CS0067, @event, null, LineColumnPair.Zero, LineColumnPair.Zero, @event.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 4) &#35;78:</para><para>The 'l' suffix is easily confused with the digit '1' -- use 'L' for clarity</para></summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> of type <see cref="UInt64"/>
        /// which contains the potentially confusing suffix.</param>
        public static ICompilerSourceModelWarning<IPrimitiveExpression<ulong>> WarningCS0078(IPrimitiveExpression<ulong> expression)
        {
            var start = expression.Start ?? LineColumnPair.Zero;
            var end = expression.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<IPrimitiveExpression<ulong>>(CS0078, expression, expression.FileName, start, end);
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 3) &#35;105:</para><para>The using directive for '<paramref name="coercion"/>' appeared previously in this namespace</para></summary>
        /// <param name="coercion">The <see cref="INamespaceInclusionScopeCoercion"/> which appeared previously in the 
        /// active scope.</param>
        public static ICompilerSourceModelWarning<INamespaceInclusionScopeCoercion> WarningCS0105(INamespaceInclusionScopeCoercion coercion)
        {
            var start = coercion.Start ?? LineColumnPair.Zero;
            var end = coercion.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<INamespaceInclusionScopeCoercion>(CS0105, coercion, coercion.FileName, start, end, coercion.Namespace);
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 3) &#35;105:</para><para>The using directive for '<paramref name="coercion"/>' appeared previously in this namespace</para></summary>
        /// <param name="coercion">The <see cref="INamespaceInclusionRenameScopeCoercion"/> which appeared previously in the 
        /// active scope.</param>
        public static ICompilerSourceModelWarning<INamespaceInclusionRenameScopeCoercion> WarningCS0105(INamespaceInclusionRenameScopeCoercion coercion)
        {
            var start = coercion.Start ?? LineColumnPair.Zero;
            var end = coercion.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<INamespaceInclusionRenameScopeCoercion>(CS0105, coercion, coercion.FileName, start, end, coercion.NewName);
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 3) &#35;105:</para><para>The using directive for <paramref name="coercion"/> appeared previously in this namespace</para></summary>
        /// <param name="coercion">The <see cref="ITypeInclusionScopeCoercion"/> which appeared previously in the 
        /// active scope.</param>
        public static ICompilerSourceModelWarning<ITypeInclusionScopeCoercion> WarningCS0105(ITypeInclusionScopeCoercion coercion)
        {
            var start = coercion.Start ?? LineColumnPair.Zero;
            var end = coercion.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<ITypeInclusionScopeCoercion>(CS0105, coercion, coercion.FileName, start, end, coercion.IncludedType.FullName);
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 3) &#35;105:</para><para>The using directive for '<paramref name="coercion"/>' appeared previously in this namespace</para></summary>
        /// <param name="coercion">The <see cref="ITypeInclusionRenameScopeCoercion"/> which appeared previously in the 
        /// active scope.</param>
        public static ICompilerSourceModelWarning<ITypeInclusionRenameScopeCoercion> WarningCS0105(ITypeInclusionRenameScopeCoercion coercion)
        {
            var start = coercion.Start ?? LineColumnPair.Zero;
            var end = coercion.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<ITypeInclusionRenameScopeCoercion>(CS0105, coercion, coercion.FileName, start, end, coercion.NewName);
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 2) &#35;108:</para><para>{0} hides inherited member {1}. Use the new keyword if hiding was intended.</para></summary>
        public static ICompilerSourceModelWarning<TMemberType, TBaseMemberType> WarningCS0108<TMemberType, TBaseMemberType>(TMemberType member, TBaseMemberType original)
            where TMemberType :
                IIntermediateMember,
                ISourceElement
            where TBaseMemberType :
                IMember
        {
            var start = member.Start ?? LineColumnPair.Zero;
            var end = member.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<TMemberType, TBaseMemberType>(CS0108, member, original, member.FileName, start, end, member.UniqueIdentifier.ToString(), original.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 4) &#35;109:</para><para>The member '<paramref name="member"/>' does not hide an inherited member. The new keyword is not required</para></summary>
        /// <param name="member">The <see cref="IIntermediateMember"/> that is also a <see cref="ISourceElement"/>
        /// which does not hide an inherited member, but the definition of the member indicates it does.</param>
        public static ICompilerSourceModelWarning<TMember> WarningCS0109<TMember>(TMember member)
            where TMember :
                IIntermediateMember,
                ISourceElement
        {
            var start = member.Start ?? LineColumnPair.Zero;
            var end = member.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<TMember>(CS0109, member, member.FileName, start, end, member.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 2) &#35;114:</para><para>'<paramref name="member"/>' hides inherited member '<paramref name="original"/>'. To make the current method override that implementation, add the override keyword. Otherwise add the new keyword.</para></summary>
        /// <param name="member">The <see cref="IIntermediateMember"/> which is also a <see cref="ISourceElement"/> that hides the 
        /// <paramref name="original"/> definition without overriding it.</param>
        /// <param name="original">The <see cref="IMember"/> which is eclipsed by <paramref name="member"/>
        /// but not overridden by it.</param>
        public static ICompilerSourceModelWarning<TMemberType, TBaseMemberType> WarningCS0114<TMemberType, TBaseMemberType>(TMemberType member, TBaseMemberType original)
            where TMemberType :
                IIntermediateMember,
                ISourceElement
            where TBaseMemberType :
                IMember
        {
            var start = member.Start ?? LineColumnPair.Zero;
            var end = member.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<TMemberType, TBaseMemberType>(CS0114, member, original, member.FileName, start, end, member.UniqueIdentifier.ToString(), original.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 2) &#35;162:</para><para>Unreachable code detected</para></summary>
        /// <param name="unreachableStatement">The <see cref="IStatement"/> which is unreachable within the method body.</param>
        public static ICompilerSourceModelWarning<IStatement> WarningCS0162(IStatement unreachableStatement)
        {
            //ToDo: Add location information to statements.
            return new CompilerSourceModelWarning<IStatement>(CS0162, unreachableStatement, null, LineColumnPair.Zero, LineColumnPair.Zero, unreachableStatement.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 2) &#35;162:</para><para>Unreachable code detected</para></summary>
        /// <param name="unreachableExpression">The <see cref="IExpression"/> which is unreachable based off of the
        /// short-circuiting of the expression.</param>
        public static ICompilerSourceModelWarning<IExpression> WarningCS0162(IExpression unreachableExpression)
        {
            var start = unreachableExpression.Start ?? LineColumnPair.Zero;
            var end = unreachableExpression.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<IExpression>(CS0162, unreachableExpression, unreachableExpression.FileName, start, end, unreachableExpression.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 2) &#35;164:</para><para>This label has not been referenced</para></summary>
        /// <param name="unreferencedLabel">The <see cref="ILabelStatement"/> which was not referenced 
        /// within the defining method body.</param>
        public static ICompilerSourceModelWarning<ILabelStatement> WarningCS0164(ILabelStatement unreferencedLabel)
        {
            return new CompilerSourceModelWarning<ILabelStatement>(CS0164, unreferencedLabel, null, LineColumnPair.Zero, LineColumnPair.Zero);
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the 
        /// abstract model, (level 3) &#35;168:</para><para>The variable
        /// '<paramref name="assignedButUnusedLocalVariable"/>' is assigned but
        /// its value is never used</para></summary>
        /// <param name="assignedButUnusedLocalVariable">The <see cref="ILocalMember"/> which is assigned
        /// a value, but unused within the scope of the method.</param>
        public static ICompilerSourceModelWarning<ILocalMember> WarningCS0168(ILocalMember assignedButUnusedLocalVariable)
        {
            //ToDo: Add location information to local variables.
            return new CompilerSourceModelWarning<ILocalMember>(CS0168, assignedButUnusedLocalVariable, null, LineColumnPair.Zero, LineColumnPair.Zero, assignedButUnusedLocalVariable.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the 
        /// abstract model, (level 3) &#35;169:</para>
        /// <para>The private field '<paramref name="unusedPrivateField"/>' is never used</para></summary>
        /// <param name="unusedPrivateField">The <see cref="IIntermediateClassFieldMember"/> which is
        /// defined, but never used.</param>
        public static ICompilerSourceModelWarning<IIntermediateClassFieldMember> WarningCS0169(IIntermediateClassFieldMember unusedPrivateField)
        {
            //ToDo: Add location information to fields.
            return new CompilerSourceModelWarning<IIntermediateClassFieldMember>(CS0169, unusedPrivateField, null, LineColumnPair.Zero, LineColumnPair.Zero, unusedPrivateField.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the 
        /// abstract model, (level 3) &#35;169:</para>
        /// <para>The private field '<paramref name="unusedPrivateField"/>' is never used</para></summary>
        /// <param name="unusedPrivateField">The <see cref="IIntermediateStructFieldMember"/> which is
        /// defined, but never used.</param>
        public static ICompilerSourceModelWarning<IIntermediateStructFieldMember> WarningCS0169(IIntermediateStructFieldMember unusedPrivateField)
        {
            //ToDo: Add location information to fields.
            return new CompilerSourceModelWarning<IIntermediateStructFieldMember>(CS0169, unusedPrivateField, null, LineColumnPair.Zero, LineColumnPair.Zero, unusedPrivateField.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the 
        /// abstract model, (level 1) &#35;183:</para><para>The given expression is always of the provided (<see cref="IBinaryOperationExpression{TLeft,TRight}.RightSide"/>) type.</para></summary>
        /// <param name="relationalConstant">The <see cref="ICSharpRelationalExpression"/>
        /// which always yields true, because the <see cref="IBinaryOperationExpression{TLeft, TRight}.LeftSide"/>
        /// is always the type provided within <see cref="IBinaryOperationExpression{TLeft, TRight}.RightSide"/>.</param>
        public static ICompilerSourceModelWarning<ICSharpRelationalExpression> WarningCS0183(ICSharpRelationalExpression relationalConstant)
        {
            var start = relationalConstant.Start ?? LineColumnPair.Zero;
            var end = relationalConstant.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<ICSharpRelationalExpression>(CS0183, relationalConstant, relationalConstant.FileName, start, end, relationalConstant.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the 
        /// abstract model, (level 1) &#35;184:</para><para>The given expression is never of the provided ({0}) type</para></summary>
        public static ICompilerSourceModelWarning<ICSharpRelationalExpression> WarningCS0184(ICSharpRelationalExpression relationalConstant)
        {
            var start = relationalConstant.Start ?? LineColumnPair.Zero;
            var end = relationalConstant.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<ICSharpRelationalExpression>(CS0184, relationalConstant, relationalConstant.FileName, start, end, relationalConstant.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the 
        /// abstract model, (level 1) &#35;197:</para><para>Passing {0} as ref or out or taking its address may cause a runtime exception because it is a field of a marshal-by-reference class</para></summary>
        public static ICompilerSourceModelWarning<IDirectionExpression> WarningCS0197(IDirectionExpression directedExpression)
        {

            var start = directedExpression.Directed.Start ?? LineColumnPair.Zero;
            var end = directedExpression.Directed.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<IDirectionExpression>(CS0197, directedExpression, directedExpression.FileName, start, end, directedExpression.Directed.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the 
        /// abstract model, (level 3) &#35;219:</para><para>The variable {0} is assigned but its value is never used</para></summary>
        public static ICompilerSourceModelWarning<ILocalMember> WarningCS0219(ILocalMember targetMember)
        {
            //ToDo: Add location information to local members.
            return new CompilerSourceModelWarning<ILocalMember>(CS0219, targetMember, null, LineColumnPair.Zero, LineColumnPair.Zero, targetMember.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the 
        /// abstract model, (level 2) &#35;251:</para><para>Indexing an array with a negative index (array indices always start at zero)</para></summary>
        public static ICompilerSourceModelWarning<IIndexerReferenceExpression> WarningCS0251(IIndexerReferenceExpression indexingExpression)
        {
            var start = indexingExpression.Start ?? LineColumnPair.Zero;
            var end = indexingExpression.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<IIndexerReferenceExpression>(CS0251, indexingExpression, indexingExpression.FileName, start, end, indexingExpression.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the 
        /// abstract model, (level 2) &#35;251:</para><para>Indexing an array with a negative index (array indices always start at zero)</para></summary>
        public static ICompilerSourceModelWarning<IIndexerReferenceExpression<IClassIndexerMember, IClassType>> WarningCS0251(IIndexerReferenceExpression<IClassIndexerMember, IClassType> indexingExpression)
        {
            var start = indexingExpression.Start ?? LineColumnPair.Zero;
            var end = indexingExpression.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<IIndexerReferenceExpression<IClassIndexerMember, IClassType>>(CS0251, indexingExpression, indexingExpression.FileName, start, end, indexingExpression.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the 
        /// abstract model, (level 2) &#35;251:</para><para>Indexing an array with a negative index (array indices always start at zero)</para></summary>
        public static ICompilerSourceModelWarning<IIndexerReferenceExpression<IStructIndexerMember, IStructType>> WarningCS0251(IIndexerReferenceExpression<IStructIndexerMember, IStructType> indexingExpression)
        {
            var start = indexingExpression.Start ?? LineColumnPair.Zero;
            var end = indexingExpression.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<IIndexerReferenceExpression<IStructIndexerMember, IStructType>>(CS0251, indexingExpression, indexingExpression.FileName, start, end, indexingExpression.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the 
        /// abstract model, (level 2) &#35;251:</para><para>Indexing an array with a negative index (array indices always start at zero)</para></summary>
        public static ICompilerSourceModelWarning<IIndexerSignatureReferenceExpression<IInterfaceIndexerMember, IInterfaceType>> WarningCS0251(IIndexerSignatureReferenceExpression<IInterfaceIndexerMember, IInterfaceType> indexingExpression)
        {
            var start = indexingExpression.Start ?? LineColumnPair.Zero;
            var end = indexingExpression.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<IIndexerSignatureReferenceExpression<IInterfaceIndexerMember, IInterfaceType>>(CS0251, indexingExpression, indexingExpression.FileName, start, end, indexingExpression.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the 
        /// abstract model, (level 2) &#35;252:</para><para>Possible unintended reference
        /// comparison; to get a value comparison, cast the left hand side to type {0}</para></summary>
        public static ICompilerSourceModelWarning<ICSharpInequalityExpression> WarningCS0252(ICSharpInequalityExpression inequalityExpression)
        {
            var start = inequalityExpression.Start ?? LineColumnPair.Zero;
            var end = inequalityExpression.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<ICSharpInequalityExpression>(CS0252, inequalityExpression, inequalityExpression.FileName, start, end, inequalityExpression.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the 
        /// abstract model, (level 2) &#35;253:
        /// </para><para>Possible unintended reference comparison; to get
        /// a value comparison, cast the right hand side to type {0}</para></summary>
        public static ICompilerSourceModelWarning<ICSharpInequalityExpression> WarningCS0253(ICSharpInequalityExpression inequalityExpression)
        {
            var start = inequalityExpression.Start ?? LineColumnPair.Zero;
            var end = inequalityExpression.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<ICSharpInequalityExpression>(CS0253, inequalityExpression, inequalityExpression.FileName, start, end, inequalityExpression.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the 
        /// abstract model, (level 2) &#35;278:</para><para><paramref name="offendingType"/> does not implement the <paramref name="patternName"/>. <paramref name="patternMethod1"/> is ambiguous with <paramref name="patternMethod2"/>.</para></summary>
        public static ICompilerSourceModelWarning<IExpression, IType, IMethodSignatureMember, IMethodSignatureMember> WarningCS0278(IExpression offendingExpression, IType offendingType, string patternName, IMethodSignatureMember patternMethod1, IMethodSignatureMember patternMethod2)
        {
            var start = offendingExpression.Start ?? LineColumnPair.Zero;
            var end = offendingExpression.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<IExpression, IType, IMethodSignatureMember, IMethodSignatureMember>(CS0278, offendingExpression, offendingType, patternMethod1, patternMethod2, offendingExpression.FileName, start, end, offendingType.UniqueIdentifier.ToString(), patternName, patternMethod1.UniqueIdentifier.ToString(), patternMethod2.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the 
        /// abstract model, (level 2) &#35;279:</para>
        /// <para><paramref name="offendingType"/> does not implement the <paramref name="patternName"/>. <paramref name="offendingMethod"/> is either static or not public.</para></summary>
        public static ICompilerSourceModelWarning<IExpression, IType, string, IMethodSignatureMember> WarningCS0279(IExpression offendingExpression, IType offendingType, string patternName, IMethodSignatureMember offendingMethod)
        {
            var start = offendingExpression.Start ?? LineColumnPair.Zero;
            var end = offendingExpression.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<IExpression, IType, string, IMethodSignatureMember>(CS0279, offendingExpression, offendingType, patternName, offendingMethod, offendingExpression.FileName, start, end, offendingType.UniqueIdentifier.ToString(), patternName, offendingMethod.UniqueIdentifier.ToString());
        }
        /// <summary><para>Creates a C&#9839; compiler warning, relative to the 
        /// abstract model, (level 2) &#35;280:</para><para><paramref name="offendingType"/> does not implement the <paramref name="patternName"/>. <paramref name="offendingMethod"/> has the wrong signature.</para></summary>
        public static ICompilerSourceModelWarning<IExpression, IType, string, IMethodSignatureMember> WarningCS0280(IExpression offendingExpression, IType offendingType, string patternName, IMethodSignatureMember offendingMethod)
        {
            var start = offendingExpression.Start ?? LineColumnPair.Zero;
            var end = offendingExpression.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<IExpression, IType, string, IMethodSignatureMember>(CS0280, offendingExpression, offendingType, patternName, offendingMethod, offendingExpression.FileName, start, end, offendingType.UniqueIdentifier.ToString(), patternName, offendingMethod.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative
        /// to the abstract model, (level 3) &#35;282:</para>
        /// <para>There is no defined ordering between fields in multiple
        /// declarations of <paramref name="offendingStruct"/>. To specify an
        /// ordering, all instance fields must be in the same declaration.
        /// </para></summary>
        public static ICompilerSourceModelWarning<IIntermediateStructType> WarningCS0282(IIntermediateStructType offendingStruct)
        {
            //ToDo: Add location information to structs.
            return new CompilerSourceModelWarning<IIntermediateStructType>(CS0282, offendingStruct, null, LineColumnPair.Zero, LineColumnPair.Zero, offendingStruct.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative
        /// to the abstract model, (level 3) &#35;282:</para>
        /// <para>There is no defined ordering between fields in multiple
        /// declarations of <paramref name="offendingClass"/>. To specify an
        /// ordering, all instance fields must be in the same declaration.
        /// </para></summary>
        /// <param name="offendingClass">The <see cref="IIntermediateClassType"/>
        /// which contains one or more partial instance which contains
        /// fields.</param>
        /// <remarks>The ordering of the fields will become inconsistent
        /// unless all instance fields reside within the same partial instance.</remarks>
        public static ICompilerSourceModelWarning<IIntermediateClassType> WarningCS0282(IIntermediateClassType offendingClass)
        {
            //ToDo: Add location information to classes.
            return new CompilerSourceModelWarning<IIntermediateClassType>(CS0282, offendingClass, null, LineColumnPair.Zero, LineColumnPair.Zero, offendingClass.UniqueIdentifier.ToString());
        }


    }
}
