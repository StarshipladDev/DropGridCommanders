namespace DropGrid.Client.Graphics.Gui
{
    public interface ILayoutManager<TArgumentType>
    {
        void Add(AbstractComponent component);
        void Add(AbstractComponent component, TArgumentType parameter);
        
        void Remove(AbstractComponent component);
        void Remove(AbstractComponent component, TArgumentType parameter);
        
        void Apply(in AbstractContainer container);
    }
}