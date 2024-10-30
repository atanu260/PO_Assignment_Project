namespace PO_Assignment_Project.Models
{
    public class Contractor
    {
        private string _code;
        private static int _counter = 0;
        public int Id { get; set; }

        public string Code
        {
            get
            {
                if (string.IsNullOrEmpty(_code))
                {
                    _code =GenerateNewCode();
                }
                return _code;
            }
            set
            {
                _code=value;
            }
        }
        public string Name {  get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }

        public Site? Site { get; set; }

        private string GenerateNewCode()
        {
            return $"CTR - {++_counter:C4}";
        }
    }
}
