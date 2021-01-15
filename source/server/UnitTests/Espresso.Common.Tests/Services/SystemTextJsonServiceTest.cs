using System.IO;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Espresso.Common.Services;
using Espresso.Common.Tests.TestUtilities.Models;
using Xunit;

namespace Espresso.Common.Tests.Services
{
    /// <summary>
    /// SystemTextJsonService
    /// </summary>
    public class SystemTextJsonServiceTest
    {
        #region Serialize
        [Fact]
        public async Task Serialize_ReturnsTaskWithSerializedObject_WhenObjectIsNotNull()
        {
            #region Arrange
            var someObject = new
            {
                FirstProperty = "FirstPropertyValue",
                SecondProperty = 1,
                ThirdProperty = true,
                FourthProperty = new
                {
                    FourthPropertyFirstProperty = 1
                }
            };

            var expectedSerializedObject = "{\"firstProperty\":\"FirstPropertyValue\",\"secondProperty\":1,\"thirdProperty\":true,\"fourthProperty\":{\"fourthPropertyFirstProperty\":1}}";

            var systemTextJsonService = new SystemTextJsonService(
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
            #endregion Arrange

            #region Act
            var actualSerializedObject = await systemTextJsonService
                .Serialize(
                    value: someObject,
                    cancellationToken: default
                );
            #endregion Act

            #region Assert
            Assert.Equal(
                expected: expectedSerializedObject,
                actual: actualSerializedObject
            );
            #endregion Assert
        }

        [Fact]
        public async Task Serialize_ReturnsTaskWithNullString_WhenObjectIsNull()
        {
            #region Arrange
            SystemTextJsonService someObject = null!;

            var systemTextJsonService = new SystemTextJsonService(
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );

            var expectedSerializedObject = "null";
            #endregion Arrange

            #region Act
            var actualSerializedObject = await systemTextJsonService
                .Serialize(
                    value: someObject,
                    cancellationToken: default
                );
            #endregion Act

            #region Assert
            Assert.Equal(
                expected: expectedSerializedObject,
                actual: actualSerializedObject
            );
            #endregion Assert
        }

        [Fact]
        public void Serialize_ReturnsSerializedObject_WhenObjectIsNotNull()
        {
            #region Arrange
            var someObject = new
            {
                FirstProperty = "FirstPropertyValue",
                SecondProperty = 1,
                ThirdProperty = true,
                FourthProperty = new
                {
                    FourthPropertyFirstProperty = 1
                }
            };

            var expectedSerializedObject = "{\"firstProperty\":\"FirstPropertyValue\",\"secondProperty\":1,\"thirdProperty\":true,\"fourthProperty\":{\"fourthPropertyFirstProperty\":1}}";

            var systemTextJsonService = new SystemTextJsonService(
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
            #endregion Arrange

            #region Act
            var actualSerializedObject = systemTextJsonService
                .Serialize(
                    value: someObject
                );
            #endregion Act

            #region Assert
            Assert.Equal(
                expected: expectedSerializedObject,
                actual: actualSerializedObject
            );
            #endregion Assert
        }

        [Fact]
        public void Serialize_ReturnsNull_WhenObjectIsNull()
        {
            #region Arrange
            SystemTextJsonService someObject = null!;

            var systemTextJsonService = new SystemTextJsonService(
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );

            var expectedSerializedObject = "null";
            #endregion Arrange

            #region Act
            var actualSerializedObject = systemTextJsonService
                .Serialize(
                    value: someObject
                );
            #endregion Act

            #region Assert
            Assert.Equal(
                expected: expectedSerializedObject,
                actual: actualSerializedObject
            );
            #endregion Assert
        }
        #endregion Serialize

        #region Deserialize
        [Fact]
        public async Task Deserialize_ReturnsTaskDeserializedObject_WhenInputJsonStringIsInCorrectFormat()
        {
            #region Arrange
            var expectedObject = new ExampleClassWithPublicSetters
            {
                FirstProperty = "FirstPropertyValue",
                SecondProperty = 1,
                ThirdProperty = true,
                FourthProperty = new ExampleSubClassWithPublicSetters
                {
                    FourthPropertyFirstProperty = 1
                }
            };

            var serializedObject = "{\"firstProperty\":\"FirstPropertyValue\",\"secondProperty\":1,\"thirdProperty\":true,\"fourthProperty\":{\"fourthPropertyFirstProperty\":1}}";

            var systemTextJsonService = new SystemTextJsonService(
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
            #endregion Arrange

            #region Act
            var actualDeserializedObject = await systemTextJsonService.Deserialize<ExampleClassWithPublicSetters>(
                json: serializedObject,
                cancellationToken: default
            );
            #endregion Act

            #region Assert
            Assert.Equal(
                expected: expectedObject,
                actual: actualDeserializedObject
            );
            #endregion Assert
        }

        [Fact]
        public async Task Deserialize_ThrowsJsonExceptionAsynchronously_WhenInputJsonStringIsNotInCorrectFormat()
        {
            #region Arrange
            var expectedObject = new ExampleClassWithPublicSetters
            {
                FirstProperty = "FirstPropertyValue",
                SecondProperty = 1,
                ThirdProperty = true,
                FourthProperty = new ExampleSubClassWithPublicSetters
                {
                    FourthPropertyFirstProperty = 1
                }
            };

            var serializedObject = "{\firstProperty}";

            var systemTextJsonService = new SystemTextJsonService(
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
            #endregion Arrange

            #region Act/Assert
            await Assert.ThrowsAsync<JsonException>(async () =>
            {
                var actualDeserializedObject = await systemTextJsonService.Deserialize<ExampleClassWithPublicSetters>(
                    json: serializedObject,
                    cancellationToken: default
                );
            });
            #endregion Act/Assert
        }

        [Fact]
        public async Task Deserialize_ReturnsTaskDeserializedObject_WhenInputByteArrayIsInCorrectFormat()
        {
            #region Arrange
            var expectedObject = new ExampleClassWithPublicSetters
            {
                FirstProperty = "FirstPropertyValue",
                SecondProperty = 1,
                ThirdProperty = true,
                FourthProperty = new ExampleSubClassWithPublicSetters
                {
                    FourthPropertyFirstProperty = 1
                }
            };

            var serializedByteArray = Encoding.UTF8.GetBytes("{\"firstProperty\":\"FirstPropertyValue\",\"secondProperty\":1,\"thirdProperty\":true,\"fourthProperty\":{\"fourthPropertyFirstProperty\":1}}");

            var systemTextJsonService = new SystemTextJsonService(
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
            #endregion Arrange

            #region Act
            var actualDeserializedObject = await systemTextJsonService.Deserialize<ExampleClassWithPublicSetters>(
                utf8Bytes: serializedByteArray,
                cancellationToken: default
            );
            #endregion Act

            #region Assert
            Assert.Equal(
                expected: expectedObject,
                actual: actualDeserializedObject
            );
            #endregion Assert
        }

        [Fact]
        public async Task Deserialize_ThrowsJsonExceptionAsynchronously_WhenInputByteArrayIsNotInCorrectFormat()
        {
            #region Arrange
            var expectedObject = new ExampleClassWithPublicSetters
            {
                FirstProperty = "FirstPropertyValue",
                SecondProperty = 1,
                ThirdProperty = true,
                FourthProperty = new ExampleSubClassWithPublicSetters
                {
                    FourthPropertyFirstProperty = 1
                }
            };

            var serializedByteArray = Encoding.UTF8.GetBytes("{\firstProperty}");

            var systemTextJsonService = new SystemTextJsonService(
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
            #endregion Arrange

            #region Act/Assert
            await Assert.ThrowsAsync<JsonException>(async () =>
            {
                var actualDeserializedObject = await systemTextJsonService.Deserialize<ExampleClassWithPublicSetters>(
                    utf8Bytes: serializedByteArray,
                    cancellationToken: default
                );
            });
            #endregion Act/Assert
        }


        [Fact]
        public void Deserialize_ReturnsDeserializedObject_WhenInputJsonStringIsInCorrectFormat()
        {
            #region Arrange
            var expectedObject = new ExampleClassWithPublicSetters
            {
                FirstProperty = "FirstPropertyValue",
                SecondProperty = 1,
                ThirdProperty = true,
                FourthProperty = new ExampleSubClassWithPublicSetters
                {
                    FourthPropertyFirstProperty = 1
                }
            };

            var serializedObject = "{\"firstProperty\":\"FirstPropertyValue\",\"secondProperty\":1,\"thirdProperty\":true,\"fourthProperty\":{\"fourthPropertyFirstProperty\":1}}";

            var systemTextJsonService = new SystemTextJsonService(
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
            #endregion Arrange

            #region Act
            var actualDeserializedObject = systemTextJsonService.Deserialize<ExampleClassWithPublicSetters>(
                json: serializedObject
            );
            #endregion Act

            #region Assert
            Assert.Equal(
                expected: expectedObject,
                actual: actualDeserializedObject
            );
            #endregion Assert
        }

        [Fact]
        public void Deserialize_ThrowsJsonException_WhenInputJsonStringIsNotInCorrectFormat()
        {
            #region Arrange
            var expectedObject = new ExampleClassWithPublicSetters
            {
                FirstProperty = "FirstPropertyValue",
                SecondProperty = 1,
                ThirdProperty = true,
                FourthProperty = new ExampleSubClassWithPublicSetters
                {
                    FourthPropertyFirstProperty = 1
                }
            };

            var serializedObject = "{\firstProperty}";

            var systemTextJsonService = new SystemTextJsonService(
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
            #endregion Arrange

            #region Act/Assert
            Assert.Throws<JsonException>(() =>
            {
                var actualDeserializedObject = systemTextJsonService.Deserialize<ExampleClassWithPublicSetters>(
                    json: serializedObject
                );
            });
            #endregion Act/Assert
        }
        #endregion Deserialize

        #region GetJsonHttpContent
        [Fact]
        public async Task GetJsonHttpContent_ReturnsHttpStringContentWithSeserializedObject_WhenInputJsonStringIsInCorrectFormat()
        {
            #region Arrange
            var expectedObject = new ExampleClassWithPublicSetters
            {
                FirstProperty = "FirstPropertyValue",
                SecondProperty = 1,
                ThirdProperty = true,
                FourthProperty = new ExampleSubClassWithPublicSetters
                {
                    FourthPropertyFirstProperty = 1
                }
            };

            var systemTextJsonService = new SystemTextJsonService(
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
            #endregion Arrange

            #region Act
            var actualHttpStringContent = await systemTextJsonService.GetJsonHttpContent(
                value: expectedObject,
                cancellationToken: default
            );
            var actualObject = await actualHttpStringContent.ReadFromJsonAsync<ExampleClassWithPublicSetters>();
            #endregion Act

            #region Assert
            Assert.Equal(
                expected: expectedObject,
                actual: actualObject
            );
            #endregion Assert
        }
        #endregion GetJsonHttpContent
    }
}
