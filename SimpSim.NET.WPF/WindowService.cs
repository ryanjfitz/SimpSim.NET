﻿using System.Windows;
using SimpSim.NET.Presentation;
using SimpSim.NET.Presentation.ViewModels;
using SimpSim.NET.WPF.Views;

namespace SimpSim.NET.WPF
{
    internal class WindowService : IWindowService
    {
        public void ShowAssemblyEditorWindow(AssemblyEditorWindowViewModel viewModel)
        {
            Window window = new AssemblyEditorWindow();
            window.DataContext = viewModel;
            window.Owner = Application.Current.MainWindow;
            window.Show();
        }
    }
}