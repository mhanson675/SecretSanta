using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecretSanta.Models

{
    public class Participant
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [MinLength(2), MaxLength(2)]
        public string State { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        public string GetAddress()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(AddressLine1);
            if (AddressLine2 != null)
            {
                builder.Append(" ");
                builder.Append(AddressLine2);
            }

            builder.Append(" ");
            builder.Append(City);
            builder.Append(", ");
            builder.Append(State);
            builder.Append(" ");
            builder.Append(ZipCode);

            return builder.ToString();
        }
    }
}
