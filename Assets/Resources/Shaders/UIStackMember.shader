Shader "UI/UIStackMember"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BulgeStrength ("Bulge Strength", Float) = 0.1
        _BulgePower ("Bulge Power", Float) = 2.0

        // Required by Unity UI
        _Color ("Tint", Color) = (1,1,1,1)
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
        _ClipRect ("Clip Rect", Vector) = (0,0,0,0)
        _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags 
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "UI Bulge"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _ClipRect;
            float _UseUIAlphaClip;

            float _BulgeStrength;
            float _BulgePower;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float2 worldPos : TEXCOORD1;
            };

            v2f vert (appdata v)
{
    v2f o;

    float4 pos = v.vertex;

    // -------------------------------------------------------
    // BULGE LOGIC (Adjusted Falloff Curve)
    // -------------------------------------------------------

    // 1. Calculate the UV-centered vector.
    float2 uvCentered = v.uv * 2.0 - 1.0;
    
    // 2. Calculate distance (d) from center for the falloff.
    float d = length(uvCentered); 
    
    // *** FIX: Apply a small offset to the distance before powering ***
    // This lifts the falloff curve, ensuring more displacement near the center.
    // Adjust 0.1 to control how wide the unaffected region is (smaller value = tighter center effect).
    float falloffOffset = 0.1; 
    float effectiveDistance = d + falloffOffset;

    // 3. Falloff: Use the effective distance.
    float bulgeFactor = pow(saturate(effectiveDistance*effectiveDistance), _BulgePower);

    // Deformation magnitude (in local space)
    float amount = bulgeFactor * _BulgeStrength; 

    // 4. APPLY DISPLACEMENT
    float2 displacementDirection = float2(0, 0); 

    if (d > 0.0001) // Keep the safety check for the absolute center vertex
    {
        // Use the normalized UV-centered vector for the base direction.
        displacementDirection = normalize(uvCentered);

        // MANUAL X-DIRECTION FLIP 
        displacementDirection.x *= -1.0; 
    }

    // Apply displacement
    pos.xy += displacementDirection * amount;
    
    // -------------------------------------------------------
    // Regular UI setup
    // -------------------------------------------------------
    o.vertex = UnityObjectToClipPos(pos);
    o.worldPos = pos.xy;
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    o.color = v.color * _Color;

    return o;
}

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;

                // UI masking
                col.a *= UnityGet2DClipping(i.worldPos, _ClipRect);

                if (_UseUIAlphaClip && col.a < 0.001)
                    discard;

                return col;
            }
            ENDCG
        }
    }
}
