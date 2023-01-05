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
        Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFF");
        LineNumbersForeground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFF");

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

        var padBorder = new Border
        {
            Margin = new Thickness(10)
        };

        TextArea.LeftMargins.RemoveAt(1);
        TextArea.LeftMargins.Insert(1, padBorder);

        TextArea.TextEntered += (sender, args) =>
        {
            string[] lazyChars = { "\"", "'", "(", "{", "[" };

            for (var i = 0; i < lazyChars.Length; i++)
            {
                if (args.Text.Equals(lazyChars[i]))
                {
                    var oldOffset = CaretOffset;

                    var fixedLazyChars =
                        lazyChars[i] == "(" ? ")"
                        : lazyChars[i] == "{" ? "}"
                        : lazyChars[i] == "[" ? "]" : lazyChars[i];

                    TextArea.Document.Insert(CaretOffset, fixedLazyChars);
                    CaretOffset = oldOffset;
                }
            }
        };
    }
}