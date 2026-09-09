using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMatain.Dao
{
    public class MockCodeDao : ICodeDao
    {
        private string rootCodeDataFilePath = @"C:\Users\a9034\Desktop\Course6_BackEnd\BookMatain\Data\";

        public List<SelectListItem> GetBookClassData()
        {
            string bookClassFilePath = rootCodeDataFilePath + "BOOK_CLASS.txt";

            var lines = File.ReadAllLines(bookClassFilePath);
            List<SelectListItem> result = new List<SelectListItem>();
            string splitChar = "\t";

            foreach (var item in lines)
            {
                result.Add(new SelectListItem()
                {
                    Text = item.Split(splitChar.ToCharArray())[1],
                    Value = item.Split(splitChar.ToCharArray())[0]
                });
            }
            return result;
        }


        public List<SelectListItem> GetBookKeeperData()
        {
            string bookClassFilePath = rootCodeDataFilePath + "MEMBER_M.txt";

            var lines = File.ReadAllLines(bookClassFilePath);
            List<SelectListItem> result = new List<SelectListItem>();
            string splitChar = "\t";

            foreach (var item in lines)
            {
                result.Add(new SelectListItem()
                {
                    Text = item.Split(splitChar.ToCharArray())[1] + "-" + item.Split(splitChar.ToCharArray())[2]  ,
                    Value = item.Split(splitChar.ToCharArray())[0]
                });
            }
            return result;
        }
        public List<SelectListItem> GetBookStatus()
        {
            string bookClassFilePath = rootCodeDataFilePath + "BOOK_CODE.txt";
            var lines = File.ReadAllLines(bookClassFilePath);
            List<SelectListItem> result = new List<SelectListItem>();
            string splitChar = "\t";
            foreach (var item in lines)
            {
                if (item.Split(splitChar.ToCharArray())[0] == "BOOK_STATUS")
                {
                    result.Add(new SelectListItem()
                    {
                        Text = item.Split(splitChar.ToCharArray())[3],
                        Value = item.Split(splitChar.ToCharArray())[1]
                    });
                }
            }
            return result;

        }
    }
}
