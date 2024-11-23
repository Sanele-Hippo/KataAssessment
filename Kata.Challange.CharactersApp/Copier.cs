using Kata.Challange.Characters.Services.Services;

namespace Kata.Challange.CharactersApp
{
    /// <summary>
    /// Composition design approach - Copier class has a source and destionation service injected
    /// The Copier class is responsable for copying character(s) from source to new destination
    /// The Copier class only exists as a contrete implimantation for the purpose of this task
    /// </summary>
    /// <param name="sourceService"></param>
    /// <param name="destinationService"></param>
    public class Copier(ISourceService sourceService, IDestinationService destinationService)
    {
        private const char _newLine = '\n'; // Can be stored from app settings

        /// <summary>
        /// Copy each char from source to destination
        /// </summary>
        /// <returns></returns>
        public async Task CopySingleAsync()
        {
            await foreach (var character in sourceService.ReadCharAsync())
            {
                if (character.Equals(_newLine))
                    break;
                 
                await destinationService.WriteCharAsync(character);
            }
        }

        /// <summary>
        /// Copy multiple chars from source to destination
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task CopyMultipleAsync(int count)
        {
            var characters = await sourceService.ReadCharsAsync(count);
            var charsToCopy = characters.TakeWhile(character => !character.Equals(_newLine));

            if (charsToCopy.Any())
            {
                await destinationService.WriteCharsAsync(charsToCopy);
            }
        }
    }
}
