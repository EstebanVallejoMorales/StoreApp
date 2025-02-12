namespace StoreApp.Dto.GenericResponse
{
    public class ResponseDto<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public List<ErrorDto> Errors { get; set; }

        public ResponseDto()
        {
            Errors = new List<ErrorDto>();
        }
    }
}
