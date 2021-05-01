using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Espresso.Common.Constants;
using Espresso.Common.Services.Contracts;

namespace Espresso.Common.Services.Implementations
{
    public class SystemTextJsonService : IJsonService
    {
        #region Fields
        /// <summary>
        /// Default JsonSerializerOptions
        /// </summary>
        private readonly JsonSerializerOptions _defaultJsonSerializerOptions;
        #endregion

        #region Constructors
        public SystemTextJsonService(
            JsonSerializerOptions defaultJsonSerializerOptions
        )
        {
            _defaultJsonSerializerOptions = defaultJsonSerializerOptions;
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
                options: _defaultJsonSerializerOptions,
                cancellationToken: cancellationToken
            );

            stream.Position = 0;

            using var reader = new StreamReader(stream);

            var jsonValue = await reader
                .ReadToEndAsync();

            return jsonValue;
        }

        public string Serialize<TValue>(TValue value)
        {
            var jsonString = JsonSerializer.Serialize(
                value: value,
                options: _defaultJsonSerializerOptions
            );

            return jsonString;
        }

        public async Task<TValue?> Deserialize<TValue>(
            string json,
            CancellationToken cancellationToken
        )
        {
            var utf8Bytes = Encoding.UTF8.GetBytes(json);

            var stream = new MemoryStream(utf8Bytes);

            var value = await JsonSerializer.DeserializeAsync<TValue?>(
                utf8Json: stream,
                options: _defaultJsonSerializerOptions,
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
                options: _defaultJsonSerializerOptions,
                cancellationToken: cancellationToken
            );

            return value;
        }

        public TValue? Deserialize<TValue>(string json)
        {
            var value = JsonSerializer.Deserialize<TValue?>(
                json: json,
                options: _defaultJsonSerializerOptions
            );

            return value;
        }

        public async Task<HttpContent> GetJsonHttpContent<TValue>(
            TValue value,
            CancellationToken cancellationToken
        )
        {
            var content = await Serialize(value, cancellationToken);

            var httpRequestMessage = new StringContent(
                content: content,
                encoding: Encoding.UTF8,
                mediaType: MimeTypeConstants.Json
            );

            return httpRequestMessage;
        }
        #endregion
    }
}
