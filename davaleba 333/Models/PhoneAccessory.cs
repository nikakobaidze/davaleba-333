namespace davaleba_333.Models
{
    public class PhoneAccessory
    {
        public int ID { get; set; }
        public Phone Phone { get; set; }
        public int PhoneID { get; set; }
        public Accessory Accessory { get; set; }
        public int AccessoryID { get; set; }
    }
}
