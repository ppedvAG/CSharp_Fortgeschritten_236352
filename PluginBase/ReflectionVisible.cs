namespace PluginBase;

/// <summary>
/// Attribute selbst haben keine Funktionen
/// Attribute müssen über Reflection ausgewertet werden
/// </summary>
public class ReflectionVisible : Attribute
{
	public string Name { get; init; }

    public ReflectionVisible() { }

    public ReflectionVisible(string name)
	{
		Name = name;
	}
}
