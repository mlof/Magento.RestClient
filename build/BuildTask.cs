using Cake.Core.Diagnostics;
using Cake.Frosting;

namespace Build
{
	[TaskName("Build")]

	public sealed class BuildTask : FrostingTask<BuildContext>
	{
		public override void Run(BuildContext context)
		{
			context.Log.Information("Hello");
		}
	}
}