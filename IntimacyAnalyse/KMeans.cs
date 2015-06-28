using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntimacyAnalyse
{
    class KMeans
    {
        /// <summary>
        /// Kmeans核心入口，返回类别数组
        /// </summary>
        /// <param name="rawData"></param>
        /// <param name="numClusters"></param>
        /// <returns></returns>
        public static int[] Cluster(double[][] rawData, int numClusters)
        {
            // 规范化数据集
            double[][] data = Normalized(rawData); 

            bool changed = true;
            bool success = true;

            int[] clustering = InitClustering(data.Length, numClusters, 0); 
            double[][] means = Allocate(numClusters, data[0].Length); 

            int maxCount = data.Length * 10; 
            int ct = 0;
            while (changed == true && success == true && ct < maxCount)
            {
                ++ct; 
                success = UpdateMeans(data, clustering, means);
                changed = UpdateClustering(data, clustering, means);
            }
            return clustering;
        }

        public static double[][] Normalized(double[][] rawData)
        {
         
            double[][] result = new double[rawData.Length][];
            for (int i = 0; i < rawData.Length; ++i)
            {
                result[i] = new double[rawData[i].Length];
                Array.Copy(rawData[i], result[i], rawData[i].Length);
            }

            for (int j = 0; j < result[0].Length; ++j) // each col
            {
                double colSum = 0.0;
                for (int i = 0; i < result.Length; ++i)
                    colSum += result[i][j];
                double mean = colSum / result.Length;
                double sum = 0.0;
                for (int i = 0; i < result.Length; ++i)
                    sum += (result[i][j] - mean) * (result[i][j] - mean);
                double sd = sum / result.Length;
                for (int i = 0; i < result.Length; ++i)
                    result[i][j] = (result[i][j] - mean) / sd;
            }
            return result;
        }

        /// <summary>
        /// 初始化聚类中心
        /// </summary>
        private static int[] InitClustering(int numTuples, int numClusters, int randomSeed)
        {
            Random random = new Random(randomSeed);
            int[] clustering = new int[numTuples];
            for (int i = 0; i < numClusters; ++i) 
                clustering[i] = i;
            for (int i = numClusters; i < clustering.Length; ++i)
                clustering[i] = random.Next(0, numClusters); 
            return clustering;
        }

        /// <summary>
        /// 分配新的聚类中心
        /// </summary>
        /// <param name="numClusters">聚类数</param>
        /// <param name="numColumns">数据属性列</param>
        /// <returns>初始化的新聚类中心</returns>
        private static double[][] Allocate(int numClusters, int numColumns)
        {
            double[][] result = new double[numClusters][];
            for (int k = 0; k < numClusters; ++k)
                result[k] = new double[numColumns];
            return result;
        }

        /// <summary>
        /// 更新聚类中心
        /// </summary>
        /// <param name="data">待处理数据</param>
        /// <param name="clustering">聚类标签</param>
        /// <param name="means">原聚类中心</param>
        /// <returns>更新后的聚类中心</returns>
        private static bool UpdateMeans(double[][] data, int[] clustering, double[][] means)
        {
            int numClusters = means.Length;
            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false; 

            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] = 0.0;

            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = clustering[i];
                for (int j = 0; j < data[i].Length; ++j)
                    means[cluster][j] += data[i][j];
            }

            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] /= clusterCounts[k];
            return true;
        }

        /// <summary>
        /// 更新数据分类标签
        /// </summary>
        private static bool UpdateClustering(double[][] data, int[] clustering, double[][] means)
        {
            int numClusters = means.Length;
            bool changed = false;

            int[] newClustering = new int[clustering.Length]; 
            Array.Copy(clustering, newClustering, clustering.Length);

            double[] distances = new double[numClusters]; 

            for (int i = 0; i < data.Length; ++i) 
            {
                for (int k = 0; k < numClusters; ++k)
                    distances[k] = Distance(data[i], means[k]); 

                int newClusterID = MinIndex(distances); 
                if (newClusterID != newClustering[i])
                {
                    changed = true;
                    newClustering[i] = newClusterID; // update
                }
            }

            if (changed == false)
                return false; 

            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = newClustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false; 

            Array.Copy(newClustering, clustering, newClustering.Length); // 更新
            return true; 
        }

        /// <summary>
        /// 计算元组与质心的欧几里得距离
        /// </summary>
        private static double Distance(double[] tuple, double[] mean)
        {
            double sumSquaredDiffs = 0.0;
            for (int j = 0; j < tuple.Length; ++j)
                sumSquaredDiffs += Math.Pow((tuple[j] - mean[j]), 2);
            return Math.Sqrt(sumSquaredDiffs);
        }

        private static int MinIndex(double[] distances)
        {
            int indexOfMin = 0;
            double smallDist = distances[0];
            for (int k = 0; k < distances.Length; ++k)
            {
                if (distances[k] < smallDist)
                {
                    smallDist = distances[k];
                    indexOfMin = k;
                }
            }
            return indexOfMin;
        }
        
        /// <summary>
        /// 聚类结果展示
        /// </summary>
        static void ShowData(double[][] data, int decimals, bool indices, bool newLine)
        {
            for (int i = 0; i < data.Length; ++i)
            {
                if (indices) Console.Write(i.ToString().PadLeft(3) + " ");
                for (int j = 0; j < data[i].Length; ++j)
                {
                    if (data[i][j] >= 0.0) Console.Write(" ");
                    Console.Write(data[i][j].ToString("F" + decimals) + " ");
                }
                Console.WriteLine("");
            }
            if (newLine) Console.WriteLine("");
        } // ShowData

        static void ShowVector(int[] vector, bool newLine)
        {
            for (int i = 0; i < vector.Length; ++i)
                Console.Write(vector[i] + " ");
            if (newLine) Console.WriteLine("\n");
        }

        static void ShowClustered(double[][] data, int[] clustering, int numClusters, int decimals)
        {
            for (int k = 0; k < numClusters; ++k)
            {
                Console.WriteLine("===================");
                for (int i = 0; i < data.Length; ++i)
                {
                    int clusterID = clustering[i];
                    if (clusterID != k) continue;
                    Console.Write(i.ToString().PadLeft(3) + " ");
                    for (int j = 0; j < data[i].Length; ++j)
                    {
                        if (data[i][j] >= 0.0) Console.Write(" ");
                        Console.Write(data[i][j].ToString("F" + decimals) + " ");
                    }
                    Console.WriteLine("");
                }
                Console.WriteLine("===================");
            } // k
        }

        /// <summary>
        /// 基于原始数据，分类标号和分类数计算分类的质心
        /// </summary>
        /// <param name="data"></param>
        /// <param name="clustering"></param>
        /// <param name="numClusters"></param>
        /// <returns></returns>
        public static double[][] getMeans(double[][] rawData, int[] clustering, int numClusters)
        {
            if (rawData == null) 
            {
                return null;
            }

            double[][] data = Normalized(rawData);

            double[][] means = Allocate(numClusters, data[0].Length);
            int[] clusterCounts = new int[numClusters]; 

            // 初始化聚类中心为0
            for (int k = 0; k < means.Length; k++)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] = 0.0;

            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = clustering[i];
                clusterCounts[cluster]++;
                for (int j = 0; j < data[i].Length; ++j)
                    means[cluster][j] += data[i][j]; // accumulate sum
            }

            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] /= clusterCounts[k]; // danger of div by 0
            return means;
        }
    }
}
