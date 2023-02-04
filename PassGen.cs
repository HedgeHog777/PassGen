using System;
using System.Linq;
using System.Security.Cryptography;

class PasswordGenerator {
    static void Main() {
        int passwordLength = 35;
        string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#$%&*@?";
        char[] chars = new char[passwordLength];
        byte[] data = new byte[passwordLength];

        using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider()) {
            crypto.GetBytes(data);
        }

        for (int i = 0; i < passwordLength; i++) {
            chars[i] = allowedChars[data[i] % allowedChars.Length];
        }
        
        string password = new string(chars);
        Console.WriteLine(password);
    }
}
