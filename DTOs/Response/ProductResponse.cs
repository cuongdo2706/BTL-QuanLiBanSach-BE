namespace BTL_QuanLiBanSach.DTOs.Response;

public record ProductResponse(
    long id,
    string code,
    string description,
    string imgUrl,
    bool isDeleted,
    bool isActive,
    string name,
    int? numOfPages,
    decimal price,
    string publicId,
    int? publishedYear,
    int quantity,
    string translator,
    PublisherResponse? publisher,
    List<AuthorResponse>? authors,
    List<CategoryResponse>? categories);