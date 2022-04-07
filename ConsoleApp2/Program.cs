using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ResizeImage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ax ra dar masir proje qarar dade v naam an ra be hamrah format vared konid.! \n(exp : image.png)");
            string name = Console.ReadLine();
            Console.WriteLine("width :");
            int width = int.Parse(Console.ReadLine());
            Console.WriteLine("height :");
            int height = int.Parse(Console.ReadLine());

            //تصویر اصلی
            Bitmap img = new Bitmap(System.Environment.CurrentDirectory + @"\" + name);
            //فراخوانی متدی که تصویر را تغییر اندازه میدهد
            var resizedImg = ResizeImage(img, width, height);
            //ذخیر کردن تصویر ساخته شده
            resizedImg.Save(Environment.CurrentDirectory + @"\resizedImage.png");
        }

        //  متدی که قرار است تصویر را تغییر اندازه دهد
        //  بعنوان ورودی یک عکس و طول و عرض کورد نظر را گرفته
        //  عکس را به طول و غرض مورد نظر تغییر میدهد
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            //ایجاد یک مستطیل
            var destRect = new Rectangle(0, 0, width, height);
            //ایجاد شیء عکس جدید
            var destImage = new Bitmap(width, height);

            //DPI را بدون توجه به اندازه فیزیکی حفظ می کند
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                //تعیین می کند که آیا پیکسل های یک تصویر منبع بازنویسی می شوند یا با پیکسل های پس زمینه ترکیب می شوند.
                graphics.CompositingMode = CompositingMode.SourceCopy;
                //سطح کیفیت رندر تصاویر لایه ای را تعیین می کند.
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                // تعیین می کند که چگونه مقادیر میانی بین دو نقطه پایانی محاسبه می شود
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                // مشخص می کند که آیا خطوط، منحنی ها و لبه های نواحی پر شده از صاف کردن استفاده می کنند یا خیر
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                // بر کیفیت رندر هنگام ترسیم تصویر جدید تأثیر می گذارد
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    //از ایجاد سایه در اطراف حاشیه های تصویر جلوگیری می کند
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    //رسم تصویر ساخته شده
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

    }
}
