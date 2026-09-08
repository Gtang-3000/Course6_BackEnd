using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMatain.Modle
{
    public class BookDataForEdit
    {
        public string? BookID { get; set; }

        [DisplayName("書名")]
        [StringLength(400, ErrorMessage = "書名太長了")]
        [Required(ErrorMessage = "此欄位必填")]
        public string BookName { get; set; }

        [DisplayName("作者")]
        [StringLength(60, ErrorMessage = "太長了")]
        [Required(ErrorMessage = "此欄位必填")]
        public string? BookAuthor { get; set; }

        [DisplayName("出版商")]
        [StringLength(40, ErrorMessage = "太長了")]
        [Required(ErrorMessage = "此欄位必填")]
        public string? BookPublishier { get; set; }

        [DisplayName("內容簡介")]
        [StringLength(1200, ErrorMessage = "太長了")]
        [Required(ErrorMessage = "此欄位必填")]
        public string? BookNote { get; set; }

        [DisplayName("購買日期")]
        [Required(ErrorMessage = "此欄位必填")]
        public string? BookBoughtDate { get; set; }

        [DisplayName("類別")]
        [Required(ErrorMessage = "此欄位必填")]
        public string? BookClassID { get; set; }

        [DisplayName("狀態")]
        [Required(ErrorMessage = "此欄位必填")]
        public string? Status { get; set; }
        public string? Keeper { get; set; }
    }
}
