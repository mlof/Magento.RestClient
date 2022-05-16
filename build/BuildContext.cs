using Cake.Common;
using Cake.Core;
using Cake.Frosting;

namespace Build
{
	public class BuildContext : FrostingContext
	{
		public string MsBuildConfiguration { get; set; }

		public BuildContext(ICakeContext context)
			: base(context)
		{
			this.MsBuildConfiguration = context.Argument("configuration", "Release");
		}

	}
}