using UnityEngine;

namespace Meteors
{
    public class Meteor : MonoBehaviour
    {
        public enum Type { BLUE, GREEN, RED };
        public Type type = Type.BLUE;
        private Animator animator;

        Vector3 direction = Vector3.zero;
        private static float SPEED = 5f;
        private static float DIR_RANGE_MAX = 0.5f;
        private static float DIR_RANGE_MIN = 0.3f;

        public string TypeToString(string prefix)
        {
            return type switch
            {
                Type.RED => prefix + "_Red",
                Type.GREEN => prefix + "_Green",
                Type.BLUE => prefix + "_Blue",
                _ => prefix + "_Unknown",
            };
        }

        #region Initialization
        public void Init(Vector3 pos, Vector2 spawnLoc)
        {
            animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
            SetType();

            transform.position = pos;
            RandomDirection(spawnLoc);
            RotateMeteor();
        }
        private void SetType()
        {
            int typesLength = System.Enum.GetValues(typeof(Type)).Length;
            type = (Type)Random.Range(0, typesLength);
            animator.SetInteger("Type", (int)type);
            animator.SetTrigger("Initialize");
        }
        private void RandomDirection(Vector2 spawnLoc)
        {
            bool isNegative = Random.value > 0.5f;
            float randFloat = Random.Range(DIR_RANGE_MIN, DIR_RANGE_MAX);
            randFloat = isNegative ? -randFloat : randFloat;
            switch (spawnLoc)
            {
                case Vector2 v when v.Equals(new Vector2(-1, 0)):
                    direction = new(1, randFloat);
                    break;
                case Vector2 v when v.Equals(new Vector2(1, 0)):
                    direction = new(-1, randFloat);
                    break;
                case Vector2 v when v.Equals(new Vector2(0, -1)):
                    direction = new(randFloat, 1);
                    break;
                default:
                    direction = new(randFloat, -1);
                    break;
            }
            direction.Normalize();
        }
        private void RotateMeteor()
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI;
            transform.Rotate(0f, 0f, angle);
        }
        #endregion

        private void Update()
        {
            transform.position += SPEED * Time.deltaTime * direction;
        }

        private void OnMouseDown()
        {
            EventManager.TriggerEvent(TypeToString("CollectMeteor"), 0);
            Destroy(gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("MeteorSpawnArea"))
            {
                Destroy(gameObject);
            }
        }
    }
}