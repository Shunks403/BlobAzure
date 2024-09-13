using BlobDemo;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
            .AddJsonFile("config.json")
            .Build();

string connectionString = config.GetConnectionString("Default") ?? throw new NullReferenceException("Connection string not found");

var blobService = new BlobService(connectionString);

try
{
    var container = await blobService.GetContainer("temp2");
    string blobName = "myBlob";

    Cat cat = new Cat { Name = "simona", Sound = "мау" };
    
    await blobService.UploadObjectAsync(container, blobName, cat);
    Console.WriteLine("Объект отправлен в Blob Storage");

    
    string objectType = await blobService.GetBlobObjectTypeAsync(container, blobName);
    Console.WriteLine($"Тип объекта, сохранённого в Blob: {objectType}");

    
    var downloadedObject = await blobService.DownloadObjectAsync<Cat>(container, blobName);
    Console.WriteLine($"Объект получен: {downloadedObject.Name}, {downloadedObject.Sound}");
    

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}



/*try
{
    var container = await blobService.GetContainer("temp2");
    string path = "";


    for(int i = 0; i < 5; i++)
    {
        File.Copy(Path.Combine(path, "np.jpg"), Path.Combine(path, $"np{i}.jpg"));
        await blobService.AddBlob(container, Path.Combine(path, $"np{i}.jpg"));
    }

    await blobService.DisplayBlobs(container);

    await blobService.DeleteMultipleBlobs(container, 
        Enumerable.Range(0, 5).Select(i => $"np{i}.jpg"));

    Console.WriteLine(new string('-', 40));
    await blobService.DisplayBlobs(container);
}
catch (Exception ex)
{
    Console.WriteLine($"Произошла ошибка: {ex.Message}");
}*/




class Cat
{
   public string Name { get; set; }
    
   public string Sound { get; set; }
    
}
