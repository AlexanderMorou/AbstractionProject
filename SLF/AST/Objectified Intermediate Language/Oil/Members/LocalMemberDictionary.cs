using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
using AllenCopeland.Abstraction.Slf.Oil.Statements;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    public class LocalMemberDictionary :
        IntermediateMemberDictionary<IBlockStatementParent, IBlockStatementParent, IGeneralMemberUniqueIdentifier, ILocalMember, ILocalMember>,
        ILocalMemberDictionary
    {
        /// <summary>
        /// Creates a new <see cref="LocalMemberDictionary"/> with the <paramref name="parent"/>
        /// <see cref="IBlockStatementParent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IBlockStatementParent"/> which owns the
        /// <see cref="LocalMemberDictionary"/>.</param>
        public LocalMemberDictionary(IBlockStatementParent parent)
            : base(parent)
        {
        }

        #region ILocalMemberDictionary Members

        /// <summary>
        /// Inserts a new <see cref="ITypedLocalMember"/> from the <paramref name="name"/>
        /// and <paramref name="localType"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> value representing
        /// the unique name, within the active scope, for the local to add.</param>
        /// <param name="localType">The <see cref="IType"/>
        /// associated to the <see cref="ITypedLocalMember"/> within the 
        /// active scope.</param>
        /// <returns>A new <see cref="ITypedLocalMember"/> with
        /// the <paramref name="name"/> and <paramref name="localType"/> provided.
        /// </returns>
        public ITypedLocalMember Add(string name, IType localType)
        {
            return this.Add(new TypedName(name, localType));
        }

        /// <summary>
        /// Inserts a new <see cref="ITypedLocalMember"/> from the <paramref name="nameAndType"/>
        /// provided.
        /// </summary>
        /// <param name="nameAndType">The <see cref="TypedName"/>
        /// which represents both the the unique name and type, within the active scope, 
        /// for the local to add.</param>
        /// <returns>A new <see cref="ITypedLocalMember"/> with
        /// the <paramref name="nameAndType"/> provided.
        /// </returns>
        public ITypedLocalMember Add(TypedName nameAndType)
        {
            return this.AddRange(nameAndType)[0];
        }

        /// <summary>
        /// Inserts a series of new <see cref="ITypedLocalMember"/> instances
        /// with the <see cref="TypedNameSeries"/>, relative to the active
        /// scope, for the <see cref="ITypedLocalMember"/> elements to add.
        /// </summary>
        /// <param name="namesAndTypes">The <see cref="TypedNameSeries"/>
        /// which denotes each element's name and type within the active scope.</param>
        /// <returns>a series of new <see cref="ITypedLocalMember"/> instances
        /// with the <see cref="TypedNameSeries"/> provided.</returns>
        public ITypedLocalMember[] AddRange(TypedNameSeries namesAndTypes)
        {
            var seriesElements = namesAndTypes.ToArray();
            Stack<ILocalMemberDictionary> memberScopes = GetFullScope();
            for (int i = 0; i < seriesElements.Length; i++)
            {
                var iElementName = seriesElements[i].Name;
                /* *
                 * First check for name collisions within the series itself.
                 * */
                for (int j = i + 1; j < seriesElements.Length; j++)
                    if (iElementName == seriesElements[j].Name)
                        throw new ArgumentException("Duplicate name detected");
                /* *
                 * Next, check for collisions within the scope.
                 * */
                foreach (var scope in memberScopes)
                    if (scope.ContainsKey(iElementName))
                        throw new ArgumentException("Duplicate name detected");
            }
            var parentMember = this.GetTopParent() as IIntermediateMember;
            ITypedLocalMember[] locals = new ITypedLocalMember[seriesElements.Length];
            Parallel.For(0, seriesElements.Length, i =>
            {
                var current = seriesElements[i];
                var localType = current.GetTypeRef();
                if (localType.ContainsSymbols())
                    localType = localType.SimpleSymbolDisambiguation(parentMember);
                locals[i] = new TypedLocalMember(current.Name, this.Parent, localType);
            });
            this._AddRange(from local in locals
                           select new KeyValuePair<string, ILocalMember>(local.Name, local));
            return locals;
        }

        private Stack<ILocalMemberDictionary> GetFullScope()
        {
            Stack<ILocalMemberDictionary> memberScopes = new Stack<ILocalMemberDictionary>();
            ILocalMemberDictionary currentScope = this;
            while (currentScope != null)
            {
                /* *
                 * In case someone decided to recurse their code structure
                 * *
                 * Invalid code, but it might occur.
                 * */
                if (memberScopes.Contains(currentScope))
                    break;
                memberScopes.Push(currentScope);
                var currentParent = currentScope.Parent;

                if (currentParent is IBlockStatement)
                    currentScope = ((IBlockStatement)(currentParent)).Parent.Locals;
                else
                    break;
            }
            
            return memberScopes;
        }

        private IBlockStatementParent GetTopParent()
        {
            return GetFullScope().Pop().Parent;
        }

        /// <summary>
        /// Inserts a series of new <see cref="ITypedLocalMember"/> instances
        /// with the series of <see cref="TypedName"/> elements, relative
        /// to the active scope, for the <see cref="ITypedLocalMember"/> elements to add.
        /// </summary>
        /// <param name="namesAndTypes">The <see cref="TypedName"/> series
        /// which denotes each element's name and type within the active scope.</param>
        /// <returns>a series of new <see cref="ITypedLocalMember"/> instances
        /// with the <see cref="TypedName"/> series provided.</returns>
        public ITypedLocalMember[] AddRange(params TypedName[] namesAndTypes)
        {
            return this.AddRange(new TypedNameSeries(namesAndTypes));
        }

        public ILocalMember Add(string name, IExpression initializationExpression, LocalTypingKind typingMethod = LocalTypingKind.Implicit)
        {
            if (typingMethod != LocalTypingKind.Implicit && typingMethod != LocalTypingKind.Dynamic)
                throw new ArgumentOutOfRangeException("typingMethod");
            if (name == null)
                throw new ArgumentNullException("name");
            var result = new LocalMember(name, this.Parent, typingMethod) { InitializationExpression = initializationExpression };
            this._Add(result.UniqueIdentifier, result);
            return result;
        }
        public ITypedLocalMember Add(TypedName nameAndType, IExpression initializationExpression)
        {
            var result = this.Add(nameAndType);
            result.InitializationExpression = initializationExpression;
            return result;
        }

        #endregion



        protected override bool ShouldDispose(ILocalMember declaration)
        {
            return true;
        }
    }
}
