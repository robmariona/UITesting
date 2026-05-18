namespace UITesting.Models
{

    [Collection("Sequential Tests")]
    public class ClaimModel
    {
        public string Description { get; set; } = string.Empty;
        public string Policy { get; set; } = string.Empty;
    }
}
