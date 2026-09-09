using BookMatain.Modle;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookMatain.Dao
{
    public class BookDao : IBookDao
    {

        /// <summary>
        /// 按給出的條件查書
        /// </summary>
        /// <param name="serchArg"></param>
        /// <returns>List<BookData></returns>
        public List<BookData> FilterBook(BookSerchArg serchArg)
        {
            string sql = @"
                            SELECT 
                                    C.BOOK_CLASS_ID , C.BOOK_CLASS_NAME, D.BOOK_ID, D.BOOK_NAME
                                    , D.BOOK_KEEPER,CONVERT(varchar, D.BOOK_BOUGHT_DATE, 111) AS　BOUGHT_DATE
                                    , CO.CODE_NAME, M.USER_ENAME, D.BOOK_NOTE
                            FROM BOOK_DATA AS D
                            LEFT JOIN BOOK_CLASS AS C 
	                            ON D.BOOK_CLASS_ID = C.BOOK_CLASS_ID
                            LEFT JOIN BOOK_CODE AS CO
	                            ON D.BOOK_STATUS = CO.CODE_ID
	                            AND CO.CODE_TYPE = 'BOOK_STATUS'
                            LEFT JOIN MEMBER_M AS M
	                            ON D.BOOK_KEEPER = M.USER_ID	
                            WHERE (CODE_ID = @BookStatus OR @BookStatus = '')
                                AND (C.BOOK_CLASS_ID = @BookClass OR @BookClass = '')
                                AND (D.BOOK_KEEPER = @BookKeeper OR @BookKeeper = '' ) 
                                AND (D.BOOK_NAME LIKE ('%' +@BookName+ '%') or @BookName='')
                                AND ((@StartDate = '' ) OR (CONVERT(varchar(10), D.BOOK_BOUGHT_DATE, 111) >= @StartDate))
                            AND ((@EndDate = '' ) OR (CONVERT(varchar(10), D.BOOK_BOUGHT_DATE, 111) <= @EndDate))
                            ORDER BY BOUGHT_DATE DESC";

            SqlConnection conn = new(Common.ConfigTool.GetDBConnectionString());
            SqlCommand cmd = new(sql, conn);
            //string.Empty
            cmd.Parameters.AddWithValue("@BookName", serchArg.BookName ?? "");
            cmd.Parameters.AddWithValue("@BookClass", serchArg.BookClassId ?? "");
            cmd.Parameters.AddWithValue("@BookKeeper", serchArg.BookKeeper ?? "");
            cmd.Parameters.AddWithValue("@BookStatus", serchArg.BookStatus ?? "");

            if (serchArg.BookEndTime == "year/month/day")
                cmd.Parameters.AddWithValue("@EndDate", "");
            else
                cmd.Parameters.AddWithValue("@EndDate", serchArg.BookEndTime);
            if (serchArg.BookStartTime == "year/month/day")
                cmd.Parameters.AddWithValue("@StartDate", "");
            else
                cmd.Parameters.AddWithValue("@StartDate", serchArg.BookStartTime);



            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            return MapBookDataToList(dt);
        }
        /// <summary>
        /// 把DataTable變成List<BookData>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>List<BookData></returns>
        private List<BookData> MapBookDataToList(DataTable dt)
        {
            List<BookData> result = new();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new BookData()
                {
                    BookID = row["BOOK_ID"].ToString(),
                    BookName = row["BOOK_NAME"].ToString(),
                    BookClassId = row["BOOK_CLASS_ID"].ToString(),
                    BookClassName = row["BOOK_CLASS_NAME"].ToString(),
                    BookBoughtDate = row["BOUGHT_DATE"].ToString(),
                    BookKeeper = row["USER_ENAME"].ToString(),
                    BookStatus = row["CODE_NAME"].ToString(),
                    BookNote = row["BOOK_NOTE"].ToString(),
                });
            }
            return result;
        }
        /// <summary>
        /// 新增書
        /// </summary>
        /// <param name="bookData"></param>
        /// <returns></returns>
        public string InsertBook(BookDataForEdit bookData)
        {
            string sql = @"
                            INSERT INTO BOOK_DATA
                            (
                                BOOK_NAME, BOOK_CLASS_ID,
                                BOOK_AUTHOR, BOOK_BOUGHT_DATE
                                , BOOK_PUBLISHER, BOOK_NOTE,BOOK_STATUS, BOOK_KEEPER, 
                                CREATE_USER, CREATE_DATE, MODIFY_DATE, MODIFY_USER 
                            )
                            VALUES
                            (
                                @BookName, @BookClassID, @BookAuthor ,@BookBoughtDate
                                , @BookPublishier ,@BookNote , 'A' , '', 'admin' ,GETDATE()
                                , GETDATE(), 'admin'
                            )
                            SELECT SCOPE_IDENTITY()";
            string BookID;
            using (SqlConnection conn = new SqlConnection(Common.ConfigTool.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookName", bookData.BookName ?? ""));
                cmd.Parameters.Add(new SqlParameter("@BookAuthor", bookData.BookAuthor ?? ""));
                cmd.Parameters.Add(new SqlParameter("@BookPublishier", bookData.BookPublishier ?? ""));
                cmd.Parameters.Add(new SqlParameter("@BookNote", bookData.BookNote ?? ""));
                cmd.Parameters.Add(new SqlParameter("@BookBoughtDate", bookData.BookBoughtDate ?? ""));
                cmd.Parameters.Add(new SqlParameter("@BookClassID", bookData.BookClassID ?? ""));
                //cmd.ExecuteNonQuery();
                BookID = cmd.ExecuteScalar().ToString();
                conn.Close();
            }
            return BookID;
        }
        /// <summary>
        /// 修改資料庫資料
        /// </summary>
        /// <param name="bookData"></param>
        public void UpdateBook(BookDataForEdit bookData)
        {
            string sql = @"
                            UPDATE BOOK_DATA
                            SET    BOOK_NAME = @BookName , BOOK_AUTHOR = @BookAuthor, 
                                BOOK_PUBLISHER = @BookPublishier , BOOK_NOTE = @BookNote , 
                                BOOK_BOUGHT_DATE = @BookBoughtDate , BOOK_CLASS_ID = @BookClassID ,
                                BOOK_STATUS = @BookStatus , BOOK_KEEPER = @BookKeeper
                                MODIFY_DATE = GETDATE() , MODIFY_USER = 'admin'
                            WHERE  BOOK_ID = @BookID;
                            ";
            using (SqlConnection conn = new SqlConnection(Common.ConfigTool.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookID", bookData.BookID ?? ""));
                cmd.Parameters.Add(new SqlParameter("@BookName", bookData.BookName ?? ""));
                cmd.Parameters.Add(new SqlParameter("@BookAuthor", bookData.BookAuthor ?? ""));
                cmd.Parameters.Add(new SqlParameter("@BookPublishier", bookData.BookPublishier ?? ""));
                cmd.Parameters.Add(new SqlParameter("@BookNote", bookData.BookNote ?? ""));
                cmd.Parameters.Add(new SqlParameter("@BookBoughtDate", bookData.BookBoughtDate ?? ""));
                cmd.Parameters.Add(new SqlParameter("@BookClassID", bookData.BookClassID ?? ""));
                cmd.Parameters.Add(new SqlParameter("@BookStatus", bookData.Status ?? ""));
                if (bookData.Keeper == null)
                {
                    cmd.Parameters.Add(new SqlParameter("@BookKeeper", ""));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@BookKeeper", bookData.Keeper));
                }
                cmd.ExecuteNonQuery();//執行sql
            }
        }

        /// <summary>
        /// 查書BOOK_NAME, BOOK_AUTHOR, BOOK_PUBLISHER, BOOK_NOTE, BOUGHT_DATE,
        ///BOOK_CLASS_ID, BOOK_STATUS, BOOK_KEEPER,Keeper
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns>BookDataForEdit</returns>
        public BookDataForEdit GetBookData(string bookID)
        {
            string sql = @"SELECT D.BOOK_NAME, D.BOOK_AUTHOR, D.BOOK_PUBLISHER, D.BOOK_NOTE, 
                                CONVERT(varchar, D.BOOK_BOUGHT_DATE, 23) AS　BOUGHT_DATE,
                                D.BOOK_CLASS_ID, D.BOOK_STATUS, D.BOOK_KEEPER, CONCAT(M.USER_CNAME,
                                '(',M.USER_ENAME, ')') AS Keeper, CLASS.BOOK_CLASS_NAME, CODE.CODE_NAME  
                            FROM BOOK_DATA AS D 
                                LEFT JOIN MEMBER_M AS M ON D.BOOK_KEEPER = M.USER_ID 
                                INNER JOIN BOOK_CODE AS CODE ON CODE.CODE_ID = D.BOOK_STATUS
                                INNER JOIN BOOK_CLASS AS CLASS ON CLASS.BOOK_CLASS_ID = D.BOOK_CLASS_ID
                            WHERE D.BOOK_ID = @BookID";
            BookDataForEdit result = new BookDataForEdit();
            using (SqlConnection conn = new SqlConnection(Common.ConfigTool.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookID", bookID));
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                conn.Close();

                foreach (DataRow row in dt.Rows)
                {
                    result = new BookDataForEdit()
                    {
                        BookID = bookID,
                        BookName = row["BOOK_NAME"].ToString(),
                        BookAuthor = row["BOOK_AUTHOR"].ToString(),
                        BookPublishier = row["BOOK_PUBLISHER"].ToString(),
                        BookNote = row["BOOK_NOTE"].ToString(),
                        BookBoughtDate = row["BOUGHT_DATE"].ToString(),
                        BookClassID = row["BOOK_CLASS_ID"].ToString(),
                        Status = row["BOOK_STATUS"].ToString(),
                        Keeper = row["BOOK_KEEPER"].ToString(),
                        KeeperName = row["Keeper"].ToString(),
                        ClassName = row["BOOK_CLASS_NAME"].ToString(),
                        StatusName = row["CODE_NAME"].ToString(),
                        
                    };
                }
            }
            return result;
        }
        /// <summary>
        /// 如果書被借走就不刪 不然就刪
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns>True刪  False不刪</returns>
        public bool DeleteBook(string BookID)
        {
            var Status = GetBookData(BookID).Status;
            if ((Status == "B") || (Status == "C")) return false;

            string sql = @" 
                            DELETE BOOK_DATA
                            WHERE  BOOK_ID = @BookID;
                        ";
            SqlConnection conn = new SqlConnection(Common.ConfigTool.GetDBConnectionString());
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@BookID", BookID));
            cmd.ExecuteNonQuery();//執行sql
            conn.Close();

            return true;
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
            string Keeper = bookData.Keeper;
            string Status = bookData.Status;
            if ((((Status == "B") || (Status == "C")) && ((Keeper != "") && (Keeper != null)))) return true;
            if ((((Status == "A") || (Status == "U")) && ((Keeper == "") || (Keeper == null)))) return true;
            return false;
        }
    }
}
