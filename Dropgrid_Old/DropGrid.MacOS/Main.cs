#region Using Statements
using AppKit;
using DropGrid.Client;
#endregion

namespace DropGrid.MacOS
{
	static class Program
	{
		static void Main(string[] args)
		{
			NSApplication.Init();

			using (var game = new GameEngine()) {
			    game.Run();
			}
		}
	}
}
