namespace davaleba_333.Models
{
    public class Phone
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public ICollection<PhoneAccessory> PhoneAccessories { get; set; }
    }
}
