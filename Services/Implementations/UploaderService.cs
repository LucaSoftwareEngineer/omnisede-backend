using Azure.Storage.Blobs;
using OmniSedeBackend.Config;
using OmniSedeBackend.Services.Interfaces;

namespace OmniSedeBackend.Services.Implementations;

public class UploaderService : IUploaderService
{
    private readonly BlobConfig _blobConfig;
    
    public UploaderService(BlobConfig blobConfig)
    {
        _blobConfig = blobConfig;
    }
    
    public async Task Upload(IFormFile file)
    {
        var options = new BlobClientOptions(BlobClientOptions.ServiceVersion.V2023_11_03);
        var client = new BlobContainerClient(_blobConfig.BlobConn, _blobConfig.BlobContainer, options);
        var blob = client.GetBlobClient(file.FileName);
        await blob.UploadAsync(file.OpenReadStream());
    }
}