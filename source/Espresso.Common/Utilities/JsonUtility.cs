using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Espresso.Common.Utilities
{
    public static class JsonUtility
    {
        public static async Task<string> Serialize<TValue>(
            TValue value,
            CancellationToken cancellationToken
        )
        {
            var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(
                utf8Json: stream,
                value: value,
                cancellationToken: cancellationToken
            );

            stream.Position = 0;

            using var reader = new StreamReader(stream);

            var jsonValue = await reader
                .ReadToEndAsync();

            return jsonValue;
        }

        public static async Task<TValue?> Deserialize<TValue>(
            string json,
            CancellationToken cancellationToken
        )
        {
            var utf8Bytes = System.Text.Encoding.UTF8.GetBytes(json);

            var stream = new MemoryStream(utf8Bytes);

            var value = await JsonSerializer.DeserializeAsync<TValue?>(
                utf8Json: stream,
                cancellationToken: cancellationToken
            );

            return value;
        }
    }
}