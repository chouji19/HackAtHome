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
using HAckAtHome.SAL;
using Android.Webkit;
using Android.Graphics;

namespace HackAtHomeClient
{
    [Activity(Label = "@string/ApplicationName",  Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo")]
    public class EvidenceDetailActivity : Activity
    {
        TextView textViewLab;
        TextView textViewStatus;
        TextView textViewName;
        WebView webViewDetail;
        ImageView imageViewLab;
        string Token, Name;
        int EvidenceId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EvidenceDetail);
            textViewLab = FindViewById<TextView>(Resource.Id.textViewLaboratorioDetail);
            textViewName = FindViewById<TextView>(Resource.Id.textViewNombreDetail);
            textViewStatus = FindViewById<TextView>(Resource.Id.textViewStatusDetail);
            webViewDetail = FindViewById<WebView>(Resource.Id.webViewDetail);
            imageViewLab = FindViewById<ImageView>(Resource.Id.imageViewLab);
            Name = Intent.GetStringExtra("Name");
            Token = Intent.GetStringExtra("Token");
            EvidenceId = Intent.GetIntExtra("EvidenceId",0);
            textViewName.Text = Name;
            textViewLab.Text = Intent.GetStringExtra("Title");
            textViewStatus.Text = Intent.GetStringExtra("Status");
            LoadDetail();
            Toast.MakeText(this, Intent.GetStringExtra("EvidenceId"), ToastLength.Long);
        }

        private async void LoadDetail()
        {
            EvidencesService service = new EvidencesService();
            var detail = await service.GetEvidenceByIDAsync(Token,EvidenceId);
            if(detail!=null)
            {
                webViewDetail.LoadDataWithBaseURL(null, $"<font color=\"white\">{detail.Description.Replace("<br/><br/>", "")}</font>", "text/html","uft-8",null);
                webViewDetail.SetBackgroundColor(Color.Transparent);
                Koush.UrlImageViewHelper.SetUrlDrawable(imageViewLab,detail.Url);
            }
        }
    }
}