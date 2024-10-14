using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ACM.Helpers
{
    public class AzureStorageHelperFunctions
    {
        //[Obsolete]
        //internal IHostingEnvironment _env;
        internal IWebHostEnvironment _env;
        internal SecurityOptions _securityOptions;
        internal FileStorageOptions _fileStorageOptions;
        public BlobProperties downloadBlobProperties { get; set; }

        private BlobContainerClient CreateBlobClient()
        {
            BlobContainerClient blobClient = new BlobContainerClient(_fileStorageOptions.BlobStorageConnectionString, _fileStorageOptions.BlobContainerReference);

            return blobClient;
        }

        private async Task<BlobContainerClient> GetBlobContainer()
        {
            // Create the blob client.
            BlobContainerClient blobContainerClient = CreateBlobClient();

            // Create the container if it doesn't already exist.
            await blobContainerClient.CreateIfNotExistsAsync();

            return blobContainerClient;
        }

        public async Task UploadBlob(byte[] blobData, string blobName)
        {
            if (_fileStorageOptions.UseAzureBlobStorage == true)
            {
                // Retrieve reference to a blob named "myblob".
                var container = await GetBlobContainer();
                BlobClient blockBlob = container.GetBlobClient(blobName);

                using (var stream = new MemoryStream(blobData))
                {
                    await blockBlob.UploadAsync(stream);
                }
            }
            else
            {
                string filename = GetFileNameCreateFolder(blobName);
                File.WriteAllBytes(filename, blobData);
            }
        }

        public async Task<bool> BlobExists(string blobName)
        {
            try
            {
                // Retrieve reference to a blob named "myblob".
                var container = await GetBlobContainer();
                BlobClient blockBlob = container.GetBlobClient(blobName);

                return await blockBlob.ExistsAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<byte[]> DownloadBlob(string blobName)
        {
            try
            {
                if (_fileStorageOptions.UseAzureBlobStorage == true)
                {
                    // Retrieve reference to a blob named "myblob".
                    var container = await GetBlobContainer();
                    BlobClient blockBlob = container.GetBlobClient(blobName);
                    if (blockBlob != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await blockBlob.DownloadToAsync(memoryStream);
                            downloadBlobProperties = await blockBlob.GetPropertiesAsync();

                            return memoryStream.ToArray();
                        }
                    }
                    else return null;
                }
                else
                {
                    string filename = GetFileNameCreateFolder(blobName);
                    if (File.Exists(filename))
                    {
                        return File.ReadAllBytes(filename);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task DeleteBlob(string blobName)
        {
            if (_fileStorageOptions.UseAzureBlobStorage == true)
            {
                if (!string.IsNullOrEmpty(blobName))
                {
                    // Retrieve reference to a blob named "myblob".
                    var container = await GetBlobContainer();
                    BlobClient blockBlob = container.GetBlobClient(blobName);

                    await blockBlob.DeleteIfExistsAsync();
                }
            }
            else
            {
                string filename = GetFileNameCreateFolder(blobName);
                if (!string.IsNullOrEmpty(filename))
                {
                    if (File.Exists(filename))
                    {
                        File.Delete(filename);
                    }
                }
            }
         }
    
        private string GetFileNameCreateFolder(string blobname)
        {
            string folderName = _fileStorageOptions.FolderLocation.Replace("/", "").Replace("~", "");

            if(!Directory.Exists(Path.Combine(_env.ContentRootPath, _fileStorageOptions.ParentFolder)))
            { // sometimes App Data does not Exisit by Defualt, so we need to check 
                Directory.CreateDirectory(Path.Combine(_env.ContentRootPath, _fileStorageOptions.ParentFolder));
            }
            string folderPath = Path.Combine(_env.ContentRootPath, _fileStorageOptions.ParentFolder, folderName,_fileStorageOptions.BlobContainerReference);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return Path.Combine(folderPath, blobname);
        }
    }
}
