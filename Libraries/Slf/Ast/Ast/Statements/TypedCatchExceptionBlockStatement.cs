using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public class TypedCatchExceptionBlockStatement :
        BlockStatement,
        ITypedCatchExceptionBlockStatement
    {
        private ITryStatement parent;
        private ITypedLocalMember _typedLocal;
        public TypedCatchExceptionBlockStatement(ITryStatement parent, IType exceptionType)
            : base(parent.Parent)
        {
            this.parent = parent;
            this.CaughtException = exceptionType;
            this._typedLocal = this.Locals.Add(new TypedName("_exception_", exceptionType));
            this._typedLocal.AutoDeclare = false;
        }

        public IType CaughtException { get; set; }

        public new ITryStatement Parent { get { return parent; } }

        public ITypedLocalMember ExceptionLocal
        {
            get
            {
                return this._typedLocal;
            }
        }
    }
    public class TypeNamedCatchExceptionBlockStatement :
        BlockStatement,
        ITypeNamedCatchExceptionBlockStatement
    {
        private ITryStatement parent;
        private ITypedLocalMember _typedLocal;
        public TypeNamedCatchExceptionBlockStatement(ITryStatement parent, TypedName nameAndExceptionType)
            : base(parent.Parent)
        {
            this.parent = parent;
            this.CaughtException = nameAndExceptionType.TypeReference;
            this._typedLocal = this.Locals.Add(nameAndExceptionType);
            this._typedLocal.AutoDeclare = false;
        }

        public IType CaughtException { get; set; }

        public new ITryStatement Parent { get { return parent; } }

        public ILocalDeclarationsStatement LocalVariableDeclaration
        {
            get { return this._typedLocal.GetDeclarationStatement(); }
        }

        public ITypedLocalMember LocalVariable
        {
            get { return this._typedLocal; }
        }
    }
}
