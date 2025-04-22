namespace TestMobit.Domain.Configurations
{
    public class CryptographySettings
    {
        public string StringKey { get; set; } = string.Empty;
        public string ValueKey { get; set; } = string.Empty;
        public string HashAlgorithm { get; set; } = string.Empty;
        public int PasswordIterations { get; set; }
        public string VectorInit { get; set; } = string.Empty;
        public int KeySize { get; set; }
    }
}
