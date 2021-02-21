using System;
using Xamarin.Forms;

namespace MSP_Lab.Views
{
    class GalleryGridLayout : AbsoluteLayout
    {
        private int _imageAmount = 0;
        private int _gridAmount = 0;

        private const int margin = 5;
        private double _size;

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            _size = width / 3;
            foreach (var ch in Children)
            {
                SetBounds(ch);
            }
        }

        public void Add(ImageSource source)
        {
            var img = new Image
            {
                Source = source,
                BackgroundColor = Color.LightGray,
                Margin = margin
            };
            
            SetBounds(img);
            Children.Add(img);
            _imageAmount++;
        }

        private void SetBounds(BindableObject view)
        {
            double x, y;
            double width = _size, height = _size;

            var mod = _imageAmount % 9;

            switch (mod)
            {
                case 0:
                case 1:
                case 2:
                    x = mod * _size;
                    y = 0;
                    break;
                case 3:
                    x = 0;
                    y = _size;
                    break;
                case 4:
                    x = y = _size;
                    width = height = _size * 2;
                    break;
                case 5:
                    x = 0;
                    y = _size * 2;
                    break;
                case 8:
                    _gridAmount++;
                    goto case 7;
                case 6:
                case 7:
                    x = (mod - 6) * _size;
                    y = _size * 3;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            y += _gridAmount * 4 * _size;

            SetLayoutBounds(view, new Rectangle(x, y, width, height));
        }
    }
}