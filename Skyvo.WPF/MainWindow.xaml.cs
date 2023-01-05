using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ICSharpCode.AvalonEdit;
using Skyvo.WPF.Commands;

namespace Skyvo.WPF;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var CommonOperationCommands = new List<CommandItem>()
        {
            new CommandItem()
            {
                Name = "Exit Window",
                Key = Key.X,
                Modifiers = ModifierKeys.Control | ModifierKeys.Shift,
                Command = new RelayCommand(() => Close())
            },
            new CommandItem()
            {
                Name = "Minimize Window",
                Key = Key.M,
                Modifiers = ModifierKeys.Control | ModifierKeys.Shift,
                Command = new RelayCommand(() => WindowState = WindowState.Minimized)
            },
            new CommandItem()
            {
                Name = "Show Command Bar",
                Key = Key.P,
                Modifiers = ModifierKeys.Control | ModifierKeys.Shift,
                Command = new RelayCommand(() =>
                {
                    CommandBar.Visibility = CommandBar.Visibility == Visibility.Collapsed
                        ? Visibility.Visible
                        : Visibility.Collapsed;

                    if (CommandBar.Visibility == Visibility.Visible)
                        SearchCommandBox.Focus();
                    else 
                        ((CodeEditor)CodeTabControl.SelectedContent)?.Focus();
                })
            },
            new CommandItem()
            {
                Name = "Add Tab",
                Key = Key.T,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() =>
                {
                    CodeTabControl.Items.Add(new TabItem()
                                         {
                        Header = "Untitled" + ((int)CodeTabControl.Items.Count + 1),
                        Content = new CodeEditor(),
                        IsSelected = true
                    });
                })
            },
            new CommandItem()
            {
                Name = "Remove Tab",
                Key = Key.W,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() =>
                {
                    if (CodeTabControl.Items.Count > 1)
                        CodeTabControl.Items.RemoveAt(CodeTabControl.SelectedIndex);
                })
            },
        };
        
        var TabItemOperationCommands = new List<CommandItem>()
        {
            new CommandItem()
            {
                Name = "Select Tab One",
                Key = Key.D1,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() => CodeTabControl.SelectedIndex = 0)
            },
            new CommandItem()
            {
                Name = "Select Tab Two",
                Key = Key.D2,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() => CodeTabControl.SelectedIndex = 1)
            },
            new CommandItem()
            {
                Name = "Select Tab Three",
                Key = Key.D3,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() => CodeTabControl.SelectedIndex = 2)
            },
            new CommandItem()
            {
                Name = "Select Tab Four",
                Key = Key.D4,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() => CodeTabControl.SelectedIndex = 3)
            },
            new CommandItem()
            {
                Name = "Select Tab Five",
                Key = Key.D5,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() => CodeTabControl.SelectedIndex = 4)
            },
            new CommandItem()
            {
                Name = "Select Tab Six",
                Key = Key.D6,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() => CodeTabControl.SelectedIndex = 5)
            },
            new CommandItem()
            {
                Name = "Select Tab Seven",
                Key = Key.D7,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() => CodeTabControl.SelectedIndex = 6)
            },
            new CommandItem()
            {
                Name = "Select Tab Eight",
                Key = Key.D8,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() => CodeTabControl.SelectedIndex = 7)
            },
            new CommandItem()
            {
                Name = "Select Tab Nine",
                Key = Key.D9,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() =>
                {
                    CodeTabControl.SelectedIndex =
                        (CodeTabControl.Items.Count > 9) ? CodeTabControl.Items.Count - 1 : 8;
                })
            },
        };

        
        foreach (var commonOPC in CommonOperationCommands)
        {
            CommandItem.ApplyInputBindings(InputBindings, commonOPC);
            CommandListListBox.Items.Add(new CommandItemView(commonOPC));
        }
        
        foreach (var tabItemOPC in TabItemOperationCommands)
        {
            CommandItem.ApplyInputBindings(InputBindings, tabItemOPC);
            CommandListListBox.Items.Add(new CommandItemView(tabItemOPC));
        }
    }

    private void TabControl_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }
}