using UnityEngine;

namespace ProjectSurvivor
{
	public partial class HitHurtBox : GameplayObject
	{
		public GameObject Owner;

		private void Awake()
		{
			mCollider2D = GetComponent<Collider2D>();
		}

		void Start()
		{
			if (!Owner)
			{
				Owner = transform.parent.gameObject;
			}

		}

		private Collider2D mCollider2D;
		protected override Collider2D Collider2D => mCollider2D;
	}
}
