// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/PoisonShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
		float3 normal: NORMAL;
		fixed4 color : COLOR;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;
		fixed4 color : COLOR;
	};

	sampler2D _MainTex;
	float4 _MainTex_ST;
	float4 _MainTex_TexelSize;

	v2f vert(appdata v)
	{
		v2f o;

		o.color = v.color;

		
		v.vertex = v.vertex + cos(float4(_Time.z, _Time.z, _Time.z, 1.0f)); //float4(0.025*sin(v.vertex.x + _Time.z) + 0.975*v.vertex.x, 0.025*sin(v.vertex.y + _Time.z) + 0.975*v.vertex.y, v.vertex.z, 1);
		
		//default transformations
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		return o;
	}


	//applies colour changes from swell
	fixed4 frag(v2f i) : SV_Target
	{
		//colours
		float4 red = float4(100.0f/255, 33.0f/255, 29.0f/255, 1);

		//grab texture colour
		fixed4 textureCol = tex2D(_MainTex, i.uv);

		//gain a hue as it swells
		//original colour is weighted more than swell colour
		fixed4 redCol = red*abs(sin(_Time.z));
		fixed4 finalCol = 0.2*textureCol + 0.4*redCol + 0.4*red;
		return finalCol;
	}
		ENDCG
	}
	}
}
