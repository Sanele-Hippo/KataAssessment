// See https://aka.ms/new-console-template for more information
using Kata.Challange.CharactersAppTwo;
using Kata.Challange.CharactersAppTwo.Enums;
using Kata.Challange.CharactersAppTwo.ServiceCollections;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection().AddServices().BuildServiceProvider();
var copier = serviceProvider.GetRequiredService<Copier>();

Console.WriteLine("Enter copy mechanism [Single or Multiple]");
var mechanismInput = Console.ReadLine();

if (Enum.TryParse(mechanismInput, out CopyMechanism mechanism))
{
    Console.WriteLine($"Copy mechanism:{mechanism} process start");
    await copier.Copy(mechanism);
    Console.WriteLine($"Copy mechanism:{mechanism} process end");
}
else
{
    Console.WriteLine($"'{mechanismInput}' is not a valid input");
}