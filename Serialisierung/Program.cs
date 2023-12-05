using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CsvHelper;
using System.Globalization;

namespace Serialisierung;

internal class Program
{
	static List<Fahrzeug> fahrzeuge = new()
	{
		new Fahrzeug(0, 251, FahrzeugMarke.BMW),
		new Fahrzeug(1, 274, FahrzeugMarke.BMW),
		new Fahrzeug(2, 146, FahrzeugMarke.BMW),
		new Fahrzeug(3, 208, FahrzeugMarke.Audi),
		new Fahrzeug(4, 189, FahrzeugMarke.Audi),
		new Fahrzeug(5, 133, FahrzeugMarke.VW),
		new Fahrzeug(6, 253, FahrzeugMarke.VW),
		new Fahrzeug(7, 304, FahrzeugMarke.BMW),
		new Fahrzeug(8, 151, FahrzeugMarke.VW),
		new Fahrzeug(9, 250, FahrzeugMarke.VW),
		new Fahrzeug(10, 217, FahrzeugMarke.Audi),
		new Fahrzeug(11, 125, FahrzeugMarke.Audi)
	};


	static void Main(string[] args)
	{
		//SystemJson();
		//NewtonsoftJson();
		//XML();
		//CSV();
	}

	static void SystemJson()
	{
		////System.Text.Json

		////Teil 2: Options
		//JsonSerializerOptions options = new(); //WICHTIG: Optionen beim De-/Serialisieren übergeben
		//options.WriteIndented = true; //Json schön schreiben
		//options.IncludeFields = true; //Felder (nicht-Property Variablen) mitserialisieren

		////Teil 1: Objekte von/zu Json konvertieren
		//string json = JsonSerializer.Serialize(fahrzeuge, options);
		//File.WriteAllText("Test.json", json);

		//string readJson = File.ReadAllText("Test.json");
		//List<Fahrzeug> readFzg = JsonSerializer.Deserialize<List<Fahrzeug>>(readJson, options);

		////Teil 3: Attribute
		////JsonIgnore: Ignoriert ein Feld
		////JsonPropertyName: Feld umbenennen
		////JsonDerivedType (ab .NET 7.0): Vererbung ermöglichen
		////JsonDerivedType muss auf die oberste Klasse mehrmals angehängt werden, mit den Untertypen und dem Typen selbst

		////Teil 4: Json per Hand durchgehen
		////Beispiel: Json von einer Webschnittstelle mit vielen Daten pro Json Element
		////Edit -> Paste Special -> Paste JSON as Classes
		//string cities = File.ReadAllText("history.city.list.min.json");

		////Aufgabe: Namen aller Städte + Koordinaten printen
		//JsonDocument doc = JsonDocument.Parse(cities); //Json string zu Json Dokument konvertieren
		//foreach (JsonElement element in doc.RootElement.EnumerateArray()) //Json Elemente als Array zurückgeben
		//{
		//	JsonElement city = element.GetProperty("city");
		//	string name = city.GetProperty("name").GetString();
		//	double x = city.GetProperty("coord").GetProperty("lon").GetDouble();
		//	double y = city.GetProperty("coord").GetProperty("lat").GetDouble();

		//	Console.WriteLine($"Name: {name}, (X, Y): {x}/{y}");
		//}
	}

