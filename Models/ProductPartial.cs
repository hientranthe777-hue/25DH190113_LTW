using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace _25DH190113_LTW.Models
{
    public partial class Product
    {
        [NotMapped]
        [Display(Name = "Chọn ảnh nhạc cụ")]
        public HttpPostedFileBase UploadImg { get; set; }
    }
}