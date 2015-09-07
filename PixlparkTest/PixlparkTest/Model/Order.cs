using System;
using System.Windows.Media.Imaging;

namespace PixlparkTest.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TrackingUrl { get; set; }
        public int? TrackingNumber { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public Address DeliveryAddress { get; set; }
        public Shipping Shipping { get; set; }
        public float Price { get; set; }
        public string PreviewImageUrl { get; set; }
        public float DeliveryPrice { get; set; }
        public float TotalPrice { get; set; }
        private BitmapImage _previewImage;
        public BitmapImage PreviewImage
        {
            get
            {
                if (_previewImage != null) return _previewImage;
                if(PreviewImageUrl == null) return new BitmapImage();
                _previewImage = new BitmapImage(new Uri(PreviewImageUrl));
                return _previewImage;
            }
        }
    }
}
