package md52fe15e568299beb87347e401264a32ed;


public class GradientContentPageRenderer
	extends md51558244f76c53b6aeda52c8a337f2c37.PageRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_dispatchDraw:(Landroid/graphics/Canvas;)V:GetDispatchDraw_Landroid_graphics_Canvas_Handler\n" +
			"";
		mono.android.Runtime.register ("Foobar.Droid.Renderers.GradientContentPageRenderer, CPMA-Core-APP.Android", GradientContentPageRenderer.class, __md_methods);
	}


	public GradientContentPageRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == GradientContentPageRenderer.class)
			mono.android.TypeManager.Activate ("Foobar.Droid.Renderers.GradientContentPageRenderer, CPMA-Core-APP.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public GradientContentPageRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == GradientContentPageRenderer.class)
			mono.android.TypeManager.Activate ("Foobar.Droid.Renderers.GradientContentPageRenderer, CPMA-Core-APP.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public GradientContentPageRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == GradientContentPageRenderer.class)
			mono.android.TypeManager.Activate ("Foobar.Droid.Renderers.GradientContentPageRenderer, CPMA-Core-APP.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void dispatchDraw (android.graphics.Canvas p0)
	{
		n_dispatchDraw (p0);
	}

	private native void n_dispatchDraw (android.graphics.Canvas p0);

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
