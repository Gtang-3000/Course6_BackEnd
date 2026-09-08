using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookMatain.Service
{
    public interface ICodeService
    {
        List<SelectListItem> GetBookClassData();
        List<SelectListItem> GetBookKeeperData();
        List<SelectListItem> GetBookStatus();
    }
}