using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathy.Visualization.Expressions.Nodes
{
    public abstract class Expression : Node
    {
        public float FindMaxTop()
        {
            float[] height = new float[] { 0 };
            FindMaxTop(height);

            return height[0];
        }

        public float FindMaxBottom()
        {
            float[] height = new float[] { 0 };
            FindMaxBottom(height);

            return height[0];
        }


        public virtual void FindMaxTop(float[] height)
        {
            if (height[0] < DesiredHeight / 2)
            {
                height[0] = DesiredHeight / 2;
            }
        }

        public virtual void FindMaxBottom(float[] height)
        {
            if (height[0] < DesiredHeight / 2)
            {
                height[0] = DesiredHeight / 2;
            }
        }
    }
}
