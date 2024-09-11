using PO_Assignment_Project.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PO_Assignment_Project.Models
{
    public class Vendor
    {

        private static int _codeCounter = 0;
        private string _code;

        Vendor()
        {
            Code = GenerateNewCode();
        }
        public long ID { get; set; }

        [StringLength(5)]
        public string Code
        {
            get
            {
                if (string.IsNullOrEmpty(_code))
                {
                    _code = GenerateNewCode();
                }
                return _code;
            }
            private set
            {
                _code = value;
            }
        }

        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(255)]
        public string AddressLine1 { get; set; }

        [StringLength(255)]
        public string AddressLine2 { get; set; }

        [StringLength(150)]
        [EmailAddress]
        public string ContactEmail { get; set; }

        [StringLength(10)]
        [Phone]
        public string ContactNo { get; set; }

        [DataType(DataType.Date)]
        [DateValidatorHelper(ErrorMessage = "The date must be today or in the future.")]
        public DateTime ValidTillDate { get; set; }

        public bool IsActive { get; set; }

        private string GenerateNewCode()
        {
            return $"VEN-{++_codeCounter:D4}";
        }
    }
}
