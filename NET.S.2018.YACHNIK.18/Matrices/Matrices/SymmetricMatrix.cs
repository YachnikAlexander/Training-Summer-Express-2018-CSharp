﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices
{
    public class SymmetricMatrix<T> : IMatrix<T> 
                             where T : IEquatable<T>
    {
        private T[] matrix;
        public int Dimension { get; private set; }

        public SymmetricMatrix(T[] mtx, int dimension)
        {
            if(mtx == null)
            {
                throw new ArgumentNullException(nameof(mtx));
            }

            this.matrix = mtx;
            Dimension = dimension;
        }

        public SymmetricMatrix(T[,] mtx, int dimension)
        {
            if (mtx == null)
            {
                throw new ArgumentNullException(nameof(mtx));
            }
            int length = mtx.Length * mtx.Length / 2 + mtx.Length - mtx.Length / 2;
            matrix = new T[length];

            int counter = 0; 
            for(int i = 0; i < mtx.Length; i++)
            {
                for(int j = i; j < mtx.Length; j++)
                {
                    matrix[counter++] = mtx[i, j];
                }
            }

            Dimension = dimension;
        }

        public T[,] GetMatrix()
        {
            T[,] mtx = new T[Dimension, Dimension];

            int counter = 0; 
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = i; j < Dimension; j++)
                {
                    mtx[i, j] = matrix[counter];
                    mtx[j, i] = matrix[counter];
                    counter++;
                }
            }

            return mtx;
        }

        public bool IsEqual(T[,] mtx)
        {
            T[,] matrix = this.GetMatrix();
            bool check = true;
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    if (!matrix[i, j].Equals(mtx[i, j]))
                    {
                        check = false;
                        break;
                    }
                }
            }
            return check;
        }

        public static SymmetricMatrix<T> operator +(SymmetricMatrix<T> lhs, SymmetricMatrix<T> rhs)
        {
            T[,] lhsMtx = lhs.GetMatrix();
            T[,] rhsMtx = rhs.GetMatrix();

            for (int i = 0; i < lhs.Dimension; i++)
            {
                for (int j = 0; j < lhs.Dimension; j++)
                {
                    lhsMtx[i, j] += (dynamic)lhsMtx[i, j] + (dynamic)rhsMtx[i, j];
                }
            }
            return new SymmetricMatrix<T>(lhsMtx, (int)Math.Sqrt(lhsMtx.Length));
        }
    }
}
