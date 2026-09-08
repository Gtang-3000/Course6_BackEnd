using BookMatain.Modle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMatain.Service
{
    public class BookService
    {
        Dao.BookDao bookDao = new();
        /// <summary>
        /// 按給出的條件查書
        /// </summary>
        /// <param name="serchArg"></param>
        /// <returns>List<BookData></returns>
        public List<BookData> FilterBook(BookSerchArg serchArg)
        {
           return bookDao.FilterBook(serchArg);
        }
        /// <summary>
        /// 把DataTable變成List<BookData>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>List<BookData></returns>
        public string InsertBook(BookDataForEdit bookData)
        {
            return bookDao.InsertBook(bookData);
        }
        /// <summary>
        /// 修改資料庫資料
        /// </summary>
        /// <param name="bookData"></param>
        public void UpdateBook(BookDataForEdit bookData)
        {
            bookDao.UpdateBook(bookData);
        }

        /// <summary>
        /// 查書BOOK_NAME, BOOK_AUTHOR, BOOK_PUBLISHER, BOOK_NOTE, BOUGHT_DATE,
        ///BOOK_CLASS_ID, BOOK_STATUS, BOOK_KEEPER,Keeper
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns>BookDataForEdit</returns>
        public BookDataForEdit GetBookData(string bookID)
        {
            return bookDao.GetBookData(bookID);
        }
        /// <summary>
        /// 如果書被借走就不刪 不然就刪
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns>True刪  False不刪</returns>
        public bool DeleteBook(string BookID)
        {
            return bookDao.DeleteBook(BookID);
        }
        /// <summary>
        /// 已借出B 或 已借出(未領)C 要有借閱人
        /// 可以借出A 或 不可借出U 不能有借閱人
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns>
        /// {已借出B 或 已借出(未領)C 且有借閱人
        /// 或((可以借出A 或 不可借出U) 且 沒有借閱人)} return true
        /// 其他 true
        /// </returns>
        public bool CheckBookHaveKeeperAndStatus(BookDataForEdit bookData)
        {
            return bookDao.CheckBookHaveKeeperAndStatus(bookData);
        }
    }
}
