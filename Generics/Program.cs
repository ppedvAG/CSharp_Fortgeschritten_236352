namespace Generics;

internal class Program
{
	static void Main(string[] args)
	{
		List<string> list = new List<string>(); //T wird durch string ersetzt
		list.Add("Max"); //T item -> string item
	}
}

public class DataStore<T>
{
	//Überall innerhalb der Klasse wo ein Typ stehen würde, kann T stehen
	public T[] data { get; set; } //T bei Variable

	public List<T> Data => data.ToList(); //T weitergeben

	public void Add(int index, T item) //T als Parameter
	{
		data[index] = item;
	}

	public T GetValue(int index) //T bei Rückgabetyp
	{
		if (index < 0 || index >= data.Length)
			return default; //default: Standardwert des Generics
		return data[index];
	}

	public T1 MethodeMitGeneric<T1>()
	{
        Console.WriteLine(default(T1));
        Console.WriteLine(typeof(T1));
        Console.WriteLine(nameof(T1)); //nameof: Namen des Typens als String ("int", "string", "bool", "Person", ...)

		Convert.ChangeType(new object(), typeof(T1));
		return (T1) new Object();
    }
}