using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMatain.Dao
{
    public class CodeDao
    {
        DataTable dt = new();
        SqlConnection conn = new(Common.ConfigTool.GetDBConnectionString());
        public List<SelectListItem> GetBookClassData()
        {
            string sql = "SELECT CONCAT(BOOK_CLASS_ID, '-',BOOK_CLASS_NAME) AS [書籍類別],BOOK_CLASS_ID FROM BOOK_CLASS ";

            
            using (conn)
            {
                SqlCommand cmd = new(sql, conn);
                SqlDataAdapter sqlDataAdapter = new(cmd);
                sqlDataAdapter.Fill(dt);
            }
            return MapCodeDataToList(dt, "書籍類別", "BOOK_CLASS_ID");
        }
        public List<SelectListItem> GetBookStatus()
        {
            string sql = "SELECT CONCAT(CODE_ID, '-',CODE_NAME) AS [狀態],CODE_ID  FROM BOOK_CODE  WHERE CODE_TYPE = 'BOOK_STATUS'";

            using(conn)
            {
                SqlCommand cmd = new(sql, conn);
                SqlDataAdapter sqlDataAdapter = new(cmd);
                sqlDataAdapter.Fill(dt);
            }
            return MapCodeDataToList(dt, "狀態", "CODE_ID");
        }
        public List<SelectListItem> GetBookKeeperData()
        {
            string sql = "SELECT DISTINCT CONCAT(USER_ENAME, '-' ,USER_CNAME) AS [借閱人] , USER_ID FROM MEMBER_M ORDER BY 借閱人";
            
            using (conn)
            {
                SqlCommand cmd = new(sql, conn);
                SqlDataAdapter sqlDataAdapter = new(cmd);
                sqlDataAdapter.Fill(dt);
            }
            return MapCodeDataToList(dt, "借閱人", "USER_ID");
        }

        private List<SelectListItem> MapCodeDataToList(DataTable dt, string text, string val)
        {
            var result = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new SelectListItem()
                {
                    Text = row[text].ToString(),
                    Value = row[val].ToString()
                });
            }
            return result;
        }
    }
}
