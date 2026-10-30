using FiyiStack.Areas.CMS.MenuBack.Entities;

namespace FiyiStack.Areas.CMS.MenuBack.DTOs
{
    public class FolderAndPagesForCMSDTO
    {
        public Menu Folder { get; set; } = new();
        public List<Menu> Pages { get; set; } = [];
    }
}
