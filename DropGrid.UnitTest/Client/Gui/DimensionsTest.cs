using System;
using DropGrid.Client.Graphics.Gui;
using NUnit.Framework;

namespace DropGrid.UnitTest.Client.Gui
{
    public class DimensionsTest
    {
        [Test]
        public void TestDimensions_GreaterThan_IsCorrect1()
        {
            Dimensions d1 = new Dimensions(10, 10);
            Dimensions d2 = new Dimensions(10, 11);
            
            Assert.IsTrue(d2 > d1);
        }
        
        [Test]
        public void TestDimensions_GreaterThan_IsCorrect2()
        {
            Dimensions d1 = new Dimensions(10, 10);
            Dimensions d2 = new Dimensions(11, 10);
            
            Assert.IsTrue(d2 > d1);
        }

        [Test]
        public void TestDimensions_LessThan_IsCorrect1()
        {
            Dimensions d1 = new Dimensions(10, 10);
            Dimensions d2 = new Dimensions(10, 11);
            
            Assert.IsTrue(d1 < d2);
        }
        
        [Test]
        public void TestDimensions_LessThan_IsCorrect2()
        {
            Dimensions d1 = new Dimensions(10, 10);
            Dimensions d2 = new Dimensions(11, 10);
            
            Assert.IsTrue(d1 < d2);
        }

        [Test]
        public void TestDimensions_Equality_IsCorrect()
        {
            Dimensions d1 = new Dimensions(10, 10);
            Dimensions d2 = new Dimensions(10, 10);
            
            Assert.IsTrue(d1 == d2);
        }
        
        [Test]
        public void TestDimensions_Equality_IsCorrect2()
        {
            Dimensions d1 = new Dimensions(10, 10);
            Dimensions d2 = new Dimensions(10, 10);
            
            Assert.IsTrue(d1.Equals(d2));
        }
        
        [Test]
        public void TestDimensions_Inequality_IsCorrect()
        {
            Dimensions d1 = new Dimensions(10, 10);
            Dimensions d2 = new Dimensions(11, 10);
            
            Assert.IsTrue(d1 != d2);
        }

        [Test]
        public void TestDimensions_CreateWithNegativeWidth_Fails()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws(typeof(ArgumentException), delegate { new Dimensions(-1, 0); });
        }
        
        [Test]
        public void TestDimensions_CreateWithNegativeHeight_Fails()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws(typeof(ArgumentException), delegate { new Dimensions(0, -1); });
        }

        [Test]
        public void TestDimensions_IsSet_HasCorrectBehaviour1()
        {
            Dimensions d1 = new Dimensions(0, 0);
            
            Assert.False(d1.IsSet());
        }
        
        [Test]
        public void TestDimensions_IsSet_HasCorrectBehaviour2()
        {
            Dimensions d1 = new Dimensions(1, 1);
            
            Assert.True(d1.IsSet());
        }
    }
}