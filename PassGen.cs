using System;
using System.Collections.Generic;
using System.Security.Cryptography;

internal static class PasswordGenerator
{
    private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
    private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Digits = "0123456789";
    private const string Symbols = "!#$%&*@?";

    public static string Generate(
        int length = 35,
        bool useLowercase = true,
        bool useUppercase = true,
        bool useDigits = true,
        bool useSymbols = true)
    {
        if (length <= 0)
            throw new ArgumentOutOfRangeException(nameof(length));

        var characterSets = new List<string>();

        if (useLowercase) characterSets.Add(Lowercase);
        if (useUppercase) characterSets.Add(Uppercase);
        if (useDigits) characterSets.Add(Digits);
        if (useSymbols) characterSets.Add(Symbols);

        if (characterSets.Count == 0)
            throw new InvalidOperationException("At least one character set must be selected.");

        if (length < characterSets.Count)
            throw new ArgumentException(
                "Password length is too short for the selected character sets.");

        var password = new List<char>(length);

        // Гарантуємо хоча б один символ із кожної категорії
        foreach (string set in characterSets)
        {
            password.Add(set[RandomNumberGenerator.GetInt32(set.Length)]);
        }

        // Загальний набір символів
        string allCharacters = string.Concat(characterSets);

        while (password.Count < length)
        {
            password.Add(allCharacters[
                RandomNumberGenerator.GetInt32(allCharacters.Length)]);
        }

        // Перемішуємо (Fisher-Yates)
        for (int i = password.Count - 1; i > 0; i--)
        {
            int j = RandomNumberGenerator.GetInt32(i + 1);
            (password[i], password[j]) = (password[j], password[i]);
        }

        return new string(password.ToArray());
    }
}

internal static class Program
{
    static void Main()
    {
        string password = PasswordGenerator.Generate(
            length: 32,
            useSymbols: true);

        Console.WriteLine(password);
    }
}
