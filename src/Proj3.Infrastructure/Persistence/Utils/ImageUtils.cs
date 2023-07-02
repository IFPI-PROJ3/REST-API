namespace Proj3.Infrastructure.Persistence.Utils
{
    public static class ImageUtils
    {
        public static MemoryStream ByteArrayToImage(byte[] imageBytes)
        {
            return new MemoryStream(imageBytes);
        }   
    }
}
