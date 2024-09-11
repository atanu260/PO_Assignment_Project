using PO_Assignment_Project.Data;
using PO_Assignment_Project.Helpers;
using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;

namespace PO_Assignment_Project.Models
{
    public class Material
    {
        private readonly ApplicationDbContext _context;

        private static int _codeCounter = 0;
        private string _code;


        //public Material(ApplicationDbContext context)
        //{
        //    _context = context;
        //    Code = GenerateMaterialCode();  // Auto-generate the code when a new Material is created
        //}

        public Material()
        {
            Code = GenerateNewCode();
        }
        public int ID { get; set; }

        [Required]
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

        [MaxLength(50)]
        public string ShortText { get; set; }

        public string LongText { get; set; }

        [Required]
        public string Unit { get; set; }

        [Range(0, int.MaxValue)]
        public int ReorderLevel { get; set; }

        [Range(0, int.MaxValue)]
        public int MinOrderQuantity { get; set; }

        public bool IsActive { get; set; }

        //private string GenerateMaterialCode()
        //{
            
        //        // Get the latest material ID
        //        var latestMaterial = _context.Materials.OrderByDescending(m => m.ID).FirstOrDefault();
        //        int nextId = (latestMaterial == null) ? 1 : latestMaterial.ID + 1;

        //        // Use the helper class to generate the code
        //        return CodeGeneratorHelper.GenerateCode("MAT", nextId);  // For example, "MAT-001"
            
        //}

        private string GenerateNewCode()
        {
            return $"MAT-{++_codeCounter:D4}";
        }
    }

}
