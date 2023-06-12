namespace davaleba_333.Models
{
    public class PhoneViewModel
    {
        public List<Phone> Phones { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Search { get; set; }
        public string SortOrder { get; set; }
    }
}
