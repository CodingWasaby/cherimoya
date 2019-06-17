using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mathy.Libs
{
    /// <summary>
    /// 分布类 常用分布放在一个类里 方便调用和扩展
    /// </summary>
    public class Distribution
    {
        // 连续分布
        private Normal normal; // 正态分布
        private ContinuousUniform continuousUniform; // 均匀分布
        private Triangular triangular; // 三角分布
        private StudentT studentT; // T分布
        private MathNet.Numerics.Distributions.Bernoulli bernoulli;
        // 离散分布
        private DiscreteUniform discreteUniform; // 离散均匀分布


        private String DistributionName;

        public Distribution(String DistributionName, params double[] vs) // 构造函数 根据名字初始化分布
        {
            switch (DistributionName)
            {
                case "Normal":
                    normal = new Normal(vs[0], vs[1]); // double mean, double stddev
                    break;
                case "ContinuousUniform":
                    continuousUniform = new ContinuousUniform(vs[0], vs[1]); // int lower, int upper
                    break;
                case "Triangular":
                    triangular = new Triangular(vs[0], vs[1], vs[2]); //double lower, double upper, double mode  (lower ≤ mode ≤ upper)
                    break;
                case "StudentT":
                    studentT = new StudentT(vs[0], vs[1], vs[2]);//double location, double scale, double freedom
                    break;
                case "DiscreteUniform":
                    discreteUniform = new DiscreteUniform((int)vs[0], (int)vs[1]); // int lower, int upper
                    break;
            }
            this.DistributionName = DistributionName;
        }

        public double getSample() // 获取当前分布样本
        {
            double ret = 0;
            switch (DistributionName)
            {
                case "Normal":
                    ret = normal.Sample(); break;
                case "ContinuousUniform":
                    ret = continuousUniform.Sample(); break;
                case "Triangular":
                    ret = triangular.Sample(); break;
                case "StudentT":
                    ret = studentT.Sample(); break;
                case "DiscreteUniform":
                    ret = discreteUniform.Sample(); break;
            }
            return ret;
        }

        public double[] getSamples(int num) // 获取指定个数的样本
        {
            double[] ret = new double[num];
            int[] ret_int = new int[num];
            switch (DistributionName)
            {
                case "Normal":
                    normal.Samples(ret);
                    break;
                case "ContinuousUniform":
                    continuousUniform.Samples(ret);
                    break;
                case "Triangular":
                    triangular.Samples(ret);
                    break;
                case "StudentT":
                    studentT.Samples(ret);
                    break;
                case "DiscreteUniform":
                    discreteUniform.Samples(ret_int);
                    for (int i = 0; i < num; i++)
                    {
                        ret[i] = ret_int[i];
                    }
                    break;
            }
            return ret;
        }

        public double[] getDiscreteSection()
        { // 获取离散分布的区间
            double[] ret = new double[] { 0, 0 };
            switch (DistributionName)
            {
                case "DiscreteUniform":
                    ret = new double[] { discreteUniform.LowerBound, discreteUniform.UpperBound }; break;
            }
            return ret;
        }
    }
}