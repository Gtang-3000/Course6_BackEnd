using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookMatain.Dao
{
    public interface ICodeDao
    {
        List<SelectListItem> GetBookClassData();
        List<SelectListItem> GetBookKeeperData();
        List<SelectListItem> GetBookStatus();
    }
}