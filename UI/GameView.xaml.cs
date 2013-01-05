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

        private const int CaseWidth = 96;
        private const int CaseHeight = 96;

        private const int VisibleWidth = 960;
        private const int VisibleHeight = 768;

        private const int ShiftOffset = 96;
        private int OffsetX = 0;
        private int OffsetY = 0;

        private int CaseNumberX;
        private int CaseNumberY;

        private readonly Tuple<int, int> FoodOffset = new Tuple<int, int>(8, 8);
        private const string FoodTexture = "FoodSmall";

        private readonly Tuple<int, int> IronOffset = new Tuple<int, int>(8, 24);
        private const string IronTexture = "IronSmall";

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

        protected Tuple<int, int> CaseOffset(Case c)
        {
            return new Tuple<int, int>(c.X * CaseWidth, c.Y * CaseHeight);
        }

        /// <summary>
        /// Computes an origin for the drawing, since we don't have to render every case. This method
        /// determines what's the first case we have to draw.
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <returns></returns>
        protected Tuple<int, int> DrawingOrigin(int offsetX, int offsetY)
        {
            return new Tuple<int, int>(offsetX / CaseWidth,
                                       offsetY / CaseHeight);
        }

        protected BitmapSource DrawMapToBitmap()
        {
            if (!_isMapDrawn)
            {
                Height = Map.Size * CaseHeight;
                Width = Map.Size * CaseWidth;

                CaseNumberX = (int)Math.Ceiling((double)VisibleWidth / CaseWidth);
                CaseNumberY = (int)Math.Ceiling((double)VisibleHeight / CaseHeight);

                _isMapDrawn = true;
            }
                
            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
            {
                var rect = new Rect(0, 0, CaseWidth, CaseHeight);
                var origin = DrawingOrigin(OffsetX, OffsetY);

                System.Diagnostics.Debug.WriteLine(origin);
                System.Diagnostics.Debug.WriteLine(CaseNumberX);

                for (int i = 0; i < CaseNumberX; i++)
                {
                    for (int j = 0; j < CaseNumberY; j++)
                    {
                        rect.X = i * CaseWidth;
                        rect.Y = j * CaseHeight;

                        drawingContext.DrawImage((BitmapImage)FindResource(Map.GetCaseAt(i + origin.Item1, j + origin.Item2).Texture), rect);
                    }
                }
            }

            var bitmap = new RenderTargetBitmap(
                VisibleWidth, VisibleHeight, 96, 96, PixelFormats.Default);
            bitmap.Render(drawingVisual);

            _bitmapMap = bitmap;

            return _bitmapMap;
        }

        protected void DrawCaseContent(Case c, DrawingContext context)
        {
            DrawFood(c.Food, context, CaseOffset(c));
            DrawIron(c.Iron, context, CaseOffset(c));
        }
        protected void DrawFood(int amount, DrawingContext context, Tuple<int, int> origin)
        {
            if (amount > 0)
            {
                var texture = (BitmapImage)FindResource(FoodTexture);
                context.DrawImage(texture, new Rect(origin.Item1 + FoodOffset.Item1,
                                                    origin.Item2 + FoodOffset.Item2,
                                                    16, 16));
                var formattedText = new FormattedText(
                    amount.ToString(),
                    CultureInfo.GetCultureInfo("en-us"),
                    FlowDirection.LeftToRight,
                    new Typeface("Charlemagne STD"),
                    12,
                    Brushes.PaleVioletRed);
                context.DrawText(formattedText, new Point(origin.Item1 + FoodOffset.Item1 + 20, origin.Item2 + FoodOffset.Item2 + 2));
            }
        }
        protected void DrawIron(int amount, DrawingContext context, Tuple<int, int> origin)
        {
            if (amount > 0)
            {
                var texture = (BitmapImage)FindResource(IronTexture);
                context.DrawImage(texture, new Rect(origin.Item1 + IronOffset.Item1,
                                                    origin.Item2 + IronOffset.Item2,
                                                    16, 16));
                /*var formattedText = new FormattedText(
                    amount.ToString(),
                    CultureInfo.GetCultureInfo("en-us"),
                    FlowDirection.LeftToRight,
                    new Typeface("Charlemagne STD"),
                    12,
                    Brushes.Red);
                context.DrawText(formattedText, new Point(origin.Item1 + IronOffset.Item1 + 20, origin.Item2 + IronOffset.Item2 + 2));*/
            }
        }

        protected override void OnRender(DrawingContext context)
        {
            base.OnRender(context);

            // Draw textures
            // This takes a lot of memory (~750 Mb) but not much time
            var background = DrawMapToBitmap();
            context.DrawImage(background, new Rect(0, 0, VisibleWidth, VisibleHeight));

            // Draw cases' content
            // This takes a lot of time, but not much memory
            /*for (int i = 0; i < Map.Size; i++)
            {
                for (int j = 0; j < Map.Size; j++)
                {
                    DrawCaseContent(Map.GetCaseAt(i, j), context);
                }
            }*/
        }

        protected Case CaseAtPosition(double x, double y)
        {
            int i = (int) (x / CaseWidth);
            int j = (int) (y / CaseHeight);
            return Map.GetCaseAt(i, j);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            double x = e.GetPosition(this).X;
            double y = e.GetPosition(this).Y;
            Case c = CaseAtPosition(x + OffsetX, y + OffsetY);
            System.Diagnostics.Debug.WriteLine("Case:" + c.X + "," + c.Y);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if (OffsetX > 0)
                    {
                        OffsetX -= ShiftOffset;
                        this.InvalidateVisual();
                    }
                    break;
                case Key.Right:
                    
                    if (OffsetX + (CaseNumberX * CaseWidth) <= Width - CaseWidth)
                    {
                        OffsetX += ShiftOffset;
                        this.InvalidateVisual();
                    }
                    break;
                case Key.Up:
                    if (OffsetY > 0)
                    {
                        OffsetY -= ShiftOffset;
                        this.InvalidateVisual();
                    }
                    break;
                case Key.Down:
                    if (OffsetY + (CaseNumberY * CaseHeight) <= Height - CaseHeight)
                    {
                        OffsetY += ShiftOffset;
                        this.InvalidateVisual();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
