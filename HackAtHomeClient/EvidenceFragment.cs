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
using HackAtHome.Entities;

namespace HackAtHomeClient
{
    public class EvidenceFragment : Fragment
    {
        public List<Evidence> EvidenceList { get; set; }

        //public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        //{
        //    View view = inflater.Inflate(Resource.Layout.ListItem, null);
        //    return view;
        //}
        public override void OnCreate(Bundle bundle)
        {
            //Android.Util.Log.Debug("Lab11Log", "Activity B - onCreate");
            base.OnCreate(bundle);
            RetainInstance = true;
        }
    }
}