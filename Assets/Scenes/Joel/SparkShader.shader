// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/SparkShader"
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
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;
		fixed4 color : COLOR;
	};

	sampler2D _MainTex;
	float4 _MainTex_ST;

	v2f vert(appdata v)
	{
		v2f o;

		//geometry
		float y = 0.7*sin((v.vertex.x) + _Time.z * 10); //undulating / rippling effect to simulate a jagged bolt
		float x = 0.7*v.vertex.x + 0.3*(1 - (sin(_Time.z)))*v.vertex.x; //70% fixed length + 30% variable length
		float z = ((cos(_Time.z) + v.vertex.z) / 300) + (sin(v.vertex.z) / 200); // very thin base with slight variance over time + kinks in chain
		v.vertex = float4(x, y, z, 1);

		//colours
		float4 brightblue = float4(0.49, 0.98, 1, 1);
		float4 indigo = float4(75,0,130,1);
		fixed4 col = 1.6*0.005*abs(v.vertex.y)*indigo + 0.3*brightblue; //scale intensity based on height, brightest white is at peaks
		o.color = col;

		//default transformations
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		return i.color;
	}
		ENDCG
	}
	}
}
