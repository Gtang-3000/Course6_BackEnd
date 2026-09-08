using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMatain.Service
{
    public class CodeService : ICodeService
    {
        private readonly BookMatain.Dao.ICodeDao  _codeDao;

        public CodeService(BookMatain.Dao.ICodeDao codeDao)
        {
            _codeDao = codeDao;
        }

        public List<SelectListItem> GetBookClassData()
        {
            return _codeDao.GetBookClassData();
        }
        public List<SelectListItem> GetBookStatus()
        {
            return _codeDao.GetBookStatus();
        }
        public List<SelectListItem> GetBookKeeperData()
        {
            return _codeDao.GetBookKeeperData();
        }
    }
}
