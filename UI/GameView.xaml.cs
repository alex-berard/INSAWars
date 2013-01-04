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

        private readonly Tuple<int, int> FoodOffset = Tuple.Create(8, 8);
        private const string FoodTexture = "FoodSmall";

        private readonly Tuple<int, int> IronOffset = Tuple.Create(8, 24);
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
            return Tuple.Create(c.X * CaseWidth, c.Y * CaseHeight);
        }

        protected BitmapSource DrawMapToBitmap()
        {
            if (!_isMapDrawn)
            {
                Height = Map.Size * CaseHeight;
                Width = Map.Size * CaseWidth;
                
                var drawingVisual = new DrawingVisual();
                using (var drawingContext = drawingVisual.RenderOpen())
                {
                    var rect = new Rect(0, 0, CaseWidth, CaseHeight);
                    for (int i = 0; i < Map.Size; i++)
                    {
                        for (int j = 0; j < Map.Size; j++)
                        {
                            rect.X = i * CaseWidth;
                            rect.Y = j * CaseHeight;
                            var texture = (BitmapImage)FindResource(Map.GetCaseAt(i, j).Texture);
                            drawingContext.DrawImage(texture, rect);
                        }
                    }
                }

                var bitmap = new RenderTargetBitmap(
                    (int)Width, (int)Height, 96, 96, PixelFormats.Default);
                bitmap.Render(drawingVisual);

                _bitmapMap = bitmap;
                _isMapDrawn = true;
            }

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
                var formattedText = new FormattedText(
                    amount.ToString(),
                    CultureInfo.GetCultureInfo("en-us"),
                    FlowDirection.LeftToRight,
                    new Typeface("Charlemagne STD"),
                    12,
                    Brushes.Red);
                context.DrawText(formattedText, new Point(origin.Item1 + IronOffset.Item1 + 20, origin.Item2 + IronOffset.Item2 + 2));
            }
        }

        protected override void OnRender(DrawingContext context)
        {
            base.OnRender(context);

            // Draw textures
            var background = DrawMapToBitmap();
            context.DrawImage(background, new Rect(0, 0, Width, Height));

            // Draw cases' content
            for (int i = 0; i < Map.Size; i++)
            {
                for (int j = 0; j < Map.Size; j++)
                {
                    DrawCaseContent(Map.GetCaseAt(i, j), context);
                }
            }
        }

        protected Case CaseAtPosition(double x, double y)
        {
            int i = (int) Math.Round(x / CaseWidth);
            int j = (int) Math.Round(y / CaseHeight);
            System.Diagnostics.Debug.WriteLine("Case:" + i.ToString() + "," + j.ToString());
            return Map.GetCaseAt(i, j);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            double x = e.GetPosition(this).X;
            double y = e.GetPosition(this).Y;
            System.Diagnostics.Debug.WriteLine("Clic:" + x.ToString() + "," + y.ToString());
            Case c = CaseAtPosition(x, y);
            System.Diagnostics.Debug.WriteLine("New Value:" + CaseAtPosition(x, y).Food.ToString());
            c.Food = 458;
            this.InvalidateVisual();
        }
    }
}