	static void NewtonsoftJson()
	{
		//Newtonsoft.Json

		//Teil 2: Options
		JsonSerializerSettings options = new(); //WICHTIG: Optionen beim De-/Serialisieren übergeben
		//options.Formatting = Formatting.Indented; //Json schön schreiben
		options.TypeNameHandling = TypeNameHandling.Objects; //Vererbung ermöglichen (ohne Attribute)

		//Teil 1: Objekte von/zu Json konvertieren
		string json = JsonConvert.SerializeObject(fahrzeuge, options);
		File.WriteAllText("Test.json", json);

		string readJson = File.ReadAllText("Test.json");
		List<Fahrzeug> readFzg = JsonConvert.DeserializeObject<List<Fahrzeug>>(readJson, options);

		//Teil 3: Attribute
		//JsonIgnore: Ignoriert ein Feld
		//JsonProperty: Feld umbenennen

		//Teil 4: Json per Hand durchgehen
		//Beispiel: Json von einer Webschnittstelle mit vielen Daten pro Json Element
		//Edit -> Paste Special -> Paste JSON as Classes
		string cities = File.ReadAllText("history.city.list.min.json");

		//Aufgabe: Namen aller Städte + Koordinaten printen
		JToken doc = JToken.Parse(cities);
		foreach (JToken element in doc)
		{
			JToken city = element["city"];
			string name = city["name"].Value<string>();
			double x = city["coord"]["lon"].Value<double>();
			double y = city["coord"]["lat"].Value<double>();

			Console.WriteLine($"Name: {name}, (X, Y): {x}/{y}");
		}
	}

	static void XML()
	{
		//Teil 1: XML schreiben/lesen
		XmlSerializer xml = new(fahrzeuge.GetType()); //Dieser Serializer benötigt die Typinformationen
		using (StreamWriter sw = new("Test.xml"))
		{
			xml.Serialize(sw, fahrzeuge);
		}

		using StreamReader sr = new("Test.xml");
		List<Fahrzeug> readFzg = xml.Deserialize(sr) as List<Fahrzeug>;

		//Teil 2: Attribute
		//XmlIgnore: Ignoriert das Feld
		//XmlAttribute: Definiert, das das Feld in Attributschreibweise geschrieben wird
		//XmlInclude: Vererbung ermöglichen, wie bei System.Text.Json

		//Teil 3: XML per Hand durchgehen
		XmlDocument doc = new XmlDocument();
		sr.BaseStream.Position = 0; //Stream auf Position 0 zurücksetzen, nachdem er schon einmal gelesen wurde
		doc.Load(sr);

		foreach (XmlElement element in doc.DocumentElement)
		{
			XmlElement city = element["city"];
			string name = city["name"].InnerText;
			double x = double.Parse(city["coord"]["lon"].InnerText);
			double y = double.Parse(city["coord"]["lat"].InnerText);

			Console.WriteLine($"Name: {name}, (X, Y): {x}/{y}");
		}
	}

	static void CSV()
	{
		using StreamWriter sw = new("Test.csv");
		CsvWriter writer = new(sw, new CultureInfo("de-DE"));
		writer.WriteRecords(fahrzeuge);
		sw.Close();

		using StreamReader sr = new("Test.csv");
		CsvReader reader = new(sr, new CultureInfo("de-DE"));
		reader.GetRecords<Fahrzeug>().ToList();
	}
}

[DebuggerDisplay("ID: {ID}, MaxV: {MaxV}, Marke: {Marke}")]

//[JsonDerivedType(typeof(Fahrzeug), "FZG")]
//[JsonDerivedType(typeof(PKW), "PKW")]

[XmlInclude(typeof(Fahrzeug))]
[XmlInclude(typeof(PKW))]
public class Fahrzeug
{
	//System.Xml
	//[XmlIgnore]
	//[XmlAttribute]
	public int ID { get; set; }

	//Newtonsoft.Json
	//[JsonIgnore]
	//[JsonProperty("Maximalgeschwindigkeit")]
	public int MaxV { get; set; }

	//System.Text.Json
	//[JsonIgnore]
	//[JsonPropertyName("M")]
	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int iD, int maxV, FahrzeugMarke marke)
	{
		ID = iD;
		MaxV = maxV;
		Marke = marke;
	}

    public Fahrzeug()
    {
        
    }
}

public class PKW : Fahrzeug
{
	public PKW(int iD, int maxV, FahrzeugMarke marke) : base(iD, maxV, marke)
	{
	}

    public PKW()
    {
        
    }
}

public enum FahrzeugMarke { Audi, BMW, VW }