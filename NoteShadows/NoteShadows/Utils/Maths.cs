using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NoteShadows.Utils
{
    public class Maths
    {

        public static float Vector3Distance(Vector3 p_startPos, Vector3 p_endPos) 
        {

            float l_distance = (float)(Math.Pow(p_endPos.x - p_startPos.x, 2)+ 
                Math.Pow(p_endPos.y - p_startPos.y, 2)+ 
                Math.Pow(p_endPos.z - p_startPos.z, 2));

            l_distance = (float)Math.Sqrt(l_distance);

            return Math.Abs(l_distance);
        }

    }
}
