using System.ComponentModel.DataAnnotations;

namespace davaleba_333.ViewModels
{
    public class UserFormViewModel
    {
       
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
