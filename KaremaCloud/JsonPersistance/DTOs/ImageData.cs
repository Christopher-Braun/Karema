using System;
using System.Drawing;

namespace KaReMa.Interfaces
{
    public class ImageData
    {
        public ImageData()
        {

        }

        public ImageData(Bitmap image)
        {
            this.Image = image;
        }

        public void SetImage(Bitmap image)
        {
            this.Image = image;
        }

        public void DeleteImage()
        {
            this.Image = null;
        }

        public Bitmap Image
        {
            get;
            private set;
        }

        public Boolean HasImage
        {
            get
            {
                return this.Image != null;
            }
        }
    }
}
