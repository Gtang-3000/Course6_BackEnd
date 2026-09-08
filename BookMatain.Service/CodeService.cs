using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMatain.Service
{
    public class CodeService
    {
        Dao.CodeDao codeDao = new();
        public List<SelectListItem> GetBookClassData()
        {            
            return codeDao.GetBookClassData();
        }
        public List<SelectListItem> GetBookStatus()
        {
            return codeDao.GetBookStatus();
        }
        public List<SelectListItem> GetBookKeeperData()
        {
            return codeDao.GetBookKeeperData();
        }
    }
}
