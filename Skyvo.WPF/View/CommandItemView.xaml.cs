using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Skyvo.WPF;

public partial class CommandItemView : UserControl
{
    private readonly CommandItem _itemInfo;

    public CommandItemView(CommandItem itemInfo)
    {
        InitializeComponent();

        _itemInfo = itemInfo;
        DataContext = itemInfo;
        
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var Keys = 
            _itemInfo.ShortCutInfo
            .Split(".");

        foreach (var key in Keys)
        {
            var BackBorder = new Border()
            {
                Background = ((SolidColorBrush)(new BrushConverter().ConvertFromString("#272727"))),
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(5, 0, 0, 0),
                Padding = new Thickness(6, 0, 6, 0),
                Effect =  new DropShadowEffect()
                {
                    ShadowDepth = 3,
                    Opacity = 0.4
                }
            };
            
            var KeyTextBlock = new TextBlock()
            {
                Text = key,
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontFamily = ((FontFamily)(Application.Current.TryFindResource("PoppinsRegular")))
            };
            
            BackBorder.Child = KeyTextBlock;
            KeyPanel.Children.Add(BackBorder);
        }
    }
}