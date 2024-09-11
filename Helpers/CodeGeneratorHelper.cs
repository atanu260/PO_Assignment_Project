namespace PO_Assignment_Project.Helpers
{
    public class CodeGeneratorHelper
    {
        public static string GenerateCode(string prefix, int nextId)
        {
            // Generate a code in the format "PREFIX-001"
            return $"{prefix}-{nextId.ToString("D3")}";
        }
    }
}
