using System.Text;

namespace LinqErweiterungsmethoden;

internal static class ExtensionMethods
{
	public static int Quersumme(this int x) //Mit this sich auf einen Typen beziehen
	{
		return x.ToString().Sum(e => (int) char.GetNumericValue(e));
	}

	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> x)
	{
		//Dictionary<T, int> map = new Dictionary<T, int>();
		//foreach (T t in x)
		//{
		//	map.Add(t, Random.Shared.Next());
		//}
		//return map.OrderBy(e => e.Value).Select(e => e.Key); //Hier Sortieralgorithmus per Hand einbauen

		return x.OrderBy(e => Random.Shared.Next()); //Jedem Wert wird ein Zufallswert zugewiesen, nachdem Sortiert wird
	}

	//Liste printen ála Python
	public static string AsString<TObject>(this IEnumerable<TObject> x)
	{
		StringBuilder sb = new StringBuilder();
		sb.Append("[");
		sb.Append(string.Join(", ", x));
		sb.Append("]");
		return sb.ToString();
	}

	public static string AsString<TObject, TSelector>(this IEnumerable<TObject> x, Func<TObject, TSelector> selector)
	{
		StringBuilder sb = new StringBuilder();
		sb.Append("[");
		sb.Append(string.Join(", ", x.Select(e => selector(e)))); //Wandle jedes Element der Liste x in die Form des selectors um
		sb.Append("]");
		return sb.ToString();
	}
}
