using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.Graphics.Gui
{
    public abstract class AbstractComponent
    {
        public int X { get; internal set; }
        public int Y { get; internal set; }
        public int Width { get; internal set; }
        public int Height { get; internal set; }
        
        public int PaddingUp { get; internal set; }
        public int PaddingDown { get; internal set; }
        public int PaddingLeft { get; internal set; }
        public int PaddingRight { get; internal set; }
        
        public int MarginUp { get; internal set; }
        public int MarginDown { get; internal set; }
        public int MarginLeft { get; internal set; }
        public int MarginRight { get; internal set; }

        private Dimensions _preferredSize;
        private Dimensions _minimumSize;
        private Dimensions _maximumSize;
        
        private ComponentState _state;
        private bool _enabled;
        private bool _visible;
        private AbstractComponent _parent;
        private readonly List<AbstractComponent> _children = new List<AbstractComponent>();
        private bool _disposed;

        private Color _backgroundColor = Color.Transparent;
        private Color _foregroundColor = Color.Transparent;

        public AbstractComponent()
        {
            _state = ComponentState.Normal;
        }

        public abstract void Render(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime);
        public abstract void Update(GameEngine engine, GameTime gameTime);
        
        public void AddChildren([NotNull] AbstractComponent aspiringChild, params AbstractComponent[] additionalChildren)
        {
            if (aspiringChild == this)
                throw new ArgumentException("Cannot add component itself to its child!");
            if (aspiringChild.GetParent() != null && aspiringChild.GetParent() != this)
                throw new ArgumentException("Child component already has another parent!");
            
            aspiringChild.SetParent(this);
            _children.Add(aspiringChild);
            
            foreach (AbstractComponent child in additionalChildren) 
                AddChildren(child);
        }
        
        public List<AbstractComponent> GetChildren() => _children;

        protected virtual void SetParent([NotNull] AbstractComponent parent)
        {
            if (parent == this)
                throw new ArgumentException("Cannot set self to be its own parent!");

            AbstractComponent parentTest = parent.GetParent();
            while (parentTest != null)
            {
                if (parentTest == this)
                    throw new ArgumentException("Cannot set a child of the current component to be its own parent!");
                parentTest = parentTest.GetParent();
            }
            
            _parent = parent;
        }

        [CanBeNull]
        public AbstractComponent GetParent() => _parent;

        public void SetMargin(int margin) => SetMargin(margin, margin);
        
        public void SetMargin(int upAndDown, int leftAndRight) =>
            SetMargin(upAndDown, upAndDown, leftAndRight, leftAndRight);
        
        public void SetMargin(int up, int down, int left, int right)
        {
            MarginUp = up;
            MarginDown = down;
            MarginLeft = left;
            MarginRight = right;
        }

        public void SetPadding(int padding) => SetPadding(padding, padding);

        public void SetPadding(int upAndDown, int leftAndRight) =>
            SetPadding(upAndDown, upAndDown, leftAndRight, leftAndRight);
        
        public void SetPadding(int up, int down, int left, int right)
        {
            PaddingUp = PaddingUp;
            PaddingDown = PaddingDown;
            PaddingLeft = PaddingLeft;
            PaddingRight = PaddingRight;
        }

        public bool IsVisible() => _visible;

        public void SetVisible(bool flag)
        {
            _visible = flag;
            ApplyToChildren(child => child.SetVisible(flag));
        }

        public bool IsEnabled() => _enabled;

        public void SetEnabled(bool flag)
        {
            _enabled = flag;
            _state = _enabled ? ComponentState.Normal : ComponentState.Disabled;
            ApplyToChildren(child => child.SetEnabled(flag));
        }

        public void Dispose() => _disposed = true;

        public bool IsDisposed() => _disposed;

        public void SetPreferredWidth(int width) => SetPreferredSize(width, _preferredSize.Height);
        public void SetPreferredHeight(int height) => SetPreferredSize(_preferredSize.Width, height);
        public void SetPreferredSize(int width, int height) => _preferredSize = new Dimensions(width, height);
        public Dimensions GetPreferredSize() => _preferredSize;

        public void SetMinimumWidth(int width) => SetMinimumSize(width, _minimumSize.Height);
        public void SetMinimumHeight(int height) => SetMinimumSize(_minimumSize.Width, height);
        public void SetMinimumSize(int width, int height) => _minimumSize = new Dimensions(width, height);

        public Dimensions GetMinimumSize() => _minimumSize;

        public void SetMaximumWidth(int width) => SetMaximumSize(width, _maximumSize.Height);
        public void SetMaximumHeight(int height) => SetMaximumSize(_maximumSize.Width, height);

        public void SetMaximumSize(int width, int height) => _maximumSize = new Dimensions(width, height);
        public Dimensions GetMaximumSize() => _maximumSize;
        
        protected void SetComponentState(ComponentState state)
        {
            if (state == ComponentState.Disabled)
                SetEnabled(false);
            else
                _state = state;
        }

        protected ComponentState getComponentState() => _state;

        private void ApplyToChildren(Action<AbstractComponent> action)
        {
            foreach (AbstractComponent child in GetChildren())
                action.Invoke(child);
        }
    }
}