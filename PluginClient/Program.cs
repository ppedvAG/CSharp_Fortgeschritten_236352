using PluginBase;
using System.Reflection;
using System.Text;

namespace PluginClient;

internal class Program
{
	static void Main(string[] args)
	{
		Assembly assembly = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_12_04\PluginCalculator\bin\Debug\net7.0\PluginCalculator.dll"); //Pfade sollten in einer Config stehen
		IPlugin plugin = Activator.CreateInstance(assembly.GetTypes().First(e => e.GetInterface("IPlugin") != null)) as IPlugin;

		PrintProperties(plugin);
		PrintMethods(plugin);
	}

	public static void PrintProperties(object o)
	{
		Console.WriteLine(
		o.GetType()
			.GetProperties()
			.Aggregate(new StringBuilder(), (agg, prop) => agg.Append($"{prop.Name}: {prop.GetValue(o)}\n"))
			.ToString());
	}

	private static void PrintMethods(object o)
	{
		Console.WriteLine(
			o.GetType()
			.GetMethods()
			.Where(e => e.GetCustomAttribute<ReflectionVisible>() != null)
			.AsString(m => $"{m.Name}({m.GetParameters().AsString(e => $"{e.ParameterType} {e.Name}")})\n"));
	}
}