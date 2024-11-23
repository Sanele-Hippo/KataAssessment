// See https://aka.ms/new-console-template for more information
using Kata.Challange.CharactersApp;
using Kata.Challange.CharactersApp.ServiceCollections;
using Microsoft.Extensions.DependencyInjection;
using System;

public class Program
{
    private static Copier _copier = null!;

    private static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection().AddServices().BuildServiceProvider();

        _copier = serviceProvider.GetRequiredService<Copier>();

        await ExecuteCopySingleAsync();
        await ExecuteCopyMultipleAsync();
    }

    private static async Task ExecuteCopySingleAsync()
    {
        Console.WriteLine("Copy each character process start");
        await _copier.CopySingleAsync();
        Console.WriteLine("Copy each character process end");
    }

    private static async Task ExecuteCopyMultipleAsync()
    {
        Console.WriteLine("Enter record count");
        var countInput = Console.ReadLine();

        if (int.TryParse(countInput, out var count))
        {
            Console.WriteLine("Copy multiple characters process start");
            await _copier.CopyMultipleAsync(count);
            Console.WriteLine("Copy multiple characters process end");
        }
        else
        {
            Console.WriteLine($"'{countInput}' is not a valid input");
        }

    }
}