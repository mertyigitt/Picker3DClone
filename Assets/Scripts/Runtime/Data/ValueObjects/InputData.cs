using System;
using UnityEngine;

namespace Runtime.Data.ValueObjects
{
    [Serializable]
    public struct InputData
    {
        public float HorizontalInputSpeed; // hareket ve parmak hizi odakli limitasyon
        public Vector2 ClampValues; //Fiziksel limitasyon
        public float ClampSpeed; //yumusatma hizi
        
        
        
        
    }
}