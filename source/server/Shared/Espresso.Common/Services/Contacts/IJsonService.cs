// IJsonService.cs
//
// © 2022 Espresso News. All rights reserved.

namespace Espresso.Common.Services.Contracts;

/// <summary>
/// JSON serializer/deserializer service.
/// </summary>
public interface IJsonService
{
    /// <summary>
    /// Serializes <paramref name="value"/> to JSON string.
    /// </summary>
    /// <typeparam name="TValue">Type that is being serialized.</typeparam>
    /// <param name="value">value that is being serialized.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public Task<string> Serialize<TValue>(
        TValue value,
        CancellationToken cancellationToken);

    /// <summary>
    /// Serializes <paramref name="value"/> to JSON string.
    /// </summary>
    /// <typeparam name="TValue">Type that is being serialized.</typeparam>
    /// <param name="value">value that is being serialized.</param>
    /// <returns>Serialized value.</returns>
    public string Serialize<TValue>(TValue value);

    /// <summary>
    /// Serializes <paramref name="value"/> to JSON Stream.
    /// </summary>
    /// <typeparam name="TValue">Type that is being serialized.</typeparam>
    /// <param name="value">value that is being serialized.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public Task<Stream> SerializeToStream<TValue>(
        TValue value,
        CancellationToken cancellationToken);

    /// <summary>
    /// Deserializes <paramref name="json"/> string into object of type <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">Resulting object type.</typeparam>
    /// <param name="json">JSON string.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public Task<TValue?> Deserialize<TValue>(
        string json,
        CancellationToken cancellationToken);

    /// <summary>
    /// Deserializes <paramref name="utf8Bytes"/> bytes into object of type <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">Resulting object type.</typeparam>
    /// <param name="utf8Bytes">JSON UTF8 bytes.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public Task<TValue?> Deserialize<TValue>(
        byte[] utf8Bytes,
        CancellationToken cancellationToken);

    /// <summary>
    /// Deserializes <paramref name="utf8Stream"/> stream into object of type <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">Resulting object type.</typeparam>
    /// <param name="utf8Stream">JSON UTF8 stream.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public Task<TValue?> Deserialize<TValue>(
        Stream utf8Stream,
        CancellationToken cancellationToken);

    /// <summary>
    /// Deserializes <paramref name="json"/> string into object of type <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">Resulting object type.</typeparam>
    /// <param name="json">JSON string.</param>
    /// <returns>Deserialized object.</returns>
    public TValue? Deserialize<TValue>(string json);

    /// <summary>
    /// Creates <see cref="HttpContent"/> by serializing <paramref name="value"/> into JSON string.
    /// </summary>
    /// <typeparam name="TValue">Type being serialized.</typeparam>
    /// <param name="value">Value being serialized.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public Task<HttpContent> GetJsonHttpContent<TValue>(
        TValue value,
        CancellationToken cancellationToken);
}
