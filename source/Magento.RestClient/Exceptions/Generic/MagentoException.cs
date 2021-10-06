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
		private MagentoException(string message) : base()
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
			if (c.Parameters == null)
			{
				return new MagentoException(stringbuilder.ToString());
			}

			stringbuilder.AppendLine();
			stringbuilder.AppendLine("Parameters:");


			if (c.Parameters is IEnumerable<string> s)
			{
				foreach (var parameter in s)
				{
					stringbuilder.AppendLine(parameter);
				}
			}
			else
			{
				if (c.Parameters is not JObject o)
				{
					return new MagentoException(stringbuilder.ToString());
				}

				if (o.Type == JTokenType.Object)
				{
					stringbuilder.AppendLine(o.ToString());
				}
				else if (o.Type == JTokenType.Array)
				{
					foreach (var child in o.Children())
					{
						stringbuilder.AppendLine(child.ToString());
					}
				}
			}

			return new MagentoException(stringbuilder.ToString());
		}

		internal class MagentoError
		{
			[JsonProperty("message")] public string Message { get; set; }
			[JsonProperty("parameters")] public object Parameters { get; set; }
		}
	}
}