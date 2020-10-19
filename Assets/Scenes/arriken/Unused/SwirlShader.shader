// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/SwirlShader"
{
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_RotationSpeed("RotationSpeed", Float) = 2.0
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Standard fullforwardshadows vertex:vert
			#pragma target 3.0
			sampler2D _MainTex;
			struct Input {
				float2 uv_MainTex;
				float2 val;
			};
			float _RotationSpeed;
			void vert(inout appdata_full v, out Input o) {
				UNITY_INITIALIZE_OUTPUT(Input,o);
				float sinX = sin(_RotationSpeed * _Time.y);
				float cosX = cos(_RotationSpeed * _Time.y);
				float2x2 rotationMatrix = float2x2(cosX, -sinX, sinX, cosX);
				o.val.xy = mul(v.texcoord.xy - 0.5, rotationMatrix) + 0.5;
			}

			fixed4 _Color;
			void surf(Input IN, inout SurfaceOutputStandard o) {
				fixed4 c = tex2D(_MainTex, IN.val) * _Color;
				o.Albedo = c.rgb;
			}
			ENDCG
		 }
			 FallBack "Diffuse"
}
