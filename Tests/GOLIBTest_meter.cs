#define MONO_CAIRO_DEBUG_DISPOSE


using System;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using System.Diagnostics;

//using GGL;
using go;
using System.Threading;


namespace test
{
	class GOLIBTest_meter : OpenTKGameWindow, IValueChange
	{
		public GOLIBTest_meter ()
			: base(1024, 600,"test")
		{}

		#region IValueChange implementation
		public event EventHandler<ValueChangeEventArgs> ValueChanged;
		#endregion

		#region FPS
		int _fps = 0;

		public int fps {
			get { return _fps; }
			set {
				if (_fps == value)
					return;

				_fps = value;

				if (_fps > fpsMax) {
					fpsMax = _fps;
					ValueChanged.Raise(this, new ValueChangeEventArgs ("fpsMax", fpsMax));
				} else if (_fps < fpsMin) {
					fpsMin = _fps;
					ValueChanged.Raise(this, new ValueChangeEventArgs ("fpsMin", fpsMin));
				}

				if (ValueChanged != null)
					ValueChanged.Raise(this, new ValueChangeEventArgs ("fps", _fps));
			}
		}
		string name = "testName";

		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

		public int fpsMin = int.MaxValue;
		public int fpsMax = 0;

		void resetFps ()
		{
			fpsMin = int.MaxValue;
			fpsMax = 0;
			_fps = 0;
		}
		#endregion
		AnalogMeter g;

		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);
			LoadInterface("Interfaces/testMeter.goml", out g);
		}

		protected override void OnRenderFrame (FrameEventArgs e)
		{
			GL.Clear (ClearBufferMask.ColorBufferBit);
			base.OnRenderFrame (e);
			SwapBuffers ();
		}
		private int frameCpt = 0;
		protected override void OnUpdateFrame (FrameEventArgs e)
		{
			base.OnUpdateFrame (e);

			fps = (int)RenderFrequency;

			if (frameCpt > 200) {
				resetFps ();
				frameCpt = 0;
			}
			frameCpt++;
		}
		[STAThread]
		static void Main ()
		{
			Console.WriteLine ("starting example");

			using (GOLIBTest_meter win = new GOLIBTest_meter( )) {
				win.Run (30.0);
			}
		}
	}
}