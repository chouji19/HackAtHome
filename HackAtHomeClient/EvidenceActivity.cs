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
using HackAtHome.CustomAdapters;
using HackAtHome.Entities;
using HAckAtHome.SAL;

namespace HackAtHomeClient
{
    [Activity(Label = "@string/ApplicationName", Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo")]
    public class EvidenceActivity : Activity
    {
        TextView textViewLab;
        TextView textViewStatus;
        TextView textViewName;
        ListView ListEvidences;
        EvidenceFragment EvFrag;
        string Token,Name;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Evidence);
            textViewLab = FindViewById<TextView>(Resource.Id.textViewLaboratorio);
            textViewName = FindViewById<TextView>(Resource.Id.textViewNombre);
            textViewStatus = FindViewById<TextView>(Resource.Id.textViewStatus);
            Name = Intent.GetStringExtra("Name");
            Token = Intent.GetStringExtra("Token");
            textViewName.Text = Name;
            ListEvidences = FindViewById<ListView>(Resource.Id.ListItems);
            ListEvidences.ItemClick += ListEvidences_ItemClick;
            EvFrag = (EvidenceFragment)this.FragmentManager.FindFragmentByTag("EvFrag");
            if (EvFrag == null)
            {
                EvFrag = new EvidenceFragment();
                var FragmentTransaction = this.FragmentManager.BeginTransaction();
                FragmentTransaction.Add(EvFrag, "EvFrag");
                FragmentTransaction.Commit();
                GetEvidences();
            }
            else
            {
                ListEvidences.Adapter = new EvidencesAdapter(this, EvFrag.EvidenceList, Resource.Layout.ListItem, Resource.Id.textViewLaboratorio, Resource.Id.textViewStatus);
            }



        }

        private void ListEvidences_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if(EvFrag.EvidenceList[e.Position]!=null)
            {
                var Evidence = EvFrag.EvidenceList[e.Position];
                var intent = new Intent(this, typeof(EvidenceDetailActivity));
                intent.PutExtra("Token", Token);
                intent.PutExtra("Name",Name);
                intent.PutExtra("EvidenceId",Evidence.EvidenceId);
                intent.PutExtra("Status", Evidence.Status);
                intent.PutExtra("Title", Evidence.Title);
                StartActivity(intent);
            }
        }


        private async void GetEvidences()
        {
            //var FragmentTransaction = this.FragmentManager.BeginTransaction();
            
            EvidencesService service = new EvidencesService();
            EvFrag.EvidenceList = await service.GetEvidencesAsync(Token);
            if (EvFrag.EvidenceList.Count>0)
            {
                ListEvidences.Adapter = new EvidencesAdapter(this, EvFrag.EvidenceList, Resource.Layout.ListItem,Resource.Id.textViewLaboratorio,Resource.Id.textViewStatus);
                
                //txtEmail.Text = result.FullName;

            }
        }
    }
}