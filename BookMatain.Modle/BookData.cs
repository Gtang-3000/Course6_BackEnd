using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMatain.Modle
{
    public class BookData
    {
        public string? BookID { get; set; }
        [DisplayName("書名")]
        public string? BookName { get; set; }
        [DisplayName("類別id")]
        public string? BookClassId { get; set; }
        [DisplayName("圖書類別")]
        public string? BookClassName { get; set; }
        [DisplayName("借閱人")]
        public string? BookKeeper { get; set; }
        [DisplayName("書籍狀態")]
        public string? BookStatus { get; set; }
        [DisplayName("購買日期")]
        public string? BookBoughtDate { get; set; }

        public string? BookNote { get; set; }   
    }
}
