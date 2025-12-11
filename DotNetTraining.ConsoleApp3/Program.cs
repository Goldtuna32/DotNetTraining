using DotNetTraining.ConsoleApp3;

//HttpClientExample httpClient = new HttpClientExample();
//await httpClient.Get();
//await httpClient.Edit(1);
//await httpClient.Post("foo", "bar", 1);
//await httpClient.Update(1, "updated title", "updated body", 1);
//await httpClient.Delete(1);
Console.WriteLine("waiting for api...");
Console.ReadLine();

RefitExample refitExample = new RefitExample();
await refitExample.Run();

Console.ReadLine();