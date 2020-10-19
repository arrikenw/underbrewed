// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/SwellShader"
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

		//swelling effect, pulsates over time
		float upperbound = 1*0.1;
		float swell = abs(sin(_Time.z))*upperbound;
		v.vertex.xyz = v.vertex.xyz + (v.normal.xyz * swell);

		o.color = v.color;

		//default transformations
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		return o;
	}


	//applies colour changes from swell
	fixed4 frag(v2f i) : SV_Target
	{
		//colours
		float4 indigo = float4(75.0 / 255,0,130.0 / 255,1);
		fixed4 green = float4(0,1,0,1);

		/*get colour from texture based on camera position
		//kinda cool, maybe try to find a use for it
		//https://answers.unity.com/questions/1498163/can-i-sample-a-texture-in-a-fragment-shader-using.html
		float2 new_uv = (i.vertex) * _MainTex_TexelSize.xy;
		fixed4 textureCol = tex2D(_MainTex, new_uv); */

		//grab texture colour
		fixed4 textureCol = tex2D(_MainTex, i.uv);

		//gain a hue as it swells
		//original colour is weighted more than swell colour
		fixed4 swellCol = green*abs(sin(_Time.z));
		fixed4 finalCol = 0.6*textureCol + 0.4*swellCol;
		return finalCol;
	}
		ENDCG
	}
	}
}
