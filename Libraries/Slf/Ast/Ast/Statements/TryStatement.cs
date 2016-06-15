using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using System.Collections;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf.Ast.Statements
{
    public class TryStatement :
        BlockStatement,
        ITryStatement
    {
        private ControlledDictionary<IType, ITypedCatchExceptionBlockStatement> _exceptionBlocksBackup = new ControlledDictionary<IType,ITypedCatchExceptionBlockStatement>();
        private BlockStatement _catchAll;
        private BlockStatement _finally;

        public TryStatement(IBlockStatementParent parent)
            : base(parent)
        {
        }

        public int ClauseCount
        {
            get 
            {
                return _exceptionBlocksBackup.Count;
            }
        }

        public int StatementCount
        {
            get { return base.Count; }
        }

        public IBlockStatement CatchAll
        {
            get 
            {
                if (this._catchAll == null)
                    this._catchAll = new BlockStatement(this.Parent);
                return this._catchAll;
            }
        }

        public bool HasCatchAll
        {
            get
            {
                return this._catchAll != null;
            }
            set
            {
                if (this._catchAll != null && !value)
                {
                    if (this._catchAll.baseList != null)
                        this._catchAll.baseList.Clear();
                    this._catchAll = null;
                }
                else if (this._catchAll == null && value)
                    this._catchAll = new BlockStatement(this.Parent);
            }
        }

        public IBlockStatement Finally
        {
            get 
            {
                if (this._finally == null)
                    this._finally = new BlockStatement(this.Parent);
                return this._finally;
            }
        }

        public bool HasFinally
        {
            get
            {
                return this._finally != null;
            }
            set
            {
                if (this._finally != null && !value)
                {
                    if (this._finally.baseList != null)
                        this._finally.baseList.Clear();
                    this._finally = null;
                }
                else if (this._finally == null && value)
                    this._finally = new BlockStatement(this.Parent);
            }
        }

        public ITypedCatchExceptionBlockStatement Catch(IType exceptionType)
        {
            var result = new TypedCatchExceptionBlockStatement(this, exceptionType);
            this._exceptionBlocksBackup._Add(exceptionType, result);
            return result;
        }

        public ITypeNamedCatchExceptionBlockStatement Catch(TypedName nameAndType)
        {
            var result = new TypeNamedCatchExceptionBlockStatement(this, nameAndType);
            this._exceptionBlocksBackup._Add(result.LocalVariable.LocalType, result);
            return result;
        }

        public IControlledCollection<IType> Keys
        {
            get { return _exceptionBlocksBackup.Keys; }
        }

        public IControlledCollection<ITypedCatchExceptionBlockStatement> Values
        {
            get { return _exceptionBlocksBackup.Values; }
        }

        public ITypedCatchExceptionBlockStatement this[IType key]
        {
            get { return _exceptionBlocksBackup[key]; }
        }

        public bool ContainsKey(IType key)
        {
            return _exceptionBlocksBackup.ContainsKey(key);
        }

        public bool TryGetValue(IType key, out ITypedCatchExceptionBlockStatement value)
        {
            return _exceptionBlocksBackup.TryGetValue(key, out value);
        }


        public bool Contains(KeyValuePair<IType, ITypedCatchExceptionBlockStatement> item)
        {
            return _exceptionBlocksBackup.Contains(item);
        }

        public void CopyTo(KeyValuePair<IType, ITypedCatchExceptionBlockStatement>[] array, int arrayIndex = 0)
        {
            _exceptionBlocksBackup.CopyTo(array, arrayIndex);
        }

        KeyValuePair<IType, ITypedCatchExceptionBlockStatement> IControlledCollection<KeyValuePair<IType, ITypedCatchExceptionBlockStatement>>.this[int index]
        {
            get {
                return _exceptionBlocksBackup[index];
            }
        }

        public new KeyValuePair<IType, ITypedCatchExceptionBlockStatement>[] ToArray()
        {
            return _exceptionBlocksBackup.ToArray();
        }

        public int IndexOf(KeyValuePair<IType, ITypedCatchExceptionBlockStatement> element)
        {
            return _exceptionBlocksBackup.IndexOf(element);
        }

        public new IEnumerator<KeyValuePair<IType, ITypedCatchExceptionBlockStatement>> GetEnumerator()
        {
            return _exceptionBlocksBackup.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }


        public override TResult Accept<TResult, TContext>(IStatementVisitor<TResult, TContext> visitor, TContext context)
        {
            return visitor.Visit(this, context);
        }

        public override void Accept(IStatementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
