using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Magento.RestClient.Exceptions
{
	public class MagentoException : Exception
	{
		private MagentoException(string message) : base(message)
		{
		}

		public static MagentoException Parse(string content)
		{
			var stringbuilder = new StringBuilder();
			var jObject = JObject.Parse(content);
			var c = JsonConvert.DeserializeObject<MagentoError>(content);
			Debug.Assert(c != null, nameof(c) + " != null");

			stringbuilder.Append(c.Message);
			if (c.Parameters != null)
			{
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
					if (c.Parameters is JObject o)
					{
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