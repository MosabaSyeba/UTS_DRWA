using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtsPos.Data
{
    public interface ICategory
    {
        IEnumerable<Models.Category> GetCategories();
        Models.Category GetCategoryById(int categoryId);
        Models.Category AddCategory(Models.Category category);
        Models.Category UpdateCategory(Models.Category category);
        void DeleteCategory(int categoryId);
    }
}