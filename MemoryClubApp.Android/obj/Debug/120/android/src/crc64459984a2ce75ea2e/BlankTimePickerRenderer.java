package crc64459984a2ce75ea2e;


public class BlankTimePickerRenderer
	extends crc643f46942d9dd1fff9.EntryRenderer
	implements
		mono.android.IGCUserPeer,
		android.app.TimePickerDialog.OnTimeSetListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTimeSet:(Landroid/widget/TimePicker;II)V:GetOnTimeSet_Landroid_widget_TimePicker_IIHandler:Android.App.TimePickerDialog/IOnTimeSetListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Global.InputForms.Droid.Renderers.BlankTimePickerRenderer, Global.InputForms.Droid", BlankTimePickerRenderer.class, __md_methods);
	}


	public BlankTimePickerRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == BlankTimePickerRenderer.class)
			mono.android.TypeManager.Activate ("Global.InputForms.Droid.Renderers.BlankTimePickerRenderer, Global.InputForms.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public BlankTimePickerRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == BlankTimePickerRenderer.class)
			mono.android.TypeManager.Activate ("Global.InputForms.Droid.Renderers.BlankTimePickerRenderer, Global.InputForms.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public BlankTimePickerRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == BlankTimePickerRenderer.class)
			mono.android.TypeManager.Activate ("Global.InputForms.Droid.Renderers.BlankTimePickerRenderer, Global.InputForms.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public void onTimeSet (android.widget.TimePicker p0, int p1, int p2)
	{
		n_onTimeSet (p0, p1, p2);
	}

	private native void n_onTimeSet (android.widget.TimePicker p0, int p1, int p2);

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
