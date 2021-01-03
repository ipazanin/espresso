
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Espresso.Common.IServices
{
    public interface IJsonService
    {
        public Task<string> Serialize<TValue>(
            TValue value,
            CancellationToken cancellationToken
        );

        public Task<TValue?> Deserialize<TValue>(
            string json,
            CancellationToken cancellationToken
        );

        public Task<TValue?> Deserialize<TValue>(
            byte[] utf8Bytes,
            CancellationToken cancellationToken
        );

        public string Serialize<TValue>(TValue value);

        public TValue? Deserialize<TValue>(string json);

        public Task<HttpContent> GetJsonHttpContent<TValue>(
            TValue value,
            CancellationToken cancellationToken
        );
    }
}