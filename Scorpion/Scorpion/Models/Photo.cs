using System.IO;
using Xamarin.Forms;

namespace Scorpion.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public byte[] PhotoBlob { get; set; }
        public ImageSource PhotoBlobS => ImageSource.FromStream(() => new MemoryStream(PhotoBlob));
        public int ArticleId { get; set; }
    }
}