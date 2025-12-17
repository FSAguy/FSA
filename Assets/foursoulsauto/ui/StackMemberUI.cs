using System;
using foursoulsauto.core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace foursoulsauto.ui
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class StackMemberUI : Graphic, IPointerEnterHandler, IPointerExitHandler
    {
        protected override void Start()
        {
            base.Start();

            material = Instantiate(material); // clone to avoid changing base values for all members
        }

        public event Action<StackMemberUI> PointerEntered;
        public event Action<StackMemberUI> PointerExited;

        public Mesh mesh;
        public Texture texture;
        
        public IVisualStackEffect Effect { get; set; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEntered?.Invoke(this);
        }
        
        public override Texture mainTexture => texture != null ? texture : s_WhiteTexture; 
        

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExited?.Invoke(this);
        }

        public void AnimateAboutToPop()
        {
            material.SetFloat("_BulgeIntensity", 20f);
        }

        public void AnimateDeflate()
        {
            material.SetFloat("_BulgeIntensity", 0f);
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
            if (mesh == null) return;

            // Rect dimensions in UI space
            Rect r = rectTransform.rect;
            float width = r.width;
            float height = r.height;

            // Get mesh bounds
            Bounds bounds = mesh.bounds;
            Vector3 size = bounds.size;

            // Scale factors to make mesh fit the rect
            float scaleX = width / size.x;
            float scaleY = height / size.y;
            Vector3 scale = new Vector3(scaleX, scaleY, 1f);

            var verts = mesh.vertices;
            var uvs = mesh.uv;
            var cols = mesh.colors.Length > 0 ? mesh.colors : null;
            var tris = mesh.triangles;

            for (int i = 0; i < verts.Length; i++)
            {
                UIVertex v = new UIVertex();

                Vector3 pos = verts[i];

                // Center pivot: shift mesh so bounds center sits at (0,0)
                pos -= bounds.center;

                // Scale into UI space
                pos = Vector3.Scale(pos, scale);

                v.position = pos;
                v.uv0 = uvs.Length > i ? uvs[i] : Vector2.zero;
                v.color = cols != null ? cols[i] : color;

                vh.AddVert(v);
            }

            // Triangles
            for (int i = 0; i < tris.Length; i += 3)
            {
                vh.AddTriangle(tris[i], tris[i + 1], tris[i + 2]);
            }
        }

    }
}
