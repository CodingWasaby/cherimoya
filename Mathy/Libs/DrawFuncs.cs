using Mathy.Model.Draw;
using Mathy.Planning;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mathy.Maths;
using MathNet.Numerics.Statistics;
using MathNet.Numerics.Distributions;

namespace Mathy.Libs
{
    public class DrawFuncs
    {
        /// <summary>
        ///  HK
        ///  每行是一组，每列为一个柱
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="H"></param>
        /// <param name="L"></param>
        /// <param name="ec"></param>
        public static void Draw_HK(Matrix matrix, double H, double L, object ec)
        {
            EvaluationContext e = ec as EvaluationContext;
            matrix.SetDecimalDigitCount(e.Settings.DecimalDigitCount);
            e.SetValueAcrossSteps("Draw_HK" + Guid.NewGuid().ToString().Replace("-", ""), new HK
            {
                Matrix = matrix,
                H = H,
                L = L
            });
        }

        /// <summary> 
        ///  比对
        /// 每行为一个点，列1 mean，列2 standardDeviation
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="ec"></param>
        public static void Draw_Comparison(Matrix matrix, object ec)
        {
            EvaluationContext e = ec as EvaluationContext;
            var com = new Comparison();
            com.Datas = new List<string>();
            foreach (var r in matrix.Rows)
            {
                com.Datas.Add(r[0] + ":" + r[1]);
            }
            e.SetValueAcrossSteps("Draw_Comparison" + Guid.NewGuid().ToString().Replace("-", ""), com);
        }

        /// <summary>
        /// 均值
        /// 数组
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="ec"></param>
        public static void Draw_MeanValue(double[] datas, object ec)
        {
            EvaluationContext e = ec as EvaluationContext;
            datas = (datas as double[]).Where(j => !double.IsNaN(j)).Select(j => Math.Round(j, e.Settings.DecimalDigitCount)).ToArray();
            double mean = datas.Mean();
            double standardDeviation = datas.StandardDeviation();
            e.SetValueAcrossSteps("Draw_MeanValue" + Guid.NewGuid().ToString().Replace("-", ""), new MeanValue
            {
                Datas = string.Join(",", datas),
                H = Math.Round(mean + standardDeviation, e.Settings.DecimalDigitCount),
                L = Math.Round(mean - standardDeviation, e.Settings.DecimalDigitCount),
                M = Math.Round(mean, e.Settings.DecimalDigitCount),
            });
        }


        /// <summary>
        /// 线性回归
        /// 数组
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="ec"></param>
        public static void Draw_LinearRegression(double[] datas, object ec)
        {
            double k = 0, b = 0;
            double sumX = 0, sumY = 0;
            double avgX = 0, avgY = 0;
            int i = 1;
            foreach (var v in datas)
            {
                sumX += i;
                sumY += v;
                i++;
            }
            avgX = sumX / (datas.Count());
            avgY = sumY / (datas.Count());

            //sumA=(x-avgX)(y-avgY)的和 sumB=(x-avgX)平方
            double sumA = 0, sumB = 0;
            i = 1;
            foreach (var v in datas)
            {
                sumA += (i - avgX) * (v - avgY);
                sumB += (i - avgX) * (i - avgX);
                i++;
            }
            k = sumA / (sumB + 0.0);
            b = avgY - k * avgX;

            EvaluationContext e = ec as EvaluationContext;
            LinearRegression line = new LinearRegression()
            {
                Datas = (datas as double[]).Where(j => !double.IsNaN(j)).Select(j => Math.Round(j, e.Settings.DecimalDigitCount)).ToArray(),
                K = Math.Round(k, e.Settings.DecimalDigitCount),
                B = Math.Round(b, e.Settings.DecimalDigitCount)
            };
            e.SetValueAcrossSteps("Draw_LinearRegression" + Guid.NewGuid().ToString().Replace("-", ""), line);
        }


        /// <summary>
        ///  直方图
        /// 每行为一条线。第一条线有阴影
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="ec"></param>
        public static void Draw_Histogram(Matrix matrix, object ec)
        {
            EvaluationContext e = ec as EvaluationContext;
            var h = new Model.Draw.Histogram();
            h.Datas = new List<string[]>();
            foreach (var n in matrix.Rows)
            {
                var cn = n.Count();
                cn = cn < 20 ? 20 : cn;
                //cn = cn > 200 ? 200 : cn;
                var temp = n.OrderBy(m => m).ToArray();
                double mean = temp.Mean();
                double standardDeviation = temp.StandardDeviation();
                double step = (temp.Max() - temp.Min()) / temp.Count();
                var pdfs = new List<string>();
                for (var i = -cn / 2; i <= cn / 2; i++)
                {
                    double p = Normal.PDF(mean, standardDeviation, mean + step * i);
                    pdfs.Add(Math.Round(mean + step * i, e.Settings.DecimalDigitCount) + ":" + Math.Round(p, e.Settings.DecimalDigitCount));
                }
                h.Datas.Add(pdfs.ToArray());
            }
            e.SetValueAcrossSteps("Draw_Histogram" + Guid.NewGuid().ToString().Replace("-", ""), h);
        }

        /// <summary>
        ///  直方图
        /// 每行为一条线。第一条线有阴影
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="ec"></param>
        public static Model.Draw.Histogram Draw_Histogram(List<double[]> datas, int dit)
        {
            var h = new Model.Draw.Histogram();
            h.Datas = new List<string[]>();
            foreach (var n in datas)
            {
                var cn = n.Count();
                cn = cn < 20 ? 20 : cn;
                //cn = cn > 200 ? 200 : cn;
                double mean = n.Mean();
                double standardDeviation = n.StandardDeviation();
                double step = (n.Max() - n.Min()) / n.Count();
                var pdfs = new List<string>();
                for (var i = -cn / 2; i <= cn / 2; i++)
                {
                    double p = Normal.PDF(mean, standardDeviation, mean + step * i);
                    pdfs.Add(Math.Round(mean + step * i, dit) + ":" + Math.Round(p, dit));
                }
                h.Datas.Add(pdfs.ToArray());
            }
            return h;
        }
    }
}