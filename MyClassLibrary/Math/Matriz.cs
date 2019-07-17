using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace MyClassLibrary.Math
{
    /// <summary>
    /// Define uma matriz e suas operações.
    /// </summary>
    public class Matriz
    {
        private double[,] _elementos;
        private int _linhas;
        private int _colunas;
        public Matriz()
        {
        }
        public Matriz(int linhas, int colunas) : this()
        {
            _linhas = linhas;
            _colunas = colunas;
            this._elementos = new double[linhas, colunas];
        }
        /// <summary>
        /// Retorna o elemento na posição dada.
        /// </summary>
        public double this[int i, int j]
        {
            get { return this._elementos[i, j]; }
            set { this._elementos[i, j] = value; }
        }
        /// <summary>
        /// Retorna a coluna como um array.
        /// </summary>
        public double[] this[int j]
        {
            get { return RetornaColuna(j); }
        }
        public int Linhas
        {
            get { return _linhas; }
            set
            {
                _linhas = value;
                if (_linhas > 0 && _colunas > 0 )
                    this._elementos = new double[_linhas, _colunas];
            }
        }
        public int Colunas
        {
            get { return _colunas; }
            set
            {
                _colunas = value;
                if(_linhas > 0 && _colunas > 0)
                    this._elementos = new double[_linhas, _colunas];
            }
        }
        /// <summary>
        /// Devolve o número máximo de elementos de uma matriz.
        /// </summary>
        public int Count
        {
            get { return _linhas * _colunas; }
        }
        /// <summary>
        /// Devolve a matriz com cada elemento multiplicado por um escalar x.
        /// </summary>
        public Matriz MultiplicaEscalar(double x)
        {
            Matriz multiplicaEscalar = new Matriz(Linhas, Colunas);
            InterarMatriz(this, (i, j) => multiplicaEscalar[i, j] = this[i, j] * x);
            return multiplicaEscalar;
        }
        /// <summary>
        /// Devolve a soma de duas matrizes, se possível.
        /// </summary>
        public Matriz Soma(Matriz b)
        {
            Matriz matrizSoma = null;
            if(this.Linhas == b.Linhas && this.Colunas == b.Colunas)
            {
                matrizSoma = new Matriz(Linhas, Colunas);
                InterarMatriz(this, (i, j) => matrizSoma[i, j] = this[i, j] * b[i, j]);
            }
            return matrizSoma;
        }
        /// <summary>
        /// Devolve a subtração de duas matrizes, se possível.
        /// </summary>
        public Matriz Subtracao(Matriz b)
        {
            return Soma(b.Oposto());
        }
        /// <summary>
        /// Devolve a multiplicação da matriz pela matriz b, se isso for possível.
        /// </summary>
        public Matriz Multiplicacao(Matriz b)
        {
            Matriz multiplicacao = null;
            if(Colunas == b.Linhas)
            {
                multiplicacao = new Matriz(Linhas, b.Colunas);
                InterarMatriz(this, (i, j) => multiplicacao[i, j] = this[i, j] * b[i, j]);
            }
            return multiplicacao;
        }
        /// <summary>
        /// Devolve a matriz oposta. Todos os elementos multiplicados por -1.
        /// </summary>
        public Matriz Oposto()
        {
            return MultiplicaEscalar(-1);
        }
        /// <summary>
        /// Devolve a Transposta da matriz.
        /// </summary>
        public Matriz Transposta()
        {
            Matriz transp = new Matriz(Colunas, Linhas);
            InterarMatriz(this, (i, j) => transp[i, j] = this[j, i]);
            return transp;
        }
        /// <summary>
        /// Matriz obtida pela supressão da linha k e da coluna l.
        /// </summary>
        public Matriz Menor(int k, int l)
        {
            Matriz menor = null;
            if (Linhas > 1 && Colunas > 1)
            {
                menor = new Matriz(Linhas - 1, Colunas - 1);
                int i, j, p, q;
                for (i = 0, p = 0; i < Linhas; i++)
                    if(i != k)
                    {
                        for(j = 0, q = 0; j < Colunas; j++)
                            if(j != l)
                            {
                                menor[p, q] = this[i, j];
                                q++;
                            }
                        p++;
                    }
            }
            return menor;
        }
        /// <summary>
        /// Complemento algébrico, apenas para matrizes quadradas
        /// </summary>
        public double Cal(int k, int l)
        {
            return Pow(-1, k + l) * Menor(k, l).Determinante();
        }
        /// <summary>
        /// Devolve a matriz de complementos algébricos, a matriz deve ser quadrada.
        /// </summary>
        public Matriz MatrizCal()
        {
            Matriz mCal = new Matriz(Linhas, Colunas);
            InterarMatriz(this, (i, j) => mCal[i, j] = this.Cal(i, j));
            return mCal;
        }
        /// <summary>
        /// Caso a matriz seja quadrada, calcula seu determinante.
        /// </summary>
        public double Determinante()
        {
            double det = 0.0;
            for (int j = 0; j < Colunas; j++)
                det += this[0, j] * Cal(0, j);
            return det;
        }
        /// <summary>
        /// Devolve a Inversa da Matriz. A matriz deve ser quadrada.
        /// </summary>
        public Matriz Inversa()
        {
            double det = Determinante();
            if(det != 0.0)
                return MatrizCal().Transposta().MultiplicaEscalar(1.0 / det);
            return null;
        }
        #region Operações de Matrizes
        public static Matriz operator +(Matriz a, Matriz b)
        {
            return a.Soma(b);
        }
        public static Matriz operator -(Matriz a, Matriz b)
        {
            return a.Subtracao(b);
        }
        public static Matriz operator *(Matriz a, Matriz b)
        {
            return a.Multiplicacao(b);
        }
        public static bool operator ==(Matriz a, Matriz b)
        {
            return Comparison(a, b);
        }
        public static bool operator !=(Matriz a, Matriz b)
        {
            return !Comparison(a, b);
        }
        #endregion
        /// <summary>
        /// Verifica se a matriz é quadrada.
        /// </summary>
        public bool Quadradra()
        {
            if (Linhas > 0 && Colunas > 0 && Linhas == Colunas)
                return true;
            return false;
        }
        private static bool Comparison(Matriz a, Matriz b)
        {
            if (a.Linhas != b.Linhas || a.Colunas != b.Colunas)
                return false;
            else
                for (int i = 0; i < a.Linhas; i++)
                    for (int j = 0; j < a.Colunas; j++)
                        if (a[i, j] != b[i, j])
                            return false;

            return true;
        }
        public override bool Equals(object obj)
        {
            if (obj is Matriz)
                return Comparison(this, (Matriz)obj);
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        private static void InterarMatriz(Matriz m, Action<int, int> action)
        {
            for (int i = 0; i < m.Linhas; i++)
                for (int j = 0; j < m.Colunas; j++)
                    action(i, j);
        }
        public double[] RetornaLinha(int i)
        {
            double[] linha = new double[this.Colunas];
            for (int j = 0; j < this.Colunas; j++)
                linha[j] = this[i, j];
            return linha;
        }
        public double[] RetornaColuna(int j)
        {
            double[] coluna = new double[this.Linhas];
            for (int i = 0; i < this.Linhas; i++)
                coluna[i] = this[i, j];
            return coluna;
        }
    }
}
