using System.Windows.Input;

namespace Skyvo.WPF;

public class CommandItem
{
    public string Name { get; set; }
    public Key Key { get; set; }
    public ModifierKeys Modifiers { get; set; }
    public ICommand Command { get; set; }

    public string ShortCutInfo => $"{Modifiers}.{Key}";

    public static void ApplyInputBindings(
        InputBindingCollection inputBinding,
        CommandItem self)
    {
        var keyBind = new KeyBinding
        {
            Key = self.Key,
            Modifiers = self.Modifiers,
            Command = self.Command
        };

        inputBinding.Add(keyBind);
    }
}