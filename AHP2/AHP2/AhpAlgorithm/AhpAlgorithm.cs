using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AHP2.Models;

namespace AHP2.AhpAlgorithm
{
    public class AhpAlgorithm
    {
        /*
        public double[,] StringToDouble(string stringArray)
        {
            string[] records = stringArray.Split('|'); //NVARCHAR format in data base, example: "1,0000:7,0000:2,0000|0.1400:1,0000:3,0000|0,5000:0,3300:1,0000"
            int n = records.Length;
            double[,] doubleArray = new double[n, n];
            
            for (int i = 0; i < n; i++)
            {
                string[] columns = records[i].Split(':');
                for (int j = 0; j < n; j++)
                {
                    try
                    {
                        doubleArray[i, j] = double.Parse(columns[j]);
                    }
                    catch
                    {
                        return null;
                    }
                }
            }

            return doubleArray;
        }

        public string DoubleToString(double[,] doubleArray)
        {
            int n = doubleArray.GetLength(0);
            string stringArray = "";

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    stringArray += doubleArray[i, j].ToString("0.0000");
                    if (j != n - 1)
                        stringArray += ":";
                }
                if (i != n - 1)
                    stringArray += "|";
            }

            return stringArray;
        }*/
        public List<CriterionsComparableViewModels> SelectComparable(List<Criterion> criterions)
        {
            List<CriterionsComparableViewModels> criterionsComparableVM = new List<CriterionsComparableViewModels>();
            int n = criterions.Count;

            for(int i = 0; i < n; i++)
                for(int j = 0; j < n; j++)
                {
                    if(j > i)
                    {
                        criterionsComparableVM.Add(new CriterionsComparableViewModels
                        {
                            Criterion1 = criterions[i],
                            Criterion2 = criterions[j],
                            Rate = "1"
                        });
                    }
                }
            return criterionsComparableVM;
        }

        public double[,] CriterionsComparableToDouble(List<CriterionsComparableViewModels> cCVMList)
        {
            int listComparables = cCVMList.Count;
            int n = GetN(listComparables);
            double[,] list = new double[n, n];

            List<CriterionsComparableViewModels> compaarableList = cCVMList;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                        list[i, j] = 1;
                    else if (j > i && compaarableList.Count != 0)
                    {
                        var a = RateStringToDouble(compaarableList[0].Rate);
                        list[i, j] = a;
                        list[j, i] = 1 / a;
                        compaarableList.RemoveAt(0);
                    }
                }
            }

            return list;
        }

        private double RateStringToDouble(string rate)
        {
            if (rate.Length == 1)
                return double.Parse(rate);
            else
            {
                string[] str = rate.Split('/');
                return (double.Parse(str[0].ToString()) / double.Parse(str[1].ToString()));
            }
        }

        private int GetN(int listComparables)
        {
            int n = 0;

            while ((n * n - n) != 2 * listComparables) // count of comparables = (n * n - n)/2
            {
                n++;
            }

            return n;
        }

        public double[,] NormalizedMatrix(double[,] ratingMatrix)
        {
            int n = ratingMatrix.GetLength(0);
            double[,] normalizedMatrix = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                double columnSum = 0;
                for (int j = 0; j < n; j++)
                {
                    columnSum += ratingMatrix[j, i];
                }
                for (int j = 0; j < n; j++)
                {
                    normalizedMatrix[j, i] = ratingMatrix[j, i] / columnSum;
                }
            }

            return normalizedMatrix;
        }

        public double[] LocalWeight(double[,] normalizedMatrix)
        {
            int n = normalizedMatrix.GetLength(0);
            double[] localWeights = new double[n];

            for (int i = 0; i < n; i++)
            {
                double d = 0;
                for (int j = 0; j < n; j++)
                {
                    d += normalizedMatrix[i, j];
                }
                localWeights[i] = d / n;
            }

            return localWeights;
        }

        public double[] GlobalWeight(double[] localWeights, double localWeightParent)
        {
            int n = localWeights.Length;
            double[] globalWegiht = new double[n];

            for (int i = 0; i < n; i++)
            {
                globalWegiht[i] = localWeights[i] * localWeightParent;
            }

            return globalWegiht;
        }

        public double CrRate(double[,] ratingMatrix)
        {
            int n = ratingMatrix.GetLength(0);
            double[] localWeigths = LocalWeight(NormalizedMatrix(ratingMatrix));
            double lambda = 0;

            for (int i = 0; i < n; i++)
            {
                double columnSum = 0;
                for (int j = 0; j < n; j++)
                {
                    columnSum += ratingMatrix[j, i];
                }
                lambda += (columnSum * localWeigths[i]);
            }

            return ((lambda - n) / (n - 1)) / GetRI(n);
        }

        private double GetRI(int n)
        {
            switch (n)
            {
                case 2: return 0;
                case 3: return 0.52;
                case 4: return 0.89;
                case 5: return 1.11;
                case 6: return 1.25;
                case 7: return 1.35;
                case 8: return 1.40;
                case 9: return 1.49;
                case 10: return 1.52;
                default: return 0;
            }
        }
    }
}