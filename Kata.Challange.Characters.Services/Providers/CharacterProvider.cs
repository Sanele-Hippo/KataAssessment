namespace Kata.Challange.Characters.Services.Providers
{
    /// <summary>
    /// Characters data access provider
    /// Note: Forcing a method to be async is not recommended as it can lead to unnecessary task scheduling, Just used it here as a reflaction of a source or repository
    /// </summary>
    public interface ICharacterProvider
    {
        /// <summary>
        /// Allowing consumers to asynchronously iterate over a collection of characters
        /// Use yield to return character on demand in case the consumer breaks from the loop when there is new line ('\n')
        /// </summary>
        /// <returns>IAsyncEnumerable<char></returns>
        IAsyncEnumerable<char> GetCharacterAsync();

        /// <summary>
        /// Allowing consumers to retrieve specific number of characters in a source of characters
        /// </summary>
        /// <param name="count"></param>
        /// <returns>IEnumerable<char></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        Task<IEnumerable<char>> GetCharactersAsync(int count);

        /// <summary>
        /// Allowing consumers to add new character in a destination source
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        Task AddCharacterAsync(char character);

        /// <summary>
        /// Allowing consumers to add range of new characters in a destination source
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        Task AddCharactersAsync(IEnumerable<char> characters);

        /// <summary>
        /// Allowing consumers to get characters from destination source
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<char>> GetDestinationCharactersAsync();
    }
    public class CharacterProvider(List<char> characters) : ICharacterProvider
    {
        private readonly List<char> _charactersSource =
            characters.Count > 0 ? characters : ['o', 'l', 'd', 'm', 'u', '\n', 't', 'u', 'a', 'l', '\n'];

        public List<char> CharactersDestination { get; } = [];

        public async Task AddCharacterAsync(char character)
        {
            CharactersDestination.Add(character);
            await Task.CompletedTask;
        }

        public async Task AddCharactersAsync(IEnumerable<char> characters)
        {
            CharactersDestination.AddRange(characters);
            await Task.CompletedTask;
        }

        public async IAsyncEnumerable<char> GetCharacterAsync()
        {
            foreach (var character in _charactersSource)
                yield return await Task.FromResult(character);
        }

        public Task<IEnumerable<char>> GetCharactersAsync(int count)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be at least 1.");

            var characters = _charactersSource.Take(count);
            return Task.FromResult(characters);
        }

        public async Task<IEnumerable<char>> GetDestinationCharactersAsync()
        {
            return await Task.FromResult(CharactersDestination);
        }
    }
}
