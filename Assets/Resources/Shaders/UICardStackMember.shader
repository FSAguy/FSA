Shader "UI/UIStackMember"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _MaskTex ("Mask Texture (R mask)", 2D) = "white" {}
        _FrameTex ("Frame Texture (RGBA)", 2D) = "white" {}

        _CropMin ("Crop Min (UV)", Vector) = (0.2091, 0.4276, 0, 0)
        _CropMax ("Crop Max (UV)", Vector) = (0.7917, 0.8541, 0, 0)

        _BulgePower ("Bulge Power", Float) = 1.5
        _BulgeIntensity ("Bulge Intensity", Float) = 0.8
        _BulgeScale ("Bulge Pixel Scale", Float) = 0.35

        _MaskColor ("Mask Color", Color) = (1,1,1,1)
        _Color ("Tint", Color) = (1,1,1,1)

        // UI mask/stencil helpers (used by Unity)
        _Stencil ("Stencil ID", Float) = 0
        _StencilComp ("Stencil Comparison", Float) = 8
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
        _ClipRect ("Clip Rect", Vector) = (0,0,0,0)
        _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="False" }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        ZWrite Off
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "UIBulgeMasked"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float4 _MainTex_ST;
            sampler2D _MaskTex;
            float4 _MaskTex_TexelSize;
            sampler2D _FrameTex;
            float4 _FrameTex_TexelSize;

            float4 _CropMin;
            float4 _CropMax;

            float _BulgePower;
            float _BulgeIntensity;
            float _BulgeScale;

            fixed4 _MaskColor;
            fixed4 _Color;

            float _UseUIAlphaClip;
            float4 _ClipRect;

            struct appdata
            {
                float4 vertex : POSITION;   // local UI vertex position (pixels)
                float2 uv     : TEXCOORD0;  // mesh UV 0..1 across subdivided quad
                float4 color  : COLOR;      // vertex tint (masking may use this)
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv     : TEXCOORD0;  // mesh uv
                float4 color  : COLOR;
                float2 worldPos : TEXCOORD1; // used for UnityGet2DClipping
            };

            // Convert UVs in mesh (0..1) to crop-space uv (0..1 across crop area)
            float2 UVtoCropUV(float2 meshUV)
            {
                return lerp(_CropMin.xy, _CropMax.xy, meshUV);
            }

            v2f vert (appdata v)
            {
                v2f o;

                // Map mesh UV to crop UV [0..1] across the crop in the source texture
                float2 uvCrop = UVtoCropUV(float2(1.0 - v.uv.x, v.uv.y));

                // Convert uvCrop to centered local coords in [-1..1] relative to crop center
                float2 cropCenter = (_CropMin.xy + _CropMax.xy) * 0.5;
                float2 cropSizeUV = _CropMax.xy - _CropMin.xy;
                float2 uvLocal = (uvCrop - cropCenter) / (cropSizeUV * 0.5); // now -1..1 across crop

                // radial distance (0 at center, ~1 at edges/corners)
                float r = length(uvLocal);

                // bulge falloff: increase with distance (corners move more)
                // power > 1 concentrates bulge toward corners, <1 more linear
                float bulgeFactor = pow(saturate(r), _BulgePower);

                // get texture resolution (pixels)
                float2 texSize = 1.0 / _MainTex_TexelSize.xy; // width, height in pixels

                // crop size in pixels
                float2 cropSizePx = cropSizeUV * texSize;

                // amount in pixel units: use normalized bulgeFactor * intensity * scale * max(cropSize)
                float amountPx = bulgeFactor * _BulgeIntensity * _BulgeScale * max(cropSizePx.x, cropSizePx.y);

                // outward direction in crop-local space (uvLocal)
                float2 outward = float2(0,0);
                if (r > 0.0001) outward = normalize(uvLocal);
                else outward = float2(0,0);

                // apply offset in local vertex coordinates (UI vertices are in local pixel units)
                // Note: v.vertex.xy is in local UI pixels (Rect transform units)
                // We need to map the pixel-space offset (amountPx) into the local vertex space.
                // The subdivided mesh was scaled to the RectTransform in OnPopulateMesh, so 1 unit = 1 pixel.
                float2 offsetLocal = outward * amountPx;

                float4 pos = v.vertex;
                pos.xy += offsetLocal;

                // Standard UI transforms
                o.vertex = UnityObjectToClipPos(pos);
                o.uv = v.uv;
                o.color = v.color * _Color;
                o.worldPos = pos.xy;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // map mesh UV -> crop UV -> sample
                float2 uvCrop = UVtoCropUV(i.uv);
                uvCrop.x = 1.0 - uvCrop.x;

                // sample main, mask and frame
                fixed4 mainCol = tex2D(_MainTex, uvCrop);
                fixed maskSample = tex2D(_MaskTex, uvCrop).r; // mask in R channel
                fixed4 frameCol = tex2D(_FrameTex, uvCrop);

                // apply mask (mask * maskColor) to main
                mainCol.rgb *= maskSample * _MaskColor.rgb;
                mainCol.a *= maskSample * _MaskColor.a;

                // composite frame over main (premultiplied blend not assumed; simple alpha lerp)
                fixed4 outCol = lerp(mainCol, frameCol, frameCol.a);

                // apply vertex tint & material tint
                outCol *= i.color;

                // RectMask2D support
                outCol.a *= UnityGet2DClipping(i.worldPos, _ClipRect);

                if (_UseUIAlphaClip > 0.5 && outCol.a < 0.001)
                    discard;

                return outCol;
            }

            ENDCG
        }
    }
}
