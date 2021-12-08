package md54d04b18618a7b07bb9774079e8b8d78b;


public class CustomNavigationPageRenderer
	extends md58432a647068b097f9637064b8985a5e0.NavigationPageRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onViewAdded:(Landroid/view/View;)V:GetOnViewAdded_Landroid_view_View_Handler\n" +
			"";
		mono.android.Runtime.register ("CPMA_Core_APP.Droid.Renderers.CustomNavigationPageRenderer, CPMA-Core-APP.Android", CustomNavigationPageRenderer.class, __md_methods);
	}


	public CustomNavigationPageRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == CustomNavigationPageRenderer.class)
			mono.android.TypeManager.Activate ("CPMA_Core_APP.Droid.Renderers.CustomNavigationPageRenderer, CPMA-Core-APP.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public CustomNavigationPageRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == CustomNavigationPageRenderer.class)
			mono.android.TypeManager.Activate ("CPMA_Core_APP.Droid.Renderers.CustomNavigationPageRenderer, CPMA-Core-APP.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public CustomNavigationPageRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == CustomNavigationPageRenderer.class)
			mono.android.TypeManager.Activate ("CPMA_Core_APP.Droid.Renderers.CustomNavigationPageRenderer, CPMA-Core-APP.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onViewAdded (android.view.View p0)
	{
		n_onViewAdded (p0);
	}

	private native void n_onViewAdded (android.view.View p0);

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
