using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Garten.Model
{
   public  class Post : BindableBase
    {

        public string image { get; set; }

        public List<string> Images { get; set; }
        public string Titel { get; set; }
        public string Location { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string PostId { get; set; }

        public string Time { get; set; }
        public MyUser User { get; set; }
    }
}
