using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyFSM : StateMachine
{
        public WanderState wander;
        public FollowState follow;
        public FleeState flee;
        public float range = 5f;
        public int ammo = 3;
        public int maxAmmo = 3;
        public Transform spawnPoint;
        public GameObject arrow;
        public float throwForce;
        public float timeToShoot = 5.0f;
        public float time = 0;
        public float timeToReload = 5f;
        void Awake()
        {
            wander = new WanderState(this, gameObject);
            follow = new FollowState(this, gameObject);
            flee = new FleeState(this, gameObject);

        }
        protected override State GetInitialState()
        {
            return wander;
        }
        public GameObject InstantiateObject(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return Instantiate(prefab, position, rotation);
        }

        public Coroutine StartCoroutineFSM(IEnumerator coroutine)
        {
            return StartCoroutine(coroutine);
        }
    
}
