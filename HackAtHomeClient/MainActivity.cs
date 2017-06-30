using Android.App;
using Android.Widget;
using Android.OS;
using HackAtHome.Entities;
using HAckAtHome.SAL;
using Android.Content;
using HackAtHome.SAL;
using Android.Webkit;
using Android.Graphics;
using Android.Views;
using System.Threading.Tasks;

namespace HackAtHomeClient
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo")]
    public class MainActivity : Activity
    {
        private EditText txtEmail;
        private EditText txtPassword;
        private Button btnValidar;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);
            txtEmail = FindViewById<EditText>(Resource.Id.editTextEmail);
            txtPassword = FindViewById<EditText>(Resource.Id.editTextPassword);
            btnValidar = FindViewById<Button>(Resource.Id.buttonValidate);
            btnValidar.Click += BtnValidar_Click;


        }

        private void BtnValidar_Click(object sender, System.EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                AuthenticateService();
            else
            {
                Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);
                AlertDialog Alert = builder.Create();
                Alert.SetTitle("Formulario");
                Alert.SetIcon(Resource.Drawable.Icon);
                Alert.SetMessage($"Debe ingresar los datos de acceso");
                Alert.SetButton("Ok", (s, ev) => { });
                Alert.Show();
            }

        }

        private async void AuthenticateService()
        {

            AuthenticateService service = new AuthenticateService();
            var result = await service.AutenticateAsync(txtEmail.Text, txtPassword.Text);
            if (result.Status == Status.Success)
            {
                var MicrosoftEvidence = new LabItem
                {
                    Email = txtEmail.Text,
                    DeviceId = Android.Provider.Settings.Secure.GetString(
                        ContentResolver, Android.Provider.Settings.Secure.AndroidId),
                    Lab = "Hack@Home"
                };
                MicrosoftServiceClient mService = new MicrosoftServiceClient();
                var intent = new Intent(this, typeof(EvidenceActivity));
                intent.PutExtra("Token", result.Token);
                intent.PutExtra("Name", result.FullName);
                StartActivity(intent);
                await mService.SendEvidence(MicrosoftEvidence);
            }
            else
            {
                Android.App.AlertDialog.Builder builder = new AlertDialog.Builder(this);
                AlertDialog Alert = builder.Create();
                Alert.SetTitle("Resultado de la verificación");
                Alert.SetIcon(Resource.Drawable.Icon);
                Alert.SetMessage($"{result.Status}\n{result.FullName}\n{result.Token}");
                Alert.SetButton("Ok", (s, ev) => { });
                Alert.Show();
            }
        }
    }
}

