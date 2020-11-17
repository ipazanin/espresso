// using System.IO;
// using System.Text.Json;
// using System.Threading;
// using System.Threading.Tasks;

// namespace Espresso.Common.Utilities
// {
//     public static class JsonUtility
//     {
//         #region Properties
//         public static JsonSerializerOptions DefaultOptions => new JsonSerializerOptions
//         {
//             PropertyNameCaseInsensitive = true,
//             AllowTrailingCommas = true,
//             MaxDepth = 100,
//             ReadCommentHandling = JsonCommentHandling.Skip,
//         };
//         #endregion

//         #region Methods
//         public static async Task<string> Serialize<TValue>(
//             TValue value,
//             CancellationToken cancellationToken
//         )
//         {
//             var stream = new MemoryStream();
//             await JsonSerializer.SerializeAsync(
//                 utf8Json: stream,
//                 value: value,
//                 options: DefaultOptions,
//                 cancellationToken: cancellationToken
//             );

//             stream.Position = 0;

//             using var reader = new StreamReader(stream);

//             var jsonValue = await reader
//                 .ReadToEndAsync();

//             return jsonValue;
//         }

//         public static async Task<TValue?> Deserialize<TValue>(
//             string json,
//             CancellationToken cancellationToken
//         )
//         {
//             var utf8Bytes = System.Text.Encoding.UTF8.GetBytes(json);

//             var stream = new MemoryStream(utf8Bytes);

//             var value = await JsonSerializer.DeserializeAsync<TValue?>(
//                 utf8Json: stream,
//                 options: DefaultOptions,
//                 cancellationToken: cancellationToken
//             );

//             return value;
//         }

//         public static async Task<TValue?> Deserialize<TValue>(
//             byte[] utf8Bytes,
//             CancellationToken cancellationToken
//         )
//         {
//             var stream = new MemoryStream(utf8Bytes)
//             {
//                 Position = 0
//             };

//             var value = await JsonSerializer.DeserializeAsync<TValue?>(
//                 utf8Json: stream,
//                 options: DefaultOptions,
//                 cancellationToken: cancellationToken
//             );

//             return value;
//         }
//     }
//     #endregion
// }