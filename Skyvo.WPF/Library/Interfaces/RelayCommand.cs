﻿using System;
using System.Windows.Input;

namespace Skyvo.WPF.Commands;

public class RelayCommand : ICommand
{
    private readonly Action _execute;

    public RelayCommand(Action execute)
    {
        _execute = execute;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        _execute?.Invoke();
    }

    public event EventHandler CanExecuteChanged;
}