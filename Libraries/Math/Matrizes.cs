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
        private int _linhas;
        public int Linhas
        {
            get { return _linhas; }
            set
            {
                _linhas = value;
                if (elementos == null && _linhas > 0 && _colunas > 0 )
                    this.elementos = new int[_linhas, _colunas];
            }
        }
        private int _colunas;
        public int Colunas
        {
            get { return _colunas; }
            set
            {
                _colunas = value;
                if(elementos == null && _linhas > 0 && _colunas > 0)
                    this.elementos = new int[_linhas, _colunas];
            }
        }
        public int Count
        {
            get { return _linhas * _colunas; }
        }
        public int this[int i, int j]
        {
            get { return this.elementos[i, j]; }
            set { this.elementos[i, j] = value; }
        }
        public int[,] elementos;

        public Matriz() { }
        public Matriz(int linhas, int colunas)
        {
            _linhas = linhas;
            _colunas = colunas;
            this.elementos = new int[linhas, colunas];
        }

        public Matriz Soma(Matriz b)
        {
            Matriz matrizSoma = new Matriz();
            if(this.Linhas == b.Linhas && this.Colunas == b.Colunas)
            {
                matrizSoma.Linhas = this.Linhas;
                matrizSoma.Colunas = this.Colunas;
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
    }
}
