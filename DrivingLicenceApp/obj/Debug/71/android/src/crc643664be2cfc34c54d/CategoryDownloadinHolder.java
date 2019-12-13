package crc643664be2cfc34c54d;


public class CategoryDownloadinHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("DrivingLicenceApp.Holder.CategoryDownloadinHolder, DrivingLicenceApp", CategoryDownloadinHolder.class, __md_methods);
	}


	public CategoryDownloadinHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == CategoryDownloadinHolder.class)
			mono.android.TypeManager.Activate ("DrivingLicenceApp.Holder.CategoryDownloadinHolder, DrivingLicenceApp", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
