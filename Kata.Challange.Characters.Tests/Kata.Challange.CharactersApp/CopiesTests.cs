using Kata.Challange.Characters.Services.Services;
using Kata.Challange.CharactersApp;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata.Challange.Characters.Tests.Kata.Challange.CharactersApp
{
    [TestFixture]
    internal class Given_copies
    {
        public class When_coping_each_character_from_source : CopiesBase
        {

            [OneTimeSetUp]
            public void OneTimeSetUp()
            {
                MockReadCharResponse(['a', 'b', 'c', '\n', 'd']);
            }

            [Test]
            public async Task Then_it_should_return_each_character_and_write_to_destination()
            {
                await Copier.CopySingleAsync();

                DestinationServiceMock.Verify(c => c.WriteCharAsync('a'), Times.Once);
                DestinationServiceMock.Verify(c => c.WriteCharAsync('b'), Times.Once);
                DestinationServiceMock.Verify(c => c.WriteCharAsync('c'), Times.Once);
                DestinationServiceMock.Verify(c => c.WriteCharAsync('d'), Times.Never);
                DestinationServiceMock.Verify(c => c.WriteCharAsync(It.IsAny<char>()), Times.Exactly(3));
            }
        }

        public class When_coping_multiple_characters_from_source : CopiesBase
        {
            private readonly List<char> _expectedCharacters = ['a', 'b', 'c'];

            [OneTimeSetUp]
            public void OneTimeSetUp()
            {
                MockReadCharsValidResponse(['a', 'b', 'c', '\n', 'd']);
            }

            [TestCase(5)]
            [TestCase(10)]
            public async Task Then_it_should_return_list_of_character_and_write_to_destination(int count)
            {
                await Copier.CopyMultipleAsync(count);

                DestinationServiceMock.Verify(c => c.WriteCharsAsync(It.Is<IEnumerable<char>>(actual
                    => ValidateWriteCharsRequest(actual, _expectedCharacters))));
            }
        }

        public class CopiesBase
        {
            protected Mock<ISourceService> SourceServiceMock;
            protected Mock<IDestinationService> DestinationServiceMock;
            protected Copier Copier;

            [OneTimeSetUp]
            public void TestBaseOneTimeSetUp()
            {
                SourceServiceMock = new Mock<ISourceService>();
                DestinationServiceMock = new Mock<IDestinationService>();
                Copier = new Copier(SourceServiceMock.Object, DestinationServiceMock.Object);
            }

            protected void MockReadCharResponse(IEnumerable<char> response)
            {
                SourceServiceMock.Setup(s =>
                    s.ReadCharAsync()).Returns(GetAsyncEnumerable(response));
            }

            protected void MockReadCharsValidResponse(IEnumerable<char> response)
            {
                SourceServiceMock.Setup(s =>
                    s.ReadCharsAsync(It.Is<int>(actual =>
                        ValidateReadCharsRequest(actual)))).ReturnsAsync(response);

            }

            protected static bool ValidateWriteCharsRequest(IEnumerable<char> actual, IEnumerable<char> expectedChars)
            {
                return actual.SequenceEqual(expectedChars);
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
