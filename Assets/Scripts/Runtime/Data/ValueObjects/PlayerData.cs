using System;
using Unity.Mathematics;

namespace Runtime.Data.ValueObjects
{
    [Serializable]
    public struct PlayerData
    {
        public PlayerMovementData MovementData;
        public PlayerMeshData MeshData;
        public playerForceData ForceData;
        
        
    }
    
    [Serializable]
    public struct PlayerMovementData
    {
        public float ForwardSpeed;
        public float SidewaySpeed;
    }
    
    [Serializable]
    public struct PlayerMeshData
    {
        public float ScaleCounter;
    }
    
    [Serializable]
    public struct playerForceData
    {
        public float3 ForceParameters;
    }
}