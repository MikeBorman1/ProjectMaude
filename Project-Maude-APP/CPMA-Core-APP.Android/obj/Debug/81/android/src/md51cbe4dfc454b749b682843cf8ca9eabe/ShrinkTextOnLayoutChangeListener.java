package md51cbe4dfc454b749b682843cf8ca9eabe;


public class ShrinkTextOnLayoutChangeListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnLayoutChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLayoutChange:(Landroid/view/View;IIIIIIII)V:GetOnLayoutChange_Landroid_view_View_IIIIIIIIHandler:Android.Views.View/IOnLayoutChangeListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Xamarin.Toolkit.Effects.Droid.ShrinkTextOnLayoutChangeListener, Xamarin.Toolkit.Effects", ShrinkTextOnLayoutChangeListener.class, __md_methods);
	}


	public ShrinkTextOnLayoutChangeListener ()
	{
		super ();
		if (getClass () == ShrinkTextOnLayoutChangeListener.class)
			mono.android.TypeManager.Activate ("Xamarin.Toolkit.Effects.Droid.ShrinkTextOnLayoutChangeListener, Xamarin.Toolkit.Effects", "", this, new java.lang.Object[] {  });
	}

	public ShrinkTextOnLayoutChangeListener (android.widget.TextView p0)
	{
		super ();
		if (getClass () == ShrinkTextOnLayoutChangeListener.class)
			mono.android.TypeManager.Activate ("Xamarin.Toolkit.Effects.Droid.ShrinkTextOnLayoutChangeListener, Xamarin.Toolkit.Effects", "Android.Widget.TextView, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void onLayoutChange (android.view.View p0, int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8)
	{
		n_onLayoutChange (p0, p1, p2, p3, p4, p5, p6, p7, p8);
	}

	private native void n_onLayoutChange (android.view.View p0, int p1, int p2, int p3, int p4, int p5, int p6, int p7, int p8);

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
