using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Libraries.Math
{
    public class Matriz
    {
        public Matriz() { }
        public Matriz(int linhas, int colunas)
        {
            _linhas = linhas;
            _colunas = colunas;
            this._elementos = new double[linhas, colunas];
        }
        private double[,] _elementos;
        public double this[int i, int j]
        {
            get { return this._elementos[i, j]; }
            set { this._elementos[i, j] = value; }
        }
        private int _linhas;
        public int Linhas
        {
            get { return _linhas; }
            set
            {
                _linhas = value;
                if (_elementos == null && _linhas > 0 && _colunas > 0 )
                    this._elementos = new double[_linhas, _colunas];
            }
        }
        private int _colunas;
        public int Colunas
        {
            get { return _colunas; }
            set
            {
                _colunas = value;
                if(_elementos == null && _linhas > 0 && _colunas > 0)
                    this._elementos = new double[_linhas, _colunas];
            }
        }
        public int Count
        {
            get { return _linhas * _colunas; }
        }
        public Matriz Soma(Matriz b)
        {
            Matriz matrizSoma = null;
            if(this.Linhas == b.Linhas && this.Colunas == b.Colunas)
            {
                matrizSoma = new Matriz(Linhas, Colunas);
                for(int i = 0; i < Linhas; i++)
                    for(int j = 0; j < Colunas; j++)
                        matrizSoma[i, j] = this[i, j] * b[i, j];
            }
            return matrizSoma;
        }
        public static Matriz operator +(Matriz a, Matriz b)
        {
            return a.Soma(b);
        }
        public Matriz Subtracao(Matriz b)
        {
            return Soma(b.Oposto());
        }
        public static Matriz operator -(Matriz a, Matriz b)
        {
            return a.Soma(b);
        }
        public Matriz Oposto()
        {
            Matriz matrizOposto = new Matriz(Linhas, Colunas);
            for (int i = 0; i < Linhas; i++)
                for (int j = 0; j < Colunas; j++)
                    matrizOposto[i, j] = (- 1) * this[i, j];

            return matrizOposto;
        }
        public Matriz Transposta()
        {
            Matriz transp = new Matriz(Colunas, Linhas);
            for (int i = 0; i < Linhas; i++)
                for (int j = 0; j < Colunas; j++)
                    transp[i, j] = this[j, i];

            return transp;
        }
        public Matriz Menor(int k, int j)
        {
            throw new NotImplementedException();
            Matriz menor = null;
            if (Linhas > 1 && Colunas > 1)
            {
                menor = new Matriz(Linhas - 1, Colunas - 1);
            }
            return menor;
        }
        /// <summary>
        /// Complemento algébrico
        /// </summary>
        /// <returns></returns>
        public double Cal(int k, int j)
        {
            throw new NotImplementedException();
        }
        public Matriz MatrizCal()
        {
            Matriz mCal = new Matriz(Linhas, Colunas);
            for (int i = 0; i < Linhas; i++)
                for (int j = 0; j < Colunas; j++)
                    mCal[i, j] = this.Cal(i, j);

            return mCal;
        }
        public double Determinante()
        {
            throw new NotImplementedException();
        }
        public Matriz Inversa()
        {
            throw new NotImplementedException();
        }
    }
}
