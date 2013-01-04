using System;
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
using INSAWars.Game;

namespace UI
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        private bool _isMapDrawn;
        private BitmapSource _bitmapMap;
        const int WIDTH = 96;
        const int HEIGHT = 96;

        public GameView()
        {
            _isMapDrawn = false;
            InitializeComponent();
        }

        public Map Map
        {
            get;
            set;
        }

        protected BitmapSource DrawMapToBitmap()
        {
            if (!_isMapDrawn)
            {
                Height = Map.Size * HEIGHT;
                Width = Map.Size * WIDTH;
                
                DrawingVisual drawingVisual = new DrawingVisual();
                using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                {
                    for (int i = 0; i < Map.Size; i++)
                    {
                        for (int j = 0; j < Map.Size; j++)
                        {
                            BitmapImage texture = (BitmapImage)FindResource(Map.GetCaseAt(i, j).Texture);
                            drawingContext.DrawImage(texture, new Rect(i * WIDTH, j * HEIGHT, WIDTH, HEIGHT));
                        }
                    }
                }

                var bitmap = new RenderTargetBitmap(
                    Convert.ToInt32(Width), Convert.ToInt32(Height), 96, 96, PixelFormats.Default);
                bitmap.Render(drawingVisual);

                _bitmapMap = bitmap;
                _isMapDrawn = true;
            }

            return _bitmapMap;
        }

        protected override void OnRender(DrawingContext context)
        {
            base.OnRender(context);
            BitmapSource bitmap = DrawMapToBitmap();
            context.DrawImage(bitmap, new Rect(0, 0, Width, Height));
        }
    }
}
