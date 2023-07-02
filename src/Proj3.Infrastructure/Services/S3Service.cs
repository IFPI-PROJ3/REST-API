using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Proj3.Infrastructure.Persistence.Utils;

namespace Proj3.Infrastructure.Services
{
    public static class S3Service
    {
        private static string accessKey = "AKIAZ6KS7EKEWT7D6DNU";
        private static string secretKey = "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";
        private static RegionEndpoint bucketRegion = RegionEndpoint.SAEast1;
        private static string bucketName = "wechange-images-bucket";

        private static AmazonS3Client getNewAmazonS3Client()
        {
            return new AmazonS3Client(accessKey, secretKey, bucketRegion);
        }

        public static async Task FileUploadAsync(byte[] imageBytes, string fileName)
        {
            var s3Client = getNewAmazonS3Client();            
            
            using (var stream = ImageUtils.ByteArrayToImage(imageBytes))
            {
                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    InputStream = stream,                    
                    ContentType = "image/jpg",
                    Key = fileName
                };

                await s3Client.PutObjectAsync(request);
            }
        }

        public static async Task DeleteFileAsync(string fileName)
        {
            var s3Client = getNewAmazonS3Client();

            var request = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = fileName
            };

            await s3Client.DeleteObjectAsync(request);
        }
    }
}
