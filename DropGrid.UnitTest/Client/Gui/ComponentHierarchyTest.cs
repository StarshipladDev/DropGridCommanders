using System;
using DropGrid.Client;
using DropGrid.Client.Graphics;
using DropGrid.Client.Graphics.Gui;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace DropGrid.UnitTest.Client.Gui
{
    public class ComponentHierarchyTest
    {
        private class MockComponent : AbstractComponent {
            public override void Render(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime)
            {
                throw new NotImplementedException();
            }

            public override void Update(GameEngine engine, GameTime gameTime)
            {
                throw new NotImplementedException();
            }
        }
        
        private class MockContainer : AbstractContainer {
            public override void Render(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime)
            {
                throw new NotImplementedException();
            }

            public override void Update(GameEngine engine, GameTime gameTime)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void TestHierarchy_AddChildToParent_Works()
        {
            MockComponent parent = new MockComponent();
            MockComponent child = new MockComponent();
            
            parent.AddChildren(child);
            
            Assert.IsTrue(child.GetParent() == parent);
            Assert.AreEqual(1, parent.GetChildren().Count);
        }

        [Test]
        public void TestHierarchy_AddSelfAsChild_Fails()
        {
            MockComponent self = new MockComponent();
            
            Assert.Throws(typeof(ArgumentException), delegate { self.AddChildren(self); });
        }

        [Test]
        public void TestHierarchy_AddChildAlreadyWithAnotherParent_Fails()
        {
            MockComponent topLevelParent = new MockComponent();
            MockComponent child1 = new MockComponent();
            MockComponent child2 = new MockComponent();
            
            topLevelParent.AddChildren(child1);
            topLevelParent.AddChildren(child2);
            
            Assert.Throws(typeof(ArgumentException), delegate { child1.AddChildren(child2); });
        }

        [Test]
        public void TestHierarchy_AddContainerToAnotherContainer_Works()
        {
            MockContainer c1 = new MockContainer();
            MockContainer c2 = new MockContainer();
            
            c1.AddChildren(c2);
            
            Assert.IsTrue(c2.GetParent() == c1);
        }

        [Test]
        public void TestHierarchy_AddContainerAsChildToNonContainerComponent_Fails()
        {
            MockComponent parent = new MockComponent();
            MockContainer childContainer = new MockContainer();

            Assert.Throws(typeof(ArgumentException), delegate { parent.AddChildren(childContainer); });
        }

        [Test]
        public void TestStateDependency_SetParentDisabled_ChildrenAlsoDisabled()
        {
            MockComponent parent = new MockComponent();
            for (int i = 0; i < 10; ++i)
            {
                MockComponent child = new MockComponent();
                parent.AddChildren(child);
            }

            parent.SetEnabled(false);

            foreach (AbstractComponent child in parent.GetChildren())
            {
                Assert.IsFalse(child.IsEnabled());
            }
        }
    }
}