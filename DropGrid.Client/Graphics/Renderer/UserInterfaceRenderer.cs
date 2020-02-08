using System;
using System.Collections.Generic;
using DropGrid.Client.Graphics.Gui;
using JetBrains.Annotations;

namespace DropGrid.MacOS.Graphics.Renderer
{
    public static class UserInterfaceRenderer
    {
        private static List<AbstractComponent> _interfaceElements = new List<AbstractComponent>();

        public static bool HasModalComponent()
        {
            // TODO: Check for modal dialogs in the element stack
            return false;
        }

        public static void AddComponent([NotNull] AbstractComponent component)
        {
            _interfaceElements.Add(component);
        }

        /// <summary>
        /// Removes a component from the renderer. If the component is a container, or contains child components, they<br />
        /// will also be removed recursively.
        /// </summary>
        /// <param name="component">Component to remove.</param>
        public static void RemoveComponent([NotNull] AbstractComponent component)
        {
            // Assume all circular dependencies has been resolved, there should be no
            // stack overflow here from infinite recursion. 
            List<AbstractComponent> children = component.GetChildren();
            foreach (AbstractComponent child in children) 
                RemoveComponent(child);

            int index = _interfaceElements.IndexOf(component);
            if (index != -1)
                _interfaceElements.Remove(component);
        }
    }
}