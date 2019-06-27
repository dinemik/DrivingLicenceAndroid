using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using Android.Support.Design.Widget;
using System;
using Android.Content;

namespace DrivingLicenceApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private TextInputLayout NickOrEmileTxt { get; set; }
        private TextInputLayout PasswordTxt { get; set; }

        private Button SingInBtn { get; set; }
        private Button SingUpBtn { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            NickOrEmileTxt = FindViewById<TextInputLayout>(Resource.Id.LogInNickOrEmile);
            PasswordTxt = FindViewById<TextInputLayout>(Resource.Id.LogInPassword);
            //
            SingInBtn = FindViewById<Button>(Resource.Id.SingInBtn);
            SingUpBtn = FindViewById<Button>(Resource.Id.SingUpBtn);
            ///
            SingInBtn.Click += new EventHandler(SingIn);
            SingUpBtn.Click += new EventHandler(SingUp);

            //Intent Category = new Intent(this, typeof(CategoryActivity));
            //StartActivity(Category);
        }

        /*TODO*/
        private void SingIn(object sender, EventArgs args)
        {
            Intent Category = new Intent(this, typeof(CategoryActivity));
            StartActivity(Category);
        }
        
        /*TODO*/
        private void SingUp(object sender, EventArgs args)
        {

        }
    }
}