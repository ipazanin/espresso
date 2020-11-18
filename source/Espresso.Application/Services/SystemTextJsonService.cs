using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Application.IServices;

namespace Espresso.Application.Services
{
    public class SystemTextJsonService : IJsonService
    {
        #region Fields
        public static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            AllowTrailingCommas = true,
            MaxDepth = 100,
            ReadCommentHandling = JsonCommentHandling.Skip,
        };
        #endregion

        #region Constructors
        public SystemTextJsonService()
        {

        }
        #endregion

        #region Methods
        public async Task<string> Serialize<TValue>(
            TValue value,
            CancellationToken cancellationToken
        )
        {
            var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(
                utf8Json: stream,
                value: value,
                options: DefaultJsonSerializerOptions,
                cancellationToken: cancellationToken
            );

            stream.Position = 0;

            using var reader = new StreamReader(stream);

            var jsonValue = await reader
                .ReadToEndAsync();

            return jsonValue;
        }

        public async Task<TValue?> Deserialize<TValue>(
            string json,
            CancellationToken cancellationToken
        )
        {
            var utf8Bytes = System.Text.Encoding.UTF8.GetBytes(json);

            var stream = new MemoryStream(utf8Bytes);

            var value = await JsonSerializer.DeserializeAsync<TValue?>(
                utf8Json: stream,
                options: DefaultJsonSerializerOptions,
                cancellationToken: cancellationToken
            );

            return value;
        }

        public async Task<TValue?> Deserialize<TValue>(
            byte[] utf8Bytes,
            CancellationToken cancellationToken
        )
        {
            var stream = new MemoryStream(utf8Bytes)
            {
                Position = 0
            };

            var value = await JsonSerializer.DeserializeAsync<TValue?>(
                utf8Json: stream,
                options: DefaultJsonSerializerOptions,
                cancellationToken: cancellationToken
            );

            return value;
        }

        public TValue? Deserialize<TValue>(string json)
        {
            var value = JsonSerializer.Deserialize<TValue?>(
                json: json,
                options: DefaultJsonSerializerOptions
            );

            return value;
        }

        public string Serialize<TValue>(TValue value)
        {
            var jsonString = JsonSerializer.Serialize(
                value: value,
                options: DefaultJsonSerializerOptions
            );

            return jsonString;
        }

        public static void MapJsonSerializerOptionsToDefaultOptions(
            JsonSerializerOptions jsonSerializerOptions
        )
        {
            var defaultJsonOptions = DefaultJsonSerializerOptions;

            jsonSerializerOptions.PropertyNameCaseInsensitive = defaultJsonOptions.PropertyNameCaseInsensitive;
            jsonSerializerOptions.AllowTrailingCommas = defaultJsonOptions.AllowTrailingCommas;
            jsonSerializerOptions.MaxDepth = defaultJsonOptions.MaxDepth;
            jsonSerializerOptions.ReadCommentHandling = defaultJsonOptions.ReadCommentHandling;
        }
        #endregion
    }
}