using Avalonia.Controls;
using LibVLCFadeAnimation.ViewModels;
using System;

namespace LibVLCFadeAnimation.Views;

public partial class VideoPlayer : UserControl
{
    public VideoPlayer()
    {
        InitializeComponent();
    }

    private void OnDataContextChanged(object sender, EventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.Play();
        }
    }
}