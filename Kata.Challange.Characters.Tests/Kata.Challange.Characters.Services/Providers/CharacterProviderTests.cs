using Kata.Challange.Characters.Services.Providers;

namespace Kata.Challange.Characters.Tests.Kata.Challange.Characters.Services.Providers
{
    [TestFixture]
    internal class Given_character_provider
    {
        public class When_getting_character : CharacterProviderBase
        {
            private List<char> _expectedCharacters;

            [OneTimeSetUp]
            public void OneTimeSetUp()
            {
                _expectedCharacters = ['a', 'b', 'c', '\n'];
            }

            [Test]
            public async Task Then_it_should_return_each_character_in_a_source()
            {
                var results = new List<char>();

                await foreach (var character in CharacterProvider.GetCharacterAsync())
                {
                    results.Add(character);
                }

                Assert.That(_expectedCharacters, Is.EqualTo(results));
            }
        }

        public class When_getting_multiple_characters : CharacterProviderBase
        {
            [OneTimeSetUp]
            public void OneTimeSetUp() { }

            [TestCase(1)]
            [TestCase(2)]
            public async Task Then_with_valid_count_number_it_should_return_characters(int count)
            {
                var characters = await CharacterProvider.GetCharactersAsync(count);
                Assert.That(characters.Count, Is.EqualTo(count));
            }

            [Test]
            public void Then_with_invalid_count_number_it_should_throw_exception()
            {
                var exeption = Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                    await CharacterProvider.GetCharactersAsync(0));

                Assert.That(exeption.Message, Does.Contain("Count must be at least 1."));
            }
        }

        public class When_adding_character : CharacterProviderBase
        {
            [OneTimeSetUp]
            public void OneTimeSetUp() { }

            [Test]
            public async Task Then_it_should_add_single_character_to_destination_source()
            {
                await CharacterProvider.AddCharacterAsync('a');
                var characters = await CharacterProvider.GetDestinationCharactersAsync();

                Assert.Multiple(() =>
                {
                    Assert.That(characters.Count(), Is.EqualTo(1));
                    Assert.That(characters.First(), Is.EqualTo('a'));
                });
            }
        }

        public class When_adding_multiple_characters : CharacterProviderBase
        {
            [OneTimeSetUp]
            public void OneTimeSetUp() { }

            [Test]
            public async Task Then_it_should_add_multiple_characters_to_destination_source()
            {
                await CharacterProvider.AddCharactersAsync(['a', 'b']);
                var characters = await CharacterProvider.GetDestinationCharactersAsync();

                Assert.Multiple(() =>
                {
                    Assert.That(characters.Count(), Is.EqualTo(2));
                    Assert.That(characters.First(), Is.EqualTo('a'));
                    Assert.That(characters.Last(), Is.EqualTo('b'));
                });
            }
        }

        public class CharacterProviderBase
        {
            protected ICharacterProvider CharacterProvider;
            [OneTimeSetUp]
            public void TestBaseOneTimeSetUp()
            {
                CharacterProvider = new CharacterProvider(['a', 'b', 'c', '\n']);
            }

        }
    }
}
