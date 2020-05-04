using ImagePost.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImagePost.Models
{
    public class EnlargedViewModel
    {
        public Image Image { get; set; }
        public bool CanLike { get; set; }
    }
}
