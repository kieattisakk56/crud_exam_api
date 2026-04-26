namespace ProjectApi.Application.Interfaces.Services;

public interface IEncryptionService
{
    string Encrypt(string plainText);
    string Decrypt(string cipherText);
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
}

public interface IJwtService
{
    string GenerateToken(int userId, string username, string role);
}

public interface IQrCodeService
{
    byte[] GenerateQrCode(string text);
}

