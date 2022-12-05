namespace MP.ApiDotNet6.Application.DTOs
{
    public class PagedBaseResponseDTO<T>
    {
        public PagedBaseResponseDTO(int totalRegister, List<T> data)
        {
            TotalRegister = totalRegister;
            Data = data;
        }

        public int TotalRegister { get; private set; }
        public List<T> Data { get; private set; }
    }

    
}
