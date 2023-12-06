using System.Text;

namespace Reflection;

internal static class ExtensionMethods
{
	public static string AsString<TObject, TSelector>(this IEnumerable<TObject> x, Func<TObject, TSelector> selector)
	{
		StringBuilder sb = new StringBuilder();
		sb.Append("[");
		sb.Append(string.Join(", ", x.Select(e => selector(e)))); //Wandle jedes Element der Liste x in die Form des selectors um
		sb.Append("]");
		return sb.ToString();
	}
}
