using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;

namespace Skyvo.WPF;

public class CodeEditor : TextEditor
{
    public CodeEditor()
    {
        HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

        ShowLineNumbers = true;
        Foreground = ((SolidColorBrush)new BrushConverter().ConvertFromString("#FFF"));
        LineNumbersForeground = ((SolidColorBrush)new BrushConverter().ConvertFromString("#FFF"));

        Background = Brushes.Transparent;
        BorderBrush = Brushes.Transparent;
        BorderThickness = new Thickness(0);

        Margin = new Thickness(5);
        Padding = new Thickness(3);

        FontFamily = new FontFamily("Consolas");
        FontSize = 15;

        Options.HighlightCurrentLine = true;
        Options.EnableHyperlinks = false;
        Options.EnableEmailHyperlinks = false;
        Options.EnableImeSupport = false;
        Options.AllowScrollBelowDocument = true;
        
        Border PadBorder = new Border()
        {
            Margin = new Thickness(10)
        };
        
        TextArea.LeftMargins.RemoveAt(1);
        TextArea.LeftMargins.Insert(1, PadBorder);

        TextArea.TextEntered += (sender, args) =>
        {
            string[] LazyChars = { "\"", "'", "(", "{", "[" };
            
            for (int i = 0; i < LazyChars.Length; i++) 
            {
                if (args.Text.Equals(LazyChars[i]))
                {
                    int OldOffset = CaretOffset;
                    
                    string FixedLazyChars =
                        LazyChars[i] == "(" ? ")" : LazyChars[i] == "{" ? "}"
                        : LazyChars[i] == "[" ? "]" : LazyChars[i];
                    
                    TextArea.Document.Insert(CaretOffset, FixedLazyChars);
                    CaretOffset = OldOffset;
                }
            }
        };
    }
}