namespace DropGrid.Client.Graphics.Gui
{
    public abstract class AbstractLayoutManager<TArgumentType> : ILayoutManager<TArgumentType>
    {
        public abstract void Add(AbstractComponent component);
        public abstract void Add(AbstractComponent component, TArgumentType parameter);
        public abstract void Remove(AbstractComponent component);
        public abstract void Remove(AbstractComponent component, TArgumentType parameter);
        
        public abstract void LayoutChildren(AbstractContainer container);
        
        public void Apply(in AbstractContainer container)
        {
            AdjustContainerSize(container);
            
            LayoutChildren(container);
        }

        private void AdjustContainerSize(AbstractContainer container)
        {
            Dimensions minSize = container.GetMinimumSize();
            Dimensions maxSize = container.GetMaximumSize();
            Dimensions prefSize = container.GetPreferredSize();
            
        }
    }
}