using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using AllenCopeland.Abstraction.OldCodeGen.Expression;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using AllenCopeland.Abstraction.OldCodeGen._Internal;
using AllenCopeland.Abstraction.OldCodeGen.Statements;
using System.Collections.ObjectModel;
using AllenCopeland.Abstraction.OldCodeGen.Translation;

namespace AllenCopeland.Abstraction.OldCodeGen.Types.Members
{
    public class TypeConversionOverloadMember :
        Member<IMemberParentType, CodeSnippetTypeMember>,
        ITypeConversionOverloadMember
    {
        /// <summary>
        /// Data member for <see cref="Requirement"/>.
        /// </summary>
        private TypeConversionRequirement requirement;
        /// <summary>
        /// Data member for <see cref="Direction"/>.
        /// </summary>
        private TypeConversionDirection direction;
        /// <summary>
        /// Data member for <see cref="CoercionType"/>.
        /// </summary>
        private ITypeReference coercionType;
        private ICollection<string> definedLabelNames;
        /// <summary>
        /// Data member for <see cref="Source"/>.
        /// </summary>
        private ITypeConversionOverloadSource source;
        private IStatementBlock statements;

        /// <summary>
        /// Creates a new <see cref="TypeConversionOverloadMember"/> instance
        /// with the <paramref name="parentTarget"/> provided.
        /// </summary>
        /// <param name="parentTarget">The containing type of the <see cref="TypeConversionOverloadMember"/></param>
        public TypeConversionOverloadMember(IMemberParentType parentTarget)
            : base(null, parentTarget)
        {
        }

        public override string GetUniqueIdentifier()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Requirement.ToString());
            sb.Append(" ");
            switch (direction)
            {
                case TypeConversionDirection.ToContainingType:
                    sb.Append(this.parentTarget.GetTypeName(CodeGeneratorHelper.DefaultTranslatorOptions));
                    sb.Append("->");
                    sb.Append(this.CoercionType.TypeInstance.GetTypeName(CodeGeneratorHelper.DefaultTranslatorOptions, this.CoercionType.TypeParameters.ToArray()));
                    break;
                case TypeConversionDirection.FromContainingType:
                    sb.Append(this.CoercionType.TypeInstance.GetTypeName(CodeGeneratorHelper.DefaultTranslatorOptions, this.CoercionType.TypeParameters.ToArray()));
                    sb.Append("->");
                    sb.Append(this.parentTarget.GetTypeName(CodeGeneratorHelper.DefaultTranslatorOptions));
                    break;
            }
            return sb.ToString();
        }

        protected override IMemberReferenceExpression OnGetReference()
        {
            throw new NotSupportedException("Coercion members cannot be referenced as they alter expression interpretation/execution.");
        }

        public override CodeSnippetTypeMember GenerateCodeDom(ICodeDOMTranslationOptions options)
        {
            IIntermediateCodeTranslator iict = null;
            if (options.LanguageProvider != null)
            {
                if (options.LanguageProvider is CSharpCodeProvider)
                    iict = _OIL._Core.DefaultCSharpCodeTranslator;
                //else if (options.LanguageProvider is VBCodeProvider)
                //{
                //    iict = _OIL._Core.DefaultVBCodeTranslator;
                //    iict.Options = CodeGeneratorHelper.DefaultTranslatorOptions;
                //}
            }
            else
                iict = _OIL._Core.DefaultCSharpCodeTranslator;
            iict.Target = new StringFormWriter();
            iict.TranslateMember(this);
            return new CodeSnippetTypeMember(iict.Target.ToString());
        }

        #region ITypeConversionOverloadMember Members

        /// <summary>
        /// Returns/sets whether the conversion overload is implicit or explicit.
        /// </summary>
        public TypeConversionRequirement Requirement
        {
            get
            {
                return this.requirement;
            }
            set
            {
                this.requirement = value;
            }
        }

        /// <summary>
        /// Returns/sets whether the conversion overload is from the containing type or 
        /// to the containing type.
        /// </summary>
        public TypeConversionDirection Direction
        {
            get
            {
                return this.direction;
            }
            set
            {
                this.direction = value;
            }
        }

        /// <summary>
        /// Returns/sets the type which is coerced by the overload.
        /// </summary>
        public ITypeReference CoercionType
        {
            get
            {
                return this.coercionType;
            }
            set
            {
                this.coercionType = value;
            }
        }


        public ITypeConversionOverloadSource Source
        {
            get
            {
                if (this.source == null)
                    this.source = this.InitializeSource();
                return this.source;
            }
        }

        private ITypeConversionOverloadSource InitializeSource()
        {
            return new TypeConversionOverloadSource();
        }

        #endregion

        #region IBlockParent Members

        public IStatementBlock Statements
        {
            get
            {
                if (this.statements == null)
                    this.statements = new StatementBlock(this);
                return this.statements;
            }
        }

        public ICollection<string> DefinedLabelNames
        {
            get
            {
                if (this.definedLabelNames == null)
                    this.definedLabelNames = new Collection<string>();
                return this.definedLabelNames;
            }
        }

        #endregion

        #region IStatementBlockInsertBase Members
        public void Add(IStatement statement)
        {
            this.Statements.Add(statement);
        }

        public IBlockStatement NewBlock()
        {
            return this.Statements.NewBlock();
        }

        public ISwitchStatement SelectCase(IExpression caseSwitch)
        {
            return this.Statements.SelectCase(caseSwitch);
        }

        public ISwitchStatement SelectCase(IExpression caseSwitch, IExpression[][] cases, IStatement[][] caseStatements)
        {
            return this.Statements.SelectCase(caseSwitch, cases, caseStatements);
        }

        public ICrementStatement Crement(IAssignStatementTarget target, CrementType crementType, CrementOperation operation)
        {
            return this.Statements.Crement(target, crementType, operation);
        }

        public ICrementStatement Preincrement(IAssignStatementTarget target)
        {
            return this.Statements.Preincrement(target);
        }

        public ICrementStatement Postincrement(IAssignStatementTarget target)
        {
            return this.Statements.Postincrement(target);
        }

        public ICrementStatement Increment(IAssignStatementTarget target, CrementType crementType)
        {
            return this.Statements.Increment(target, crementType);
        }

        public ICrementStatement Predecrement(IAssignStatementTarget target)
        {
            return this.Statements.Predecrement(target);
        }

        public ICrementStatement Postdecrement(IAssignStatementTarget target)
        {
            return this.Statements.Postdecrement(target);
        }

        public ICrementStatement Decrement(IAssignStatementTarget target, CrementType crementType)
        {
            return this.Statements.Decrement(target, crementType);
        }

        public IConditionStatement IfThen(IExpression condition, params IStatement[] trueStatements)
        {
            return this.Statements.IfThen(condition, trueStatements);
        }

        public IConditionStatement IfThen(IExpression condition, IStatement[] trueStatements, IStatement[] falseStatements)
        {
            return this.Statements.IfThen(condition, trueStatements, falseStatements);
        }

        public IIterationStatement Iterate(IStatement init, IStatement increment, IExpression test)
        {
            return this.Statements.Iterate(init, increment, test);
        }

        public IAssignStatement Assign(IAssignStatementTarget target, IExpression value)
        {
            return this.Statements.Assign(target, value);
        }

        public IEnumeratorStatement Enumerate(IMemberParentExpression enumeratorSource, ITypeReference itemType)
        {
            return this.Statements.Enumerate(enumeratorSource, itemType);
        }

        public ILocalDeclarationStatement DefineLocal(IStatementBlockLocalMember local)
        {
            return this.Statements.DefineLocal(local);
        }

        public IBreakStatement Break()
        {
            return this.Statements.Break();
        }

        public IBreakStatement Break(IExpression conditionForBreak)
        {
            return this.Statements.Break(conditionForBreak);
        }

        public ISimpleStatement CallMethod(IMethodReferenceExpression method, params IExpression[] arguments)
        {
            return this.Statements.CallMethod(method, arguments);
        }
        public ISimpleStatement CallMethod(IMethodInvokeExpression method)
        {
            return this.Statements.CallMethod(method);
        }

        public IStatementBlockLocalMembers Locals
        {
            get
            {
                return this.Statements.Locals;
            }
        }
        public IReturnStatement Return(IExpression result)
        {
            return this.Statements.Return(result);
        }

        public IReturnStatement Return()
        {
            return this.Statements.Return();
        }


        #endregion


        protected override IMemberReferenceExpression OnGetReference(IMemberParentExpression owner)
        {
            throw new NotSupportedException("Coercion members cannot be referenced as they alter expression interpretation/execution.");
        }
    }
}
