namespace BTL_QuanLiBanSach.DTOs.Response;

public record Page<T>(T content, int size, int page, int totalElements, int totalPages);
