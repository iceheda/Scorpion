using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Scorpion.Models
{
    public class ArticleWithPhoto
    {
        public Article Article { get; set; }
        public List<Photo> Photos { get; set; }

        public Photo Photo =>  Photos.FirstOrDefault();
    }
}
