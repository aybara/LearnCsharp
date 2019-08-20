using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClassLibrary.Math;

namespace UnitTests.MathUnitTests
{
    [TestClass]
    public class MatrizUnitTests
    {
        static Matriz mt1;
        static Matriz mt2;
        static Matriz mt3;
        //static Matriz mt4;
        //static Matriz mt5;
        //static Matriz mt6;
        //static Matriz mt7;
        //static Matriz mt8;
        //static Matriz mt9;
        //static Matriz mt10;
        //static Matriz mt11;
        //static Matriz mt12;
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            mt1 = new double[,] { { 1, 2 }, { 3, 4 } };
            mt2 = new double[,] { { 2, -1, 3 }, { 0, 1, 1 }, { -1, -2, 0 } };
            mt3 = new double[,] { { 3, 1, 2, 4 }, { 2, 0, 5, 1 }, { 1, -1, -2, 6 }, { -2, 3, 2, 3 } };
        }
        [ClassCleanup]
        public static void Cleanup()
        {

        }
        [TestMethod]
        public void Create()
        {
            Matriz m1 = new Matriz();
            Matriz m2 = new Matriz(1, 2);
            Matriz m3 = new Matriz(6, 4);
            Matriz m4 = new Matriz(9, 8);
            Matriz m5 = new Matriz(3, 5);

            double[,] elementos1 = { { 2, 2 }, { 3, 4 } };
            Matriz m6 = new Matriz(elementos1);
            Matriz m7 = new Matriz();
            m7 = elementos1;
            Matriz m8 = new Matriz();
            m8.SetElementos = elementos1;

            Assert.ThrowsException<NullReferenceException>(() => m1[1, 1]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => m2[1, 3]);
            Assert.AreEqual(0, m1.Count);
            Assert.AreEqual(2, m2.Count);
            Assert.AreEqual(24, m3.Count);
            Assert.AreEqual(72, m4.Count);
            Assert.AreEqual(15, m5.Count);
            Assert.AreEqual(4, m6.Count);
            Assert.AreEqual(2, m6.Linhas);
            Assert.AreEqual(3, m6[1, 0]);
            Assert.AreEqual(4, m7.Count);
            Assert.AreEqual(2, m7.Linhas);
            Assert.AreEqual(3, m7[1, 0]);
            Assert.AreEqual(4, m8.Count);
            Assert.AreEqual(2, m8.Linhas);
            Assert.AreEqual(3, m8[1, 0]);
        }
        [TestMethod]
        public void AccessElements()
        {

        }
        [TestMethod]
        public void MultiplicaEscalar()
        {
            Matriz m1 = new double[,] { { 1, 2 }, { 3, 4 } };
            Matriz m2 = new double[,] { { 4, 1, 2 }, { 8, 2, 9 } };
            Matriz m3 = new double[,] { { 1 }, { 2 }, { 3 }, { 9 } };
            Matriz m4 = new double[,] { { 4, 9, 8 } };

            Assert.AreEqual(new double[,] { { 4, 8 }, { 12, 16 } }, 4 * m1);
            Assert.AreEqual(new double[,] { { 12, 3, 6 }, { 24, 6, 27 } }, 3 * m2);
            Assert.AreEqual(new double[,] { { 10 }, { 20 }, { 30 }, { 90 } }, 10 * m3);
            Assert.AreEqual(new double[,] { { 80, 180, 160 } }, 20 * m4);
        }
        [TestMethod]
        public void SomaDeMatrizes()
        {
            Matriz m1 = new double[,] { { 0, 1 }, { 1, 0 } };
            Matriz m2 = new double[,] { { 1, 0 }, { 0, 1 } };
            Matriz m3 = new double[,] { { 1, 2, 3 }, { 1, 2, 5 } };
            Matriz m4 = new double[,] { { 2, 3, 4 }, { 5, 6, 7 } };
            Assert.AreEqual(new double[,] { { 1, 1 }, { 1, 1 } }, m1 + m2);
            Assert.AreEqual(null, m1 + m3);
            Assert.AreEqual(new double[,] { { 3, 5, 7 }, { 6, 8, 12 } }, m3 + m4);
        }
        [TestMethod]
        public void SubtracaoDeMatrizes()
        {

        }
        [TestMethod]
        public void MultiplicacaoMatrizes()
        {
            Matriz m1 = new double[,] { { 1, 2 }, { 3, 4 } };
            Matriz m2 = new double[,] { { 10 }, { 20 } };
            Matriz m1m2 = new double[,] { { 50 }, { 110 } };
            Assert.AreEqual(m1m2, m1.Multiplicacao(m2));
        }
        [TestMethod]
        public void Oposto()
        {

        }
        [TestMethod]
        public void Transposta()
        {
            Matriz tMt1 = new double[,] { { 1, 3 }, { 2, 4 } };
            Assert.AreEqual(tMt1, mt1.Transposta());
            Matriz tMt2 = new double[,] { { 2, 0, -1 }, { -1, 1, -2 }, { 3, 1, 0 } };
            Assert.AreEqual(tMt2, mt2.Transposta());
        }
        [TestMethod]
        public void Menor()
        {
            Matriz mt1Menor11 = new double[,] { { 4 } };
            Matriz mt1Menor12 = new double[,] { { 3 } };
            Matriz mt1Menor21 = new double[,] { { 2 } };
            Matriz mt1Menor22 = new double[,] { { 1 } };
            Assert.AreEqual(mt1Menor11, mt1.Menor(0, 0));
            Assert.AreEqual(mt1Menor12, mt1.Menor(0, 1));
            Assert.AreEqual(mt1Menor21, mt1.Menor(1, 0));
            Assert.AreEqual(mt1Menor22, mt1.Menor(1, 1));

            Matriz mt2Menor11 = new double[,] { { 1, 1 }, { -2, 0 } };
            Matriz mt2Menor12 = new double[,] { { 0, 1 }, { -1, 0 } };
            Matriz mt2Menor13 = new double[,] { { 0, 1 }, { -1, -2 } };
            Matriz mt2Menor21 = new double[,] { { -1, 3 }, { -2, 0 } };
            Matriz mt2Menor22 = new double[,] { { 2, 3 }, { -1, 0 } };
            Matriz mt2Menor23 = new double[,] { { 2, -1 }, { -1, -2 } };
            Matriz mt2Menor31 = new double[,] { { -1, 3 }, { 1, 1 } };
            Matriz mt2Menor32 = new double[,] { { 2, 3 }, { 0, 1 } };
            Matriz mt2Menor33 = new double[,] { { 2, -1 }, { 0, 1 } };
            Assert.AreEqual(mt2Menor11, mt2.Menor(0, 0));
            Assert.AreEqual(mt2Menor12, mt2.Menor(0, 1));
            Assert.AreEqual(mt2Menor13, mt2.Menor(0, 2));
            Assert.AreEqual(mt2Menor21, mt2.Menor(1, 0));
            Assert.AreEqual(mt2Menor22, mt2.Menor(1, 1));
            Assert.AreEqual(mt2Menor23, mt2.Menor(1, 2));
            Assert.AreEqual(mt2Menor31, mt2.Menor(2, 0));
            Assert.AreEqual(mt2Menor32, mt2.Menor(2, 1));
            Assert.AreEqual(mt2Menor33, mt2.Menor(2, 2));
        }
        [TestMethod]
        public void ComplementoAlgebrico()
        {
            Assert.AreEqual(4, mt1.Cal(0, 0));
            Assert.AreEqual(-3, mt1.Cal(0, 1));
            Assert.AreEqual(-2, mt1.Cal(1, 0));
            Assert.AreEqual(1, mt1.Cal(1, 1));

            Assert.AreEqual(2, mt2.Cal(0, 0));
            Assert.AreEqual(-1, mt2.Cal(0, 1));
            Assert.AreEqual(1, mt2.Cal(0, 2));
            Assert.AreEqual(-6, mt2.Cal(1, 0));
            Assert.AreEqual(3, mt2.Cal(1, 1));
            Assert.AreEqual(5, mt2.Cal(1, 2));
            Assert.AreEqual(-4, mt2.Cal(2, 0));
            Assert.AreEqual(-2, mt2.Cal(2, 1));
            Assert.AreEqual(2, mt2.Cal(2, 2));
        }
        [TestMethod]
        public void MatrizComplementosAlgebricos()
        {
            Matriz calMt1 = new double[,] { { 4, -3 }, { -2, 1 } };
            Assert.AreEqual(calMt1, mt1.MatrizCal());
            Matriz calMt2 = new double[,] { { 2, -1, 1 }, { -6, 3, 5 }, { -4, -2, 2 } };
            Assert.AreEqual(calMt2, mt2.MatrizCal());
            Matriz calMt3 = new double[,] { { 109, 113, -41, -13 }, { -40, -92, 74, 16 }, { -41, -79, 7, 47 }, { -50, 38, 16, 20 } };
            Assert.AreEqual(calMt3, mt3.MatrizCal());
        }
        [TestMethod]
        public void Determinante()
        {
            Matriz matrizDimensao1 = new double[,] { { 2 } };
            Assert.AreEqual(2, matrizDimensao1.Determinante());
            Assert.AreEqual(-2, mt1.Determinante());
            Assert.AreEqual(8, mt2.Determinante());
            Assert.AreEqual(306, mt3.Determinante());
        }
        [TestMethod]
        public void Inversa()
        {
            Matriz inversaMt1 = new double[,] { { -4, 2 }, { 3, -1 } };
            inversaMt1 = inversaMt1.MultiplicaEscalar(0.5);
            Assert.AreEqual(inversaMt1, mt1.Inversa());
            Matriz inversaMt2 = new double[,] { { 2, -6, -4 }, { -1, 3, -2 }, { 1, 5, 2 } };
            inversaMt2 = inversaMt2.MultiplicaEscalar(1.0 / 8);
            Assert.AreEqual(inversaMt2, mt2.Inversa());
        }
        [TestMethod]
        public void SeMatrizEQuadrada()
        {

        }
        [TestMethod]
        public void Metodo_Equals()
        {

        }
        [TestMethod]
        public void RetornaLinha()
        {

        }
        [TestMethod]
        public void RetornaColuna()
        {

        }
    }
}
