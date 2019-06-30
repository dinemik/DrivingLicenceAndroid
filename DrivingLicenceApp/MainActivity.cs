using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Android.Support.Design.Widget;
using System;
using Android.Content;

namespace DrivingLicenceApp
{
    /*
     * TODO
     * MainLauncher = false...
     * this is main UI
     */
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
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

            Intent Category = new Intent(this, typeof(CategoryActivity));
            StartActivity(Category);
        }

        private void SingIn(object sender, EventArgs args)
        {
            Intent Category = new Intent(this, typeof(CategoryActivity));
            StartActivity(Category);
        }

        private void SingUp(object sender, EventArgs args)
        {

        }
    }
}