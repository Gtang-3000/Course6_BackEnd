using BookMatain.Models;
using BookMatain.Modle;
using BookMatain.Service;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookMatain.Controllers
{
    public class BookController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetBookBySerch(BookSerchArg bookSerchArg)
        {
            BookService bookService = new();
            var Result = bookService.FilterBook(bookSerchArg);
            return Json(Result);
        }
        [HttpPost]
        public IActionResult GetBookDataByID(string BookID)
        {
            BookService bookService = new();
            var Result = bookService.GetBookData(BookID);

            return Json(Result);
        }
        [HttpPost]
        public IActionResult GetBookClassSelectData()
        {
            CodeService codeService = new();
            var Result = codeService.GetBookClassData();
            return Json(Result);
        }
        public IActionResult GetBookStatusSelectData()
        {
            CodeService codeService = new();
            var Result = codeService.GetBookStatus();
            return Json(Result);
        }
        public IActionResult GetBookKeeperSelectData()
        {
            CodeService codeService = new();
            var Result = codeService.GetBookKeeperData();
            return Json(Result);
        }

        [HttpPost]
        public IActionResult Delete(string BookID)
        {
            BookService bookService = new();
            return Json(bookService.DeleteBook(BookID));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BookDataForEdit bookData)
        {
            BookService bookService = new();
            ModelState.Remove("Status");
            ModelState.Remove("BookID");
            ModelState.Remove("Keeper");
            if (ModelState.IsValid)
            {
                return Json(bookService.InsertBook(bookData));
            }
            return Json("");
        }
        [HttpGet]
        public IActionResult Update(string BookID)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Update(BookDataForEdit bookData)
        {
            BookService bookService = new();
            if (ModelState.IsValid)
            {
                if (bookService.CheckBookHaveKeeperAndStatus(bookData))
                {
                    bookService.UpdateBook(bookData);
                    return Json("儲存了");
                }
                else
                {
                    return Json("沒存成功\r\n已借出 或 已借出(未領) 要有借閱人\r\n可以借出 或 不可借出 不能有借閱人");
                }
            }
            return Json("沒存成功\r\n資料填寫不完全");
        }
        [HttpGet]
        public IActionResult Detail(string BookID)
        {
            BookService bookService = new();
            var Book = bookService.GetBookData(BookID);
            return View(Book);
        }
    }
}
