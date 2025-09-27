using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Entities;

namespace BTL_QuanLiBanSach.Services;

public class AuthorMapper
{
    public static AuthorResponse ToAuthorResponse(Author author)
    {
        return new AuthorResponse(author.Id, author.Name);
    }

    public static List<AuthorResponse> ToAuthorResponses(List<Author> authors)
    {
        return authors.Select(ToAuthorResponse).ToList();
    }
}