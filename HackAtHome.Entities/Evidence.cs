using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HackAtHome.Entities
{
    public class Evidence
    {
        public int EvidenceId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
    }
}