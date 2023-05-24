namespace davaleba_333.Models
{
    public class Accessory
    {
        public int ID { get; set; }
        public string AccessoryName { get; set; }
        public ICollection<PhoneAccessory> PhoneAccessory{ get; set; }
    }
}
