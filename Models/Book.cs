using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace cSharp_LibrarySystemAPI_Integration.Model
{
    public class Book
    {
        [JsonIgnore]
        [Key]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int PublicationYear { get; set; }
        [Required]
        [JsonIgnore]
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public List<BorrowingTransaction> BorrowingTransactions { get; set; }
    }
}
