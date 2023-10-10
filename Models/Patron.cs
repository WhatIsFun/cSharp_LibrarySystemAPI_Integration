using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace cSharp_LibrarySystemAPI_Integration.Model
{
    public class Patron
    {
        [Key]
        [JsonIgnore]
        public int PatronId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNum { get; set; }
        public int Age { get; set; }
        [JsonIgnore]
        public List<BorrowingTransaction> BorrowingTransactions { get; set; }
    }
}
