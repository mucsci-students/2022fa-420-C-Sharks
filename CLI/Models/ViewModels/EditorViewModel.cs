using System.ComponentModel.DataAnnotations;

namespace CLI.Models.ViewModels
{
    public class EditorViewModel
    {
        [Required]
        public string mySavedModel { set; get; }
    }
}
