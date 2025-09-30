namespace BTL_QuanLiBanSach.DTOs.Response;

public record PageResponse<T>(
    List<T> content,
    int size,
    int page,
    int totalElements,
    int totalPages
);