using Kata.Challange.CharactersAppTwo.Enums;
using Kata.Challange.CharactersAppTwo.Strategies;

namespace Kata.Challange.CharactersAppTwo
{
    /// <summary>
    /// Execute copy strategy
    /// Execution of copy strategy doesnt have to change when adding new copy machanism
    /// It avoid if statement on execution logic when adding new copy machanism to ensure the logic is closed for modification 
    /// </summary>
    /// <param name="copies"></param>
    public class Copier(Dictionary<CopyMechanism, Func<ICopyStrategy>> copies)
    {
        public async Task Copy(CopyMechanism copyMechanism)
        {
            await copies[copyMechanism]().Execute();
        }
    }
}
