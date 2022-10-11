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
    /// ��������� ���� �������
    /// </summary>
    [System.Serializable]
    public struct HumanoidBody
    {
        public CustomBody ������;
        public CustomBody ����;
        public CustomBody ���;
    }

    /// <summary>
    /// ��������� ���� ����������
    /// </summary>
    [System.Serializable]
    public struct CustomBody
    {
        /// <summary>
        /// ����� ����
        /// </summary>
        public float mass;
        /// <summary>
        /// ����� ���������������
        /// </summary>
        public Vector2 contact;
        /// <summary>
        /// ������ ����
        /// </summary>
        public Vector2 center;
        /// <summary>
        /// ����
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
