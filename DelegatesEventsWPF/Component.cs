using System;
using System.Threading.Tasks;

namespace DelegatesEventsWPF;

/// <summary>
/// Komponente, die eine Arbeit verrichtet und ihren Status über Events zurückgibt
/// </summary>
public class Component
{
	public event Action ProcessStarted;

	public event Action ProcessEnded;

	public event Action<int> Progress;

	public async void DoWork()
	{
		ProcessStarted?.Invoke(); //Der User kann dieses Event nicht anhängen -> ?.Invoke
		for (int i = 0; i < 10; i++)
		{
			await Task.Delay(200);
			Progress?.Invoke(i); //Der User kann dieses Event nicht anhängen -> ?.Invoke
		}
		ProcessEnded?.Invoke(); //Der User kann dieses Event nicht anhängen -> ?.Invoke
	}
}
