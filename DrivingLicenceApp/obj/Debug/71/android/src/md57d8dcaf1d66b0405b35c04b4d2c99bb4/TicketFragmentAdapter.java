package md57d8dcaf1d66b0405b35c04b4d2c99bb4;


public class TicketFragmentAdapter
	extends android.support.v4.app.FragmentPagerAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getCount:()I:GetGetCountHandler\n" +
			"n_getItem:(I)Landroid/support/v4/app/Fragment;:GetGetItem_IHandler\n" +
			"";
		mono.android.Runtime.register ("DrivingLicenceApp.ViewPager.TicketFragmentAdapter, DrivingLicenceApp", TicketFragmentAdapter.class, __md_methods);
	}


	public TicketFragmentAdapter (android.support.v4.app.FragmentManager p0)
	{
		super (p0);
		if (getClass () == TicketFragmentAdapter.class)
			mono.android.TypeManager.Activate ("DrivingLicenceApp.ViewPager.TicketFragmentAdapter, DrivingLicenceApp", "Android.Support.V4.App.FragmentManager, Xamarin.Android.Support.Fragment", this, new java.lang.Object[] { p0 });
	}


	public int getCount ()
	{
		return n_getCount ();
	}

	private native int n_getCount ();


	public android.support.v4.app.Fragment getItem (int p0)
	{
		return n_getItem (p0);
	}

	private native android.support.v4.app.Fragment n_getItem (int p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
