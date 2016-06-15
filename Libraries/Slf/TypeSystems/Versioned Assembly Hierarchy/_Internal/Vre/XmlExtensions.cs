using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Vre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AllenCopeland.Abstraction.Slf._Internal.Vre
{
    internal static class XmlExtensions
    {
        public static TDictionary ParseDictionary<TDictionary, TKey, TValue, TNode>(XmlNode owner, XmlNamespaceManager manager, string plural, string detail, Func<PluralSingleExtraDetail<TNode>, TValue> valueSelector, Func<PluralSingleExtraDetail<Tuple<TNode, TValue>>, TKey> keySelector)
            where TDictionary :
                IDictionary<TKey, TValue>,
                new()
            where TNode :
                XmlNode
        {
            return ParseDictionary<TDictionary, TKey, TValue, TNode>(owner, manager, new Tuple<string, IEnumerable<string>>[] { new Tuple<string, IEnumerable<string>>(plural, new string[] { detail }) }, valueSelector, keySelector);
        }

        public static TDictionary ParseDictionary<TDictionary, TKey, TValue, TNode>(XmlNode owner, XmlNamespaceManager manager, string plural, IEnumerable<string> detail, Func<PluralSingleExtraDetail<TNode>, TValue> valueSelector, Func<PluralSingleExtraDetail<Tuple<TNode, TValue>>, TKey> keySelector)
            where TDictionary :
                IDictionary<TKey, TValue>,
                new()
            where TNode :
                XmlNode
        {
            return ParseDictionary<TDictionary, TKey, TValue, TNode>(owner, manager, new Tuple<string, IEnumerable<string>>[] { new Tuple<string, IEnumerable<string>>(plural, detail) }, valueSelector, keySelector);
        }

        public static TDictionary ParseDictionary<TDictionary, TKey, TValue, TNode>(XmlNode owner, XmlNamespaceManager manager, IEnumerable<Tuple<string, IEnumerable<string>>> pluralSingleDetails, Func<PluralSingleExtraDetail<TNode>, TValue> valueSelector, Func<PluralSingleExtraDetail<Tuple<TNode, TValue>>, TKey> keySelector)
            where TDictionary :
                IDictionary<TKey, TValue>,
                new()
            where TNode :
                XmlNode
        {
            TDictionary result = new TDictionary();
            foreach (var psDetail in pluralSingleDetails)
            {
                var psdCurrent = new PluralSingleDetail() { Plural = psDetail.Item1 };
                var targetNode = owner;
                if (!string.IsNullOrEmpty(psdCurrent.Plural))
                    targetNode = targetNode.SelectSingleNode(string.Format("./vre:{0}",psdCurrent.Plural), manager);
                if (targetNode == null)
                    continue;
                foreach (var detail in psDetail.Item2)
                {
                    psdCurrent.Single = detail;
                    XmlNodeList xnl = targetNode.SelectNodes(string.Format("./vre:{0}", psdCurrent.Single), manager);
                    foreach (TNode node in from node in xnl.Cast<XmlNode>()
                                           where node is TNode
                                           select (TNode)node)
                    {
                        var value = valueSelector(new PluralSingleExtraDetail<TNode>(psdCurrent) { Extra = node });
                        var key = keySelector(new PluralSingleExtraDetail<Tuple<TNode, TValue>>(psdCurrent) { Extra = Tuple.Create(node, value) });
                        result.Add(key, value);
                    }
                }
            }
            return result;
        }

        public static int ParseAttribute(this XmlElement element, string attrName, int @default = -1, string namespaceURI = null)
        {
            return ParseAttribute<int>(element, attrName, (conv, def) =>
                {
                    int result;
                    if (int.TryParse(conv, out result))
                        return result;
                    return def;
                });
        }

        public static T ParseAttribute<T>(this XmlElement element, string attrName, Func<string, T, T> converter, T @default = default(T), string namespaceURI= null)
        {
            string attrValue;
            if (converter == null)
                throw new ArgumentNullException("converter");
            if (namespaceURI != null)
                attrValue = element.GetAttribute(attrName, namespaceURI);
            else
                attrValue = element.GetAttribute(attrName);
            if (attrValue == string.Empty)
                return @default;
            return converter(attrValue, @default);

        }

        public static TVersion GetVersion<TEnvironment, TVersion, TIdentityManager>(TEnvironment environment, string version, string versionServicePack)
            where TEnvironment :
                IVreEnvironment<TEnvironment, TVersion, TIdentityManager>
            where TVersion :
                IVreEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
            where TIdentityManager :
                class,
                IIdentityManager
        {
            int? servicePackLevel = null;
            if (!string.IsNullOrEmpty(versionServicePack))
            {
                if (versionServicePack.Length > 2 && versionServicePack.Substring(0, 2).ToLower() == "sp")
                {
                    string partial = versionServicePack.Substring(2);
                    int spLevel;
                    if (int.TryParse(partial, out spLevel))
                        servicePackLevel = spLevel;
                }
            }
            foreach (var vreVersion in environment.Versions)
                if (vreVersion.VersionText == version)
                {
                    if (vreVersion.IsServicePack && servicePackLevel != null)
                    {
                        if (vreVersion.ServicePackLevel == servicePackLevel)
                            return vreVersion;
                    }
                    else if (!vreVersion.IsServicePack && servicePackLevel == null)
                        return vreVersion;
                }
            return default(TVersion);
        }
    }
    internal class PluralSingleExtraDetail<TExtra> :
        PluralSingleDetail
    {
        public TExtra Extra { get; internal set; }
        public PluralSingleExtraDetail(PluralSingleDetail detail)
        {
            this.Plural = detail.Plural;
            this.Single = detail.Single;
        }
    }
    internal class PluralSingleDetail
    {
        public string Plural { get; internal set; }
        public string Single { get; internal set; }
    }
}
