﻿Shader "Custom/NewBubble"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert addshadow 

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float3 vertex;
        };

		void vert(inout appdata_full v)
		{
			// r is radius of bubble		
			float r = 3.0f;
			// k is height of centre of the bubble
			float k = fmod(_Time[0] * 20, 3) - 3.0f;

			// calculate square values
			float sq_x = pow(v.vertex.x, 2);
			float sq_z = pow(v.vertex.z, 2);
			float sq_r = pow(r, 2);

			if (sq_x + sq_z < sq_r) {
				v.vertex.y = r * sqrt(1 - (sq_x / sq_r) - (sq_z / sq_r)) + k;
				if (v.vertex.y < 0)
				{
					v.vertex.y = 0;
				}
			};
		}

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			if (IN.vertex.y > 0.0f) 
			{
				 c *= 0.3f;
			};
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
