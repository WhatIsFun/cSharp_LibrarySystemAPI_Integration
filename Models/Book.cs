using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace cSharp_LibrarySystemAPI_Integration
{
    public class Book
    {
        [Key]
        [Newtonsoft.Json.JsonIgnore]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int PublicationYear { get; set; }
        [Required]
        [Newtonsoft.Json.JsonIgnore]
        public bool IsAvailable { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public List<BorrowingTransaction> BorrowingTransactions { get; set; }
    }
}
