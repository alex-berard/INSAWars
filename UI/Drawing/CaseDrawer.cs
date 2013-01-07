using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.Drawing
{
    public class CaseDrawer
    {
        private const int ItemWidth = 48;
        private const int ItemHeight = 20;

        private int _itemCount;
        private DrawingContext _context;
        private Tuple<int, int> _origin;

        public CaseDrawer(DrawingContext context, Tuple<int, int> origin)
        {
            _itemCount = 0;
            _context = context;
            _origin = origin;
        }

        public void Draw(BitmapImage icon, FormattedText text=null)
        {
            if (_itemCount < 4)
            {
                DrawLeft(icon, text);
            }
            else
            {
                DrawRight(icon, text);
            }

            _itemCount++;
        }

        private void DrawLeft(BitmapImage icon, FormattedText text = null)
        {
            _context.DrawImage(icon, new Rect(_origin.Item1,
                                                    _origin.Item2 + _itemCount * ItemHeight,
                                                    16, 16));
            if (text != null)
            {
                _context.DrawText(text, new Point(_origin.Item1 + 20, _origin.Item2 + _itemCount * ItemHeight));
            }
        }

        private void DrawRight(BitmapImage icon, FormattedText text = null)
        {
        }

        public void Clear()
        {
            _itemCount = 0;
        }
    }
}
