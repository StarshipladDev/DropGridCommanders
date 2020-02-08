using System.Collections.Generic;
using JetBrains.Annotations;

namespace DropGrid.Client.Graphics.Gui
{
    public abstract class AbstractComponent
    {
        private ComponentState _state;
        private AbstractComponent _parent;
        private List<AbstractComponent> _children = new List<AbstractComponent>();

        // Listeners
        private List<IStateDependentListener> _dependentListeners = new List<IStateDependentListener>();

        public bool Enabled
        {
            get => Enabled;
            set
            {
                if (value != Enabled)
                {
                    foreach (IStateDependentListener listener in _dependentListeners)
                        listener.StateChanged(value);
                }
                
                Enabled = value; 
                _state = value == false ? ComponentState.Disabled : ComponentState.Normal;
                
                foreach (AbstractComponent child in _children)
                    child.Enabled = value;
            }
        }

        public AbstractComponent()
        {
            _state = ComponentState.Normal;
        }

        public void SetEnableStateDependsOn([NotNull] AbstractComponent dependentComponent)
        {
            var defaultListener = new DefaultStateDependentListener(this);
            dependentComponent._dependentListeners.Add(defaultListener);
        }

        public void SetParent(AbstractComponent parent) => _parent = parent;

        [CanBeNull]
        public AbstractComponent GetParent() => _parent;
    }

    internal class DefaultStateDependentListener : IStateDependentListener
    {
        private AbstractComponent _component;
        
        public DefaultStateDependentListener(AbstractComponent component)
        {
            _component = component;
        }
        
        public void StateChanged(bool dependentEnabled)
        {
            _component.Enabled = dependentEnabled;
        }
    } 
}