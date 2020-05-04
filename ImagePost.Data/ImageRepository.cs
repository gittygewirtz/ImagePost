using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImagePost.Data
{
    public class ImageRepository
    {
        private string _conStr;
        public ImageRepository(string conStr)
        {
            _conStr = conStr;
        }

        public void AddImage(Image image)
        {
            using(var context = new ImageContext(_conStr))
            {
                context.Images.Add(image);
                context.SaveChanges();
            }
        }

        public List<Image> GetImages()
        {
            using (var context = new ImageContext(_conStr))
            {
                return context.Images.OrderByDescending(i => i.DatePosted).ToList();
            }
        }

        public void AddLike(int imgId)
        {
            using (var context = new ImageContext(_conStr))
            {
                var img = context.Images.FirstOrDefault(i => i.Id == imgId);
                img.Likes++;
                context.SaveChanges();
            }
        }

        public Image GetById(int imgId)
        {
            using (var context = new ImageContext(_conStr))
            {
                return context.Images.FirstOrDefault(i => i.Id == imgId);
            }                
        }

        public int GetLikes(int imgId)
        {
            using (var context = new ImageContext(_conStr))
            {
                return context.Images.Where(i => i.Id == imgId).Select(i => i.Likes).FirstOrDefault();
            }
        }
    }
}
