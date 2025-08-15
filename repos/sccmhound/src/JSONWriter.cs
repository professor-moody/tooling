using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using SCCMHound.src.models;
using SharpHoundCommonLib.Enums;
using SharpHoundCommonLib.OutputTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace SCCMHound
{
    public class JSONWriter
    {
                public static void writeJSONFileComputers(List<ComputerExt> obCollection, string filename)
        {
            /* Commented out for now to make it easier to debug while the tools new. Will minify output down the track.
#if DEBUG
            Formatting format = Formatting.Indented;
#else
            Formatting format = Formatting.None;
#endif
            */

            Formatting format = Formatting.Indented; // TODO remove this when implementing the above DEBUG check

            if (File.Exists(filename))
                throw new ArgumentException($"File {filename} already exists.");

            var meta = new MetaTag
            {
                Count = obCollection.Count,
                CollectionMethods = (long)SharpHoundCommonLib.Enums.CollectionMethod.Session,
                DataType = "computers",
                Version = 5
            };

            JsonTextWriter jsonWriter = new JsonTextWriter(new StreamWriter(filename, false, new UTF8Encoding(false)));
            jsonWriter.Formatting = format;
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName("data");
            jsonWriter.WriteStartArray();

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
            {
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter()
                },
                Formatting = format
            };
            foreach (OutputBase ob in obCollection)
            {
                jsonWriter.WriteRawValue(JsonConvert.SerializeObject(ob, serializerSettings));
            }
            jsonWriter.Flush();
            jsonWriter.WriteEndArray();
            jsonWriter.WritePropertyName("meta");
            jsonWriter.WriteRawValue(JsonConvert.SerializeObject(meta, format));
            jsonWriter.Flush();
            jsonWriter.Close();
            return;
        }


        // TODO implement C# generics for type to write
        public static void writeJSONFileUsers(List<User> obCollection, string filename)
        {
#if DEBUG
            Formatting format = Formatting.Indented;
#else
            Formatting format = Formatting.None;
#endif
            if (File.Exists(filename))
                throw new ArgumentException($"File {filename} already exists.");

            var meta = new MetaTag
            {
                Count = obCollection.Count,
                CollectionMethods = (long)SharpHoundCommonLib.Enums.CollectionMethod.Session,
                DataType = "users",
                Version = 5
            };

            JsonTextWriter jsonWriter = new JsonTextWriter(new StreamWriter(filename, false, new UTF8Encoding(false)));
            jsonWriter.Formatting = format;
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName("data");
            jsonWriter.WriteStartArray();

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
            {
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter()
                },
                Formatting = format
            };
            foreach (OutputBase ob in obCollection)
            {
                jsonWriter.WriteRawValue(JsonConvert.SerializeObject(ob, serializerSettings));
            }
            jsonWriter.Flush();
            jsonWriter.WriteEndArray();
            jsonWriter.WritePropertyName("meta");
            jsonWriter.WriteRawValue(JsonConvert.SerializeObject(meta, format));
            jsonWriter.Flush();
            jsonWriter.Close();
            return;
        }

        public static void writeJSONFileGroups(List<Group> obCollection, string filename)
        {
#if DEBUG
            Formatting format = Formatting.Indented;
#else
            Formatting format = Formatting.None;
#endif
            if (File.Exists(filename))
                throw new ArgumentException($"File {filename} already exists.");

            var meta = new MetaTag
            {
                Count = obCollection.Count,
                CollectionMethods = (long)SharpHoundCommonLib.Enums.CollectionMethod.Session,
                DataType = "groups",
                Version = 5
            };

            JsonTextWriter jsonWriter = new JsonTextWriter(new StreamWriter(filename, false, new UTF8Encoding(false)));
            jsonWriter.Formatting = format;
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName("data");
            jsonWriter.WriteStartArray();

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
            {
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter()
                },
                Formatting = format
            };
            foreach (OutputBase ob in obCollection)
            {
                jsonWriter.WriteRawValue(JsonConvert.SerializeObject(ob, serializerSettings));
            }
            jsonWriter.Flush();
            jsonWriter.WriteEndArray();
            jsonWriter.WritePropertyName("meta");
            jsonWriter.WriteRawValue(JsonConvert.SerializeObject(meta, format));
            jsonWriter.Flush();
            jsonWriter.Close();
            return;
        }

        public static void writeJSONFileDomains(List<Domain> obCollection, string filename)
        {
#if DEBUG
            Formatting format = Formatting.Indented;
#else
            Formatting format = Formatting.None;
#endif
            if (File.Exists(filename))
                throw new ArgumentException($"File {filename} already exists.");

            var meta = new MetaTag
            {
                Count = obCollection.Count,
                CollectionMethods = (long)SharpHoundCommonLib.Enums.CollectionMethod.Session,
                DataType = "domains",
                Version = 5
            };

            JsonTextWriter jsonWriter = new JsonTextWriter(new StreamWriter(filename, false, new UTF8Encoding(false)));
            jsonWriter.Formatting = format;
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName("data");
            jsonWriter.WriteStartArray();

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
            {
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter()
                },
                Formatting = format
            };
            foreach (OutputBase ob in obCollection)
            {
                jsonWriter.WriteRawValue(JsonConvert.SerializeObject(ob, serializerSettings));
            }
            jsonWriter.Flush();
            jsonWriter.WriteEndArray();
            jsonWriter.WritePropertyName("meta");
            jsonWriter.WriteRawValue(JsonConvert.SerializeObject(meta, format));
            jsonWriter.Flush();
            jsonWriter.Close();
            return;
        }


    }
}
