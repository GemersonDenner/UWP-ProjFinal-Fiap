using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwp.ProjFinal.Abstracts;

namespace Uwp.ProjFinal.Models
{
    public class AgendaDates: NotifyableClass
    {


        private string _FormatedDate;

        public string FormatedDate
        {
            get { return _FormatedDate; }
            set { Set(ref _FormatedDate, value); }
        }

        private int _Quantity;

        public int Quantity
        {
            get { return _Quantity; }
            set { Set(ref _Quantity, value); }
        }
    }
}
