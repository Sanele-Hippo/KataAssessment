using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata.Challange.CharactersAppTwo.Strategies
{
    /// <summary>
    /// The Copier class is responsable for copying characters from a source to a destination using strategy pattern
    /// Copy copy single and copy multiple are closed for modication
    /// New copy matchanism will extend the ICopy and implement its own behavior without modifying existing implimantation
    /// </summary>
    public interface ICopyStrategy
    {
        Task Execute();
    }
}
