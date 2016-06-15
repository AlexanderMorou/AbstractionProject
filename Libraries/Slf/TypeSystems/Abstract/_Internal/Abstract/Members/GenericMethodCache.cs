using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer;
using AllenCopeland.Abstraction.Utilities.Collections;

namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    internal class GenericMethodCache<TMethodParameter, TMethod, TMethodParent> :
        _IGenericMethodRegistrar,
        _IGenericParamParent,
        IDisposable
        where TMethodParameter :
            class,
            IMethodSignatureParameterMember<TMethodParameter, TMethod, TMethodParent>
        where TMethod :
            IMethodSignatureMember<TMethodParameter, TMethod, TMethodParent>
        where TMethodParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TMethod, TMethodParameter, TMethodParent>
    {
        private bool disposing = false;
        private ControlledDictionary<IControlledTypeCollection, TMethod> genericCache;
        private IDictionary<IMethodParent, TMethod> genericChildCache;
        private object syncObject = new object();
        #region _IGenericMethodRegistrar Members

        public void RegisterGenericChild(IMethodParent parent, IMethodMember genericChild)
        {
            lock (syncObject)
            {
                if (this.disposing)
                    return;
                if (!(genericChild is TMethod))
                    throw new ArgumentException(ThrowHelper.GetExceptionMessage(ExceptionMessageId.ValueIsWrongType, "genericChild", genericChild.GetType().FullName, typeof(TMethod).FullName), "genericChild");
                if (this.genericChildCache == null)
                    this.genericChildCache = new Dictionary<IMethodParent, TMethod>();
                if (!genericChildCache.ContainsKey(parent))
                    genericChildCache.Add(parent, (TMethod)genericChild);
            }
        }

        public void UnregisterGenericChild(IMethodParent parent)
        {
            lock (syncObject)
            {
                if (this.genericChildCache == null || this.disposing)
                    return;
                TMethod toDispose;
                if (genericChildCache.TryGetValue(parent, out toDispose))
                {
                    toDispose.Dispose();
                    genericChildCache.Remove(parent);
                }
            }
        }

        public void RegisterGenericMethod(IMethodMember targetMethod, IControlledTypeCollection typeParameters)
        {
            lock (this.syncObject)
            {
                if (this.disposing)
                    return;
                if (this.genericCache == null)
                    this.genericCache = new ControlledDictionary<IControlledTypeCollection, TMethod>();
                TMethod dummy = default(TMethod);
                if (!this.ContainsGenericMethod(typeParameters, ref dummy))
                    this.genericCache._Add(typeParameters, (TMethod)targetMethod);
            }
        }

        public void UnregisterGenericMethod(IControlledTypeCollection typeParameters)
        {
            lock (this.syncObject)
            {
                if (this.genericCache == null || this.disposing)
                    return;
                TMethod toDispose;
                var fd = this.genericCache.Keys.FirstOrDefault(itc => itc.SequenceEqual(typeParameters));
                if (fd != null)
                {
                    if (this.genericCache.TryGetValue(fd, out toDispose))
                    {
                        toDispose.Dispose();
                        genericCache._Remove(fd);
                    }
                }
            }
        }

        #endregion

        public void PositionalShift(int from, int to)
        {
            lock (this.syncObject)
            {
                if (this.disposing)
                    return;
                if (this.genericCache != null)
                {

                    bool backwards = from > to;
                    Dictionary<IControlledTypeCollection, IControlledTypeCollection> keyReplacements = new Dictionary<IControlledTypeCollection, IControlledTypeCollection>();
                    foreach (var ctc in this.genericCache.Keys)
                    {
                        var items = ctc.ToArray();

                        var item = items[from];
                        if (backwards)
                            for (int i = from; i > to; i--)
                                items[i] = items[i - 1];
                        else
                            for (int i = from; i < to; i++)
                                items[i] = items[i + 1];

                        items[to] = item;
                        keyReplacements.Add(ctc, items.ToCollection());
                    }
                    foreach (var ctc in keyReplacements.Keys)
                    {
                        _IGenericParamParent signature = (_IGenericParamParent)genericCache[ctc];
                        genericCache.Keys[genericCache.Keys.IndexOf(ctc)] = keyReplacements[ctc];
                        signature.PositionalShift(from, to);
                    }
                }
                if (this.genericChildCache != null)
                    foreach (_IGenericParamParent signature in genericChildCache.Values)
                        signature.PositionalShift(from, to);
            }
        }

        public bool ContainsGenericMethod(IControlledTypeCollection typeParameters, ref TMethod r)
        {
            lock (this.syncObject)
            {
                if (this.disposing)
                    return false;
                if (this.genericCache == null)
                    return false;
                var fd = this.genericCache.Keys.FirstOrDefault(itc => itc.SequenceEqual(typeParameters));
                if (fd == null)
                    return false;
                r = this.genericCache[fd];
                return true;
            }
        }

        public void Dispose()
        {
            lock (this.syncObject)
            {
                if (this.disposing)
                    return;
                disposing = true;
                if (this.genericCache != null)
                {
                    foreach (var genericInst in this.genericCache.Values)
                        genericInst.Dispose();
                    foreach (var genericChild in this.genericChildCache.Values)
                        genericChild.Dispose();
                    this.genericCache._Clear();
                }
                if (this.genericChildCache != null)
                    this.genericChildCache.Clear();
                disposing = false;
            }
        }
    }
}
