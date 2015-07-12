using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace AllenCopeland.Abstraction.Slf.Vre
{
    public class VreXmlGateway
    {
        private const string vreNamespaceUri    = @"http://www.AlexanderMorou.com/schemas/VersionedRuntimeEnvironment/2015/vre.xsd";
        private const string vreNamespacePrefix = @"vre";
        private static XmlSchema vreSchema;

        private static XmlSchema VreSchema
        {
            get
            {
                return (vreSchema ?? (vreSchema = InitializeVreSchema()));
            }
        }

        public static TEnvironment GetVersionedEnvironment<TEnvironment, TVersion, TIdentityManager>(XmlDocument envDescriptor, Func<XmlNamespaceManager, TEnvironment> environmentFactory)
            where TEnvironment :
                VreXmlEnvironment<TEnvironment, TVersion, TIdentityManager>
            where TVersion :
                VreXmlEnvironmentVersion<TEnvironment, TVersion, TIdentityManager>
            where TIdentityManager :
                class,
                IIdentityManager
        {
            if (!envDescriptor.Schemas.Contains(vreNamespaceUri))
            {
                envDescriptor.Schemas.Add(VreSchema);
                envDescriptor.Schemas.Compile();
            }
            List<Tuple<object, ValidationEventArgs>> validationErrors = new List<Tuple<object, ValidationEventArgs>>();
            envDescriptor.Validate((o, v) => validationErrors.Add(Tuple.Create(o, v)));
            if (validationErrors.Count > 0)
                throw new EnvironmentDesscriptorException("Environment Descriptor contains errors", validationErrors);
            XmlNamespaceManager xnm = new XmlNamespaceManager(envDescriptor.NameTable);
            xnm.AddNamespace(vreNamespacePrefix, vreNamespaceUri);
            TEnvironment result = environmentFactory(xnm);
            return result;
        }

        private static XmlSchema InitializeVreSchema()
        {
            var resourceData = Resources.vre;
            using (var schemaBody = new StringReader(resourceData))
            using (var xmlReader = XmlReader.Create(schemaBody))
            {
                List<Tuple<object, ValidationEventArgs>> schemaErrors = new List<Tuple<object, ValidationEventArgs>>();
                var schema = XmlSchema.Read(xmlReader, (o, v) =>
                {
                    schemaErrors.Add(Tuple.Create(o, v));
                });
                if (schemaErrors.Count > 0)
                    throw new EnvironmentDesscriptorException("Underlying Schema describing Versioned Runtime Environment is invalid.", schemaErrors);
                return schema;
            }
        }
        private class EnvironmentDesscriptorException :
            XmlSchemaException
        {
            private Dictionary<object, ValidationEventArgs[]> data;
            public EnvironmentDesscriptorException(string message, List<Tuple<object, ValidationEventArgs>> data)
                : base(message)
            {
                this.data = data.GroupBy(k => k.Item1, v => v.Item2).ToDictionary(k => k.Key ?? (object)"{{null}}", v => v.ToArray());
            }
            public override IDictionary Data
            {
                get
                {
                    return this.data;
                }
            }
        }

    }
}
