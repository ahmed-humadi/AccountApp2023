using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLib;
using DataEntitiesLib;

namespace ModelsLib
{
    public class CategoryTableModel
    {
        private CategoryBL categoryBL;

        public CategoryTableModel()
        {
            categoryBL = new CategoryBL();
        }
        public CategoryTable GetCategories()
        {
            return categoryBL.GetCategories();
        }
        public void UpdateCategoryTable(CategoryTable categoryTable)
        {
            categoryBL.UpdateCategoryTable(categoryTable);
        }
    }
}
