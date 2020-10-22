// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
// http://enemyhideout.com/2016/08/creating-a-whirlpool-shader/
Shader "Chill/WhirlPool"
{
  Properties
  {
    _MainTex("Texture", 2D) = "white" {}
    _MotionTex("Motion", 2D) = "black" {}
    _Speed("Speed", float) = 0
    _Swirliness("Swirliness", float) = .75
    _Rotation("Rotation", float) = 0
    _Swirl("Swirl", float) = 0
	_Color("Color", Color) = (1,1,1,1)

  }

  SubShader
  {
    Tags {
      "Queue" = "Transparent"
      "RenderType" = "Transparent"
      "PreviewType" = "Plane"
    }
    LOD 100
    ZWrite Off
	Blend SrcAlpha OneMinusSrcAlpha

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
        float4 vertex : SV_POSITION;
        float2 uv : TEXCOORD0;
      };

      sampler2D _MainTex;
      sampler2D _MotionTex;
      float _Speed;
      float _Rotation;
      float _Swirl;
      float _Swirliness;
	  half4 _Color;

      v2f vert(appdata v)
      {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        return o;
      }

      float2 rotate( float magnitude , float2 p )
      {
        float sinTheta = sin(magnitude);
        float cosTheta = cos(magnitude);
        float2x2 rotationMatrix = float2x2(cosTheta, -sinTheta, sinTheta, cosTheta);
        return mul(p, rotationMatrix);
      }

      fixed4 frag(v2f i) : SV_Target
      {
        fixed4 motion = tex2D(_MotionTex, i.uv);

        float2 p = i.uv - float2(0.5, 0.5);

		if (length(p) > 0.5)
		{
			return fixed4(0, 0, 0, 0);
		}
        // Rotate based upon direction
        p = rotate(_Swirl * (motion.r * _Time), p);
        p = rotate(_Rotation * _Time * _Speed, p);

        // get the angle of our points, and divide it in half
        float a = atan2(p.y , p.x ) * 0.5;
        // the square root of the dot product will convert our angle into our distance from center
        float r = sqrt(dot(p,p));
        float2 uv;
        // x is equal to the square root modified by:
        // _Speed: The speed at which the pool twists.
        // _Swirliness: How many 'rings' on the x we have.
        uv.x = (_Time * _Speed) - 1/(r + _Swirliness);
        // uv.x = r;
        uv.y = a/3.1416;

        // Now we can get our color.
        fixed4 fragColor = tex2D(_MainTex, uv) * _Color;

        return fragColor;
      }
      ENDCG
    }
  }
}