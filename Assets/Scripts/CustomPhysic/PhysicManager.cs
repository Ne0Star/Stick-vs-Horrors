using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

public class PhysicManager : MonoBehaviour
{


    [SerializeField] private HumanoidBody test;

    private void Awake()
    {

    }

    private void Update()
    {
        
    }

    /// <summary>
    /// Описывает тело целиком
    /// </summary>
    [System.Serializable]
    public struct HumanoidBody
    {
        public CustomBody голова;
        public CustomBody тело;
        public CustomBody шея;
    }

    /// <summary>
    /// Описывает одну конечность
    /// </summary>
    [System.Serializable]
    public struct CustomBody
    {
        /// <summary>
        /// Масса тела
        /// </summary>
        public float mass;
        /// <summary>
        /// Месьл соприкосновения
        /// </summary>
        public Vector2 contact;
        /// <summary>
        /// Сентер тела
        /// </summary>
        public Vector2 center;
        /// <summary>
        /// Сила
        /// </summary>
        public Vector2 velocity;
    }


    [BurstCompile]
    public struct Simulator : IJobParallelForTransform
    {

        public void Execute(int i, TransformAccess transform)
        {

        }
    }
}
