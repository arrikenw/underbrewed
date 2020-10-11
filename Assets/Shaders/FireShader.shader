// Textures by Evgeny Starostin - https://80.lv/articles/breakdown-magic-fire-effect-in-unity/

Shader "Custom/FireShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MaskTex("Texture", 2D) = "white" {}

    }
    SubShader
    {
        
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Opaque"}
        
        Blend SrcAlpha OneMinusSrcAlpha
        
        ZWrite Off
            
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _MainTex_ST;
            //float4 _MaskTex_ST;

            uniform sampler2D _MainTex;
            uniform sampler2D _MaskTex;

            uniform float _BlendFct = 0.2;

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


            // Implementation of fragment shaders
            fixed4 frag (vertOut v) : SV_Target
            { 
                fixed4 col = tex2D(_MainTex, v.uv);
                fixed4 dissolve = smoothstep(col * _BlendFct, v.color * (1.0f - _BlendFct), 100);

                col *= dissolve * v.color;

                return col;
            }
            ENDCG
        }
    }
}