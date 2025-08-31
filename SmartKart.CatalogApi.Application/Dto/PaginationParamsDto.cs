namespace SmartKart.CatalogApi.Application.Dto
{
    public class PaginationParamsDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public PaginationParamsDto() { }

        public PaginationParamsDto(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize <= 0 ? 10 : pageSize;
        }
    }
}
