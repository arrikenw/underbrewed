// Texture and tutorial by Evgeny Starostin - https://80.lv/articles/breakdown-magic-fire-effect-in-unity/

Shader "Custom/FireShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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

            uniform sampler2D _MainTex;      

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
                return o;
            }


            // Implementation of fragment shaders
            fixed4 frag(vertOut v) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, v.uv) * v.color;
                return col;
            }
            ENDCG
        }
    }
}