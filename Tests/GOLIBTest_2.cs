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
	class GOLIBTest_2 : OpenTKGameWindow
	{
		public GOLIBTest_2 ()
			: base(1024, 600,"test")
		{}

		Container g;

		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);
			LoadInterface("Interfaces/test2.goml", out g);

		}
		protected override void OnRenderFrame (FrameEventArgs e)
		{
			GL.Clear (ClearBufferMask.ColorBufferBit);
			base.OnRenderFrame (e);
			SwapBuffers ();
		}

		protected override void OnUpdateFrame (FrameEventArgs e)
		{
			base.OnUpdateFrame (e);
		}

		[STAThread]
		static void Main ()
		{
			Console.WriteLine ("starting example");

			using (GOLIBTest_2 win = new GOLIBTest_2( )) {
				win.Run (30.0);
			}
		}
	}
}