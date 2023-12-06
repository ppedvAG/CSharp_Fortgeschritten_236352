namespace PluginBase;

/// <summary>
/// Der Client und das Plugin kennen dieses Interfacec
/// Das Plugin hat das Interface
/// Der Client kann von diesem Interface Variablen anlegen, um Plugins zu speichern
/// </summary>
public interface IPlugin
{
	string Name { get; }

	string Description { get; }

	string Version { get; }

	string Author { get; }
}