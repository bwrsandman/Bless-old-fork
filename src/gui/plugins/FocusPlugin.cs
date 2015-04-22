using System;
using Gtk;
using Bless.Gui;
using Bless.Gui.Areas;
using Bless.Gui.Drawers;
using Bless.Plugins;
using Bless.Tools;

namespace Bless.Gui.Plugins
{

	public class FocusPlugin : GuiPlugin
	{
		DataBook dataBook;
		NullPatternHighlighter patternHighlighter;
		Window mainWindow;
		IPluginPreferences pluginPreferences;

		// Analysis disable once UnusedParameter
		public FocusPlugin (Window mw, UIManager uim)
		{
			mainWindow = mw;
			pluginPreferences = new FocusPreferences ();

			name = "Focus";
			author = "Sandy Carter";
			description = "Reduce focus on null bytes";
		}

		public override bool Load ()
		{
			dataBook = (DataBook)GetDataBook (mainWindow);

			patternHighlighter = new NullPatternHighlighter (dataBook);

			Preferences.Proxy.Subscribe ("Highlight.Focus", "fc", new PreferencesChangedHandler (OnPreferencesChanged));

			loaded = true;
			return true;
		}

		public override IPluginPreferences PluginPreferences {
			get { return pluginPreferences; }
		}

		void OnPreferencesChanged (Preferences prefs)
		{
			if (prefs ["Highlight.Focus"] == "True")
				patternHighlighter.Active = true;
			else
				patternHighlighter.Active = false;
		}
	}

class NullPatternHighlighter : PatternHighlighter
	{

		public NullPatternHighlighter (DataBook db)
			: base(db)
		{
			findStrategy.Pattern = new byte[1]{ 0 };  // Null bytes
		}

		/// <summary>
		/// Adds pattern match highlights to an area group before it is rendered
		/// </summary>
		protected override void BeforeRender (AreaGroup ag)
		{
			if (!active)
				return;

			int nrows;
			Util.Range view = ag.GetViewRange (out nrows);

			if (view.Start < 0 || view.End < 0)
				return;

			findStrategy.Buffer = ag.Buffer;
			findStrategy.Position = view.Start;

			// Merge overlapping matches
			Util.Range match;
			Util.Range currentHighlight = new Util.Range ();

			while ((match = findStrategy.FindNext (view.End)) != null) {
				if (currentHighlight.End >= match.Start)
					currentHighlight.End = match.End;
				else { 
					ag.AddHighlight (currentHighlight.Start, currentHighlight.End, Drawer.HighlightType.Unfocus);
					currentHighlight = match;
				}
			}

			ag.AddHighlight (currentHighlight.Start, currentHighlight.End, Drawer.HighlightType.Unfocus);
		}
	}

	class FocusPreferences : IPluginPreferences
	{
		FocusPreferencesWidget preferencesWidget;

		public Widget Widget {
			get {
				if (preferencesWidget == null)
					InitWidget ();
				return preferencesWidget;
			}
		}

		public void LoadPreferences ()
		{
			if (preferencesWidget == null)
				InitWidget ();

			preferencesWidget.EnableFocusCheckButton.Active = Preferences.Instance ["Highlight.Focus"] == "True";
		}


		public void SavePreferences ()
		{

		}

		void InitWidget ()
		{
			preferencesWidget = new FocusPreferencesWidget ();
			preferencesWidget.EnableFocusCheckButton.Toggled += OnEnableFocusToggled;
		}

		void OnEnableFocusToggled (object o, EventArgs args)
		{
			Preferences.Instance ["Highlight.Focus"] = preferencesWidget.EnableFocusCheckButton.Active.ToString ();
		}
	}

	class FocusPreferencesWidget : HBox
	{
		readonly CheckButton enableFocusCheckButton;

		public CheckButton EnableFocusCheckButton {
			get { return enableFocusCheckButton; }
		}

		public FocusPreferencesWidget ()
		{
			enableFocusCheckButton = new CheckButton ("Unfocus null bytes");
			PackStart (enableFocusCheckButton, false, false, 6);
			ShowAll ();
		}
	}

}