using System;

namespace DropGrid.Client.Graphics.Gui
{
    public abstract class AbstractContainer : AbstractComponent
    {
        protected override void SetParent(AbstractComponent parent)
        {
            if (!parent.GetType().IsSubclassOf(typeof(AbstractContainer)))
                throw new ArgumentException("Containers cannot have a non-container parent!");
            
            base.SetParent(parent);
        }
    }
}