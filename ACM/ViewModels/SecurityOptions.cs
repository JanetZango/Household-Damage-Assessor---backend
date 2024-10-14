namespace ACM.ViewModels
{
    public class SecurityOptions
    {
        public string SecretKey { get; set; }
        public string PasswordSalt { get; set; }
        public string WebsiteHostUrl { get; set; }
        public string BlobStorageConnectionString { get; set; }
        public string BlobContainerReference { get; set; }
    }
    public class FileStorageOptions
    {
        public bool UseFileStorage { get; set; }
        public string FolderLocation { get; set; }
        public bool UseAzureBlobStorage { get; set; }
        public string BlobStorageConnectionString { get; set; }
        public string BlobContainerReference { get; set; }
        public string ParentFolder { get; set; }
    }
}
