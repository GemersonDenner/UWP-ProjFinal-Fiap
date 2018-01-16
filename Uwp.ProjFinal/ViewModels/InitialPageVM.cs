using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwp.ProjFinal.Models;
using Uwp.ProjFinal.Abstracts;
using Windows.UI.Xaml;
using Uwp.ProjFinal.Services;
using Uwp.ProjFinal.Pages;
using System.Collections.ObjectModel;
using Uwp.ProjFinal.Repositories;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Newtonsoft.Json;

namespace Uwp.ProjFinal.ViewModels
{
    public class InitialPageVM : NotifyableClass
    {
        public async void Inicializate()
        {
            await AgendaItemRepository.LoadAll();
        }

        public EFAgendaItemRepository AgendaItemRepository { get; private set; } = EFAgendaItemRepository.Instance;

        public ObservableCollection<AgendaItem> AgendaItems => AgendaItemRepository.Items;

        private AgendaItem _AgendaItem;

        public AgendaItem AgendaItem
        {
            get { return _AgendaItem; }
            set { Set(ref _AgendaItem, value); }
        }

        private bool _isSplitViewOpen;

        public bool IsSplitViewOpen
        {
            get { return _isSplitViewOpen; }
            set { Set(ref _isSplitViewOpen, value); }
        }

        private Visibility _CanCancel;
        public Visibility CanCancel
        {
            get { return _CanCancel; }
            set { Set(ref _CanCancel, value); }
        }


        private Visibility _CanDelete;
        public Visibility CanDelete
        {
            get { return _CanDelete; }
            set { Set(ref _CanDelete, value); }
        }

        private Visibility _CanInclude;
        public Visibility CanInclude
        {
            get { return _CanInclude; }
            set { Set(ref _CanInclude, value); }
        }

        private string _TextFlyout;
        public string TextFlyout
        {
            get { return _TextFlyout; }
            set { Set(ref _TextFlyout, value); }
        }


        private ObservableCollection<AgendaItem> _SelectedsAgendaItemByAgendaDate;
        public ObservableCollection<AgendaItem> SelectedsAgendaItemByAgendaDate
        {
            get { return _SelectedsAgendaItemByAgendaDate ?? new ObservableCollection<AgendaItem>(); }
            set { Set(ref _SelectedsAgendaItemByAgendaDate, value); }
        }

        public enum PageState
        {
            MinWidth0 = 0,
            MinWidth700
        }
        public PageState State { get; set; }

        private ObservableCollection<AgendaDates> _AgendaDates;
        public ObservableCollection<AgendaDates> AgendaDates
        {
            get { return _AgendaDates = ListAgendaDates(); }
            set
            {
                if (Equals(_AgendaDates, value))
                {
                    return;
                }
                Set(ref _AgendaDates, value);
            }
        }

        private ObservableCollection<AgendaDates> ListAgendaDates()
        {
            var itens = AgendaItems.Select(x => x.Time.ToString("MM/yyyy"));
            var lista = new ObservableCollection<AgendaDates>();
            itens.Distinct().ToList().ForEach(x =>
            {
                var ai = new AgendaDates()
                {
                    FormatedDate = x,
                    Quantity = itens.Count(c => c.Equals(x))
                };
                lista.Add(ai);
            });
            return lista;
        }

        private AgendaDates _selectedAgendaDate;
        public AgendaDates SelectedAgendaDate
        {
            get { return _selectedAgendaDate; }
            set
            {
                if (Equals(_selectedAgendaDate, value))
                {
                    return;
                }
                Set(ref _selectedAgendaDate, value);

                //FilteredTodoItems = TodoItems;
            }
        }
        public void AgendaDate_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var listView = (ListView)sender;

            var agendaDate = ((FrameworkElement)e.OriginalSource).DataContext as AgendaDates;

            if (SelectedAgendaDate != null && Equals(agendaDate, SelectedAgendaDate))
            {
                SelectedAgendaDate = null;
            }
            else
            {
                SelectedAgendaDate = agendaDate;
                FillAgendaItemByAgendaDate(SelectedAgendaDate);
            }
        }

        private void FillAgendaItemByAgendaDate(AgendaDates objOrigin)
        {
            TextFlyout = string.Empty;
            SelectedsAgendaItemByAgendaDate.Clear();
            AgendaItems.Where(x => x.Time.ToString("MM/yyyy").Equals(objOrigin.FormatedDate)).ToList().ForEach(f =>
            {
                SelectedsAgendaItemByAgendaDate.Add(f);
                TextFlyout += (!string.IsNullOrEmpty(TextFlyout)) ? Environment.NewLine : string.Empty;
                TextFlyout += $"{f.Time.ToString("dd/MM/yyyy")} - {f.Title}";
            });
        }

        public void AgendaDate_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            SelectedAgendaDate = ((FrameworkElement)e.OriginalSource).DataContext as AgendaDates;
            FillAgendaItemByAgendaDate(SelectedAgendaDate);
        }

        public void AddAgendaItem_Click(object sender, RoutedEventArgs e)
        {
            CanDelete = Visibility.Collapsed;

            if (State == PageState.MinWidth700)
            {
                AgendaItem = new AgendaItem();
            }
            else
            {
                NavigationService.Navigate<AddAgendaItem>(new AgendaItem());
            }
        }

        public async void DeleteAgendaItem_Click(object sender, RoutedEventArgs e)
        {
            if (AgendaItem.Id != new Guid())
            {
                await AgendaItemRepository.Delete(AgendaItem);
                CanDelete = Visibility.Collapsed;
                this.AgendaDates = ListAgendaDates();
            }
        }

        public void HamburguerButton_Click()
        {
            IsSplitViewOpen = !IsSplitViewOpen;
        }

        public void AgendaItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = (ListView)sender;

            if (listView.SelectedItem == null)
            {
                return;
            }

            if (State == PageState.MinWidth700)
            {
                AgendaItem = JsonConvert.DeserializeObject<AgendaItem>(JsonConvert.SerializeObject(listView.SelectedItem));
                CanDelete = Visibility.Visible;
            }
            else
            {
                NavigationService.Navigate<AddAgendaItem>(listView.SelectedItem);
            }
        }
    }
}
