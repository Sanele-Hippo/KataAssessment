using Kata.Challange.Characters.Services.Providers;
using Kata.Challange.Characters.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata.Challange.Characters.Tests.Kata.Challange.Characters.Services.Services
{
    [TestFixture]
    internal class Given_source_service
    {
        public class When_reading_char_from_source : SourceServiceBase
        {
            private readonly List<char> _results = [];
            private readonly List<char> _expectedCharacters = ['a', 'd', 'c', '\n', 'd'];

            [OneTimeSetUp]
            public void OneTimeSetUp()
            {
                MockReadCharResponse(['a', 'd', 'c', '\n', 'd']);
            }

            [Test]
            public async Task Then_it_should_return_each_character_from_source()
            {
                await foreach (var character in SourceService.ReadCharAsync())
                {
                    _results.Add(character);
                }

                Assert.That(_results, Is.EqualTo(_expectedCharacters));
            }
        }

        public class When_reading_multiple_chars_with_valid_count_number : SourceServiceBase
        {
            private readonly List<char> _expectedCharacters = ['a', 'd', 'c', '\n', 'd'];

            [OneTimeSetUp]
            public void OneTimeSetUp()
            {
                MockReadCharsValidResponse(['a', 'd', 'c', '\n', 'd']);
            }

            [Test]
            public async Task Then_it_should_return_characters_from_source()
            {
                var characters = await SourceService.ReadCharsAsync(5);
                Assert.That(characters.Count, Is.EqualTo(_expectedCharacters.Count));
            }
        }

        public class When_reading_multiple_chars_with_invalid_count_number : SourceServiceBase
        {
            private readonly List<char> _expectedCharacters = ['a', 'd', 'c', '\n', 'd'];

            [OneTimeSetUp]
            public void OneTimeSetUp()
            {
                MockReadCharsInValidResponse();
            }

            [Test]
            public void Then_it_should_throw_exception()
            {
                var exception = Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                     await SourceService.ReadCharsAsync(0));

                Assert.That(exception.Message, Does.Contain("Count must be at least 1."));
            }
        }

        public class SourceServiceBase
        {
            protected Mock<ICharacterProvider> CharacterProviderMock;
            protected ISourceService SourceService;

            [OneTimeSetUp]
            public void TestBaseOneTimeSetUp()
            {
                CharacterProviderMock = new Mock<ICharacterProvider>();
                SourceService = new SourceService(CharacterProviderMock.Object);
            }

            protected void MockReadCharResponse(List<char> response)
            {
                CharacterProviderMock.Setup(s =>
                    s.GetCharacterAsync()).Returns(GetAsyncEnumerable(response));
            }

            protected void MockReadCharsInValidResponse()
            {
                CharacterProviderMock.Setup(s =>
                   s.GetCharactersAsync(It.IsAny<int>()))
                    .Throws(new ArgumentOutOfRangeException("count", "Count must be at least 1."));
            }

            protected void MockReadCharsValidResponse(IEnumerable<char> response)
            {
                CharacterProviderMock.Setup(s =>
                   s.GetCharactersAsync(It.Is<int>(actual =>
                     ValidateReadCharsRequest(actual)))).ReturnsAsync(response);
            }

            private static bool ValidateReadCharsRequest(int actual)
            {
                return actual > 0;
            }

            private static async IAsyncEnumerable<char> GetAsyncEnumerable(IEnumerable<char> characters)
            {
                foreach (var character in characters)
                {
                    yield return await Task.FromResult(character);
                }
            }
        }
    }
}
