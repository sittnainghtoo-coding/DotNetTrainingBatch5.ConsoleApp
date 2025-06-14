// See https://aka.ms/new-console-template for more information
using DotNetTrainingBatch5.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
//adoDotNetExample.Delete();
//Console.ReadKey();


//DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Edit(1);
//dapperExample.Edit(10);
//dapperExample.Create("Chit Tal", "KO Sitt", "mylove");
//dapperExample.Update(1, "Chit Tal", "KO Sitt", "mylove update");

//dapperExample.Delete(15);

EFCoreExample efCoreExample = new EFCoreExample();
//efCoreExample.Read();
//efCoreExample.Create("Chit Tal", "KO Sitt", "mylove");
//efCoreExample.Edit(16);
//efCoreExample.Update(16, "Sorry", "KO Sitt", "Demo update");
efCoreExample.Delete(19);

Console.ReadKey();
