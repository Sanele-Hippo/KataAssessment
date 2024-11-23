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
    internal class Given_destination_service
    {
        public class When_writing_char_to_destination : DestinationServiceBase
        {
            private const char _characterToWrite = 'a';

            [OneTimeSetUp]
            public void OneTimeSetUp() { }

            [Test]
            public async Task Then_it_should_add_character_to_destination_source()
            {
                await DestinationService.WriteCharAsync(_characterToWrite);
                CharacterProviderMock.Verify(s =>
                    s.AddCharacterAsync(_characterToWrite), Times.Once);
            }
        }

        public class When_writing_chars_to_destination : DestinationServiceBase
        {
            private readonly List<char> _characterToWrite = ['a', 'b'];

            [OneTimeSetUp]
            public void OneTimeSetUp() { }

            [Test]
            public async Task Then_it_should_add_character_to_destination_source()
            {
                await DestinationService.WriteCharsAsync(_characterToWrite);
                CharacterProviderMock.Verify(s =>
                    s.AddCharactersAsync(_characterToWrite), Times.Once);
            }
        }

        public class DestinationServiceBase
        {
            protected Mock<ICharacterProvider> CharacterProviderMock;
            protected IDestinationService DestinationService;
            [OneTimeSetUp]
            public void TestBaseOneTimeSetUp()
            {
                CharacterProviderMock = new Mock<ICharacterProvider>();
                DestinationService = new DestinationService(CharacterProviderMock.Object);
            }

        }
    }
}
