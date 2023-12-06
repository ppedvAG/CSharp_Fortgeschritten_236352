Task test = Test();
Console.WriteLine("TEST");
await test;


async Task Test()
{
	await Task.Delay(1000);
}