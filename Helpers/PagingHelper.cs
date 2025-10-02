using BTL_QuanLiBanSach.DTOs.Request;
using BTL_QuanLiBanSach.DTOs.Response;
using Microsoft.EntityFrameworkCore;

namespace BTL_QuanLiBanSach.Helpers
{
    public static class PagingHelper
    {
        public static async Task<PageResponse<T>> ToPagedResponseAsync<T>(
            this IQueryable<T> query,
            PagingRequest request)
        {
            var totalElements = await query.CountAsync();

            var items = await query
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalElements / request.Size);

            return new PageResponse<T>(
                items,
                request.Size,
                request.Page,
                totalElements,
                totalPages
            );
        }
    }

}
