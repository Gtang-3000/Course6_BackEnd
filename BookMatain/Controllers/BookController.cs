using BookMatain.Models;
using BookMatain.Modle;
using BookMatain.Service;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookMatain.Controllers
{
    public class BookController : Controller
    {
        private readonly BookMatain.Service.IBookService _bookService;
        private readonly BookMatain.Service.ICodeService _codeService;

        public BookController(BookMatain.Service.IBookService bookService , BookMatain.Service.ICodeService codeService)
        {
            _bookService = bookService;
            _codeService = codeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetBookBySerch(BookSerchArg bookSerchArg)
        {
            try {
                var Result = _bookService.FilterBook(bookSerchArg);
            return Json(Result);
            }
            catch (Exception ex)
            {
                BookMatain.Common.Logger.Write(ex.Message, BookMatain.Common.Logger.LogCategoryEnum.Error);
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult GetBookDataByID(string BookID)
        {
            try
            {
                var Result = _bookService.GetBookData(BookID);
                return Json(Result);
            }
            catch (Exception ex)
            {
                BookMatain.Common.Logger.Write(ex.Message, BookMatain.Common.Logger.LogCategoryEnum.Error);
                return View("Error");
            }
            
        }
        [HttpPost]
        public IActionResult GetBookClassSelectData()
        {
            try
            {
                var Result = _codeService.GetBookClassData();
                return Json(Result);
            }
            catch (Exception ex)
            {
                BookMatain.Common.Logger.Write(ex.Message, BookMatain.Common.Logger.LogCategoryEnum.Error);
                return View("Error");
            }
        }
        public IActionResult GetBookStatusSelectData()
        {
            try
            {
                var Result = _codeService.GetBookStatus();
                return Json(Result);
            }
            catch (Exception ex)
            {
                BookMatain.Common.Logger.Write(ex.Message, BookMatain.Common.Logger.LogCategoryEnum.Error);
                return View("Error");
            }   
        }
         
        public IActionResult GetBookKeeperSelectData()
        {
            try
            {
                var Result = _codeService.GetBookKeeperData();
                return Json(Result);
            }
            catch (Exception ex)
            {
                BookMatain.Common.Logger.Write(ex.Message, BookMatain.Common.Logger.LogCategoryEnum.Error);
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult Delete(string BookID)
        {
            try
            {
                var Result = _bookService.DeleteBook(BookID);
                return Json(Result);
            }
            catch (Exception ex)
            {
                BookMatain.Common.Logger.Write(ex.Message, BookMatain.Common.Logger.LogCategoryEnum.Error);
                return View("Error");
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            try { 
                return View();
            }
            catch (Exception ex)
            {
                BookMatain.Common.Logger.Write(ex.Message, BookMatain.Common.Logger.LogCategoryEnum.Error);
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult Create(BookDataForEdit bookData)
        {
            try
            {
                ModelState.Remove("Status");
                ModelState.Remove("BookID");
                ModelState.Remove("Keeper");
                if (ModelState.IsValid)
                {
                    return Json(_bookService.InsertBook(bookData));
                }
                return Json("");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpGet]
        public IActionResult Update(string BookID)
        {
            try { 
                return View();
            }
            catch (Exception ex)
            {
                BookMatain.Common.Logger.Write(ex.Message, BookMatain.Common.Logger.LogCategoryEnum.Error);
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult Update(BookDataForEdit bookData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_bookService.CheckBookHaveKeeperAndStatus(bookData))
                    {
                        _bookService.UpdateBook(bookData);
                        return Json("儲存了");
                    }
                    else
                    {
                        return Json("沒存成功\n資料沒填完");
                    }
                }
                else
                {
                    return Json("沒存成功\r\n已借出 或 已借出(未領) 要有借閱人\r\n可以借出 或 不可借出 不能有借閱人");
                }
            }
            catch (Exception ex)
            {
                BookMatain.Common.Logger.Write(ex.Message, BookMatain.Common.Logger.LogCategoryEnum.Error);
                return View("Error");
            }
        }
        [HttpGet]
        public IActionResult Detail(string BookID)
        {
            try
            {
                var Book = _bookService.GetBookData(BookID);
                return View(Book);
            }
            catch (Exception ex)
            {
                BookMatain.Common.Logger.Write(ex.Message, BookMatain.Common.Logger.LogCategoryEnum.Error);
                return View("Error");
            }
        }
    }
}
