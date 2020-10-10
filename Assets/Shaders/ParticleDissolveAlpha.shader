// Textures by Evgeny Starostin - https://80.lv/articles/breakdown-magic-fire-effect-in-unity/

Shader "Custom/UnlitDissolveAlpha"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MaskTex("Texture", 2D) = "white" {}
        _BlendFct("Blend Factor", Float) = 0.8

    }
    SubShader
    {
        Blend SrcAlpha OneMinusSrcAlpha
        
        ZWrite Off
            
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            uniform sampler2D _MainTex;
            uniform sampler2D _MaskTex;

            uniform float _BlendFct;

            struct vertIn
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float3 uv : TEXCOORD0;
            };

            struct vertOut
            {
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
                float3 uv : TEXCOORD0;
            };


            // Implenentation of vertex shader
            vertOut vert (vertIn v)
            {
                vertOut o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
        		o.uv = v.uv;
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }


            // Implementation of fragment shader
            fixed4 frag (vertOut v) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, v.uv);
                float dissolve = smoothstep(v.uv * (1 - _BlendFct), v.color * (1 + _BlendFct), _BlendFct);
                col *= dissolve * v.color;

                return col;
            }
            ENDCG
        }
    }
}