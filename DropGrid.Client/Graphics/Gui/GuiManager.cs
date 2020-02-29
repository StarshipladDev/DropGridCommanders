using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.Graphics.Gui
{
    public static class GuiManager
    {
        private static List<AbstractComponent> Components = new List<AbstractComponent>();

        public static void Add(AbstractComponent c)
        {
            if (!Components.Contains(c))
                Components.Add(c);
        }

        public static void Remove(AbstractComponent c)
        {
            if (Components.Contains(c))
                Components.Remove(c);
        }

        public static void Render(GameEngine engine, GraphicsRenderer renderer, GameTime time)
        {
            foreach (AbstractComponent c in Components)
            {
                if (c.IsVisible())
                    c.Render(engine, renderer, time);
            }
        }

        public static void Update(GameEngine engine, GameTime gameTime)
        {
            var toRemove = new List<AbstractComponent>();
            foreach (AbstractComponent c in Components)
            {
                if (c.IsDisposed())
                    toRemove.Add(c);
                else if (c.IsVisible())
                    c.Update(engine, gameTime);
            }

            foreach (AbstractComponent c in toRemove)
                Remove(c);
        }
    }
}