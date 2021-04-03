using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YumApi.Models
{
    public class Category
    {
        public Category()
        {
            this.Recipe = new HashSet<Recipe>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Recipe> Recipe { get; set; }


    }
}
