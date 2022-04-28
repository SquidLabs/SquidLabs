using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SquidLabs.Tentacles.Infrastructure.Abstractions;

namespace SquidLabs.Tentacles.Infrastructure.Tests;

public class TestFileEntry: IFileEntry
{
    private readonly FileInfo _fileInfo;

    public TestFileEntry(string filePath)
    {
        _fileInfo = new FileInfo(filePath);
    }
    
    public TestFileEntry(string filePath, in Stream readStream)
    {
        _fileInfo = new FileInfo(filePath);
        var writeStream = _fileInfo.OpenWrite();
         readStream.CopyToAsync(writeStream);
    }
    
   
    public async Task<Stream> ToStreamAsync(CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(_fileInfo.OpenRead());
    }

    public async static Task<IFileEntry> FromStreamAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(new TestFileEntry(fileName, stream));
    }
}