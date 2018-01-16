using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Uwp.ProjFinal.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Uwp.ProjFinal.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InitialPage : Page
    {
        public InitialPageVM ViewModel { get; } = new InitialPageVM();
        public InitialPage()
        {
            this.InitializeComponent();
            ViewModel.CanCancel = Visibility.Collapsed;
            UpdateViewModelState(VisualStateGroupScreenWidth.CurrentState);
            VisualStateGroupScreenWidth.CurrentStateChanged += MainPage_CurrentStateChanged;
            ViewModel.Inicializate();
        }

        private void UpdateViewModelState(VisualState currentState)
        {
            ViewModel.State = currentState == VisualStateMinWidth1 ? InitialPageVM.PageState.MinWidth0 : InitialPageVM.PageState.MinWidth700;

            if (ViewModel.State == InitialPageVM.PageState.MinWidth700)
            {
                ViewModel.IsSplitViewOpen = true;
            }
        }

        private void MainPage_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateViewModelState(e.NewState);
        }
    }
}
