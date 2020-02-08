using System.Collections.Generic;

namespace DropGrid.Client.Graphics.Gui
{
    public enum BorderSector { North, West, East, South, Center }
    
    public class BorderLayout : AbstractLayoutManager<BorderSector>
    {
        private Dictionary<BorderSector, AbstractComponent> _layout = new Dictionary<BorderSector,AbstractComponent>();
        
        public override void Add(AbstractComponent component)
        {
            Add(component, BorderSector.Center);
        }

        public override void Add(AbstractComponent component, BorderSector sector)
        {
            if (_layout.ContainsKey(sector))
                _layout.Remove(sector);
            
            _layout.Add(sector, component);
        }

        public override void Remove(AbstractComponent component)
        {
            foreach (BorderSector sector in _layout.Keys)
            {
                if (_layout[sector] != component)
                    continue;
                
                Remove(component, sector);
                break;
            }
        }

        public override void Remove(AbstractComponent component, BorderSector parameter)
        {
            _layout.Remove(parameter);
        }

        public override void LayoutChildren(AbstractContainer container)
        {
            
        }
    }
}