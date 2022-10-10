using System.ComponentModel.DataAnnotations;

namespace UML.Models.ViewModels
{
    public class EditorViewModel
    {
        [Required]
        public string mySavedModel { set; get; }
    }
}
