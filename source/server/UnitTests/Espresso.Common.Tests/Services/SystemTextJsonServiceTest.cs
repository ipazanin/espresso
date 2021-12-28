// SystemTextJsonServiceTest.cs
//
// © 2021 Espresso News. All rights reserved.

using System.Net.Http.Json;
using Espresso.Common.Services.Implementations;
using Espresso.Common.Tests.TestUtilities.Models;
using Xunit;

namespace Espresso.Common.Tests.Services;

/// <summary>
/// SystemTextJsonService.
/// </summary>
public class SystemTextJsonServiceTest
{
    /// <summary>
    ///
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    [Fact]
    public async Task Serialize_ReturnsTaskWithSerializedObject_WhenObjectIsNotNull()
    {
        var someObject = new
        {
            FirstProperty = "FirstPropertyValue",
            SecondProperty = 1,
            ThirdProperty = true,
            FourthProperty = new
            {
                FourthPropertyFirstProperty = 1,
            },
        };

        const string? ExpectedSerializedObject = "{\"firstProperty\":\"FirstPropertyValue\",\"secondProperty\":1,\"thirdProperty\":true,\"fourthProperty\":{\"fourthPropertyFirstProperty\":1}}";

        var systemTextJsonService = new SystemTextJsonService(
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

        var actualSerializedObject = await systemTextJsonService
            .Serialize(
                value: someObject,
                cancellationToken: default);

        Assert.Equal(
            expected: ExpectedSerializedObject,
            actual: actualSerializedObject);
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    [Fact]
    public async Task Serialize_ReturnsTaskWithNullString_WhenObjectIsNull()
    {
        SystemTextJsonService someObject = null!;

        var systemTextJsonService = new SystemTextJsonService(
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

        const string? ExpectedSerializedObject = "null";

        var actualSerializedObject = await systemTextJsonService
            .Serialize(
                value: someObject,
                cancellationToken: default);

        Assert.Equal(
            expected: ExpectedSerializedObject,
            actual: actualSerializedObject);
    }

    [Fact]
    public void Serialize_ReturnsSerializedObject_WhenObjectIsNotNull()
    {
        var someObject = new
        {
            FirstProperty = "FirstPropertyValue",
            SecondProperty = 1,
            ThirdProperty = true,
            FourthProperty = new
            {
                FourthPropertyFirstProperty = 1,
            },
        };

        const string? ExpectedSerializedObject = "{\"firstProperty\":\"FirstPropertyValue\",\"secondProperty\":1,\"thirdProperty\":true,\"fourthProperty\":{\"fourthPropertyFirstProperty\":1}}";

        var systemTextJsonService = new SystemTextJsonService(
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

        var actualSerializedObject = systemTextJsonService
            .Serialize(
                value: someObject);

        Assert.Equal(
            expected: ExpectedSerializedObject,
            actual: actualSerializedObject);
    }

    [Fact]
    public void Serialize_ReturnsNull_WhenObjectIsNull()
    {
        SystemTextJsonService someObject = null!;

        var systemTextJsonService = new SystemTextJsonService(
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

        const string? ExpectedSerializedObject = "null";

        var actualSerializedObject = systemTextJsonService
            .Serialize(
                value: someObject);

        Assert.Equal(
            expected: ExpectedSerializedObject,
            actual: actualSerializedObject);
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    [Fact]
    public async Task Deserialize_ReturnsTaskDeserializedObject_WhenInputJsonStringIsInCorrectFormat()
    {
        var expectedObject = new ExampleClassWithPublicSetters
        {
            FirstProperty = "FirstPropertyValue",
            SecondProperty = 1,
            ThirdProperty = true,
            FourthProperty = new ExampleSubClassWithPublicSetters
            {
                FourthPropertyFirstProperty = 1,
            },
        };

        const string? SerializedObject = "{\"firstProperty\":\"FirstPropertyValue\",\"secondProperty\":1,\"thirdProperty\":true,\"fourthProperty\":{\"fourthPropertyFirstProperty\":1}}";

        var systemTextJsonService = new SystemTextJsonService(
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

        var actualDeserializedObject = await systemTextJsonService.Deserialize<ExampleClassWithPublicSetters>(
            json: SerializedObject,
            cancellationToken: default);

        Assert.Equal(
            expected: expectedObject,
            actual: actualDeserializedObject);
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    [Fact]
    public Task Deserialize_ThrowsJsonExceptionAsynchronously_WhenInputJsonStringIsNotInCorrectFormat()
    {
        const string? SerializedObject = "{\firstProperty}";

        var systemTextJsonService = new SystemTextJsonService(
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

        return Assert.ThrowsAsync<JsonException>(async () =>
         {
             var actualDeserializedObject = await systemTextJsonService.Deserialize<ExampleClassWithPublicSetters>(
                 json: SerializedObject,
                 cancellationToken: default);
         });
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    [Fact]
    public async Task Deserialize_ReturnsTaskDeserializedObject_WhenInputByteArrayIsInCorrectFormat()
    {
        var expectedObject = new ExampleClassWithPublicSetters
        {
            FirstProperty = "FirstPropertyValue",
            SecondProperty = 1,
            ThirdProperty = true,
            FourthProperty = new ExampleSubClassWithPublicSetters
            {
                FourthPropertyFirstProperty = 1,
            },
        };

        var serializedByteArray = Encoding.UTF8.GetBytes("{\"firstProperty\":\"FirstPropertyValue\",\"secondProperty\":1,\"thirdProperty\":true,\"fourthProperty\":{\"fourthPropertyFirstProperty\":1}}");

        var systemTextJsonService = new SystemTextJsonService(
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

        var actualDeserializedObject = await systemTextJsonService.Deserialize<ExampleClassWithPublicSetters>(
            utf8Bytes: serializedByteArray,
            cancellationToken: default);

        Assert.Equal(
            expected: expectedObject,
            actual: actualDeserializedObject);
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    [Fact]
    public Task Deserialize_ThrowsJsonExceptionAsynchronously_WhenInputByteArrayIsNotInCorrectFormat()
    {
        var serializedByteArray = Encoding.UTF8.GetBytes("{\firstProperty}");

        var systemTextJsonService = new SystemTextJsonService(
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

        return Assert.ThrowsAsync<JsonException>(async () =>
         {
             var actualDeserializedObject = await systemTextJsonService.Deserialize<ExampleClassWithPublicSetters>(
                 utf8Bytes: serializedByteArray,
                 cancellationToken: default);
         });
    }

    [Fact]
    public void Deserialize_ReturnsDeserializedObject_WhenInputJsonStringIsInCorrectFormat()
    {
        var expectedObject = new ExampleClassWithPublicSetters
        {
            FirstProperty = "FirstPropertyValue",
            SecondProperty = 1,
            ThirdProperty = true,
            FourthProperty = new ExampleSubClassWithPublicSetters
            {
                FourthPropertyFirstProperty = 1,
            },
        };

        const string? SerializedObject = "{\"firstProperty\":\"FirstPropertyValue\",\"secondProperty\":1,\"thirdProperty\":true,\"fourthProperty\":{\"fourthPropertyFirstProperty\":1}}";

        var systemTextJsonService = new SystemTextJsonService(
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

        var actualDeserializedObject = systemTextJsonService.Deserialize<ExampleClassWithPublicSetters>(
            json: SerializedObject);

        Assert.Equal(
            expected: expectedObject,
            actual: actualDeserializedObject);
    }

    [Fact]
    public void Deserialize_ThrowsJsonException_WhenInputJsonStringIsNotInCorrectFormat()
    {
        var expectedObject = new ExampleClassWithPublicSetters
        {
            FirstProperty = "FirstPropertyValue",
            SecondProperty = 1,
            ThirdProperty = true,
            FourthProperty = new ExampleSubClassWithPublicSetters
            {
                FourthPropertyFirstProperty = 1,
            },
        };

        const string? SerializedObject = "{\firstProperty}";

        var systemTextJsonService = new SystemTextJsonService(
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

        Assert.Throws<JsonException>(() =>
        {
            var actualDeserializedObject = systemTextJsonService.Deserialize<ExampleClassWithPublicSetters>(
                json: SerializedObject);
        });
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    [Fact]
    public async Task GetJsonHttpContent_ReturnsHttpStringContentWithSeserializedObject_WhenInputJsonStringIsInCorrectFormat()
    {
        var expectedObject = new ExampleClassWithPublicSetters
        {
            FirstProperty = "FirstPropertyValue",
            SecondProperty = 1,
            ThirdProperty = true,
            FourthProperty = new ExampleSubClassWithPublicSetters
            {
                FourthPropertyFirstProperty = 1,
            },
        };

        var systemTextJsonService = new SystemTextJsonService(
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

        var actualHttpStringContent = await systemTextJsonService.GetJsonHttpContent(
            value: expectedObject,
            cancellationToken: default);
        var actualObject = await actualHttpStringContent.ReadFromJsonAsync<ExampleClassWithPublicSetters>();

        Assert.Equal(
            expected: expectedObject,
            actual: actualObject);
    }
}
