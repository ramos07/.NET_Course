using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace FitnessMembership.Model
{
    class Member : MessageBase
    {
        public string FirstName { get; set; }
    }
}
