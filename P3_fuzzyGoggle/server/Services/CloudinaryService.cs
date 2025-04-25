using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using WebApplication1.Interfaces;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService()
    {
        // Load the .env file
        try
        {
            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to load .env file.", ex);
        }


        // connect to cloudinary 

        var cloudinaryUrl = Environment.GetEnvironmentVariable("CLOUDINARY_URL");

        if (string.IsNullOrEmpty(cloudinaryUrl))
        {
            throw new InvalidOperationException("CLOUDINARY_URL environment variable is not set.");
        }

        // Initialize Cloudinary instance
        _cloudinary = new Cloudinary(cloudinaryUrl) { Api = { Secure = true } };
    }

    public async Task<string> UploadImageAsync(IFormFile image)
    {
        if (image == null || image.Length == 0)
        {
            throw new ArgumentException("File is invalid.");
        }

        using var stream = image.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(image.FileName, stream),
            UseFilename = true,
            UniqueFilename = false,
            Overwrite = true,
            Folder = "Fuzzy Goggles"
            // Transformation = new Transformation().Width(150).Height(150).Crop("fill")
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error != null)
        {
            throw new InvalidOperationException($"Cloudinary upload failed: {uploadResult.Error.Message}");
        }

        return uploadResult.SecureUrl.ToString();
    }



    public async Task<string> UploadVideoAsync(IFormFile video)
    {
        if (video == null || video.Length == 0)
        {
            throw new ArgumentException("Video file is invalid.");
        }

        var allowedVideoTypes = new[] { "video/mp4", "video/avi", "video/webm", "video/quicktime" };
        if (!allowedVideoTypes.Contains(video.ContentType))
        {
            throw new ArgumentException("Unsupported video format.");
        }

        using var stream = video.OpenReadStream();

        var uploadParams = new VideoUploadParams
        {
            File = new FileDescription(video.FileName, stream),
            UseFilename = true,
            UniqueFilename = false,
            Overwrite = true,
            Folder = "Fuzzy Goggles"
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.Error != null)
        {
            throw new InvalidOperationException($"Cloudinary video upload failed: {uploadResult.Error.Message}");
        }

        return uploadResult.SecureUrl.ToString();
    }

}



