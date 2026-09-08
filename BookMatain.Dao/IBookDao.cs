using BookMatain.Modle;

namespace BookMatain.Dao
{
    public interface IBookDao
    {
        bool CheckBookHaveKeeperAndStatus(BookDataForEdit bookData);
        bool DeleteBook(string BookID);
        List<BookData> FilterBook(BookSerchArg serchArg);
        BookDataForEdit GetBookData(string bookID);
        string InsertBook(BookDataForEdit bookData);
        void UpdateBook(BookDataForEdit bookData);
    }
}