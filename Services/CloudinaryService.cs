using BTL_QuanLiBanSach.Configuration;
using BTL_QuanLiBanSach.DTOs.Response;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BTL_QuanLiBanSach.Services;

public class CloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService()
    {
        Account account = new Account(
            "dwhyfa80s",
            "668643833117639",
            "W1EsHlyKdr2266oY5OgkAlUtu-o"
        );

        _cloudinary = new Cloudinary(account);
    }

    public async Task<ImageResponse> upload(IFormFile file)
    {
        if (file == null || file.Length == 0) return null;
        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, stream),
            UseFilename = true,
            UniqueFilename = true,
            Overwrite = false
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        return new ImageResponse(uploadResult.PublicId, uploadResult.SecureUrl.ToString());
    }

    public async Task<ImageResponse> update(string existedPublicId, IFormFile file)
    {
        if (file == null || file.Length == 0) return null;
        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, stream),
            PublicId = existedPublicId,
            Invalidate = true,
            Overwrite = false
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        return new ImageResponse(uploadResult.PublicId, uploadResult.SecureUrl.ToString());
    }
}