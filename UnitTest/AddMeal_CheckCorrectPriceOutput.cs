using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestApplication.ViewModels;
using System.Collections.Generic;
using TestApplication;

namespace UnitTest
{
    [TestClass]
    public class AddMeal_CheckCorrectPriceOutput
    {
        [TestMethod]
        public void CheckCorrectPriceOutput()
        {
            //Unit test..test
            var addvm = new AddMealViewModel();
            
            List<IngredientModel> testList = new List<IngredientModel>();

            IngredientModel model1 = new IngredientModel("TestOne", 1, 5.99m, 1);
            IngredientModel model2 = new IngredientModel("TestTwo", 2, 2.99m, 1);
            IngredientModel model3 = new IngredientModel("TestThree", 3, 6.99m, 1);
            IngredientModel model4 = new IngredientModel("TestFour", 4, 7.00m, 1);

            testList.Add(model1);
            testList.Add(model2);
            testList.Add(model3);
            testList.Add(model4);

            Assert.AreEqual(addvm.TotalPrice(testList), 22.97m);
        }
    }
}
