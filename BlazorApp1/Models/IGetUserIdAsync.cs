namespace BlazorApp1.Models
{
    public interface IGetUserIdAsync
    {
        public Task<int?> GetUserIdAsync();
    }
}
