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

            if (_size == width / 3) return;

            _size = width / 3;

            var gridAmount = 0;
            var cnt = 0;

            foreach (var ch in Children)
            {
                SetBounds(ch, cnt, ref gridAmount);
                cnt++;
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
            
            SetBounds(img, _imageAmount, ref _gridAmount);
            Children.Add(img);
            _imageAmount++;
        }

        private void SetBounds(BindableObject view, int imageAmount, ref int gridAmount)
        {
            double x, y;
            double width, height;

            var mod = imageAmount % 9;

            x = mod switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 0,
                4 => 1,
                5 => 0,
                6 => 0,
                7 => 1,
                8 => 2,
                _ => throw new ArgumentOutOfRangeException()
            };

            y = mod switch
            {
                0 => 0,
                1 => 0,
                2 => 0,
                3 => 1,
                4 => 1,
                5 => 2,
                6 => 3,
                7 => 3,
                8 => 3,
                _ => throw new ArgumentOutOfRangeException()
            };

            width = height = mod switch
            {
                4 => 2,
                _ => 1
            };

            y += gridAmount * 4;

            x *= _size;
            y *= _size;
            width *= _size;
            height *= _size;

            if (mod == 8) gridAmount++;

            SetLayoutBounds(view, new Rectangle(x, y, width, height));
        }
    }
}