﻿// Copyright (c) Sean Nowotny

using UnityEngine;

namespace Logic.BurstedAosDODJob
{
    public static class RenderSystem
    {
        private static Transform[] transformPool = new Transform[1000]; // TODO: Replace hard-coded length
        private static MeshRenderer[] meshPool = new MeshRenderer[1000]; // TODO: Replace hard-coded length
        private static Object prefab;
        private static Material[] materials;

        public static void Initialize(GameObject _prefab, Material[] _materials)
        {
            prefab = _prefab;
            materials = _materials;

            for (var i = 0; i < transformPool.Length; i++)
            {
                transformPool[i] = ((GameObject)GameObject.Instantiate(prefab)).transform;
                meshPool[i] = transformPool[i].GetComponent<MeshRenderer>();
            }
        }

        public static void Clear()
        {
            for (var i = 0; i < transformPool.Length; i++)
            {
                GameObject.Destroy(transformPool[i].gameObject);
            }
        }

        public static void Run(ref Data data)
        {
            for (var i = 0; i < data.Vehicles.Length; i++)
            {
                if (!data.Vehicles[i].IsAlive)
                {
                    meshPool[i].enabled = false;
                    continue;
                }

                var position = data.Vehicles[i].Position;
                transformPool[i].position = new Vector3((float) position.x, 0, (float) position.y);
                meshPool[i].enabled = true;
                meshPool[i].material = materials[data.Vehicles[i].Team];
            }
        }
    }
}