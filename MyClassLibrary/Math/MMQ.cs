﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClassLibrary.Math;
using static System.Math;

namespace MyClassLibrary.Math
{
    /// <summary>
    /// Método dos Mínimos Quadrados
    /// </summary>
    public class MMQ
    {
        private Matriz M;
        private Matriz B;
        public Parameter[] Parameters { get; private set; }
        public ExperimentalPoint[] Dados { get; }
        public IFunction[] Functions { get; }
        public MMQ(ExperimentalPoint[] dados, IFunction[] functions)
        {
            Dados = dados;
            Functions = functions;
            Parameters = new Parameter[Functions.Length];
        }

        public Parameter[] FitFunction()
        {
            M = new Matriz(Functions.Length, Functions.Length);
            B = new Matriz(Functions.Length, 1);
            ComputeSums();
            ComputeParameters();
            return Parameters;
        }

        private void ComputeSums()
        {
            for (int i = 0; i < Functions.Length; i++)
                for (int j = i; j < Functions.Length; j++)
                {
                    foreach (var point in Dados)
                        M[i, j] += Functions[i].Value(point.X) * Functions[j].Value(point.X) / Pow(point.Error, 2);
                    M[j, i] = M[i, j];
                }

            for (int i = 0; i < Functions.Length; i++)
                foreach (var point in Dados)
                    B[i, 0] += Functions[i].Value(point.X) * point.Y / Pow(point.Error, 2);
        }
        private void ComputeParameters()
        {
            Matriz inversa = M.Inversa();
            Matriz cramer = inversa * B;
            for(int i = 0; i < Parameters.Length; i++)
            {
                Parameters[i].Value = cramer[0, i];
                Parameters[i].Error = inversa[i, i];
            }
        }
    }
}
