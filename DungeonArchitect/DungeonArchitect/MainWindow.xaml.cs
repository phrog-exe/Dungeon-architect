using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static DungeonArchitect.DungeonLogic;


    
namespace DungeonArchitect
    {
        public partial class MainWindow : Window
        {
            // Lista kafelków, którą widzi interfejs graficzny
            public ObservableCollection<TileViewModel> TileViews { get; set; } = new ObservableCollection<TileViewModel>();

            public MainWindow()
            {
                InitializeComponent();
                DungeonDisplay.ItemsSource = TileViews;
            }

            private void GenerateBtn_Click(object sender, RoutedEventArgs e)
            {
                // wartości z suwaków
                int size = (int)SizeSlider.Value;
                int complexity = (int)ComplexitySlider.Value;

                
                var builder = new SimpleDungeonBuilder();
                var director = new MapDirector(builder);

               
                director.Construct(size, size, complexity);
                var map = builder.GetMap();

                // Odświeżamy widok w GUI
                TileViews.Clear();
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        var tile = map[x, y];
                        TileViews.Add(new TileViewModel { Color = GetColorForTile(tile.Type) });
                    }
                }
            }

            private SolidColorBrush GetColorForTile(TileType type)
            {
                return type switch
                {
                    TileType.Wall => new SolidColorBrush(Color.FromRgb(30, 30, 30)), // Ciemny szary
                    TileType.Floor => Brushes.AntiqueWhite,
                    TileType.Entrance => Brushes.LimeGreen,
                    TileType.Exit => Brushes.Crimson,
                    _ => Brushes.Black
                };
            }
        }

        // żeby GUI wiedziało jaki kolor narysować
        public class TileViewModel
        {
            public Brush Color { get; set; }
        }
    }
