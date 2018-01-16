using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Uwp.ProjFinal.Models;
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
    public sealed partial class NewAgendaItem : UserControl
    {
        public AddAgendaItemVM ViewModel { get; } = new AddAgendaItemVM();
        public NewAgendaItem()
        {
            this.InitializeComponent();
            this.ViewModel.createNewAgenda();
        }
        //AddAgendaItem

        public AgendaItem AddAgendaItem
        {
            get { return (AgendaItem)GetValue(EditTodoItemProperty); }
            set
            {
                SetValue(EditTodoItemProperty, value);
                ViewModel.AgendaItem = value;
                ViewModel.createNewAgenda();
                Bindings.Update();
            }
        }

        public static readonly DependencyProperty EditTodoItemProperty =
            DependencyProperty.Register("AddAgendaItem", typeof(AgendaItem), typeof(NewAgendaItem), new PropertyMetadata(new AgendaItem()));

    }
}
