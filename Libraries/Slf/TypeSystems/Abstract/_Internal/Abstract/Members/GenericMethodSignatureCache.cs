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
    internal class GenericMethodSignatureCache<TSignatureParameter, TSignature, TSignatureParent> :
        _IGenericMethodSignatureRegistrar,
        _IGenericParamParent,
        IDisposable
        where TSignatureParameter :
            class,
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignature :
            IMethodSignatureMember<TSignatureParameter, TSignature, TSignatureParent>
        where TSignatureParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TSignatureParent>
    {
        private bool disposing = false;
        private ControlledDictionary<IControlledTypeCollection, TSignature> genericCache;
        private IDictionary<IMethodSignatureParent, TSignature> genericChildCache;
        private object syncObject = new object();
        #region _IGenericMethodRegistrar Members

        public void RegisterGenericChild(IMethodSignatureParent parent, IMethodSignatureMember genericChild)
        {
            lock (syncObject)
            {
                if (this.disposing)
                    return;
                if (!(genericChild is TSignature))
                    throw new ArgumentException(ThrowHelper.GetExceptionMessage(ExceptionMessageId.ValueIsWrongType, "genericChild", genericChild.GetType().FullName, typeof(TSignature).FullName), "genericChild");
                if (this.genericChildCache == null)
                    this.genericChildCache = new Dictionary<IMethodSignatureParent, TSignature>();
                if (!genericChildCache.ContainsKey(parent))
                    genericChildCache.Add(parent, (TSignature)genericChild);
            }
        }

        public void UnregisterGenericChild(IMethodSignatureParent parent)
        {
            lock (syncObject)
            {
                if (this.genericChildCache == null || this.disposing)
                    return;
                TSignature toDispose;
                if (genericChildCache.TryGetValue(parent, out toDispose))
                {
                    toDispose.Dispose();
                    genericChildCache.Remove(parent);
                }
            }
        }

        public void RegisterGenericMethod(IMethodSignatureMember targetMethod, IControlledTypeCollection typeParameters)
        {
            lock (this.syncObject)
            {
                if (this.disposing)
                    return;
                if (this.genericCache == null)
                    this.genericCache = new ControlledDictionary<IControlledTypeCollection, TSignature>();
                TSignature dummy = default(TSignature);
                if (!this.ContainsGenericMethod(typeParameters, ref dummy))
                    this.genericCache._Add(typeParameters, (TSignature)targetMethod);
            }
        }

        public void UnregisterGenericMethod(IControlledTypeCollection typeParameters)
        {
            lock (this.syncObject)
            {
                if (this.genericCache == null || this.disposing)
                    return;
                TSignature toDispose;
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

        public bool ContainsGenericMethod(IControlledTypeCollection typeParameters, ref TSignature r)
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
                foreach (var genericInst in this.genericCache.Values)
                    genericInst.Dispose();
                foreach (var genericChild in this.genericChildCache.Values)
                    genericChild.Dispose();
                this.genericCache._Clear();
                this.genericChildCache.Clear();
                disposing = false;
            }
        }
    }
}
