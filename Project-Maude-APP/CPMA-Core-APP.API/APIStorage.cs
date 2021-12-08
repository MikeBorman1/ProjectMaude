using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Globalization;
using Microsoft.AppCenter.Crashes;

namespace CPMA_Core_APP.API
{
    public class APIStorage : IStorage
    {
        public async Task UploadPhoto(string photoUri, string photoPath)
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=inthebag;AccountKey=l/nLOL9Etb6T6JMxGxpC8W8zuUK+NGtQWnVcTLjy9m+PRqTLGvjA2+AW4C2tSYcxyNqQtlznt9nRyHlyCQBijQ==;EndpointSuffix=core.windows.net;";
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                var container = cloudBlobClient.GetContainerReference("app-images");
                try
                {
                    var blobRef = container.GetBlockBlobReference(photoUri);

                    await blobRef.UploadFromFileAsync(photoPath);
                }
                catch(Exception e)
                {
                    Crashes.TrackError(e);
                }
            }
            else
            {
                // Otherwise, let the user know that they need to define the environment variable.
                Console.WriteLine(
                    "A connection string has not been defined in the system environment variables. " +
                    "Add an environment variable named 'CONNECT_STR' with your storage " +
                    "connection string as a value.");
                Console.WriteLine("Press any key to exit the application.");
                Console.ReadLine();
            }

        }

        public async Task CopyPhoto(string source, string target)
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=inthebag;AccountKey=l/nLOL9Etb6T6JMxGxpC8W8zuUK+NGtQWnVcTLjy9m+PRqTLGvjA2+AW4C2tSYcxyNqQtlznt9nRyHlyCQBijQ==;EndpointSuffix=core.windows.net;";
            CloudStorageAccount storageAccount;
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            source = textInfo.ToLower(source);
            target = textInfo.ToLower(target);
            try
            {
                if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
                {
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                    var container = cloudBlobClient.GetContainerReference("app-images");

                    CloudBlockBlob sourceBlob = container.GetBlockBlobReference(source);
                    CloudBlockBlob targetBlob = container.GetBlockBlobReference(target);
                    await targetBlob.StartCopyAsync(sourceBlob);
                }
                else
                {
                    // Otherwise, let the user know that they need to define the environment variable.
                    Console.WriteLine(
                        "A connection string has not been defined in the system environment variables. " +
                        "Add an environment variable named 'CONNECT_STR' with your storage " +
                        "connection string as a value.");
                    Console.WriteLine("Press any key to exit the application.");
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
        }
    }
}
