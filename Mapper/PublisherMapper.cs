using BTL_QuanLiBanSach.DTOs.Response;
using BTL_QuanLiBanSach.Entities;

namespace BTL_QuanLiBanSach.Mapper;

public class PublisherMapper
{
    public static PublisherResponse ToPublisherResponse(Publisher publisher)
    {
        if (publisher != null) return new PublisherResponse(publisher.Id, publisher.Name);
        else return null;
    }

    public static List<PublisherResponse> ToPublisherResponses(List<Publisher> publishers)
    {
        return publishers
            .Select(ToPublisherResponse)
            .ToList();
    }
}