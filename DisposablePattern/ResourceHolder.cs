using System.Runtime.InteropServices;

namespace DisposablePattern;

public sealed class ResourceHolder(string filePath) : IDisposable
{
    // allocate 100 bytes of unmanaged memory
    private IntPtr _memory = Marshal.AllocHGlobal(100);
    private StreamReader _streamReader = new(filePath);
    private bool _disposed;

    public string ReadFile()
    {
        return _streamReader == null
            ? throw new ObjectDisposedException(nameof(ResourceHolder))
            : _streamReader.ReadToEnd();
    }
    
    ~ResourceHolder()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        // free unmanaged resources
        if (disposing)
        {
            _streamReader.Dispose();
            _streamReader = null;
        }

        Marshal.FreeHGlobal(_memory);
        _memory = IntPtr.Zero;
        _disposed = true;
    }
}