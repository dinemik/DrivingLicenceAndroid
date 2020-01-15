package md541b73d7972fceac6d3b291d044e81a89;


public class EndUiHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("DrivingLicenceApp.Holder.EndUiHolder, DrivingLicenceApp", EndUiHolder.class, __md_methods);
	}


	public EndUiHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == EndUiHolder.class)
			mono.android.TypeManager.Activate ("DrivingLicenceApp.Holder.EndUiHolder, DrivingLicenceApp", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

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
