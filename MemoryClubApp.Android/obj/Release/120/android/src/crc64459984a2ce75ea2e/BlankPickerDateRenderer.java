package crc64459984a2ce75ea2e;


public class BlankPickerDateRenderer
	extends crc643f46942d9dd1fff9.EntryRenderer
	implements
		mono.android.IGCUserPeer,
		android.app.DatePickerDialog.OnDateSetListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDateSet:(Landroid/widget/DatePicker;III)V:GetOnDateSet_Landroid_widget_DatePicker_IIIHandler:Android.App.DatePickerDialog/IOnDateSetListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Global.InputForms.Droid.Renderers.BlankPickerDateRenderer, Global.InputForms.Droid", BlankPickerDateRenderer.class, __md_methods);
	}


	public BlankPickerDateRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == BlankPickerDateRenderer.class)
			mono.android.TypeManager.Activate ("Global.InputForms.Droid.Renderers.BlankPickerDateRenderer, Global.InputForms.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public BlankPickerDateRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == BlankPickerDateRenderer.class)
			mono.android.TypeManager.Activate ("Global.InputForms.Droid.Renderers.BlankPickerDateRenderer, Global.InputForms.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public BlankPickerDateRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == BlankPickerDateRenderer.class)
			mono.android.TypeManager.Activate ("Global.InputForms.Droid.Renderers.BlankPickerDateRenderer, Global.InputForms.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public void onDateSet (android.widget.DatePicker p0, int p1, int p2, int p3)
	{
		n_onDateSet (p0, p1, p2, p3);
	}

	private native void n_onDateSet (android.widget.DatePicker p0, int p1, int p2, int p3);

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
