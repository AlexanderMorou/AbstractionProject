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
    ///// \<summary\>{\<para\>Creates a C\&\#9839; compiler warning, relative to the abstract model, \(level :z\) \&\#35;:z\:\</para\>}\<para\>[^<]*\</para\>\</summary\>:b*\n{(:b*[/][^\n]*\n)*:b*}public static {:i(\<:i(\<:i\>)@(, :i)*\>)} WarningCS{:z}
    /// <summary>
    /// 
    /// </summary>
    public static partial class CSharpCompilerMessages
    {
        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 4) &#35;28:</para><para><include file='..\..\Properties\Resources.resx' path='root/data[@name="CSharpWarnings_CS0028"]/value/text()'/></para></summary>
        /// <param name="method">The <see cref="IIntermediateClassMethodMember"/></param>
        public static ICompilerSourceModelWarning<IIntermediateClassMethodMember> WarningCS0028(IIntermediateClassMethodMember method)
        {
            //ToDo: Add location information to methods.
            return new CompilerSourceModelWarning<IIntermediateClassMethodMember>(CS0028, method, null, LineColumnPair.Zero, LineColumnPair.Zero, method.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 4) &#35;28:</para><para><include file='..\..\Properties\Resources.resx' path='root/data[@name="CSharpWarnings_CS0028"]/value/text()'/></para></summary>
        public static ICompilerSourceModelWarning<IIntermediateStructMethodMember> WarningCS0028(IIntermediateStructMethodMember method)
        {
            //ToDo: Add location information to methods.
            return new CompilerSourceModelWarning<IIntermediateStructMethodMember>(CS0028, method, null, LineColumnPair.Zero, LineColumnPair.Zero, method.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 3) &#35;67:</para><para><include file='..\..\Properties\Resources.resx' path='root/data[@name="CSharpWarnings_CS0067"]/value/text()'/></para></summary>
        /// <param name="event">The <see cref="IIntermediateClassEventMember"/> which is never used.</param>
        public static ICompilerSourceModelWarning<IIntermediateClassEventMember> WarningCS0067(IIntermediateClassEventMember @event)
        {
            return new CompilerSourceModelWarning<IIntermediateClassEventMember>(CS0067, @event, null, LineColumnPair.Zero, LineColumnPair.Zero, @event.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 3) &#35;67:</para><para><include file='..\..\Properties\Resources.resx' path='root/data[@name="CSharpWarnings_CS0067"]/value/text()'/></para></summary>
        /// <param name="event">The <see cref="IIntermediateStructEventMember"/> which is never used.</param>
        public static ICompilerSourceModelWarning<IIntermediateStructEventMember> WarningCS0067(IIntermediateStructEventMember @event)
        {
            return new CompilerSourceModelWarning<IIntermediateStructEventMember>(CS0067, @event, null, LineColumnPair.Zero, LineColumnPair.Zero, @event.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 4) &#35;78:</para><para><include file='..\..\Properties\Resources.resx' path='root/data[@name="CSharpWarnings_CS0078"]/value/text()'/></para></summary>
        /// <param name="expression">The <see cref="IPrimitiveExpression{T}"/> of type <see cref="UInt64"/>
        /// which contains the potentially confusing suffix.</param>
        public static ICompilerSourceModelWarning<IPrimitiveExpression<ulong>> WarningCS0078(IPrimitiveExpression<ulong> expression)
        {
            var start = expression.Start ?? LineColumnPair.Zero;
            var end = expression.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<IPrimitiveExpression<ulong>>(CS0078, expression, expression.FileName, start, end);
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 3) &#35;105:</para><para><include file='..\..\Properties\Resources.resx' path='root/data[@name="CSharpWarnings_CS0105"]/value/text()'/></para></summary>
        /// <param name="coercion">The <see cref="INamespaceInclusionScopeCoercion"/> which appeared previously in the 
        /// active scope.</param>
        public static ICompilerSourceModelWarning<INamespaceInclusionScopeCoercion> WarningCS0105(INamespaceInclusionScopeCoercion coercion)
        {
            var start = coercion.Start ?? LineColumnPair.Zero;
            var end = coercion.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<INamespaceInclusionScopeCoercion>(CS0105, coercion, coercion.FileName, start, end, coercion.Namespace);
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 3) &#35;105:</para><para><include file='..\..\Properties\Resources.resx' path='root/data[@name="CSharpWarnings_CS0105"]/value/text()'/></para></summary>
        /// <param name="coercion">The <see cref="INamespaceInclusionRenameScopeCoercion"/> which appeared previously in the 
        /// active scope.</param>
        public static ICompilerSourceModelWarning<INamespaceInclusionRenameScopeCoercion> WarningCS0105(INamespaceInclusionRenameScopeCoercion coercion)
        {
            var start = coercion.Start ?? LineColumnPair.Zero;
            var end = coercion.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<INamespaceInclusionRenameScopeCoercion>(CS0105, coercion, coercion.FileName, start, end, coercion.NewName);
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 3) &#35;105:</para><para><include file='..\..\Properties\Resources.resx' path='root/data[@name="CSharpWarnings_CS0105"]/value/text()'/></para></summary>
        /// <param name="coercion">The <see cref="ITypeInclusionScopeCoercion"/> which appeared previously in the 
        /// active scope.</param>
        public static ICompilerSourceModelWarning<ITypeInclusionScopeCoercion> WarningCS0105(ITypeInclusionScopeCoercion coercion)
        {
            var start = coercion.Start ?? LineColumnPair.Zero;
            var end = coercion.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<ITypeInclusionScopeCoercion>(CS0105, coercion, coercion.FileName, start, end, coercion.IncludedType.FullName);
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 3) &#35;105:</para><para><include file='..\..\Properties\Resources.resx' path='root/data[@name="CSharpWarnings_CS0105"]/value/text()'/></para></summary>
        /// <param name="coercion">The <see cref="ITypeInclusionRenameScopeCoercion"/> which appeared previously in the 
        /// active scope.</param>
        public static ICompilerSourceModelWarning<ITypeInclusionRenameScopeCoercion> WarningCS0105(ITypeInclusionRenameScopeCoercion coercion)
        {
            var start = coercion.Start ?? LineColumnPair.Zero;
            var end = coercion.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<ITypeInclusionRenameScopeCoercion>(CS0105, coercion, coercion.FileName, start, end, coercion.NewName);
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 2) &#35;108:</para><para><include file='..\..\Properties\Resources.resx' path='root/data[@name="CSharpWarnings_CS0108"]/value/text()'/></para></summary>
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

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 4) &#35;109:</para><para><include file='..\..\Properties\Resources.resx' path='root/data[@name="CSharpWarnings_CS0109"]/value/text()'/></para></summary>
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

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 2) &#35;114:</para><para><include file='..\..\Properties\Resources.resx' path='root/data[@name="CSharpWarnings_CS0114"]/value/text()'/></para></summary>
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

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 2) &#35;162:</para><para><include file='..\..\Properties\Resources.resx' path='root/data[@name="CSharpWarnings_CS0162"]/value/text()'/></para></summary>
        /// <param name="unreachableStatement">The <see cref="IStatement"/> which is unreachable within the method body.</param>
        public static ICompilerSourceModelWarning<IStatement> WarningCS0162(IStatement unreachableStatement)
        {
            //ToDo: Add location information to statements.
            return new CompilerSourceModelWarning<IStatement>(CS0162, unreachableStatement, null, LineColumnPair.Zero, LineColumnPair.Zero, unreachableStatement.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 2) &#35;162:</para><para><include file='..\..\Properties\Resources.resx' path='root/data[@name="CSharpWarnings_CS0162"]/value/text()'/></para></summary>
        /// <param name="unreachableExpression">The <see cref="IExpression"/> which is unreachable based off of the
        /// short-circuiting of the expression.</param>
        public static ICompilerSourceModelWarning<IExpression> WarningCS0162(IExpression unreachableExpression)
        {
            var start = unreachableExpression.Start ?? LineColumnPair.Zero;
            var end = unreachableExpression.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<IExpression>(CS0162, unreachableExpression, unreachableExpression.FileName, start, end, unreachableExpression.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 2) &#35;164:</para><para><include file='..\..\Properties\Resources.resx' path='root/data[@name="CSharpWarnings_CS0164"]/value/text()'/></para></summary>
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

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 3) &#35;169:</para><para><include file='..\..\Properties\Resources.resx' path='root/data[@name="CSharpWarnings_CS0169"]/value/text()'/></para></summary>
        /// <param name="unusedPrivateField">The <see cref="IIntermediateClassFieldMember"/> which is
        /// defined, but never used.</param>
        public static ICompilerSourceModelWarning<IIntermediateClassFieldMember> WarningCS0169(IIntermediateClassFieldMember unusedPrivateField)
        {
            //ToDo: Add location information to fields.
            return new CompilerSourceModelWarning<IIntermediateClassFieldMember>(CS0169, unusedPrivateField, null, LineColumnPair.Zero, LineColumnPair.Zero, unusedPrivateField.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 3) &#35;169:</para><para><include file='..\..\Properties\Resources.resx' path='root/data[@name="CSharpWarnings_CS0169"]/value/text()'/></para></summary>
        /// <param name="unusedPrivateField">The <see cref="IIntermediateStructFieldMember"/> which is
        /// defined, but never used.</param>
        public static ICompilerSourceModelWarning<IIntermediateStructFieldMember> WarningCS0169(IIntermediateStructFieldMember unusedPrivateField)
        {
            //ToDo: Add location information to fields.
            return new CompilerSourceModelWarning<IIntermediateStructFieldMember>(CS0169, unusedPrivateField, null, LineColumnPair.Zero, LineColumnPair.Zero, unusedPrivateField.UniqueIdentifier.ToString());
        }

        /// <summary><para>Creates a C&#9839; compiler warning, relative to the abstract model, (level 1) &#35;183:</para><para><include file='..\..\Properties\Resources.resx' path='root/data[@name="CSharpWarnings_CS0183"]/value/text()'/></para></summary>
        /// <param name="relationalConstant">The <see cref="ICSharpRelationalExpression"/>
        /// which always yields true, because the <see cref="IBinaryOperationExpression{TLeft, TRight}.LeftSide"/>
        /// is always the type provided within <see cref="IBinaryOperationExpression{TLeft, TRight}.RightSide"/>.</param>
        public static ICompilerSourceModelWarning<ICSharpRelationalExpression> WarningCS0183(ICSharpRelationalExpression relationalConstant)
        {
            var start = relationalConstant.Start ?? LineColumnPair.Zero;
            var end = relationalConstant.End ?? LineColumnPair.Zero;
            return new CompilerSourceModelWarning<ICSharpRelationalExpression>(CS0183, relationalConstant, relationalConstant.FileName, start, end, relationalConstant.ToString());
        }
    }
}
