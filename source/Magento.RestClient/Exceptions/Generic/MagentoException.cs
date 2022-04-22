using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Magento.RestClient.Exceptions.Generic
{
    public class MagentoException : Exception
    {
        private MagentoException(string message) : base(message)
        {
        }

        public MagentoException() : base()
        {
        }

        public MagentoException(string message, Exception innerException) : base(message, innerException)
        {
        }


        public static MagentoException Parse(string content)
        {
            var stringbuilder = new StringBuilder();
            var c = JsonConvert.DeserializeObject<MagentoError>(content);
            Debug.Assert(c != null, nameof(c) + " != null");

            stringbuilder.Append(c.Message);


            stringbuilder.AppendLine();


            if (c.Parameters is IEnumerable<string> s)
            {
                stringbuilder.AppendLine("Parameters:");

                foreach (var parameter in s)
                {
                    stringbuilder.AppendLine(parameter);
                }
            }
            else
            {

                if (c.Parameters is JArray array)
                {
                    foreach (var child in array.Children())
                    {
                        stringbuilder.AppendLine(child.ToString());
                    }

                }
                else if (c.Parameters is JObject o)
                {
                    stringbuilder.AppendLine(o.ToString());

                }
                
            }

            stringbuilder.AppendLine();


            return new MagentoException(stringbuilder.ToString())
            {
                Parameters = c.Parameters
            };
        }

        public object Parameters { get; set; }

        internal class MagentoError
        {
            [JsonProperty("message")] public string Message { get; set; }
            [JsonProperty("parameters")] public object Parameters { get; set; }
        }
    }
}