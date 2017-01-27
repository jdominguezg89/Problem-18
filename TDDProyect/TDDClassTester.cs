using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SumaRutaValorMaximoNameSpace;

namespace TDDProyectNameSpace
{
    [TestClass]
    public class TDDClassTester
    {
        [TestMethod]
        public void TestMethod1()
        {
            SumaRutaValorMaximoClass sumaValorMaximo = new SumaRutaValorMaximoClass();
            int sumaMaxima = sumaValorMaximo.SumaRutaValorMaximoAction();

            //Este es el valor que se espera que devuelva el argoritmo 1074
            Assert.AreEqual(7273, sumaMaxima);
        }
    }
}
