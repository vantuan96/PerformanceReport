using System.Collections.Generic;
using Kztek_Library.Models;

namespace Kztek_Library.Helpers
{
    public class StaticList
    {
        /// <summary>
        /// Danh sách loại menu
        /// </summary>
        /// <returns>List<SelectListModel></returns>
        public static List<SelectListModel> MenuType()
        {
            var list = new List<SelectListModel> {
                                        new SelectListModel { ItemValue = "1", ItemText = "Menu"},
                                        new SelectListModel { ItemValue = "2", ItemText = "Function"}
                                    };
            return list;
        }
    }
}