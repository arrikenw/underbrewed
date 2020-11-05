// http://enemyhideout.com/2016/08/creating-a-whirlpool-shader/
Shader "Chill/LiquidSwirl"
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

	  // do nothing here
      v2f vert(appdata v)
      {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        return o;
      }

	  // rotates a point
      float2 rotate( float rotationAmount, float2 p)
      {
		  float a = atan2(p.y, p.x);
		  float r = length(p);

		  a += rotationAmount;

		  return float2(cos(a) * r, sin(a) * r);
      }

      fixed4 frag(v2f i) : SV_Target
      {
		// texture for how much to swirl (more swirl towards outside)
        fixed4 motion = tex2D(_MotionTex, i.uv);
		
		// find point position relative to middle of uv
        float2 p = i.uv - float2(0.5, 0.5);

		// if point is outside the circle, render it transparent
		if (length(p) > 0.5)
		{
			return fixed4(0, 0, 0, 0);
		}

		// For rotation

        // _Swirl is general swirl amount, motion.r increases swirl amount the further out it is
        p = rotate(_Rotation * _Time * _Speed, p);
		p = rotate(_Swirl * (motion.a * _Time), p);

		// For texture moving towards center
		float2 uv;
        
		uv.x = (_Time[0] * _Speed) - (1 / (length(p) + _Swirliness));
		// angle of new point
		float a = atan2(p.y, p.x);
		// divide angle by two pi to get angle in radians scaled to between 0-1
        uv.y = a/(3.1416 * 2);

        // Now we can get our color.
        fixed4 fragColor = tex2D(_MainTex, uv) * _Color;

        return fragColor;
      }
      ENDCG
    }
  }
}