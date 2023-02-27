using System.Collections;
using UnityEngine;

namespace MagmaMc.Modding
{
    public class MarkAsDelete : MonoBehaviour
    {
        public void Awake() =>
            Destroy(gameObject);
    }
}