using Cake.Frosting;

namespace Build
{
	[TaskName("Default")]
	[IsDependentOn(typeof(BuildTask))]
	public class DefaultTask : FrostingTask
	{
	}
}