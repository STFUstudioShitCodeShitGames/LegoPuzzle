    using System;
    using UnityEngine;

    [RequireComponent(typeof(BoxCollider2D))]
    public class Chastichka : MonoBehaviour
    {
        public event Action<Chastichka> Voshel; 

        [HideInInspector] public int Hus;
        private SpriteRenderer _sus;
        public SpriteRenderer Sus => _sus ??= GetComponent<SpriteRenderer>();
        
        public Vector2 AwukePos { get; set; }

        public bool Zamen => !Sus.enabled;
        
        private Vector2 _priol;

        private void Start()
        {
            var suuh = GetComponent<BoxCollider2D>();
            suuh.size = _sus.size;
        }

        private void OnMouseDown()
        {
            if (Zamen)
                return;
            
            Voshel?.Invoke(this);
        }
    }
