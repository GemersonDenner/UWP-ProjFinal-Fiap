using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwp.ProjFinal.Abstracts;
using Uwp.ProjFinal.Models;
using Uwp.ProjFinal.Pages;
using Uwp.ProjFinal.Repositories;
using Uwp.ProjFinal.Services;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;

namespace Uwp.ProjFinal.ViewModels
{
    public class AddAgendaItemVM : NotifyableClass
    {
        public AddAgendaItemVM()
        {
            DataTransferManager.GetForCurrentView().DataRequested += AddAgendaItemVM_DataRequested;
        }

        private void AddAgendaItemVM_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;

            StringBuilder text = new StringBuilder();
            text.AppendLine($"Tarefa: {AgendaItem.Title}");
            text.AppendLine($"Descricao: {AgendaItem.Description}");

            request.Data.SetText(text.ToString());
            request.Data.Properties.Title = AgendaItem.Title;
        }
        public EFAgendaItemRepository AgendaItemRepository { get; private set; } = EFAgendaItemRepository.Instance;

        private AgendaItem _AgendaItem;
        public AgendaItem AgendaItem
        {
            get { return _AgendaItem ?? new AgendaItem(); }
            set { Set(ref _AgendaItem, value); }
        }
        public void createNewAgenda()
        {
        }

        public enum PageState
        {
            MinWidth0 = 0,
            MinWidth700
        }
        public PageState State { get; set; }

        public void CancelAdd_Click(object sender, RoutedEventArgs e)
        {

            if (State == PageState.MinWidth700)
            {
                AgendaItem = new AgendaItem();
            }
            else
            {
                NavigationService.Navigate<InitialPage>();
            }
        }

        public async void AddAgendaItem_ClickAsync(object sender, RoutedEventArgs e)
        {
            var dados = this.AgendaItem;
            await AgendaItemRepository.Create(dados);
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
