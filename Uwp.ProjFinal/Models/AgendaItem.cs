using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwp.ProjFinal.Abstracts;

namespace Uwp.ProjFinal.Models
{
    public class AgendaItem : NotifyableClass
    {
        public AgendaItem()
        {
        }
        private Guid _Id;
        public Guid Id
        {
            get { return _Id; }
            set { Set(ref _Id, value); }
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { Set(ref _Title, value); }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { Set(ref _Description, value); }
        }

        private DateTime _Time;
        public DateTime Time
        {
            get { return _Time; }
            set { Set(ref _Time, value); }
        }

        private bool _RemindMe;
        public bool RemindMe
        {
            get { return _RemindMe; }
            set { Set(ref _RemindMe, value); }
        }
    }
}
