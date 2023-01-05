using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Skyvo.WPF;

namespace Skyvo.WPF;

public partial class MainWindow : Window
{
    private readonly List<CommandItem> _commonOperationCommands;
    private readonly List<CommandItem> _tabItemOperationCommands;
    
    public MainWindow()
    {
        InitializeComponent();

        _commonOperationCommands = new List<CommandItem>()
        { 
            new()
            {
                Name = "Exit Window",
                Key = Key.X,
                Modifiers = ModifierKeys.Control | ModifierKeys.Shift,
                Command = new RelayCommand(() => Close())
            },
            new()
            {
                Name = "Minimize Window",
                Key = Key.M,
                Modifiers = ModifierKeys.Control | ModifierKeys.Shift,
                Command = new RelayCommand(() => WindowState = WindowState.Minimized)
            },
            new()
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
            new()
            {
                Name = "Add Tab",
                Key = Key.T,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() =>
                {
                    CodeTabControl.Items.Add(new TabItem
                    {
                        Header = "Untitled" + (CodeTabControl.Items.Count + 1),
                        Content = new CodeEditor(),
                        IsSelected = true
                    });
                })
            },
            new()
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
            new()
            {
                Name = "Execute",
                Key = Key.E,
                Modifiers = ModifierKeys.Control | ModifierKeys.Shift,
                Command = new RelayCommand(() => MessageBox.Show("Execute"))
            },
            new()
            {
                Name = "Clear",
                Key = Key.C,
                Modifiers = ModifierKeys.Control | ModifierKeys.Shift,
                Command = new RelayCommand(() => MessageBox.Show("Clear"))
            },
            new()
            {
                Name = "Open",
                Key = Key.G,
                Modifiers = ModifierKeys.Control | ModifierKeys.Shift,
                Command = new RelayCommand(() => MessageBox.Show("Open"))
            },
            new()
            {
                Name = "Save",
                Key = Key.B,
                Modifiers = ModifierKeys.Control | ModifierKeys.Shift,
                Command = new RelayCommand(() => MessageBox.Show("Save"))
            },
            new()
            {
                Name = "Attach",
                Key = Key.A,
                Modifiers = ModifierKeys.Control | ModifierKeys.Shift,
                Command = new RelayCommand(() => MessageBox.Show("Attach"))
            }
        };
        
        _tabItemOperationCommands = new List<CommandItem>
        {
            new()
            {
                Name = "Select Tab One",
                Key = Key.D1,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() => CodeTabControl.SelectedIndex = 0)
            },
            new()
            {
                Name = "Select Tab Two",
                Key = Key.D2,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() => CodeTabControl.SelectedIndex = 1)
            },
            new()
            {
                Name = "Select Tab Three",
                Key = Key.D3,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() => CodeTabControl.SelectedIndex = 2)
            },
            new()
            {
                Name = "Select Tab Four",
                Key = Key.D4,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() => CodeTabControl.SelectedIndex = 3)
            },
            new()
            {
                Name = "Select Tab Five",
                Key = Key.D5,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() => CodeTabControl.SelectedIndex = 4)
            },
            new()
            {
                Name = "Select Tab Six",
                Key = Key.D6,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() => CodeTabControl.SelectedIndex = 5)
            },
            new()
            {
                Name = "Select Tab Seven",
                Key = Key.D7,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() => CodeTabControl.SelectedIndex = 6)
            },
            new()
            {
                Name = "Select Tab Eight",
                Key = Key.D8,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() => CodeTabControl.SelectedIndex = 7)
            },
            new()
            {
                Name = "Select Tab Nine",
                Key = Key.D9,
                Modifiers = ModifierKeys.Control,
                Command = new RelayCommand(() =>
                {
                    CodeTabControl.SelectedIndex =
                        CodeTabControl.Items.Count > 9 ? CodeTabControl.Items.Count - 1 : 8;
                })
            }
        };
        
        Loaded += OnLoaded;
        SearchCommandBox.TextChanged += SearchCommandBoxOnTextChanged;
        SearchCommandBox.KeyDown += SearchCommandBoxOnKeyDown;
    }

    private void SearchCommandBoxOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
            return;
        
        if (CommandListListBox.SelectedIndex < 0)
            return;

        ((CommandItem)((CommandItemView)((ListBoxItem)CommandListListBox.SelectedItem)
                .Content).DataContext)?.Command.Execute(null);
        
        CommandBar.Visibility = Visibility.Collapsed;
    }

    private void LoadKeyBinds(bool firstLoad = false)
    {
        CommandListListBox.Items.Clear();
        
        foreach (var commonOpc in _commonOperationCommands)
        {
            if (firstLoad)
                CommandItem.ApplyInputBindings(InputBindings, commonOpc);
         
            var item = new ListBoxItem()
            {
                Content = new CommandItemView(commonOpc)
            };

            item.PreviewMouseDown += (sender, args) =>
            {
                commonOpc.Command.Execute(null);
                CommandBar.Visibility = Visibility.Collapsed;
            };
            
            CommandListListBox.Items.Add(item);
        }

        foreach (var tabItemOpc in _tabItemOperationCommands)
        {        
            if (firstLoad)
                CommandItem.ApplyInputBindings(InputBindings, tabItemOpc);
        
            var item = new ListBoxItem()
            {
                Content = new CommandItemView(tabItemOpc)
            };

            item.PreviewMouseDown += (sender, args) =>
            {
                tabItemOpc.Command.Execute(null);
                CommandBar.Visibility = Visibility.Collapsed;
            };
            
            CommandListListBox.Items.Add(item);
        }
    }

    private void SearchCommandBoxOnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (SearchCommandBox.Text.Length < 1)
        {
            LoadKeyBinds();
        }

        var oldItems = CommandListListBox.Items.
            OfType<ListBoxItem>().ToList();
        
        CommandListListBox.Items.Clear();
        foreach (var item in oldItems)
        {
            var shortCutContent = (CommandItem)((CommandItemView)item.Content)
                .DataContext;
            
            if (shortCutContent.Name.ToLower().
                Contains(SearchCommandBox.Text.ToLower()))
            {
                CommandListListBox.Items.Add(item);
                
                CommandListListBox.SelectedItem = item;
                CommandListListBox.ScrollIntoView(item);
            }
        }
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        LoadKeyBinds(true);
    }

    private void TabControl_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }
}