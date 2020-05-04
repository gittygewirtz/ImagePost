using System;

namespace ImagePost.Data
{
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DatePosted { get; set; }
        public int Likes { get; set; }
    }
}
